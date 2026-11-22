Imports Microsoft.ApplicationBlocks.Data

Public Class CourseController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Course_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Course_GetAll")
        Return ds.Tables(0)
    End Function
    Public Function Course_GetByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Course_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function Course_GetViewByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Course_GetViewByID", id)
        Return ds.Tables(0)
    End Function

    Public Function Course_Save(ByVal CourseID As Integer, TypeID As Integer, ByVal Name_TH As String, Name_EN As String, ByVal ShortName_TH As String, ShortName_EN As String, ByVal CourseDetail_TH As String, CourseDetail_EN As String, MUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Course_Save"), CourseID, TypeID, Name_TH, Name_EN, ShortName_TH, ShortName_EN, CourseDetail_TH, CourseDetail_EN, MUser)
    End Function

    Public Function Course_Delete(ByVal pID As String) As Integer
        SQL = "delete from Course where Courseid ='" & pID & "'"
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
End Class
