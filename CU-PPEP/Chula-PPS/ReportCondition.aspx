<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportCondition.aspx.vb" Inherits=".ReportCondition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-pie-chart icon-gradient bg-arielle-smile"></i>
                </div>
                <div>
                    <asp:Label ID="lblReportHeader" runat="server"></asp:Label>
                    <div class="page-title-subheading">
                        <asp:Label ID="lblSubheading" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td align="center" valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table border="0" align="center">
                    <tr>
                        <td align="left">ปีการศึกษา (Year) :                                                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="150"
                                CssClass="form-control select2" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">รายวิชา (Course) :</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCourse" runat="server" Width="500px" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">ผลัดฝึก (Batch) :</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlPhase" runat="server" Width="500px" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">แหล่งฝึก (Site) :</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="500px" CssClass="form-control select2"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-primary" Text="Export" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr> 
    </table>
</asp:Content>
