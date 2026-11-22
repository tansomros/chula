<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportViewer.aspx.vb" Inherits=".ReportViewer" Debug="true"   %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1><asp:Label ID="lblReportName" runat="server"></asp:Label></h1>    
    </section>    
    <!-- Main content -->
    <section class="content">        
<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td align="center">
      </td>
  </tr> <tr>
    <td align="center"> 
         <asp:Label ID="lblAlert" runat="server" CssClass="text11b_blue"></asp:Label></td>
  </tr>
  <tr>
    <td align="center">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
      </td>
  </tr>
   <tr>
    <td align="center">
          <asp:DropDownList ID="DropDownList1" runat="server">
          </asp:DropDownList>
          <asp:ImageButton ID="cmdPrint" runat="server" 
            ImageUrl="images/btnprint.png" />
          </td>
  </tr>
</table>
</section>
</asp:Content>
