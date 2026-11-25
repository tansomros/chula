Imports System.Data
Imports System.Data.SqlClient

Public Class StudentAchievementController
    Inherits ApplicationBaseClass

    Private Shared _schemaEnsured As Boolean = False
    Private Shared ReadOnly _schemaLock As New Object()

    Private Sub EnsureSchema()
        If _schemaEnsured Then Exit Sub

        SyncLock _schemaLock
            If _schemaEnsured Then Exit Sub

            Const ddl As String =
                "IF OBJECT_ID('dbo.StudentAchievements', 'U') IS NULL " &
                "BEGIN " &
                "    CREATE TABLE dbo.StudentAchievements (" &
                "        AchievementId INT IDENTITY(1,1) NOT NULL PRIMARY KEY," &
                "        StudentCode NVARCHAR(30) NOT NULL," &
                "        AcademicYearBe INT NOT NULL," &
                "        StudyYear INT NOT NULL," &
                "        ActivityType NVARCHAR(100) NULL," &
                "        EventName NVARCHAR(500) NOT NULL," &
                "        EventNameEn NVARCHAR(500) NULL," &
                "        AwardName NVARCHAR(300) NOT NULL," &
                "        AwardNameEn NVARCHAR(300) NULL," &
                "        OrganizerName NVARCHAR(300) NULL," &
                "        EventDate DATE NOT NULL," &
                "        GrantAmount DECIMAL(18,2) NULL," &
                "        Remark NVARCHAR(1000) NULL," &
                "        IsSelfConfirmed BIT NOT NULL CONSTRAINT DF_StudentAchievements_IsSelfConfirmed DEFAULT (1)," &
                "        IsDeleted BIT NOT NULL CONSTRAINT DF_StudentAchievements_IsDeleted DEFAULT (0)," &
                "        CreatedBy NVARCHAR(50) NULL," &
                "        UpdatedBy NVARCHAR(50) NULL," &
                "        CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_StudentAchievements_CreatedAt DEFAULT (SYSUTCDATETIME())," &
                "        UpdatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_StudentAchievements_UpdatedAt DEFAULT (SYSUTCDATETIME())" &
                "    );" &
                "    CREATE INDEX IX_StudentAchievements_StudentCode ON dbo.StudentAchievements (StudentCode, IsDeleted, EventDate DESC);" &
                "    CREATE INDEX IX_StudentAchievements_UpdatedAt ON dbo.StudentAchievements (UpdatedAt);" &
                "END"

            Try
                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()
                    Using cmd As New SqlCommand(ddl, conn)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                _schemaEnsured = True
            Catch
                ' Ignore if DB user lacks DDL permission and table was already provisioned by DBA script
            End Try
        End SyncLock
    End Sub

    Public Function Achievement_GetSearch(
        ByVal studentCode As String,
        ByVal academicYearBe As Integer,
        ByVal activityType As String,
        ByVal keyword As String) As DataTable

        EnsureSchema()

        Dim dt As New DataTable()
        Const sqlText As String =
            "SELECT " &
            "    a.AchievementId, " &
            "    a.StudentCode, " &
            "    LTRIM(RTRIM(CONCAT(ISNULL(st.Prefix, N''), ISNULL(st.FirstName, N''), N' ', ISNULL(st.LastName, N'')))) AS StudentName, " &
            "    ISNULL(m.MajorName, N'') AS MajorName, " &
            "    a.AcademicYearBe, " &
            "    a.StudyYear, " &
            "    ISNULL(a.ActivityType, N'') AS ActivityType, " &
            "    a.EventName, " &
            "    ISNULL(a.EventNameEn, N'') AS EventNameEn, " &
            "    a.AwardName, " &
            "    ISNULL(a.AwardNameEn, N'') AS AwardNameEn, " &
            "    ISNULL(a.OrganizerName, N'') AS OrganizerName, " &
            "    a.EventDate, " &
            "    a.GrantAmount, " &
            "    ISNULL(a.Remark, N'') AS Remark, " &
            "    a.IsSelfConfirmed, " &
            "    a.CreatedAt, " &
            "    a.UpdatedAt " &
            "FROM dbo.StudentAchievements AS a " &
            "LEFT JOIN dbo.Students AS st ON LTRIM(RTRIM(st.Student_Code)) = a.StudentCode " &
            "LEFT JOIN dbo.Major AS m ON m.MajorID = st.MajorID " &
            "WHERE a.IsDeleted = 0 " &
            "  AND (@StudentCode = N'' OR a.StudentCode = @StudentCode) " &
            "  AND (@AcademicYearBe = 0 OR a.AcademicYearBe = @AcademicYearBe) " &
            "  AND (@ActivityType = N'' OR a.ActivityType = @ActivityType) " &
            "  AND (" &
            "        @Keyword = N'' " &
            "        OR a.EventName LIKE N'%' + @Keyword + N'%' " &
            "        OR ISNULL(a.EventNameEn, N'') LIKE N'%' + @Keyword + N'%' " &
            "        OR a.AwardName LIKE N'%' + @Keyword + N'%' " &
            "        OR ISNULL(a.AwardNameEn, N'') LIKE N'%' + @Keyword + N'%' " &
            "        OR ISNULL(a.OrganizerName, N'') LIKE N'%' + @Keyword + N'%' " &
            "        OR a.StudentCode LIKE N'%' + @Keyword + N'%' " &
            "        OR ISNULL(st.FirstName, N'') LIKE N'%' + @Keyword + N'%' " &
            "        OR ISNULL(st.LastName, N'') LIKE N'%' + @Keyword + N'%' " &
            "      ) " &
            "ORDER BY a.AcademicYearBe DESC, a.EventDate DESC, a.AchievementId DESC;"

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(sqlText, conn)
                cmd.Parameters.Add("@StudentCode", SqlDbType.NVarChar, 30).Value = If(studentCode, "").Trim()
                cmd.Parameters.Add("@AcademicYearBe", SqlDbType.Int).Value = academicYearBe
                cmd.Parameters.Add("@ActivityType", SqlDbType.NVarChar, 100).Value = If(activityType, "").Trim()
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = If(keyword, "").Trim()

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function Achievement_GetById(
        ByVal achievementId As Integer,
        Optional ByVal ownerStudentCode As String = "") As DataTable

        EnsureSchema()

        Dim dt As New DataTable()
        Const sqlText As String =
            "SELECT " &
            "    a.AchievementId, " &
            "    a.StudentCode, " &
            "    LTRIM(RTRIM(CONCAT(ISNULL(st.Prefix, N''), ISNULL(st.FirstName, N''), N' ', ISNULL(st.LastName, N'')))) AS StudentName, " &
            "    ISNULL(m.MajorName, N'') AS MajorName, " &
            "    ISNULL(st.LevelClass, 1) AS CurrentLevelClass, " &
            "    a.AcademicYearBe, " &
            "    a.StudyYear, " &
            "    ISNULL(a.ActivityType, N'') AS ActivityType, " &
            "    a.EventName, " &
            "    ISNULL(a.EventNameEn, N'') AS EventNameEn, " &
            "    a.AwardName, " &
            "    ISNULL(a.AwardNameEn, N'') AS AwardNameEn, " &
            "    ISNULL(a.OrganizerName, N'') AS OrganizerName, " &
            "    a.EventDate, " &
            "    a.GrantAmount, " &
            "    ISNULL(a.Remark, N'') AS Remark, " &
            "    a.IsSelfConfirmed, " &
            "    a.CreatedAt, " &
            "    a.UpdatedAt " &
            "FROM dbo.StudentAchievements AS a " &
            "LEFT JOIN dbo.Students AS st ON LTRIM(RTRIM(st.Student_Code)) = a.StudentCode " &
            "LEFT JOIN dbo.Major AS m ON m.MajorID = st.MajorID " &
            "WHERE a.AchievementId = @AchievementId " &
            "  AND a.IsDeleted = 0 " &
            "  AND (@OwnerStudentCode = N'' OR a.StudentCode = @OwnerStudentCode);"

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(sqlText, conn)
                cmd.Parameters.Add("@AchievementId", SqlDbType.Int).Value = achievementId
                cmd.Parameters.Add("@OwnerStudentCode", SqlDbType.NVarChar, 30).Value = If(ownerStudentCode, "").Trim()

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function Achievement_Save(
        ByVal achievementId As Integer,
        ByVal studentCode As String,
        ByVal academicYearBe As Integer,
        ByVal studyYear As Integer,
        ByVal activityType As String,
        ByVal eventName As String,
        ByVal eventNameEn As String,
        ByVal awardName As String,
        ByVal awardNameEn As String,
        ByVal organizerName As String,
        ByVal eventDate As Date,
        ByVal grantAmount As Nullable(Of Decimal),
        ByVal remark As String,
        ByVal isSelfConfirmed As Boolean,
        ByVal actor As String,
        Optional ByVal ownerStudentCode As String = "") As Integer

        EnsureSchema()

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()

            If achievementId <= 0 Then
                Const insertSql As String =
                    "INSERT INTO dbo.StudentAchievements (" &
                    "    StudentCode, AcademicYearBe, StudyYear, ActivityType, " &
                    "    EventName, EventNameEn, AwardName, AwardNameEn, OrganizerName, " &
                    "    EventDate, GrantAmount, Remark, IsSelfConfirmed, IsDeleted, " &
                    "    CreatedBy, UpdatedBy, CreatedAt, UpdatedAt" &
                    ") OUTPUT INSERTED.AchievementId VALUES (" &
                    "    @StudentCode, @AcademicYearBe, @StudyYear, @ActivityType, " &
                    "    @EventName, @EventNameEn, @AwardName, @AwardNameEn, @OrganizerName, " &
                    "    @EventDate, @GrantAmount, @Remark, @IsSelfConfirmed, 0, " &
                    "    @Actor, @Actor, SYSUTCDATETIME(), SYSUTCDATETIME()" &
                    ");"

                Using cmd As New SqlCommand(insertSql, conn)
                    BindCommonParameters(cmd, studentCode, academicYearBe, studyYear, activityType,
                                         eventName, eventNameEn, awardName, awardNameEn, organizerName,
                                         eventDate, grantAmount, remark, isSelfConfirmed, actor)
                    Dim newId As Object = cmd.ExecuteScalar()
                    Return Convert.ToInt32(newId)
                End Using
            Else
                Const updateSql As String =
                    "UPDATE dbo.StudentAchievements SET " &
                    "    AcademicYearBe = @AcademicYearBe, " &
                    "    StudyYear = @StudyYear, " &
                    "    ActivityType = @ActivityType, " &
                    "    EventName = @EventName, " &
                    "    EventNameEn = @EventNameEn, " &
                    "    AwardName = @AwardName, " &
                    "    AwardNameEn = @AwardNameEn, " &
                    "    OrganizerName = @OrganizerName, " &
                    "    EventDate = @EventDate, " &
                    "    GrantAmount = @GrantAmount, " &
                    "    Remark = @Remark, " &
                    "    IsSelfConfirmed = @IsSelfConfirmed, " &
                    "    UpdatedBy = @Actor, " &
                    "    UpdatedAt = SYSUTCDATETIME() " &
                    "WHERE AchievementId = @AchievementId " &
                    "  AND IsDeleted = 0 " &
                    "  AND (@OwnerStudentCode = N'' OR StudentCode = @OwnerStudentCode);"

                Using cmd As New SqlCommand(updateSql, conn)
                    cmd.Parameters.Add("@AchievementId", SqlDbType.Int).Value = achievementId
                    cmd.Parameters.Add("@OwnerStudentCode", SqlDbType.NVarChar, 30).Value = If(ownerStudentCode, "").Trim()
                    BindCommonParameters(cmd, studentCode, academicYearBe, studyYear, activityType,
                                         eventName, eventNameEn, awardName, awardNameEn, organizerName,
                                         eventDate, grantAmount, remark, isSelfConfirmed, actor)
                    Dim affected As Integer = cmd.ExecuteNonQuery()
                    If affected > 0 Then
                        Return achievementId
                    End If
                    Return 0
                End Using
            End If
        End Using
    End Function

    Public Function Achievement_Delete(
        ByVal achievementId As Integer,
        ByVal actor As String,
        Optional ByVal ownerStudentCode As String = "") As Boolean

        EnsureSchema()

        Const deleteSql As String =
            "UPDATE dbo.StudentAchievements " &
            "SET IsDeleted = 1, UpdatedBy = @Actor, UpdatedAt = SYSUTCDATETIME() " &
            "WHERE AchievementId = @AchievementId " &
            "  AND IsDeleted = 0 " &
            "  AND (@OwnerStudentCode = N'' OR StudentCode = @OwnerStudentCode);"

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New SqlCommand(deleteSql, conn)
                cmd.Parameters.Add("@AchievementId", SqlDbType.Int).Value = achievementId
                cmd.Parameters.Add("@Actor", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(actor), DBNull.Value, CObj(actor.Trim()))
                cmd.Parameters.Add("@OwnerStudentCode", SqlDbType.NVarChar, 30).Value = If(ownerStudentCode, "").Trim()
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Private Shared Sub BindCommonParameters(
        ByVal cmd As SqlCommand,
        ByVal studentCode As String,
        ByVal academicYearBe As Integer,
        ByVal studyYear As Integer,
        ByVal activityType As String,
        ByVal eventName As String,
        ByVal eventNameEn As String,
        ByVal awardName As String,
        ByVal awardNameEn As String,
        ByVal organizerName As String,
        ByVal eventDate As Date,
        ByVal grantAmount As Nullable(Of Decimal),
        ByVal remark As String,
        ByVal isSelfConfirmed As Boolean,
        ByVal actor As String)

        cmd.Parameters.Add("@StudentCode", SqlDbType.NVarChar, 30).Value = studentCode.Trim()
        cmd.Parameters.Add("@AcademicYearBe", SqlDbType.Int).Value = academicYearBe
        cmd.Parameters.Add("@StudyYear", SqlDbType.Int).Value = studyYear
        cmd.Parameters.Add("@ActivityType", SqlDbType.NVarChar, 100).Value = ToDbValue(activityType)
        cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 500).Value = eventName.Trim()
        cmd.Parameters.Add("@EventNameEn", SqlDbType.NVarChar, 500).Value = ToDbValue(eventNameEn)
        cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 300).Value = awardName.Trim()
        cmd.Parameters.Add("@AwardNameEn", SqlDbType.NVarChar, 300).Value = ToDbValue(awardNameEn)
        cmd.Parameters.Add("@OrganizerName", SqlDbType.NVarChar, 300).Value = ToDbValue(organizerName)
        cmd.Parameters.Add("@EventDate", SqlDbType.Date).Value = eventDate.Date

        Dim pGrant As SqlParameter = cmd.Parameters.Add("@GrantAmount", SqlDbType.Decimal)
        pGrant.Precision = 18
        pGrant.Scale = 2
        If grantAmount.HasValue Then
            pGrant.Value = grantAmount.Value
        Else
            pGrant.Value = DBNull.Value
        End If

        cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = ToDbValue(remark)
        cmd.Parameters.Add("@IsSelfConfirmed", SqlDbType.Bit).Value = isSelfConfirmed
        cmd.Parameters.Add("@Actor", SqlDbType.NVarChar, 50).Value = ToDbValue(actor)
    End Sub

    Private Shared Function ToDbValue(ByVal value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If
        Return value.Trim()
    End Function
End Class
