<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="SlideManage.aspx.vb" Inherits=".SlideManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1>จัดการภาพข่าวสไลด์ 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Slide manage</li>
      </ol>
    </section>

<section class="content">
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">จัดการภาพข่าวสไลด์</h3>   
          <div class="box-tools pull-right">      
          </div>  
        </div>
        <div class="box-body">         
           <table class="style1">
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label5" runat="server" Text="เรื่อง:"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtName" ErrorMessage="**" ForeColor="Red" 
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" BackColor="#FFFFCC" Height="28px" 
                                Width="450px" CssClass="tb10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label8" runat="server" Text="URL:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName0" runat="server" BackColor="#FFFFCC" Height="28px" 
                                Width="450px" CssClass="tb10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label6" runat="server" Text="ไฟล์ภาพ:"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="FileUpload1" ErrorMessage="**" ForeColor="Red" 
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tb10" 
                    ToolTip="เลือกไฟล์อัพโหลด" Width="395px" />
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" 
                                Text="ขนาดที่แนะนำ (กxย) 1190 x 420 px."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            &nbsp;</td>
                        <td>
                            <span class="panel-title" >
                  <span class="panel-title" style="text-align: right" >


    <asp:Button ID="bttSave" runat="server" CssClass="buttonOrange" 
        Text="บันทึกการเปลี่ยนแปลง" ValidationGroup="A" Font-Size="Small" />
                  &nbsp;<asp:Button ID="bttCancel" runat="server" CssClass="buttonRed" 
        Text="ยกเลิก/กลับ" Font-Bold="False" PostBackUrl="~/Default.aspx" Font-Size="Small" />
                            </span>
  
  </span>
                        </td>
                    </tr>
    </table>

             </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    

      <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการสไลด์ทั้งหมด</h3>   
          <div class="box-tools pull-right">      
          </div>  
        </div>
        <div class="box-body">
  
                <asp:GridView ID="grdSlide" runat="server" AutoGenerateColumns="False" 
                    BorderStyle="None" BorderWidth="0px" GridLines="None" ShowHeader="False"> 
                   
                    <Columns>

                     <asp:TemplateField ItemStyle-HorizontalAlign=Left ItemStyle-VerticalAlign=Top >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkOk" runat="server" CommandArgument='<%# Eval("id")%>' 
                                    CssClass="buttonUpdate" ForeColor="White" OnClick="IsActiveItem" 
                                    OnClientClick="return confirm('คุณต้องการแสดงรายการนี้ !! ใช่หรือไม่?')" 
                                    Text="แสดง"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDel" runat="server"
                                    CommandArgument = '<%# Eval("id")%>'
                                 OnClientClick = "return confirm('คุณต้องการงดแสดงรายการนี้ !! ใช่หรือไม่?')"
                                Text = "งดแสดง" OnClick = "DeleteVN" CssClass=buttonRed ForeColor=White></asp:LinkButton>
                                <asp:LinkButton ID="lnkDel0" runat="server" CommandArgument='<%# Eval("id")%>' 
                                    CssClass="buttonRed" ForeColor="White" OnClick="DeleteVN_a" 
                                    OnClientClick="return confirm('คุณต้องการงดแสดงรายการนี้ !! ใช่หรือไม่?')" 
                                    Text="ลบ"></asp:LinkButton>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("IsActive")%>' />
                            </ItemTemplate>
       
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
       
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Image ID="img_valid"  ImageUrl='<%# Bind("IaActive_img") %>' runat="server"  ToolTip="การแสดง..." />
                                <a href='<%# DataBinder.Eval(Container.DataItem, "url") %>' target=_blank>
                                   <asp:Label ID="lblFile1" runat="server" Text='<%# Bind("title_th") %>'></asp:Label><br />
                                <asp:Image ID="imgIndex"  ImageUrl='<%# Bind("img_path") %>' runat="server"   
                                    Height="60" Width="200" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                    BorderWidth="1px" />
                                                            
                                </a>
                                <br /><asp:Label ID="Label4" runat="server" Text='<%# Bind("MWhen", "{0:dd/MM/yyyy}") %>' Font-Size="Small" Font-Italic="True"></asp:Label>
                                   
                                <hr />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
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
