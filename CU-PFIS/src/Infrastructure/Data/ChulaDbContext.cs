using System.Reflection;
using Chula.Application.Common.Interfaces;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Entities.Integration;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Security;
using Chula.Domain.Entities.Students;
using Chula.Domain.Entities.Training;
using Microsoft.EntityFrameworkCore;

namespace Chula.Infrastructure.Data;

public sealed class ChulaDbContext : DbContext, IChulaDbContext
{
    public ChulaDbContext(DbContextOptions<ChulaDbContext> options)
        : base(options)
    {
    }

    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Major> Majors => Set<Major>();

    public DbSet<AcademicPosition> AcademicPositions => Set<AcademicPosition>();

    public DbSet<AdmissionRound> AdmissionRounds => Set<AdmissionRound>();

    public DbSet<DocumentTemplate> DocumentTemplates => Set<DocumentTemplate>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<PdpaConsent> PdpaConsents => Set<PdpaConsent>();

    public DbSet<FacultyMember> FacultyMembers => Set<FacultyMember>();

    public DbSet<FacultyEducation> FacultyEducations => Set<FacultyEducation>();

    public DbSet<FacultyPublication> FacultyPublications => Set<FacultyPublication>();

    public DbSet<Student> Students => Set<Student>();

    public DbSet<StudentInternship> StudentInternships => Set<StudentInternship>();

    public DbSet<ImportBatch> ImportBatches => Set<ImportBatch>();

    public DbSet<DataConflictQueueItem> DataConflictQueueItems => Set<DataConflictQueueItem>();

    public DbSet<TrainingBudgetPolicy> TrainingBudgetPolicies => Set<TrainingBudgetPolicy>();

    public DbSet<FacultyTrainingBudgetAllocation> FacultyTrainingBudgetAllocations => Set<FacultyTrainingBudgetAllocation>();

    public DbSet<TrainingRequest> TrainingRequests => Set<TrainingRequest>();

    public DbSet<TrainingApprovalHistory> TrainingApprovalHistories => Set<TrainingApprovalHistory>();

    public DbSet<TrainingBudgetLedgerEntry> TrainingBudgetLedgerEntries => Set<TrainingBudgetLedgerEntry>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
