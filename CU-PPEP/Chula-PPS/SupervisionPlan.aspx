<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisionPlan.aspx.vb" Inherits=".SupervisionPlan" %>


<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     
    
     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>รายการแผนนิเทศของท่าน</h1>
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">เลือกปีการศึกษา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                            
<table>
<tr>
    <td>ปีการศึกษา :</td>
    <td><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList></td>
</tr>
</table>
               
                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
     <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">รายการแผนการไปนิเทศของท่าน&nbsp; <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;แผน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" VerticalAlign="Middle" />
            <columns>
                <asp:BoundField DataField="Code" HeaderText="Code" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="รายละเอียด แหล่งฝึก/อาจารย์/เวลานัดหมาย">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:HyperLink ID="lnkLocation" CssClass="link_blue_new" runat="server" NavigateUrl='<%# "LocationDetail.aspx?ActionType=loc&ItemType=l&lid=" & DataBinder.Eval(Container.DataItem, "LocationID") %>' Text ='<%# DataBinder.Eval(Container.DataItem, "LocationName") %>'></asp:HyperLink>
                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="2">ผู้นิเทศ :
                                  <u><asp:Label ID="lblSupervisor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupervisorName") %>'></asp:Label></u>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%">วัน-เวลา นัดหมาย :
                                    <asp:Label ID="lblDttm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AppointmentDttm") %>'></asp:Label>
                                    &nbsp;<asp:Label ID="lblTravel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TravelBy") %>'></asp:Label>
                                </td>
                                 <td rowspan="2" valign="top">Remark : </td>
                            </tr>
                            <tr>
                                <td>สถานะการยืนยัน :
                                    <i><asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConfirmStatus") %>'></asp:Label>
                                    &nbsp;<asp:Label ID="lblStatusDttm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConfirmDttm") %>'></asp:Label></i>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="ApproveStatusTXT" HeaderText="การพิจารณา" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="แก้ไข">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' Visible='<%# IIf(DataBinder.Eval(Container.DataItem, "ApproveStatus") = "Y", False, True) %>' ImageUrl="images/icon-edit.png" />
                    </ItemTemplate>
                    <ItemStyle Width="35px" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    
                      
                      CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" VerticalAlign="Middle" HorizontalAlign="Center" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
</div>
            <div class="box-footer clearfix">           
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ยังไม่พบแผนนิเทศที่เลือก" Width="100%"></asp:Label>
            </div>
          </div>
    </section>    
</asp:Content>
