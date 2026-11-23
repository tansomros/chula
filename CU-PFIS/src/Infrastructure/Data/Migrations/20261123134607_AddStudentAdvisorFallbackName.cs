using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentAdvisorFallbackName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "academic_advisor_name_en",
                table: "students",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "academic_advisor_name_th",
                table: "students",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "academic_advisor_name_en",
                table: "students");

            migrationBuilder.DropColumn(
                name: "academic_advisor_name_th",
                table: "students");
        }
    }
}
