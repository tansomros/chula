Imports Microsoft.ApplicationBlocks.Data
Public Class PharmacistController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Pharmacist_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_Get")
        Return ds.Tables(0)
    End Function
    Public Function Pharmacist_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetAll")
        Return ds.Tables(0)
    End Function

    Public Function GetPharmacist_ByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetByUID", id)
        Return ds.Tables(0)

    End Function

    Public Function Pharmacist_GetCode(PID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetCode", PID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If

    End Function

    Public Function GetAssessmentInfo(BYear As Integer, StdCode As String, subjCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "GetAssessmentInfo", BYear, StdCode, subjCode)
        Return ds.Tables(0)
    End Function



    Public Function Pharmacist_GetLevelClass() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Pharmacist_GetLevelClass"))
        Return ds.Tables(0)
    End Function

    Public Function Pharmacist_GetCountByLevelClass(Level As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Pharmacist_GetCountByLevelClass"), Level)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) > 0 Then
                Return ds.Tables(0).Rows(0)(0)
            Else
                Return 0
            End If
        Else
            Return 0
        End If

    End Function

    Public Function Pharmacist_GetByStatus(pStatus As String, id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetByStatus", pStatus, id)
        Return ds.Tables(0)
    End Function

    Public Function Pharmacist_GetNoUser() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetNoUser")
        Return ds.Tables(0)
    End Function


    Public Function GetPharmacist_BySearch(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetSearch", id)
        Return ds.Tables(0)
    End Function
    Public Function Pharmacist_NoUserBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetNoUserBySearch", Search)
        Return ds.Tables(0)
    End Function

    Public Function GetPharmacist_BySearch4Print(MajorID As String, MinorID As String, SubMinor As String, Adv As Integer, lvclass As String, pKey As String) As DataTable

        'SQL = "select IIF(CONVERT(varchar,Pharmacisttatus)=40,'สำเร็จการศึกษา',CONVERT(varchar,LevelClass)) ClassName  , * from  View_Pharmacist_Bio  where (Code like '%" & pKey & "%' OR FirstName like '%" & pKey & "%' OR  LastName like '%" & pKey & "%') "

        'If MajorID <> 0 Then
        '    SQL &= " And MajorID=" & MajorID
        'End If

        'If MinorID <> 0 Then
        '    SQL &= " And MinorID=" & MinorID
        'End If

        'If SubMinor <> 0 Then
        '    SQL &= " And SubMinorID=" & SubMinor
        'End If

        'If Adv <> 0 Then
        '    SQL &= " And AdvisorID=" & Adv
        'End If

        'If lvclass <> 0 Then
        '    SQL &= " And LevelClass=" & lvclass
        'End If


        'SQL &= " order by LevelClass ,firstname "
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PharmacistBio_GetSearch", MajorID, MinorID, SubMinor, Adv, lvclass, pKey)
        Return ds.Tables(0)
    End Function

    Public Function Pharmacist_Add(ByVal pCode As String, ByVal pFName As String, pLName As String, MajorID As Integer, MinorID As Integer, SubMinorID As Integer, AdvisorID As Integer, gpax As Double, lvClass As Integer, StdStatus As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Pharmacist_Add"), pCode, pFName, pLName, MajorID, MinorID, SubMinorID, AdvisorID, gpax, lvClass, StdStatus)
    End Function
    Public Function TMP_Pharmacist_SET2BIO(ByVal pCode As String, user As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Pharmacist_SET2BIO"), pCode, user)
        'SQL = "Insert Into tmpPharmacistBio  Select UID,'" & user & "' From Pharmacist  where " & pCode
        'Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
    Public Function TMP_Pharmacist_SET2BIOBySTDID(ByVal pCode As String, user As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Pharmacist_SET2BIOBySTDID"), pCode, user)
    End Function

    Public Function TMP_Pharmacist_SET2CERT(ByVal pCode As String, user As String, DocumentDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Pharmacist_SET2CERT"), pCode, user, DocumentDate)
    End Function
    Public Function TMP_Pharmacist_SET2CERTBySTDID(ByVal pCode As String, user As String, DocumentDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Pharmacist_SET2CERTBySTDID"), pCode, user, DocumentDate)
    End Function


    Public Function Pharmacist_Update(ByVal StdCode As String, ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal NickName As String, ByVal Gender As String, ByVal Email As String, ByVal BirthDay As String, ByVal BloodGroup As String, ByVal Telephone As String, ByVal Mobile As String, ByVal CongenitalDisease As String, ByVal MedicineUsually As String, ByVal MedicalHistory As String, ByVal Hobby As String, ByVal Talent As String, ByVal Expectations As String, ByVal Father_FirstName As String, ByVal Father_LastName As String, ByVal Father_Career As String, ByVal Father_Tel As String, ByVal Mother_FirstName As String, ByVal Mother_LastName As String, ByVal Mother_Career As String, ByVal Mother_Tel As String, ByVal Sibling As Integer, ByVal ChildNo As Integer, ByVal Address As String, ByVal District As String, ByVal City As String, ByVal ProvinceID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal PrimarySchool As String, ByVal PrimaryProvince As String, ByVal PrimaryYear As Integer, ByVal SecondarySchool As String, ByVal SecondaryProvince As String, ByVal SecondaryYear As Integer, ByVal HighSchool As String, ByVal HighProvince As String, ByVal HighYear As Integer, ByVal ContactName As String, ByVal ContactTel As String, ByVal ContactAddress As String, ByVal ContactRelation As String, picpath As String, UpdateBy As String, gpax As Double, AddressLive As String, TelLive As String, FirstNameEN As String, LastNameEN As String, Religion As String, MedicalRecommend As String, HospitalName As String, DoctorName As String, Characteristic As String, FavoriteSubject As String, EduExperience As String, PayorID As Integer, PayorDetail As String, CVLink As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Pharmacist_Update2", StdCode, Prefix, FirstName, LastName, NickName, Gender, Email, Telephone, Mobile, BirthDay, BloodGroup, Father_FirstName, Father_LastName, Father_Career, Father_Tel, Mother_FirstName, Mother_LastName, Mother_Career, Mother_Tel, Sibling, ChildNo, Address, District, City, ProvinceID, ProvinceName, ZipCode, CongenitalDisease, MedicineUsually, MedicalHistory, Hobby, Talent, Expectations, PrimarySchool, PrimaryProvince, PrimaryYear, SecondarySchool, SecondaryProvince, SecondaryYear, HighSchool, HighProvince, HighYear, ContactName, ContactAddress, ContactTel, ContactRelation, picpath, UpdateBy, gpax, AddressLive, TelLive, FirstNameEN, LastNameEN, Religion, MedicalRecommend, HospitalName, DoctorName, Characteristic, FavoriteSubject, EduExperience, PayorID, PayorDetail, CVLink)

    End Function

    Public Function Pharmacist_UpdateSmall(ByVal StdCode_Old As String, ByVal StdCode_New As String, ByVal FirstName As String, ByVal LastName As String, MajorID As Integer, MinorID As Integer, SubMinorID As Integer, AdvisorID As Integer, gpax As Double, lvClass As Integer, UpdateBy As String, StdStatus As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Pharmacist_UpdateSmall", StdCode_Old, StdCode_New, FirstName, LastName, MajorID, MinorID, SubMinorID, AdvisorID, gpax, lvClass, UpdateBy, StdStatus)

    End Function


    Public Function Pharmacist_Delete(ByVal pID As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Pharmacist_Delete", pID)

    End Function

    Public Function PharmacistLevel_UpdateOneLevel() As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PharmacistLevel_UpdateOneLevel")
    End Function

    Public Function PharmacistLevel_UpdateByCode(ByVal pID As String, pLevel As Integer, pPass As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PharmacistLevel_UpdateByCode", pID, pLevel, pPass)
    End Function

    Public Function PharmacistStatus_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PharmacistStatus_GetAll")
        Return ds.Tables(0)
    End Function


#Region "Recommend"

#End Region

    Public Function COVIDScreening_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function COVIDScreening_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_Get")
        Return ds.Tables(0)
    End Function
    Public Function COVIDScreening_GetByPharmacist(PharmacistCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_GetByPharmacist", PharmacistCode)
        Return ds.Tables(0)
    End Function

    Public Function COVIDScreening_GetByLocation(LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_GetByLocation", LocationID)
        Return ds.Tables(0)
    End Function
    Public Function COVIDScreening_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "COVIDScreening_Delete", UID)
    End Function

    Public Function PharmacistPhase_Get(PYear As Integer, StdCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PharmacistPhase_Get", PYear, StdCode)
        Return ds.Tables(0)
    End Function
    Public Function PharmacistPhase_GetLocation(PYear As Integer, StdCode As String, PhaseNo As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PharmacistPhase_GetLocation", PYear, StdCode, PhaseNo)
        Return ds.Tables(0)
    End Function

    Public Function COVIDScreening_Save(UID As Integer, PharmacistCode As String, PhaseNo As String, LocationID As String, IsSymptom1 As String, IsSymptom2 As String, IsSymptom3 As String, IsSymptom4 As String, IsSymptom5 As String, IsNoSymptom As String, IsTravel As String, TravelDesc As String, TravelStart As String, TravelEnd As String, IsHistory As String, HistoryDesc As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "COVIDScreening_Save", UID, PharmacistCode, PhaseNo, LocationID, IsSymptom1, IsSymptom2, IsSymptom3, IsSymptom4, IsSymptom5, IsNoSymptom, IsTravel, TravelDesc, TravelStart, TravelEnd, IsHistory, HistoryDesc, CUser)
    End Function

    Public Function Pharmacist_GetSex(PharmacistCode As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Pharmacist_GetSex", PharmacistCode)
        Return String.Concat(ds.Tables(0).Rows(0)(0))
    End Function
End Class
