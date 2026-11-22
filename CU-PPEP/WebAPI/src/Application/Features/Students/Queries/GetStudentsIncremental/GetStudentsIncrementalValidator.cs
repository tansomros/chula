using FluentValidation;

namespace CUPPEP.Application.Features.Students.Queries.GetStudentsIncremental;

public sealed class GetStudentsIncrementalValidator : AbstractValidator<GetStudentsIncrementalQuery>
{
    public GetStudentsIncrementalValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 1000);
    }
}
