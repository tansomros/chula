using FluentValidation;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.ImportScopusPublications;

public sealed class ImportScopusPublicationsValidator : AbstractValidator<ImportScopusPublicationsCommand>
{
    public ImportScopusPublicationsValidator()
    {
        RuleFor(x => x.FacultyMemberExternalId)
            .NotEmpty();

        RuleFor(x => x.Publications)
            .NotEmpty();

        RuleForEach(x => x.Publications).ChildRules(pub =>
        {
            pub.RuleFor(p => p.Title).NotEmpty().MaximumLength(500);
            pub.RuleFor(p => p.PublishedYear).InclusiveBetween(1950, 2600);
        });
    }
}
