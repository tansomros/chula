<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentPrint.aspx.vb" Inherits=".DocumentPrint" MasterPageFile="~/Site.Master"  %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<section class="content-header">
      <h1>ตั้งค่าเอกสาร/บันทึกข้อความ/จดหมาย</h1>   
    </section>

<section class="content"> 
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">ตั้งค่าเอกสาร/บันทึกข้อความ/จดหมาย</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    <table>
                  <tr>
                      <td width="100">เลือกจดหมาย :</td>
                      <td >
                          <asp:DropDownList ID="ddlDocument" runat="server" AutoPostBack="True" 
                                                          Width="99%" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
                  </tr>
                  <tr>
                      <td>ค้นหา :</td>
                      <td >
                          <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td >
      <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-pink" Text="ค้นหา" Width="100px" />
                      </td>
                  </tr>
              </table>                                       
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 <div class="box box-pink">

            <div class="box-header">
              <i class="fa fa-filter"></i>
              <h3 class="box-title">รายการเอกสาร/บันทึกข้อความ/จดหมาย ทั้งหมด <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;รายการ</h3>
             
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
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:BoundField HeaderText="No." DataField="RunningNumber">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก" />
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
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
    </section>           

</asp:Content>