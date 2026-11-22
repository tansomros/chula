Imports Microsoft.ApplicationBlocks.Data

Public Class TrainingController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Training_GetByPersonID(PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Training_GetByPersonID", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function Training_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Training_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function Training_Save(ByVal UID As Integer, ByVal RYear As Integer, ByVal TRNCATEUID As String, TRNGRPUID As String, TRNTYPEUID As String, PersonID As String, TopicName As String, Tag As String, Remark As String, ByVal StatusFlag As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Training_Save"), UID, RYear, TRNCATEUID, TRNGRPUID, TRNTYPEUID, PersonID, TopicName, Tag, Remark, StatusFlag)
    End Function

    Public Function Training_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Training_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function Training_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from UserTraining where roleid =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function


    Public Function UserRole_chkHasRole(ByVal pUser As String, roleID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserRole_chkHasRole"), pUser, roleID)
        Return ds.Tables(0)
    End Function
End Class
