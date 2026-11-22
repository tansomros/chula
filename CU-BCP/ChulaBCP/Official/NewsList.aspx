<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="NewsList.aspx.vb" Inherits="NewsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-news-paper icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>News & Article
                                    <div class="page-title-subheading">จัดการข่าวประชาสัมพันธ์ และบทความวิชาการ</div>
                                </div>
                            </div>
                        </div>
</div>
<section class="content">                 
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">
              <asp:Label ID="lblCategoryName" runat="server" Text="รายการข่าวประชาสัมพันธ์"></asp:Label></h2>   
          <div class="box-tools pull-right"><asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-success" Text="เพิ่มใหม่" Width="100px" />          
          </div>
        </div>
        <div class="box-body">
  <div class="form-group">            
                  <b>ค้นหา :</b><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox> &nbsp;<asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="70px" /> 
</div>        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="20" CssClass="table table-hover">
                        <RowStyle BackColor="#F7F7F7" VerticalAlign="Top" />
                        <columns>
                            <asp:BoundField HeaderText="ID" DataField="NewsID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="เรื่อง">
                                 <ItemTemplate>
                                     <asp:HyperLink ID="hlnkNews" runat="server"  target="_blank" Text='<%# DataBinder.Eval(Container.DataItem, "NewsHead") %>'  NavigateUrl='<%# "../" & DataBinder.Eval(Container.DataItem, "NewsLink") %>'></asp:HyperLink>
                                 </ItemTemplate>
                            </asp:TemplateField>
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NewsID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NewsID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                                
    </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->            

  </section>                  

</asp:Content>
