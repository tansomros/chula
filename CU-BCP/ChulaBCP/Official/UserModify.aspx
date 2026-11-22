<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="UserModify.aspx.vb" Inherits=".UserModify" %>
    <%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">    
    
            <link href="css/DragAndDrop.css" rel="stylesheet" type="text/css" />
            <script lang="javascript">
                function onUploadControlFileUploadComplete(s, e) {
                    if (e.isValid)
                        document.getElementById("uploadedImage").src = "/images/users/" + e.callbackData;
                    setElementVisible("uploadedImage", e.isValid);
                    document.getElementById("uploadedImage").className = "profile-user-img img-responsive img-circle";
                }

                function onImageLoad() {
                    var externalDropZone = document.getElementById("externalDropZone");
                    var uploadedImage = document.getElementById("uploadedImage");
                    uploadedImage.style.left = (externalDropZone.clientWidth - uploadedImage.width) / 2 + "px";
                    uploadedImage.style.top = (externalDropZone.clientHeight - uploadedImage.height) / 2 + "px";
                    setElementVisible("dragZone", false);
                    document.getElementById("uploadedImage").className = "profile-user-img img-responsive img-circle";

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
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>User Management
                                    <div class="page-title-subheading">จัดการผู้ใช้งาน </div>
                                </div>
                            </div>
                        </div>
                    </div>            

<section class="content">
     <div class="row">
          <section class="col-lg-8 connectedSortable">
              
     <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">เพิ่ม/แก้ไข</h3>   
          <div class="box-tools pull-right">      
                 
          </div>  
            <asp:HiddenField ID="Username_Old" runat="server" /> 
        </div>
        <div class="box-body">
                <div class="row">
                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>User ID</label>
                                            <asp:Label ID="lblUserID" CssClass="form-control text-center" runat="server"></asp:Label>  
                                        </div>
                                    </div>
                      <div class="col-md-4">
                                        <div class="form-group">
                                            <label> กลุ่มผู้ใช้</label>  
                      <asp:DropDownList ID="ddlUserGroup" runat="server"  CssClass="form-control select2" AutoPostBack="True">
                      <asp:ListItem Value="1">บุคลากร/ผู้ใช้งานอื่นๆ</asp:ListItem>                  
                      <asp:ListItem Value="2">นิสิต/เภสัชกรประจำบ้าน</asp:ListItem>  
                      <asp:ListItem Value="3">อาจารย์</asp:ListItem>
                      <asp:ListItem Value="4">ศิษย์เก่า</asp:ListItem>
                  </asp:DropDownList>
                                           
  </div>
                                    </div>
                    

                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>เชื่อมโยงข้อมูลบุคคล</label> 
                                            <asp:DropDownList ID="ddlPerson" runat="server" AutoPostBack="True" CssClass="form-control select2">
                  </asp:DropDownList>
  </div>
                                    </div>  
                    </div>
       <div class="row">
            <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ชื่อ</label>
   <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ></asp:TextBox>
  </div>
                                    </div>
                        <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Username</label>
   <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox> <asp:Label ID="lblAlert" runat="server" ForeColor="Red" Text="Username นี้มีอยู่ในระบบแล้ว"></asp:Label>
  </div>
                                    </div>
                        <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Password</label>
  <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"             MaxLength="15"></asp:TextBox>
  </div>
                                    </div>
                    
             </div>
       <div class="row">
           <div class="col-md-12">
                                        <div class="form-group">
                                            <label>สิทธิ์</label>
   <asp:RadioButtonList ID="optRole" runat="server" RepeatDirection="Horizontal">
                  </asp:RadioButtonList>
  </div>
                                    </div>
                   
               <div class="col-md-12 text-center">
                   <asp:CheckBox ID="chkStatus" runat="server" Text="เปิดใช้งาน/Active" Checked="true" />
      </div>
           <div class="col-md-12 text-center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="บันทึก" Width="100" />
&nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" Width="100" />
                        </div>

                    </div>       
             </div>
      
      </div>
   
              </section>
                    <section class="col-lg-4 connectedSortable">
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
                                    <img id="uploadedImage" src='<% Response.Write("/" & UserPic & "/" & Session("userimg")) %>' width="150" height="150" class="profile-user-img img-responsive img-circle" alt="" onload="onImageLoad()" />
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
                            </div>
                        </div>

                    </section>

</div>

 
</section>
</asp:Content>
