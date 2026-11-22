<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="MediaList.aspx.vb" Inherits="MediaList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>Media 
        <small>จัดการสื่อการสอน</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home></a></li>
        <li class="active">ดาวน์โหลด></li> <li class="active"> สื่อการสอน</li>
      </ol>
    </section>

      <section class="content">
                 
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการสื่อการสอน</h3>   
          <div class="box-tools pull-right"><asp:Button ID="cmdAdd" runat="server" CssClass="buttonlink" Text="อัพโหลดใหม่" Width="100px" />          
          </div>
               
       
             
                       
        </div>
        <div class="box-body">
  <div class="form-group">            
                  <b>ค้นหา :</b><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox> &nbsp;<asp:Button ID="cmdSearch" runat="server" CssClass="buttonSmall" Text="ค้นหา" Width="70px" />      

             
                                    
                       
</div>      
                 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="20">
                        <RowStyle BackColor="#F7F7F7" VerticalAlign="Top" />
                        <columns>
                            <asp:BoundField HeaderText="No." DataField="nRow">
                            <HeaderStyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="เรื่อง">
                                 <ItemTemplate>
                                     <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descriptions") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FilePath") %>'></asp:HyperLink>
                                 </ItemTemplate>
                            </asp:TemplateField>
                    <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                    <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา" />
                    <asp:BoundField DataField="SubjectName" HeaderText="ชื่อรายวิชา" />
                    <asp:BoundField DataField="Instructor" HeaderText="ผู้สอน" />
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
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
