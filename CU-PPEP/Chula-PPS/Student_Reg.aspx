<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Student_Reg.aspx.vb" Inherits=".Student_Reg" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/uidialog.css" />
    <link rel="stylesheet" type="text/css" href="css/student-achievement.css?v=8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ach-page">
        <asp:HiddenField ID="hdStudentID" runat="server" />
        <input type="hidden" id="hdActiveRegTab" name="hdActiveRegTab" value="#tabGeneral" />

        <!-- Hero Header Banner -->
        <div class="ach-hero">
            <div class="ach-hero-left">
                <div class="ach-hero-icon">
                    <i class="fa fa-user-circle-o"></i>
                </div>
                <div>
                    <h1 class="ach-hero-title">
                        แก้ไขข้อมูลนิสิต
                        <span class="bio-pill bio-pill-blue" style="font-size:13px;">
                            <i class="fa fa-id-badge"></i>
                            รหัสนิสิต: <asp:Label ID="lblStdCode" runat="server"></asp:Label>
                        </span>
                    </h1>
                    <p class="ach-hero-subtitle">เลือกแท็บด้านล่างเพื่อกรอกหรือปรับปรุงข้อมูลแต่ละหมวดหมู่ให้ครบถ้วน แล้วกดปุ่มบันทึกข้อมูล</p>
                </div>
            </div>
            <div class="ach-hero-actions">
                <a href="Student_Bio.aspx?ActionType=mnuStudentBio" class="ach-btn ach-btn-secondary">
                    <i class="fa fa-arrow-left"></i> กลับหน้าประวัตินิสิต
                </a>
            </div>
        </div>

        <!-- Tabbed Form Card -->
        <div class="reg-tabs-card">
            <ul id="regTabs" class="nav nav-tabs reg-tabs-nav" role="tablist">
                <li role="presentation" class="active">
                    <a href="#tabGeneral" aria-controls="tabGeneral" role="tab" data-toggle="tab">
                        <i class="fa fa-id-card-o"></i> 1. ข้อมูลทั่วไป
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
                <li role="presentation">
                    <a href="#tabPersonal" aria-controls="tabPersonal" role="tab" data-toggle="tab">
                        <i class="fa fa-address-book-o"></i> 2. ข้อมูลส่วนตัว &amp; ที่อยู่
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
                <li role="presentation">
                    <a href="#tabHealthFamily" aria-controls="tabHealthFamily" role="tab" data-toggle="tab">
                        <i class="fa fa-heartbeat"></i> 3. สุขภาพ &amp; ครอบครัว
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
                <li role="presentation">
                    <a href="#tabEducation" aria-controls="tabEducation" role="tab" data-toggle="tab">
                        <i class="fa fa-graduation-cap"></i> 4. ประวัติการศึกษา
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
                <li role="presentation">
                    <a href="#tabExperience" aria-controls="tabExperience" role="tab" data-toggle="tab">
                        <i class="fa fa-star-o"></i> 5. ประสบการณ์ &amp; CV
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
                <li role="presentation">
                    <a href="#tabEmergencyPhoto" aria-controls="tabEmergencyPhoto" role="tab" data-toggle="tab">
                        <i class="fa fa-ambulance"></i> 6. ผู้ติดต่อฉุกเฉิน &amp; รูปประจำตัว
                        <span class="reg-tab-error-dot" title="มีข้อมูลจำเป็นที่ยังไม่ได้กรอก"></span>
                    </a>
                </li>
            </ul>

            <div class="tab-content reg-tab-content">
                <!-- ==================== TAB 1: ข้อมูลทั่วไป ==================== -->
                <div role="tabpanel" class="tab-pane active" id="tabGeneral">
                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">1</span>
                            <h4 class="ach-section-title">ข้อมูลชื่อ-นามสกุล</h4>
                            <span class="ach-section-hint">ช่องที่มี <span class="ach-star-red">*</span> จำเป็นต้องระบุ</span>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>คำนำหน้าชื่อ</label>
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control">
                                        <asp:ListItem>นาย</asp:ListItem>
                                        <asp:ListItem>นางสาว</asp:ListItem>
                                        <asp:ListItem>นาง</asp:ListItem>
                                        <asp:ListItem>อื่นๆ</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="reg-field-hint">*อื่นๆ ให้กรอกรวมในช่องเดียวกับชื่อ</span>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อเล่น <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtNickName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>เพศ</label>
                                    <div class="reg-radio-inline">
                                        <asp:RadioButtonList ID="optGender" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="M">ชาย</asp:ListItem>
                                            <asp:ListItem Value="F">หญิง</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>รอบสมัครเข้าเรียน</label>
                                    <asp:DropDownList ID="ddlAdmissionRound" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="-- กรุณาเลือก --" Value="" />
                                        <asp:ListItem Text="รอบ Portfolio" Value="" disabled="true" style="font-weight:bold" />
                                        <asp:ListItem Text="  - Port โอลิมปิกวิชาการ" Value="1" />
                                        <asp:ListItem Text="  - Port ภาษาอังกฤษ" Value="2" />
                                        <asp:ListItem Text="รอบ Quota" Value="" disabled="true" style="font-weight:bold" />
                                        <asp:ListItem Text="  - Quota สาธิตจุฬาฯ" Value="3" />
                                        <asp:ListItem Text="  - Quota กีฬาชาติ" Value="4" />
                                        <asp:ListItem Text="  - Quota จุฬาชนบท" Value="5" />
                                        <asp:ListItem Text="  - Quota มหาดไทย" Value="6" />
                                        <asp:ListItem Text="  - Quota กำเนิดวิทย์" Value="7" />
                                        <asp:ListItem Text="รอบ กสพท." Value="8" style="font-weight:bold" />
                                        <asp:ListItem Text="อื่นๆ" Value="99" style="font-weight:bold" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อ (ภาษาไทย) <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtFirstNameTH" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFirstNameTH" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>นามสกุล (ภาษาไทย) <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtLastNameTH" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtLastNameTH" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อ (ภาษาอังกฤษ) <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtFirstNameEN" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFirstNameEN" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>นามสกุล (ภาษาอังกฤษ) <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                        ControlToValidate="txtLastNameEN" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtLastNameEN" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">2</span>
                            <h4 class="ach-section-title">ข้อมูลการศึกษาและอาจารย์ที่ปรึกษา</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>สาขาวิชา</label>
                                    <asp:DropDownList ID="ddlMajor" runat="server" AutoPostBack="True" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>สาขา (แขนง)</label>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlMinor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlMajor" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>อาจารย์ที่ปรึกษา (Academic Advisor)</label>
                                    <asp:DropDownList ID="ddlAdvisor" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>อาจารย์ที่ปรึกษาหลักโครงการปริญญานิพนธ์</label>
                                    <asp:DropDownList ID="ddlSeniorProject" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>GPAX <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                        ControlToValidate="txtGPAX" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtGPAX" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="เช่น 3.50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5 col-sm-6">
                                <div class="form-group">
                                    <label>สถานะนิสิต</label>
                                    <asp:DropDownList CssClass="form-control select2" ID="ddlStudentStatus" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>กำลังศึกษาชั้นปีที่</label>
                                    <asp:TextBox ID="txtLevelClass" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ==================== TAB 2: ข้อมูลส่วนตัว & ที่อยู่ ==================== -->
                <div role="tabpanel" class="tab-pane" id="tabPersonal">
                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">1</span>
                            <h4 class="ach-section-title">ข้อมูลส่วนบุคคลและการติดต่อ</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-sm-12">
                                <div class="form-group">
                                    <label>วัน/เดือน/ปีเกิด (พ.ศ.) <span class="ach-star-red">*</span></label>
                                    <div class="reg-dob-grid">
                                        <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>กรุ๊ปเลือด</label>
                                    <asp:DropDownList CssClass="form-control select2" ID="ddlBloodGroup" runat="server">
                                        <asp:ListItem Selected="True">A</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                        <asp:ListItem>AB</asp:ListItem>
                                        <asp:ListItem>O</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>ศาสนา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                        ControlToValidate="txtRegion" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>อีเมล <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtEmail" CssClass="reg-val-msg" ErrorMessage="โปรดกรอกอีเมล" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtEmail" CssClass="reg-val-msg" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" Display="Dynamic"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>โทรศัพท์บ้าน</label>
                                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>มือถือ <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="txtMobile" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>อุปนิสัย</label>
                                    <asp:TextBox ID="txtCharacter" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>งานอดิเรก <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                                        ControlToValidate="txtHobby" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtHobby" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">2</span>
                            <h4 class="ach-section-title">ข้อมูลที่อยู่</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>ที่อยู่ปัจจุบันระหว่างศึกษา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server"
                                        ControlToValidate="txtAddressLive" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddressLive" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>เบอร์โทรศัพท์ (ที่อยู่ระหว่างศึกษา)</label>
                                    <asp:TextBox ID="txtTelLive" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ที่อยู่ตามบัตรประชาชน <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server"
                                        ControlToValidate="txtAddressCard" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddressCard" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ตำบล / แขวง <span class="ach-star-red">*</span></label>
                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>อำเภอ / เขต <span class="ach-star-red">*</span></label>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label>จังหวัด <span class="ach-star-red">*</span></label>
                                    <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-6">
                                <div class="form-group">
                                    <label>รหัสไปรษณีย์</label>
                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control text-center" MaxLength="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ==================== TAB 3: สุขภาพ & ครอบครัว ==================== -->
                <div role="tabpanel" class="tab-pane" id="tabHealthFamily">
                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">1</span>
                            <h4 class="ach-section-title">ข้อมูลสุขภาพและสิทธิการรักษา</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>โรคประจำตัว <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtCongenitalDisease" CssClass="reg-val-msg"
                                        ErrorMessage="โปรดกรอกหากไม่มีให้ใส่ -" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtCongenitalDisease" runat="server" CssClass="form-control" MaxLength="50" placeholder="หากไม่มีให้ใส่ -"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>ยาที่ใช้เป็นประจำ <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="txtMedicineUsually" CssClass="reg-val-msg"
                                        ErrorMessage="โปรดกรอกหากไม่มีให้ใส่ -" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMedicineUsually" runat="server" CssClass="form-control" MaxLength="100" placeholder="หากไม่มีให้ใส่ -"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>ประวัติการแพ้ยา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                        ControlToValidate="txtMedicalHistory" CssClass="reg-val-msg"
                                        ErrorMessage="โปรดกรอก หากไม่มีให้ใส่ -" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMedicalHistory" runat="server" CssClass="form-control" MaxLength="50" placeholder="หากไม่มีให้ใส่ -"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>คำแนะนำในการปฏิบัติตัวเมื่อมีอาการ</label>
                                    <asp:TextBox ID="txtMedicalRecommend" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>สิทธิการรักษา</label>
                                    <asp:DropDownList CssClass="form-control select2" ID="ddlPayor" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ระบุเพิ่มเติม</label>
                                    <asp:TextBox ID="txtPayorDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อสถานพยาบาลที่รักษา</label>
                                    <asp:TextBox ID="txtHospital" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อแพทย์ที่รักษา</label>
                                    <asp:TextBox ID="txtDoctor" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">2</span>
                            <h4 class="ach-section-title">ข้อมูลครอบครัว</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อบิดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                        ControlToValidate="txtFather_FirstName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFather_FirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>นามสกุลบิดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                        ControlToValidate="txtFather_LastName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFather_LastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>อาชีพบิดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                        ControlToValidate="txtFather_Career" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFather_Career" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>เบอร์โทรศัพท์บิดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                        ControlToValidate="txtFather_Tel" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFather_Tel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>ชื่อมารดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                        ControlToValidate="txtMother_FirstName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMother_FirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>นามสกุลมารดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                        ControlToValidate="txtMother_LastName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMother_LastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>อาชีพมารดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                                        ControlToValidate="txtMother_Career" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMother_Career" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>เบอร์โทรศัพท์มารดา <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server"
                                        ControlToValidate="txtMother_Tel" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMother_Tel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>จำนวนพี่น้อง <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                                        ControlToValidate="txtSibling" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSibling" runat="server" CssClass="form-control text-center" MaxLength="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label>เป็นลูกคนที่ <span class="ach-star-red">*</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                                        ControlToValidate="txtChildNo" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtChildNo" runat="server" CssClass="form-control text-center" MaxLength="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ==================== TAB 4: ประวัติการศึกษา ==================== -->
                <div role="tabpanel" class="tab-pane" id="tabEducation">
                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">1</span>
                            <h4 class="ach-section-title">ประวัติการศึกษาขั้นพื้นฐาน <span class="ach-star-red">*</span></h4>
                        </div>

                        <div class="table-responsive" style="border:1px solid #e2e8f0; border-radius:12px;">
                            <table class="table bio-edu-table">
                                <thead>
                                    <tr>
                                        <th style="width:190px;">ระดับการศึกษา</th>
                                        <th>สถานศึกษา</th>
                                        <th style="width:220px;">จังหวัด</th>
                                        <th class="text-center" style="width:120px;">ปีที่จบ (พ.ศ.)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">ประถมศึกษา <span class="ach-star-red">*</span></th>
                                        <td><asp:TextBox ID="txtPrimarySchool" runat="server" CssClass="form-control" placeholder="ชื่อโรงเรียนประถมศึกษา"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtPrimaryProvince" runat="server" CssClass="form-control" placeholder="จังหวัด"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtPrimaryYear" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="พ.ศ."></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">มัธยมศึกษาตอนต้น <span class="ach-star-red">*</span></th>
                                        <td><asp:TextBox ID="txtSecondarySchool" runat="server" CssClass="form-control" placeholder="ชื่อโรงเรียนมัธยมศึกษาตอนต้น"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtSecondaryProvince" runat="server" CssClass="form-control" placeholder="จังหวัด"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtSecondaryYear" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="พ.ศ."></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">มัธยมศึกษาตอนปลาย <span class="ach-star-red">*</span></th>
                                        <td><asp:TextBox ID="txtHighSchool" runat="server" CssClass="form-control" placeholder="ชื่อโรงเรียนมัธยมศึกษาตอนปลาย"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtHighProvince" runat="server" CssClass="form-control" placeholder="จังหวัด"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtHighYear" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="พ.ศ."></asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">2</span>
                            <h4 class="ach-section-title">วุฒิการศึกษาอื่นๆ</h4>
                            <asp:HiddenField ID="hdEduUID" runat="server" />
                        </div>

                        <div class="reg-edu-add-box">
                            <div class="row">
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label>หลักสูตร</label>
                                        <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control" MaxLength="50" placeholder="ชื่อหลักสูตร"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label>สถานศึกษา</label>
                                        <asp:TextBox ID="txtSchool" runat="server" MaxLength="50" CssClass="form-control" placeholder="ชื่อสถาบัน"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label>จังหวัด</label>
                                        <asp:TextBox ID="txtProvinceEdu" runat="server" MaxLength="20" CssClass="form-control" placeholder="จังหวัด"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-3">
                                    <div class="form-group">
                                        <label>ปีที่จบ</label>
                                        <asp:TextBox ID="txtYearEdu" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="พ.ศ."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <div>
                                            <asp:Button ID="cmdAddEdu" runat="server" Text="+ เพิ่มวุฒิ" CausesValidation="False" CssClass="ach-btn ach-btn-primary" style="width:100%;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="table-responsive" style="border:1px solid #e2e8f0; border-radius:12px;">
                            <asp:GridView ID="grdEdu"
                                runat="server"
                                CellPadding="0"
                                GridLines="None"
                                AutoGenerateColumns="False"
                                Width="100%"
                                CssClass="table bio-edu-table"
                                EmptyDataText="ยังไม่มีข้อมูลวุฒิการศึกษาอื่นๆ">
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
                                    <asp:TemplateField HeaderText="แก้ไข">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" CausesValidation="False"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/icon-delete.png" CausesValidation="False"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <!-- ==================== TAB 5: ประสบการณ์ & CV ==================== -->
                <div role="tabpanel" class="tab-pane" id="tabExperience">
                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">1</span>
                            <h4 class="ach-section-title">ประสบการณ์ กิจกรรม และความสามารถพิเศษ</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>วิชาที่ชอบเรียน</label>
                                    <asp:TextBox ID="txtFavoriteSubject" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>ความสามารถพิเศษ</label>
                                    <asp:TextBox ID="txtTalent" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ประสบการณ์การทำงาน / กิจกรรมระหว่างศึกษา</label>
                                    <asp:TextBox ID="txtExperience" runat="server" TextMode="MultiLine" Rows="4" MaxLength="4000" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ความคาดหวังในอาชีพ</label>
                                    <asp:TextBox ID="txtExpectations" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ach-section">
                        <div class="ach-section-head">
                            <span class="ach-section-num">2</span>
                            <h4 class="ach-section-title">Resume / Personal CV Link</h4>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="margin-bottom:0;">
                                    <label>Personal CV Link (URL)</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-link"></i>
                                        </div>
                                        <asp:TextBox ID="txtLinkCV" runat="server" CssClass="form-control" MaxLength="100" placeholder="https://..."></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ==================== TAB 6: ผู้ติดต่อฉุกเฉิน & รูปประจำตัว ==================== -->
                <div role="tabpanel" class="tab-pane" id="tabEmergencyPhoto">
                    <div class="row">
                        <div class="col-md-7">
                            <div class="ach-section" style="border-bottom:0; margin-bottom:0; padding-bottom:0;">
                                <div class="ach-section-head">
                                    <span class="ach-section-num" style="background:#fff1f2; color:#be123c; border-color:#fecdd3;">
                                        <i class="fa fa-ambulance"></i>
                                    </span>
                                    <h4 class="ach-section-title">ผู้ติดต่อกรณีฉุกเฉิน</h4>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>ชื่อ-นามสกุล <span class="ach-star-red">*</span></label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                ControlToValidate="txtContactName" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>เกี่ยวข้องเป็น <span class="ach-star-red">*</span></label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                ControlToValidate="txtContactRelation" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtContactRelation" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>เบอร์โทรศัพท์ <span class="ach-star-red">*</span></label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="txtContactTel" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtContactTel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>ที่อยู่ <span class="ach-star-red">*</span></label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="txtContactAddress" CssClass="reg-val-msg" ErrorMessage="โปรดกรอก" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtContactAddress" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="ach-section" style="border-bottom:0; margin-bottom:0; padding-bottom:0;">
                                <div class="ach-section-head">
                                    <span class="ach-section-num">
                                        <i class="fa fa-picture-o"></i>
                                    </span>
                                    <h4 class="ach-section-title">รูปประจำตัว</h4>
                                </div>

                                <div class="text-center" style="margin-bottom:14px;">
                                    <div class="bio-photo-frame">
                                        <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" Width="150px" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>อัปโหลดรูปภาพใหม่</label>
                                    <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />
                                </div>

                                <div class="reg-photo-guide">
                                    <strong><i class="fa fa-info-circle" style="color:#008749;"></i> คำแนะนำรูปประจำตัว</strong>
                                    1. ชนิดไฟล์รูปภาพเป็น <b>JPG</b> เท่านั้น ขนาดไม่เกิน <b>500 KB</b> (แนะนำกว้าง ~150 px สูง ~180 px)<br />
                                    2. ชื่อไฟล์ต้องเป็นภาษาอังกฤษ<br />
                                    3. เป็นภาพติดบัตรทางการ สุภาพ สวมชุดนิสิต หรือชุดปฏิบัติวิชาชีพเท่านั้น (งดใส่หมวก แว่นตาแฟชั่น หรือโพสต์ท่าทาง)
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Bottom Navigation & Save Bar -->
        <div class="reg-action-bar">
            <div class="reg-action-group">
                <button type="button" id="btnPrevTab" class="ach-btn ach-btn-secondary">
                    <i class="fa fa-chevron-left"></i> แท็บก่อนหน้า
                </button>
                <button type="button" id="btnNextTab" class="ach-btn ach-btn-secondary">
                    แท็บถัดไป <i class="fa fa-chevron-right"></i>
                </button>
            </div>
            <div class="reg-action-group">
                <asp:Button ID="cmdClear" runat="server" CausesValidation="False" CssClass="ach-btn ach-btn-secondary" Text="ล้างค่า" />
                <asp:Button ID="cmdPrint" runat="server" CausesValidation="False" CssClass="ach-btn ach-btn-warning-soft" Text="พิมพ์" />
                <asp:Button ID="cmdSave" runat="server" CssClass="ach-btn ach-btn-primary" Text="บันทึกข้อมูล" OnClientClick="return regValidateAndFocusTab();" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function regActivateTab(tabSelector) {
            var $link = $('#regTabs a[href="' + tabSelector + '"]');
            if ($link.length) {
                $link.tab('show');
            }
        }

        function regUpdateStepButtons() {
            var $tabs = $('#regTabs > li');
            var activeIdx = $tabs.index($tabs.filter('.active'));
            $('#btnPrevTab').prop('disabled', activeIdx <= 0);
            $('#btnNextTab').prop('disabled', activeIdx >= $tabs.length - 1);
        }

        function regRefreshSelect2($scope) {
            if ($.fn.select2) {
                $scope.find('select.select2').each(function () {
                    var $el = $(this);
                    if ($el.hasClass('select2-hidden-accessible')) {
                        $el.select2('destroy');
                    }
                    $el.select2({ width: '100%' });
                });
            }
        }

        function regValidateAndFocusTab() {
            if (typeof Page_ClientValidate === 'function') {
                var isValid = Page_ClientValidate();
                $('#regTabs > li').removeClass('has-error');
                if (!isValid && typeof Page_Validators !== 'undefined') {
                    var firstPaneId = null;
                    var firstControl = null;
                    for (var i = 0; i < Page_Validators.length; i++) {
                        var v = Page_Validators[i];
                        if (!v.isvalid) {
                            var $pane = $(v).closest('.tab-pane');
                            if ($pane.length) {
                                var paneId = '#' + $pane.attr('id');
                                $('#regTabs a[href="' + paneId + '"]').parent('li').addClass('has-error');
                                if (!firstPaneId) {
                                    firstPaneId = paneId;
                                    if (v.controltovalidate) {
                                        firstControl = document.getElementById(v.controltovalidate);
                                    }
                                }
                            }
                        }
                    }
                    if (firstPaneId) {
                        regActivateTab(firstPaneId);
                        if (firstControl) {
                            setTimeout(function () {
                                firstControl.focus();
                                $('html, body').animate({ scrollTop: $(firstControl).offset().top - 140 }, 220);
                            }, 120);
                        }
                    }
                }
                return isValid;
            }
            return true;
        }

        $(function () {
            var storageKey = 'cu_ppep_student_reg_tab';

            // Restore active tab on postback (e.g. adding Other Education)
            var savedTab = sessionStorage.getItem(storageKey);
            if (savedTab && $('#regTabs a[href="' + savedTab + '"]').length) {
                regActivateTab(savedTab);
            }
            regUpdateStepButtons();

            $('#regTabs a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                var target = $(e.target).attr('href');
                $('#hdActiveRegTab').val(target);
                sessionStorage.setItem(storageKey, target);
                regRefreshSelect2($(target));
                regUpdateStepButtons();
            });

            $('#btnPrevTab').on('click', function () {
                var $tabs = $('#regTabs > li');
                var idx = $tabs.index($tabs.filter('.active'));
                if (idx > 0) {
                    $tabs.eq(idx - 1).find('a').tab('show');
                }
            });

            $('#btnNextTab').on('click', function () {
                var $tabs = $('#regTabs > li');
                var idx = $tabs.index($tabs.filter('.active'));
                if (idx < $tabs.length - 1) {
                    $tabs.eq(idx + 1).find('a').tab('show');
                }
            });

            // Re-apply Select2 width after UpdatePanel async postback (ddlMajor -> ddlMinor)
            if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    regRefreshSelect2($('.tab-pane.active'));
                });
            }
        });
    </script>
</asp:Content>
