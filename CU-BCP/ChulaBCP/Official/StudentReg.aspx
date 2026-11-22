<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentReg.aspx.vb" Inherits=".StudentReg" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/pagestyles.css">
<link rel="stylesheet" type="text/css" href="css/custyles.css">
<link rel="stylesheet" type="text/css" href="css/uidialog.css">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Student
                                    <div class="page-title-subheading">จัดการข้อมูลเภสัชกร</div>
                                </div>
                            </div>
                        </div>
                    </div>
    <section class="content">     
         <div class="row">
      <section class="col-lg-8 connectedSortable">
 <div class="box box-primary">
   

            <div class="box-header">
              <i class="fa fa-info-circle"></i>

              <div class="box-title">ข้อมูลทั่วไป</div>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            <asp:HiddenField ID="hdStudentID" runat="server" />
            </div>
            <div class="box-body">
                <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>
                <div class="col-md-3">
                <div class="form-group">
                  <label>คำนำหน้าชื่อ</label>
                  <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2">                   
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>เพศ</label>
                  <asp:RadioButtonList ID="optGender" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="M">ชาย</asp:ListItem>
                    <asp:ListItem Value="F">หญิง</asp:ListItem>
                  </asp:RadioButtonList>
                </div>
              </div>
   <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อเล่น <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtNickName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control text-center" MaxLength="20"></asp:TextBox>

                </div>
              </div>            
                </div>
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>ชื่อ (ภาษาไทย) <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="txtFirstNameTH" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFirstNameTH" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>นามสกุล (ภาษาไทย) <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtLastNameTH" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtLastNameTH" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>ชื่อ (ภาษาอังกฤษ) <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtFirstNameEN" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFirstNameEN" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>นามสกุล (ภาษาอังกฤษ) <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                    ControlToValidate="txtLastNameEN" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtLastNameEN" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                </div>
              </div>
            </div>
                     <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>สาขาวิชา</label>
            <asp:Label ID="lblMajorName" runat="server" CssClass="form-control"></asp:Label>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>อาจารย์ที่ปรึกษา</label>
            <asp:Label ID="lblAdvisorName" runat="server" CssClass="form-control"></asp:Label>

                </div>
              </div>
   </div> 
                   <div class="row"> 
              <div class="col-md-3">
                <div class="form-group">
                  <label>GPAX</label><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtGPAX" CssClass="text-red" ErrorMessage="โปรดกรอก"></asp:RequiredFieldValidator>
          &nbsp;<asp:TextBox ID="txtGPAX" runat="server" CssClass="form-control text-center" MaxLength="4"></asp:TextBox>
                </div>

              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>กำลังศึกษาชั้นปีที่</label>
            <asp:Label ID="lblLevelClass" runat="server" CssClass="form-control"></asp:Label>
                 

                </div>
              </div>
            </div>               
    </div>
</div>
             </section>
            <section class="col-lg-4 connectedSortable">
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-picture-o"></i>
              <h3 class="box-title">รูปประจำตัว</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                 <table width="100%" border="0" cellspacing="2" cellpadding="0">
      <tr>
        <td width="100">รูปภาพ</td>
        <td>
            <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />
          </td>
         <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" 
                Width="150px" />
          </td>
        </tr>
      <tr>
        <td colspan="3" valign="top"><b>คำแนะนำ</b><br />
            1. ชนิดไฟล์รูปภาพเป็น JPG เท่านั้น และ ขนาดไม่เกิน 500 KB <br />              
          ควรมีความกว้างประมาณ 150 pixel และความสูงประมาณ 180 pixel<br />
          2. ควรเป็นภาพติดบัตร เป็นภาพทางการ และสุภาพ หรือ ชุดปฏิบัติวิชาชีพเท่านั้น (งดเว้นภาพโพสต์ท่าทาง เช่น ใส่หมวก, แว่นตาแฟชั่น, ชูสองนิ้ว)</td>       
        </tr>
    </table>
    </div>
