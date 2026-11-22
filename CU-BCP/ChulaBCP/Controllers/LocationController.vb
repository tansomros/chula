Imports Microsoft.ApplicationBlocks.Data

Public Class LocationController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Location_Get() As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_Get")
        Return ds.Tables(0)
    End Function
    Public Function Location_GetMaps() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetMaps")
        Return ds.Tables(0)
    End Function
    Public Function Location_GetMapsByLocation(LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetMapsByLocation", LocationID)
        Return ds.Tables(0)
    End Function
    Public Function LocationAssessment_Get(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationAssessment_GetByYear", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Location_CheckDuplicate(Name As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_CheckDuplicate", Name)
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull2Zero(ds.Tables(0).Rows(0)(0)) > 0 Then
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If
    End Function
    Public Function Location_GetIDByName(Name As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetIDByName", Name)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function

    Public Function Location_GetByID(id As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function Location_GetBySearch(keySearch As String, Optional pType As Integer = 0) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetBySearch", keySearch, pType)
        Return ds.Tables(0)
    End Function

    Public Function Location_GetBySearch(Year As Integer, Optional keySearch As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByYear", Year, keySearch)
        Return ds.Tables(0)
    End Function

    Public Function Location_GetByGroupID(gid As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByGroupID", gid)
        Return ds.Tables(0)
    End Function
    Public Function Location_GetByStudent(StudentCode As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByStudent", StudentCode)
        Return ds.Tables(0)
    End Function

    Public Function Location_Add(ByVal LocationName As String, ByVal LocationGroupID As Integer, ByVal LocationTypeID As Integer, DepartmentID As Integer, ByVal DepartmentName As String, LevelID As Integer, Bed As Integer, Branch As Integer, ByVal Address As String, ByVal ProvinceID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal Office_Tel As String, ByVal Office_Fax As String, ByVal Co_Name As String, ByVal Co_Position As String, ByVal Co_Mail As String, ByVal Co_Tel As String, ByVal Office_hours As String, ByVal Officer_Count As Integer, ByVal UpdBy As String, ByVal StatusFlag As Integer, ByVal BillName As String _
                                 , ByVal WorkDayID As Integer _
           , ByVal WorkDayDesc As String _
           , ByVal WorkTimeID As Integer _
           , ByVal WorkTimeDesc As String _
           , ByVal HasResidence As Integer _
           , ByVal HasCost As Integer _
           , ByVal PayAmount As Double _
           , ByVal isForce As Integer _
           , ByVal ConfirmHold As String _
           , ByVal ApartmentName As String _
           , ByVal ApartmentTel As String _
           , ByVal ApartmentAddr As String _
           , ByVal ApartmentTravel As String _
           , ByVal ApartmentDistance As String _
           , ByVal isBranch As Integer _
           , ByVal BranchRemark As String _
           , ByVal WorkList As String _
           , ByVal WorkOther As String _
           , ByVal WorkTop As String _
           , ByVal Remark As String _
           , ByVal Informant As String _
           , ByVal InfoPosition As String _
           , ByVal InfoDate As String _
           , ByVal ApartmentName2 As String _
           , ByVal ApartmentTel2 As String _
           , ByVal ApartmentAddr2 As String _
           , ByVal ApartmentTravel2 As String _
           , ByVal ApartmentDistance2 As String _
           , ByVal ApartmentName3 As String _
           , ByVal ApartmentTel3 As String _
           , ByVal ApartmentAddr3 As String _
           , ByVal ApartmentTravel3 As String _
           , ByVal ApartmentDistance3 As String, ByVal LetterName As String, ByVal Country As String _
           , ByVal WebSite As String _
           , ByVal Facebook As String _
           , ByVal ZoneID As String _
           , ByVal OfficeID As String _
           , ByVal isReceive As String _
           , ByVal isPay As String, Lat As String, Lng As String
        ) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Location_Add"), LocationName, LocationGroupID, LocationTypeID, DepartmentID, DepartmentName, LevelID, Bed, Branch, Address, ProvinceID, ProvinceName, ZipCode, Office_Tel, Office_Fax, Co_Name, Co_Position, Co_Mail, Co_Tel, Office_hours, Officer_Count, UpdBy, StatusFlag, BillName, WorkDayID _
           , WorkDayDesc _
           , WorkTimeID _
           , WorkTimeDesc _
           , HasResidence _
           , HasCost _
           , PayAmount _
           , isForce _
           , ConfirmHold _
           , ApartmentName _
           , ApartmentTel _
           , ApartmentAddr _
           , ApartmentTravel _
           , ApartmentDistance _
           , isBranch _
           , BranchRemark _
           , WorkList _
           , WorkOther _
           , WorkTop _
           , Remark _
           , Informant _
           , InfoPosition _
           , InfoDate _
           , ApartmentName2 _
           , ApartmentTel2 _
           , ApartmentAddr2 _
           , ApartmentTravel2 _
           , ApartmentDistance2 _
            , ApartmentName3 _
           , ApartmentTel3 _
           , ApartmentAddr3 _
           , ApartmentTravel3 _
           , ApartmentDistance3, LetterName, Country _
           , WebSite _
           , Facebook _
           , ZoneID _
           , OfficeID _
           , isReceive _
           , isPay, Lat, Lng)
    End Function

    Public Function Location_Update(ByVal LocationID As Integer, ByVal LocationName As String, ByVal LocationGroupID As Integer, ByVal LocationTypeID As Integer, DepartmentID As Integer, ByVal DepartmentName As String, LevelID As Integer, Bed As Integer, Branch As Integer, ByVal Address As String, ByVal ProvinceID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal Office_Tel As String, ByVal Office_Fax As String, ByVal Co_Name As String, ByVal Co_Position As String, ByVal Co_Mail As String, ByVal Co_Tel As String, ByVal Office_hours As String, ByVal Officer_Count As Integer, ByVal UpdBy As String, ByVal StatusFlag As String, ByVal BillName As String, ByVal WorkDayID As Integer _
           , ByVal WorkDayDesc As String _
           , ByVal WorkTimeID As Integer _
           , ByVal WorkTimeDesc As String _
           , ByVal HasResidence As Integer _
           , ByVal HasCost As Integer _
           , ByVal PayAmount As Double _
           , ByVal isForce As Integer _
           , ByVal ConfirmHold As String _
           , ByVal ApartmentName As String _
           , ByVal ApartmentTel As String _
           , ByVal ApartmentAddr As String _
           , ByVal ApartmentTravel As String _
           , ByVal ApartmentDistance As String _
           , ByVal isBranch As Integer _
           , ByVal BranchRemark As String _
           , ByVal WorkList As String _
           , ByVal WorkOther As String _
           , ByVal WorkTop As String _
           , ByVal Remark As String _
           , ByVal Informant As String _
           , ByVal InfoPosition As String _
           , ByVal InfoDate As String _
           , ByVal ApartmentName2 As String _
           , ByVal ApartmentTel2 As String _
           , ByVal ApartmentAddr2 As String _
           , ByVal ApartmentTravel2 As String _
           , ByVal ApartmentDistance2 As String _
           , ByVal ApartmentName3 As String _
           , ByVal ApartmentTel3 As String _
           , ByVal ApartmentAddr3 As String _
           , ByVal ApartmentTravel3 As String _
           , ByVal ApartmentDistance3 As String, ByVal LetterName As String, ByVal Country As String _
           , ByVal WebSite As String _
           , ByVal Facebook As String _
           , ByVal ZoneID As String _
           , ByVal OfficeID As String _
           , ByVal isReceive As String _
           , ByVal isPay As String, Lat As String, Lng As String
           ) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Location_Update", LocationID, LocationName, LocationGroupID, LocationTypeID, DepartmentID, DepartmentName, LevelID, Bed, Branch, Address, ProvinceID, ProvinceName, ZipCode, Office_Tel, Office_Fax, Co_Name, Co_Position, Co_Mail, Co_Tel, Office_hours, Officer_Count, UpdBy, StatusFlag, BillName, WorkDayID _
           , WorkDayDesc _
           , WorkTimeID _
           , WorkTimeDesc _
           , HasResidence _
           , HasCost _
           , PayAmount _
           , isForce _
           , ConfirmHold _
           , ApartmentName _
           , ApartmentTel _
           , ApartmentAddr _
           , ApartmentTravel _
           , ApartmentDistance _
           , isBranch _
           , BranchRemark _
           , WorkList _
           , WorkOther _
           , WorkTop _
           , Remark _
           , Informant _
           , InfoPosition _
           , InfoDate _
           , ApartmentName2 _
           , ApartmentTel2 _
           , ApartmentAddr2 _
           , ApartmentTravel2 _
           , ApartmentDistance2 _
            , ApartmentName3 _
           , ApartmentTel3 _
           , ApartmentAddr3 _
           , ApartmentTravel3 _
           , ApartmentDistance3, LetterName, Country _
           , WebSite _
           , Facebook _
           , ZoneID _
           , OfficeID _
           , isReceive _
           , isPay, Lat, Lng)

    End Function

    Public Function Location_UpdateisSameGender(ByVal LocationID As Integer, ByVal isSame As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Location_UpdateisSameGender", LocationID, isSame)
    End Function
    Public Function Location_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from Locations where Locationid =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function LocationAssessment_Get(pYear As Integer, pCourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationAssessment_Get", pYear, pCourseID)
        Return ds.Tables(0)
    End Function

    Public Function Location_GetIsSameGender(LocationID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetIsSameGender", LocationID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return String.Concat(ds.Tables(0).Rows(0)(0))
        Else
            Return "N"
        End If
    End Function


#Region "Coordinator"

    Public Function Coordinator_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Coordinator_Get")
        Return ds.Tables(0)
    End Function
    Public Function Coordinator_GetSearch(LocationID As Integer, LocationName As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Coordinator_GetSearch", LocationID, LocationName)
        Return ds.Tables(0)
    End Function

    Public Function Coordinator_Add(ByVal LocationID As Integer, ByVal Co_Name As String, ByVal Co_Position As String, ByVal Co_Mail As String, ByVal Co_Tel As String, SubjectID As Integer, ByVal isDefault As Integer, StatusFlag As String, Remark As String, ByVal WorkID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Coordinator_Add"), LocationID, Co_Name, Co_Position, Co_Mail, Co_Tel, SubjectID, isDefault, StatusFlag, Remark, WorkID)
    End Function

    Public Function Coordinator_UpdateStatus(ByVal LocationID As Integer, COID As Integer, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Coordinator_UpdateStatus"), LocationID, COID, Remark)
    End Function


    Public Function Coordinator_Delete(ByVal COID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Coordinator_Delete"), COID)
    End Function
    Public Function Coordinator_DeleteQ(ByVal LID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Coordinator_DeleteQ"), LID)
    End Function
#End Region


End Class

Public Class LocationGroupController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function LocationGroup_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationGroup_Get")
        Return ds.Tables(0)
    End Function

    Public Function LocationGroup_Add(ByVal pCode As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationGroup_Add"), pCode, pName, desc, pStatus)
    End Function

    Public Function LocationGroup_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationGroup_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

End Class

Public Class LocationTypeController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function LocationType_ByGroupID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationType_GetByGroupID", id)
        Return ds.Tables(0)
    End Function
End Class
Public Class LocationZoneController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function LocationZone_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationZone_Get")
        Return ds.Tables(0)
    End Function
    Public Function LocationZone_GetWithBlank() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationZone_GetWithBlank")
        Return ds.Tables(0)
    End Function

    Public Function LocationZone_GetByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationZone_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function LocationZone_Save(id As String, name As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationZone_Save", id, name)
    End Function
    Public Function LocationZone_Delete(id As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationZone_Delete", id)
    End Function
End Class
Public Class LocationOfficeController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function LocationOffice_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationOffice_Get")
        Return ds.Tables(0)
    End Function
    Public Function LocationOffice_GetWithBlank() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationOffice_GetWithBlank")
        Return ds.Tables(0)
    End Function
    Public Function LocationOffice_GetByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationOffice_GetByID", id)
        Return ds.Tables(0)
    End Function
    Public Function LocationOffice_Save(id As String, name As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationOffice_Save", id, name)
    End Function
    Public Function LocationOffice_Delete(id As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationOffice_Delete", id)
    End Function

End Class

Public Class LocationWorkController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function LocationWork_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationWork_Get")
        Return ds.Tables(0)
    End Function
    Public Function LocationWork_GetWithBlank() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationWork_GetWithBlank")
        Return ds.Tables(0)
    End Function

    Public Function LocationWork_GetByID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationWork_GetByID", id)
        Return ds.Tables(0)
    End Function
    Public Function LocationWork_GetByGroup(group As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationWork_GetByGroup", group)
        Return ds.Tables(0)
    End Function

    Public Function LocationWork_Save(id As Integer, group As String, name As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationWork_Save", id, group, name)
    End Function
    Public Function LocationWork_Delete(id As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationWork_Delete", id)
    End Function
    Public Function LocationWorkDetail_DeleteByLocation(LocationID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationWorkDetail_DeleteByLocation", LocationID)
    End Function

    Public Function LocationWorkDetail_Get(LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationWorkDetail_Get", LocationID)
        Return ds.Tables(0)
    End Function
    Public Function LocationWorkDetail_Save(LocationID As Integer, WorkID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationWorkDetail_Save", LocationID, WorkID)
    End Function

End Class

Public Class LocationDepartmentController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function LocationDepartment_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationDepartment_Get")
        Return ds.Tables(0)
    End Function

    Public Function LocationDepartment_Add(ByVal pCode As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationDepartment_Add"), pCode, pName, desc, pStatus)
    End Function

    Public Function LocationDepartment_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationDepartment_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

End Class
Public Class LocationCreditController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Location_GetCreditBalance(LocationID As Integer) As Double
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetCreditBalance", LocationID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Dbl(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function LocationDepartment_Add(ByVal pCode As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationDepartment_Add"), pCode, pName, desc, pStatus)
    End Function

    Public Function LocationDepartment_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "LocationDepartment_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

End Class

