<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <link href="css/adminstyles.css" rel="stylesheet" type="text/css" media="all">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-key icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Change Password
                                    <div class="page-title-subheading">เปลี่ยนรหัสผ่าน        </div>
                                </div>
                            </div>
                        </div>
                    </div>      

<section class="content">
    <asp:Panel ID="pnForm" runat="server">
        <table border="0" align="center" cellpadding="5" cellspacing="5" >
  <tr>
    <td>รหัสผ่านเดิม :</td>
    <td>
         <asp:TextBox ID="txtOld" runat="server" CssClass="textbox" TextMode="password" ToolTip="Old Password"></asp:TextBox>     </td>
  </tr>
  <tr>
    <td>รหัสผ่านใหม่ :</td>
    <td>
        <asp:TextBox ID="txtNew" runat="server"  TextMode="Password" CssClass="textbox"></asp:TextBox>      </td>
  </tr>
  <tr>
    <td>ยืนยันรหัสผ่านใหม่อีกครั้ง :</td>
    <td>
        <asp:TextBox ID="txtRe" runat="server"  TextMode="Password" CssClass="textbox"></asp:TextBox></td>
  </tr>
            <tr>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
            </tr>           
  <tr>
    <td colspan="2" align="center">
      <asp:Button ID="cmdSave" runat="server" Text="เปลี่ยนรหัสผ่าน" CssClass="buttonlink" />
      </td>
  </tr>
</table>
    </asp:Panel>

<asp:Panel ID="pnResult" runat="server"  >
    <div class="error-page">
        <h2 class="headline text-aqua"><i class="fa fa-info-circle text-aqua"></i></h2>

        <div class="error-content">
          <h3>ระบบเปลี่ยนรหัสผ่านให้ท่านใหม่เรียบร้อยแล้ว</h3>
          <p>ในการเข้าระบบครั้งต่อไปท่านต้องใส่รหัสผ่านใหม่ในการเข้าระบบ</p> 
        </div>
      </div>
    </asp:Panel>
    </section>
</asp:Content>
