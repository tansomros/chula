<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssesseeListAdmin.aspx.vb" Inherits=".AssesseeListAdmin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>แก้ไข/ตรวจสอบ คะแนนประเมิน (สำหรับ Admin)</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไขค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">
              
<table border="0" align="left" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา</td>
                                                    <td align="left">
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                    </td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา</td>
  <td align="left">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" class="texttopic">แหล่งฝึก</td>
  <td align="left">
                                                      <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" class="texttopic">ค้นหานิสิต</td>
  <td align="left">
                  <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>                                                    &nbsp;<span class="text10_blue">(ค้นหาจากรหัสนิสิต ,ชื่อ หรือ นามสกุล)</span></td>
</tr>
<tr>
  <td align="left" class="texttopic">&nbsp;</td>
  <td align="left">
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="100px" />
    </td>
</tr>
  </table>  


          </td>
      </tr>
        
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="AssessorUID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
                <asp:BoundField DataField="AliasName" HeaderText="แบบประเมิน" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต">
                <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา" />
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดที่">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AssessorName" HeaderText="ผู้ประเมิน">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AssessorRoleID" HeaderText="สำหรับ" >
                <ItemStyle Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="EvaluationGroupUID" HeaderText="EGUID" />
            <asp:TemplateField HeaderText="แก้ไข">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"  CommandArgument='<%# Container.DataItemIndex %>'  />
              </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
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
       
    </table>
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

    </section>
</asp:Content>
