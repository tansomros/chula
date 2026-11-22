<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentStudent.aspx.vb" Inherits=".AssessmentStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>ให้คะแนนประเมิน</h1>   
    </section>

<section class="content">  
    <div class="box box-primary">

    <div class="box-header">
        <i class="fa fa-filter"></i>
        <h3 class="box-title">ข้อมูลการประเมิน</h3>

        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove">
                <i class="fa fa-times"></i>
            </button>
        </div>
    </div>

    <div class="box-body">

        <div class="row">

            <!-- รูป -->
            <div class="col-md-2 col-sm-3 text-center">
                <asp:Image ID="picStudent" runat="server"
                    ImageUrl="images/unpic.jpg"
                    CssClass="img-responsive"
                    Style="max-width:120px;" />
            </div>

            <!-- ข้อมูล -->
            <div class="col-md-10 col-sm-9">

                <div class="row">

                    <div class="col-md-2 text-bold">
                        รหัสนิสิต(Student ID)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblStdCode" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-2 text-bold">
                        ชื่อ - นามสกุล(Name)
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row" style="margin-top:5px;">

                    <div class="col-md-2 text-bold">
                        สาขา(Major)
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblMajorName" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-2 text-bold">
                        ชื่อเล่น(Nickname)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNickName" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row" style="margin-top:5px;">

                    <div class="col-md-2 text-bold">
                        ปีการศึกษา(Year)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                        <asp:Label ID="lblYearEn" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2 text-bold">
                        รายวิชา(Course)
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblSubjectCode" runat="server"></asp:Label>
                        <asp:Label ID="lblSubjectName" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-1 text-bold">
                        ผลัดที่(Batch)	
                    </div>
                    <div class="col-md-1">
                        <asp:HiddenField ID="hdPhaseID" runat="server" />
                        <asp:Label ID="lblPhaseNo" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row" style="margin-top:5px;">

                    <div class="col-md-2 text-bold">
                        แหล่งฝึก(Site)	
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                        (<asp:Label ID="lblLocationID" runat="server"></asp:Label>)
                    </div>

                    <div class="col-md-2 text-bold">
                        ตำแหน่ง(Role)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblAssessorRole" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row" style="margin-top:10px;">

                    <div class="col-md-2 text-bold">
                        ผู้ประเมิน(Evaluator)	
                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="lblAssessorName" runat="server"></asp:Label>
                    </div>

                </div>

            </div>

        </div>

    </div>

    <div class="box-footer clearfix"></div>

