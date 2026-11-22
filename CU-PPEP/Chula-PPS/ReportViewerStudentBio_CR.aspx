<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteNoAjax.Master" CodeBehind="ReportViewerStudentBio_CR.aspx.vb" Inherits=".ReportViewerStudentBio_CR" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" PrintMode="ActiveX" />
      </td>
  </tr>
</table>

 


</asp:Content>
