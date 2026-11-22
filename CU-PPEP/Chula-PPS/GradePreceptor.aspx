<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GradePreceptor.aspx.vb" Inherits=".GradePreceptor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>กำหนดคะแนนตัดเกรด<u>สำหรับแหล่งฝึก</u> </h1>
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-gears"></i>

              <h3 class="box-title">กำหนดเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table cellSpacing="1" cellPadding="1" border="0">
<tr>    
    <td align="left" >ปีการศึกษา</td>
    <td align="left" colspan="3" ><asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" Width="100" AutoPostBack="True"></asp:DropDownList></td>
</tr>
   <tr>
                                                  <td class="texttopic">รายวิชา</td>
                                                  <td class="texttopic"  colspan="3">
                                                      <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
                                                 
                                                </tr>
<tr>    
    <td >เกรด</td>
    <td colspan="3" ><asp:TextBox ID="txtGrade" runat="server"  CssClass="text-center"  Width="60px"></asp:TextBox></td>
</tr>
<tr>    
    <td >คะแนนตั้งแต่</td>
    <td colspan="3" >
        <table>
            <tr>
                <td>
        <asp:TextBox ID="txtMin" runat="server"  CssClass="text-center"  Width="60px"></asp:TextBox>
                </td>
                <td>ถึง</td>
                <td>
        <asp:TextBox ID="txtMax" runat="server"  CssClass="text-center"   Width="60px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>

                                                <tr>
                                                    <td colspan="4" align="center" vAlign="top" >
                                                       
                                          <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
                                                                                          </td>
                                                </tr>
  </table>        
                     
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
  <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-sort-alpha-asc"></i>

              <h3 class="box-title">รายการเกรด</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">
      <table border="0" cellspacing="1" cellpadding="0">
            <tr>
                 <td align="left" >ปีการศึกษา</td>
    <td align="left" ><asp:DropDownList ID="ddlYearSearch" runat="server" Width="100" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList></td>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>               
                </td>
            </tr>
            </table>
              <asp:GridView ID="grdData"  CssClass="table table-hover" Width="50%"
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" AllowPaging="True" PageSize="8">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="ปีการศึกษา" DataField="EduYear">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />                      </asp:BoundField>
                <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา" />
            <asp:BoundField DataField="Grade" HeaderText="เกรด">
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="MinScore" HeaderText="Min" >
                <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="MaxScore" HeaderText="Max">
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
  </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    </section>
</asp:Content>
