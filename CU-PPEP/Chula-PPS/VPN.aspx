<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VPN.aspx.vb" Inherits=".VPN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>
        VPN Account
      </h1>
       
    </section>

    <!-- Main content -->
<section class="content">
     <div class="error-page">
        <h2 class="headline text-yellow"> VPN</h2>

        <div class="error-content">
          <h3><i class="fa fa-warning text-yellow"></i>สำหรับการใช้งาน Internet คณะเภสัชศาสตร์ จุฬาฯ</h3>

          <p>            
               Username : <asp:Label ID="lblVPNUser" runat="server" Text=""></asp:Label>   
          </p>
            <p>
             Password : <asp:Label ID="lblVPNPass" runat="server" Text=""></asp:Label>
          </p>          
        </div>
        <!-- /.error-content -->
      </div>
</section>
</asp:Content>
