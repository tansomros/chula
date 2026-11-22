<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Courses_Reg.aspx.vb" Inherits=".Courses_Reg" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>รายวิชาฝึกปฏิบัติวิชาชีพ</h1>     
    </section>
<section class="content">

<table cellSpacing="1" cellPadding="1" border="0" align="center">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        เลือกปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" Width="150" CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
                                                </tr>
  </table> 

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">รายวิชาที่ฝึกใน ปีการศึกษา <asp:Label ID="lblYear" runat="server"></asp:Label>
                </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">    
             
   
              <asp:GridView ID="grdCourse" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" VerticalAlign="Top" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="ชื่อรายวิชา">
                    <ItemTemplate>
                        <asp:Label ID="lblTH0" runat="server" CssClass="NameTH" Text='<%# DataBinder.Eval(Container.DataItem, "NameTH") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblEN0" runat="server" CssClass="NameEN" Text='<%# DataBinder.Eval(Container.DataItem, "NameEN") %>'></asp:Label>        
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <ItemStyle HorizontalAlign="Center" />                </asp:BoundField>
                <asp:BoundField HeaderText="สถานะ" DataField="Status" />
            <asp:TemplateField HeaderText="แหล่งฝึก">
              <itemtemplate>                                     
                  <asp:LinkButton ID="lnkSelect" runat="server" CssClass="label label-success" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' >เลือกแหล่งฝึก</asp:LinkButton>
                </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="top" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" VerticalAlign="Top" />
          </asp:GridView>
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"  Text="ยังไม่พบรายวิชาที่ฝึกฯในปีนี้"></asp:Label>   
     </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    </section>
</asp:Content>
