using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Application.Features.Students.Queries.GetStudentByCode;
using CUPPEP.Domain.Entities.Extensions;
using CUPPEP.Infrastructure.Data.Configurations.Legacy;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Infrastructure.Data;

public sealed partial class CuPpepDbContext : DbContext, ICuPpepDbContext
{
    public CuPpepDbContext(DbContextOptions<CuPpepDbContext> options)
        : base(options)
    {
    }

    public DbSet<StudentProfileExt> StudentProfiles => Set<StudentProfileExt>();

    public DbSet<StudentAchievementExt> StudentAchievements => Set<StudentAchievementExt>();

    public DbSet<StudentLicenseResultExt> StudentLicenseResults => Set<StudentLicenseResultExt>();

    public DbSet<StudentEmploymentRound1Ext> StudentEmploymentsRound1 => Set<StudentEmploymentRound1Ext>();

    public async Task<IReadOnlyList<CuppepPracticeHistoryDto>> GetPracticeHistoryByStudentCodeAsync(
        string studentCode,
        CancellationToken cancellationToken)
    {
        var rows = await Database
            .SqlQuery<PracticeHistorySpRow>(
                $"EXEC dbo.RPT_Student_PracticeHistory @Student_Code = {studentCode}")
            .ToListAsync(cancellationToken);

        return rows
            .Select(h => new CuppepPracticeHistoryDto(
                h.PYear,
                h.LocationId,
                h.StudentCode ?? string.Empty,
                h.SubjectCode ?? string.Empty,
                h.LocationName,
                h.SubjectName))
            .ToList();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        LegacyTablesModelBuilder.Apply(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private sealed class PracticeHistorySpRow
    {
        public int PYear { get; set; }

        [Column("LocationID")]
        public int LocationId { get; set; }

        [Column("Student_Code")]
        public string? StudentCode { get; set; }

        public string? SubjectCode { get; set; }

        public string? LocationName { get; set; }

        public string? SubjectName { get; set; }
    }
}
