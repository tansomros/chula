<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportConditionBySubject.aspx.vb" Inherits=".ReportConditionBySubject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
      <div class="app-page-title">
      <div class="page-title-wrapper">
          <div class="page-title-heading">
              <div class="page-title-icon">
                  <i class="lnr-pie-chart icon-gradient bg-arielle-smile"></i>
              </div>
              <div><asp:Label ID="lblReportHeader" runat="server"></asp:Label>
               <div class="page-title-subheading"> <asp:Label ID="lblSubheading" runat="server"></asp:Label>
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
             
            
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" >
                                                        ปีการศึกษา (Year) :                                                        </td>
                                                    <td align="left" >
                                                        <asp:DropDownList ID="ddlYear" runat="server" width="100"
                                                            CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" >รายวิชา (Course) :</td>
  <td align="left" >
                                                      <asp:DropDownList ID="ddlCourse" runat="server" 
                                                          Width="500px" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td colspan="2" align="center" >
      <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-primary" Text="Export" Width="100px" />
&nbsp;</td>
  </tr>
  </table>      </td>
      </tr>
       <tr>
          <td  align="center" valign="top"  >
              &nbsp;</td>
      </tr>
       <tr>
         <td  align="center" valign="top" >
              &nbsp;</td>
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
