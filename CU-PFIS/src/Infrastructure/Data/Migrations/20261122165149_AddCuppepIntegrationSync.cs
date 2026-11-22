using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCuppepIntegrationSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "import_batches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_system = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_since_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    started_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    completed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    total_pulled_count = table.Column<int>(type: "integer", nullable: false),
                    created_count = table.Column<int>(type: "integer", nullable: false),
                    updated_count = table.Column<int>(type: "integer", nullable: false),
                    skipped_unchanged_count = table.Column<int>(type: "integer", nullable: false),
                    conflict_count = table.Column<int>(type: "integer", nullable: false),
                    error_message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    triggered_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_import_batches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "data_conflict_queue_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    import_batch_id = table.Column<int>(type: "integer", nullable: false),
                    source_system = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    target_external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    source_record_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    current_values_json = table.Column<string>(type: "text", nullable: false),
                    incoming_values_json = table.Column<string>(type: "text", nullable: false),
                    changed_fields_summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    resolved_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    resolved_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    resolution_note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_data_conflict_queue_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_data_conflict_queue_items_import_batches_import_batch_id",
                        column: x => x.import_batch_id,
                        principalTable: "import_batches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_data_conflict_queue_items_entity_type_status",
                table: "data_conflict_queue_items",
                columns: new[] { "entity_type", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_data_conflict_queue_items_external_id",
                table: "data_conflict_queue_items",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_data_conflict_queue_items_import_batch_id",
                table: "data_conflict_queue_items",
                column: "import_batch_id");

            migrationBuilder.CreateIndex(
                name: "ix_data_conflict_queue_items_target_external_id",
                table: "data_conflict_queue_items",
                column: "target_external_id");

            migrationBuilder.CreateIndex(
                name: "ix_import_batches_external_id",
                table: "import_batches",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_import_batches_source_system_entity_type_started_at",
                table: "import_batches",
                columns: new[] { "source_system", "entity_type", "started_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_conflict_queue_items");

            migrationBuilder.DropTable(
                name: "import_batches");
        }
    }
}
