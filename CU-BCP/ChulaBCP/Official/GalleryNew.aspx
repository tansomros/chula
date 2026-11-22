<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSiteSimple.Master" CodeBehind="GalleryNew.aspx.vb" Inherits=".GalleryNew" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>Create Gallery
        <small>สร้างอัลบั้มภาพกิจกรรม</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">สร้างอัลบั้มภาพกิจกรรม</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">สร้างอัลบั้มภาพกิจกรรม</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body">
                  <table align="center" cellpadding="2" cellspacing="4"  class="table" >         
          <tr>
              <td width="100">&nbsp;Album ID</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
 <tr>
     <td width="100">แสดงในเว็บ</td>

            <td>
                <asp:CheckBox ID="chkTH" runat="server" Checked="True" Text="ภาษาไทย" />
&nbsp;<asp:CheckBox ID="chkEN" runat="server" Text="ภาษาอังกฤษ" />
            </td>
        </tr>
          <tr>
              <td>ชื่ออัลบั้ม (ไทย)</td>
              <td>
                  <asp:TextBox ID="txtNameTH" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
              <td>ชื่ออัลบั้ม (EN)</td>
              <td>
                  <asp:TextBox ID="txtNameEN" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
              <td>รายละเอียด (ไทย)</td>
              <td>
                  <asp:TextBox ID="txtTitleTH" runat="server" MaxLength="100" Width="95%" Height="100px" TextMode="MultiLine"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
              <td>รายละเอียด (EN)</td>
              <td>
                  <asp:TextBox ID="txtTitleEN" runat="server" MaxLength="100" Width="95%" Height="100px" TextMode="MultiLine"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
            <td>ภาพปก</td>
            <td>
                <asp:Image ID="imgCover" runat="server" Height="111px" ImageUrl="../images/gallery/cover0.jpg" Width="260px" />
              </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
              &nbsp;<small>(รูป .jpg ขนาด 260 x 111 )</small></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td class="textnotic">
                <asp:HyperLink ID="hlnkCover" runat="server" Target="_blank">[hlnkDoc]</asp:HyperLink>
                 </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="cmdSave" runat="server" CssClass="buttonlink" Text="บันทึก" Width="100px" />
              &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="buttonlink" Text="ยกเลิก" Width="100px" />
              </td>
          </tr>
                      </table>

 </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->   
          <asp:Panel ID="pnUpload" runat="server">
        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">Upload รูปภาพ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body">
                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Theme="SoftOrange">
            <settings rootfolder="~\Images\" thumbnailfolder="~\Images\Thumb\" />
                    <settings allowedfileextensions=" .jpg, .jpeg, .gif, .txt, .png" 
                        enablemultiselect="True" rootfolder="~\Images\" 
                        thumbnailfolder="~\Images\Thumb\" />
                    <SettingsEditing AllowCopy="True" AllowCreate="True" AllowDelete="True" 
                        AllowDownload="True" AllowMove="True" AllowRename="True" />
                    <SettingsUpload>
                        <AdvancedModeSettings EnableMultiSelect="True">
                        </AdvancedModeSettings>
                    </SettingsUpload>
                </dx:ASPxFileManager>
            </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->  
</asp:Panel>
  </section>                  

</asp:Content>
