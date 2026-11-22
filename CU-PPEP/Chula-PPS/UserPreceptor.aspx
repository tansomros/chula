<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserPreceptor.aspx.vb" Inherits=".UserPreceptor" %>


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
   

<section class="content-header">
      <h1>กำหนดอาจารย์ (จุฬา) ให้ทำหน้าที่ อาจารย์แหล่งฝึก</h1>   
    </section>

<section class="content">  
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">Step 1. ค้นหาและเลือกแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    <table border="0" align="left" cellPadding="1" cellSpacing="1">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                       </td>
                                                    <td align="left" class="texttopic" width="150">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
                                               
                                                  <td align="left" class="texttopic">แหล่งฝึก :</td>
                                                  <td align="left" class="texttopic">
                                        <asp:TextBox ID="txtFindLocation" runat="server" 
                      Width="150px"></asp:TextBox>
                                                    </td>
                                                  <td align="left" class="texttopic">   <asp:LinkButton ID="lnkFind" runat="server" CssClass="btn btn-warning">ค้นหา</asp:LinkButton> </td>
                                                </tr>
                                                </table>   
                <br />
                <asp:GridView ID="grdLocation" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="LocationID" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="แหล่งฝึก" DataField="LocationName">
                <HeaderStyle HorizontalAlign="Left" />
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภท">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="จัดการ">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# Container.DataItemIndex  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>

    </div>
        </div>

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">Step 2.กำหนดอาจารย์ให้ทำหน้าที่อาจารย์แหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center"> 
       <tr>
          <td  align="left" valign="top">
              <table >
                  <tr>
                      <td class="texttopic">แหล่งฝึก</td>
                      <td align="right" width="50">
                          <asp:Label ID="lblLocationID" runat="server"></asp:Label>
&nbsp;:</td>
                      <td>
                          <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                      </td>
                  </tr>
              </table>
           </td>
      </tr>
       <tr>
          <td valign="top">
              <table width="100%" border="0" cellspacing="2" cellpadding="0">
                <tr>
                  <td width="50%" class="MenuSt">รายชื่อ อ. ที่กำหนดให้</td>
                  <td rowspan="2"><img src="images/arrow-orange-icons.png" width="24"   /></td>
                  <td width="50%"  class="MenuSt">เลือกเพิ่ม อ.แหล่งฝึก</td>
            </tr>
                <tr>
                  <td valign="top" align="center">
                      <asp:GridView ID="grdCoor" 
                             runat="server" CellPadding="2" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
                    <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                    <columns>
                    <asp:BoundField HeaderText="No.">
                      <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                                      </asp:BoundField>
                    <asp:BoundField HeaderText="ชื่อ อ.แหล่งฝึก" DataField="PreceptorName">
                        <HeaderStyle HorizontalAlign="Left" />
                      <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                                      </asp:BoundField>
                    <asp:TemplateField HeaderText="ลบ">
                      <itemtemplate>
                        <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                                        </itemtemplate>
                      <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />                  
                    </asp:TemplateField>
                    </columns>
                    <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                  
                    <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />                  
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />                  
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                  </asp:GridView>
                  <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"  
                  
                  Text="No Data" Height="30px" Width="100%"></asp:Label></td>
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
         <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" AllowPaging="True" CssClass="table table-hover" 
                             Font-Bold="False" Width="100%" PageSize="5">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' ImageUrl="images/icon_arrow.png" />
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
        </table>
  </div>
        </div>
    </section>
</asp:Content>
