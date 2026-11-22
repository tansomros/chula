<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSiteSimple.Master" CodeBehind="NewsPrivateAdd.aspx.vb" Inherits=".NewsPrivateAdd" %>

<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>จัดการข่าวประชาสัมพันธ์ 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">News</li>
      </ol>
    </section>

<section class="content"> 
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">จัดการข่าวประชาสัมพันธ์ (บุคลากร)</h3>   
          <div class="box-tools pull-right">   
              News ID : <asp:Label ID="lblNewsID" runat="server" Text=""></asp:Label>   
          </div>  
        </div>
        <div class="box-body"> 
    <table class="table">       
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="Black" Text="เรื่อง"></asp:Label>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtHead" ErrorMessage="**จำเป็น" ForeColor="Red" 
                    ValidationGroup="D"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtHead" runat="server" BackColor="#FFFFCC" 
                     Height="25px"                     Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Category :&nbsp;<asp:DropDownList ID="ddlCategory" runat="server" CssClass="Optioncontrol">
                <asp:ListItem Selected="True" Value="QA">งานประกันคุณภาพ</asp:ListItem>
                <asp:ListItem Value="AC">งานบริการวิชาการ</asp:ListItem>
                <asp:ListItem Value="HR">งานพัฒนาบุคลากร</asp:ListItem>
                <asp:ListItem Value="ST">งานแผนยุทธศาสตร์</asp:ListItem>
                <asp:ListItem Value="IA">งานวิเทศสัมพันธ์และทุน</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>สำหรับเว็บ :
                <asp:CheckBox ID="chkTH" runat="server" Checked="True" Text="ภาษาไทย" />
&nbsp;<asp:CheckBox ID="chkEN" runat="server" Text="ภาษาอังกฤษ" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="Black" Text="เอกสารแนบ"></asp:Label><asp:Label 
                    ID="lblAttach" runat="server" ForeColor="Black" Text="ไม่มีไฟล์แนบ"></asp:Label>
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
           
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="Black" Text="เนื้อหา"></asp:Label>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtDetail" ErrorMessage="**จำเป็น" ForeColor="Red" 
                    ValidationGroup="D"></asp:RequiredFieldValidator>
                <dx:ASPxHtmlEditor ID="txtDetail" runat="server" Theme="Office2003Olive" 
                    Width="98%">
                </dx:ASPxHtmlEditor>
           
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" 
                    Theme="Office2003Olive" ToolTip="อัพโหลดไฟล์ภาพ..." Width="99%">
                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" EnableMultiSelect="True" />
                    <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" 
                        AllowRename="True" />
                    <SettingsUpload>
                        <AdvancedModeSettings EnableMultiSelect="True">
                        </AdvancedModeSettings>
                    </SettingsUpload>
                </dx:ASPxFileManager>
            </td>
        </tr>       
        <tr>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" ForeColor="Red" 
                    Text="ยกเลิกการแสดงข่าว" />
            </td>
        </tr>       
    </table>

            
             </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
                <asp:Button ID="bttSave" runat="server" CssClass="buttonSave" 
                    Text="บันทึก" ValidationGroup="D" />
                &nbsp;</div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    

    </section>
</asp:Content>
