<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GradeModify.aspx.vb" Inherits=".GradeModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <section class="content-header">
      <h1>แก้ไขเกรด</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">               
<table width="100%">
<tr>
    <td class="texttopic">ปีการศึกษา</td><td>
       <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                            </td></tr>
<tr>
  <td class="texttopic">รายวิชา :</td>
  <td>
      <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True" Width="90%"></asp:DropDownList>
  </td> 
</tr>
<tr>
              <td width="100" >ค้นหา</td>
              <td>
                  <asp:TextBox ID="txtSearch" runat="server" Width="185px"></asp:TextBox>
                  &nbsp;<asp:Button ID="cmdSearch" runat="server" Text="ค้นหา" CssClass="btn btn-warning" Width="60" />&nbsp;<asp:Button ID="cmdSearchAll" runat="server" Text="ดูทั้งหมด" CssClass="btn btn-warning" />

              </td>
    </tr>
     
            </table>
                                      
</div>
            <div class="box-footer text-center">
             
            </div>
 </div>       
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="modal_loading">
                <div class="center_loading">
                    <img alt="" src="images/loading.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-circle-o"></i>

              <h3 class="box-title">รายการผลการตัดเกรด</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" PageSize="20" Width="100%" AllowPaging="True">
                            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                            <columns>
                                <asp:BoundField DataField="nRow" HeaderText="No.">
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต">
                                <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                                <asp:BoundField DataField="TotalScore" HeaderText="Total">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                 <asp:TemplateField HeaderText="แก้ไข">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"   CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>'  />
              </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
                            </columns>
                            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <headerstyle CssClass="th" Font-Bold="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCourse" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmdSearchAll" EventName="Click" />                       
                    </Triggers>
                </asp:UpdatePanel>
                                                 
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>



</section>
</asp:Content>
