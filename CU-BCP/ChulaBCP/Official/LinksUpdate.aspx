<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSiteSimple.Master" CodeBehind="LinksUpdate.aspx.vb" Inherits=".LinksUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>Slid Management
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Slid</li>
      </ol>
    </section>

<section class="content">
   <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title"><asp:Label ID="lblTimeTable" runat="server" Text="จัดการภาพข่าวสไลด์"></asp:Label></h3>   
          <div class="box-tools pull-right">          
         
                      </div>            
                       
        </div>
        <div class="box-body">   
            
      <table class="style1">
          <tr>
              <td class="style5">
                  <asp:Label ID="Label3" runat="server" Text="ชื่อ:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtName" runat="server" CssClass="tb10" Width="350px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ControlToValidate="txtName" ErrorMessage="**" ForeColor="Red" 
                      ValidationGroup="lnk"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td class="style5">
                  <asp:Label ID="Label4" runat="server" Text="url:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtUrl" runat="server" CssClass="tb10" Width="350px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                      ControlToValidate="txtUrl" ErrorMessage="**" ForeColor="Red" 
                      ValidationGroup="lnk"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td class="style5">
                  <asp:Label ID="Label5" runat="server" Text="ไฟล์ภาพ:"></asp:Label>
              </td>
              <td>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tb10" 
                    ToolTip="เลือกไฟล์อัพโหลด" Width="395px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                      runat="server" ControlToValidate="FileUpload1" ErrorMessage="**" 
                      ForeColor="Red" ValidationGroup="lnk"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" 
                                Text="ขนาดที่แนะนำ (กxย) 200 x 60 px."></asp:Label>
                        </td>
          </tr>
          <tr>
              <td class="style5">
                  &nbsp;</td>
              <td>
                  <span class="panel-title" >
                  <span class="panel-title" style="text-align: right" >
    <asp:Button ID="bttSave" runat="server" CssClass="buttonUpdate" 
        Text="บันทึกการเปลี่ยนแปลง" ValidationGroup="lnk" />
                      <asp:Button ID="bttCancel" runat="server" CssClass="buttonOrange" 
        Text="ยกเลิก/กลับ" Font-Bold="False" PostBackUrl="Default.aspx" />

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
          <h2 class="box-title">รายการภาพสไลด์</h3>   
          <div class="box-tools pull-right"></div>         
     </div>   
 
        <div class="box-body">  
              <asp:GridView ID="dgvList" runat="server" CellPadding="4" ForeColor="#333333" 
          GridLines="None" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" PageSize="5" Width="100%">
          <Columns>

               <asp:TemplateField HeaderText="ชื่อ" ItemStyle-HorizontalAlign=Left ItemStyle-VerticalAlign=Top >
                 <ItemTemplate >
                  <asp:LinkButton ID="lnkDel0" runat="server" CommandArgument='<%# Eval("lnkID")%>' 
                                    CssClass="buttonRed" ForeColor="White" OnClick="DeleteItem" 
                                    OnClientClick="return confirm('คุณต้องการงดแสดงรายการนี้ !! ใช่หรือไม่?')" 
                                    Text="ลบ"></asp:LinkButton>
                              <a href='<%# DataBinder.Eval(Container.DataItem, "url") %>' target=_blank>
                              <asp:Label ID="lnkName" runat="server" Text='<%# Bind("lnkName") %>'></asp:Label>
                              </a>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="url" ItemStyle-HorizontalAlign=Left ItemStyle-VerticalAlign=Top >
                 <ItemTemplate >
                 <a href='<%# DataBinder.Eval(Container.DataItem, "url") %>' target=_blank>
                
                                <asp:Label ID="lblUrl" runat="server" Text='<%# Bind("url") %>'></asp:Label>
                 </a>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ภาพ" ItemStyle-HorizontalAlign=Left ItemStyle-VerticalAlign=Top >
                 <ItemTemplate >
                        <a href='<%# DataBinder.Eval(Container.DataItem, "url") %>' target=_blank>
                             <asp:Image ID="imgIndex"  ImageUrl='<%# Bind("ImagePath") %>' runat="server"  ToolTip='<%# Bind("lnkName") %>'  Height="60px" Width="200px" BorderWidth="1px" />
                        </a>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:TemplateField>
                               
          </Columns>

          <AlternatingRowStyle BackColor="White" />
          <EditRowStyle BackColor="#7C6F57" />
          <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
          <PagerStyle HorizontalAlign="Center" CssClass="dc_pagination" />
          <RowStyle BackColor="#E3EAEB" />
       
         
      </asp:GridView>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
          ConnectionString="<%$ ConnectionStrings:strConn %>" 
          SelectCommand="SELECT lnkID, lnkName, url, ImagePath FROM Links ORDER BY Orders">
      </asp:SqlDataSource>
             </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    
      </section>
</asp:Content>
