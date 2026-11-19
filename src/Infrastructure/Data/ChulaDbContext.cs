using System.Reflection;
using Chula.Application.Common.Interfaces;
using Chula.Domain.Common;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Security;
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

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
