<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="CourseManage.aspx.vb" Inherits="CourseManage" %>

<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>หลักสูตร
                                    <div class="page-title-subheading">จัดการข้อมูลหลักสูตร บนหน้าเพจเว็บไซต์</div>
                                </div>
                            </div>
                        </div>
                    </div>  
      <section class="content">
                 
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">หลักสูตร</h3>   
           <div class="box-tools pull-right">
                <button type="button" class="btn btn-default btn-sm" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-default btn-sm" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>   
        </div>
        <div class="box-body">
            <div class="row"> 
    <div class="col-md-12">
           <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-hover" ForeColor="#333333" GridLines="None" Width="100%" PageSize="20">
                        <RowStyle BackColor="#F7F7F7" VerticalAlign="Top" />
                        <columns>
                            <asp:BoundField HeaderText="ID" DataField="CourseID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:BoundField>
                    <asp:BoundField DataField="ShortName_TH" HeaderText="ตัวย่อ" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                    <asp:BoundField DataField="Name_TH" HeaderText="ชื่อหลักสูตร" >
                             <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
    </div>
   
</div>

                 
                                             
    </div>     
      </div>       

          
        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">เพิ่ม/แก้ไข รายละเอียดหลักสูตร</h3>

          <div class="box-tools pull-right">
                <button type="button" class="btn btn-default btn-sm" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-default btn-sm" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>   
        </div>
        <div class="box-body">
            <div class="row"> 
    <div class="col-md-2">
        <div class="form-group">
            <label>Course ID</label>
              <asp:Label ID="lblCourseID" runat="server" CssClass="form-control text-center" ></asp:Label> 
        </div>
    </div>
           <div class="col-md-4">
        <div class="form-group">
            <label>ประเภท</label>
            <asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server">
                <asp:ListItem Value="1">หลักสูตรที่ปิดสอน</asp:ListItem>
                <asp:ListItem Value="2">หลักสูตรระยะสั้น</asp:ListItem>
            </asp:DropDownList>      
        </div>
    </div>
              <div class="col-md-3">
        <div class="form-group">
            <label>ชื่อย่อ (ภาษาไทย)</label>
              <asp:TextBox ID="txtShortNameTH" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
                    <div class="col-md-3">
        <div class="form-group">
            <label>ชื่อย่อ (ภาษาอังกฤษ)</label>
                     <asp:TextBox ID="txtShortNameEN" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>    
                <div class="col-md-6">
        <div class="form-group">
            <label>ชื่อหลักสูตร (ภาษาไทย)</label>
               <asp:TextBox ID="txtNameTH" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>
                <div class="col-md-6">
        <div class="form-group">
            <label>ชื่อหลักสูตร (ภาษาอังกฤษ)</label>
               <asp:TextBox ID="txtNameEN" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
                  
      <div class="col-md-12">
        <div class="form-group">
            <label>รายละเอียด (ภาษาไทย)</label>
                      <dx:ASPxHtmlEditor ID="txtDetailTH" runat="server" Height="500px" Width="100%" Theme="Default">
                                     <SettingsDialogs>
                                                <InsertImageDialog>
                                                    <SettingsImageUpload>
                                                        <FileSystemSettings UploadFolder="~/images/page" />
                                                    </SettingsImageUpload>                                                   
                                                </InsertImageDialog>
                    </SettingsDialogs>
                                </dx:ASPxHtmlEditor> 
        </div>
    </div>
                  <div class="col-md-12">
        <div class="form-group">
            <label>รายละเอียด (ภาษาอังกฤษ)</label>
                       <dx:ASPxHtmlEditor ID="txtDetailEN" runat="server" Height="500px" Width="100%" Theme="Default">
                                     <SettingsDialogs>
                                                <InsertImageDialog>
                                                    <SettingsImageUpload>
                                                        <FileSystemSettings UploadFolder="~/images/page" />
                                                    </SettingsImageUpload>
                                                </InsertImageDialog>
                    </SettingsDialogs>
                                </dx:ASPxHtmlEditor> 
        </div>
    </div>
                     <div class="col-md-6">
        <div class="form-group">
            <label>สถานะ</label>
            <asp:CheckBox ID="chkStatusFlag" Text="Active" runat="server" />
        </div>
    </div>
                <div class="col-md-6">
        <div class="form-group">      
                      <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" 
        Text="บันทึก" Width="100" />
    &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" 
        Text="ยกเลิก" Width="100" />
        </div>
    </div> 
</div>

        </div>       
      </div>
  </section>   
</asp:Content>
