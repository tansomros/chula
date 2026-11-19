using Chula.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.AcademicYears.Commands.CreateAcademicYear;

public sealed class CreateAcademicYearCommandValidator : AbstractValidator<CreateAcademicYearCommand>
{
    private readonly IChulaDbContext _context;

    public CreateAcademicYearCommandValidator(IChulaDbContext context)
    {
        _context = context;

        RuleFor(v => v.YearBe)
            .InclusiveBetween(2500, 2700)
            .WithMessage("ปีการศึกษา (พ.ศ.) ต้องอยู่ในช่วง 2500 ถึง 2700")
            .MustAsync(BeUniqueYearBe)
            .WithMessage("ปีการศึกษานี้มีอยู่ในระบบแล้ว");

        RuleFor(v => v.StartDate)
            .NotEmpty()
            .WithMessage("กรุณาระบุวันเริ่มต้นปีการศึกษา");

        RuleFor(v => v.EndDate)
            .NotEmpty()
            .WithMessage("กรุณาระบุวันสิ้นสุดปีการศึกษา")
            .GreaterThanOrEqualTo(v => v.StartDate)
            .WithMessage("วันสิ้นสุดปีการศึกษาต้องมากกว่าหรือเท่ากับวันเริ่มต้นปีการศึกษา");
    }

    private async Task<bool> BeUniqueYearBe(int yearBe, CancellationToken cancellationToken)
    {
        return !await _context.AcademicYears
            .AnyAsync(x => x.YearBe == yearBe, cancellationToken);
    }
}
