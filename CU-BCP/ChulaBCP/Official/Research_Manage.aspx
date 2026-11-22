<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Research_Manage.aspx.vb" Inherits="Research_Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-bicycle icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>งานวิจัย
                                    <div class="page-title-subheading">จัดการข้อมูลงานวิจัย</div>
                                </div>
                            </div>
                        </div>
                    </div>

   
 <section class="content">          
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">ผลงานการวิจัย</h3>
             <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
                <div class="row">
              <div class="col-md-1">
                <div class="form-group">
                  <label>ปี</label>
                     <asp:TextBox ID="txtRYear" runat="server"  CssClass="form-control text-center"></asp:TextBox>
                <asp:HiddenField ID="hdfResearchID" runat="server" />
                </div>
              </div>
                <div class="col-md-3">
                <div class="form-group">
                  <label>ประเภทงานวิจัย</label>
                  <asp:DropDownList ID="ddlResearchType" runat="server" CssClass="form-control select2">                   
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>งานวิจัยด้าน</label>
  <asp:DropDownList ID="ddlResearchGroup" runat="server" CssClass="form-control select2">                   
                  </asp:DropDownList>
                     
                </div>
              </div>
   <div class="col-md-2">
                <div class="form-group">
                  <label>การนำเสนอ</label>
                     <asp:DropDownList ID="ddlResearchPresented" runat="server" CssClass="form-control select2" >
                     </asp:DropDownList>
                </div>
              </div>     
                      <div class="col-md-3">
                <div class="form-group">
                  <label>เจ้าของ/ผู้แต่ง </label>
                     <asp:DropDownList ID="ddlPerson" runat="server" CssClass="form-control select2">
                     </asp:DropDownList>
                </div>
              </div>   

                      <div class="col-md-12">
                <div class="form-group">
                  <label>ชื่องานวิจัย</label>
                      <asp:TextBox ID="txtRTitle" runat="server"   CssClass="form-control" >
                 </asp:TextBox>
                </div>
              </div>   
                     <div class="col-md-12">
                <div class="form-group">
                  <label>บทคัดย่อ</label>
                     <asp:FileUpload ID="FileUploadFile" runat="server" /><br />
                      <asp:HyperLink ID="hlnkAttachs" runat="server" Target="_blank">ไม่มีเอกสารแนบ</asp:HyperLink>
&nbsp;<asp:Button ID="cmdDelFile" runat="server" CssClass="btn btn-danger" Text="ลบเอกสาร" />
               
                </div>
              </div>   
                      <div class="col-md-12 text-center">
                <div class="form-group">
                  <label></label>
                     <asp:Button ID="cmdSaveResearch" runat="server" CssClass="btn btn-primary" Text="บันทึกงานวิจัย" />
                &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" Text="ล้าง/ยกเลิก" />
                </div>
              </div>   
                </div> 
        </div> 

     <div class="box-footer">
       
                
  <asp:Panel ID="pnAlert" runat="server" Width="100%">
                  <div class="alert alert-danger alert-dismissible">
                
                <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    <p><asp:Label ID="lblAlert" runat="server"></asp:Label></p>
              </div>
                   </asp:Panel>
       
     </div>

      </div>
      <!-- /.box -->   

                
             <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการงานวิจัย</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse"><i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">
            <table>
                <tr>
                    <td>ค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>&nbsp;<asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />
                    </td>
                </tr>              
               
            </table>

                                <asp:GridView ID="grdResearch" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99%" AllowPaging="True">
                                    <RowStyle BackColor="White" VerticalAlign="Top" />
                                    <columns>
                                        <asp:BoundField DataField="nRow" HeaderText="No.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="ชื่องานวิจัย">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TitleTH") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RYear" HeaderText="ปี">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PresentName" HeaderText="การนำเสนอ">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupName" HeaderText="ด้าน">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="ประเภท">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="แก้ไข">
                                            <itemtemplate>
                                                <asp:ImageButton ID="imgEditR" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-edit.png" />
                                            </itemtemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบ">
                                            <itemtemplate>
                                                <asp:ImageButton ID="imgDelR" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-delete.png" />
                                            </itemtemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </columns>
                                    <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <pagerstyle HorizontalAlign="Center" CssClass="dc_pagination" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <headerstyle CssClass="gridheader" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="#EBEBEB" />
                                </asp:GridView>
                         
             
            </div>
        <div class="box-footer">
        
        </div>
      </div>    
  </section>                  

</asp:Content>
