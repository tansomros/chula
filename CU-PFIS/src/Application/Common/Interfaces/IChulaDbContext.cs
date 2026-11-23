using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Entities.Integration;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Security;
using Chula.Domain.Entities.Students;
using Chula.Domain.Entities.Training;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Common.Interfaces;

public interface IChulaDbContext
{
    DbSet<AcademicYear> AcademicYears { get; }

    DbSet<Department> Departments { get; }

    DbSet<Major> Majors { get; }

    DbSet<AcademicPosition> AcademicPositions { get; }

    DbSet<AdmissionRound> AdmissionRounds { get; }

    DbSet<DocumentTemplate> DocumentTemplates { get; }

    DbSet<Permission> Permissions { get; }

    DbSet<RolePermission> RolePermissions { get; }

    DbSet<PdpaConsent> PdpaConsents { get; }

    DbSet<FacultyMember> FacultyMembers { get; }

    DbSet<FacultyEducation> FacultyEducations { get; }

    DbSet<FacultyPublication> FacultyPublications { get; }

    DbSet<Student> Students { get; }

    DbSet<StudentInternship> StudentInternships { get; }

    DbSet<ImportBatch> ImportBatches { get; }

    DbSet<DataConflictQueueItem> DataConflictQueueItems { get; }

    DbSet<TrainingBudgetPolicy> TrainingBudgetPolicies { get; }

    DbSet<FacultyTrainingBudgetAllocation> FacultyTrainingBudgetAllocations { get; }

    DbSet<TrainingRequest> TrainingRequests { get; }

    DbSet<TrainingApprovalHistory> TrainingApprovalHistories { get; }

    DbSet<TrainingBudgetLedgerEntry> TrainingBudgetLedgerEntries { get; }

    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
