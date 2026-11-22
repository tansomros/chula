<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="RegisterMenu.aspx.vb" Inherits=".RegisterMenu" %> 
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="fa fa-registered icon-gradient bg-primary"></i>
                                </div>
                                <div>Register
                                    <div class="page-title-subheading">ลงทะเบียน</div>
                                </div>
                            </div>
                        </div>
</div>

<section class="content">
    <div class="row">
      <section class="col-lg-12 connectedSortable">
        <div class="main-card mb-12 card">
            <div class="card-header">
                <i class="header-icon lnr-select icon-gradient bg-success"></i>เลือกหัวข้อที่ต้องการลงทะเบียน
                <div class="box-tools pull-right">
                </div>                 
            </div>
            <div class="card-body">
                <div class="row">
                     <% For Each row As DataRow In dtReg.Rows %>
                          <div class="col-md-6">
                                <a href="<% =String.Concat(row("TopicType")) %>?m=reg&tid=<% =String.Concat(row("UID")) %>">
                                    <div class="card mb-3 widget-content bg-blue-gradient">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left">
                                            <div class="text-white"><% =String.Concat(row("TopicName")) %></div> 
                                        </div>                                       
                                    </div>
                                </div> 
                                </a>
                            </div>
                      <% Next %> 
                </div>
            </div> 
            </div>
      </section>
    </div>
</section>
</asp:Content>
