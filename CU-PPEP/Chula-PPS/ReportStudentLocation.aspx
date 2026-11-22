<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportStudentLocation.aspx.vb" Inherits=".ReportStudentLocation" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="Page_Header">
        รายงานการเลือกแหล่งฝึกของนิสิต</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" >
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" >
                                                        <asp:DropDownList ID="ddlYear" runat="server" 
                                                            CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" >รายวิชา :</td>
  <td align="left" >
                                                      <asp:DropDownList ID="ddlCourse" runat="server" 
                                                          Width="400px" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td colspan="2" align="center" >
      <asp:Button ID="cmdPrint1" runat="server" CssClass="btn btn-pink" Text="รายงานแบบที่ 1" />
&nbsp;<asp:Button ID="cmdPrint2" runat="server" CssClass="btn btn-pink" Text="รายงานแบบที่ 2" />
      &nbsp;<asp:Button ID="cmdPrint3" runat="server" CssClass="btn btn-pink" Text="รายงานแสดงทั้งปี" />
    </td>
  </tr>
  </table>      </td>
      </tr>
       <tr>
          <td  align="center" valign="top"  >
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>      </td>
      </tr>
       <tr>
         <td  align="center" valign="top" >
             <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
            <ContentTemplate> 
                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ProcessingMode="Remote" ShowParameterPrompts="False" ShowPrintButton="true" DocumentMapWidth="100%" ZoomMode="PageWidth">
    </rsweb:ReportViewer>      
                
            </ContentTemplate> 
        </asp:UpdatePanel>
           </td>
       </tr>
       <tr>
         <td  align="center" valign="top"  >
             <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels" 
                 Text="ไม่มีข้อมูลการเลือกแหล่งฝึก" Width="95%"></asp:Label>
           </td>
       </tr>
        <tr>
         <td    align="left" valign="top" >&nbsp;</td>
       </tr>

       <tr>
         <td   align="center" valign="top" >&nbsp;</td>
      </tr>
       <tr>
         <td height="200"  align="left" valign="top" >&nbsp;</td>
       </tr>
    
    </table>
    
</asp:Content>
