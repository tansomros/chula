using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhase2TrainingAndBudgetLedger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "training_budget_policies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fiscal_year_be = table.Column<int>(type: "integer", nullable: false),
                    fiscal_year_ce = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    annual_quota_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    allow_future_quota_borrowing = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    max_borrow_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_budget_policies", x => x.id);
                    table.CheckConstraint("ck_training_budget_policies_borrow_nonneg", "max_borrow_amount >= 0");
                    table.CheckConstraint("ck_training_budget_policies_quota_nonneg", "annual_quota_amount >= 0");
                });

            migrationBuilder.CreateTable(
                name: "training_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faculty_member_id = table.Column<int>(type: "integer", nullable: false),
                    fiscal_year_be = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    work_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    title_th = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    title_en = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    session_topic_th = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    session_topic_en = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    organizer_and_venue_th = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    organizer_and_venue_en = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    requested_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    approved_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    bank_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    bank_account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    bank_account_holder_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    revision_note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    submitted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    verified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    approved_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    source_system = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    source_record_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    imported_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("pk_training_requests", x => x.id);
                    table.CheckConstraint("ck_training_requests_approved_nonneg", "approved_amount >= 0");
                    table.CheckConstraint("ck_training_requests_date_range", "end_date >= start_date");
                    table.CheckConstraint("ck_training_requests_requested_nonneg", "requested_amount >= 0");
                    table.ForeignKey(
                        name: "fk_training_requests_faculty_members_faculty_member_id",
                        column: x => x.faculty_member_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "training_approval_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    training_request_id = table.Column<int>(type: "integer", nullable: false),
                    from_status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    to_status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    actor_login = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    action_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_approval_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_training_approval_histories_training_requests_training_requ",
                        column: x => x.training_request_id,
                        principalTable: "training_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "training_budget_ledger_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faculty_member_id = table.Column<int>(type: "integer", nullable: false),
                    training_request_id = table.Column<int>(type: "integer", nullable: true),
                    fiscal_year_be = table.Column<int>(type: "integer", nullable: false),
                    source_fiscal_year_be = table.Column<int>(type: "integer", nullable: true),
                    entry_type = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    effective_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    recorded_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_budget_ledger_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_training_budget_ledger_entries_faculty_members_faculty_memb",
                        column: x => x.faculty_member_id,
                        principalTable: "faculty_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_training_budget_ledger_entries_training_requests_training_r",
                        column: x => x.training_request_id,
                        principalTable: "training_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_training_approval_histories_external_id",
                table: "training_approval_histories",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_training_approval_histories_training_request_id_action_at",
                table: "training_approval_histories",
                columns: new[] { "training_request_id", "action_at" });

            migrationBuilder.CreateIndex(
                name: "ix_training_budget_ledger_entries_external_id",
                table: "training_budget_ledger_entries",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_training_budget_ledger_entries_faculty_member_id_fiscal_yea",
                table: "training_budget_ledger_entries",
                columns: new[] { "faculty_member_id", "fiscal_year_be" });

            migrationBuilder.CreateIndex(
                name: "ix_training_budget_ledger_entries_training_request_id",
                table: "training_budget_ledger_entries",
                column: "training_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_training_budget_policies_external_id",
                table: "training_budget_policies",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_training_budget_policies_fiscal_year_be",
                table: "training_budget_policies",
                column: "fiscal_year_be",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_training_requests_external_id",
                table: "training_requests",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_training_requests_faculty_member_id_fiscal_year_be",
                table: "training_requests",
                columns: new[] { "faculty_member_id", "fiscal_year_be" });

            migrationBuilder.CreateIndex(
                name: "ix_training_requests_fiscal_year_be_status",
                table: "training_requests",
                columns: new[] { "fiscal_year_be", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_training_requests_source_record_id",
                table: "training_requests",
                column: "source_record_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "training_approval_histories");

            migrationBuilder.DropTable(
                name: "training_budget_ledger_entries");

            migrationBuilder.DropTable(
                name: "training_budget_policies");

            migrationBuilder.DropTable(
                name: "training_requests");
        }
    }
}
