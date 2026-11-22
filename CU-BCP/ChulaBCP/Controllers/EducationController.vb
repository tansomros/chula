Imports Microsoft.ApplicationBlocks.Data
Public Class EducationController

    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Education_Get(StdID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Education_Get", StdID)
        Return ds.Tables(0)
    End Function
    Public Function Education_Save(ByVal EUID As Integer, StudentID As Integer _
           , ByVal CourseName As String _
            , ByVal Institute As String _
           , ByVal ProvinceName As String _
           , ByVal EduYear As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Education_Save"), EUID, StudentID, CourseName, Institute, ProvinceName, EduYear)
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


    Public Function Courses_GetSubjectByID(PCourseID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetSubjectByID", PCourseID)
        Return ds.Tables(0)
    End Function

    Public Function Courses_GetSubjectBySubjectCode(Code As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetSubjectBySubjectCode", Code)
        Return ds.Tables(0)
    End Function


    Public Function Courses_GetByStudent(pYear As Integer, StudentCode As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Courses_GetByStudent", pYear, StudentCode)
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



    Public Function CoursesCoordinator_Delete(pID As Integer) As Integer
        SQL = "delete from   CourseCoordinator  Where itemID=" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function Course4Student_GetByStdICode(pYear As Integer, pCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Course4Student_GetByStudentCode", pYear, pCode)
        Return ds.Tables(0)
    End Function

End Class
