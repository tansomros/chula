<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="AboutResearchUpdate.aspx.vb" Inherits=".AboutResearchUpdate" %>

<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content">  
       <section class="content-header">
      <h1>จัดการข้อมูลเกี่ยวกับสถานวิจัย
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">จัดการข้อมูลเกี่ยวกับสถานวิจัย</li>
      </ol>
    </section>
    <br />
   
               <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">
              <asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label></h3>

          <div class="box-tools pull-right">  <asp:RadioButtonList ID="optCode" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
              <asp:ListItem Selected="True" Value="th">ภาษาไทย</asp:ListItem>
              <asp:ListItem Value="en">English</asp:ListItem>
              </asp:RadioButtonList>
                </div>
        </div>
        <div class="box-body"> 
            
สำหรับเว็บภาษา <asp:Label ID="lblLang" runat="server" Text="" CssClass="text-success text-bold"></asp:Label>
      <dx:ASPxHtmlEditor ID="txtDetail" runat="server" Height="500px" 
        Theme="Office2003Olive" Width="100%">
    </dx:ASPxHtmlEditor>


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
