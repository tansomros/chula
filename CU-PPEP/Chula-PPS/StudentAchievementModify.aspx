<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentAchievementModify.aspx.vb" Inherits=".StudentAchievementModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/student-achievement.css?v=6" />
    <script type="text/javascript">
        $(function () {
            if ($.fn.datepicker) {
                var d = new Date();
                var todayTh = ('0' + d.getDate()).slice(-2) + '/' + ('0' + (d.getMonth() + 1)).slice(-2) + '/' + (d.getFullYear() + 543);
                $('.js-datepicker-th').datepicker({
                    format: 'dd/mm/yyyy',
                    dateFormat: 'dd/mm/yyyy',
                    language: 'th-th',
                    isBuddhist: true,
                    defaultDate: todayTh,
                    autoclose: true
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ach-page ach-page-narrow">
        <!-- Hero Header -->
        <div class="ach-hero">
            <div class="ach-hero-left">
                <div class="ach-hero-icon">
                    <i class="fa fa-trophy"></i>
                </div>
                <div>
                    <h1 class="ach-hero-title">
                        <asp:Label ID="lblPageTitle" runat="server" Text="บันทึกผลงานนิสิต"></asp:Label>
                        <asp:Label ID="lblModeBadge" runat="server" CssClass="ach-mode-badge" Text="เพิ่มรายการใหม่"></asp:Label>
                    </h1>
                    <p class="ach-hero-subtitle">
                        <asp:Label ID="lblBoxTitle" runat="server" Text="กรอกข้อมูลการแข่งขัน การเข้าร่วมกิจกรรม ทุนสนับสนุน และรางวัลที่ได้รับ"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="ach-hero-actions">
                <asp:HyperLink ID="hlnkBackTop" runat="server" CssClass="ach-btn ach-btn-secondary">
                    <i class="fa fa-arrow-left"></i> กลับหน้ารายการผลงาน
                </asp:HyperLink>
            </div>
        </div>

        <asp:HiddenField ID="hdAchievementId" runat="server" Value="0" />

        <!-- Admin Student Lookup (visible only when Admin adds without ?stdcode) -->
        <div id="pnStudentInput" runat="server" class="ach-card" visible="false">
            <div class="ach-card-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group" style="margin-bottom:0;">
                            <label>ระบุรหัสนิสิต <span class="ach-star-red">*</span></label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStudentCodeInput" runat="server" CssClass="form-control" MaxLength="20" placeholder="เช่น 6533000133" style="border-radius:10px 0 0 10px !important;"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="cmdLookupStudent" runat="server" CssClass="ach-btn ach-btn-search" style="border-radius:0 10px 10px 0 !important; height:39px;">
                                        <i class="fa fa-search"></i> ดึงข้อมูลนิสิต
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Student Profile Strip -->
        <div id="pnStudentReadOnly" runat="server" class="ach-profile-strip">
            <div class="ach-profile-identity">
                <div class="ach-profile-avatar">
                    <i class="fa fa-user"></i>
                </div>
                <div>
                    <div class="ach-profile-name">
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div class="ach-profile-meta">
                        <span class="ach-meta-chip ach-meta-chip-code">
                            <i class="fa fa-id-badge"></i>
                            <asp:Label ID="lblStudentCode" runat="server"></asp:Label>
                        </span>
                        <span class="ach-meta-chip">
                            <i class="fa fa-flask"></i>
                            <asp:Label ID="lblMajorName" runat="server"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Main Form Card -->
        <div class="ach-card">
            <div class="ach-card-body">
                <!-- Section 1 -->
                <div class="ach-section">
                    <div class="ach-section-head">
                        <span class="ach-section-num">1</span>
                        <h4 class="ach-section-title">ข้อมูลทั่วไปและช่วงเวลา</h4>
                        <span class="ach-section-hint">ช่องที่มี <span class="ach-star-red">*</span> จำเป็นต้องระบุ</span>
                    </div>

                    <div class="row">
                        <div class="col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>ปีการศึกษา (พ.ศ.) <span class="ach-star-red">*</span></label>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>ชั้นปีที่ศึกษาขณะได้รับผลงาน <span class="ach-star-red">*</span></label>
                                <asp:DropDownList ID="ddlStudyYear" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="1">ชั้นปีที่ 1</asp:ListItem>
                                    <asp:ListItem Value="2">ชั้นปีที่ 2</asp:ListItem>
                                    <asp:ListItem Value="3">ชั้นปีที่ 3</asp:ListItem>
                                    <asp:ListItem Value="4">ชั้นปีที่ 4</asp:ListItem>
                                    <asp:ListItem Value="5">ชั้นปีที่ 5</asp:ListItem>
                                    <asp:ListItem Value="6">ชั้นปีที่ 6</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>ประเภทผลงาน / กิจกรรม <span class="ach-star-red">*</span></label>
                                <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="การแข่งขันทางวิชาการ / วิชาชีพ">การแข่งขันทางวิชาการ / วิชาชีพ</asp:ListItem>
                                    <asp:ListItem Value="การนำเสนอผลงานวิจัย / วิชาการ">การนำเสนอผลงานวิจัย / วิชาการ</asp:ListItem>
                                    <asp:ListItem Value="รางวัลเชิดชูเกียรติ / เยาวชนดีเด่น">รางวัลเชิดชูเกียรติ / เยาวชนดีเด่น</asp:ListItem>
                                    <asp:ListItem Value="ทุนการศึกษา / ทุนสนับสนุน">ทุนการศึกษา / ทุนสนับสนุน</asp:ListItem>
                                    <asp:ListItem Value="กิจกรรมเสริมหลักสูตร / จิตอาสา / ผู้นำนิสิต">กิจกรรมเสริมหลักสูตร / จิตอาสา / ผู้นำนิสิต</asp:ListItem>
                                    <asp:ListItem Value="กีฬา / ศิลปวัฒนธรรม">กีฬา / ศิลปวัฒนธรรม</asp:ListItem>
                                    <asp:ListItem Value="อื่นๆ">อื่นๆ</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>วัน/เดือน/ปีที่จัดงาน <span class="ach-star-red">*</span></label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtEventDate" runat="server" CssClass="form-control js-datepicker-th" MaxLength="10" placeholder="วว/ดด/ปปปป (พ.ศ.)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Section 2 -->
                <div class="ach-section">
                    <div class="ach-section-head">
                        <span class="ach-section-num">2</span>
                        <h4 class="ach-section-title">รายละเอียดงานแข่งขัน รางวัล และทุนสนับสนุน</h4>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ชื่องานแข่งขัน / โครงการ / กิจกรรมที่เข้าร่วม (ภาษาไทย) <span class="ach-star-red">*</span></label>
                                <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" MaxLength="500" placeholder="ระบุชื่องานแข่งขัน โครงการ หรือกิจกรรมที่เข้าร่วม"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ชื่องานแข่งขัน / กิจกรรมที่เข้าร่วม (ภาษาอังกฤษ - ถ้ามี)</label>
                                <asp:TextBox ID="txtEventNameEn" runat="server" CssClass="form-control" MaxLength="500" placeholder="Competition / Event Name in English (Optional)"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>รางวัลที่ได้รับ / ผลการเข้าร่วม (ภาษาไทย) <span class="ach-star-red">*</span></label>
                                <asp:TextBox ID="txtAwardName" runat="server" CssClass="form-control" MaxLength="300" placeholder="เช่น รางวัลชนะเลิศ, รองชนะเลิศอันดับ 1, เหรียญทอง, เข้าร่วมการแข่งขัน"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>รางวัลที่ได้รับ / ผลการเข้าร่วม (ภาษาอังกฤษ - ถ้ามี)</label>
                                <asp:TextBox ID="txtAwardNameEn" runat="server" CssClass="form-control" MaxLength="300" placeholder="e.g. First Prize, Gold Medal, Participant (Optional)"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>หน่วยงานผู้จัดงาน / สถานที่จัดงาน</label>
                                <asp:TextBox ID="txtOrganizerName" runat="server" CssClass="form-control" MaxLength="300" placeholder="เช่น สมาคมนิสิตนักศึกษาเภสัชศาสตร์แห่งประเทศไทย, จุฬาลงกรณ์มหาวิทยาลัย"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>จำนวนเงินสนับสนุน / ทุนที่ได้รับ (บาท)</label>
                                <div class="input-group">
                                    <div class="input-group-addon">฿</div>
                                    <asp:TextBox ID="txtGrantAmount" runat="server" CssClass="form-control" MaxLength="15" onkeypress="AllowOnlyDouble()" placeholder="0.00 (เว้นว่างได้หากไม่มีเงินสนับสนุน/ทุน)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" style="margin-bottom:0;">
                                <label>รายละเอียดเพิ่มเติม / หมายเหตุ</label>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" placeholder="ระบุรายละเอียดเพิ่มเติม เช่น ชื่อทีม อาจารย์ที่ปรึกษากิจกรรม หรือระดับของการแข่งขัน (ระดับชาติ/นานาชาติ)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Section 3 -->
                <div class="ach-section">
                    <div class="ach-section-head">
                        <span class="ach-section-num">3</span>
                        <h4 class="ach-section-title">การยืนยันความถูกต้องของข้อมูล</h4>
                    </div>

                    <div class="ach-confirm-card">
                        <div class="ach-confirm-icon">
                            <i class="fa fa-shield"></i>
                        </div>
                        <div>
                            <asp:CheckBox ID="chkSelfConfirm" runat="server" Checked="True"
                                Text="&nbsp;ข้าพเจ้าขอรับรองและยืนยันด้วยตนเองว่าข้อมูลผลงานและรางวัลข้างต้นเป็นความจริงทุกประการ (ไม่ต้องแนบไฟล์เกียรติบัตร)" />
                        </div>
                    </div>
                </div>

                <!-- Action Bar -->
                <div class="ach-form-actions">
                    <div>
                        <asp:HyperLink ID="hlnkBack" runat="server" CssClass="ach-btn ach-btn-secondary">
                            <i class="fa fa-arrow-left"></i> ย้อนกลับ
                        </asp:HyperLink>
                    </div>
                    <div class="ach-form-actions-right">
                        <asp:Button ID="cmdDel" runat="server" CssClass="ach-btn ach-btn-danger-outline" Text="ลบรายการ" Visible="False"
                            OnClientClick="return confirm('ต้องการลบข้อมูลผลงานนิสิตรายการนี้ใช่หรือไม่?');" />
                        <asp:HyperLink ID="hlnkEdit" runat="server" CssClass="ach-btn ach-btn-warning-soft" Visible="False">
                            <i class="fa fa-pencil"></i> แก้ไขข้อมูล
                        </asp:HyperLink>
                        <asp:Button ID="cmdSave" runat="server" CssClass="ach-btn ach-btn-primary" Text="บันทึกข้อมูล" />
                    </div>
                </div>
            </div>

            <div class="ach-card-footer">
                <div class="ach-info-banner">
                    <i class="fa fa-info-circle"></i>
                    <span>ข้อมูลผลงานที่บันทึกจะถูกส่งต่อไปยังระบบสารสนเทศคณะเภสัชศาสตร์ (CU-PFIS) เพื่อให้เจ้าหน้าที่ตรวจสอบและรับรองก่อนแสดงในประวัตินิสิต</span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
