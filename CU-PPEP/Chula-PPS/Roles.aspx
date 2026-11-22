<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Roles.aspx.vb" Inherits=".Roles" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    </asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<section class="content-header">
      <h1> สิทธิ์การใช้งาน</h1>   
    </section>

<section class="content">  

     
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข สิทธิ์การใช้งาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

  
<table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        รหัส : 
                                                        <asp:Label ID="lblID" runat="server"></asp:Label>                                                    </td>
                                                    <td align="left" class="texttopic"><asp:TextBox ID="txtCode" runat="server" 
                                                         Width="89px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ชื่อกลุ่มสิทธิ์ :</td>
                                                    <td align="left" class="texttopic"><asp:TextBox ID="txtName" runat="server" Width="300px" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">คำอธิบาย :</td>
                                                  <td align="left" class="texttopic">
                                                      <asp:TextBox ID="txtDesc" runat="server" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        Status :                                                        </td>
                                                    <td align="left" class="texttopic"><asp:CheckBox ID="chkStatus" runat="server" Text="Active" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">&nbsp;                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" class="texttopic">
                                                 
													   <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>                                            </td>
                                                </tr>
  </table>        
  
 
                                                   
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการสิทธิ์การใช้งาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">       
   
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="RoleID" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="RoleName" HeaderText="ชื่อกลุ่มสิทธิ์">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="Descriptions" HeaderText="คำอธิบาย">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem,"IsPublic") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"RoleID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"RoleID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
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
