-- =============================================================================
-- CU-PPEP (chula_pps) Read-Only Integration Views for CU-PFIS (Decision D11)
-- Verified against live chula_pps schema (View_Student_Bio, Students, Persons).
-- =============================================================================

CREATE OR ALTER VIEW dbo.vw_pfis_students AS
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
GO

CREATE OR ALTER VIEW dbo.vw_pfis_teachers AS
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
GO
