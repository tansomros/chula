Imports Microsoft.ApplicationBlocks.Data
Public Class PC2Controller
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function PC2_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_GetYear")
        Return ds.Tables(0)
    End Function
    Public Function PC2_GetByCoordinator(ByVal EduYear As Integer, pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_GetByCoordinator", EduYear, pID)
        Return ds.Tables(0)
    End Function
    Public Function PC2_GetByLocation(ByVal EduYear As Integer, pLocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_GetByLocation", EduYear, pLocationID)
        Return ds.Tables(0)
    End Function

    Public Function PC2_GetFromAssessment(ByVal EduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_GetFromAssessment", EduYear)
        Return ds.Tables(0)
    End Function
    Public Function PC2_GetByYear(ByVal EduYear As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_GetByYear", EduYear)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Student_GetStudentInPC2(ByVal EduYear As Integer, Optional pKey As String = "") As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Student_GetStudentInPC2", EduYear, pKey)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Student_GetNoRegister(ByVal EduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Student_GetNoRegister", EduYear)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Student_HasPermission(ByVal EduYear As Integer, Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Student_HasPermission", EduYear, Code)
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


    Public Function PC2_Student_GetStudentNoPC2(ByVal EduYear As Integer, pMajor As Integer, pLevel As Integer, Optional pKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PC2_Student_GetStudentNoPC2"), EduYear, pMajor, pLevel, pKey)

        Return ds.Tables(0)
    End Function

    Public Function PC2_Student_Add(ByVal EduYear As Integer, ByVal StudentCode As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Student_Add"), EduYear, StudentCode)
    End Function

    Public Function PC2_Student_Delete(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_Student_Delete", pID)
    End Function

    Public Function PC2_Student_DeleteByMajorAndLevel(EduYear As Integer, pMajorID As Integer, cLevel As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Student_DeleteByMajor"), EduYear, pMajorID, cLevel)
    End Function


#Region "Subject"

    Public Function PC2_Subject_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_Get")
        Return ds.Tables(0)
    End Function

    Public Function PC2_Subject_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_GetActive")
        Return ds.Tables(0)
    End Function

    Public Function PC2_Subject_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function PC2_Subject_GetByYear(EduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_GetByYear", EduYear)
        Return ds.Tables(0)
    End Function
    Public Function PC2_Subject_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_GetBySearch", Search)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Subject_GetNoQuota(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Subject_GetNoQuota", pYear)
        Return ds.Tables(0)
    End Function
    Public Function PC2_Subject_Save(ByVal UID As Integer, ByVal NameTH As String, ByVal NameEN As String, ByVal StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_Subject_Save", UID, NameTH, NameEN, StatusFlag)
    End Function

    Public Function PC2_Subject_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_Subject_Delete", UID)
    End Function

#End Region

#Region "Quota"
    Public Function PC2_Quota_GetByYear(EduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Quota_GetByYear", EduYear)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Quota_GetBySubject(EduYear As Integer, SubjUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Quota_GetBySubject", EduYear, SubjUID)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function
    Public Function PC2_Assessment_GetStudentCount(EduYear As Integer, SubjUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Assessment_GetStudentCount", EduYear, SubjUID)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function


    Public Function PC2_Quota_Save(ByVal REQYEAR As Integer, ByVal SubjectUID As Integer, ByVal QTY As Integer, MUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Quota_Save"), REQYEAR, SubjectUID, QTY, MUser)
    End Function

    Public Function PC2_Quota_Delete(ByVal REQID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Quota_Delete"), REQID)
    End Function

#End Region

#Region "Register"
    Public Function PC2_StudentRegister_GetBySubject(EduYear As Integer, SubjectUID As Integer, DegreeNo As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_StudentRegister_GetBySubject", EduYear, SubjectUID, DegreeNo)
        Return ds.Tables(0)
    End Function
    Public Function PC2_StudentRegister_GetByStudent(EduYear As Integer, StudentCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_StudentRegister_GetByStudent", EduYear, StudentCode)
        Return ds.Tables(0)
    End Function

    Public Function PC2_StudentRegister_CheckAssessment(RID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PC2_StudentRegister_CheckAssessment"), RID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function PC2_StudentRegister_Add(RegYear As String, Student_Code As String, DegreeNo As Integer, SubjectUID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_StudentRegister_Add"), RegYear, Student_Code, DegreeNo, SubjectUID, UpdBy)
    End Function

    Public Function PC2_StudentRegister_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_StudentRegister_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function PC2_StudentRegister_UpdatePriority(ByVal ItemID As Integer, ByVal flage As String, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_StudentRegister_UpdatePriority", ItemID, flage, updBy)
    End Function

    Public Function PC2_StudentRegister_UpdatePriorityItem(ByVal pID As Integer, ByVal pDegree As Integer, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_StudentRegister_UpdatePriorityItem", pID, pDegree, updBy)

    End Function

    Public Function PC2_StudentRegister_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PC2_StudentRegister_Delete", pID)
    End Function
    Public Function PC2_StudentRegister_GetCount(ByVal EduYear As Integer, StudentCode As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_StudentRegister_GetCount", EduYear, StudentCode)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0)
        Else
            Return 0
        End If
    End Function

#End Region

#Region "Assessment"
    Public Function PC2_Assessment_GetCheckDup(RegYear As String, stdcode As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PC2_Assessment_CheckDup"), RegYear, stdcode)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function PC2_Assessment_Get(EduYear As Integer, SubjectUID As Integer, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Assessment_Get", EduYear, SubjectUID, Search)
        Return ds.Tables(0)
    End Function
    Public Function PC2_Assessment_GetByStudent(EduYear As Integer, StudentCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Assessment_GetByStudent", EduYear, StudentCode)
        Return ds.Tables(0)
    End Function

    Public Function PC2_Student_GetStudentNoAssessment(EduYear As Integer, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PC2_Student_GetStudentNoAssessment", EduYear, Search)
        Return ds.Tables(0)
    End Function


    Public Function PC2_Assessment_Add(RegYear As String, SubjectUID As Integer, StudentCode As String, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Assessment_Add"), RegYear, SubjectUID, StudentCode, UpdBy)
    End Function
    Public Function PC2_Assessment_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PC2_Assessment_Delete"), UID)
    End Function
#End Region
End Class