</div>
 
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">Resume/Social</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                <div class="row">
                     <div class="col-md-12">
                <div class="form-group">
                  <label>Line ID</label>                   
                  <asp:TextBox ID="txtLineID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                    <div class="col-md-12">
                <div class="form-group">
                  <label>Personal CV Link</label>                   
                  <asp:TextBox ID="txtLinkCV" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>

                </div>
                
     
    </div>
</div>     
                </section>
   </div>
 
        <div class="row">
      <section class="col-lg-12 connectedSortable">
        <div class="box box-primary">
          <div class="box-header">
            <i class="fa fa-asterisk"></i>

            <h3 class="box-title">ข้อมูลส่วนตัว</h3>

            <div class="box-tools pull-right">
              <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
              </button>
              <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
          </div>
          <div class="box-body">
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>วันเกิด <span class="text-red">*</span></label><br />
                    <table>
                        <tr>
                            <td> <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control select2" Width="70"> </asp:DropDownList></td>
                               <td><asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control select2" Width="120"> </asp:DropDownList></td>
                               <td> <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" Width="85"> </asp:DropDownList></td>
                        </tr>
                    </table>                                
                 
                </div>
              </div>
              <div class="col-md-4">
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
         
              <div class="col-md-4">
                <div class="form-group">
                  <label>ศาสนา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtRegion"
                    CssClass="text-red" ErrorMessage="โปรดกรอก"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control text-center" MaxLength="20"></asp:TextBox>
                </div>
              </div>
                   </div>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>อีเมล <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                    CssClass="text-red" ErrorMessage="โปรดกรอกอีเมล"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtEmail" CssClass="text-red" ErrorMessage=" รูปแบบอีเมลไม่ถูกต้อง"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                  </asp:RegularExpressionValidator>
                  <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
              </div>
              <div class="col-md-4">
                <div class="form-group">
                  <label>โทรศัพท์บ้าน</label>
                  <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-4">
                <div class="form-group">
                  <label>มือถือ <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMobile"
                    CssClass="text-sm text-red" ErrorMessage="โปรดกรอก"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>

                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>โรคประจำตัว <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                    ControlToValidate="txtCongenitalDisease" CssClass="text-red"
                    ErrorMessage="โปรดกรอกหากไม่มีให้ใส่ -"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtCongenitalDisease" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                </div> <!-- row -->
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>ยาที่ใช้เป็นประจำ <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                    ControlToValidate="txtMedicineUsually" CssClass="text-red" ErrorMessage="โปรดกรอกหากไม่มีให้ใส่ -">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMedicineUsually" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>

                </div> <!-- row -->
              </div>
            </div>
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>ประวัติการแพ้ยา <span class="text-red">*<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtMedicalHistory"
                        CssClass="text-red" ErrorMessage="โปรดกรอก หากไม่มีให้ใส่ -">
                      </asp:RequiredFieldValidator>
                    </span></label>
                  <asp:TextBox ID="txtMedicalHistory" runat="server"
                    CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div> <!-- row -->
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>คําแนะนําในการปฏิบัติตัวเมื่อมีอาการ</label>
                  <asp:TextBox ID="txtMedicalRecommend" runat="server"  CssClass="form-control" MaxLength="200"></asp:TextBox>


                </div> <!-- row -->
              </div>
            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>สิทธิการรักษา</label>
                  <asp:DropDownList CssClass="form-control select2" ID="ddlPayor" runat="server">
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>ระบุ</label>
                  <asp:TextBox ID="txtPayorDesc" runat="server"  CssClass="form-control"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อสถานพยาบาลที่รักษา</label>
                  <asp:TextBox ID="txtHospital" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อแพทย์ที่รักษา</label>
                  <asp:TextBox ID="txtDoctor" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อบิดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                    ControlToValidate="txtFather_FirstName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFather_FirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>นามสกุลบิดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                    ControlToValidate="txtFather_LastName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFather_LastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>อาชีพบิดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                    ControlToValidate="txtFather_Career" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFather_Career" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>เบอร์โทรศัพท์บิดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                    ControlToValidate="txtFather_Tel" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtFather_Tel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
              </div>

            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อมารดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                    ControlToValidate="txtMother_FirstName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMother_FirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>นามสกุลมารดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                    ControlToValidate="txtMother_LastName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMother_LastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>อาชีพมารดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                    ControlToValidate="txtMother_Career" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMother_Career" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>เบอร์โทรศัพท์มารดา <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server"
                    ControlToValidate="txtMother_Tel" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtMother_Tel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>จำนวนพี่น้อง <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                    ControlToValidate="txtSibling" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtSibling" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>เป็นลูกคนที่ <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                    ControlToValidate="txtChildNo" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtChildNo" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
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
                  <label>งานอดิเรก <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtHobby"
                    CssClass="text-red" ErrorMessage="โปรดกรอก"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtHobby" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-9">
                <div class="form-group">
                  <label>ที่อยู่ระหว่างการศึกษา<span class="text-red">*</span></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server"
                    ControlToValidate="txtAddressLive" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtAddressLive" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>เบอร์โทรศัพท์</label>
                  <asp:TextBox ID="txtTelLive" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>ที่อยู่ตามบัตรประชาชน <span class="text-red">*</span> </label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server"
                    ControlToValidate="txtAddressCard" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtAddressCard" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
              </div> 
                
                  <div class="col-md-3">
                <div class="form-group">
                  <label>ตำบล/แขวง</label>
                  <asp:TextBox ID="txtDistrict" runat="server"  CssClass="form-control"  ></asp:TextBox>
                </div>
              </div>

                  <div class="col-md-3">
                <div class="form-group">
                  <label>อำเภอ/เขต</label>
                  <asp:TextBox ID="txtCity" runat="server"  CssClass="form-control"  ></asp:TextBox>
                </div>
              </div>


              <div class="col-md-2">
                <div class="form-group">
                  <label>จังหวัด <span class="text-red">*</span></label>
                  <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"> </asp:DropDownList>
                </div>
              </div>
              <div class="col-md-1">
                <div class="form-group">
                  <label>รหัส ปษณ.</label>
                  <asp:TextBox ID="txtZipCode" runat="server"  CssClass="form-control"  MaxLength="5"></asp:TextBox>
                </div>
              </div>
            </div>
          </div>
        </div>       
         
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-graduation-cap"></i>

              <h3 class="box-title">ประวัติการศึกษา <span class="text-red">*</span></h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
               <table border="0"  class="table table-borderless">
      <tr>
        <td width="200" align="left">ระดับ</td>
        <td  align="center">สถานศึกษา</td>
        <td width="200" align="center">จังหวัด</td>
        <td width="100" align="center">ปีที่จบ</td>
      </tr>
      <tr>
        <td>ประถมศึกษา</td>
        <td align="center">
            <asp:TextBox ID="txtPrimarySchool" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td align="center">
            <asp:TextBox ID="txtPrimaryProvince" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td  align="center">
            <asp:TextBox ID="txtPrimaryYear" runat="server" MaxLength="4" Width="77px" CssClass="form-control text-center"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td>มัธยมศึกษาตอนต้น</td>
        <td align="center">
            <asp:TextBox ID="txtSecondarySchool" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td align="center">
            <asp:TextBox ID="txtSecondaryProvince" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td align="center">
            <asp:TextBox ID="txtSecondaryYear" runat="server" MaxLength="4" Width="77px" CssClass="form-control text-center"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td>มัธยมศึกษาตอนปลาย </td>
        <td align="center">
            <asp:TextBox ID="txtHighSchool" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td align="center">
            <asp:TextBox ID="txtHighProvince" runat="server" CssClass="form-control"></asp:TextBox>          </td>
        <td align="center">
            <asp:TextBox ID="txtHighYear" runat="server" MaxLength="4" Width="77px" CssClass="form-control text-center"></asp:TextBox>
          </td>
      </tr>     

      <tr>
        <td class="text-blue text-bold">วุฒิการศึกษาอื่นๆ</td>
        <td align="center">
                        <asp:HiddenField ID="hdEduUID" runat="server" />
                    </td>
        <td align="center">
            &nbsp;</td>
        <td align="center">
            &nbsp;</td>
      </tr>
      
      <tr>
      
                <td colspan="4" align="left">
