<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PaymentConfig.aspx.vb" Inherits=".PaymentConfig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
        Payment Config : กำหนดค่าตอบแทน</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="center" valign="top">
             
            
<table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td width="150" align="left" class="texttopic">
                                                        ID : 
                                                        </td>
                                                    <td align="left" >
                                                        <asp:Label ID="lblID" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150" align="left" class="texttopic">
                                                        รายวิชา</td>
                                                    <td align="left" >
                                                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">การคำนวณ</td>
                                                    <td align="left">
                                                        <asp:RadioButtonList ID="optType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Selected="True">แบบรายหัว (ต่อคนต่อวัน)</asp:ListItem>
                                                            <asp:ListItem Value="2">แบบเหมาจ่าย (ต่อผลัดต่อคน)</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">Rate ค่าตอบแทน </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtRateAmount" runat="server" MaxLength="4"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">Effective To</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtEffectiveTo" runat="server"></asp:TextBox> <cc1:calendarextender
            ID="CalendarExtender1" runat="server"  PopupButtonID="txtEffectiveTo"  
            TargetControlID="txtEffectiveTo">        </cc1:calendarextender>   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">Status</td>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkActive" runat="server" Checked="True" Text="Active / ใช้งาน" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" class="texttopic">
        <asp:Button ID="cmdSave" runat="server" Text="บันทึก" CssClass="btn btn-pink" Width="70px" />
        &nbsp;<asp:Button ID="cmdClear" runat="server" Text="ยกเลิก" CssClass="btn btn-pink" Width="70px" />
                                                    </td>
                                                </tr>
  </table>        
  
 
  
      </td>
      </tr>
       <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
          <td align="left" valign="top"><table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>                </td>
              <td >
                  &nbsp;
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />
                </td>
            </tr>
            
          </table></td>
      </tr>
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" DataKeyNames="UID" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รายวิชา">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PayAmount" HeaderText="ค่าตอบแทน" />
                <asp:BoundField DataField="PaymentTypeName" HeaderText="คำนวณแบบ" />
                <asp:BoundField DataField="EffectiveTo" HeaderText="Effective To" />
                <asp:TemplateField HeaderText="Active">
                     <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsPublic")%>' />                        </itemtemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID")%>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID")%>' />                        </itemtemplate>
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
