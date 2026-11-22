<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="SubjectModify.aspx.vb"
    Inherits=".SubjectModify" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">   
        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-bookmarks icon-gradient bg-success"></i>
                        </div>
                        <div>Course Syllabus Management
                            <div class="page-title-subheading">จัดการรายละเอียดประมวลรายวิชา </div>
                        </div>
                    </div>
                </div>
            </div>


            <section class="content">
                <div class="row">
                    <section class="col-lg-12 connectedSortable">
                        <div class="main-card mb-3 card">
                            <div class="card-header">ข้อมูลประมวลรายวิชา<asp:HiddenField ID="hdSubjectUID" runat="server" />
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>รหัสวิชา</label>
                                            <asp:TextBox ID="txtCode" runat="server" cssclass="form-control text-center" placeholder="Code"></asp:TextBox>
                                        </div>
                                    </div>                                       
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>ชื่อภาษาไทย</label>
                                            <asp:TextBox ID="txtNameTH" runat="server" cssclass="form-control" placeholder="ชื่อรายวิชาภาษาไทย"></asp:TextBox>
                                        </div>

                                    </div>
                                     <div class="col-md-5">
                                        <div class="form-group">
                                            <label>ชื่อภาษาอังกฤษ</label>
                                            <asp:TextBox ID="txtNameEN" runat="server" cssclass="form-control" placeholder="ชื่อรายวิชาภาษาอังกฤษ"></asp:TextBox>
                                        </div>

                                    </div>
                                    
                                     </div> 
                                     <div class="row"> 
                                          <div class="col-md-2">
                                        <div class="form-group">
                                            <label>หน่วยกิต</label>
                                            <asp:TextBox ID="txtUnit" runat="server" cssclass="form-control text-center" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                          <div class="col-md-2">
                                        <div class="form-group">
                                            <label>ประเภทวิชา</label>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="C">CREDIT</asp:ListItem>
                                                <asp:ListItem Value="A">AUDIT</asp:ListItem>
                                                <asp:ListItem Value="N">Non-CREDIT</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                            <div class="col-md-3">
                                        <div class="form-group">
                                            <label>ไฟล์เอกสาร (PDF)</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="80%" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label></label>
                                            <asp:HyperLink ID="hlnkDoc" runat="server" Target="_blank"></asp:HyperLink>
                                        </div>
                                    </div>

                                          <div class="col-md-2">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:CheckBox ID="chkStatus" cssclass="form-control no-border" Checked="true" runat="server" Text="ใช้งาน" />
                        </div>
                    </div>

                               </div> 
                            </div>
                                                               

                            <div class="d-block text-right card-footer">

                            </div>
                        </div>
                    </section>
                </div>        

                <div class="row text-center">                   
                    <div class="col-md-12">
                        <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="Save" Width="120px" />
                        <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete"  Width="120px" />
                    </div>
                </div>
            </section>
        </asp:Content>