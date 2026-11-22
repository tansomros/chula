<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="DeanMessage.aspx.vb" Inherits=".DeanMessageUpdate" %>

<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content">  
       <section class="content-header">
      <h1>About Us
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">About Us</li>
      </ol>
    </section>
    <br />
   
               <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">
              <asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label></h3>

          <div class="box-tools pull-right"><asp:RadioButtonList ID="optCode" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
              <asp:ListItem Selected="True" Value="th">ภาษาไทย</asp:ListItem>
              <asp:ListItem Value="en">English</asp:ListItem>
              </asp:RadioButtonList>
                </div>
        </div>
        <div class="box-body"> 
            
            <table style="width:100%;">
                <tr>
                    <td style="width:50px;">ID</td>
                    <td> 
            
 <asp:Label ID="lblMsgID" runat="server" CssClass="text-success text-bold"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Header</td>
                    <td>
                        <asp:TextBox ID="txtHead" runat="server" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Title</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">Message</td>                     
                </tr>
                 <tr>
                    <td colspan="2">
                        <dx:ASPxHtmlEditor ID="txtDetail" runat="server" Height="500px" Width="99%"></dx:ASPxHtmlEditor>
                    </td>                     
                </tr>
                 <tr>
                    <td  colspan="2">
                        <asp:CheckBox ID="chkActive" runat="server" Checked="True" ForeColor="Red" Text="Active" />
                     </td>
                   
                </tr>


            </table>
   


</div>
        <!-- /.box-body -->
        <div class="box-footer">
         <asp:Button ID="bttSave" runat="server" CssClass="buttonSave" 
        Text="บันทึก" Width="100px" />
                   &nbsp;<asp:Button ID="bttCancel" runat="server" CssClass="buttonSave" 
        Text="ยกเลิก" Font-Bold="False" PostBackUrl="Dafault.aspx" />
        </div>
        <!-- /.box-footer-->
      </div>
         
  </section>     
</asp:Content>
