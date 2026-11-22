<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Slide.aspx.vb" Inherits=".Slide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-car icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Slide Management
                                    <div class="page-title-subheading">จัดการภาพสไลด์ข่าวประชาสัมพันธ์หน้าแรก</div>
                                </div>
                            </div>
                        </div>
                    </div>

<section class="content">
   <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title"><asp:Label ID="lblTimeTable" runat="server" Text="จัดการภาพข่าวสไลด์"></asp:Label></h2> 
        </div>
        <div class="box-body">             
             <div class="row">
                   <div class="col-md-1">
          <div class="form-group">
            <label>ID</label>
               <asp:label ID="lblUID" runat="server"   CssClass="form-control text-center"></asp:label>
          </div>
        </div>

           <div class="col-md-11">
          <div class="form-group">
            <label>Title</label>
               <asp:TextBox ID="txtTitle" runat="server"   CssClass="form-control"></asp:TextBox>
          </div>
        </div>
        <div class="col-md-12">
          <div class="form-group">
                    <label>URL</label>
              <asp:TextBox ID="txtURL" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    
                     <div class="col-md-12">
          <div class="form-group">
            <label>ไฟล์ภาพ (ขนาดที่แนะนำ (กxย) 1190 x 420 px.)</label>
              <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tb10" 
                    ToolTip="เลือกไฟล์อัพโหลด" Width="395px" />
          </div>
        </div>
</div>     

 </div> 
        <div class="box-footer">
        <asp:Button ID="bttSave" runat="server" CssClass="btn btn-success"   Text="บันทึก"/>
        &nbsp;<asp:Button ID="bttCancel" runat="server" CssClass="btn btn-danger" Text="ยกเลิก" /> 
        </div> 
      </div>   

 <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการภาพสไลด์</h2>   
          <div class="box-tools pull-right"></div>         
     </div>   
 
        <div class="box-body">  
                <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover table-borderless"
                    BorderStyle="None" BorderWidth="0px" GridLines="Horizontal" ShowHeader="False" AllowPaging="True" PageSize="5" CellPadding="4"> 
                   
                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>

<table>
    <tr>
        <td> <asp:Image ID="img_valid"  ImageUrl='<%# "../" & DataBinder.Eval(Container.DataItem, "ImgStatus")  %>' runat="server"  ToolTip="สถานะ" /></td>
         <td><asp:Label ID="lblFile1" runat="server" Text='<%# Bind("title") %>'></asp:Label></td>
    </tr>
      <tr>
        <td colspan="2"> <a href='<%# DataBinder.Eval(Container.DataItem, "url") %>' target=_blank>                             
                                <asp:Image ID="imgIndex"  ImageUrl='<%# "../imageSlide/" & DataBinder.Eval(Container.DataItem, "ImgPath") %>' runat="server"   
                                    Height="60px" Width="200px" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                    BorderWidth="1px" />                                                            
                                </a></td> 
    </tr>
    <tr>
        <td colspan="2"> แก้ไขเมื่อ :   <asp:Label ID="Label4" runat="server" Text='<%# Bind("MWhen", "{0:dd/MM/yyyy}") %>'></asp:Label></td>
    </tr>
</table>
                                </ItemTemplate>
                        </asp:TemplateField>                   

<asp:TemplateField><ItemTemplate>
                                <asp:LinkButton ID="lnkOk" runat="server" CommandArgument='<%# Eval("UID")%>' 
                                    CssClass="btn btn-success" ForeColor="White" OnClick="ActiveItem" 
                                    OnClientClick="return confirm('คุณต้องการแสดงรายการนี้ !! ใช่หรือไม่?')" 
                                    Text="แสดง" Width="100"></asp:LinkButton>
                                <asp:LinkButton ID="lnkInActive" runat="server"
                                    CommandArgument = '<%# Eval("UID")%>'
                                 OnClientClick = "return confirm('คุณต้องการงดแสดงรายการนี้ !! ใช่หรือไม่?')"
                                Text = "งดแสดง" OnClick = "InActiveItem" CssClass="btn btn-warning" Width="100"></asp:LinkButton>
    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UID")%>' CssClass="btn btn-primary" Text="แก้ไข" Width="100"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDel" runat="server" CommandArgument='<%# Eval("UID")%>' 
                                    CssClass="btn btn-danger" ForeColor="White" OnClick="DeleteItem" 
                                    OnClientClick="return confirm('คุณต้องการลบรายการนี้ !! ใช่หรือไม่?')" 
                                    Text="ลบ" Width="100"></asp:LinkButton>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("StatusFlag")%>' />
                            
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="dc_pagination" HorizontalAlign="Center" />
                </asp:GridView>
  
   </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    
      </section>
     

</asp:Content>
