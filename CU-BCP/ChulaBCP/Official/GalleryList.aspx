<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="GalleryList.aspx.vb" Inherits="GalleryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>ภาพกิจกรรม
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ภาพกิจกรรม</li>
      </ol>
    </section>

<section class="content">
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <div class="box-title">
              <table>
                 <tr>
            <td>ค้นหา</td>
            <td><asp:TextBox ID="txtSearch" runat="server"  ></asp:TextBox></td>
           
                    <td><asp:Button ID="cmdSearch" runat="server" CssClass="buttonSmall" Text="ค้นหา" Width="70px" />      

                        </td> 
                </tr>
            </table>  </div>   
          <div class="box-tools pull-right"><asp:Button ID="cmdNew" runat="server" CssClass="buttonlink" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>                          
        </div>      
       
      </div>
      <!-- /.box -->    
                     
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการภาพกิจกรรม</h3>   
          <div class="box-tools pull-right"><asp:Button ID="cmdAdd" runat="server" CssClass="buttonlink" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>
               
       
             
                       
        </div>
        <div class="box-body">
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="2" GridLines="None" Width="100%" AllowPaging="True" BorderStyle="None" CssClass="gridnormal" PageSize="20" CellSpacing="3">
                        <RowStyle BackColor="#FFFFFF" VerticalAlign="Top" CssClass="gridrow" />
                        <columns>
                            <asp:BoundField DataField="AlbumID" HeaderText="ID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AlbumName" HeaderText="ชื่ออัลบั้ม" >
                             <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AlbumID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                           <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AlbumID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="gridalternatingrow" />
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
