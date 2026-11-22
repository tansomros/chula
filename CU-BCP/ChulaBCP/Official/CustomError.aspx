<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="CustomError.aspx.vb" Inherits="CustomError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <section class="content-header">
      <h1>
        Notice Page
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>       
        <li class="active">Notice Page</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">

      <div class="error-page">
        <h2 class="headline text-red"><i class="fa fa-warning text-red"></i></h2>

        <div class="error-content">
          <h3>ท่านไม่มีสิทธิ์เข้าถึงข้อมูลส่วนนี้</h3>
          <p>โปรดลอง Login เข้าสู่ระบบใหม่อีกครั้ง 
              <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="../Login.aspx" CssClass="buttonSave">คลิกที่นี่เพื่อ Login</asp:LinkButton>
            </p>
          

        </div>
      </div>
      <!-- /.error-page -->

    </section>
    <!-- /.content --> 
</asp:Content>
