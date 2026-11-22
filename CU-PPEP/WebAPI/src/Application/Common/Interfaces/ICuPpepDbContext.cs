using CUPPEP.Application.Features.Students.Queries.GetStudentByCode;
using CUPPEP.Domain.Entities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Common.Interfaces;

public partial interface ICuPpepDbContext
{
    DbSet<StudentProfileExt> StudentProfiles { get; }

    DbSet<StudentAchievementExt> StudentAchievements { get; }

    DbSet<StudentLicenseResultExt> StudentLicenseResults { get; }

    DbSet<StudentEmploymentRound1Ext> StudentEmploymentsRound1 { get; }

    Task<IReadOnlyList<CuppepPracticeHistoryDto>> GetPracticeHistoryByStudentCodeAsync(
        string studentCode,
        CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
