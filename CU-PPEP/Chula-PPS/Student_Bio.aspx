<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Student_Bio.aspx.vb" Inherits=".Student_Bio" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/uidialog.css" />
    <link rel="stylesheet" type="text/css" href="css/student-achievement.css?v=7" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ach-page">
        <asp:HiddenField ID="hdStudentID" runat="server" />

        <!-- Hero Header Banner -->
        <div class="ach-hero">
            <div class="ach-hero-left">
                <div class="ach-hero-icon">
                    <i class="fa fa-user"></i>
                </div>
                <div>
                    <h1 class="ach-hero-title">ข้อมูลประวัตินิสิต</h1>
                    <p class="ach-hero-subtitle">ข้อมูลทั่วไป ประวัติส่วนตัว ข้อมูลสุขภาพ ครอบครัว ประวัติการศึกษา และข้อมูลติดต่อกรณีฉุกเฉิน</p>
                </div>
            </div>
            <div class="ach-hero-actions">
                <asp:HyperLink ID="hlnkBack" runat="server" CssClass="ach-btn ach-btn-primary">
                    <i class="fa fa-pencil"></i> แก้ไขประวัติ
                </asp:HyperLink>
                <asp:HyperLink ID="hlnkPrint" runat="server" CssClass="ach-btn ach-btn-secondary" Target="_blank">
                    <i class="fa fa-print"></i> พิมพ์ประวัติ
                </asp:HyperLink>
            </div>
        </div>

        <!-- Main 2-Column Content -->
        <div class="row">
            <!-- Left Column -->
            <div class="col-lg-8">
                <!-- Card 1: ข้อมูลทั่วไป -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-id-card-o"></i> ข้อมูลทั่วไป
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <div class="bio-highlight-row">
                            <span class="bio-pill bio-pill-blue">
                                <i class="fa fa-id-badge"></i>
                                รหัสนิสิต: <asp:Label ID="lblStdCode" runat="server"></asp:Label>
                            </span>
                            <span class="bio-pill bio-pill-green">
                                <i class="fa fa-check-circle"></i>
                                สถานะ: <asp:Label ID="lblStudentStatus" runat="server"></asp:Label>
                            </span>
                            <span class="bio-pill bio-pill-amber">
                                <i class="fa fa-line-chart"></i>
                                GPAX: <asp:Label ID="lblGPAX" runat="server"></asp:Label>
                            </span>
                        </div>

                        <div class="bio-field-grid bio-field-grid-3">
                            <div class="bio-field">
                                <span class="bio-field-label">คำนำหน้า</span>
                                <span class="bio-field-value"><asp:Label ID="lblPrefix" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อ (ภาษาไทย)</span>
                                <span class="bio-field-value"><asp:Label ID="lblFirstName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">นามสกุล (ภาษาไทย)</span>
                                <span class="bio-field-value"><asp:Label ID="lblLastName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อเล่น / เพศ</span>
                                <span class="bio-field-value">
                                    <asp:Label ID="lblNickName" runat="server"></asp:Label>
                                    <span style="color:#94a3b8; font-weight:400;"> / </span>
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อ (ภาษาอังกฤษ)</span>
                                <span class="bio-field-value"><asp:Label ID="lblFirstNameEN" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">นามสกุล (ภาษาอังกฤษ)</span>
                                <span class="bio-field-value"><asp:Label ID="lblLastNameEN" runat="server"></asp:Label></span>
                            </div>
                        </div>

                        <div class="bio-field-grid" style="margin-top:14px;">
                            <div class="bio-field">
                                <span class="bio-field-label">สาขาวิชา</span>
                                <span class="bio-field-value"><asp:Label ID="lblMajorName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">สาขา (แขนง)</span>
                                <span class="bio-field-value"><asp:Label ID="lblMinorName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อาจารย์ที่ปรึกษา (Academic Advisor)</span>
                                <span class="bio-field-value"><asp:Label ID="lblAdvisorName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อาจารย์ที่ปรึกษาหลักโครงการปริญญานิพนธ์ (Senior Project)</span>
                                <span class="bio-field-value"><asp:Label ID="lblSeniorProject" runat="server"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Card 2: ข้อมูลส่วนตัว สุขภาพ ครอบครัว และที่อยู่ -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-address-book-o"></i> ข้อมูลส่วนตัว
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <!-- 2.1 ข้อมูลส่วนบุคคลและการติดต่อ -->
                        <div class="bio-subhead">
                            <i class="fa fa-user-circle-o"></i> ข้อมูลส่วนบุคคลและการติดต่อ
                        </div>
                        <div class="bio-field-grid bio-field-grid-3">
                            <div class="bio-field">
                                <span class="bio-field-label">วันเกิด</span>
                                <span class="bio-field-value"><asp:Label ID="lblBirthDate" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">กรุ๊ปเลือด</span>
                                <span class="bio-field-value"><asp:Label ID="lblBloodGroup" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ศาสนา</span>
                                <span class="bio-field-value"><asp:Label ID="lblReligion" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อีเมล</span>
                                <span class="bio-field-value"><asp:Label ID="lblEmail" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">โทรศัพท์บ้าน</span>
                                <span class="bio-field-value"><asp:Label ID="lblTelephone" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">มือถือ</span>
                                <span class="bio-field-value"><asp:Label ID="lblMobile" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="bio-field-grid" style="margin-top:14px;">
                            <div class="bio-field">
                                <span class="bio-field-label">อุปนิสัย</span>
                                <span class="bio-field-value"><asp:Label ID="lblCharacter" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">งานอดิเรก</span>
                                <span class="bio-field-value"><asp:Label ID="lblHobby" runat="server"></asp:Label></span>
                            </div>
                        </div>

                        <!-- 2.2 ข้อมูลสุขภาพและสิทธิการรักษา -->
                        <div class="bio-subhead">
                            <i class="fa fa-heartbeat"></i> ข้อมูลสุขภาพและสิทธิการรักษา
                        </div>
                        <div class="bio-field-grid">
                            <div class="bio-field">
                                <span class="bio-field-label">โรคประจำตัว</span>
                                <span class="bio-field-value"><asp:Label ID="lblCongenitalDisease" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ยาที่ใช้เป็นประจำ</span>
                                <span class="bio-field-value"><asp:Label ID="lblMedicineUsually" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ประวัติการแพ้ยา</span>
                                <span class="bio-field-value"><asp:Label ID="lblMedicalHistory" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">คำแนะนำในการปฏิบัติตัวเมื่อมีอาการ</span>
                                <span class="bio-field-value"><asp:Label ID="lblMedicalRecommend" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field bio-field-full">
                                <span class="bio-field-label">สิทธิการรักษา</span>
                                <span class="bio-field-value"><asp:Label ID="lblClaim" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อสถานพยาบาล</span>
                                <span class="bio-field-value"><asp:Label ID="lblHospitalName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อแพทย์ที่รักษา</span>
                                <span class="bio-field-value"><asp:Label ID="lblDoctorName" runat="server"></asp:Label></span>
                            </div>
                        </div>

                        <!-- 2.3 ข้อมูลครอบครัว -->
                        <div class="bio-subhead">
                            <i class="fa fa-users"></i> ข้อมูลครอบครัว
                        </div>
                        <div class="bio-field-grid">
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อ-นามสกุลบิดา</span>
                                <span class="bio-field-value">
                                    <asp:Label ID="lblFather_FirstName" runat="server"></asp:Label>
                                    <asp:Label ID="lblFather_LastName" runat="server" style="margin-left:4px;"></asp:Label>
                                </span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อาชีพบิดา / เบอร์โทรศัพท์</span>
                                <span class="bio-field-value">
                                    <asp:Label ID="lblFather_Career" runat="server"></asp:Label>
                                    <span style="color:#94a3b8; font-weight:400;"> · </span>
                                    <asp:Label ID="lblFather_Tel" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อ-นามสกุลมารดา</span>
                                <span class="bio-field-value">
                                    <asp:Label ID="lblMother_FirstName" runat="server"></asp:Label>
                                    <asp:Label ID="lblMother_LastName" runat="server" style="margin-left:4px;"></asp:Label>
                                </span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อาชีพมารดา / เบอร์โทรศัพท์</span>
                                <span class="bio-field-value">
                                    <asp:Label ID="lblMother_Career" runat="server"></asp:Label>
                                    <span style="color:#94a3b8; font-weight:400;"> · </span>
                                    <asp:Label ID="lblMother_Tel" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">จำนวนพี่น้อง</span>
                                <span class="bio-field-value"><asp:Label ID="lblSibling" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">เป็นลูกคนที่</span>
                                <span class="bio-field-value"><asp:Label ID="lblChildNo" runat="server"></asp:Label></span>
                            </div>
                        </div>

                        <!-- 2.4 ข้อมูลที่อยู่ -->
                        <div class="bio-subhead">
                            <i class="fa fa-map-marker"></i> ข้อมูลที่อยู่
                        </div>
                        <div class="bio-field-grid">
                            <div class="bio-field bio-field-full">
                                <span class="bio-field-label">ที่อยู่ปัจจุบันระหว่างศึกษา</span>
                                <span class="bio-field-value"><asp:Label ID="lblAddressLive" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field bio-field-full">
                                <span class="bio-field-label">โทรศัพท์ (ที่อยู่ระหว่างศึกษา)</span>
                                <span class="bio-field-value"><asp:Label ID="lblTelLive" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field bio-field-full">
                                <span class="bio-field-label">ที่อยู่ตามบัตร ปชช.</span>
                                <span class="bio-field-value"><asp:Label ID="lblAddress" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ตำบล / แขวง</span>
                                <span class="bio-field-value"><asp:Label ID="lblDistrict" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">อำเภอ / เขต</span>
                                <span class="bio-field-value"><asp:Label ID="lblCity" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">จังหวัด</span>
                                <span class="bio-field-value"><asp:Label ID="lblProvince" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">รหัสไปรษณีย์</span>
                                <span class="bio-field-value"><asp:Label ID="lblZipCode" runat="server"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Card 3: ประวัติการศึกษา -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-graduation-cap"></i> ประวัติการศึกษา
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <div class="table-responsive" style="border:1px solid #e2e8f0; border-radius:12px;">
                            <table class="table bio-edu-table">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width:180px;">ระดับ</th>
                                        <th scope="col">สถานศึกษา</th>
                                        <th scope="col" class="text-center" style="width:170px;">จังหวัด</th>
                                        <th scope="col" class="text-center" style="width:100px;">ปีที่จบ</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">ประถมศึกษา</th>
                                        <td><asp:Label ID="lblPrimarySchool" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblPrimaryProvince" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblPrimaryYear" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">มัธยมศึกษาตอนต้น</th>
                                        <td><asp:Label ID="lblSecondarySchool" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblSecondaryProvince" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblSecondaryYear" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">มัธยมศึกษาตอนปลาย</th>
                                        <td><asp:Label ID="lblHighSchool" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblHighProvince" runat="server"></asp:Label></td>
                                        <td class="text-center"><asp:Label ID="lblHighYear" runat="server"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <div class="bio-subhead" style="margin-top:20px;">
                            <i class="fa fa-certificate"></i> วุฒิการศึกษาอื่น
                        </div>
                        <div class="table-responsive" style="border:1px solid #e2e8f0; border-radius:12px;">
                            <asp:GridView ID="grdEdu"
                                runat="server"
                                CellPadding="0"
                                GridLines="None"
                                AutoGenerateColumns="False"
                                Width="100%"
                                CssClass="table bio-edu-table"
                                EmptyDataText="ไม่มีข้อมูลวุฒิการศึกษาอื่น">
                                <HeaderStyle CssClass="bio-edu-head" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="CourseName" HeaderText="หลักสูตร" />
                                    <asp:BoundField DataField="Institute" HeaderText="สถานศึกษา" />
                                    <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ปีที่จบ" DataField="EduYear">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Right Column -->
            <div class="col-lg-4">
                <!-- Card 1: รูปประจำตัว -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-picture-o"></i> รูปประจำตัว
                        </h3>
                    </div>
                    <div class="ach-card-body text-center">
                        <div class="bio-photo-frame">
                            <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" Width="170px" />
                        </div>
                    </div>
                </div>

                <!-- Card 2: ผู้ติดต่อกรณีฉุกเฉิน -->
                <div class="ach-card bio-emergency-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-ambulance"></i> ผู้ติดต่อกรณีฉุกเฉิน
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <div class="bio-emergency-list">
                            <div class="bio-field">
                                <span class="bio-field-label">ชื่อผู้ติดต่อ</span>
                                <span class="bio-field-value"><asp:Label ID="lblContactName" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">เกี่ยวข้องเป็น</span>
                                <span class="bio-field-value"><asp:Label ID="lblContactRelation" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">เบอร์โทรศัพท์</span>
                                <span class="bio-field-value"><asp:Label ID="lblContactTel" runat="server"></asp:Label></span>
                            </div>
                            <div class="bio-field">
                                <span class="bio-field-label">ที่อยู่</span>
                                <span class="bio-field-value"><asp:Label ID="lblContactAddress" runat="server"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Card 3: ประสบการณ์/กิจกรรม/ความสามารถพิเศษ -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-star-o"></i> ประสบการณ์ / กิจกรรม / ความสามารถพิเศษ
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <div class="bio-note-stack">
                            <div class="bio-note-box">
                                <div class="bio-note-box-label">วิชาที่ชอบเรียน</div>
                                <div class="bio-note-box-value"><asp:Label ID="lblFavoriteSubject" runat="server"></asp:Label></div>
                            </div>
                            <div class="bio-note-box">
                                <div class="bio-note-box-label">ประสบการณ์การทำงาน / กิจกรรมระหว่างศึกษา</div>
                                <div class="bio-note-box-value"><asp:Label ID="lblExperience" runat="server"></asp:Label></div>
                            </div>
                            <div class="bio-note-box">
                                <div class="bio-note-box-label">ความสามารถพิเศษ</div>
                                <div class="bio-note-box-value"><asp:Label ID="lblTalent" runat="server"></asp:Label></div>
                            </div>
                            <div class="bio-note-box">
                                <div class="bio-note-box-label">ความคาดหวังในอาชีพ</div>
                                <div class="bio-note-box-value"><asp:Label ID="lblExpectations" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Card 4: Resume/CV -->
                <div class="ach-card">
                    <div class="ach-card-header">
                        <h3 class="ach-card-title">
                            <i class="fa fa-file-text-o"></i> Resume / CV
                        </h3>
                    </div>
                    <div class="ach-card-body">
                        <asp:HyperLink ID="hlnkCV" runat="server" CssClass="ach-btn ach-btn-primary bio-cv-btn" NavigateUrl="#" Target="_blank">
                            <i class="fa fa-external-link"></i> คลิกดู Personal CV Link
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Alert Modal (preserved for Student_Bio.aspx.vb compatibility) -->
        <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
            TargetControlID="Panel1"
            PopupControlID="Panel1"
            DropShadow="false"
            PopupDragHandleControlID="headbox"
            BackgroundCssClass="dialog_bg"
            Drag="True"
            OkControlID="btnOK">
        </cc2:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" Style="display: none;" DefaultButton="btnOK">
            <table width="300" border="0" align="center" cellpadding="2" cellspacing="0" class="dialog_box">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="dialog_box title" id="headbox">ผลการทำงาน</td>
                            </tr>
                            <tr>
                                <td align="center" class="dialog_content">
                                    <asp:Label ID="lblAlert" runat="server">บันทึกข้อมูลเรียบร้อยแล้ว</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnOK" runat="server" Text="Close" CssClass="buttonModal" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <script type="text/javascript">
            function fnClickOK(sender, e) {
                __doPostBack(sender, e);
            }
        </script>
    </div>
</asp:Content>
