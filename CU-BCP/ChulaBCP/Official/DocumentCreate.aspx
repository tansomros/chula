<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DocumentCreate.aspx.vb" Inherits=".DocumentCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-pen icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>สร้างเอกสาร
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
                    </div>
<section class="content">     
         <div class="row">    
            <section class="col-lg-12 connectedSortable">
<div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-pencil icon-gradient bg-success">
            </i>ระบุข้อมูลเงื่อนไขเอกสาร                 
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdStudentID" runat="server" />
            <asp:HiddenField ID="hdRequestID" runat="server" />
              </div>                 
            </div>
            <div class="card-body">
    <div class="row">
              <div class="col-md-2">
                <div class="form-group">
                  <label>เลขที่</label>
                  <asp:TextBox CssClass="form-control text-center" ID="txtNo" runat="server"></asp:TextBox>
                </div>
              </div>             
                    <div class="col-md-5">
                <div class="form-group">
                  <label>ลงวันที่</label>
                  <asp:TextBox CssClass="form-control text-center" ID="txtDate" runat="server"></asp:TextBox>
                </div>
              </div>
         <div class="col-md-5">
                <div class="form-group">
                  <label>ชั้นปีที่</label>
                  <asp:DropDownList CssClass="form-control text-center" ID="ddlLevel" runat="server"></asp:DropDownList>
                </div>
              </div>
          <div class="col-md-5">
                <div class="form-group">
                  <label>สาขา</label>
                  <asp:DropDownList CssClass="form-control text-center" ID="ddlMajor" runat="server"></asp:DropDownList>
                </div>
              </div>
                </div>      
    </div>
</div>
   </section>
   </div>
    <asp:Panel ID="pnCommittee" runat="server">
         <div class="row">
      <section class="col-lg-12 connectedSortable">   
      
 
</section>
   </div>               
  </asp:Panel>  

        <div align="center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Next" Width="100px" />
        <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-success" Text="พิมพ์" Width="100px" />
            <asp:Button ID="cmdDelete"  runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" />
        </div>

</section>
</asp:Content>
 

