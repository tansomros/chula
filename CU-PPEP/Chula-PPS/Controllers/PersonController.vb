Imports Microsoft.ApplicationBlocks.Data
Public Class PersonController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Person_GetByID(id As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function Person_GetByType(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetByType", id)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetNoUserByType(ptype As String, pkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetNoUserByType", ptype, pkey)
        Return ds.Tables(0)
    End Function

    Public Function Person_GetSearch(ptype As String, pkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetSearch", ptype, pkey)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetPersonIDByUserID(uid As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetPersonIDByUserID", uid)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function

    Public Function Person_GetLocationByUserID(id As Integer) As Integer

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetLocationByUserID", id)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function Preceptor_GetBySearch(LocationId As String, SearchCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Preceptor_GetBySearch", LocationId, SearchCond)
        Return ds.Tables(0)
    End Function

    Public Function Preceptor_NoUserGetByLocation(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Preceptor_NoUserGetByLocation", id)
        Return ds.Tables(0)
    End Function

    Public Function Preceptor_GetByLocation(LocationId As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Preceptor_GetByLocation", LocationId)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetName(pid As Integer) As String

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetName", pid)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function

    Public Function Location_GetByPersonID(id As Integer) As Integer

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByPersonID", id)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If


    End Function

    Public Function Teacher_Add(ByVal pPrefix As String, ByVal pFName As String, ByVal pLName As String, pPosID As Integer, pPosition As String, pDeptID As Integer, ByVal pDeptName As String, status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Teacher_Add"), pPrefix, pFName, pLName, pPosID, pPosition, pDeptID, pDeptName, status)
    End Function

    Public Function Person_Add(ByVal pPrefix As Integer, ByVal pFName As String, ByVal pLName As String, pPosition As String, pMail As String, pTel As String, ByVal pType As String, pLocation As Integer, IsMain As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_Add"), pPrefix, pFName, pLName, pPosition, pMail, pTel, pType, pLocation, IsMain)
    End Function

    Public Function Person_AddUser(ByVal pUserName As String, ByVal pFName As String, ByVal pLName As String, pMail As String, ByVal pType As String, pLocation As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_AddUser"), pUserName, pFName, pLName, pMail, pType, pLocation)
    End Function

    Public Function Person_UpdateUserID(ByVal sProfileID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_UpdateUserID"), sProfileID)
    End Function

    Public Function Person_Update(ByVal PersonID As Integer, Prefix As Integer, FirstName As String, LastName As String, NickName As String, Gender As String, PositionID As Integer, PositionName As String, DeptID As Integer, DeptName As String, Email As String, Telephone As String, MobilePhone As String, Address As String, District As String, City As String, ProvinceID As Integer, ProvinceName As String, ZipCode As String, PicturePath As String, UpdateBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_Update", PersonID, Prefix, FirstName, LastName, NickName, Gender, PositionID, PositionName, DeptID, DeptName, Email, Telephone, MobilePhone, Address, District, City, ProvinceID, ProvinceName, ZipCode, PicturePath, UpdateBy)

    End Function
    Public Function Person_UpdateByUser(ByVal PersonID As Integer, Prefix As Integer, FirstName As String, LastName As String, NickName As String, PositionID As Integer, PositionName As String, DeptID As Integer, DeptName As String, Email As String, Telephone As String, MobilePhone As String, Address As String, District As String, City As String, ProvinceID As Integer, ProvinceName As String, ZipCode As String, PicturePath As String, UpdateBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_UpdateByUser", PersonID, Prefix, FirstName, LastName, NickName, PositionID, PositionName, DeptID, DeptName, Email, Telephone, MobilePhone, Address, District, City, ProvinceID, ProvinceName, ZipCode, PicturePath, UpdateBy)

    End Function

    Public Function Person_Save(ByVal PersonID As Integer, ByVal pType As String, LocationId As Integer, ByVal WorkId As Integer, ByVal pPrefix As Integer, ByVal pFName As String, ByVal pLName As String, PositionName As String, pMail As String, Tel As String, LicenceNo As String, Credits As Double, IsTraining As String, IsApprove As String, StatusFlag As String, PhotoPath As String, CVPath As String, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_Save"), PersonID, pType, LocationId, WorkId, pPrefix, pFName, pLName, PositionName, pMail, Tel, LicenceNo, Credits, IsTraining, IsApprove, StatusFlag, PhotoPath, CVPath, MUser)
    End Function

    Public Function Teacher_UpdateSmall(ByVal PersonID As Integer, Prefix As String, FirstName As String, LastName As String, pPosID As Integer, PositionName As String, pDeptID As Integer, DeptName As String, UpdateBy As String, status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Teacher_UpdateSmall", PersonID, Prefix, FirstName, LastName, pPosID, PositionName, pDeptID, DeptName, UpdateBy, status)

    End Function

    Public Function Person_UpdateUserIDandMail(ByVal PersonID As Integer, userid As Integer, mail As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_UpdateUserIDandMail", PersonID, userid, mail)

    End Function

    Public Function Person_Delete(ByVal pID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_Delete", pID)

        'SQL = "delete from Persons where Personid =" & pID
        'Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function Preceptor_GetNoUser() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Preceptor_GetNoUser")
        Return ds.Tables(0)
    End Function
    Public Function Preceptor_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Preceptor_GetAll")
        Return ds.Tables(0)
    End Function

    Public Function Preceptor_Add(ByVal pPrefix As Integer, ByVal pFName As String, ByVal pLName As String, pPosition As String, pWork As Integer, pMail As String, pTel As String, ByVal pType As String, pLocation As Integer, CUser As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Preceptor_Add"), pPrefix, pFName, pLName, pPosition, pWork, pMail, pTel, pType, pLocation, CUser)
    End Function

End Class
