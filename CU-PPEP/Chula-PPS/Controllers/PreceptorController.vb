Imports Microsoft.ApplicationBlocks.Data
Public Class PreceptorController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function GetPreceptor() As DataSet
        SQL = "select * from Preceptors "
        Return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function GetPreceptor_ByID(id As Integer) As DataTable
        SQL = "select * from Preceptors where Preceptorid=" & id
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function
    Public Function GetPreceptor_ByLocation(id As Integer) As DataTable
        SQL = "select * from Preceptors where Locationid=" & id
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function PreceptorLocation_Get(Year As Integer, LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PreceptorLocation_Get", Year, LocationID)
        Return ds.Tables(0)
    End Function

    Public Function PreceptorLocation_GetByUser(Year As Integer, UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PreceptorLocation_GetByUser", Year, UserID)
        Return ds.Tables(0)
    End Function
    Public Function Preceptor_Add(ByVal pCode As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        'SQL = "insert into Preceptor (roleid,rolename,ispublic) values(" & pCode & ",'" & pName & "'," & pStatus & ")"
        'Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Preceptor_Add"), pCode, pName, desc, pStatus)
    End Function

    Public Function Preceptor_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Preceptor_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function Preceptor_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from Preceptors where Preceptorid =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function PreceptorLocation_Add(Year As Integer, PersonID As Integer, ByVal LocationID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PreceptorLocation_Add", Year, PersonID, LocationID)
    End Function
    Public Function PreceptorLocation_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PreceptorLocation_Delete", UID)
    End Function

    Public Function PreceptorLocation_Get4Assessment(pYear As Integer, pUserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PreceptorLocation_Get4Assessment", pYear, pUserID)
        Return ds.Tables(0)
    End Function

End Class
