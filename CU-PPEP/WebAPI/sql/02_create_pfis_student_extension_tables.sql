-- =============================================================================
-- CU-PPEP (chula_pps) Extension Tables for New Student Input Screens (A02, A07, A08)
-- Run when adding the new student self-entry WebForms pages in Chula-PPS.
-- All tables include UpdatedAt so CU-PFIS can pull incrementally (?updatedSince=).
-- =============================================================================

-- 1. Student Admission Round (2-tier) & Profile Supplement
IF OBJECT_ID('dbo.PFIS_StudentProfileExt', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PFIS_StudentProfileExt (
        StudentCode             NVARCHAR(30)  NOT NULL PRIMARY KEY,
        AdmissionRoundCode      NVARCHAR(50)  NULL,
        SeniorProjectTitleTh    NVARCHAR(500) NULL,
        SeniorProjectTitleEn    NVARCHAR(500) NULL,
        UpdatedAt               DATETIME2(3)  NOT NULL DEFAULT SYSUTCDATETIME()
    );
END
GO

-- 2. Student Achievements & Awards (A08: self-confirmed, no certificate upload)
IF OBJECT_ID('dbo.PFIS_StudentAchievements', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PFIS_StudentAchievements (
        AchievementId           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        StudentCode             NVARCHAR(30)  NOT NULL,
        AcademicYearBe          INT           NOT NULL,
        StudyYear               INT           NOT NULL,
        EventName               NVARCHAR(500) NOT NULL,
        AwardName               NVARCHAR(300) NOT NULL,
        EventDate               DATE          NOT NULL,
        GrantAmount             DECIMAL(18,2) NULL,
        IsDeleted               BIT           NOT NULL DEFAULT 0,
        CreatedAt               DATETIME2(3)  NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAt               DATETIME2(3)  NOT NULL DEFAULT SYSUTCDATETIME()
    );
    CREATE INDEX IX_PFIS_StudentAchievements_UpdatedAt ON dbo.PFIS_StudentAchievements (UpdatedAt);
END
GO

-- 3. Professional License Exam Checklist (A08: CC1, CC2, IP1, IP2, PC1, PC2 — no file upload)
IF OBJECT_ID('dbo.PFIS_StudentLicenseResults', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PFIS_StudentLicenseResults (
        StudentCode             NVARCHAR(30) NOT NULL PRIMARY KEY,
        PassedCc1               BIT          NOT NULL DEFAULT 0,
        PassedCc2               BIT          NOT NULL DEFAULT 0,
        PassedIp1               BIT          NOT NULL DEFAULT 0,
        PassedIp2               BIT          NOT NULL DEFAULT 0,
        PassedPc1               BIT          NOT NULL DEFAULT 0,
        PassedPc2               BIT          NOT NULL DEFAULT 0,
        UpdatedAt               DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME()
    );
    CREATE INDEX IX_PFIS_StudentLicenseResults_UpdatedAt ON dbo.PFIS_StudentLicenseResults (UpdatedAt);
END
GO

-- 4. Graduate Employment Round 1 (Faculty Survey in CU-PPEP per A07)
IF OBJECT_ID('dbo.PFIS_StudentEmploymentRound1', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PFIS_StudentEmploymentRound1 (
        StudentCode             NVARCHAR(30)  NOT NULL PRIMARY KEY,
        GraduationYearBe        INT           NOT NULL,
        EmploymentStatus        NVARCHAR(100) NOT NULL,
        JobTitle                NVARCHAR(300) NULL,
        WorkplaceName           NVARCHAR(300) NULL,
        SurveyAnswersJson       NVARCHAR(MAX) NULL,
        UpdatedAt               DATETIME2(3)  NOT NULL DEFAULT SYSUTCDATETIME()
    );
    CREATE INDEX IX_PFIS_StudentEmploymentRound1_UpdatedAt ON dbo.PFIS_StudentEmploymentRound1 (UpdatedAt);
END
GO