<div class="row">
                     <div class="col-md-3">
                <div class="form-group">
                  <label>หลักสูตร</label>
                  <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
                     <div class="col-md-3">
                <div class="form-group">
                  <label>สถานศึกษา</label>
                  <asp:TextBox ID="txtSchool" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                     <div class="col-md-3">
                <div class="form-group">
                  <label>จังหวัด</label>
                  <asp:TextBox ID="txtProvinceEdu" runat="server" MaxLength="20"  CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                     <div class="col-md-1">
                <div class="form-group">
                  <label>ปีที่จบ</label>
                   <asp:TextBox ID="txtYearEdu" runat="server"  CssClass="form-control" MaxLength="4"></asp:TextBox>
                </div>
              </div>
                     <div class="col-md-2">
                <div class="form-group">
                  <br />
                  <asp:Button ID="cmdAddEdu" runat="server" Text="+ บันทึก" CssClass="btn btn-success" />
                </div>
              </div>
            </div>    </td>
      </tr>
      
      <tr>
        <td colspan="4">
              <asp:GridView ID="grdEdu" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="CourseName" HeaderText="หลักสูตร">
                </asp:BoundField>
                <asp:BoundField DataField="Institute" HeaderText="สถานศึกษา" />
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" /> 
                </asp:BoundField>
            <asp:BoundField HeaderText="ปีที่จบ" DataField="EduYear">
              <itemstyle HorizontalAlign="Center" />                      </asp:BoundField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>        
    </table>  
    </div>
