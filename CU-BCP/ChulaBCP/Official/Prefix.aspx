<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Prefix.aspx.vb" Inherits=".Prefix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">   
     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-user-female icon-gradient bg-primary"></i>
                                </div>
                                <div>Prefix
                                    <div class="page-title-subheading"> จัดการคำนำหน้าชื่อ</div>
                                </div>
                            </div>
                        </div>
                    </div>    
<section class="content">
      <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">เพิ่ม/แก้ไข</h2>   
          <div class="box-tools pull-right">          
          </div>      
        </div>
        <div class="box-body">    
            
             <div class="row">
              <div class="col-md-1">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="form-control text-center" ID="lblID" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-4">
                <div class="form-group">
                  <label>คำนำหน้าชื่อ</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass  ="form-control"    ></asp:TextBox>
                </div>
              </div>
 <div class="col-md-4">
                <div class="form-group">
                  <label>ใช้สำหรับ</label><br />
                    <asp:CheckBox ID="chkStudent" runat="server" Text="นิสิต" />
                                                        <asp:CheckBox ID="chkTeacher" runat="server" Text="อาจารย์" />
                                                        <asp:CheckBox ID="chkPreceptor" runat="server" Text="อาจารย์แหล่งฝึก" />
                                                          <asp:CheckBox ID="chkStaff" runat="server" Text="บุคลากร" /> 
                </div>
              </div>

                 <div class="col-md-1">
                <div class="form-group">
                  <label>Status</label>          <br />
                    <asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                                                            Checked="True" />
                </div>
              </div>
                  <div class="col-md-2">
                <div class="form-group">
                  <label></label>
                    <br />
                      <asp:Button ID="cmdSave" runat="server" text="Save" CssClass="btn btn-primary"></asp:Button>
                                          <asp:Button ID="cmdClear" runat="server" 
                                                        text="Cancel" CssClass="btn btn-secondary"></asp:Button>
                </div>
              </div>

                </div>  
                 
         </div>
      </div>
    
      <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการ</h2>   
          <div class="box-tools pull-right">          
          </div>      
        </div>
        <div class="box-body">  
            <table border="0" >
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >&nbsp;
                  <asp:Button ID="cmdFind" runat="server" text="ค้นหา" CssClass="btn btn-warning" />                  
                </td>
            </tr>
            
          </table>

 <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="ID" DataField="PrefixID">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้าชื่อ">
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PrefixID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PrefixID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
           </div>
          </div>
    
  </section>  
</asp:Content>
