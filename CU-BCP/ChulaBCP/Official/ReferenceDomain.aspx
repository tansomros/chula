<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="ReferenceDomain.aspx.vb" Inherits="ReferenceDomain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>Reference Domain
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Reference Domain</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-primary">
        <div class="box-header with-border">
          <h2 class="box-title">เพิ่ม/แก้ไข Reference Domain</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
                  <table align="center"  class="table50">         
          <tr>
              <td width="50">UID</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
          <tr>
              <td width="50">Code</td>
              <td>
          <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
            <td>Description</td>
            <td><asp:TextBox ID="txtDescriptions" runat="server" Width="50%"></asp:TextBox></td>
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
          <h2 class="box-title">Reference Domain</h3>

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
                 
 <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="Code">
                        <RowStyle BackColor="White"  VerticalAlign="Top" />
                        <columns>
                            <asp:BoundField HeaderText="UID" DataField="UID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="Code">
                            <headerstyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Descriptions" HeaderText="Descriptions">
                            <headerstyle HorizontalAlign="Left" />
                            <itemstyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
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
                        <AlternatingRowStyle BackColor="#EBEBEB" />
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