</div>
</section>
   </div>
    <div class="row">
      <section class="col-lg-6 connectedSortable">
                    
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">ข้อมูลอื่นๆ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                <div class="row">
                     <div class="col-md-6">
                <div class="form-group">
                  <label>เลขที่บัญชีธนาคาร (ป้อนเฉพาะตัวเลข)</label>                   
                  <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                    <div class="col-md-6">
                <div class="form-group">
                  <label>ธนาคาร</label>                   
                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>

                </div>
                      <div class="row">
                     <div class="col-md-12">
                <div class="form-group">
                  <label>สถานที่ทำงาน</label>                   
                  <asp:TextBox ID="txtWorkPlace" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                          </div>
                            <div class="row">
                    <div class="col-md-3">
                <div class="form-group">
                  <label>Size เสื้อกาวน์</label>                   
                  <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
                           <div class="col-md-3">
                <div class="form-group">
                  <label>กรมธรรม์ Covid-19</label>                   
                    <asp:RadioButtonList ID="optCovid" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Y">มี</asp:ListItem>
                        <asp:ListItem Selected="True" Value="N">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              </div>

                </div>
                
     
    </div>
</div>

    </section>      
        
      <section class="col-lg-6 connectedSortable">      
        <div class="box box-primary">
          <div class="box-header">
            <i class="fa fa-ambulance"></i>
            <h3 class="box-title">ผู้ติดต่อฉุกเฉิน</h3>
            <div class="box-tools pull-right">
              <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
              </button>
              <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
          </div>
          <div class="box-body">
            <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                    ControlToValidate="txtContactName" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>เบอร์โทรศัพท์ <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                    ControlToValidate="txtContactTel" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtContactTel" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>เกี่ยวข้องเป็น <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                    ControlToValidate="txtContactRelation" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtContactRelation" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label>ที่อยู่ <span class="text-red">*</span></label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                    ControlToValidate="txtContactAddress" CssClass="text-red" ErrorMessage="โปรดกรอก">
                  </asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtContactAddress" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
              </div>
            </div>
          </div>
        </div>
      
  </section>
    </div>
   
                   

        <div align="center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="บันทึก" Width="100px" />
        <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" Width="100px" />
        <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-success" Text="พิมพ์" Width="100px" />
        </div>

</section>
</asp:Content>
 

