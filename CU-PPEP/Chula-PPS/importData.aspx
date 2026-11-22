<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="importData.aspx.vb" Inherits=".importData" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Page_Header">จัดการข้อมูลนิสิต</div> 
 <table width="100%" border="0" cellspacing="2" cellpadding="0">
   <tr>
     <td width="50%"><table border="0" align="center" cellpadding="0" cellspacing="2">
       <tr>
         <td width="100">รหัสนิสิต</td>
         <td>
             <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>           </td>
       </tr>
       <tr>
         <td>ชื่อ</td>
         <td>
             <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>           </td>
       </tr>
       <tr>
         <td>นามสกุล</td>
         <td>
             <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>           </td>
       </tr>
       <tr>
         <td>&nbsp;</td>
         <td><span class="texttopic">
                                          <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="images/btnsave.png"></asp:ImageButton>
                                        </span></td>
       </tr>
     </table></td>
     <td valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="0">
       <tr>
         <td>Import from Excel file</td>
         </tr>
       <tr>
         <td>
             <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Button ID="cmdImport" runat="server" Text="import" Width="64px" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Label ID="lblResult" runat="server" CssClass="GreenAlert"></asp:Label>           </td>
         </tr>
     </table></td>
   </tr>
   <tr>
     <td colspan="2" align="center">
         &nbsp;</td>
    </tr>
   <tr>
     <td colspan="2">
         <asp:GridView ID="grdData" 
                             runat="server" CellPadding="4" 
                                                        GridLines="None" 
             AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False" Width="100%" AutoGenerateColumns="False">
                        <RowStyle BackColor="#EFF3FB" />
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationB dc_paginationB05" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            ForeColor="#E3088A" />                     
                        <Columns>
                            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต" />
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อนิสิต" />
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>  </td>
    </tr>
 </table>
</asp:Content>
