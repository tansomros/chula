<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="PersonInfo.aspx.vb" Inherits="PersonInfo" %>

    <%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

        <asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">

            <link href="css/DragAndDrop.css" rel="stylesheet" type="text/css" />

            <script lang="javascript">
                function onUploadControlFileUploadComplete(s, e) {
                    if (e.isValid)
                        document.getElementById("uploadedImage").src = "/images/persons/" + e.callbackData;
                    setElementVisible("uploadedImage", e.isValid);
                    document.getElementById("uploadedImage").className = "profile-user-img img-responsive";
                }

                function onImageLoad() {
                    var externalDropZone = document.getElementById("externalDropZone");
                    var uploadedImage = document.getElementById("uploadedImage");
                    uploadedImage.style.left = (externalDropZone.clientWidth - uploadedImage.width) / 2 + "px";
                    uploadedImage.style.top = (externalDropZone.clientHeight - uploadedImage.height) / 2 + "px";
                    setElementVisible("dragZone", false);
                    document.getElementById("uploadedImage").className = "profile-user-img img-responsive";

                }

                function setElementVisible(elementId, visible) {
                    document.getElementById(elementId).className = visible ? "" : "hidden";
                }
            </script>


        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-user icon-gradient bg-mean-fruit"></i>
                        </div>
                        <div>ประวัติส่วนตัว
                            <div class="page-title-subheading">แก้ไขข้อมูลประวัติส่วนตัว.</div>
                        </div>
                    </div>
                </div>
            </div>

            <section class="content">
                <div class="row">  
                    <section class="col-lg-3 connectedSortable">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h2 class="box-title">รูปประจำตัว</h2>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse">
              <i class="fa fa-minus"></i></button>

                                </div>
                            </div>
                            <div class="box-body" align="center">

                                <div id="externalDropZone" class="dropZoneExternal">
                                    <div id="dragZone">
                                        <span class="dragZoneText">ลากรูปมาวางที่นี่</span>
                                    </div>
                                    <img id="uploadedImage" src='<% Response.Write("../" & PersonPic & Session("personimg")) %>' height="150"   class="profile-user-img img-responsive" alt="" onload="onImageLoad()" />
                                    <div id="dropZone" class="hidden">
                                        <span class="dropZoneText">Drop an image here</span>
                                    </div>
                                </div>
                                <dx:ASPxUploadControl ID="UploadControl" ClientInstanceName="UploadControl" runat="server" UploadMode="Auto" AutoStartUpload="True" Width="80%" ShowProgressPanel="True" CssClass="uploadControl" DialogTriggerID="externalDropZone" OnFileUploadComplete="UploadControl_FileUploadComplete">
                                    <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" ExternalDropZoneID="externalDropZone" DropZoneText="" />
                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg, .jpeg, .gif, .png" ErrorStyle-CssClass="validationMessage" />
                                    <BrowseButton Text="เลือกรูปสำหรับอัพโหลด..." />
                                    <DropZoneStyle CssClass="uploadControlDropZone" />
                                    <ProgressBarStyle CssClass="uploadControlProgressBar" />
                                    <ClientSideEvents DropZoneEnter="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', true); }" DropZoneLeave="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', false); }" FileUploadComplete="onUploadControlFileUploadComplete">
                                    </ClientSideEvents>
                                </dx:ASPxUploadControl>



                                <div class="pull-left"> </div>
                                <div class="pull-right"> </div>

                            </div>
                            <div class="box-footer">
                                <asp:Label ID="lblNoSuccess" runat="server" ForeColor="Red" Text="Upload รูปไม่สำเร็จ กรุณาตรวจสอบไฟล์ แล้วลองใหม่อีกครั้ง" Visible="False"></asp:Label>
                                <br />
                            </div>
                        </div>
                        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">ประเภทบุคคล</h2>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse">
              <i class="fa fa-minus"></i></button>            
          </div>
            </div>
        <div class="box-body mailbox-messages">   
            <div class="row">
                      <div class="col-md-12">
        <div class="form-group mailbox-messages">          
            <asp:CheckBox ID="chkExclusive" runat="server" Text="คณะกรรมการบริหารหลักสูตร" /><br />
            <asp:CheckBox ID="chkFaculty" runat="server" Text="คณาจารย์" /><br />
            <asp:CheckBox ID="chkTeacher" runat="server" Text="อาจารย์ประจำหลักสูตร" /><br />
            <asp:CheckBox ID="chkStaff" runat="server" Text="เจ้าหน้าที่/บุคลากร" /><br />
            <asp:CheckBox ID="chkAlumni" runat="server" Text="ศิษย์เก่า" />
        </div>
    </div>   
             </div>   
             </div> 
      </div>
                        </section>
                    <section class="col-lg-9 connectedSortable">

                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h2 class="box-title">ข้อมูลทั่วไป</h2>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse">
              <i class="fa fa-minus"></i></button>

                                </div>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>ID</label>
                                            <asp:Label ID="lblPersonID" CssClass="form-control text-center" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>รหัสพนักงาน</label>
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" placeholder="" CssClass="form-control text-center"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>คำนำหน้าชื่อ</label>
                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>ชื่อภาษาไทย</label>
                                            <asp:TextBox ID="txtFnameTH" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>นามสกุลภาษาไทย</label>
                                            <asp:TextBox ID="txtLnameTH" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>ชื่อภาษาอังกฤษ</label>
                                            <asp:TextBox ID="txtFnameEN" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>นามสกุลภาษาอังกฤษ</label>
                                            <asp:TextBox ID="txtLnameEN" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>เบอร์โทร</label>
                                            <asp:TextBox ID="txtTel" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>อีเมล์</label>
                                            <asp:TextBox ID="txtMail" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                      <div class="col-md-6">
                                        <div class="form-group">
                                            <label>สถานที่ทำงานปัจจุบัน</label>
                                            <asp:TextBox ID="txtWorkplace" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                     <div class="col-md-4">
 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMail" ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                         </div>
                                </div>
                            </div>                           
                        </div>
                
               
