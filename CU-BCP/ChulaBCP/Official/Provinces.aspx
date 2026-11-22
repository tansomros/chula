<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Provinces.aspx.vb" Inherits=".Provinces" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server"> 
    </asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-user-female icon-gradient bg-primary"></i>
                                </div>
                                <div>Province
                                    <div class="page-title-subheading">  การจัดการชื่อจังหวัด</div>
                                </div>
                            </div>
                        </div>
                    </div>    
<section class="content">
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="center" valign="top">
             
            
<table  border="0">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ID : 
                                                        </td>
                                                    <td align="left" >
                                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
&nbsp;*Auto ID</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ชื่อจังหวัด :</td>
                                                    <td align="left" class="texttopic"><asp:TextBox ID="txtName" runat="server" Width="300px" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">&nbsp;                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" class="texttopic">
                                                        &nbsp;
											            &nbsp;
													    <span >
                                          <asp:Button ID="cmdSave" runat="server" text="ค้นหา" CssClass="btn btn-primary"></asp:Button>
                                          <asp:Button ID="cmdClear" runat="server" 
                                                        text="ค้นหา" CssClass="btn btn-secondary"></asp:Button>
                                        </span>                                                    </td>
                                                </tr>
  </table>        
  
 
  
      </td>
      </tr>
       <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="60%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="ProvinceID" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProvinceID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProvinceID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
       
    </table>
  </section>  
</asp:Content>
