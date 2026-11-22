Imports Microsoft.ApplicationBlocks.Data

Public Class ReferenceValueController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

#Region "Referece Value"
    Public Function ReferenceValue_GetByDomainCode(ByVal DomainCode As String, Optional Blank As String = "") As DataTable
        If Blank = "Y" Then
            ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetByDomainCodeAndBlank", DomainCode)
        Else
            ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetByDomainCode", DomainCode)
        End If
        Return ds.Tables(0)
    End Function

    Public Function ReferenceDomain_GetByCode(ByVal Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceDomain_GetByCode", Code)
        Return ds.Tables(0)
    End Function
    Public Function ReferenceDomain_ChkDup(ByVal Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceDomain_GetByCode", Code)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function ReferenceDomain_GetSearch(ByVal Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceDomain_GetSearch", Search)
        Return ds.Tables(0)
    End Function
    Public Function ReferenceDomain_Add( Code As String, Desc As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceDomain_Add", Code, Desc)
    End Function
    Public Function ReferenceDomain_Update(UID As Integer, Code As String, Desc As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceDomain_Update", UID, Code, Desc)
    End Function

    Public Function ReferenceDomain_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceDomain_Delete", UID)
    End Function

    Public Function ReferenceValue_GetByCode(ByVal Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetByCode", Code)
        Return ds.Tables(0)
    End Function
    Public Function ReferenceValue_ChkDup(Domain As String, ByVal Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetByCode", Domain, Code)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function ReferenceValue_GetSearch(DomainCode As String, ByVal Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetSearch", DomainCode, Search)
        Return ds.Tables(0)
    End Function
    Public Function ReferenceValue_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ReferenceValue_GetByUID", UID)
        Return ds.Tables(0)
    End Function

    Public Function ReferenceValue_Add(Domain As String, Code As String, Desc As String, StatusFlag As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceValue_Add", Domain, Code, Desc, StatusFlag, CUser)
    End Function
    Public Function ReferenceValue_Update(UID As Integer, Domain As String, Code As String, Desc As String, StatusFlag As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceValue_Update", UID, Domain, Code, Desc, StatusFlag, CUser)
    End Function

    Public Function ReferenceValue_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ReferenceValue_Delete", UID)
    End Function


    Public Function WorkType_Add(UID As Integer, Name As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "WorkType_Save", UID, Name, CUser)
    End Function
    Public Function WorkType_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "WorkType_Delete", UID)
    End Function

#End Region




End Class
