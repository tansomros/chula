Imports Microsoft.ApplicationBlocks.Data
Public Class StudentController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Student_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_Get")
        Return ds.Tables(0)
    End Function
    Public Function Student_Get4Select() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_Get4Select")
        Return ds.Tables(0)
    End Function
    Public Function GetPhaseNoOfAssessment(BYear As Integer, StdCode As String, subjCode As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "GetPhaseNoOfAssessment", BYear,StdCode,subjCode)
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

    Public Function Student_GetLevelClass() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Student_GetLevelClass"))
        Return ds.Tables(0)
    End Function

    Public Function Student_GetCountByLevelClass(Level As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Student_GetCountByLevelClass"), Level)
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


    Public Function GetStudentBio_ByMajorID(id As Integer) As DataTable
        SQL = "select * from  View_Student_Bio  where  MajorID=" & id
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)

    End Function

    Public Function GetStudentBio_ByID(id As String) As DataTable
        SQL = "select * from  View_Student_Bio  where Student_Code='" & id & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function
    Public Function Student_GetByStatus(pStatus As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetByStatus", pStatus)
        Return ds.Tables(0)
    End Function

    Public Function Student_GetNoUser() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetNoUser")
        Return ds.Tables(0)
    End Function


    Public Function GetStudent_BySearch(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetSearch", id)
        Return ds.Tables(0)
    End Function
    Public Function Student_NoUserBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetNoUserBySearch", Search)
        Return ds.Tables(0)
    End Function


    Public Function GetStudent_ByQuery(id As String) As DataTable
        SQL = "select * from  View_Student_Bio  where " & id & " order by firstname "
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function


    Public Function GetStudent_BySearch4Print(MajorID As String, Adv As Integer, lvclass As String, pKey As String) As DataTable

        'SQL = "select IIF(CONVERT(varchar,StudentStatus)=40,'สำเร็จการศึกษา',CONVERT(varchar,LevelClass)) ClassName  , * from  View_Student_Bio  where (Student_Code like '%" & pKey & "%' OR FirstName like '%" & pKey & "%' OR  LastName like '%" & pKey & "%') "

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
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentBio_GetSearch", MajorID, Adv, lvclass, pKey)
        Return ds.Tables(0)
    End Function


    Public Function TMP_Student_SET2BIO(ByVal pCode As String, user As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Student_SET2BIO"), pCode, user)
        'SQL = "Insert Into tmpStudentBio  Select Student_ID,'" & user & "' From Students  where " & pCode
        'Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
    Public Function TMP_Student_SET2BIOBySTDID(ByVal pCode As String, user As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Student_SET2BIOBySTDID"), pCode, user)
    End Function

    Public Function TMP_Student_SET2CERT(ByVal pCode As String, user As String, DocumentDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Student_SET2CERT"), pCode, user, DocumentDate)
    End Function
    Public Function TMP_Student_SET2CERTBySTDID(ByVal pCode As String, user As String, DocumentDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TMP_Student_SET2CERTBySTDID"), pCode, user, DocumentDate)
    End Function

    Public Function Student_UpdateSmall(ByVal StdCode_Old As String, ByVal StdCode_New As String, ByVal FirstName As String, ByVal LastName As String, MajorID As Integer, MinorID As Integer, SubMinorID As Integer, AdvisorID As Integer, gpax As Double, lvClass As Integer, UpdateBy As String, StdStatus As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Student_UpdateSmall", StdCode_Old, StdCode_New, FirstName, LastName, MajorID, MinorID, SubMinorID, AdvisorID, gpax, lvClass, UpdateBy, StdStatus)

    End Function


    Public Function Student_Delete(ByVal pID As String) As Integer
       
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Student_Delete", pID)

    End Function

    Public Function StudentLevel_UpdateOneLevel() As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentLevel_UpdateOneLevel")
    End Function

    Public Function StudentLevel_UpdateByCode(ByVal pID As String, pLevel As Integer, pPass As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentLevel_UpdateByCode", pID, pLevel, pPass)
    End Function


    Public Function StudentStatus_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentStatus_GetAll")
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
    Public Function COVIDScreening_GetByStudent(StudentCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_GetByStudent", StudentCode)
        Return ds.Tables(0)
    End Function

    Public Function COVIDScreening_GetByLocation(LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "COVIDScreening_GetByLocation", LocationID)
        Return ds.Tables(0)
    End Function
    Public Function COVIDScreening_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "COVIDScreening_Delete", UID)
    End Function

    Public Function StudentPhase_Get(PYear As Integer, StdCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentPhase_Get", PYear, StdCode)
        Return ds.Tables(0)
    End Function
    Public Function StudentPhase_GetLocation(PYear As Integer, StdCode As String, PhaseNo As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentPhase_GetLocation", PYear, StdCode, PhaseNo)
        Return ds.Tables(0)
    End Function

    Public Function COVIDScreening_Save(UID As Integer, StudentCode As String, PhaseNo As String, LocationID As String, IsSymptom1 As String, IsSymptom2 As String, IsSymptom3 As String, IsSymptom4 As String, IsSymptom5 As String, IsNoSymptom As String, IsTravel As String, TravelDesc As String, TravelStart As String, TravelEnd As String, IsHistory As String, HistoryDesc As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "COVIDScreening_Save", UID, StudentCode, PhaseNo, LocationID, IsSymptom1, IsSymptom2, IsSymptom3, IsSymptom4, IsSymptom5, IsNoSymptom, IsTravel, TravelDesc, TravelStart, TravelEnd, IsHistory, HistoryDesc, CUser)
    End Function

    Public Function Student_GetSex(StudentCode As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetSex", StudentCode)
        Return String.Concat(ds.Tables(0).Rows(0)(0))
    End Function
    '-----------------

    Public Function Student_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetAll")
        Return ds.Tables(0)
    End Function

    Public Function Student_GetAlumni() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetAlumni")
        Return ds.Tables(0)
    End Function

    Public Function GetStudent_ByID(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetByUID", id)
        Return ds.Tables(0)

    End Function

    Public Function Student_GetCode(PID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_GetCode", PID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If

    End Function
    Public Function Student_Add(ByVal pCode As String, ByVal pFName As String, pLName As String, MajorID As Integer, MinorID As Integer, SubMinorID As Integer, AdvisorID As Integer, gpax As Double, lvClass As Integer, StdStatus As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Student_Add"), pCode, pFName, pLName, MajorID, MinorID, SubMinorID, AdvisorID, gpax, lvClass, StdStatus)
    End Function


    Public Function Student_Update(ByVal StdCode As String, ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal NickName As String, ByVal Gender As String, ByVal Email As String, ByVal BirthDay As String, ByVal BloodGroup As String, ByVal Telephone As String, ByVal Mobile As String, ByVal CongenitalDisease As String, ByVal MedicineUsually As String, ByVal MedicalHistory As String, ByVal Hobby As String, ByVal Talent As String, ByVal Expectations As String, ByVal Father_FirstName As String, ByVal Father_LastName As String, ByVal Father_Career As String, ByVal Father_Tel As String, ByVal Mother_FirstName As String, ByVal Mother_LastName As String, ByVal Mother_Career As String, ByVal Mother_Tel As String, ByVal Sibling As Integer, ByVal ChildNo As Integer, ByVal Address As String, ByVal District As String, ByVal City As String, ByVal ProvinceID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal PrimarySchool As String, ByVal PrimaryProvince As String, ByVal PrimaryYear As Integer, ByVal SecondarySchool As String, ByVal SecondaryProvince As String, ByVal SecondaryYear As Integer, ByVal HighSchool As String, ByVal HighProvince As String, ByVal HighYear As Integer, ByVal ContactName As String, ByVal ContactTel As String, ByVal ContactAddress As String, ByVal ContactRelation As String, picpath As String, UpdateBy As String, gpax As Double, AddressLive As String, TelLive As String, FirstNameEN As String, LastNameEN As String, Religion As String, MedicalRecommend As String, HospitalName As String, DoctorName As String, Characteristic As String, FavoriteSubject As String, EduExperience As String, PayorID As Integer, PayorDetail As String, CVLink As String, LineID As String, BankCode As String, BankAccount As String, WorkPlace As String, SizeGown As String, CovidInsure As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Student_Update", StdCode, Prefix, FirstName, LastName, NickName, Gender, Email, Telephone, Mobile, BirthDay, BloodGroup, Father_FirstName, Father_LastName, Father_Career, Father_Tel, Mother_FirstName, Mother_LastName, Mother_Career, Mother_Tel, Sibling, ChildNo, Address, District, City, ProvinceID, ProvinceName, ZipCode, CongenitalDisease, MedicineUsually, MedicalHistory, Hobby, Talent, Expectations, PrimarySchool, PrimaryProvince, PrimaryYear, SecondarySchool, SecondaryProvince, SecondaryYear, HighSchool, HighProvince, HighYear, ContactName, ContactAddress, ContactTel, ContactRelation, picpath, UpdateBy, gpax, AddressLive, TelLive, FirstNameEN, LastNameEN, Religion, MedicalRecommend, HospitalName, DoctorName, Characteristic, FavoriteSubject, EduExperience, PayorID, PayorDetail, CVLink, LineID, BankCode, BankAccount, WorkPlace, SizeGown, CovidInsure)

    End Function



#Region "Certification"
    Public Function Certificate_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Certificate_Get")
        Return ds.Tables(0)
    End Function
    Public Function Certificate_GetByStudent(id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Certificate_GetByStudent", id)
        Return ds.Tables(0)
    End Function
    Public Function Certificate_Add(ByVal StdID As Integer, DocumentUID As String, DocName As String, FilePath As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Certificate_Add", StdID, DocumentUID, DocName, FilePath, CUser)
    End Function
    Public Function Certificate_Delete(ByVal UID As Integer, StdID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Certificate_Delete", UID, StdID)
    End Function

#End Region

End Class
