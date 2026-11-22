<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Department.aspx.vb" Inherits=".Department" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="Page_Header">
        จัดการชื่อภาควิชา</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="center" valign="top">
             
            
<table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        รหัส : 
                                                        </td>
                                                    <td align="left" class="texttopic"> 
                                                        <asp:Label ID="lblID" runat="server"></asp:Label>                                                    &nbsp;<asp:Label ID="lblMode" runat="server" Text="Mode : Add New" 
                  CssClass="block_Mode"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ชื่อภาควิชา :</td>
                                                    <td align="left" class="texttopic"><asp:TextBox ID="txtName" runat="server" 
                                                            Width="300px" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">Status : </td>
                                                    <td align="left" ><asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                                                            Checked="True" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" class="texttopic">
                                                       
                                          <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="images/btnsave.png"></asp:ImageButton>
                                          <asp:ImageButton ID="cmdClear" runat="server" 
                                                        ImageUrl="images/btnclear.png"></asp:ImageButton>
                                                                                          </td>
                                                </tr>
  </table>        
  
 
  
      </td>
      </tr>
       <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
          <td  align="left" valign="top"><table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:ImageButton ID="cmdFind" runat="server" ImageUrl="images/btnSearch.png" />                  
                </td>
            </tr>
            <tr>
                  <td colspan="3" class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก 
                      ชื่อ</td>
              </tr>
          </table></td>
      </tr>
       <tr>
          <td align="left" valign="top"  >
              &nbsp;</td>
    </tr>

        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="ID" DataField="DepartmentUID">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="DepartmentName" HeaderText="ชื่อภาควิชา">
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DepartmentUID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DepartmentUID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
       
    </table>
    
</asp:Content>
