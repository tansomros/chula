Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Globalization
Imports Microsoft.ApplicationBlocks.Data



Public Class ApplicationBaseClass : Inherits BaseClass

    Friend Connection As SqlConnection

    Public strTableName As String
    Public strKeyGen As String
    Public tblField() As stcField
    Public FieldSort As String


    Private sqlSrvDatabase As String = "IMWEB"
    Friend Transaction As SqlTransaction
    Public intTotalField As Integer = 0

    Public Shared sqlServer As String
    Public Shared sqlDatabase As String
    Public Shared sqlUsername As String
    Public Shared sqlPassword As String
    Public Shared sqlReport As String
    Public Shared ReportURL As String
    Public Shared ConnectionString As String
    Public Shared PassPhase As String

    Public Shared isAdmin As Boolean
    Public Shared isDrugShop As Boolean
    Public Shared isReporter As Boolean

#Region "Private Members"

    Private Const ProviderType As String = "thedev"
    Public Const ModuleQualifier As String = ""
    Public Const ObjectQualifier As String = ""
    Public Const DatabaseOwner As String = ""

    Public Const ModuleQualifierCPA As String = "CPA_"

#End Region
    Public Structure stcField
        Private _fldName As String
        Private _fldValue As String
        Private _fldType As String
        Private _fldAffect As Boolean
        Private _fldLength As Integer

        Property fldName() As String
            Get
                Return _fldName
            End Get
            Set(ByVal Value As String)
                _fldName = Value
            End Set
        End Property
        Property fldValue() As String
            Get
                Return _fldValue
            End Get
            Set(ByVal Value As String)
                _fldValue = Value
            End Set
        End Property
        Property fldType() As String
            Get
                Return _fldType
            End Get
            Set(ByVal Value As String)
                _fldType = Value
            End Set
        End Property
        Property fldAffect() As Boolean
            Get
                Return _fldAffect
            End Get
            Set(ByVal Value As Boolean)
                _fldAffect = Value
            End Set
        End Property
        Property fldLength() As Integer
            Get
                Return _fldLength
            End Get
            Set(ByVal Value As Integer)
                _fldLength = Value
            End Set
        End Property

        Public Sub New(ByVal fValue As String)
            _fldValue = fValue
        End Sub
    End Structure

    Public Shared Function GetFullyQualifiedName(ByVal name As String) As String
        Return DatabaseOwner & ObjectQualifier & ModuleQualifier & name
    End Function

    Public Shared Function GetFullyQualifiedNameCPA(ByVal name As String) As String
        Return DatabaseOwner & ObjectQualifier & ModuleQualifierCPA & name
    End Function

    Public Sub getConnectionString()

        sqlServer = Convert.ToString(ConfigurationSettings.AppSettings("ServerPath"))
        sqlDatabase = Convert.ToString(ConfigurationSettings.AppSettings("DatabaseName"))
        sqlUsername = Convert.ToString(ConfigurationSettings.AppSettings("Username"))
        sqlPassword = Convert.ToString(ConfigurationSettings.AppSettings("Password"))
        sqlReport = Convert.ToString(ConfigurationSettings.AppSettings("ReportPath"))
        ReportURL = Convert.ToString(ConfigurationSettings.AppSettings("ReportURL"))
        PassPhase = Convert.ToString(ConfigurationSettings.AppSettings("PassPhase"))

        ConnectionString = "Data Source=" & sqlServer & ";Database=" & sqlDatabase & ";User Id=" & sqlUsername & ";Password=" & sqlPassword & ";" 'Pooling=false;Connect Timeout=300;"

    End Sub


    Public Sub New(Optional ByVal conn As SqlClient.SqlConnection = Nothing)
        getConnectionString()

        If Not IsNothing(conn) Then
            Me.Conn = conn
        End If

    End Sub


    Public Property Conn() As SqlConnection
        Get
            Return Connection
        End Get
        Set(ByVal Value As SqlConnection)
            Connection = Value
        End Set
    End Property

    'Public Function OpenConnection(Optional ByVal constr As String = "", Optional ByVal f_ShowMsg As Boolean = True) As Boolean
    '    Dim ConString As String = ""
    '    If constr.Length = 0 Then
    '        ConString = "Data Source=" & sqlServer & ";Database=" & sqlDatabase & ";User Id=" & sqlUsername & ";Password=" & sqlPassword & ";"
    '    End If
    '    Try
    '        Connection = New SqlConnection
    '        If Connection.State = ConnectionState.Open Then Connection.Close()
    '        Connection = New SqlConnection(ConString)
    '        Connection.Open()
    '        Return True
    '    Catch ex As Exception
    '        If f_ShowMsg Then MsgBox(ex.Message, MsgBoxStyle.OkOnly)
    '        Return False
    '    End Try
    'End Function

    'Public Sub Cnn()
    '    With Conn
    '        If .State = ConnectionState.Open Then .Close()
    '        .ConnectionString = ConnectionString
    '        .Open()
    '    End With
    'End Sub

    Public Function GET_DATE_SERVER() As Date
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, "select getdate() as dateServer")
        Return ds.Tables(0).Rows(0).Item(0)
        ds = Nothing
    End Function
    Public Function GET_DATETIME_SERVER() As DateTime
         Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, "select getdate() as dateServer")
        Return ds.Tables(0).Rows(0).Item(0)
        ds = Nothing
    End Function

    'Public Function ExecuteNonQuery(ByVal strSQL As String) As Boolean
    '    Dim excCommand As SqlCommand
    '    Dim retValue As Boolean = False
    '    Try
    '        excCommand = New SqlCommand(strSQL, Connection)
    '        If Not IsNothing(Transaction) Then excCommand.Transaction = Transaction
    '        excCommand.ExecuteNonQuery()
    '        retValue = True
    '    Catch ex As Exception

    '        MsgBox(ex.Message & excCommand.CommandText, MsgBoxStyle.OkOnly, "Error")
    '        retValue = False
    '    End Try
    '    If Not excCommand Is Nothing Then
    '        excCommand.Dispose()
    '        excCommand = Nothing
    '    End If
    '    Return retValue
    'End Function


    'Public Function ExecuteSQL(ByVal strSQL As String) As Boolean
    '    Dim excCommand As SqlCommand
    '    Dim retValue As Boolean = False
    '    Try
    '        excCommand = New SqlCommand(strSQL, Connection)
    '        If Not IsNothing(Transaction) Then excCommand.Transaction = Transaction
    '        excCommand.ExecuteNonQuery()
    '        retValue = True
    '    Catch ex As Exception

    '        MsgBox(ex.Message & excCommand.CommandText, MsgBoxStyle.OKOnly, "Error")
    '        retValue = False
    '    End Try
    '    If Not excCommand Is Nothing Then
    '        excCommand.Dispose()
    '        excCommand = Nothing
    '    End If
    '    Return retValue
    'End Function

    'Public Function ExecuteQuery(ByVal strSQL As String) As DataTable
    '    Dim da As SqlDataAdapter
    '    Dim ds As DataSet
    '    Dim dt As DataTable
    '    Try
    '        da = New SqlDataAdapter(strSQL, Connection)
    '        ds = New DataSet
    '        If Not IsNothing(Transaction) Then da.SelectCommand.Transaction = Transaction
    '        da.Fill(ds, "dataname")
    '        dt = ds.Tables("dataname")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
    '        dt = New DataTable
    '    End Try
    '    If Not da Is Nothing Then
    '        da.Dispose()
    '        da = Nothing
    '    End If
    '    ds = Nothing
    '    Return dt
    'End Function


    'Public Function delData(Optional ByVal strWhere As String = "", Optional ByVal f_debug As Boolean = False) As Boolean
    '    Dim strSql As String
    '    strSql = "delete from " & strTableName
    '    If strWhere.Length > 0 Then strSql &= " where " & strWhere
    '    If f_debug Then showSQLString(strSql)
    '    delData = ExecuteSQL(strSql)
    'End Function

    'Public Sub showSQLString(ByVal strSql As String)
    '    If f_debug Then InputBox("sql command :", "DBClass debug", strSql)
    'End Sub

    'Public Function selData(Optional ByVal strSQL As String = "", Optional ByVal strWhere As String = "", Optional ByVal strOrder As String = "", Optional ByVal f_debug As Boolean = False) As DataTable
    '    If strSQL.Length = 0 Then strSQL = "select * from " & strTableName
    '    If strWhere.Length > 0 And strSQL.IndexOf("where") <= 0 Then strSQL &= " where " & strWhere
    '    If strOrder.Length > 0 And strSQL.IndexOf("order by") <= 0 Then strSQL &= " order by " & strOrder
    '    If f_debug Then showSQLString(strSQL)

    '    selData = ExecuteQuery(strSQL)
    'End Function

    'Public Function selTopData(Optional ByVal topVal As Integer = 0, Optional ByVal strSQL As String = "", Optional ByVal strWhere As String = "", Optional ByVal strOrder As String = "", Optional ByVal f_debug As Boolean = False) As DataTable
    '    If topVal > 0 Then
    '        If strSQL.Length > 0 Then
    '            strSQL = strSQL.Remove(strSQL.ToUpper.IndexOf("select".ToUpper), 6)
    '            strSQL = "select top " & topVal.ToString & strSQL
    '        Else
    '            strSQL = "select top " & topVal.ToString & " * from " & strTableName
    '        End If
    '    End If
    '    selTopData = selData(strSQL, strWhere, strOrder, f_debug)
    'End Function

    'Public Function countRecByStrSql(ByVal str_Sql As String, Optional ByVal f_open_connection As Boolean = True) As Integer
    '    Dim dt As New DataTable
    '    Dim int_return As Integer
    '    If f_open_connection Then
    '        If Me.OpenDatabase Then
    '            dt = Me.ExecuteQuery(str_Sql)
    '            Me.CloseDatabase()
    '        End If
    '    Else
    '        dt = Me.ExecuteQuery(str_Sql)
    '    End If
    '    int_return = dt.Rows.Count
    '    Return int_return
    'End Function

    Private Sub RunningNumber_AddNew(ByVal tCode As String, ProvID As String)
        SQL = "Insert Into Running(Code,ProvinceID,LastRunning)"
        SQL &= " Values('" & tCode
        SQL &= "','" & ProvID
        SQL &= "',0)"
        ExecuteDataQuery(SQL)
    End Sub

    Public Sub RunningNumber_Update(ByVal tCode As String, ProvID As String)
        SQL = "Update Running  set LastRunning=LastRunning+1  Where  Code='" & tCode
        SQL &= "' And ProvinceID='" & ProvID & "'"
        ExecuteDataQuery(SQL)
    End Sub
    Public Function RunningNumber_Get(ByVal Code As String, ProvID As String) As Integer
        Dim sqlRun As String
        Dim ds As New DataSet

        sqlRun = "select LastRunning from  Running  where Code='" & Code & "' And ProvinceID='" & ProvID & "'"

        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sqlRun)

        If ds.Tables(0).Rows.Count > 0 Then
            Return CLng(ds.Tables(0).Rows(0).Item(0)) + 1
        Else
            RunningNumber_AddNew(Code, ProvID)
            Return 1
        End If

        ds = Nothing

    End Function

    Public Function genRunningNumber(ByVal cTypeID As String, ByVal cProvID As String) As String

        Dim str As String
        Dim rn As Integer
        Dim i As Integer
        Dim strZero As String = ""
        rn = RunningNumber_Get(cTypeID, cProvID)
        If rn < 1000 Then
            str = CStr(rn)
            For i = 0 To 3 - Len(str)
                strZero = strZero & "0"
            Next
        End If

        genRunningNumber = strZero & rn

    End Function

    Public Function ExecuteDataTable(ByVal statement As String) As DataTable
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, statement)
        Return ds.Tables(0)
    End Function

    Public Function ExecuteDataQuery(ByVal statement As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, statement)

    End Function
    'Public Function ExecuteStoredProcedure(ByVal commandText As String, ByVal Parameters() As String) As Int32
    '    Dim dt As New DataTable
    '    Dim returnValue As Integer
    '    Dim i As Integer
    '    Dim objAdapter As New SqlDataAdapter
    '    Dim sqlCmd As SqlCommand

    '    Try

    '        Cnn()

    '        sqlCmd = New SqlCommand()
    '        sqlCmd.Connection = Conn
    '        sqlCmd.CommandText = commandText
    '        sqlCmd.CommandTimeout = 180
    '        sqlCmd.CommandType = CommandType.StoredProcedure
    '        objAdapter.SelectCommand = sqlCmd
    '        SqlCommandBuilder.DeriveParameters(objAdapter.SelectCommand)

    '        For i = 1 To Parameters.Length
    '            If (objAdapter.SelectCommand.Parameters(i).Direction = ParameterDirection.Input) Or (objAdapter.SelectCommand.Parameters(i).Direction = ParameterDirection.InputOutput) Then

    '                objAdapter.SelectCommand.Parameters(i).Value = Parameters(i - 1)
    '            End If
    '        Next


    '        sqlCmd.ExecuteNonQuery()
    '        returnValue = (sqlCmd.Parameters("@RETURN_VALUE").Value)


    '        Return returnValue

    '    Catch ex As SqlException

    '    Finally

    '    End Try

    '    Return 1

    'End Function


End Class

Public Class BaseClass
    Protected adapter As SqlDataAdapter
    Protected cmd As SqlCommand
    Protected trans As SqlTransaction
    Protected _Error As String

    Property Message() As String
        Get
            Return Me._Error
        End Get
        Set(ByVal Value As String)
            Me._Error = Value
        End Set
    End Property

End Class

