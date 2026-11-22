<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="CertificateView.aspx.vb" Inherits=".CertificateView" %> 
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-medal icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Certificate
                                    <div class="page-title-subheading">Certificate เภสัชกรประจำบ้าน</div>
                                </div>
                            </div>
                        </div>
</div>

<section class="content">   
    <div class="row">
     <% For Each row As DataRow In dtCert2.Rows %>
    <div class="col-md-4">
         <a  href="../documents/certificate/<% =String.Concat(row("FilePath")) %>" target="_blank" >
         <div class="card-shadow-primary card-border mb-3 card">
          <div class="dropdown-menu-header">
            <div class="dropdown-menu-header-inner bg-primary">
              <div class="menu-header-image opacity-4" style="background-image: url('assets/images/dropdown-header/cu.jpg');"></div>
               <div class="menu-header-content">
                   <div class="icon-wrapper rounded-circle">
                       <div class="icon-wrapper-bg bg-white opacity-7"></div>
                        <i class="lnr-license text-primary"></i>
                       </div>
                   <h5 class="menu-header-title text-capitalize mb-0 fsize-3"><% =String.Concat(row("CertificateName")) %></h5>
                   <h6 class="menu-header-subtitle mb-3"></h6>                  
               </div>
            </div>
              </div>
         </div>           
        </a> 
    </div>   
    <%  Next %>     
        </div>
     <div class="col-md-12 text-center">
           <% If Request.Cookies("ROLE_STD").Value = True Then %> 
     <a href="CertificateUpload?m=cert" class="btn btn-success btn-sm"><i class="fa fa-plus-circle"></i> Upload</a>
    <%  End If %>   
</div>
    </section>
</asp:Content>
