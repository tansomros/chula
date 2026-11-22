<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_Subject.aspx.vb" Inherits=".PC2_Subject" %>

<%@ Register Assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    
      <section class="content-header">
      <h1>จัดการวิชา (สำหรับ PC2)</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-plus-circle"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข วิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
                <asp:HiddenField ID="hdUID" runat="server" />
            </div>
            <div class="box-body"> 
                 <div class="row">

          <div class="col-md-3">
          <div class="form-group">
            <label>ชื่อภาษาไทย</label>
              <asp:TextBox ID="txtNameTH" runat="server" CssClass="form-control" placeholder="ชื่อระบบโรคภาษาไทย"></asp:TextBox> 
          </div>

        </div>

          <div class="col-md-3">
          <div class="form-group">
            <label>ชื่อภาษาอังกฤษ </label>
              <asp:TextBox ID="txtNameEN" runat="server" CssClass="form-control" placeholder="Subject Name (English)"></asp:TextBox> 
          </div>

        </div>
                       <div class="col-md-2">
          <div class="form-group">
            <label>Status</label> <br />
              <asp:CheckBox ID="chkStatus" runat="server" Text="Active" Checked="True" />
          </div>

        </div>
                       <div class="col-md-4">
          <div class="form-group">
            <label></label> <br />
               <asp:Button runat="server" Text="บันทึก" CssClass="btn btn-pink" Width="100px" ID="cmdSave"></asp:Button>
                <asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-pink" Width="100px" ID="cmdClear"></asp:Button>           
          </div>
        </div>
       </div>            
                
</div>
            <div class="box-footer clearfix text-center">           
               
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">วิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
       
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
       
        <tr>
          <td  align="left" valign="top">
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />                  
                </td>
          
              </tr>
          </table>

          </td>
      </tr>    
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="table table-hover" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
            <asp:BoundField DataField="NameTH" HeaderText="ชื่อ">
                <HeaderStyle HorizontalAlign="Left" />
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>

<asp:BoundField DataField="NameEN" HeaderText="Name">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
       
    </table>
                                                
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div> 
                       
    </section> 
</asp:Content>
