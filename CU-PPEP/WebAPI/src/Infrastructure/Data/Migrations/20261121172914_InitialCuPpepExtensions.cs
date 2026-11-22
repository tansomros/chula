using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CUPPEP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCuPpepExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "StudentAchievements",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AcademicYearBe = table.Column<int>(type: "int", nullable: false),
                    StudyYear = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AwardName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EventDate = table.Column<DateOnly>(type: "date", nullable: false),
                    GrantAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAchievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEmploymentRound1s",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GraduationYearBe = table.Column<int>(type: "int", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    WorkplaceName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SurveyAnswersJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEmploymentRound1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentLicenseResults",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PassedCc1 = table.Column<bool>(type: "bit", nullable: false),
                    PassedCc2 = table.Column<bool>(type: "bit", nullable: false),
                    PassedIp1 = table.Column<bool>(type: "bit", nullable: false),
                    PassedIp2 = table.Column<bool>(type: "bit", nullable: false),
                    PassedPc1 = table.Column<bool>(type: "bit", nullable: false),
                    PassedPc2 = table.Column<bool>(type: "bit", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLicenseResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfileExts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AdmissionRoundCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SeniorProjectTitleTh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SeniorProjectTitleEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfileExts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_ExternalId",
                schema: "dbo",
                table: "StudentAchievements",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_StudentCode",
                schema: "dbo",
                table: "StudentAchievements",
                column: "StudentCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEmploymentRound1s_ExternalId",
                schema: "dbo",
                table: "StudentEmploymentRound1s",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentEmploymentRound1s_StudentCode",
                schema: "dbo",
                table: "StudentEmploymentRound1s",
                column: "StudentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLicenseResults_ExternalId",
                schema: "dbo",
                table: "StudentLicenseResults",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLicenseResults_StudentCode",
                schema: "dbo",
                table: "StudentLicenseResults",
                column: "StudentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfileExts_ExternalId",
                schema: "dbo",
                table: "StudentProfileExts",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfileExts_StudentCode",
                schema: "dbo",
                table: "StudentProfileExts",
                column: "StudentCode",
                unique: true);

            migrationBuilder.Sql("""
                CREATE OR ALTER VIEW dbo.vw_students AS
                SELECT
                    CAST(v.Student_ID AS NVARCHAR(50))                                              AS SourceRecordId,
                    LTRIM(RTRIM(v.Student_Code))                                                    AS StudentCode,
                    v.Prefix                                                                        AS PrefixTh,
                    LTRIM(RTRIM(ISNULL(v.FirstName, '')))                                           AS FirstNameTh,
                    LTRIM(RTRIM(ISNULL(v.LastName, '')))                                            AS LastNameTh,
                    LTRIM(RTRIM(ISNULL(v.FirstNameEN, '')))                                         AS FirstNameEn,
                    LTRIM(RTRIM(ISNULL(v.LastNameEN, '')))                                          AS LastNameEn,
                    v.Email                                                                         AS Email,
                    v.MajorID                                                                       AS MajorId,
                    v.MajorName                                                                     AS MajorNameTh,
                    v.MinorID                                                                       AS MinorId,
                    v.MinorName                                                                     AS MinorNameTh,
                    v.AdvisorID                                                                     AS AdvisorId,
                    NULLIF(LTRIM(RTRIM(CONCAT(ISNULL(v.PrefixName, ''), ISNULL(v.Ad_Fname, ''), ' ', ISNULL(v.Ad_Lname, '')))), '')
                                                                                                    AS AdvisorNameTh,
                    NULLIF(LTRIM(RTRIM(CONCAT(ISNULL(sp.PrefixName, ''), ISNULL(sp.FirstName, ''), ' ', ISNULL(sp.LastName, '')))), '')
                                                                                                    AS SeniorProjectAdvisorNameTh,
                    v.HighSchool                                                                    AS HighSchoolTh,
                    v.HighProvince                                                                  AS HighSchoolProvinceTh,
                    v.HighYear                                                                      AS HighSchoolGradYear,
                    ISNULL(v.LevelClass, 1)                                                         AS StudyYear,
                    v.GPAX                                                                          AS Gpax,
                    v.StudentStatus                                                                 AS StudentStatusCode,
                    v.StatusName                                                                    AS StudentStatusNameTh,
                    ISNULL(v.LastUpdate, v.CreateDate)                                              AS UpdatedAt
                FROM dbo.View_Student_Bio AS v
                LEFT JOIN dbo.Students AS st
                    ON st.Student_ID = v.Student_ID
                LEFT JOIN dbo.Persons AS sp
                    ON sp.PersonID = st.SeniorProjectID
                WHERE v.Student_Code IS NOT NULL AND LTRIM(RTRIM(v.Student_Code)) <> '';
                """);

            migrationBuilder.Sql("""
                CREATE OR ALTER VIEW dbo.vw_teachers AS
                SELECT
                    CAST(p.PersonID AS NVARCHAR(50))            AS SourceRecordId,
                    p.PersonID                                  AS PersonId,
                    LTRIM(RTRIM(ISNULL(p.FirstName, '')))       AS FirstNameTh,
                    LTRIM(RTRIM(ISNULL(p.LastName, '')))        AS LastNameTh,
                    p.PositionName                              AS PositionNameTh,
                    p.DepartmentUID                             AS DepartmentId,
                    p.DepartmentName                            AS DepartmentNameTh,
                    p.Email                                     AS Email,
                    p.Telephone                                 AS Telephone,
                    p.StatusFlag                                AS StatusFlag
                FROM dbo.Persons AS p
                WHERE p.PersonType = 'T';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.vw_students;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.vw_teachers;");

            migrationBuilder.DropTable(
                name: "StudentAchievements",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentEmploymentRound1s",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentLicenseResults",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentProfileExts",
                schema: "dbo");
        }
    }
}
