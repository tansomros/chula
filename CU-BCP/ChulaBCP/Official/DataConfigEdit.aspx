<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DataConfigEdit.aspx.vb" Inherits=".DataConfigEdit" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
      <title> </title>
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
      <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-config icon-gradient bg-primary"></i>
            </div>
            <div>Data Configuration Edit<div class="page-title-subheading">แก้ไขค่าเริ่มต้นระบบ</div></div>
        </div>
   </div>
</div>
<section class="content">
<div class="row">
        <div class="box box-primary">
        <div class="box-header">
          <i class="fa fa-edit"></i>
          <h3 class="box-title">จัดการค่าเริ่มต้นอื่นๆ</h3>          
        </div>
        <div class="box-body">
          <div class="row">
                <div class="col-md-1">
              <div class="form-group">
                <label>ID</label>
                <asp:label ID="lblUID" runat="server" cssclass="form-control text-center" BackColor="WhiteSmoke" ></asp:label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-group">
                <label>Code</label>
                <asp:label ID="lblCode" runat="server" cssclass="form-control text-center" BackColor="WhiteSmoke"></asp:label>
              </div>
            </div>
            <div class="col-md-9">
              <div class="form-group">
                <label>Name</label>
                   <asp:label ID="lblName" runat="server" cssclass="form-control text-center" BackColor="WhiteSmoke"></asp:label> 
              </div>
            </div>   
      </div>        
   <div class="row">
              <div class="col-md-6">
              <div class="form-group">
                <label>Description</label>
                <asp:TextBox ID="txtDesc" runat="server" cssclass="form-control"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-6">
              <div class="form-group">
                <label>Value</label>
                <asp:TextBox ID="txtValues" runat="server" cssclass="form-control"></asp:TextBox>
              </div>
            </div>    
          </div>
        </div>
        <div class="box-footer text-center">
             <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Width="100" Text="บันทึก"></asp:Button>
                <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Width="100" Text="ยกเลิก"></asp:Button>
        </div>
      </div>
</div>
</section>
  </asp:Content>