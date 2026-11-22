<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="NewsAdd2.aspx.vb" Inherits=".NewsAdd2" %>
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
                                <div>จัดการข่าวประชาสัมพันธ์
                                    <div class="page-title-subheading">จัดการข้อมูลเกี่ยวกับองค์กร บนหน้าเพจเว็บไซต์</div>
                                </div>
                            </div>
                        </div>
                    </div>      

<section class="content"> 
                   
     <div class="main-card mb-3 card">
        <div class="card-header">
          จัดการข่าวประชาสัมพันธ์        
        </div>
        <div class="card-body"> 

             <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>News ID</label>
                                            <asp:Label ID="lblNewsID" CssClass="form-control" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-11">
                                        <div class="form-group">
                                            <label>หัวข้อข่าว</label> 
                                            <asp:TextBox ID="txtHead" runat="server"   CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                

                                </div>
             <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>เนื้อหา</label>
                                           
                <dx:ASPxHtmlEditor ID="txtDetail" runat="server"   Width="98%" Theme="Default" Height="800px">  
                                            <SettingsDialogs>
                                                <InsertImageDialog>
                                                    <SettingsImageUpload>
                                                        <FileSystemSettings UploadFolder="~/images/news" />
                                                    </SettingsImageUpload>                                                   
                                                </InsertImageDialog>
                    </SettingsDialogs>
                                            </dx:ASPxHtmlEditor>
                                        </div>
                                    </div>
                 </div>
   <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>File manager</label>
                                            <dx:ASPxFileManager ID="ASPxFileManager1" runat="server"  ToolTip="อัพโหลดไฟล์ภาพ..." Width="99%">
                    <Settings RootFolder="~\images\news\" ThumbnailFolder="~\Thumb\" EnableMultiSelect="True" />
                    <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" 
                        AllowRename="True" />
                    <SettingsUpload>
                        <AdvancedModeSettings EnableMultiSelect="True">
                        </AdvancedModeSettings>
                    </SettingsUpload>
                </dx:ASPxFileManager>
                                        </div>
                                    </div>
                 </div>

             <div class="row">
    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>ภาพหน้าปก</label>
                                               <asp:FileUpload ID="FileUploadImage" runat="server" />    
                                            <br />
                                            <asp:Image ID="imgCover" runat="server" Width="200" />
                                        </div>
                                    </div>
                   <div class="col-md-6">
                                        <div class="form-group">
                                            <label>เอกสารแนบ</label>  
                                            <asp:FileUpload ID="FileUploadFile" runat="server" /><br />
                                           <asp:HyperLink ID="hlnkAttachs" runat="server" Target="_blank">ไม่มีเอกสารแนบ</asp:HyperLink>
&nbsp;<asp:Button ID="cmdDelFile" runat="server" CssClass="btn btn-danger" Text="ลบเอกสารแนบ" />
                <br />
              
                                        </div>
                                    </div>
             
                 </div>

            <div class="row">
                 <div class="col-md-12">
                                        <div class="form-group">
                                            <label>สถานะ</label>
    <asp:CheckBox ID="chkActive" runat="server"  Checked="true" Text="แสดงข่าว/Public" />

</div>
                                    </div>

                 </div>  

            
             </div> 
        <div class="card-footer">
       
                <asp:Button ID="bttSave" runat="server" CssClass="btn btn-success" 
                    Text="บันทึก" ValidationGroup="D" />
                </div> 
      </div>
    </section>
</asp:Content>
