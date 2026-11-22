 
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ChangePassword.aspx.vb" Inherits=".ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />

<link rel="stylesheet" type="text/css" href="css/uidialog.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <section class="content-header">
      <h1>Change Password</h1>  
    </section>
<section class="content">
    
    <asp:Panel ID="pnForm" runat="server"> 
 
 <table border="0" align="center" cellpadding="0" cellspacing="2">
  <tr>
    <td>Old password :</td>
    <td>
        <asp:TextBox ID="txtOld" runat="server" Width="200px" TextMode="Password"></asp:TextBox>      </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>New password :</td>
    <td>
        <asp:TextBox ID="txtNew" runat="server" Width="200px" TextMode="Password"></asp:TextBox>      </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>Confirm new password :</td>
    <td>
        <asp:TextBox ID="txtRe" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
    <td><asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtNew" ControlToValidate="txtRe" 
            ErrorMessage="The new password confirmation does not match." CssClass="validateAlert"></asp:CompareValidator></td>
  </tr>
  <tr>
      <td></td>
    <td align="center"><br />
        <asp:LinkButton ID="cmdSave"  CssClass="buttonLink"  runat="server"> Submit </asp:LinkButton>      </td>
       <td></td>
    </tr>
</table>
    </asp:Panel>
           
           
                    <asp:Panel ID="pnResult" runat="server"  >
                        <table border="0" cellpadding="0" cellspacing="2" width="90%">
                            <tr>
                                <td align="center">
                                     Your password has been updated successfully.
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    You will need to use the new password for your next login.
                                </td>
                            </tr>
                             <tr>
          <td align="center">
              &nbsp;</td>
        </tr>

                        </table>
                    </asp:Panel>
      </section>
</asp:Content>
