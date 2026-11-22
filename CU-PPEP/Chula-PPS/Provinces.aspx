<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Provinces.aspx.vb" Inherits=".Provinces" %>


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
        การจัดการชื่อจังหวัด</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="center" valign="top">
             
            
<table cellSpacing="1" cellPadding="1" border="0">
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
                                          <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="images/btnsave.png"></asp:ImageButton>
                                          <asp:ImageButton ID="cmdClear" runat="server" 
                                                        ImageUrl="images/btnclear.png"></asp:ImageButton>
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
    
</asp:Content>
