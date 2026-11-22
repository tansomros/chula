<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssignSpecify.aspx.vb" Inherits=".AssignSpecify" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">       
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1>จัดสรรแหล่งฝึกให้นิสิต by Admin</h1>   
    </section>

<section class="content">  
  <div class="row"> 
        <section class="col-lg-12 connectedSortable">    
     <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">กำหนดเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                   <div class="row">
           <div class="col-md-2">
          <div class="form-group">
            <label>ปี</label>
             <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2 text-center"></asp:DropDownList>  
          </div>

        </div>

        <div class="col-md-5">
          <div class="form-group">
            <label>รายวิชา</label>
              <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>     
          </div>
        </div>
      
       </div>  
   <div class="row"> 
  <div class="col-md-5">
          <div class="form-group">
            <label>แหล่งฝึก</label>
  <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>  
     
          </div>

        </div>
    
         <div class="col-md-5">
          <div class="form-group">
            <label>ผลัดฝึก</label>
              <asp:DropDownList ID="ddlPhase" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>     
          </div>
        </div>
       </div>
</div>
            <div class="box-footer clearfix">           
            </div>
          </div>    
</section>
  </div>
      <div class="row"> 

<section class="col-lg-6 connectedSortable">   
 
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ได้สิทธิ์ไปฝึกตามเงื่อนไข ทั้งหมด&nbsp; <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">     
        <table border="0" >
            <tr>
              <td>ค้นหา</td>
              <td width="150">
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>              </td>
            </tr>
           
          </table>
 <asp:GridView ID="grdStudent" CssClass="table table-hover"
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัส">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" Width="120px"/>

            </asp:BoundField>

                <asp:BoundField DataField="StudentName" HeaderText="ชื่อนิสิต">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </asp:BoundField>
                <asp:BoundField DataField="SkillName" HeaderText="งาน" />
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/delete.png"     CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessmentID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" HorizontalAlign="Left" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>

<asp:Label ID="lblNo" runat="server" CssClass="alert alert-error show" Text="ยังไม่พบนิสิตที่จัดสรรแหล่งฝึกตามเงื่อนไข" Width="100%"></asp:Label>  
</div> 
        <div class="box-footer clearfix">           
                <small class="text-primary">*ตามเงื่อนไข ปี/งาน/แหล่งฝึก/ผลัดฝึก</small>
            </div>
          </div>
</section>
<section class="col-lg-6 connectedSortable">
     <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เพิ่มนิสิต</h3><small>แสดงเฉพาะ นศ.ที่ยังได้แหล่งฝึกไม่ครบ</small>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
           <table border="0"  width="100%">   
    <tr>
          <td valign="top" > 
  <table border="0" >
            <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td > <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>            </td>
            </tr>
            </table>         </td>
        </tr>
    <tr>
          <td align="center" valign="top" class="mailbox-messages">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="Student_Code" CssClass="table table-hover" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top"  Width="90px" />                      </asp:BoundField>
                <asp:BoundField DataField="StudentName" HeaderText="ชื่อนิสิต">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left"/>
                </asp:BoundField>

            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True"  
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr> 
    <tr>
        <td align="center" valign="top">
            <asp:Label ID="lblNotic" runat="server" CssClass="alert alert-error show" Text="กรุณาเลือกเงื่อนไขเพื่อค้นหาข้อมูลก่อน" Width="99%"></asp:Label>
        </td>
    </tr>
    <tr>    
          <td valign="top">
              <table border="0" >
            <tr>
              <td></td>             
              <td><asp:LinkButton ID="lnkSubmitAdd" runat="server"  CssClass="buttonModal" Visible="False">เพิ่มทั้งหมด</asp:LinkButton></td>
            </tr>
          </table></td>
      </tr>       
    <tr>
          <td align="center" valign="top">
         <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-save" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>         </td>
        </tr>
  
    
</table>                             
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</section>
</div> 
    </section>    
</asp:Content>
