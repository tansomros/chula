<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Person_Reg.aspx.vb" Inherits=".Person_Reg" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/pagestyles.css">
    <link rel="stylesheet" type="text/css" href="css/custyles.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>แก้ไขข้อมูลส่วนตัว (User Information)</h1>
    </section>
    <section class="content">

        <div class="box box-pink">
            <div class="box-header">
                <i class="fa fa-book"></i>

                <h3 class="box-title">กรุณากรอกข้อมูลให้ครบทุกช่อง / Please complete all fields.
                </h3>

                <div class="box-tools pull-right">
                    <asp:HiddenField ID="hdPersonID" runat="server" />
                    <asp:HiddenField ID="hdUserID" runat="server" />
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>คำนำหน้า(Title)</label>
                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>ชื่อ(First Name)</label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>นามสกุล(Last Name)</label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ชื่อเล่น (Nick Name)</label>
                            <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label>หน่วยงาน (Organization)</label>
                            <asp:TextBox ID="txtOrganizationName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>ตำแหน่ง (Position)</label>
                            <asp:DropDownList CssClass="form-control select2" ID="ddlPosition" runat="server"></asp:DropDownList>
                        </div>
                    </div>
  <div class="col-md-3">
                        <div class="form-group">
                            <label>อีเมล(E-mail)</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                  
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>โทรศัพท์(Telephone)</label>
                            <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>มือถือ(Mobile Number)</label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>                   
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>ที่อยู่ (Address)</label>
                            <asp:TextBox ID="txtAddress" runat="server" Width="370px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>ตำบล (Subdistict)</label>
                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>อำเภอ (District/City)</label>
    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>                
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>จังหวัด (State/Province)</label>
                            <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>รหัสไปรษณีย์ (Zip Code)</label>
                            <asp:TextBox ID="txtZipCode" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                        </div>
                    </div>
                </div>



                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                <tr>
                                    <td width="100" class="texttopic">รูปภาพ/Picture</td>
                                    <td>
                                        <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />
                                    </td>
                                    <td rowspan="3" valign="top">
                                        <p>
                                            <b>คำแนะนำ</b><br />
                                            1. ชนิดไฟล์รูปภาพเป็น JPG เท่านั้น และ ขนาดไม่เกิน 500 KB
                                            <br />

                                            ควรมีความกว้างประมาณ 150 pixel และความสูงประมาณ 180 pixel<br />
                                            2. ชื่อไฟล์ต้องเป็นภาษาอังกฤษ<br />
                                            3. ควรเป็นภาพทางการ และสุภาพ 
                                            <br />

                                            (งดเว้นภาพโพสต์ท่าทาง เช่น ใส่หมวก, แว่นตาแฟชั่น, ชูสองนิ้ว ฯลฯ)
                                            <br />
                                        </p>
                                        <p>
                                            <b>Guidelines: </b>
                                            <br />

                                            1. The image file must be in JPG format only, and the size should not exceed 500 KB.
                                            <br />
                                            The recommended dimensions are approximately 150 pixels wide and 180 pixels high.
                                            <br />

                                            2. The file name must be in English.
                                            <br />

                                            3. The image should be formal and appropriate.
                                            <br />
                                            (Avoid poses such as wearing hats, fashion glasses, making peace signs, etc.)
                                        </p>


                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg"
                                            Width="150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="Save" Width="100px" />
                            <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                </table>

            </div>
            <div class="box-footer clearfix">
            </div>
        </div>
    </section>
</asp:Content>


