<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="PreceptorData.aspx.vb" Inherits=".PreceptorData" %>

    <%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        </asp:Content>

        <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <section class="content-header">
                <h1>จัดการข้อมูลอาจารย์แหล่งฝึก (Preceptor)</h1>
            </section>

            <section class="content">

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-search-plus"></i>

                        <h3 class="box-title">เพิ่ม/แก้ไข ข้อมูลอาจารย์แหล่งฝึก</h3>
                          <asp:HiddenField ID="hdUID" runat="server" />   
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i
                                    class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">                        
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>แหล่งฝึก (Training Site) <span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True"
                                        CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-6">
                                 <div class="form-group">
                                     <label>ส่วนงาน (Work Field)</label>
                                     <asp:DropDownList ID="ddlWork" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                 </div>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>คำนำหน้า <span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>ชื่อ (First Name) <span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>นามสกุล (Last Name) <span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                              <div class="col-md-3">
      <div class="form-group">
          <label>ตำแหน่ง (Position) <span class="text-red">*</span></label>
          <asp:TextBox ID="txtPositionName" runat="server" CssClass="form-control">
          </asp:TextBox>
      </div>
  </div>  
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>E-mail <span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtMail" CssClass="validateAlert"
                                        ErrorMessage="&lt;&lt; รูปแบบอีเมลไม่ถูกต้อง"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>เบอร์โทรศัพท์ (Contact Number) <span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>
                          
                        </div>

                                                 <div class="row">
                                              <div class="col-md-3">
                                <div class="form-group">
                                    <label>เลขที่ใบประกอบวิชาชีพ (License Number) <span
                                            class="text-red">*</span></label>
                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>             
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>หน่วยกิตการศึกษาต่อเนื่องสะสม (CPE Credits)</label>
                                        <asp:TextBox ID="txtCPECredits" runat="server" CssClass="form-control"
                                            TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                          
                                    <div class="col-md-3">
                                        <div class="form-group"><br />
                                             <asp:CheckBox ID="chkTraining" runat="server" Text="ผ่านการอบรมแหล่งฝึก (Training Participation)" /> <br />
                                             <asp:CheckBox ID="chkApprove" runat="server" Text="ยืนยัน/อนุมัติ (ส่งเอกสาร)" /> 
                                        </div>
                                    </div>
                              <div class="col-md-1">
                                 <div class="form-group"><br />
                                     <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                 </div>
                              </div>
                                </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>รูปภาพ (Photo) <span class="text-red">*</span></label>
                                                <asp:FileUpload ID="fuPhoto" runat="server"/>
                                                <span class="text12_nblue">* ไฟล์ JPG/PNG ขนาดไม่เกิน 500 KB,
                                                    ควรมีขนาดประมาณ 150x180 px</span>
                                            </div>
                                            <div class="form-group">
                                                <asp:Image ID="imgPhoto" runat="server" ImageUrl="images/unpic.jpg"
                                                    Width="120px" />
                                            </div>
                                        </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>ไฟล์ CV / Resume <span class="text-red">*</span></label>
                                                 <asp:TextBox ID="txtCvLink" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="text12_nblue">* แนบลิงค์ CV/Resume </span>
                                                </div>
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hlnkCV" runat="server"
                                                        CssClass="btn btn-info btn-xs" Visible="false" Target="_blank">
                                                        <i class="fa fa-file-pdf-o"></i> ดู CV/Resume
                                                    </asp:HyperLink>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">                                        
                                            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100"
                                                Text="บันทึก"></asp:Button>   
                                            <a href="PreceptorList.aspx?ActionType=tch&ItemType=pct" class="btn btn-secondary">ย้อนกลับ</a>
                                        </div>
                                    </div>

                    </div>
                    <div class="box-footer clearfix">
                    </div>
                </div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                    <ProgressTemplate>
                        <img alt="" src="images/progress_bar.gif" height="25" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </section>
        </asp:Content>