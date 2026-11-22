<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Courses.aspx.vb" Inherits=".Courses" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
 
<section class="content-header">
      <h1>จัดการรายวิชาที่เปิดฝึกฯ</h1>   
    </section>

<section class="content">  

     <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกปีการศึกษา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" Width="100" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
                                                </tr>
  </table> 
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายวิชาที่เปิดฝึกฯใน ปีการศึกษา 
              <asp:Label ID="lblYear" runat="server"></asp:Label></h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                                 <asp:GridView ID="grdCourse" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" Width="100px" />                      </asp:BoundField>
                <asp:BoundField DataField="NameTH" HeaderText="ชื่อรายวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <ItemStyle HorizontalAlign="Center" Width="100px" />                </asp:BoundField>
            <asp:TemplateField HeaderText="อ.ประจำวิชา">
              <itemtemplate>
                  <asp:ImageButton ID="imgView" runat="server" 
                      CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' 
                      ImageUrl="images/cubo.gif" Width="25" />                </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/icon-delete.png"   CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC01" HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
</div> 
        <div class="box-footer clearfix">           
                
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"                   
                  Text="ยังไม่พบรายวิชาที่เปิดฝึกฯในปีนี้   เลือกรายวิชาที่ต้องการเปิดด้านล่าง"></asp:Label>                      
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-play-circle-o"></i>

              <h3 class="box-title">เพิ่มรายวิชาที่เปิดฝึกฯ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages">                                      

        
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center"   Width="90px" />                      </asp:BoundField>
                <asp:BoundField DataField="NameTH" HeaderText="ชื่อรายวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <ItemStyle HorizontalAlign="Center" />                </asp:BoundField>
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
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
            </div>
          </div>
                       
    </section>  
    
</asp:Content>
