<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ResultPage.aspx.vb" Inherits=".ResultPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/uidialog.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Result Page
      </h1>     
    </section>
    <!-- Main content -->
    <section class="content">

      <div class="error-page">
        <h2 class="headline text-success"><i class="fa fa-info-circle text-success"></i></h2>
        <div class="error-content">
          <h3><asp:Label ID="lblMsg" runat="server" Text="บันทึกข้อมูลเรียบร้อย"></asp:Label></h3>

          <p>
              <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
          </p>
            <p><asp:HyperLink ID="hlnkBack" runat="server" CssClass="btn btn-warning">กลับไปแก้ไขประวัติอีกครั้ง</asp:HyperLink>
&nbsp;<asp:HyperLink ID="hlnkPrint" runat="server" CssClass="btn btn-success" Target="_blank">พิมพ์ประวัติ</asp:HyperLink>
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
