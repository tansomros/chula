<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ReportViewer.aspx.vb" Inherits=".ReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   

<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td align="center">
        <asp:Panel ID="Panel1" runat="server">
        <table border="0" cellspacing="2" cellpadding="0"> <tr>
    <td align="center" class="text11b_blue">&nbsp;</td>
  </tr>
  <tr>
    <td class="text11b_blue">Browser นี้ไม่สนับสนุนระบบแสดงผลรายงาน กรุณาเปลี่ยนไปใช้ 
        Chrome เพื่อประสิทธิภาพของรายงาน </td>
  </tr>
  <tr>
    <td align="center" class="text11b_blue"> ขออภัยในความไม่สะดวก</td>
  </tr>
 <tr>
    <td class="text11b_blue">Browser does not support display reports. Please switch to Google Chrome Browser for performance reports.</td>
  </tr>
  <tr>
    <td align="center" class="text11b_blue"> Sorry for the inconvenience</td>
  </tr>
</table>
</asp:Panel>
      </td>
  </tr>
   
    <tr>
        <td align="center">
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ProcessingMode="Remote" ShowParameterPrompts="False" ShowPrintButton="true" DocumentMapWidth="100%" ZoomMode="PageWidth">
    </rsweb:ReportViewer>
        </td>
    </tr>
</table>
</asp:Content>
