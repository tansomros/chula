Imports Microsoft.ApplicationBlocks.Data
Public Class SeminarController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Seminar_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Seminar_GetYear")
        Return ds.Tables(0)
    End Function

    Public Function Seminar_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Seminar_GetByUID", UID)
        Return ds.Tables(0)
    End Function

    Public Function Seminar_GetBySearch(pYear As Integer, pWorkType As String, pGroup As String, isPresented As String, Country As String, Budget As String, pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Seminar_GetBySearch", pYear, pWorkType, pGroup, isPresented, Country, Budget, pKey)
        Return ds.Tables(0)
    End Function

    Public Function Seminar_Save(ByVal UID As Integer _
           , ByVal PersonID As Integer _
           , ByVal WKTYPE As String _
           , ByVal SEMGRP As String _
           , ByVal StartDate As String _
           , ByVal EndDate As String _
           , ByVal isPresented As String _
           , ByVal PresentedType As String _
           , ByVal PortfolioName As String _
           , ByVal SeminarName As String _
           , ByVal COUNTRY As String _
           , ByVal BUDGET As String _
           , ByVal MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Seminar_Save"), UID, PersonID, WKTYPE, SEMGRP, StartDate, EndDate, isPresented, PresentedType, PortfolioName, SeminarName, COUNTRY, BUDGET, MUser)
    End Function

    Public Function Seminar_Delete(itemID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Seminar_Delete"), itemID)
    End Function



#Region "WorkType"
    Public Function WorkType_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "WorkType_Get")
        Return ds.Tables(0)
    End Function

    Public Function WorkType_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "WorkType_Save", UID, Name, CUser)
    End Function
    Public Function WorkType_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "WorkType_Delete", UID )
    End Function

#End Region


#Region "Group"
    Public Function SeminarGroup_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SeminarGroup_Get")
        Return ds.Tables(0)
    End Function

    Public Function SeminarGroup_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SeminarGroup_Save", UID, Name, CUser)
    End Function
    Public Function SeminarGroup_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SeminarGroup_Delete", UID)
    End Function
#End Region


#Region "Presented Type"
    Public Function Presented_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Presented_Get")
        Return ds.Tables(0)
    End Function

    Public Function Presented_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Presented_Save", UID, Name, CUser)
    End Function
    Public Function Presented_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Presented_Delete", UID)
    End Function
#End Region


#Region "Country"
    Public Function Country_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Country_Get")
        Return ds.Tables(0)
    End Function

    Public Function Country_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Country_Save", UID, Name, CUser)
    End Function
    Public Function Country_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Country_Delete", UID)
    End Function
#End Region

#Region "Budget"
    Public Function Budget_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Budget_Get")
        Return ds.Tables(0)
    End Function

    Public Function Budget_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Budget_Save", UID, Name, CUser)
    End Function
    Public Function Budget_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Budget_Delete", UID)
    End Function
#End Region

End Class