</div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-check"></i>

              <h3 class="box-title"><asp:Label ID="lblEvaGroup" runat="server" Font-Bold="True"></asp:Label> </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
                  
            </div>
            <div class="box-body"> 
   <table width="100%" border="0" cellspacing="0" cellpadding="0" >
           <tr>
          <td align="left" valign="top">
              <asp:Label ID="lblRemark" runat="server"></asp:Label>
              </td>
                   </tr>
               <tr>
          <td align="right" valign="top">
              เลือก <b>N/A</b> หากไม่ได้ฝึก/ไม่ได้ประเมินข้อนั้นๆ (ระบบจะไม่นำคะแนนมารวม) <br />  Select N/A if the item was not practiced or not evaluated (the system will exclude it from the total score).
              </td>
                   </tr>
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="EvaluationTopicUID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="หัวข้อการประเมิน (Topic)">
                    <ItemTemplate>
                        <asp:Label ID="lblTopicName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TopicName") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblRemark" runat="server" CssClass="text9_blue" Text='<%# DataBinder.Eval(Container.DataItem, "Remark") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Round 1">
                <ItemTemplate>
                    <asp:TextBox ID="txtScore1" runat="server" CssClass="txtscore" Width="45px" Text='<%# DataBinder.Eval(Container.DataItem, "Score1") %>' AutoPostBack="True" OnTextChanged="txtScore1_TextChanged" ></asp:TextBox>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Round 2">
                <ItemTemplate>
                    <asp:TextBox ID="txtScore2" runat="server" CssClass="txtscore" Width="45px" Text='<%# DataBinder.Eval(Container.DataItem, "Score2") %>' AutoPostBack="True" OnTextChanged="txtScore2_TextChanged"></asp:TextBox>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />          
            </asp:TemplateField>
                <asp:TemplateField HeaderText="N/A">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkNot" runat="server" CssClass="mailbox-messages" Checked='<%# ConvertYN2Boolean(String.Concat(DataBinder.Eval(Container.DataItem, "isExclude"))) %>' AutoPostBack="True" OnCheckedChanged="chkNot_CheckedChanged"  />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th"  Font-Bold="false" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
       
        <tr>
          <td align="center" valign="top">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                        <table align="right">
                  <tr>
                      <td align="right">คะแนนรวม (Total)</td>
                      <td class="text-bold" width="165" align="center">
                                                        <asp:Label ID="lblScore1" runat="server"></asp:Label>
                                                    </td>
                      <td class="text-bold" width="85" align="left">
                                                        <asp:Label ID="lblScore2" runat="server"></asp:Label>
                                                    </td>
                  </tr>
                  <tr>
                      <td align="right">รวมคะแนน (คะแนนที่ได้ x น้ำหนัก)/ฐานคะแนนที่มีการประเมินจริง <br /> Total Score = (Score × Weight) / Maximum Assessed Score</td>
                      <td class="text-bold" width="165" align="center">
                                                        <asp:Label ID="lblTotalScore1" runat="server"></asp:Label>
                                                    </td>
                      <td class="text-bold"  width="85" align="left">
                                                        <asp:Label ID="lblTotalScore2" runat="server"></asp:Label>
                                                    </td>
                 </tr>
              </table>
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="grdData" EventName="SelectedIndexChanged">
                      </asp:AsyncPostBackTrigger>
                  </Triggers>
              </asp:UpdatePanel> 
              
             
            </td>
      </tr>
                 <tr>
<td> &nbsp; </td>
       </tr>
                <tr>
      <td>

<div class="text-bold">โปรดระบุภาพรวมความรู้ ความเข้าใจ ทักษะ กิจกรรม หรือชิ้นงานหลักอื่น ๆ ในการฝึกปฏิบัติงาน <br /> 
Please describe the student's overall knowledge, understanding, skills, activities, or key outputs during the internship.

</div> 
 <asp:TextBox ID="txtComments" runat="server" Height="100px" TextMode="MultiLine" Width="100%"></asp:TextBox>
          </td>
             </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelComment" runat="server">
                                        <ContentTemplate>
                                              <asp:Panel ID="pnComment" runat="server">
                                  <div class="alert alert-danger p-2"> เนื่องจากท่านประเมินให้นิสิตต่ำกว่าเกณฑ์ที่กำหนด ท่านต้องระบุเหตุผลก่อน แล้วบันทึกอีกครั้ง <br />
                                  Evaluation is below the required standard. Enter a reason before saving.</div> 
                                    <asp:TextBox ID="txtReason" runat="server" Height="100px" TextMode="MultiLine" Width="100%" BackColor="#FFFFDD"></asp:TextBox>
                               </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCal" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    </td>
                            </tr>
        
                            <tr>
                                <td class="text-center">
                                    <asp:Button ID="cmdCal" runat="server" CssClass="btn btn-success" Text="ปรับปรุงคะแนน" />
                                    <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" Width="120px" />                                    
                                </td>
                            </tr>
        <tr>
          <td align="center" valign="top">

              <asp:Button ID="cmdHome" runat="server" Text="หน้ารายชื่อนิสิต" CssClass="btn btn-warning" />
      <asp:Button ID="cmdBack" runat="server" CssClass="btn btn-secondary" Text="&lt;&lt; ย้อนกลับ" Width="100px" />
            &nbsp;</td>
      </tr>
       
    </table>
                            
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
 
 </section>   
</asp:Content>
