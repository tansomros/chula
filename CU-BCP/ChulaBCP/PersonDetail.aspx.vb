Imports System.IO
Public Class PersonDetail
    Inherits System.Web.UI.Page


    Dim dt As New DataTable
    Dim ctlP As New PersonController

    Dim ctlR As New ResearchController
    Dim ctlT As New TrainingController

    Dim dtR As New DataTable
    Dim dtT As New DataTable



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session("UserID") Is Nothing Then
            cmdPrint.Visible = False
            lblMobileLabel.Visible = False
            lblMobile.Visible = False
        Else
            cmdPrint.Visible = True
            lblMobileLabel.Visible = True
            lblMobile.Visible = True
        End If
        If Not Page.IsPostBack Then
            GetData(String.Concat(Request("pid")))
            Session("CVID") = String.Concat(Request("pid"))
        End If
    End Sub

    Private Sub GetData(PersonID As String)
        dt = ctlP.Person_GetByID(StrNull2Zero(PersonID))

        If dt.Rows.Count > 0 Then
            lblName.Text = String.Concat(dt.Rows(0).Item("FullNameTH"))
            lblPersonName.Text = String.Concat(dt.Rows(0).Item("FullNameTH"))
            'hPersonType.Value = string.concat(dt.Rows(0).Item("PersonType"))
            lblPosition.Text = String.Concat(dt.Rows(0).Item("PositionNameTH"))

            lblEmpCode.Text = String.Concat(dt.Rows(0).Item("EmployeeCode"))

            lblPositionEdu.Text = String.Concat(dt.Rows(0).Item("EduPositionNameTH"))

            lblEducate.Text = String.Concat(dt.Rows(0).Item("EducateName"))
            lblTel.Text = String.Concat(dt.Rows(0).Item("Contact"))
            lblMail.Text = String.Concat(dt.Rows(0).Item("Email"))
            lblMobile.Text = String.Concat(dt.Rows(0).Item("Tel"))

            If DBNull2Str(dt.Rows(0).Item("ImagePath")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")))

                If objfile.Exists Then
                    imgPerson.ImageUrl = "~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")
                Else
                    imgPerson.ImageUrl = "~/" & PersonPic & "/nopic.jpg"
                End If
            End If

            If DBNull2Str(dt.Rows(0).Item("PersonType")) = "1" Then
                lblPosition_Edu_label.Visible = True
                lblPositionEdu.Visible = True
                'lblPosition.Text = String.Concat(dt.Rows(0).Item("EduPositionName"))
                lblMajor.Text = String.Concat(dt.Rows(0).Item("MajorName"))
                lblMajor_Label.Text = "สาขาวิชา"
            Else
                lblPosition_Edu_label.Visible = False
                lblPositionEdu.Visible = False
                'lblPosition.Text = String.Concat(dt.Rows(0).Item("WorkPositionName"))
                lblMajor.Text = String.Concat(dt.Rows(0).Item("MajorName"))
                lblMajor_Label.Text = "ส่วนงาน"
            End If

            LoadEducation(PersonID)
            LoadWork(PersonID)
            LoadResearch(PersonID)
            LoadTraining(PersonID)

        End If
        dt = Nothing
    End Sub
    Private Sub LoadEducation(PersonID As Integer)
        dt = ctlP.PersonEducation_GetView(PersonID)
        If dt.Rows.Count > 0 Then
            grdEdu.DataSource = dt
            grdEdu.DataBind()
        End If
    End Sub

    Private Sub LoadResearch(PersonID As Integer)
        dtR = ctlR.Research_GetByPersonID(PersonID)
        grdResearch.DataSource = dtR
        grdResearch.DataBind()
    End Sub

    Private Sub LoadTraining(PersonID As Integer)
        dtT = ctlT.Training_GetByPersonID(PersonID)
        grdTrain.DataSource = dtT
        grdTrain.DataBind()
    End Sub

    Private Sub LoadWork(PersonID As Integer)
        dt = ctlP.PersonWork_GetView(PersonID)
        If dt.Rows.Count > 0 Then

            lblAdvisorSubject.Text = dt.Rows(0)("Course_Main")
            lblCoordinatorSubject.Text = dt.Rows(0)("Course_Coordinator")
            If lblAdvisorSubject.Text = "" Then
                lblAdvisorSubject.Text = "ไม่มี"
            End If

            If lblCoordinatorSubject.Text = "" Then
                lblCoordinatorSubject.Text = "ไม่มี"
            End If

            lblAward.Text = DisplayNoneTH(dt.Rows(0)("Award"))
            'lblresearch_pub.Text = DisplayNoneTH(dt.Rows(0)("Professional_Research_Pub"))
            'lblresearch_present.Text = DisplayNoneTH(dt.Rows(0)("Professional_Research_Present"))
            'lblMedical_Pub.Text = DisplayNoneTH(dt.Rows(0)("Medical_Pub"))
            'lblMedical_Present.Text = DisplayNoneTH(dt.Rows(0)("Medical_Present"))

            'lblTrain_pro_in.Text = DisplayNoneTH(dt.Rows(0)("Professional_Training_In"))
            'lblTrain_pro_out.Text = DisplayNoneTH(dt.Rows(0)("Professional_Training_Out"))
            'lblTrain_medical_in.Text = DisplayNoneTH(dt.Rows(0)("Medical_Training_In1") + dt.Rows(0)("Medical_Training_In2") + dt.Rows(0)("Medical_Training_In3") + dt.Rows(0)("Medical_Training_In4"))

            'lblTrain_medical_out.Text = DisplayNoneTH(dt.Rows(0)("Medical_Training_Out1") + dt.Rows(0)("Medical_Training_Out2") + dt.Rows(0)("Medical_Training_Out3") + dt.Rows(0)("Medical_Training_Out4"))

            'Dim sTrainQA As String = ""
            'Dim sQA() As String = Split(DBNull2Str(dt.Rows(0)("QA_Training")), "|")
            'For i = 0 To sQA.Length - 1
            '    Select Case sQA(i)
            '        Case "QA1"
            '            sTrainQA = sTrainQA + "- การบริหาร MSE <br/>"
            '        Case "QA2"
            '            sTrainQA = sTrainQA + "- การบริหารการศึกษา EC <br/>"
            '        Case "QA3"
            '            sTrainQA = sTrainQA + "- การบริหารงานคุณภาพ EdPEx <br/>"
            '        Case "QA4"
            '            sTrainQA = sTrainQA + "- การบริหารงานคุณภาพ QA <br/>"
            '        Case "QA5"
            '            sTrainQA = sTrainQA + "- การบริหารงานคุณภาพWFM <br/>"
            '    End Select
            'Next

            'If sTrainQA = "" And dt.Rows(0)("QA_Training_Other") = "" Then
            '    lblTrain_QA.Text = "ไม่มี"
            'Else
            '    lblTrain_QA.Text = sTrainQA + "- " + dt.Rows(0)("QA_Training_Other")
            'End If
        Else
            lblAdvisorSubject.Text = "ไม่มี"
            lblCoordinatorSubject.Text = "ไม่มี"
            lblAward.Text = "ไม่มี"
            'lblresearch_pub.Text = "ไม่มี"
            'lblresearch_present.Text = "ไม่มี"
            'lblMedical_Pub.Text = "ไม่มี"
            'lblMedical_Present.Text = "ไม่มี"
            'lblTrain_pro_in.Text = "ไม่มี"
            'lblTrain_pro_out.Text = "ไม่มี"
            'lblTrain_medical_in.Text = "ไม่มี"
            'lblTrain_medical_out.Text = "ไม่มี"
            'lblTrain_QA.Text = "ไม่มี"
        End If

    End Sub

End Class