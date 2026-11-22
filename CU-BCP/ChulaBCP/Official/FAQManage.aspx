<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="FAQManage.aspx.vb" Inherits="FAQManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>ถาม-ตอบ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">จัดการคำถาม-ตอบ</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-warning">
        <div class="box-header with-border">
          <h2 class="box-title">เพิ่ม/แก้ไข คำถาม-ตอบ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
                  <table align="center"  class="table">         
          <tr>
              <td width="70">รหัส</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
          <tr>
            <td>หมวดหมู่</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <td>คำถาม</td>
            <td><asp:TextBox ID="txtQ" runat="server" Width="100%"></asp:TextBox></td>
          </tr>
          <tr>
            <td>คำตอบ</td>
            <td>
          <asp:TextBox ID="txtA" runat="server" Width="100%" Height="100px" TextMode="MultiLine"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
            <td>Active</td>
            <td>
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active / ใช้งาน" />
              </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="cmdSave" runat="server" CssClass="buttonlink" Text="บันทึก" Width="100px" />
              &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="buttonlink" Text="ยกเลิก" Width="100px" />
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
          <h2 class="box-title">รายการคำถาม-ตอบ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
  <div class="form-group">            
                  <b>ค้นหา :</b><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox> &nbsp;<asp:Button ID="cmdSearch" runat="server" CssClass="buttonSmall" Text="ค้นหา" Width="70px" />               
</div>      
                 
 <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True">
                        <RowStyle BackColor="#F7F7F7" VerticalAlign="Top" />
                        <columns>
                            <asp:BoundField HeaderText="No.">
                            <HeaderStyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Q" HeaderText="ถาม">
                            </asp:BoundField>
                            <asp:BoundField DataField="A" HeaderText="ตอบ">
                            <headerstyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" ImageUrl="~/Official/images/icon-ok.png" Visible='<%# ConvertStatusFlag2CHK(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                         
                                
                    <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ไม่พบข้อมูล" Width="100%"></asp:Label>
    </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->            

  </section>                  

</asp:Content>
