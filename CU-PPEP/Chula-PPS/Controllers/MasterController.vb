Imports Microsoft.ApplicationBlocks.Data
Public Class MasterController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

#Region "Prominent"
    Public Function Prominent_GetByType(pType As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prominent_GetByType", pType)
        Return ds.Tables(0)
    End Function
    Public Function Prominent_GetAssessment(EduYear As Integer, StudentCode As String, LocationID As Integer, UserID As Integer, PType As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prominent_GetAssessment", EduYear, StudentCode, LocationID, UserID, PType)
        Return ds.Tables(0)
    End Function
#End Region

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

    Public Function CoursesCoordinator_Add(ByVal CYEAR As Integer _
           , ByVal CourseID As Integer _
            , ByVal SubjectCode As String _
           , ByVal PersonID As Integer _
           , ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CourseCoordinator_Add"), CYEAR, CourseID, SubjectCode, PersonID, UpdBy)
    End Function


End Class
