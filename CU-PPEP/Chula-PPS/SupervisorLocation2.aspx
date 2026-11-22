<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisorLocation.aspx.vb" Inherits=".SupervisorLocation" %>


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
        เลือกแหล่งฝึกที่ต้องการไปนิเทศ</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="left" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปี :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="Objcontrol">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา :</td>
  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                                                          Width="400px" CssClass="Objcontrol">                                                      </asp:DropDownList>                                                    </td>
  <td align="left">&nbsp;</td>
</tr>
  </table>      </td>
      </tr>
       <tr>
          <td  align="left" valign="top"  class="MenuSt"><table border="0" cellspacing="2" cellpadding="0">
          <tr>
              <td colspan="3">รายชื่อแหล่งฝึกที่เลือกแล้วทั้งหมด&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;แหล่ง</td>
            </tr>   <tr>
              <td>ค้นหา</td>
              <td width="150">
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:ImageButton ID="cmdFindStd" runat="server" 
                      ImageUrl="images/btnSearch.png" />              </td>
            </tr>
           
          </table>
           </td>
      </tr>
       <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="itemID">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:BoundField DataField="TimePhaseID" HeaderText="ผลัดที่" />
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">

                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                      
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภท" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    
                      
                      CommandArgument='<%#  Container.DataItemIndex  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
              <asp:Label ID="lblNo" runat="server" CssClass="text_red" 
                  
                  
                  Text="ยังไม่พบแหล่งฝึกที่เลือก &lt;br/&gt;  เลือกแหล่งฝึกที่ต้องการเปิดด้านล่าง"></asp:Label>           </td>
      </tr>
      
      <tr>
        <td>&nbsp;</td>	
      </tr>
        <tr>
          <td  align="left" valign="top"  class="Section_Header">เลือกแหล่งฝึกและผลัดฝึก</td>
      </tr>
              <tr>
          <td valign="top"  class="skin_Search"> <table border="0" cellspacing="1" cellpadding="0">
           <tr>
              <td >จังหวัด</td>
              <td >
            <asp:DropDownList CssClass="Objcontrol" ID="ddlProvince" runat="server">            </asp:DropDownList>
               </td>
              <td >&nbsp;</td>
            </tr>  <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td ><asp:ImageButton ID="cmdFind" runat="server" ImageUrl="images/btnSearch.png" />              </td>
            </tr>
           
            </table>         </td>
        </tr>


        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เลือก">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
                <asp:BoundField DataField="TimePhaseID" HeaderText="ผลัดฝึกที่">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="ชื่อแหล่งฝึก">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField HeaderText="ประเภท" DataField="LocationGroupName">                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
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
       <tr>
          <td align="left" valign="top"><table border="0" cellspacing="2" cellpadding="0">
            <tr>
              <td></td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
            </tr>
          </table></td>
      </tr>
        
        <tr>
          <td align="center" valign="top">
        <asp:ImageButton ID="cmdSave" runat="server" 
            ImageUrl="images/btnsave.png" />
        <asp:ImageButton ID="cmdClear" runat="server" 
            ImageUrl="images/btnclear.png" />            </td>
        </tr>
       
    </table>
    
</asp:Content>
