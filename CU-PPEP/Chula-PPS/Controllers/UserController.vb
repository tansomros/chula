Imports Microsoft.ApplicationBlocks.Data

Public Class UserController
    Inherits ApplicationBaseClass
    Public ds As New DataSet
    Public Function User_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Get"))
        Return ds.Tables(0)
    End Function

    Public Function User_CheckLogin(ByVal pUsername As String, ByVal pPassword As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Login"), pUsername, pPassword)
        Return ds.Tables(0)
    End Function
    Public Function User_GetPassword(ByVal Username As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetPassword"), Username)
        If ds.Tables(0).Rows.Count > 0 Then
            Return String.Concat(ds.Tables(0).Rows(0)(0))
        Else
            Return "none"
        End If

    End Function
    Public Function User_GetCountNoUser(ByVal gID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetCountNoUser"), gID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function User_GetByUserID(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByUserID"), UserID)
        Return ds.Tables(0)
    End Function
    Public Function User_GetLocationIdByUserID(ByVal UserID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetLocationIdByUserID"), UserID)

        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function

    Public Function User_GetInfo(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetInfo"), UserID)
        Return ds.Tables(0)
    End Function
    Public Function User_CheckDuplicate(ByVal Username As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_CheckDuplicate"), Username)
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
    Public Function User_GetUID(ByVal Username As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetUID"), Username)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function Users_Online() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Users_Online"))
        Return ds.Tables(0)
    End Function


    Public Function GetUsers_ByUsername(id As String) As DataTable
        SQL = "select * from Users where username ='" & id & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function User_GetEmail(pKey As String) As DataTable
        SQL = "select *  from  Users where username ='" & pKey & "' Or Email='" & pKey & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function User_GetMail(UserId As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "User_GetMail", UserId)
        If ds.Tables(0).Rows.Count > 0 Then
            Return String.Concat(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If

    End Function


    Public Function User_GetBySearch(Grp As Integer, id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "User_GetSearch", Grp, id)
        Return ds.Tables(0)
    End Function
    Public Function User_GetNameByUserID(ByVal userid As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetNameByUserID"), userid)
        Return String.Concat(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function User_GetNameByUsername(ByVal username As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetNameByUsername"), username)
        Return String.Concat(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function User_LastLog_Update(ByVal pUsername As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_LastLog_Update"), pUsername)
    End Function
    Public Function User_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Delete"), pID)
    End Function

    'Public Function User_Add(ByVal Username As String, ByVal Password As String, ByVal FName As String, ByVal LName As String, ByVal Email As String, ByVal IsSuperUser As Integer, ByVal Status As Integer, ByVal UserProfileID As Integer, ByVal ProfileID As String) As Integer

    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Add"), Username, Password, FName, LName, Email, IsSuperUser, Status, UserProfileID, ProfileID)
    'End Function
    Public Function User_Add(ByVal Username As String, ByVal Password As String, ByVal DisplayName As String, ByVal IsSuperUser As Integer, ByVal StatusFlag As String, ByVal UserProfileID As Integer, ByVal ProfileID As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Add"), Username, Password, DisplayName, IsSuperUser, StatusFlag, UserProfileID, ProfileID)
    End Function

    Public Function User_Save(ByVal Username As String, ByVal Password As String, ByVal FName As String, ByVal LName As String, ByVal Email As String, ByVal IsSuperUser As Integer, ByVal Status As Integer, ByVal UserProfileID As Integer, ByVal ProfileID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Save"), Username, Password, FName, LName, Email, IsSuperUser, Status, UserProfileID, ProfileID)
    End Function

    Public Function User_Update(ByVal Username As String, ByVal Password As String, ByVal FName As String, ByVal LName As String, ByVal Email As String, ByVal IsSuperUser As Integer, ByVal StatusFlag As String, ByVal UserProfileID As Integer, ByVal ProfileID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Update"), Username, Password, FName, LName, Email, IsSuperUser, StatusFlag, UserProfileID, ProfileID)
    End Function

    Public Function User_Update(ByVal UserID As Integer, ByVal Password As String, ByVal DisplayName As String, ByVal Email As String, ByVal IsSuperUser As Integer, ByVal Status As String, ByVal ProfileID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_UpdateDatail"), UserID, Password, DisplayName, Email, IsSuperUser, Status, ProfileID)
    End Function

    Public Function User_CheckPolicyAccept(ByVal UserID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_CheckPolicyAccept"), UserID)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return String.Concat(ds.Tables(0).Rows(0)(0))
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Function User_AcceptPolicy(ByVal UserID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_AcceptPolicy"), UserID)
    End Function

    Public Function User_ChangeUsername(ByVal Username As String, ByVal Username2 As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_ChangeUsername"), Username, Username2)
    End Function

    Public Function UserRole_Add(ByVal Username As String, ByVal RoleID As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRole_Add"), Username, RoleID, UpdBy)
    End Function
    Public Function UserRole_Save(ByVal Username As String, ByVal RoleID As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRole_Save"), Username, RoleID, UpdBy)
    End Function

    Public Function User_UpdateProfileID(ByVal Username As String, sProfileID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_UpdateProfileID"), Username, sProfileID)
    End Function

    Public Function UserRole_UpdateStatus(Username As String, RoleID As Integer, bActive As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRole_UpdateStatus"), Username, RoleID, bActive, UpdBy)
    End Function

    Public Function User_ChangePassword(ByVal Username As String, ByVal Password As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_ChangePassword"), Username, Password)
    End Function

    Public Function User_GenLogfile(ByVal Username As String, ByVal Act_Type As String, DB_Effective As String, Descrp As String, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_genLogfile"), Username, Act_Type, DB_Effective, Descrp, Remark)
    End Function

    Public Function User_UpdateMail(ByVal Username As String, email As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_UpdateEmail"), Username, email)
    End Function


    Public Function SendAlert_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("SendAlert_Get"))
        Return ds.Tables(0)
    End Function
    Public Function SendAlert_GetMail() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("SendAlert_GetMail"))
        Return ds.Tables(0)
    End Function

    Public Function SendAlert_Save(ByVal LocationID As Integer, TimePhaseID As Integer, email As String, Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SendAlert_Save"), LocationID, TimePhaseID, email, Status)
    End Function
    Public Function SendAlert_UpdateStatus(ByVal LocationID As Integer, TimePhaseID As Integer, Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SendAlert_UpdateStatus"), LocationID, TimePhaseID, Status)
    End Function
    Public Function LocationAlertMail_Get(ByVal EduYear As Integer, PhaseID As Integer, SubjectCode As String, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LocationAlertMail_Get"), EduYear, PhaseID, SubjectCode, Status)
        Return ds.Tables(0)
    End Function
    Public Function AssessmentAlertMail_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentAlertMail_Get"))
        Return ds.Tables(0)
    End Function
    Public Function AssessmentMailAlert_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentMailAlert_Get"))
        Return ds.Tables(0)
    End Function

    Public Function AssessmentAlert_GetMail(ByVal EduYear As Integer, PhaseID As Integer, SubjectCode As String, Status As String) As DataTable
        ' ครั้งละ 20 เมล์
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentAlert_GetMail"), EduYear, PhaseID, SubjectCode, Status)
        Return ds.Tables(0)
    End Function
    Public Function AssessmentAlert_GetMailByLocation(ByVal EduYear As Integer, PhaseID As Integer, LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentAlert_GetMailByLocation"), EduYear, PhaseID, LocationID)
        Return ds.Tables(0)
    End Function

    Public Function AssessmentAlert_GetMailByPerson(ByVal EduYear As Integer, PersonID As Integer, PhaseID As Integer, LocationID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentAlert_GetMailByPerson"), EduYear, PersonID, PhaseID, LocationID)
        Return ds.Tables(0)
    End Function
    Public Function AssessmentAlert_Save(ByVal LocationID As Integer, PhaseID As Integer, email As String, Status As String, username As String, password As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentAlert_Save"), LocationID, PhaseID, email, Status, username, password)
    End Function
    Public Function AssessmentAlert_UpdateStatus(ByVal LocationID As Integer, PhaseID As Integer, Status As String, Email As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentAlert_UpdateStatus"), LocationID, PhaseID, Status, Email)
    End Function
End Class
Public Class UserRoleController
    Inherits ApplicationBaseClass
    Public ds As New DataSet

    Public Function UserRole_GetByUserID(ByVal pUserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserRole_GetByUserID"), pUserID)
        Return ds.Tables(0)
    End Function

    Public Function UserRole_GetActiveRoleByUID(ByVal pUserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserRole_GetActiveRoleByUID"), pUserID)
        Return ds.Tables(0)
    End Function


    Public Function UserAction_CheckByUser(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserAction_CheckByUser"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function UserAction_Checked(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserAction_Checked"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function UserAction_Add(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserAction_Add"), Username, LocationID, RoleAction, UpdBy)
    End Function
    Public Function UserAction_Update(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserAction_Update"), Username, LocationID, RoleAction, UpdBy)
    End Function


End Class


Public Class VPNController
    Inherits ApplicationBaseClass
    Public ds As New DataSet
    Public Function VPNAccount_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("VPNAccount_Get"))
        Return ds.Tables(0)
    End Function

    Public Function VPNAccount_GetByLocation(LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("VPNAccount_GetByLocation"), LocationID)
        Return ds.Tables(0)
    End Function
    Public Function VPNAccount_Save(LocationID As Integer, Username As String, Password As String, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("VPNAccount_Save"), LocationID, Username, Password, StatusFlag)
    End Function
    Public Function VPNAccount_Delete(LocationID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("VPNAccount_Delete"), LocationID)
    End Function
End Class