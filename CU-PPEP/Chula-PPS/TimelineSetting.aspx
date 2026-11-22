<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TimelineSetting.aspx.vb" Inherits=".TimelineSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>Timeline Setting
     <small>กำหนดเวลาการทำงานระบบ</small> </h1>

    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <section class="col-lg-6 connectedSortable">
                <ul class="timeline">
                    <li class="time-label">
                        <span class="bg-red">ปีการศึกษาหลัก
                        </span>
                    </li>
                    <li>
                        <i class="fa fa-calendar bg-blue"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header no-border">กำหนดปีการศึกษาหลัก</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEduYear" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pt-2">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveYear" CssClass="btn btn-primary" runat="server" Text="Save" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="time-label">
                        <span class="bg-navy">ปีการศึกษาสำหรับการประเมิน
                        </span>
                    </li>
                    <li>

                        <i class="fa fa-calendar bg-lime"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header">กำหนดปีการศึกษาสำหรับการประเมิน</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtYearAssm" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pt-2">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveYearAssm" CssClass="btn btn-primary" runat="server" Text="Save" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>

                    <li class="time-label">
                        <span class="bg-green">นิสิตเลือกแหล่งฝึก
                        </span>
                    </li>
                    <li>

                        <i class="fa fa-registered bg-purple"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header">กำหนดวันที่เปิด-ปิด ให้นิสิตเลือกแหล่งฝึก</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ตั้งแต่วันที่</label>
                                            <asp:TextBox ID="txtReg_Start" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ถึง</label>
                                            <asp:TextBox ID="txtReg_End" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pt-4">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveSelect" CssClass="btn bg-purple" runat="server" Text="Save" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="time-label">
                        <span class="bg-purple">ประกาศผลการคัดเลือก
                        </span>
                    </li>
                    <li>

                        <i class="fa fa-bullhorn bg-yellow"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header">วันที่ประกาศผลการคัดเลือกแหล่งฝึก</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAnnouncedDate" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pt-2">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveAnnounced" CssClass="btn btn-warning" runat="server" Text="Save" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>

            </section>
            <section class="col-lg-6 connectedSortable">
                <ul class="timeline">
                    <li class="time-label">
                        <span class="bg-maroon">อาจารย์ประเมินนิสิต
                        </span>
                    </li>
                    <li>
                        <i class="fa fa-calendar bg-green"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header">กำหนดวันที่เปิด-ปิด ให้อาจารย์ประจำรายวิชาประเมินนิสิต</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ตั้งแต่วันที่</label>
                                            <asp:TextBox ID="txtAssess_Start" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ถึง</label>
                                            <asp:TextBox ID="txtAssess_End" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pt-2">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveAssessment" CssClass="btn bg-green" runat="server" Text="Save" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>

                    <li class="time-label">
                        <span class="bg-yellow">แหล่งฝึกประเมินนิสิต
                        </span>
                    </li>
                    <li>
                        <i class="fa fa-calendar bg-purple"></i>
                        <div class="timeline-item">
                            <h3 class="timeline-header">กำหนดวันที่เปิด-ปิด ให้แหล่งฝึกประเมินนิสิต</h3>
                            <div class="timeline-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ตั้งแต่วันที่</label>
                                            <asp:TextBox ID="txtAssessLoc_Start" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ถึง</label>
                                            <asp:TextBox ID="txtAssessLoc_End" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pt-2">
                                        <div class="form-group">
                                            <asp:Button ID="cmdSaveLocAssessment" CssClass="btn bg-green" runat="server" Text="Save" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                   

                </ul>
            </section>
        </div>
    </section>
</asp:Content>
