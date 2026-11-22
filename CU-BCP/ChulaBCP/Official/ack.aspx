<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ack.aspx.vb" Inherits="ack" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-coffee icon-gradient bg-primary"></i>
                                </div>
                                <div> Cookies List
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
                    </div>    

<section class="content">
                   
      <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-select icon-gradient bg-success">
            </i>Cookies   
             <div class="btn-actions-pane-right">
                     
            </div>
        </div>
        <div class="card-body">  
             <div class="row">
                  <div class="col-md-12">
                    <div class="form-group">
                    <label></label>
                        <asp:Label ID="lblCK" runat="server" Text=""></asp:Label>
                    </div>
                   </div>
                

                                </div>                                     
    </div>
   
        <div class="box-footer text-center">  
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="ทดสอบ Line Notify" />
        </div>   
      </div>                 
       
  </section>              
</asp:Content>
