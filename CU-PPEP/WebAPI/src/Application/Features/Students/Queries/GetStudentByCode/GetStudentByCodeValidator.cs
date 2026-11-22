using FluentValidation;

namespace CUPPEP.Application.Features.Students.Queries.GetStudentByCode;

public sealed class GetStudentByCodeValidator : AbstractValidator<GetStudentByCodeQuery>
{
    public GetStudentByCodeValidator()
    {
        RuleFor(x => x.StudentCode)
            .NotEmpty()
            .MaximumLength(30);
    }
}
