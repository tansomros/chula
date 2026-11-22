<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GradePreceptorCopy.aspx.vb" Inherits=".GradePreceptorCopy" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <section class="content-header">
      <h1>Grade Copy(สำหรับแหล่งฝึก)</h1>
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-gears"></i>

              <h3 class="box-title">เลือกต้นแบบ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table border="0">
<tr>    
    <td >ปีการศึกษา</td>
    <td ><asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" AutoPostBack="True"  width="150"></asp:DropDownList></td>

                                                  <td class="texttopic">รายวิชา</td>
                                                  <td class="texttopic">
                                                      <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
                                                 
                                                </tr>
                                                    </table>        
                     
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
  <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-sort-alpha-asc"></i>

              <h3 class="box-title">รายวิชาที่ยังไม่ได้กำหนดเกณฑ์คะแนน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages">
                                            <div class="row">
<div class="col-md-3">
    <table border="0">
<tr>    
    <td >ปีประเมิน</td>
    <td >
        <asp:DropDownList ID="ddlDesYear" runat="server" CssClass="form-control select2" AutoPostBack="True" width="150"></asp:DropDownList></td>
  
      </tr>
      </table>
</div>
</div>
                                            <div class="row">
<div class="col-md-12">

              <asp:GridView ID="grdSubject" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="SubjectCode">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
                <asp:BoundField DataField="SubjectCode" HeaderText="รหัส" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
            <asp:BoundField DataField="SubjectName" HeaderText="ชื่อรายวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
          </asp:GridView>

                <asp:Label ID="lblNo" runat="server" Text="ท่านกำหนดครบหมดทุกวิชาที่เปิดฝึกแล้ว"></asp:Label>
    </div>
</div>   
  </div>
            <div class="box-footer clearfix">
           <table border="0" >
            <tr>
               
              <td class="mailbox-messages">
                  <asp:CheckBox ID="chkAll" runat="server" Text="หรือ เพิ่มทั้งหมดที่เหลือของปีการศึกษาที่เลือก" />
                </td>
              <td>
                                                       
                                          <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
              </td>
            </tr>
          </table>
            </div>
          </div>
    </section>
</asp:Content>
