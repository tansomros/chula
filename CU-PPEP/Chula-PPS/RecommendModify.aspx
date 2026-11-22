<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RecommendModify.aspx.vb" Inherits=".RecommendModify" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <section class="content-header">
        <h1>คำแนะนำจากนิสิต
          <small></small>
        </h1>

    </section>

    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-info-circle"></i>

                <h3 class="box-title">คำแนะนำ
                    <asp:Label ID="lblHeadName" runat="server" Text="แหล่งฝึก" Font-Italic="False" Font-Underline="True"></asp:Label></h3>
                <asp:HiddenField ID="hdUID" runat="server" />
                <div class="box-tools pull-right">
                    <table width="100%">
                        <tr>
                            <td width="50"></td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                                        <table align="right">
                                            <tr>
                                                <td>Status :</td>
                                                <td align="left" width="70">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Open"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgPending" runat="server" ImageUrl="images/statusicon_pending_01.png" Width="24px" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgApprove" runat="server" ImageUrl="images/statusicon_approve_01.png" Width="24px" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgReject" runat="server" ImageUrl="images/statusicon_reject_01.png" Width="24px" />
                                                </td>
                                            </tr>
                                        </table>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imgPending" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="imgApprove" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="imgReject" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>นิสิตที่แนะนำ</label>
                            <asp:HiddenField ID="hdStdCode" runat="server" />
                            <asp:Label ID="lblStudentName" runat="server" CssClass="form-control text-center"></asp:Label>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>แหล่งฝึกที่เคยไปฝึกมา</label>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2"></asp:DropDownList>    
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>รายวิชา</label>
                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>                

                        <div class="col-md-12">
                            <div class="form-group">
                                <label>คำแนะนำสั้นๆ</label>
                                <asp:TextBox ID="txtShortDesc" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                         </div>

                         <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>รายละเอียด</label>
                                <dx:ASPxHtmlEditor ID="txtRecommend" runat="server" Width="99%" height="500" Theme="Office2010Blue">                       
                                      <SettingsImageUpload UploadFolder="~/imgUploads/imgRecommend/" UploadImageFolder="~/imgUploads/imgRecommend/">
 </SettingsImageUpload>
                                </dx:ASPxHtmlEditor>
                            </div>
                        </div>
                              </div>
                              <div class="row">

                        <div class="col-md-12 text-center">
                            <div class="form-group">
                                <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="บันทึก" Width="120px" />
                                <asp:Button ID="cmdDel" runat="server" CssClass="btn btn-danger" Text="ลบ" Width="120px" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer clearfix">
                </div>
            </div>
    </section>


</asp:Content>
