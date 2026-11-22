<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisorManagexxx.aspx.vb" Inherits=".SupervisorManagexxx" %>

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
       กำหนดอาจารย์ออกนิเทศ โดย Admin</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
            <td align="center" valign="top">
             
            
<table border="0" align="left" cellPadding="1" cellSpacing="1">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        เลือกปี :                                                       </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">เลือกรายวิชา :</td>
                                                  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                                                          Width="400px" CssClass="Objcontrol">                                                      </asp:DropDownList>                                                    </td>
                                                  <td align="left" class="texttopic">   &nbsp;</td>
                                                </tr>
                                                </table>      </td>
      </tr>
       <tr>
          <td  align="left" valign="top">&nbsp;</td>
      </tr>
       <tr>
          <td  align="left" valign="top"  class="Section_Header">เลือกแหล่งฝึกและผลัดฝึก</td>
      </tr>
              <tr>
          <td valign="top"  class="skin_Search"> <table border="0" cellspacing="1" cellpadding="0">
           <tr>
              <td >จังหวัด</td>
              <td >
            <asp:DropDownList CssClass="Objcontrol" ID="ddlProvince" runat="server" AutoPostBack="True">            </asp:DropDownList>
               </td>
              <td >&nbsp;</td>
            </tr>  <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtLocation" runat="server" Width="150px"></asp:TextBox>              </td>
              <td ><asp:ImageButton ID="cmdFindLocation" runat="server" ImageUrl="images/btnSearch.png" />              </td>
            </tr>
           
            </table>         </td>
        </tr>


        <tr>
          <td align="center" valign="top">
          <asp:HiddenField ID="HiddenField1" runat="server" />
              <asp:GridView ID="grdLocation" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เลือก">
                <ItemTemplate>
                    <input  type="radio" name="optSelect"
            onclick="document.getElementById('<%=HiddenField1.ClientID%>').value=<%#Container.DataItemIndex%>;" value="" />            </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
                <asp:BoundField DataField="TimePhaseID" HeaderText="ผลัดฝึกที่">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="ชื่อแหล่งฝึก">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField HeaderText="จำนวนนิสิต" DataField="STDCOUNT">                
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
          <td  align="left" valign="top">&nbsp;</td>
      </tr>
       <tr>
          <td valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="0">
                <tr>
                  <td width="50%" class="MenuSt">รายชื่อ อ.นิเทศ
                  <asp:Label ID="lblSubjectName" runat="server"></asp:Label>                </td>
                  <td rowspan="2"><img src="images/arrow-32.png" width="32" height="32" /></td>
                  <td width="50%"  class="MenuSt">เลือกเพิ่ม อ.นิเทศ</td>
            </tr>
                <tr>
                  <td valign="top">
                      <asp:GridView ID="grdSupervisor" 
                             runat="server" CellPadding="2" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
                    <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                    <columns>
                    <asp:BoundField HeaderText="No.">
                      <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                                      </asp:BoundField>
                    <asp:BoundField HeaderText="ชื่ออาจารย์นิเทศ">
                      <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                                      </asp:BoundField>
                    <asp:TemplateField HeaderText="ลบ">
                      <itemtemplate>
                        <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"itemID") %>' />                                        </itemtemplate>
                      <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />                  
                    </asp:TemplateField>
                    </columns>
                    <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                  
                    <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />                  
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />                  
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                  </asp:GridView>
                  <asp:Label ID="lblNo" runat="server" CssClass="text_red" 
                  
                  Text="ยังไม่พบรายชื่ออาจารย์นิเทศในแหล่งฝึกนี้"></asp:Label></td>
                  <td><table width="100%" border="0" cellspacing="1" cellpadding="0">
                    <tr>
                      <td><table border="0" cellspacing="2" cellpadding="0">
                        <tr>
                          <td>ค้นหา</td>
                          <td>
                              <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                            </td>
                          <td>
                  <asp:ImageButton ID="cmdFind" runat="server" ImageUrl="images/btnSearch.png" />                </td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td>
         <asp:GridView ID="grdTeacher" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False" Width="100%" PageSize="5">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"PersonID") %>' ImageUrl="images/icon_arrow.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้า" >                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อ" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>  </td>
                    </tr>
                  </table></td>
                </tr>
                
              </table></td>
      </tr>
       <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
        <tr>
          <td align="center" valign="top">&nbsp;
              </td>
        </tr>
    </table>
    
</asp:Content>
