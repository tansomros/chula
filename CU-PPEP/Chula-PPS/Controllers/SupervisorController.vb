Imports Microsoft.ApplicationBlocks.Data
Public Class SupervisorController

    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Supervisor_GetByPerson(pYear As Integer, PersonID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetByPerson", pYear, PersonID)
        Return ds.Tables(0)
    End Function
    Public Function Supervisor_GetByUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetByUID", pUID)
        Return ds.Tables(0)
    End Function
    Public Function Supervisor_GetByYear(pYear As Integer) As DataTable
        SQL = "select * from  View_Supervisor  Where PYEAR=" & pYear
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function Supervisor_GetBySearch(pYear As Integer, pCourseID As Integer, pKey As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetBySearch", pYear, pCourseID, pKey)
        Return ds.Tables(0)
    End Function

    Public Function Supervisor_GetSendJob(pYear As Integer, pCourseID As Integer, pKey As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetSendJob", pYear, pCourseID, pKey)
        Return ds.Tables(0)
    End Function
    Public Function Supervisor_GetNotSendJob(pYear As Integer, pCourseID As Integer, pKey As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetNotSendJob", pYear, pCourseID, pKey)
        Return ds.Tables(0)
    End Function

    Public Function Supervisor_UpdateSendJob(pUID As Integer, pCode As String, pUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Supervisor_UpdateSendJob", pUID, pCode, pUser)
    End Function

    Public Function Supervisor_Delete(pYear As Integer, pCode As Integer) As Integer
        SQL = "delete from  Supervisor  Where PYEAR=" & pYear & " And itemID=" & pCode
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function Supervisor_GetByCourse(pYear As Integer, pCourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetByCourse", pYear, pCourseID)
        Return ds.Tables(0)
    End Function
    Public Function Supervisor_GetTeacher(pYear As Integer, pCourseID As Integer, LocationID As Integer, TimePhaseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Supervisor_GetTeacher", pYear, pCourseID, LocationID, TimePhaseID)
        Return ds.Tables(0)
    End Function

    Public Function Supervisor_GetLocationInSupervisor(pYear As Integer, subjCode As String, personID As Integer, Optional pKey As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function Supervisor_GetLocation(pYear As Integer, subjCode As String, provID As Integer, Optional pKey As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Assessment_GetForSupervisor", pYear, subjCode, pKey)
        Return ds.Tables(0)

    End Function

    Public Function Supervisor_Add(ByVal PYear As Integer, ByVal CourseID As Integer _
           , ByVal PersonID As Integer _
           , ByVal LocationID As Integer _
           , ByVal TimePhaseID As Integer _
           , ByVal WorkDate As String, ByVal WorkTime As String, ByVal TravelBy As String, ByVal UpdBy As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Supervisor_Add"), PYear, CourseID, PersonID, LocationID, TimePhaseID, WorkDate, WorkTime, TravelBy, UpdBy)
    End Function

    Public Function Supervisor_Update(ByVal PYear As Integer, ByVal CourseID As Integer _
           , ByVal PersonID As Integer _
           , ByVal LocationID As Integer _
           , ByVal TimePhaseID As Integer _
           , ByVal WorkDate As String, ByVal WorkTime As String, ByVal TravelBy As String, ByVal UpdBy As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Supervisor_Add"), PYear, CourseID, PersonID, LocationID, TimePhaseID, WorkDate, WorkTime, TravelBy, UpdBy)
    End Function



    Public Function Supervisor_CheckDup(ByVal PYear As Integer, CourseID As Integer, ByVal PersonID As Integer _
         , ByVal WorkDate As String _
         , ByVal WorkTime As Integer) As Boolean

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Supervisor_CheckDup"), PYear, CourseID, PersonID, WorkDate, WorkTime)
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


    Public Function Supervisor_Delete(pID As Integer) As Integer
        SQL = "delete from   Supervisor  Where UID=" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function


End Class