<div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">ข้อมูลตำแหน่ง/สังกัด/คุณวุฒิ</h2>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
            </div>
        <div class="box-body"> 
            <div class="row">
             <div class="col-md-6">
                                    <div class="form-group">
                                        <label>ตำแหน่งงาน</label>
                                        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control select2">
                </asp:DropDownList>
                <asp:Label ID="lblPositionName" runat="server" Text=""></asp:Label>                                       
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><asp:Label ID="lblMajor_Label" runat="server" Text="สาขาวิชา"></asp:Label></label>
                                         <asp:DropDownList ID="ddlMajor" runat="server" CssClass="form-control select2">
                </asp:DropDownList><asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2">
                </asp:DropDownList>
                         <asp:Label ID="lblMajorName" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                 <div class="col-md-5">
                                    <div class="form-group">
                                        <label><asp:Label ID="lblPositionPrefix_Label" runat="server" Text="ตำแหน่งทางวิชาการ"></asp:Label></label>
                                          <asp:DropDownList ID="ddlPositionPrefix" runat="server" CssClass="form-control select2">
                </asp:DropDownList><asp:Label ID="lblPositionEduName" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>คุณวุฒิ</label>
                                         <asp:TextBox ID="txtEducated" runat="server"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                  <div class="col-md-3">
                                    <div class="form-group">
                                        <label>สถานภาพ</label>
 <asp:DropDownList ID="ddlWorkStatus" runat="server"  CssClass="form-control select2">
                    <asp:ListItem Selected="True" Value="A">ทำงานอยู่</asp:ListItem>
                    <asp:ListItem Value="L">ลาศึกษาต่อ</asp:ListItem>
                    <asp:ListItem Value="R">เกษียณ</asp:ListItem>
                    <asp:ListItem Value="S">ลาออก</asp:ListItem>
                    <asp:ListItem Value="X">พ้นสภาพ</asp:ListItem>
                </asp:DropDownList>
                                    </div>
                                </div>
            </div>            
             </div> 
      </div>
                        </section>
                  
                </div>
                <div class="mb-3 card">
                                            <div class="card-header card-header-tab-animation">
                                                <ul class="nav nav-justified">
                                                    <li class="nav-item"><a data-toggle="tab" href="#tab-eg115-0" class="nav-link active">ประวัติการศึกษา</a></li>  
                                                    <li class="nav-item"><a data-toggle="tab" href="#tab-eg115-1" class="nav-link">ผลงานวิจัย</a></li>
                                                    <li class="nav-item"><a data-toggle="tab" href="#tab-eg115-2" class="nav-link">รางวัลที่ได้รับ</a></li>
                                                    <li class="nav-item"><a data-toggle="tab" href="#tab-eg115-3" class="nav-link">ประวัติการอบรม</a></li>
                                                </ul>
                                            </div>
                                            <div class="card-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab-eg115-0" role="tabpanel">
                                                                          
                            <asp:UpdatePanel ID="UpdatePanelEduAdd" runat="server">
                                <ContentTemplate>
                                    <table class="table no-border">
                                        <tr>
                                            <th>
                                                <asp:HiddenField ID="hdfEduID" runat="server" />
                                            </th>
                                            <th>วุฒิการศึกษา</th>
                                            <th>สาขาวิชา</th>
                                            <th>สถาบัน/มหาวิทยาลัย</th>
                                            <th width="80">ปีที่จบ</th>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px;">ภาษาไทย</td>
                                            <td>
                                                <asp:TextBox ID="txtDegreeTH" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMajorTH" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUniversityTH" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEduYearTH" runat="server" MaxLength="4" cssclass="form-control text-center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>English</td>
                                            <td>
                                                <asp:TextBox ID="txtDegreeEN" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMajorEN" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUniversityEN" runat="server" cssclass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEduYearEN" runat="server" MaxLength="4" cssclass="form-control text-center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" class="text-center">
                                                <asp:Button ID="cmdAddEdu" runat="server" CssClass="btn btn-success" Text="เพิ่ม" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grdEdu" EventName="RowCommand">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                       
                            <asp:UpdatePanel ID="UpdatePanelEducation" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdEdu" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" cssclass="table table-hover" Width="100%">
                                        <RowStyle BackColor="White" VerticalAlign="Top" />
                                        <columns>
                                            <asp:TemplateField HeaderText="วุฒิการศึกษา">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DegreeTH") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label3" runat="server" CssClass="NameEN" Text='<%# DataBinder.Eval(Container.DataItem, "DegreeEN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สาขาวิชา">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MajorTH") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label5" runat="server" CssClass="NameEN" Text='<%# DataBinder.Eval(Container.DataItem, "MajorEN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถาบัน">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UniversityTH") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label7" runat="server" CssClass="NameEN" Text='<%# DataBinder.Eval(Container.DataItem, "UniversityEN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ปีที่จบ">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "YearTH") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblYEN" runat="server" CssClass="NameEN" Text='<%# DataBinder.Eval(Container.DataItem, "YearEN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="แก้ไข">
                                                <itemtemplate>
                                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-edit.png" />
                                                </itemtemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ลบ">
                                                <itemtemplate>
                                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-delete.png" />
                                                </itemtemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </columns>
                                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <headerstyle CssClass="gridheader" Font-Bold="True" VerticalAlign="Middle" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="#EBEBEB" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmdAddEdu" EventName="Click">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                                  
                                                    </div>
                                                    <div class="tab-pane" id="tab-eg115-1" role="tabpanel">
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>ปี</label>
                                                                    <asp:TextBox ID="txtRYear" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdfResearchID" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>ประเภทงานวิจัย</label>
                                                                    <asp:DropDownList ID="ddlResearchType" runat="server" Width="100%" CssClass="form-control select2"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>งานวิจัยด้าน</label>
                                                                    <asp:DropDownList ID="ddlResearchGroup" runat="server" Width="100%" CssClass="form-control select2">
                                                                        <asp:ListItem Selected="True" Value="1">ด้านวิชาชีพ</asp:ListItem>
                                                                        <asp:ListItem Value="2">ด้านเภสัชศาสตร์</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>การนำเสนอ</label>
                                                                    <asp:DropDownList ID="ddlResearchPresented" Width="100%" runat="server" CssClass="form-control select2">
                                                                        <asp:ListItem Selected="True" Value="1">ตีพิมพ์</asp:ListItem>
                                                                        <asp:ListItem Value="2">Oral/Poster</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>ชื่องานวิจัย</label>
                                                                    <asp:TextBox ID="txtRTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>บทคัดย่อ</label>
                                                                   <asp:FileUpload ID="FileUploadFile" runat="server" /><br />
                      <asp:HyperLink ID="hlnkAttachs" runat="server" Target="_blank"></asp:HyperLink>
