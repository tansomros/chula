using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialPhase1Foundation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "academic_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title_th = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    title_en = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    short_title_th = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    short_title_en = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_academic_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "academic_years",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year_be = table.Column<int>(type: "integer", nullable: false),
                    year_ce = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_current = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_academic_years", x => x.id);
                    table.CheckConstraint("ck_academic_years_date_order", "end_date >= start_date");
                });

            migrationBuilder.CreateTable(
                name: "admission_rounds",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_th = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name_en = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tier = table.Column<int>(type: "integer", nullable: false),
                    parent_round_id = table.Column<int>(type: "integer", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admission_rounds", x => x.id);
                    table.ForeignKey(
                        name: "fk_admission_rounds_admission_rounds_parent_round_id",
                        column: x => x.parent_round_id,
                        principalTable: "admission_rounds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entity_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    entity_external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    changed_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    changed_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    old_values_json = table.Column<string>(type: "jsonb", nullable: true),
                    new_values_json = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_th = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name_en = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document_templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    name_th = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    name_en = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    template_type = table.Column<int>(type: "integer", nullable: false),
                    file_name = table.Column<string>(type: "character varying(260)", maxLength: 260, nullable: false),
                    content_type = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    sha256hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    storage_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_document_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "majors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_th = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name_en = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_majors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pdpa_consents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    login_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    policy_version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    accepted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ip_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    user_agent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pdpa_consents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    module = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    name_th = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name_en = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "faculty_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personnel_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    professional_title_th = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    professional_title_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    first_name_th = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    last_name_th = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    first_name_en = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    last_name_en = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    academic_position_id = table.Column<int>(type: "integer", nullable: false),
                    administrative_positions_th = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    administrative_positions_en = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    department_id = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    scopus_author_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    orcid_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    last_cv_confirmed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_faculty_members_academic_positions_academic_position_id",
                        column: x => x.academic_position_id,
                        principalTable: "academic_positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_faculty_members_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "faculty_educations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faculty_member_id = table.Column<int>(type: "integer", nullable: false),
                    degree_th = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    degree_en = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    institution_th = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    institution_en = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    graduation_year = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty_educations", x => x.id);
                    table.ForeignKey(
                        name: "fk_faculty_educations_faculty_members_faculty_member_id",
                        column: x => x.faculty_member_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "faculty_publications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faculty_member_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    journal_or_venue = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    published_year = table.Column<int>(type: "integer", nullable: false),
                    doi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    scopus_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    source_system = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    source_record_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    imported_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty_publications", x => x.id);
                    table.ForeignKey(
                        name: "fk_faculty_publications_faculty_members_faculty_member_id",
                        column: x => x.faculty_member_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    student_code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    first_name_th = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    last_name_th = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    first_name_en = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    last_name_en = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cohort_academic_year_id = table.Column<int>(type: "integer", nullable: false),
                    study_year = table.Column<int>(type: "integer", nullable: false),
                    major_id = table.Column<int>(type: "integer", nullable: false),
                    department_id = table.Column<int>(type: "integer", nullable: true),
                    admission_round_id = table.Column<int>(type: "integer", nullable: true),
                    academic_advisor_id = table.Column<int>(type: "integer", nullable: true),
                    high_school_th = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    high_school_en = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    source_system = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    source_record_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    imported_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_staff_verified_edit = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.id);
                    table.CheckConstraint("ck_students_study_year_range", "study_year >= 1 AND study_year <= 8");
                    table.ForeignKey(
                        name: "fk_students_academic_years_cohort_academic_year_id",
                        column: x => x.cohort_academic_year_id,
                        principalTable: "academic_years",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_students_admission_rounds_admission_round_id",
                        column: x => x.admission_round_id,
                        principalTable: "admission_rounds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_students_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_students_faculty_members_academic_advisor_id",
                        column: x => x.academic_advisor_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_students_majors_major_id",
                        column: x => x.major_id,
                        principalTable: "majors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "student_internships",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    site_th = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    site_en = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    rotation_type_th = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    rotation_type_en = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    period_label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    source_record_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_internships", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_internships_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_academic_positions_code",
                table: "academic_positions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_academic_positions_external_id",
                table: "academic_positions",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_academic_years_external_id",
                table: "academic_years",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_academic_years_year_be",
                table: "academic_years",
                column: "year_be",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_admission_rounds_code",
                table: "admission_rounds",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_admission_rounds_external_id",
                table: "admission_rounds",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_admission_rounds_parent_round_id",
                table: "admission_rounds",
                column: "parent_round_id");

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_entity_name_entity_external_id",
                table: "audit_logs",
                columns: new[] { "entity_name", "entity_external_id" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_external_id",
                table: "audit_logs",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_departments_code",
                table: "departments",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_departments_external_id",
                table: "departments",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_document_templates_code",
                table: "document_templates",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_document_templates_external_id",
                table: "document_templates",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_educations_external_id",
                table: "faculty_educations",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_educations_faculty_member_id",
                table: "faculty_educations",
                column: "faculty_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_members_academic_position_id",
                table: "faculty_members",
                column: "academic_position_id");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_members_department_id",
                table: "faculty_members",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_members_external_id",
                table: "faculty_members",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_members_personnel_code",
                table: "faculty_members",
                column: "personnel_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_publications_external_id",
                table: "faculty_publications",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_publications_faculty_member_id_scopus_id",
                table: "faculty_publications",
                columns: new[] { "faculty_member_id", "scopus_id" });

            migrationBuilder.CreateIndex(
                name: "ix_majors_code",
                table: "majors",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_majors_external_id",
                table: "majors",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pdpa_consents_external_id",
                table: "pdpa_consents",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pdpa_consents_user_id_policy_version",
                table: "pdpa_consents",
                columns: new[] { "user_id", "policy_version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_external_id",
                table: "permissions",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_external_id",
                table: "role_permissions",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_name_permission_id",
                table: "role_permissions",
                columns: new[] { "role_name", "permission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_internships_external_id",
                table: "student_internships",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_internships_student_id",
                table: "student_internships",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_academic_advisor_id",
                table: "students",
                column: "academic_advisor_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_admission_round_id",
                table: "students",
                column: "admission_round_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_cohort_academic_year_id",
                table: "students",
                column: "cohort_academic_year_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_department_id",
                table: "students",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_external_id",
                table: "students",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_students_major_id",
                table: "students",
                column: "major_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_student_code",
                table: "students",
                column: "student_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs");

            migrationBuilder.DropTable(
                name: "document_templates");

            migrationBuilder.DropTable(
                name: "faculty_educations");

            migrationBuilder.DropTable(
                name: "faculty_publications");

            migrationBuilder.DropTable(
                name: "pdpa_consents");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "student_internships");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "academic_years");

            migrationBuilder.DropTable(
                name: "admission_rounds");

            migrationBuilder.DropTable(
                name: "faculty_members");

            migrationBuilder.DropTable(
                name: "majors");

            migrationBuilder.DropTable(
                name: "academic_positions");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
