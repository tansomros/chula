<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportConditionByLevelClass.aspx.vb" Inherits=".ReportConditionByLevelClass" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <section class="content-header">
      <h1><asp:Label ID="lblReportHeader" runat="server"></asp:Label></h1>   
    </section>

<section class="content"> 
  
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left">
                                                        ชั้นปี :                                                        </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlLevel" runat="server" 
                                                            CssClass="form-control select2">
                                                            <asp:ListItem Selected="True" Value="0">ทั้งหมด</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td colspan="2" align="center" >
      <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-pink" Text="Export" Width="100px" />
    </td>
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
             &nbsp;</td>
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
</section>    
</asp:Content>
