Imports Microsoft.ApplicationBlocks.Data
Public Class PaymentController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function PaymentConfig_GetAmount(pProvID As String, pEffDate As Long, ProjID As Integer) As Double
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payment_GetAmount"), pProvID, pEffDate, ProjID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Dbl(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function PaymentConfig_Save(UID As Integer, ByVal SubjectCode As String _
    , ByVal PaymentType As Integer, PayAmount As Double _
    , ByVal EffectiveTo As String _
    , ByVal StatusFlag As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PaymentConfig_Save", UID, SubjectCode, PaymentType, PayAmount, EffectiveTo, StatusFlag)

    End Function

    Public Function PaymentConfig_Delete(ByVal PaymentID As Integer) As Boolean
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PaymentConfig_Delete", PaymentID)
    End Function
    Public Function PaymentConfig_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PaymentConfig_Get"))
        Return ds.Tables(0)
    End Function
    Public Function PaymentConfig_GetBySearch(PID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PaymentConfig_GetBySearch"), PID)
        Return ds.Tables(0)
    End Function

    Public Function PaymentConfig_GetByUID(PID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PaymentConfig_GetByUID"), PID)
        Return ds.Tables(0)
    End Function

End Class
