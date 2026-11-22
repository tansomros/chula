<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="CertificateUpload.aspx.vb" Inherits=".CertificateUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Certificate Upload
                                    <div class="page-title-subheading">จัดการไฟล์ Certificate</div>
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

    </div>
</div>
   </section>
   </div>
   
       <div class="row">
      <section class="col-lg-12 connectedSortable">   
          <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>Upload                    
                 <div class="box-tools pull-right">                      
              </div>                 
            </div>
            <div class="card-body">
                  <div class="row">  
                    <div class="col-md-6">
                <div class="form-group">
                  <label>เลือกชื่อใบรับรอง</label>
                    <asp:DropDownList ID="ddlDocName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>     
                   <div class="col-md-6">
                     <div class="form-group">
                        <label>Upload</label> 
                         <br />
                      <asp:FileUpload ID="FileUploadFile" runat="server" CssClass="btn btn-outline-primary" /> 
                         &nbsp;<asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-success" Text="Upload" />
                     </div>
                       </div>     
       </div>
                      <div class="row"> 
              <div class="col-md-12">
                <div class="form-group">
                  <label></label>                   
                    <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-hover" ForeColor="#333333" GridLines="None" Width="100%">
                        <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                        <columns>
                            <asp:BoundField DataField="nRow" HeaderText="No." >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:HyperLinkField HeaderText="Certificate Name" DataNavigateUrlFields="CertificateLink" DataTextField="CertificateName" Target="_blank" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel_Doc" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" />
                                </itemtemplate>
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" VerticalAlign="Middle" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                   
                </div>
              </div>   
                </div>

  </div>
</div>
      </section>
   </div>   
    </section>
</asp:Content>
 

