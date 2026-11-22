<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="LocationList.aspx.vb" Inherits=".LocationList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">        
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>แหล่งฝึก
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
                    </div>

<section class="content">                 
  <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-home icon-gradient bg-success">
            </i>ข้อมูลแหล่งฝึก
            <div class="btn-actions-pane-right">
                <a href="Location?m=loc" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มแหล่งฝึกใหม่</a>
            </div>
        </div>
        <div class="card-body"> 
              <table border="0">
            <tr>
              <td width="60">ประเภท</td>
              <td >
                  <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-control select2">
                  </asp:DropDownList>
                </td>
              <td width="80" class="text-center" >ชื่อแหล่งฝึก</td>
              <td ><asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" Width="200px"></asp:TextBox></td>
              <td ><asp:Button ID="cmdFind" runat="server" CssClass="btn btn-primary" Text="ค้นหา" Width="60px" /></td>                
            </tr>
            
         </table> 
              <asp:GridView ID="grdData" CssClass="table table-hover"
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />                      </asp:BoundField>
            <asp:BoundField DataField="LocationID" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                <ItemStyle HorizontalAlign="Center" />                </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภทแหล่งฝึก" />
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' ImageUrl="images/view.png" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
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
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</section>   
    
</asp:Content>
