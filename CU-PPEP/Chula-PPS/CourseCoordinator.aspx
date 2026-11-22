<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CourseCoordinator.aspx.vb" Inherits=".CourseCoordinator" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1>จัดการอาจารย์ประสานงานรายวิชา</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <table border="0" cellPadding="1" cellSpacing="1">
                                                <tr>
                                                    <td class="texttopic">
                                                        เลือกปีการศึกษา :                                                       </td>
                                                    <td class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2" width="100">                                                        </asp:DropDownList>                                                    </td>
                                                    <td class="texttopic">&nbsp;</td>
                                                    <td class="texttopic">&nbsp;</td>
                                                    <td class="texttopic">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                  <td class="texttopic">เลือกรายวิชา :</td>
                                                  <td class="texttopic">
                                                      <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" 
                                                           CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
                                                  <td class="texttopic">   <asp:Image ID="imgArrowUser" runat="server" ImageUrl="images/arrow-orange-icons.png" />
                                              &nbsp;</td>
                                                  <td class="texttopic">   
                                        <asp:TextBox ID="txtFindSubject" runat="server" 
                      Width="120px"></asp:TextBox>
                                                    </td>
                                                  <td class="texttopic">   <asp:LinkButton ID="lnkFind" runat="server" CssClass="buttonLink">ค้นหา</asp:LinkButton> </td>
                                                </tr>
                                                </table>  
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข อาจารย์ประสานงานรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
      <table width="100%" border="0">
  <tr>
    <td width="50%" valign="top">
    <table width="100%" border="0">
  <tr>
     <td width="50%" class="MenuSt">รายชื่อ อ.ประสานงานรายวิชา
                  <asp:Label ID="lblSubjectName" runat="server"></asp:Label>                </td>
  </tr>
  <tr>
    <td><asp:GridView ID="grdCoor" 
                             runat="server" CellPadding="2" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
      <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
      <columns>
        <asp:BoundField HeaderText="No.">
          <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />    
        </asp:BoundField>
        <asp:BoundField HeaderText="ชื่อ อ. ประสานงานรายวิชา">
          <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />    
        </asp:BoundField>
        <asp:TemplateField HeaderText="ลบ">
          <itemtemplate>
            <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "itemID") %>' />    
          </itemtemplate>
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
    </asp:GridView></td>
  </tr>
</table>

    
    </td>
    <td align="center" valign="middle" style="font-size:24px"> <i class="fa fa-arrow-circle-left text-maroon"></i></td>
    <td valign="top">
     <table width="100%" border="0">
  <tr>
    <td width="50%"  class="MenuSt">เลือกเพิ่ม อ.ประสานงานรายวิชา</td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="1" cellpadding="0">
                    <tr>
                      <td><table border="0" cellspacing="2" cellpadding="0">
                        <tr>
                          <td>ค้นหา</td>
                          <td>
                              <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                            </td>
                          <td>
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>               </td>
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
                            <asp:TemplateField HeaderText="เพิ่มอาจารย์">
                                <ItemTemplate> 
                                    <span class="label label-success text12">
                                    <i class="fa fa-arrow-circle-left"></i>
                                    <asp:LinkButton ID="imgSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>'  Text=" เพิ่ม"  CssClass="label label-success"/>
                               </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                
                            </asp:TemplateField>
                        <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้า" >                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อ" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC01" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>  </td>
                    </tr>
                  </table></td>
  </tr>
</table>
</td>
  </tr>
</table>                 
</div> 
        <div class="box-footer clearfix">           
                
                  <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"  
                  
                  Text="ยังไม่พบรายชื่ออาจารย์ผู้ประสานงานรายวิชานี้"></asp:Label>           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">ค้นหารายวิชา เพื่อจัดการอาจารย์ประสานงานรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              <table border="0" cellspacing="2" cellpadding="0" >
            <tr>
              <td width="100">ปีการศึกษา</td>
              <td>
                  <asp:DropDownList ID="ddlYear2" runat="server" AutoPostBack="True" 
                      Width="100px" CssClass="form-control select2">
                  </asp:DropDownList>
                </td> 
            </tr>
            
          </table>

              <asp:GridView ID="grdCourse" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="CourseID" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
                <asp:BoundField DataField="CYEAR" HeaderText="ปีการศึกษา" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />                      </asp:BoundField>
                <asp:BoundField DataField="SubjectName" HeaderText="ชื่อรายวิชา">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Edit">
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
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>  
</asp:Content>
