using Chula.Domain.Common;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Security;
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

    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
