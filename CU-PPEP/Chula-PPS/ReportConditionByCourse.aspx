<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportConditionByCourse.aspx.vb" Inherits=".ReportConditionByCourse" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
  
  <script> $(function() {
       $( "#dialog-message" ).dialog({
      autoOpen: false,
      height: 600,
      width: 600,
      modal: true,
      buttons: {
        Close: function() {
          $( this ).dialog( "close" );
        }
      },
      close: function() {
        
      }
    });
 
    $( "#user-login" )
      .button()
      .click(function() {
        $( "#dialog-message" ).dialog( "open" );
      });
  });
  </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="Page_Header">
        <asp:Label ID="lblReportHeader" runat="server"></asp:Label>
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
      <asp:Button ID="cmdView" runat="server" CssClass="btn btn-pink" Text="ดูรายงาน" />
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
