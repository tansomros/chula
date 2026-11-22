Imports Microsoft.ApplicationBlocks.Data
Public Class TeacherController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function GetTeacher() As DataTable
        SQL = "select * from Teachers order by Firstname "
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function GetTeacher_ByID(id As Integer) As DataTable
        SQL = "select * from Teachers where Teacher_id=" & id
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function GetTeacher_BySearch(id As String) As DataTable
        SQL = "select * from Teachers where Teacher_ID like '%" & id & "%' OR  FirstName like '%" & id & "%'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function Teacher_Add(ByVal pPrefix As String, ByVal pFName As String, ByVal pLName As String, pPosition As String, ByVal pDeptName As String) As Integer
       
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Teacher_Add"), pPrefix, pFName, pLName, pPosition, pDeptName)
    End Function

    Public Function Teacher_Update(ByVal TeacherID As Integer, Prefix As String, FirstName As String, LastName As String, NickName As String, Gender As String, PositionName As String, DeptUID As Integer, DeptName As String, Email As String, Telephone As String, MobilePhone As String, Address As String, District As String, City As String, ProvinceID As Integer, ProvinceName As String, ZipCode As String, PicturePath As String, UpdateBy As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Teacher_Update", TeacherID, Prefix, FirstName, LastName, NickName, Gender, PositionName, DeptUID, DeptName, Email, Telephone, MobilePhone, Address, District, City, ProvinceID, ProvinceName, ZipCode, PicturePath, UpdateBy)

    End Function

    Public Function Teacher_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from Teachers where Teacher_id =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function


End Class
