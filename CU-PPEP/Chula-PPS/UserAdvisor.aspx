<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserAdvisor.aspx.vb" Inherits=".UserAdvisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">
    <link href="css/pagestyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <section class="content-header">
      <h1>จัดการข้อมูล User Account</h1>   
    </section>

<section class="content">  
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">Step 1. เลือกอาจารย์ (แสดงเฉพาะที่ยังไม่มี User)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">   
                <table border="0" cellspacing="2" cellpadding="0" width="100%">   
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>ค้นหา</td>
                                                        <td>
                                            <asp:TextBox ID="txtFindPerson" runat="server" Width="120px"></asp:TextBox>                                                                                           </td>
                                                        <td><asp:LinkButton ID="lnkFindPerson" runat="server" CssClass="btn btn-warning" width="100" >ค้นหา</asp:LinkButton> </td>
                                                    </tr>
                                                </table>
                                            </td>
                                          </tr>
                                          
                                      <tr>
                                        <td >
              <asp:GridView ID="grdPerson" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False"   DataKeyNames="PersonID" Width="50%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้าชื่อ">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FirstName" HeaderText="ชื่อ">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
            <asp:TemplateField HeaderText="เลือก">
              <itemtemplate>
                <asp:ImageButton ID="imgSelect" runat="server" CssClass="gridbutton" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# Container.DataItemIndex  %>' />                        </itemtemplate>
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
          </asp:GridView></td>
                                        </tr> 
                                        </table>     		 
                </div> 
       
          </div>
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">Step 2. บันทึกข้อมูลผู้ใช้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
   <table width="100%" border="0" cellpadding="1" cellspacing="1">
                                          
                                          <tr>
                                            <td width="120" align="left" >UserID : <asp:Label ID="lblID" runat="server"></asp:Label>                                            </td>
                                            <td align="left" class="Normal"><asp:Label ID="lblPersonID" runat="server"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Username :</td>
                                            <td align="left" class="Normal"><asp:TextBox ID="txtUsername" runat="server" 
                                                        CssClass="text" Width="150px"></asp:TextBox>
                                              &nbsp;
                                              <asp:Image 
                                                      ID="imgAlert" runat="server" Height="16px" ImageUrl="images/alert_icon.png" 
                                                      Width="16px" />                                            </td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Password :</td>
                                            <td align="left" class="Normal"><span >
                                              <asp:TextBox ID="txtPassword" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox>
                                            </span></td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >ชื่อ :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtFirstName" runat="server" 
                                                      Width="200px"></asp:TextBox></td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >นามสกุล :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>                                            </td>
                                          </tr>
                                          <tr>
                                            <td align="left"  valign="top">อีเมล :</td>
                                            <td align="left" class="Normal"><asp:TextBox ID="txtMail" runat="server" 
                                                      Width="200px"></asp:TextBox></td>
                                          </tr>
                                          <tr>
                                            <td align="left"  valign="top"><span class="texttopic">Status : </span>
                                              </td>
                                            <td align="left" class="Normal">
                                              <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" /></td>
                                          </tr>
                                          <tr>
                                            <td colspan="2" align="left" ><asp:Label ID="lblvalidate2" runat="server" CssClass="validateAlert" 
                                                Visible="False" Width="99%"></asp:Label>
                                              &nbsp;&nbsp;
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtMail" CssClass="validateAlert" 
                                                ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                Width="99%"></asp:RegularExpressionValidator>                                            </td>
                                          </tr>
                                        </table>
                 </div> 
       
          </div>

    <div class="text-center">

         <asp:Button ID="cmdSave" runat="server" text="บันทึก" cssclass="btn btn-pink"  width="100"></asp:Button>
         <asp:Button ID="cmdClear" runat="server" text="ยกเลิก" cssclass="btn btn-secondary"  width="100"></asp:Button>
    </div>

      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

<table  width="100%">
    <tr>
        <td>
  <table border="0">
            <tr>
              <td width="50" >ค้นหา</td>

              <td>
                  <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
              <td>
                  <asp:Button ID="cmdFind" runat="server"  text="Search" cssclass="btn btn-warning" width="100" /></td>
              
                  <td  class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก username , ชื่อ</td>
              </tr>
          </table>
        </td>
    </tr>
      <tr>
        <td>
            <asp:Label ID="lblStudentCount" runat="server"></asp:Label> 
   <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField HeaderText="No.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                            </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                            <asp:BoundField HeaderText="ชื่อ" DataField="DisplayName" />
                            <asp:BoundField DataField="EMail" HeaderText="อีเมล" />
                            <asp:BoundField DataField="ProfileName" HeaderText="กลุ่ม" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                        <asp:BoundField DataField="LastLogin" HeaderText="Last Login" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png"  CssClass="gridbutton"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsPublic") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"   CssClass="gridbutton"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDel" runat="server"  CssClass="gridbutton"
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />                            </ItemTemplate>
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
        </td>
    </tr>
</table>

                 </div> 
       
          </div>
    </section>
</asp:Content>
