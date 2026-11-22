<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DocumentUpload.aspx.vb" Inherits=".DocumentUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Document Upload
                                    <div class="page-title-subheading">จัดการส่งเอกสาร</div>
                                </div>
                            </div>
                        </div>
         </div>
<section class="content">     
         <div class="row">    
            <section class="col-lg-12 connectedSortable">
<div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>ข้อมูลเภสัชกรประจำบ้าน
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdStudentID" runat="server" />
            <asp:HiddenField ID="hdDocumentID" runat="server" />
              </div>                 
            </div>
            <div class="card-body">
    <div class="row">
              <div class="col-md-2">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-6">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStudentName" runat="server"></asp:Label>
                </div>
              </div>
                </div>

                  <div class="row">
    <div class="col-md-2">
                <div class="form-group">
                  <label>ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control text-center"></asp:DropDownList>
                </div>
              </div>   
              <div class="col-md-2">
                <div class="form-group">
                  <label>ภาคการศึกษา</label>
                    <asp:RadioButtonList ID="optTerm" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ที่ 1</asp:ListItem>
                        <asp:ListItem Value="2">ที่ 2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              </div>
                  
               <div class="col-md-3">
                <div class="form-group">
                  <label>วันที่</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtRegDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>



            
                </div>
     <div class="row">  
                    <div class="col-md-6">
                <div class="form-group">
                  <label>เลือกงานที่ต้องการส่ง</label>
                    <asp:DropDownList ID="ddlDocName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>     
                   <div class="col-md-6">
                     <div class="form-group">
                        <label>File Upload </label> 
                         <br />
                      <asp:FileUpload ID="FileUploadFile" runat="server" /> 
                         &nbsp;</div>
                       </div>     
       </div>
                      <div class="row"> 
              <div class="col-md-12">
                <div class="form-group">
                  <label></label>                   
                    <asp:HyperLink ID="hlnkDocument" Target="_blank" runat="server"></asp:HyperLink>
                </div>
              </div>   
                </div>

                
            
            </div>
</div>
   </section>
   </div>   

        <div align="center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="100px" />       
            <asp:Button ID="cmdDelete"  runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" />
        </div>

</section>
</asp:Content>
 

