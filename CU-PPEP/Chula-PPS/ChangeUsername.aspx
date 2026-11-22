<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ChangeUsername.aspx.vb" Inherits=".ChangeUsername" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">
    <link href="css/pagestyles.css" rel="stylesheet" type="text/css" />
    
<script language="javascript">
        function NotAllowThai() {
            var keyCode = event.keyCode;
            if (keyCode >= 161 && keyCode <= 240) {
                event.returnValue = false;
            } else {
                event.returnValue = true;
            }
        }
    
</script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
      <section class="content-header">
      <h1>เปลี่ยน Username</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-edit"></i>

              <h3 class="box-title">แก้ไข Username</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              
                                            <table width="100%" border="0" cellpadding="1" cellspacing="1">                                      
                                          <tr>
                                            <td align="left" width="150" >Username เดิม :</td>
                                            <td align="left" class="Normal"><asp:TextBox ID="txtUsername" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox>
                                              </td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Username ใหม่ :</td>
                                            <td align="left" class="Normal"><span >
                                              <asp:TextBox ID="txtUsername1" runat="server" CssClass="text" Width="200px" MaxLength="15"></asp:TextBox>
                                            </span></td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >ยืนยัน Username ใหม่ :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtUsername2" runat="server" 
                                                      Width="200px" MaxLength="15"></asp:TextBox></td>
                                          </tr>
                                          <tr>
                                            <td colspan="2" align="left" ><asp:Label ID="lblvalidate" runat="server" CssClass="validateAlert"                                               Visible="False" Width="90%"></asp:Label>
                                              </td>
                                          </tr>
                                        </table></td>
                                     
                                          
                                	
</div>
            <div class="box-footer">
           <asp:Button ID="cmdSave" runat="server" Width="100" CssClass="btn btn-pink" Text="บันทึก"></asp:Button>
           <asp:Button ID="cmdClear" runat="server"  Width="100" CssClass="btn btn-secondary" Text="ยกเลิก"></asp:Button>
            </div>
          </div> 

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>

              <h3 class="box-title">ค้นหา User</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
  <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>

              <td>
                  <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
              <td>
                  <asp:Button ID="cmdFind" runat="server" Text="ค้นหา" CssClass="btn btn-warning" Width="70"/>                </td>
              
                  <td  class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก username , ชื่อ</td>
              </tr>
          </table> 

                                <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                        <asp:BoundField DataField="Username" HeaderText="Username" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ชื่อ" DataField="DisplayName" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProfileName" HeaderText="กลุ่ม" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"   CssClass="gridbutton"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Username") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>          
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
     
             
                                    
                                   		 
           
                         
</asp:Content>
