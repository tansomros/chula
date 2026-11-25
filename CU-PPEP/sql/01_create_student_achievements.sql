-- =============================================================================
-- CU-PPEP (chula_pps) Table: StudentAchievements (ผลงานนิสิต)
-- Naming convention: New tables in chula_pps use clean PascalCase domain names (NO PFIS_ prefix).
-- Supports student self-entry in Chula-PPS and incremental read-only sync to CU-PFIS (D11, A02, A08, A15)
-- =============================================================================

IF OBJECT_ID('dbo.StudentAchievements', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.StudentAchievements (
        AchievementId           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        StudentCode             NVARCHAR(30)   NOT NULL,
        AcademicYearBe          INT            NOT NULL,
        StudyYear               INT            NOT NULL,
        ActivityType            NVARCHAR(100)  NULL,
        EventName               NVARCHAR(500)  NOT NULL,
        EventNameEn             NVARCHAR(500)  NULL,
        AwardName               NVARCHAR(300)  NOT NULL,
        AwardNameEn             NVARCHAR(300)  NULL,
        OrganizerName           NVARCHAR(300)  NULL,
        EventDate               DATE           NOT NULL,
        GrantAmount             DECIMAL(18,2)  NULL,
        Remark                  NVARCHAR(1000) NULL,
        IsSelfConfirmed         BIT            NOT NULL CONSTRAINT DF_StudentAchievements_IsSelfConfirmed DEFAULT (1),
        IsDeleted               BIT            NOT NULL CONSTRAINT DF_StudentAchievements_IsDeleted DEFAULT (0),
        CreatedBy               NVARCHAR(50)   NULL,
        UpdatedBy               NVARCHAR(50)   NULL,
        CreatedAt               DATETIME2(3)   NOT NULL CONSTRAINT DF_StudentAchievements_CreatedAt DEFAULT (SYSUTCDATETIME()),
        UpdatedAt               DATETIME2(3)   NOT NULL CONSTRAINT DF_StudentAchievements_UpdatedAt DEFAULT (SYSUTCDATETIME())
    );

    CREATE INDEX IX_StudentAchievements_StudentCode
        ON dbo.StudentAchievements (StudentCode, IsDeleted, EventDate DESC);

    CREATE INDEX IX_StudentAchievements_UpdatedAt
        ON dbo.StudentAchievements (UpdatedAt);
END
GO
