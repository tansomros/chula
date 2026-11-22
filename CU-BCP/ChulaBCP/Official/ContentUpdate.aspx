<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master"
  CodeBehind="ContentUpdate.aspx.vb" Inherits=".ContentUpdate" %>

    <%@ Register
    assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
        <%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
      namespace="DevExpress.Web" tagprefix="dx" %>
            <%@ Register
        assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
                <asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
                </asp:Content>
                <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div><asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label>
                                    <div class="page-title-subheading">จัดการข้อมูลเกี่ยวกับองค์กร บนหน้าเพจเว็บไซต์</div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <section class="content">
                        
                          
                        <div class="main-card mb-3 card">
                            <div class="card-header">                               
                                   เนื้อหา
                            </div>
                            <div class="card-body">
                                <div class="row">                              
                                <dx:ASPxHtmlEditor ID="txtDetail" runat="server" Height="500px" Width="100%" Theme="Default">
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
                            <div class="card-footer">
                                <asp:Button ID="bttSave" runat="server" CssClass="btn btn-success" Text="บันทึก" Width="100px" /> &nbsp;
                                <asp:Button ID="bttCancel" runat="server" CssClass="btn btn-danger" Text="ยกเลิก" Font-Bold="False" PostBackUrl="Dafault.aspx" />
                            </div>
                        </div>

                    </section>
                </asp:Content>