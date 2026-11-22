Imports Microsoft.ApplicationBlocks.Data

Public Class SupervisionController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function SupervisionLocation_Get(iYear As Integer, Optional KeySearch As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "SupervisionLocation_Get", iYear, KeySearch)
        Return ds.Tables(0)
    End Function

    Public Function SupervisionLocation_GetSearch(iYear As Integer, ProvID As String, PhaseID As Integer, Optional KeySearch As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SupervisionLocation_GetSearch", iYear, ProvID, PhaseID, KeySearch)
        Return ds.Tables(0)
    End Function

    Public Function SupervisionPlan_GetByPerson(iYear As Integer, PersonID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SupervisionPlan_GetByPerson", iYear, PersonID)
        Return ds.Tables(0)
    End Function

    Public Function SupervisionPlan_Save(Code As String, EduYear As Integer, PersonID As String, WorkDate As String, WorkTime As String, TravelBy As String, Remark As String, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SupervisionPlan_Save", Code, EduYear, PersonID, WorkDate, WorkTime, TravelBy, Remark, UpdBy)
    End Function
    Public Function SupervisionPlanDetail_Save(PlanCode As String, LocationID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SupervisionPlanDetail_Save", PlanCode, LocationID, UpdBy)
    End Function

    Public Function SupervisionPlan_Delete(Code As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SupervisionPlan_Delete", Code)
    End Function


    Public Function Location_Get4Supervisor(iYear As Integer, CourseID As Integer, ProvID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_Get4Supervisor", iYear, CourseID, ProvID)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_Get4Supervisor(iYear As Integer, CourseID As Integer, LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_Get4Supervisor", iYear, CourseID, LocationID)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_GetALL4Supervisor(iYear As Integer, CourseID As Integer, LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetAll4Supervisor", iYear, CourseID, LocationID)
        Return ds.Tables(0)
    End Function


    Public Function Location_GetNotSupervision(iYear As Integer,LocationGroupID As Integer, Optional KeySearch As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetNotSupervision", iYear, LocationGroupID, KeySearch)
        Return ds.Tables(0)
    End Function

    Public Function VisitLocation_DeleteByGroup(EduYear As Integer, LocationGroupID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("VisitLocation_DeleteByGroup"), EduYear, LocationGroupID)
    End Function
    Public Function VisitLocation_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("VisitLocation_Delete"), UID)
    End Function

    Public Function VisitLocation_Add(ByVal EduYear As Integer, ByVal LocationID As Integer, ByVal isForce As String, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("VisitLocation_Add"), EduYear, LocationID, isForce, UpdBy)
    End Function


End Class
