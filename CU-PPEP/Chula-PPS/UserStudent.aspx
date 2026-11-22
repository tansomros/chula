<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserStudent.aspx.vb" Inherits=".UserStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">
    <link href="css/pagestyles.css" rel="stylesheet" type="text/css" />   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <section class="content-header">
      <h1>จัดการข้อมูล User สำหรับนิสิต</h1>     
    </section>
<section class="content">

     <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-edit"></i>

              <h3 class="box-title">สร้างข้อมูลผู้ใช้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">     
      <table width="98%" border="0" align="left" cellpadding="0" cellspacing="2">
                                      <tr>
                                        <td class="text-blue">
                                         พบนิสิตที่ยังไม่ได้สร้างรหัสผู้ใช้งานให้
                         <asp:Label ID="lblNoUserCount" runat="server" CssClass="texttopic"></asp:Label>
&nbsp;คน

                                        </td>
                                      </tr>
                                      <tr>
                                        <td>
                                            <span >
                         <asp:Button ID="cmdGenUser" runat="server" CssClass="btn btn-warning" Text="สร้าง User ให้นิสิตที่เหลือ" />
                                        </span>

                                        </td>
                                      </tr>
                                      <tr>
                                        <td  valign="top" class="block_step2" >
                                            <table width="100%" border="0" cellpadding="1" cellspacing="1">
                                          <tr>
                                            <td colspan="2" class="Topic_header">บันทึกข้อมูลผู้ใช้</td>
                                            <td class="Topic_header">&nbsp;</td>
                                            <td class="Topic_header">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td width="120" align="left" >UserID : </td>
                                            <td align="left" class="Normal"><asp:Label ID="lblID" runat="server"></asp:Label>                                            </td>
                                            <td align="left" class="Normal">&nbsp;</td>
                                            <td align="left" class="Normal">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Username :</td>
                                            <td align="left" class="Normal">
                                                <table>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtUsername" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                              <asp:Image 
                                                      ID="imgAlert" runat="server" Height="16px" ImageUrl="images/alert_icon.png" 
                                                      Width="16px" />                                            </td>
                                                    </tr>
                                                </table>
                                         
                                              </td>
                                            <td align="left" class="Normal">Password :</td>
                                            <td align="left" class="Normal"><span >
                                              <asp:TextBox ID="txtPassword" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox>
                                            </span>                                            </td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >ชื่อ :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtFirstName" runat="server" 
                                                      Width="200px"></asp:TextBox></td>
                                            <td align="left" valign="top" class="Normal">นามสกุล :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>                                            </td>
                                          </tr>
                                          <tr>
                                            <td align="left"  valign="top"><span class="texttopic">Status :</span></td>
                                            <td align="left" class="Normal">
                                              <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" /></td>
                                            <td align="left" class="Normal">
                                                &nbsp;</td>
                                            <td align="left" class="Normal">
                                                &nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left"  valign="top">&nbsp;</td>
                                            <td align="left" class="Normal" colspan="3">
                                                <asp:Label ID="lblvalidate2" runat="server" CssClass="validateAlert" 
                                                Visible="False" Width="99%"></asp:Label>
                                              </td>
                                          </tr>
                                          </table></td>
                                      </tr>
                                      <tr>
                                                                             
                                        <td valign="top" height="5"></td>
                                      </tr> 
                                      <tr>
                                        <td align="center" valign="top"><span >
                                          <asp:Button ID="cmdSave" runat="server"  CssClass="btn btn-pink" Text="บันทึก" Width="100px" ></asp:Button>
                                          <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" Width="100px" ></asp:Button>
                                        </span></td>
                                      </tr>
                    </table>	

                
 </div>
            <div class="box-footer clearfix">
            </div>
          </div>

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">รายชื่อผู้ใช้งาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">     
	  <TABLE id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%" border="0">			 
       <tr>
          <td  align="left" valign="top"><table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>

              <td>
                  <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
              <td>
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" text="ค้นหา" Width="100px" >       </asp:button>     

              </td>
              <td  class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก username , ชื่อ</td>
            </tr>
             
          </table></td>
      </tr>
       <tr>
          <td align="left" valign="top"  >
              <asp:Label ID="lblStudentCount" runat="server"></asp:Label>           </td>
    </tr>
				  <TR>
				     <TD  valign="top">
                         
                         <asp:GridView ID="grdData" runat="server" CellPadding="0"    GridLines="None"  AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="table table-hover"   Font-Bold="False" PageSize="20">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField HeaderText="No.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                            </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                            <asp:BoundField HeaderText="ชื่อ" DataField="Name" />
                            <asp:BoundField DataField="EMail" HeaderText="อีเมล" />
                            <asp:BoundField DataField="ProfileName" HeaderText="กลุ่ม" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                        <asp:BoundField DataField="LastLogin" HeaderText="Last Login" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsPublic") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>  </TD>
				     </TR>
				 
				  <TR>
				     <TD  valign="top">
                         &nbsp;</TD>
				     </TR>
								</TABLE>	

                
 </div>
            <div class="box-footer clearfix">
            </div>
          </div>
  </section> 
     
                  
            
								
</asp:Content>
