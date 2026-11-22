<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentAdmin.aspx.vb" Inherits=".AssessmentAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>แก้ไขคะแนนประเมิน (สำหรับ Admin)</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">ข้อมูลนิสิต/การประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table width="100%" border="0" cellPadding="1" cellSpacing="1" >
<tr>
                                                    <td width="80" class="texttopic">
                                                        รหัสนิสิต                                                        </td>
                                                    <td>
                                                        <asp:Label ID="lblStdCode" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="100" class="texttopic">ชื่อ - นามสกุล</td>
                                                    <td>
                                                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="texttopic">ชื่อเล่น </td>
                                                    <td>
                                                        <asp:Label ID="lblNickName" runat="server"></asp:Label>
                                                    </td>
</tr>
<tr>
  <td class="texttopic">สาขา</td>
  <td colspan="3"><asp:Label ID="lblMajorName" runat="server"></asp:Label>
                                                      </td>
     <td class="texttopic">แหล่งฝึก </td>
                                                    <td>
                                                        
                                                        <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                                                        
                                                    &nbsp;(<asp:Label ID="lblLocationID" runat="server"></asp:Label>
                                                        
                                                        )</td>
</tr>
<tr>
  <td class="texttopic">ปีการศึกษา</td>
  <td>
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                    </td>
  <td class="texttopic">รายวิชา</td>
  <td  ><asp:Label ID="lblSubjectCode" runat="server"></asp:Label>
                                                    &nbsp;
                                                        <asp:Label ID="lblSubjectName" runat="server"></asp:Label>
                                                    </td>
      <td class="texttopic">ผลัดที่ </td>
                                                    <td>
                                                        
                                                        <asp:Label ID="lblPhaseNo" runat="server"></asp:Label>
                                                        
                                                    </td>
</tr>
<tr>
  <td class="texttopic">ผู้ประเมิน</td>
  <td colspan="3">
                                                        <asp:Label ID="lblAssessorName" runat="server"></asp:Label>
                                                    </td>
      <td width="60" class="texttopic">ตำแหน่ง</td>
                                                    <td>
                                                        
                                                        <asp:Label ID="lblAssessorRole" runat="server"></asp:Label>
                                                        
                                                    </td>
</tr>
</table>
                            
</div>
            <div class="box-footer clearfix">
           
            </div>
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
          <td align="right" valign="top">
              เลือก <b>N/A</b> หากไม่ได้ฝึก/ไม่ได้ประเมินข้อนั้นๆ (ระบบจะไม่นำคะแนนมารวม)
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
                <asp:TemplateField HeaderText="หัวข้อการประเมิน">
                    <ItemTemplate>
                        <asp:Label ID="lblTopicName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TopicName") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblRemark" runat="server" CssClass="text9_blue" Text='<%# DataBinder.Eval(Container.DataItem, "Remark") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="ครั้งที่ 1">
                <ItemTemplate>
                    <asp:TextBox ID="txtScore1" runat="server" CssClass="txtscore" Width="45px" Text='<%# DataBinder.Eval(Container.DataItem, "Score1") %>' AutoPostBack="True" OnTextChanged="txtScore1_TextChanged" ></asp:TextBox>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ครั้งที่ 2">
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
                      <td align="right">คะแนนรวม</td>
                      <td class="texttopic" width="90" align="center">
                                                        <asp:Label ID="lblScore1" runat="server"></asp:Label>
                                                    </td>
                      <td class="texttopic" width="90" align="center">
                                                        <asp:Label ID="lblScore2" runat="server"></asp:Label>
                                                    </td>
                  </tr>
                  <tr>
                      <td align="right">รวมคะแนน (คะแนนที่ได้ x น้ำหนัก)/ฐานคะแนนที่มีการประเมินจริง</td>
                      <td class="texttopic" align="center">
                                                        <asp:Label ID="lblTotalScore1" runat="server"></asp:Label>
                                                    </td>
                      <td class="texttopic" align="center">
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
      <td>
<div class="text-bold">ความคิดเห็นเพิ่มเติม</div> 
 <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="100%"></asp:TextBox>
          </td>
             </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelComment" runat="server">
                                        <ContentTemplate>
                                              <asp:Panel ID="pnComment" runat="server">
                                  <div class="validateAlert"> เนื่องจากท่านประเมินให้นิสิตต่ำกว่าเกณฑ์ที่กำหนด ท่านต้องระบุเหตุผลก่อน แล้วบันทึกอีกครั้ง</div> 
                                    <asp:TextBox ID="txtComment" runat="server" Height="100px" TextMode="MultiLine" Width="100%" BackColor="#FFFFDD"></asp:TextBox>
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
                                <td class="text-right">
                                    <asp:Button ID="cmdCal" runat="server" CssClass="buttonGreen" Text="ปรับปรุงคะแนน" />
                                    <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" Width="120px" />                                    
                                </td>
                            </tr>
        <tr>
          <td align="center" valign="top">
      <asp:Button ID="cmdBack" runat="server" CssClass="btn btn-warning" Text="&lt;&lt; ย้อนกลับ" Width="100px" />
            </td>
      </tr>
       
    </table>
                            
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
 
 </section>   
</asp:Content>
