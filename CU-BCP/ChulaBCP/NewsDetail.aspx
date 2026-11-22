<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="NewsDetail.aspx.vb" Inherits="NewsDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="containerPage">
<section class="content">  
       <section class="content-header">
      <h1 class="news-heading text-green">ข่าวประชาสัมพันธ์
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ข่าวประชาสัมพันธ์</li>
      </ol>
    </section> 
    <div class="box box-success">
        <div class="box-header no-border">
          <h2 class="box-title"><asp:Label ID="lblSubject" runat="server"></asp:Label></h2>         
        </div>
        <div class="box-body">      
    
     

    <table style="width:100%;">
        <tr>
            <td style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
             
                <asp:Label ID="lblDetail" runat="server" style="color: #000000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                &nbsp;&nbsp;
                &nbsp;<br />
                <img alt="" src="Images/icon/attach.png" /> <asp:Label 
                    ID="lblAttach" runat="server" ForeColor="Black" Text="ไม่มีไฟล์แนบ" 
                    style="font-size: small"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <img  src="Images/icon/update.png" /> <asp:Label 
                    ID="lblTextUpdated" runat="server" Font-Italic="True" 
                    style="font-size: small">ปรับปรุงเมื่อ</asp:Label>
                &nbsp;<asp:Label ID="lblUpdated" runat="server" Font-Italic="True" 
                    style="font-size: small"></asp:Label>
                &nbsp;<asp:Label ID="lblTextRead" runat="server" Font-Italic="True" 
                    style="font-size: small">อ่านแล้ว</asp:Label>
                &nbsp;<asp:Label ID="lblRead" runat="server" Font-Italic="True" 
                    style="font-size: small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                &nbsp;</td>
        </tr>
    </table>
 
          
        <div class="box-footer">

        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->  
    </div>

</section>
          </div>
</asp:Content>
