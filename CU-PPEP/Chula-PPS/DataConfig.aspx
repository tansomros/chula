<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataConfig.aspx.vb"
  Inherits=".DataConfig" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title> </title>
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>Data Configuration Controls</h1>
    </section>

    <section class="content">

      <div class="box box-pink">

        <div class="box-header">
          <i class="fa fa-filter"></i>

          <h3 class="box-title">จัดการค่าเริ่มต้นอื่นๆ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-1">
              <div class="form-group">
                <label>Code</label>
                <asp:TextBox ID="txtCode" runat="server" cssclass="form-control text-center" ReadOnly="true"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-group">
                <label>Name</label>
                   <asp:TextBox ID="txtName" runat="server" cssclass="form-control text-center" ReadOnly="true"></asp:TextBox> 
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-group">
                <label>Value</label>
                <asp:TextBox ID="txtValues" runat="server" cssclass="form-control text-center"></asp:TextBox>
              </div>
            </div>          
            <div class="col-md-3">
              <div class="form-group">
                <label>คำอธิบาย</label>
                <asp:TextBox ID="txtDesc" runat="server" cssclass="form-control text-center"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-group">
                <br />
                <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
                <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Width="100" Text="ยกเลิก"></asp:Button>
              </div>
            </div>
          </div>
        </div>
        <div class="box-footer clearfix">
        </div>
      </div>
      <div class="box box-success">
        <div class="box-header">
          <i class="fa fa-list"></i>
          <h3 class="box-title">Configuration List</h3>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
          <asp:GridView ID="grdData" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
            CssClass="table table-hover" AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
              <asp:BoundField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
              </asp:BoundField>
              <asp:BoundField DataField="Name" HeaderText="Name">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="Description" HeaderText="คำอธิบาย">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="ValueConfig" HeaderText="Value">
              </asp:BoundField>
              <asp:BoundField DataField="CodeConfig" HeaderText="Code">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
              </asp:BoundField>
              <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                  <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"
                    CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>

        </div>
        <div class="box-footer clearfix">

        </div>
      </div>

    </section>
  </asp:Content>