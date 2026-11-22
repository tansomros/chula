<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="ReportServerViewer.aspx.vb" Inherits=".ReportServerViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ProcessingMode="Remote" ShowParameterPrompts="False" ShowPrintButton="true" DocumentMapWidth="100%" ZoomMode="PageWidth">
    </rsweb:ReportViewer>
</form>

