Imports Microsoft.ApplicationBlocks.Data

Public Class RegisterController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function GetStudentRegister() As DataTable
        SQL = "select * from StudentRegister "

        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function GetStudentRegister_BySearch(pYear As Integer, SubjectCode As String, pkey As String) As DataTable
        
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentRegister_GetBySearch", pYear, SubjectCode, pkey)
        Return ds.Tables(0)
    End Function

    Public Function GetStudentRegister_ByStudent(pYear As Integer, StdCode As String, SubjCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentRegister_GetByStudent", pYear, StdCode, SubjCode)
        Return ds.Tables(0)
    End Function

    Public Function GetStudentRegister_Count(pYear As Integer, StdCode As String, SubjCode As String) As Integer
        SQL = "select Count(*) as RegCount from StudentRegister where REGYEAR=" & pYear & " and Student_Code='" & StdCode & "' and SubjectCode='" & SubjCode & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function GetStudentRegister_CountSelected(pYear As Integer, StdCode As String, SubjCode As String, LID As Integer) As Integer
        SQL = "select Count(*) as RegCount from StudentRegister where REGYEAR=" & pYear & " and Student_Code='" & StdCode & "' and SubjectCode='" & SubjCode & "' and LocationID=" & LID
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function StudentRegister_GetStudentBySex(RegYear As String, LocationID As Integer, Phase As Integer, DegreeNo As Integer, sSex As String, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_GetStudentBySex"), RegYear, LocationID, Phase, DegreeNo, sSex, SubjectCode)

        Return ds.Tables(0)

    End Function

    Public Function StudentRegister_CheckDup(RegYear As String, stdCode As String, Phase As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentRegister_CheckDup"), RegYear, stdCode, Phase)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function StudentRegister_CheckAssessment(RID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentRegister_CheckAssessment"), RID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function



    Public Function StudentRegister_GetStudentNoAssessment(RegYear As Integer, SubjCode As String, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentRegister_NotAssessment"), RegYear, SubjCode, sKey)
        Return ds.Tables(0)

    End Function
    Public Function StudentRegister_GetStudentNoRegister(RegYear As Integer, SubjCode As String, CID As Integer, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentRegister_NotRegister"), RegYear, SubjCode, CID, sKey)
        Return ds.Tables(0)

    End Function

    Public Function StudentRegister_Add(RegYear As String, Student_Code As String, DegreeNo As Integer, LocationID As Integer, SubjectCode As String, Phase As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentRegister_Add"), RegYear, Student_Code, DegreeNo, LocationID, SubjectCode, Phase, UpdBy)
    End Function

    Public Function StudentRegister_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentRegister_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function StudentRegister_UpdatePriority(ByVal ItemID As Integer, ByVal flage As String, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentRegister_UpdatePriority", ItemID, flage, updBy)
    End Function

    Public Function StudentRegister_UpdatePriorityItem(ByVal pID As Integer, ByVal pDegree As Integer, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentRegister_UpdatePriorityItem", pID, pDegree, updBy)

    End Function

    Public Function StudentRegister_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentRegister_Delete", pID)
    End Function


    Public Function UserRole_chkHasRole(ByVal pUser As String, roleID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserRole_chkHasRole"), pUser, roleID)
        Return ds.Tables(0)
    End Function

End Class
