<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="ReportViewerStudentBio.aspx.vb" Inherits=".ReportViewerStudentBio" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-print icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div><asp:Label ID="lblReportTitle" runat="server" Text="พิมพ์ประวัตินิสิต"></asp:Label>
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
</div>
     

<section class="content">
             
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-print"></i>
              <h3 class="box-title">พิมพ์ประวัตินิสิต</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
              </div>                 
            </div>
            <div class="box-body">
<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td align="center">
        <asp:Panel ID="Panel1" runat="server">
        <table border="0" cellspacing="2" cellpadding="0"> <tr>
    <td align="center" class="text11b_blue">&nbsp;</td>
  </tr>
  <tr>
    <td class="text11b_blue">Browser นี้ไม่สนับสนุนระบบแสดงผลรายงาน กรุณาเปลี่ยนไปใช้ 
        Chrome เพื่อประสิทธิภาพของรายงาน </td>
  </tr>
  <tr>
    <td align="center" class="text11b_blue"> ขออภัยในความไม่สะดวก</td>
  </tr>
 <tr>
    <td class="text11b_blue">Browser does not support display reports. Please switch to Google Chrome Browser for performance reports.</td>
  </tr>
  <tr>
    <td align="center" class="text11b_blue"> Sorry for the inconvenience</td>
  </tr>
</table>
</asp:Panel>
      </td>
  </tr>
    <tr>
        <td align="center">
         <iframe src="ReportStudentBioView.aspx" height="700" width="100%" frameborder="0" title="พิมพ์ประวัตินิสิต"></iframe>
        </td>
    </tr>
</table>
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    </section>     
</asp:Content>
