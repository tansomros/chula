<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LogfilesByUser.aspx.vb" Inherits=".LogfilesByUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link rel="stylesheet" type="text/css" href="css/pagestyles.css"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

<section class="content-header">
      <h1>ตรวจสอบประวัติการใช้งานของ User</h1>   
    </section>

<section class="content">  
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                         <table border="0" align="center" cellpadding="0" cellspacing="2" width="50%">
      <tr>
        <td width="100">เลือกผู้ใช้ </td>
<td>
                                               
                                               <asp:DropDownList ID="ddlUserID"  CssClass="form-control select2"
                runat="server" > </asp:DropDownList>
                                             </td>
        <td class="text-left">                          </td>
      </tr>
      <tr>
        <td>วันที่ตั้งแต่</td>
        <td>
            <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
          </td>
        <td>รูปแบบวันที่ </td>
      </tr>
      <tr>
        <td>ถึง</td>
        <td>
            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
          </td>
        <td>dd/mm/yyyy ปี พ.ศ.</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td> <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="100px" Text="ค้นหา"></asp:Button>
          </td>
        <td>&nbsp;</td>
      </tr>
      </table>                           
</div> 
       
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการใช้งานระบบ</h3>
             
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
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="table txtcontent" 
                             Font-Bold="False" PageSize="20">
                        <RowStyle BackColor="#ffffff" />
                        <columns>
                            <asp:BoundField HeaderText="No." DataField="LogID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                            </asp:BoundField>
                            <asp:BoundField DataField="Username" HeaderText="Username">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        <asp:BoundField DataField="Work_Date" HeaderText="วันที่" />
                            <asp:BoundField DataField="Act_Type" HeaderText="ประเภท" />
                            <asp:BoundField DataField="Descrp" HeaderText="คำอธิบาย" />
                            <asp:BoundField DataField="Remark" HeaderText="หมายเหตุ" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="#f7f7f7" />
                     </asp:GridView>                 
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
     
 
</asp:Content>
