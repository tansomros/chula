<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DocumentManager.aspx.vb" Inherits=".DocumentManager" %>
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
                                    <i class="pe-7s-upload icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Document Manager
                                    <div class="page-title-subheading">จัดการเอกสารส่งงาน</div>
                                </div>
                            </div>
                        </div>
                    </div>      

<section class="content"> 
                   
     <div class="main-card mb-3 card">
        <div class="card-header">
          จัดการเอกสารส่งงาน
        </div>
        <div class="card-body">
             <div class="row">
                  <div class="col-md-2">
                      <div class="form-group">
                          <label>ปีการศึกษา</label>
                          <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2"  AutoPostBack="true"></asp:DropDownList>
                      </div>
                      </div> 
                  <div class="col-md-10">
                      <div class="form-group">
                          <label>เลือกเอกสาร</label>
                          <asp:DropDownList ID="ddlDocName" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                      </div>
                      </div> 
                 </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>File manager</label>
                        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server"  ToolTip="อัพโหลดไฟล์" Width="100%" Height="500px">
                    <Settings RootFolder="~\Documents\student\" ThumbnailFolder="~\Thumb\" EnableMultiSelect="True" />
                    <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" 
                        AllowRename="True" />
                            <SettingsFolders Visible="False" />
                            <SettingsToolbar ShowPath="False">
                            </SettingsToolbar>
                    <SettingsUpload>
                        <AdvancedModeSettings EnableMultiSelect="True">
                        </AdvancedModeSettings>
                    </SettingsUpload>
                </dx:ASPxFileManager>
                        </div>
                </div>
            </div>
            </div> 
        <div class="card-footer">       
        </div> 
      </div>
    </section>
</asp:Content>
