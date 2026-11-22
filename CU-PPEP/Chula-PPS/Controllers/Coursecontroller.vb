Imports DevExpress.CodeParser
Imports Microsoft.ApplicationBlocks.Data
Public Class CourseController

    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Year_GetByStudent(StdCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Year_GetByStudent", StdCode)
        Return ds.Tables(0)
    End Function

    Public Function Year_GetForReportByStudent(StdCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Year_GetForReportByStudent", StdCode)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetYear")
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetYearByLang(lang As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetYearByLang", lang)
        Return ds.Tables(0)
    End Function

    Public Function Courses_GetByCoordinator(pYear As Integer, pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByCoordinator", pYear, pID)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetByLocation(pYear As Integer, pLocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByLocation", pYear, pLocationID)
        Return ds.Tables(0)
    End Function

    Public Function Courses_GetFromAssessment(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetFromAssessment", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetByYear(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByYear", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetByAdv(pYear As Integer, personId As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByAdv", pYear, personId)
        Return ds.Tables(0)
    End Function
    Public Function Courses_Get4Selection(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_Get4Selection", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetByAdv4Selection(pYear As Integer, personID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByAdv4Selection", pYear, personID)
        Return ds.Tables(0)
    End Function

    Public Function Student_GetStudentNoAssessment(pYear As Integer, SkillPhaseID As Integer, Optional pKey As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Student_GetStudentNotAssessment"), pYear, SkillPhaseID, pKey)
        Return ds.Tables(0)

    End Function

    'Public Function Courses_GetByAssessment(pYear As Integer) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByAssessment", pYear)
    '    Return ds.Tables(0)
    'End Function

    Public Function Courses_GetSubjectByID(PCourseID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetSubjectByID", PCourseID)
        Return ds.Tables(0)
    End Function

    Public Function Courses_GetSubjectBySubjectCode(CYear As Integer, Code As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetSubjectBySubjectCode", CYear, Code)
        Return ds.Tables(0)
    End Function
    Public Function Courses_GetCourseIDBySubjectCode(EduYear As Integer, Code As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetCourseIDBySubjectCode", EduYear, Code)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function

    Public Function Courses_GetByStudent(pYear As Integer, StudentCode As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByStudent", pYear, StudentCode)
        Return ds.Tables(0)
    End Function


    Public Function Courses_GetBySearch(pYear As Integer, pKey As String) As DataTable
        SQL = "select * from  View_Courses  Where CYEAR=" & pYear & " And (SubjectName like '%" & pKey & "%' OR SubjectCode like '%" & pKey & "%'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function
    Public Function Courses_Add(ByVal CYEAR As Integer _
           , ByVal SubjectCode As String _
           , ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Courses_Add"), CYEAR, SubjectCode, UpdBy)
    End Function

    Public Function Courses_Delete(pYear As Integer, pCode As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Courses_Delete", pYear, pCode)
    End Function

    Public Function CoursesCoordinator_Get(pYear As Integer, pCode As Integer) As DataTable
        SQL = "select * from  View_CourseCoordinator   Where CYEAR=" & pYear & " And CourseID=" & pCode
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function CoursesCoordinator_CheckStatus(PersonID As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "CoursesCoordinator_CheckStatus", PersonID)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function CoursesCoordinator_Add(ByVal CYEAR As Integer _
           , ByVal CourseID As Integer _
            , ByVal SubjectCode As String _
           , ByVal PersonID As Integer _
           , ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseCoordinator_Add"), CYEAR, CourseID, SubjectCode, PersonID, UpdBy)
    End Function

    Public Function CoursesCoordinator_Delete(pID As Integer) As Integer
        SQL = "delete from   CourseCoordinator  Where itemID=" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function Course4Student_GetByStdICode(pYear As Integer, pCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Course4Student_GetByStudentCode", pYear, pCode)
        Return ds.Tables(0)
    End Function

    Public Function CourseCoordinator_Get4Assessment(pYear As Integer, pUserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "CourseCoordinator_Get4Assessment", pYear, pUserID)
        Return ds.Tables(0)
    End Function

    Public Function CourseStudent_GetStudentInCourse(pYear As Integer, CID As Integer, Optional pKey As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "CourseStudent_GetStudentInCourse", pYear, CID, pKey)
        Return ds.Tables(0)
    End Function

    Public Function CourseStudent_GetStudentNoCourse(pYear As Integer, pCode As Integer, pMajor As Integer, pLevel As Integer, Optional pKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CourseStudent_GetStudentNoCourse"), pYear, pCode, pMajor, pLevel, pKey)

        Return ds.Tables(0)
    End Function

    Public Function CourseStudent_Add(ByVal CID As Integer _
           , ByVal StudentCode As String _
           , ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseStudent_Add"), CID, StudentCode, UpdBy)
    End Function

    Public Function CourseStudent_Delete(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "CourseStudent_DeleteByUID", pID)
    End Function

    Public Function CourseStudent_DeleteByMajorAndLevel(pMajorID As Integer, cID As Integer, cLevel As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseStudent_DeleteByMajor"), pMajorID, cID, cLevel)
    End Function

    Public Function CourseStudent_DeleteByLevel(pID As Integer, cid As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseStudent_DeleteByLevel"), pID, cid)
    End Function

    Public Function CourseLocation_GetByLocation(pYear As Integer, LocationID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "CourseLocation_GetByLocation", pYear, LocationID)
        Return ds.Tables(0)
    End Function

    Public Function CourseLocation_GetLocationInCourse(pYear As Integer, CID As Integer, Optional pKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "CourseLocation_GetLocationInCourse", pYear, CID, pKey)
        Return ds.Tables(0)
    End Function

    Public Function CourseLocation_GetLocationNoCourse(pYear As Integer, pCode As Integer, Optional pKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CourseLocation_GetLocationNoCourse"), pYear, pCode, pKey)

        Return ds.Tables(0)
    End Function

    Public Function CourseLocation_Add(ByVal CID As Integer _
           , ByVal LocationID As Integer _
           , ByVal isTrain As String _
           , ByVal isSPV As String _
           , ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseLocation_Add"), CID, LocationID, isTrain, isSPV, UpdBy)
    End Function

    Public Function CourseLocation_Delete(pID As Integer) As Integer
        SQL = "delete from   CourseLocation  Where itemID=" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function CourseLocation_UpdateSupervisionByGroup(pID As Integer, Cid As Integer, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseLocation_UpdateSupervisionByGroup"), pID, Cid, StatusFlag)
    End Function

    Public Function CourseLocation_UpdateSupervision(pID As Integer, StatusFlag As String) As Integer
        SQL = "Update CourseLocation SET isSupervision='N' Where itemID=" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
    Public Function CourseLocation_DeleteByGroup(pID As Integer, Cid As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseLocation_DeleteByGroup"), pID, Cid)
    End Function


End Class
