Imports Microsoft.ApplicationBlocks.Data

Public Class UserController
    Inherits ApplicationBaseClass
    Public ds As New DataSet

    'Public Function User_Get() As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Get"))
    '    Return ds.Tables(0)
    'End Function

    'Public Function User_CheckLogin(ByVal pUsername As String, ByVal pPassword As String) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Login"), pUsername, pPassword)
    '    Return ds.Tables(0)
    'End Function

    Public Function User_GetByUserID(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByUserID"), UserID)
        Return ds.Tables(0)
    End Function

    'Public Function User_GetByID(ByVal pID As Integer) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByID"), pID)
    '    Return ds.Tables(0)
    'End Function

    Public Function User_GetCountNoUser(ByVal gID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetCountNoUser"), gID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
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
    'Public Function Users_Online() As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Users_Online"))
    '    Return ds.Tables(0)
    'End Function

    Public Function User_GetBySearch(CompanyUID As Integer, Grp As Integer, id As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "User_GetSearch", CompanyUID, Grp, id)
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

    'Public Function User_LastLog_Update(ByVal pUsername As String) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_LastLog_Update"), pUsername)
    'End Function
    'Public Function User_Delete(ByVal pID As Integer) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Delete"), pID)
    'End Function 
    Public Function User_SaveByUser(UserID As Integer, ByVal FName As String, ByVal Tel As String, ByVal Email As String, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_SaveByUser"), UserID, FName, Tel, Email, MUser)
    End Function
    Public Function User_Save(UserID As Integer, ByVal Name As String, ByVal Username As String, ByVal Password As String, UserGroupUID As Integer, RoleID As Integer, ByVal PersonUID As String, ByVal Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Save"), UserID, Name, Username, Password, UserGroupUID, RoleID, PersonUID, Status)
    End Function

    Public Function User_Update(UserID As Integer, ByVal Name As String, ByVal Username As String, ByVal Password As String, UserGroupUID As Integer, ByVal RoleID As Integer, ByVal PersonID As Integer, ByVal StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Update"), UserID, Name, Username, Password, UserGroupUID, RoleID, PersonID, StatusFlag)
    End Function

    Public Function User_CheckDuplicate(ByVal pUser As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByUsername"), pUser)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function User_UpdateFileName(ByVal UserID As Integer, fname As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "User_UpdateFileName", UserID, fname)

    End Function
    Public Function UserCompany_GetByUserID(UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "UserCompany_GetByUserID", UserID)
        Return ds.Tables(0)
    End Function
    Public Function UserCompany_GetByUsername(Username As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "UserCompany_GetByUsername", Username)
        Return ds.Tables(0)
    End Function
    Public Function User_ChangeUsername(ByVal Username As String, ByVal Username2 As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_ChangeUsername"), Username, Username2)
    End Function

    Public Function UserRole_Add(ByVal Username As String, ByVal RoleID As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRole_Add"), Username, RoleID, UpdBy)
    End Function
    Public Function UserRole_Save(ByVal UserID As String, ByVal GroupUID As Integer, StatusFlag As String, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserAccessGroup_Save"), UserID, GroupUID, StatusFlag, UpdBy)
    End Function

    Public Function User_UpdateProfileID(ByVal Username As String, sProfileID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_UpdateProfileID"), Username, sProfileID)
    End Function

    'Public Function UserRole_UpdateStatus(Username As String, RoleID As Integer, bActive As Integer, UpdBy As String) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRole_UpdateStatus"), Username, RoleID, bActive, UpdBy)
    'End Function

    'Public Function User_ChangePassword(ByVal Username As String, ByVal Password As String) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_ChangePassword"), Username, Password)
    'End Function

    Public Function User_GenLogfile(ByVal Username As String, ByVal Act_Type As String, DB_Effective As String, Descrp As String, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_genLogfile"), Username, Act_Type, DB_Effective, Descrp, Remark)
    End Function

    'Public Function User_UpdateMail(ByVal Username As String, email As String) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_UpdateEmail"), Username, email)
    'End Function
    Public Function UserCompany_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserCompany_Delete"), UID)
    End Function
    Public Function UserCompany_Save(ByVal UserID As Integer, Username As String, CompanyUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserCompany_Save"), UserID, Username, CompanyUID)
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

    Public Function User_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_Get"))
        Return ds.Tables(0)
    End Function

    Public Function User_Login(ByVal pUsername As String, ByVal pPassword As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_Login"), pUsername, pPassword)
        Return ds.Tables(0)
    End Function
    Public Function User_GetByUsername(ByVal Username As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetByUsername"), Username)
        Return ds.Tables(0)
    End Function

    Public Function User_GetProjectRole(ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetProjectRole"), pID)
        Return ds.Tables(0)
    End Function
    Public Function User_GetPersonCode(ByVal pID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetPersonCode"), pID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function
    Public Function User_GetByID(ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetByID"), pID)
        Return ds.Tables(0)
    End Function
    Public Function User_GetUserID(ByVal Username As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetUserID"), Username)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function User_GetBySearch(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetBySearch"), pID)
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

    Public Function GetUsersID_ByUsername(id As String) As Integer
        SQL = "select UserID  from Users where username ='" & id & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function UserLogFile_GetVisitCount() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserLogFile_GetVisitCount"))
        Return ds.Tables(0)
    End Function

    Public Function GetUsers_BySearch(id As String) As DataTable
        SQL = "select * from  View_User  where username like '%" & id & "%' OR FirstName like '%" & id & "%'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function GetUsers_ByGroupSearch(proj As Integer, Grp As Integer, KeySearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Users_GetByGroupSearch", proj, Grp, KeySearch)
        Return ds.Tables(0)
    End Function

    Public Function LocationProject_GetByLocationID(LID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationProject_GetByLocationID", LID)
        Return ds.Tables(0)
    End Function
    Public Function LocationProject_GetByUsername(username As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationProject_GetByUsername", username)
        Return ds.Tables(0)
    End Function
    Public Function User_GetName(ByVal UserID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetName"), UserID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return String.Concat(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function

    'Public Function User_GetActionByID(ByVal pID As String) As String
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, ("User_GetActionByID"), pID)
    '    Return ds.Tables(0).Rows(0)(0)
    'End Function


    Public Function User_LastLog_Update(ByVal pUsername As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_LastLog_Update"), pUsername)
    End Function
    Public Function User_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_Delete"), pID)
    End Function

    Public Function User_DeleteByUsername(ByVal pID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_DeleteByUsername"), pID)
    End Function
    Public Function User_Add(Name As String, ByVal Username As String, ByVal Password As String, UserGroupUID As Integer, ByVal RoleID As Integer, ByVal PersonID As Integer, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_Add"), Name, Username, Password, UserGroupUID, RoleID, PersonID, StatusFlag)
    End Function

    Public Function User_Update(UserID As Integer, Name As String, ByVal Username As String, ByVal Password As String, ByVal RoleID As Integer, ByVal PersonID As Integer, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_Update"), UserID, Name, Username, Password, RoleID, PersonID, StatusFlag)
    End Function

    Public Function UserQA_Update(ByVal UserID As Integer, ByVal MED As String, ByVal TMED As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserQA_Update"), UserID, MED, TMED)
    End Function
    Public Function UserQA_Delete(ByVal UserID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserQA_Delete"), UserID)
    End Function

    Public Function UserQA_GetRole(ByVal UserID As Integer) As Integer
        Return SqlHelper.ExecuteScalar(ConnectionString, ("UserQA_GetRole"), UserID)
    End Function

    Public Function UserQA_GetByUserID(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("UserQA_GetByUserID"), UserID)
        Return ds.Tables(0)
    End Function


    Public Function User_UpdateProfileID(ByVal Username As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_UpdateProfileID"), Username)
    End Function

    Public Function UserRole_UpdateStatus(Username As String, RoleID As Integer, bActive As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserRole_UpdateStatus"), Username, RoleID, bActive, UpdBy)
    End Function

    Public Function User_ChangePassword(ByVal UserID As Integer, ByVal Password As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_ChangePassword"), UserID, Password)
    End Function

    Public Function User_GenLogfile(ByVal UserID As Integer, ByVal ActionType As String, Description As String, Line1 As String, Line2 As String, Remark As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_genLogfile"), UserID, ActionType, Description, Line1, Line2, Remark)
    End Function

    Public Function User_AddLocationProject(ByVal ProjectID As Integer, ByVal LocationID As String, StatusFlag As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_AddLocationProject"), ProjectID, LocationID, StatusFlag)
    End Function

    Public Function User_UpdateMail(ByVal Username As String, email As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("User_UpdateEmail"), Username, email)
    End Function

    Public Function Users_Online() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Users_Online"))
        Return ds.Tables(0)
    End Function

End Class
Public Class UserRoleController
    Inherits ApplicationBaseClass
    Public ds As New DataSet

    Public Function UserRole_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("UserRole_Get"))
        Return ds.Tables(0)
    End Function

    Public Function UserRole_GetActiveRoleByUID(ByVal pUserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("UserRole_GetActiveRoleByUID"), pUserID)
        Return ds.Tables(0)
    End Function


    Public Function UserAction_CheckByUser(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("UserAction_CheckByUser"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function UserAction_Checked(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("UserAction_Checked"), pUser, pLocation)

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

    Public Function UserRole_Add(ByVal Username As String, ByVal RoleID As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserRole_Add"), Username, RoleID, UpdBy)
    End Function


    Public Function UserAction_Add(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserAction_Add"), Username, LocationID, RoleAction, UpdBy)
    End Function
    Public Function UserAction_Update(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("UserAction_Update"), Username, LocationID, RoleAction, UpdBy)
    End Function


End Class
