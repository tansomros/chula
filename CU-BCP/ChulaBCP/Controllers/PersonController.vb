Imports Microsoft.ApplicationBlocks.Data
Public Class PersonController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Person_GetByType(ByVal PersonType As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetByType", PersonType)
        Return ds.Tables(0)
    End Function

    Public Function Person_GetMajorByType(ByVal PersonType As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetMajorByType", PersonType)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetMajorEnglishByType(ByVal PersonType As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetMajorEnglishByType", PersonType)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetStaff(PersonType As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetStaff", PersonType)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetBoard(ByVal Level As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetBoard", Level)
        Return ds.Tables(0)
    End Function

    Public Function Person_GetLeader(PersonType As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetLeader", PersonType)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetStaff() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetOther")
        Return ds.Tables(0)
    End Function
    Public Function Person_GetTeacher() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetTeacher")
        Return ds.Tables(0)
    End Function


    Public Function Person_Get4User(ByVal PersonType As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_Get4User", PersonType)
        Return ds.Tables(0)
    End Function
    Public Function Location_GetByPersonID(id As Integer) As Integer

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetByPersonID", id)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If


    End Function
    Public Function Person_GetByID(ByVal PersonID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetByID", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetBySearch(ByVal PersonType As Integer, Search As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetBySearch", PersonType, Search)
        Return ds.Tables(0)
    End Function
    Public Function GetPerson_ByType(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Persons_GetByType", id)
        Return ds.Tables(0)
    End Function
    Public Function GetPerson_SearchByType(ptype As String, pkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Persons_GetSearch", ptype, pkey)
        Return ds.Tables(0)
    End Function
    Public Function Person_GetPersonID(ByVal PersonType As Integer, EmpID As String, NameTH As String, NameEN As String) As Integer

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetPersonID", PersonType, EmpID, NameTH, NameEN)
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull2Zero(ds.Tables(0).Rows(0)(0)) > 0 Then
                Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
            Else
                Return 0
            End If
        Else
            Return 0
        End If

    End Function
    Public Function Person_GetCode(PersonID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Person_GetCode", PersonID)
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull2Zero(ds.Tables(0).Rows(0)(0)) > 0 Then
                Return DBNull2Str(ds.Tables(0).Rows(0)(0))
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Function Teacher_Add(ByVal pPrefix As String, ByVal pFName As String, ByVal pLName As String, pPosID As Integer, pPosition As String, pDeptID As Integer, ByVal pDeptName As String, status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Teacher_Add"), pPrefix, pFName, pLName, pPosID, pPosition, pDeptID, pDeptName, status)
    End Function
    Public Function Person_Add(ByVal pPrefix As Integer, ByVal pFName As String, ByVal pLName As String, pPosition As String, pMail As String, ByVal pType As String, pLocation As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_Add"), pPrefix, pFName, pLName, pPosition, pMail, pType, pLocation)
    End Function

    Public Function Person_Update(ByVal PersonID As Integer, Prefix As Integer, FirstName As String, LastName As String, NickName As String, Gender As String, PositionID As Integer, PositionName As String, DeptID As Integer, DeptName As String, Email As String, Telephone As String, MobilePhone As String, Address As String, District As String, City As String, ProvinceID As Integer, ProvinceName As String, ZipCode As String, PicturePath As String, UpdateBy As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_Update", PersonID, Prefix, FirstName, LastName, NickName, Gender, PositionID, PositionName, DeptID, DeptName, Email, Telephone, MobilePhone, Address, District, City, ProvinceID, ProvinceName, ZipCode, PicturePath, UpdateBy)

    End Function
    Public Function Person_Save(ByVal PersonID As Integer, ByVal EmployeeCode As String, ByVal PrefixID As Integer, ByVal FNameTH As String, ByVal LNameTH As String, ByVal FNameEN As String, ByVal LNameEN As String, Contact As String, Tel As String, Email As String, PositionID As String, DepartmentUID As Integer, MajorID As Integer, PositionPrefixID As String, Educate As String, WorkStatus As String, isExclusive As String, isFaculty As String, isTeacher As String, isStaff As String, isAlumni As String, StatusFlag As String, CUser As Integer, Workplace As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Person_Save"), PersonID _
      , EmployeeCode _
      , PrefixID _
      , FNameTH _
      , LNameTH _
      , FNameEN _
      , LNameEN _
      , Contact _
      , Tel _
      , Email _
      , PositionID _
      , DepartmentUID _
      , MajorID _
      , PositionPrefixID _
      , Educate _
      , WorkStatus _
      , isExclusive _
      , isFaculty _
      , isTeacher _
      , isStaff _
      , isAlumni _
      , StatusFlag _
      , CUser, Workplace)


    End Function

    Public Function PersonWork_Save(ByVal PersonID As Integer, Award As String, Course_Main As String, Course_Coordinatior As String, MUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PersonWork_Save"), PersonID, Award, Course_Main, Course_Coordinatior, MUser)
    End Function
    Public Function Person_UpdateActiveStatus(ByVal PersonID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_UpdateActiveStatus", PersonID)
    End Function
    Public Function Person_UpdateFileName(ByVal PersonID As Integer, fname As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_UpdateFileName", PersonID, fname)

    End Function

    Public Function Person_Delete(ByVal PersonID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Person_Delete", PersonID)

    End Function
    Public Function RPT_Person_HeadingCV(ByVal lang As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_HeadingCV", lang)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Person_PrintCV(ByVal PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_PrintCV", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Person_Education(ByVal PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_Education", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Person_Research(ByVal PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_Research", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Person_Training(ByVal PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_Training", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Person_Work(ByVal PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Person_Work", PersonID)
        Return ds.Tables(0)
    End Function

    Public Function Teacher_UpdateSmall(ByVal PersonID As Integer, Prefix As String, FirstName As String, LastName As String, pPosID As Integer, PositionName As String, pDeptID As Integer, DeptName As String, UpdateBy As String, status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Teacher_UpdateSmall", PersonID, Prefix, FirstName, LastName, pPosID, PositionName, pDeptID, DeptName, UpdateBy, status)

    End Function

#Region "Teacher"
    Public Function Teacher_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Teacher_Get")
        Return ds.Tables(0)
    End Function

    Public Function Teacher_GetSearch(pkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Teacher_GetSearch", pkey)
        Return ds.Tables(0)
    End Function
#End Region

#Region "Education"
    Public Function PersonEducation_Get(ByVal PersonUID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "PersonEducation_Get", PersonUID)
        Return ds.Tables(0)
    End Function

    Public Function PersonEducation_GetByUID(ByVal UID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "PersonEducation_GetByUID", UID)
        Return ds.Tables(0)
    End Function

    Public Function PersonEducation_GetView(ByVal PersonUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PersonEducation_GetView", PersonUID)
        Return ds.Tables(0)
    End Function

    Public Function PersonEducation_Save(ByVal UID As Integer, ByVal PersonID As Integer, ByVal Level As Integer, ByVal DegreeTH As String, ByVal DegreeEN As String, ByVal MajorTH As String, ByVal MajorEN As String, UniversityTH As String, UniversityEN As String, YearTH As String, YearEN As String, MUser As Integer, Remark As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PersonEducation_Save"), UID,
      PersonID _
      , Level _
      , DegreeTH, DegreeEN _
      , MajorTH, MajorEN _
      , UniversityTH, UniversityEN _
      , YearTH, YearEN _
      , MUser, Remark)
    End Function

    Public Function PersonEducation_ActiveStatusQ(ByVal PersonID As Integer, Remark As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PersonEducation_ActiveStatusQ"), PersonID, Remark)
    End Function
    Public Function PersonEducation_DeleteQ(ByVal CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PersonEducation_DeleteQ"), CUser)
    End Function
    Public Function PersonEducation_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PersonEducation_Delete"), UID)
    End Function


    Public Function PersonWork_GetByID(ByVal PersonID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "PersonWork_GetByID", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function PersonWork_GetView(ByVal PersonID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "PersonWork_GetView", PersonID)
        Return ds.Tables(0)
    End Function
#End Region
End Class
