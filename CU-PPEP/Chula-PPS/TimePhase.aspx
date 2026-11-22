<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TimePhase.aspx.vb" Inherits=".TimePhase" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    </asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<section class="content-header">
      <h1>กำหนดผลัดฝึกประจำรายวิชา</h1>   
    </section>

<section class="content">   
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">จัดการผลัดฝึก</h3>
             
                 <div class="box-tools pull-right"><asp:Label ID="lblID" runat="server"></asp:Label> 
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

<table  border="0"  width="100%">
 <tr>
                                                    <td align="left" width="100"  valign="top">                                                        ปีการศึกษา :    </td>
                                                    <td align="left"> 
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" width="100" AutoPostBack="True" >
                                                        </asp:DropDownList>
                                                     </td>
                                                </tr>
                                                 <tr>
                                                    <td align="left"  valign="top">                                                        รายวิชา : </td>
                                                    <td align="left" >
                                                      <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">                                                       ผลัดที่เปิด : 
                                                        </td>
                                                    <td align="left" >
                                                        <asp:CheckBoxList ID="chkPhase" runat="server"> </asp:CheckBoxList>                                            </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">                                                       &nbsp;</td>
                                                    <td align="left" >
                                                         <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
                                     </td>
                                                </tr>
                                                </table>        
 </div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการผลัดฝึกในแต่ละรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">                                        
 
 
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="CourseID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="EduYear" HeaderText="ปีการศึกษา">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา">
              <itemstyle HorizontalAlign="Center" />                      </asp:BoundField>
                <asp:BoundField HeaderText="ชื่อวิชา" DataField="SubjectName" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%#  Container.DataItemIndex %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%#  Container.DataItemIndex %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
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