&nbsp;<asp:Button ID="cmdDelFile" runat="server" CssClass="btn btn-danger" Text="ลบเอกสาร" />

                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 text-center">
                                                                <div class="form-group">
                                                                    <label></label>
                                                                    <asp:Button ID="cmdSaveResearch" runat="server" CssClass="btn btn-success" Text="บันทึกงานวิจัย" /> &nbsp;
                                                                    <asp:Button ID="cmdResearchCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>รายการงานวิจัย</label>
                                                                    <asp:UpdatePanel ID="UpdatePanelResearch" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="grdResearch" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None">
                                                                                <RowStyle BackColor="White" VerticalAlign="Top" />
                                                                                <columns>
                                                                                    <asp:BoundField DataField="nRow" HeaderText="No.">
                                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="ชื่องานวิจัย">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TitleTH") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="RYear" HeaderText="ปี">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PresentName" HeaderText="การนำเสนอ">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="GroupName" HeaderText="ด้าน">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="ประเภท">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TypeName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="แก้ไข">
                                                                                        <itemtemplate>
                                                                                            <asp:ImageButton ID="imgEditR" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-edit.png" />
                                                                                        </itemtemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ลบ">
                                                                                        <itemtemplate>
                                                                                            <asp:ImageButton ID="imgDelR" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-delete.png" />
                                                                                        </itemtemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                </columns>
                                                                                <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                <headerstyle CssClass="gridheader" Font-Bold="True" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                <EditRowStyle BackColor="#2461BF" />
                                                                                <AlternatingRowStyle BackColor="#EBEBEB" />
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveResearch" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>                       
                                                    </div>
                                                    <div class="tab-pane" id="tab-eg115-2" role="tabpanel">
                                                        <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>รายการงานวิจัย</label>
                                        <asp:TextBox ID="txtAward" runat="server" Height="200px" cssclass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <small> กรุณาใส่รางวัลที่ได้รับเป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) รายละเอียด ชื่อผู้รับ ชื่อรางวัล ปี</small>
                                </div>
                            </div>

                                                    </div>
                                                    <div class="tab-pane" id="tab-eg115-3" role="tabpanel">
                                                          <div class="row">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>ปี</label>
                                    <asp:TextBox ID="txtTrainYear" runat="server" cssclass="form-control text-center"></asp:TextBox>
                                    <asp:HiddenField ID="hdfTrainUID" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>หมวดการอบรม</label>
                                    <asp:RadioButtonList ID="optTrainCate" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="IN" Selected="True">ในปรเทศ</asp:ListItem>
                                        <asp:ListItem Value="OUT">ต่างประเทศ</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>การอบรมด้าน</label>
                                    <asp:DropDownList ID="ddlTrainGroup" runat="server" Width="100%" CssClass="form-control select2">
                                        <asp:ListItem Selected="True" Value="1">ด้านวิชาชีพ</asp:ListItem>
                                        <asp:ListItem Value="2">ด้านเภสัชศาสตร์</asp:ListItem>
                                        <asp:ListItem Value="3">ด้านบริหารและประกันคุณภาพ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>ประเภท</label>
                                    <asp:DropDownList ID="ddlTrainType" runat="server" Width="100%" CssClass="form-control select2">
                                        <asp:ListItem Value="0" Selected="True">-</asp:ListItem>
                                        <asp:ListItem Value="1">การเรียนการสอน</asp:ListItem>
                                        <asp:ListItem Value="2">การวัดและประเมินผล</asp:ListItem>
                                        <asp:ListItem Value="3">การวิจัยด้านเภสัชศาสตร์</asp:ListItem>
                                        <asp:ListItem Value="4">การอบรมจิตตปัญญาศึกษา</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>หัวข้อการอบรม</label>
                                    <asp:TextBox ID="txtTrainTopic" runat="server" cssclass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="cmdSaveTrain" runat="server" CssClass="btn btn-success" Text="บันทึกการอบรม" />
                                <asp:Button ID="cmdTrainCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <h6>รายการประวัติการอบรม</h6>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdTrain" runat="server" AutoGenerateColumns="False" CellPadding="0" cssclass="table table-hover" ForeColor="#333333" GridLines="None">
                                            <RowStyle BackColor="White" VerticalAlign="Top" />
                                            <columns>
                                                <asp:TemplateField HeaderText="ปี">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="หัวข้อการอบรม">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TopicName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="หมวด" DataField="CategoryName">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="ด้าน" DataField="GroupName">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="แก้ไข">
                                                    <itemtemplate>
                                                        <asp:ImageButton ID="imgEditT" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-edit.png" />
                                                    </itemtemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลบ">
                                                    <itemtemplate>
                                                        <asp:ImageButton ID="imgDelT" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-delete.png" />
                                                    </itemtemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </columns>
                                            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <headerstyle CssClass="gridheader" Font-Bold="True" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="#EBEBEB" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cmdSaveTrain" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>       
         
                <div align="center">
                     <div class="col-md-12">    
                     <asp:Panel ID="pnAlert" Visible="false" runat="server">
                                    <div class="alert alert-danger alert-dismissible">
                                        <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                                        <p>
                                            <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </asp:Panel>
                         </div>
                     <div class="col-md-12">        
        <div  class="mailbox-messages">
            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />
        </div>
</div>
 <div class="col-md-12">  
                    <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="บันทึก" Width="100px" /> &nbsp;
                    <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" Width="100px" />
                </div>
                    </div>
            </section>
        </asp:Content>