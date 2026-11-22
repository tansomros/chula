<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DocumentMenu.aspx.vb" Inherits=".DocumentMenu" %> 
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="fa fa-file-word icon-gradient bg-primary"></i>
                                </div>
                                <div>เลือกสร้างเอกสาร
                                    <div class="page-title-subheading">หนังสือราชการ/จดหมาย/บันทึกข้อความ</div>
                                </div>
                            </div>
                        </div>
</div>

<section class="content">
    <div class="row">
      <section class="col-lg-12 connectedSortable">
        <div class="main-card mb-12 card">
            <div class="card-header">
                <i class="header-icon lnr-printer icon-gradient bg-success"></i>เลือกเอกสารที่ต้องการสร้าง
                <div class="box-tools pull-right">
                </div>                 
            </div>
            <div class="card-body">
                <div class="row">
                     
                         <asp:DataList ID="datalist_DocumentOfficer" runat="server" RepeatColumns="2">
                             <ItemTemplate>
                                                     
                        <div class="form-group">
                                <a href="DocumentGen?m=v&id=<%#DataBinder.Eval(Container.DataItem, "UID") %>"  class="font-icon-button"><i class="fa fa-file-word" aria-hidden="true"></i> <%#DataBinder.Eval(Container.DataItem, "DocumentName") %> </a> 
                                 </div>
                    
                             </ItemTemplate>
                         </asp:DataList>

                     
                    <%--<div class="col-md-6">
                        <div class="form-group">
                        <a href="DocumentGen?m=v&id=1" class="font-icon-button"><i class="fa fa-file-word" aria-hidden="true"></i> จดหมายขอนำส่งบัตรลงทะเบียน</a> 
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                        <a href="DocumentGen?m=v&id=2" class="font-icon-button"><i class="fa fa-file-word" aria-hidden="true"></i> บันทึกข้อความขอนำส่งบัตรลงทะเบียน</a> 
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                        <a href="DocumentGen?m=v&id=3" class="font-icon-button"><i class="fa fa-file-word" aria-hidden="true"></i> จดหมายขอนำส่งใบสมัครสอบประเมินผล Bedside</a> 
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                        <a href="DocumentGen?m=v&id=4" class="font-icon-button"><i class="fa fa-file-word" aria-hidden="true"></i> บันทึกข้อความขอนำส่งใบสมัครสอบประเมินผล Bedside</a> 
                        </div>
                    </div>--%>

                </div>
            </div>
        </div>       
      </section>
    </div>
</section>
</asp:Content>
