Imports Microsoft.ApplicationBlocks.Data

Public Class TimePhaseController

    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Dim objLginfo As New TimePhaseInfo

    Public Function TimePhase_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_Get")
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_GetSubject(Year As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetSubject", Year)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_ByID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByID", id)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_GetIDByPhaseID(Year As Integer, SubjectCode As String, PhaseID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetIDByPhaseID", Year, SubjectCode, PhaseID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function
    Public Function TimePhase_GetByCourse(CourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByCourse", CourseID)
        Return ds.Tables(0)
    End Function
    Public Function Phase_GetByYear(iYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Phase_GetByYear", iYear)
        Return ds.Tables(0)
    End Function
    Public Function Phase_GetByYear(iYear As Integer, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Phase_GetByYearSubject", iYear, SubjectCode)
        Return ds.Tables(0)
    End Function

    Public Function TimePhase_GetByYear(iYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByYear", iYear)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_GetBySubjectYear(iYear As Integer, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetBySubjectYear", iYear, SubjectCode)
        Return ds.Tables(0)
    End Function

    Public Function TimePhase_GetBySubject(iYear As Integer, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetBySubject", iYear, SubjectCode)
        Return ds.Tables(0)
    End Function

    Public Function TimePhase_GetBySearch(iYear As Integer, CourseID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetBySearch", iYear, CourseID)
        Return ds.Tables(0)
    End Function
    Public Function TimePhase_GetByLocation(Year As Integer, LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByLocationYear", Year, LocationID)
        Return ds.Tables(0)
    End Function
    'Public Function TimePhase_GetByUser(Year As Integer, UserID As Integer) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByUser", Year, UserID)
    '    Return ds.Tables(0)
    'End Function

    Public Function Phase_GetByLocation(Year As Integer, LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Phase_GetByLocation", Year, LocationID)
        Return ds.Tables(0)
    End Function

    Public Function TimePhase_GetByLocation(Year As Integer, SubjectCode As String, LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TimePhase_GetByLocation", Year, SubjectCode, LocationID)
        Return ds.Tables(0)
    End Function


    Public Function TimePhase_Get4ReqByYear(iYear As Integer, SubjectCode As String) As DataTable
        SQL = "select itemID,Code,Name,Descriptions From " & objLginfo.strTableName & " where ispublic=1 and  EduYear=" & iYear

        If SubjectCode <> "0" Then
            SQL &= " And  SubjectCode='" & SubjectCode & "'"
        End If
        SQL &= " group by itemID,Code,Name,Descriptions  Order by Code"

        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function


    Public Function TimePhase_Save(pYear As Integer, CourseID As Integer, ByVal PhaseID As Integer, ByVal isSelect As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TimePhase_Save"), pYear, CourseID, PhaseID, isSelect)
    End Function

    Public Function TimePhase_Update(ByVal pID As Integer, ByVal pCode As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer, pYear As Integer, CourseID As Integer, DayCount As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "TimePhase_Update", pID, pCode, pName, desc, pStatus, pYear, CourseID, DayCount)
    End Function

    Public Function TimePhase_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from " & objLginfo.strTableName & " where itemID =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
    Public Function TimePhase_DeleteByYearCourse(ByVal pYear As Integer, pCourseID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "TimePhase_DeleteByYearCourse", pYear, pCourseID)
    End Function


#Region "TrunPhase"

    Public Function TurnPhase_Get() As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "TurnPhase_Get")
        Return ds.Tables(0)
    End Function
    Public Function TurnPhase_ByID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TurnPhase_GetByID", id)
        Return ds.Tables(0)
    End Function
    Public Function TurnPhase_ByPhaseID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TurnPhase_GetByPhaseID", id)
        Return ds.Tables(0)
    End Function
    Public Function TurnPhase_GetByYear(iYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "TurnPhase_GetByYear", iYear)
        Return ds.Tables(0)
    End Function

    Public Function TurnPhase_Add(ByVal EduYear As Integer, ByVal PhaseNo As Integer, ByVal Name As String, ByVal Descriptions As String, ByVal StartDate As String, ByVal EndDate As String, DayCount As Integer, ByVal StatusFlag As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TurnPhase_Add"), EduYear, PhaseNo, Name, Descriptions, StartDate, EndDate, DayCount, StatusFlag)
    End Function

    Public Function TurnPhase_Update(ByVal PhaseID As Integer, ByVal EduYear As Integer, ByVal PhaseNo As Integer, ByVal Name As String, ByVal Descriptions As String, ByVal StartDate As String, ByVal EndDate As String, DayCount As Integer, ByVal StatusFlag As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "TurnPhase_Update", PhaseID, EduYear, PhaseNo, Name, Descriptions, StartDate, EndDate, DayCount, StatusFlag)
    End Function

    Public Function TurnPhase_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "TurnPhase_Delete", pID)
    End Function

#End Region

End Class
