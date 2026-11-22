<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ResultProcess.aspx.vb" Inherits=".ResultProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Confirm 
      </h1>     
    </section>

    <!-- Main content -->
    <section class="content">

      <div class="error-page">
        <h2 class="headline text-red"><i class="fa fa-warning text-red"></i></h2>
        <div class="error-content">
          <h3><asp:Label ID="lblMsg" runat="server" Text="ต้องการลบข้อมูลนี้ใช่หรือไม่?"></asp:Label></h3>

          <p>
              <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
          </p>
            <p>
                <asp:Button ID="cmdDelete" runat="server" Text="Confirm Delete" CssClass="btn btn-danger">  </asp:Button>
                &nbsp;<asp:HyperLink ID="hlnkBack" runat="server" CssClass="btn btn-primary">กลับหน้าหลัก</asp:HyperLink>
    
                </p>

        </div>
      </div>
      <!-- /.error-page -->

    </section>
    <!-- /.content -->



    <table width="100%" border="0" cellspacing="0" cellpadding="0">
     <tr>
      <td style="height:500px">&nbsp;</td>
    </tr>
  </table>
</asp:Content>
