using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyTrainingBudgetAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "faculty_training_budget_allocations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faculty_member_id = table.Column<int>(type: "integer", nullable: false),
                    fiscal_year_be = table.Column<int>(type: "integer", nullable: false),
                    annual_quota_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    max_borrow_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty_training_budget_allocations", x => x.id);
                    table.CheckConstraint("ck_faculty_budget_alloc_borrow_nonneg", "max_borrow_amount IS NULL OR max_borrow_amount >= 0");
                    table.CheckConstraint("ck_faculty_budget_alloc_quota_nonneg", "annual_quota_amount >= 0");
                    table.ForeignKey(
                        name: "fk_faculty_training_budget_allocations_faculty_members_faculty",
                        column: x => x.faculty_member_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_faculty_training_budget_allocations_external_id",
                table: "faculty_training_budget_allocations",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_faculty_training_budget_allocations_faculty_member_id_fisca",
                table: "faculty_training_budget_allocations",
                columns: new[] { "faculty_member_id", "fiscal_year_be" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "faculty_training_budget_allocations");
        }
    }
}
