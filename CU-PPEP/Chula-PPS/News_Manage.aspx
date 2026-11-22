<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="News_Manage.aspx.vb" Inherits=".News_Manage" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<section class="content-header">
      <h1>ข่าวประกาศ</h1>   
    </section>

<section class="content">  

         <div class="box box-pink">

            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข ข่าวประกาศ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table width="100%"> 
  <tr>
    <td>        
        <table width="100%">
      <tr>
        <td width="120">ID :</td>
        <td><table >
              <tr>
                <td width="200"><asp:Label ID="lblID" runat="server">0</asp:Label></td>
                <td width="50" align="center">ลำดับ</td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" Width="50px">99</asp:TextBox>
                  </td>
              </tr>
            </table></td>
      </tr>
      <tr>
        <td>หัวเรื่อง :</td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox>
          </td>
      </tr>
      <tr> 
        <td>ประกาศไว้ที่หน้าแรก :</td>
        <td class="mailbox-messages">
            <asp:CheckBox ID="chkFistPage" runat="server" Text="ประกาศไว้ที่หน้าแรก" />
          </td>
      </tr>
      <tr>
        <td>ประเภท :</td>
        <td class="mailbox-messages">
            <asp:RadioButtonList ID="optType" runat="server" RepeatDirection="Horizontal" 
                AutoPostBack="True">
                <asp:ListItem Value="URL">URL Link</asp:ListItem>
                <asp:ListItem Value="UPL">File Upload</asp:ListItem>
                <asp:ListItem Selected="True" Value="CON">เนื้อหาข่าว</asp:ListItem>
            </asp:RadioButtonList>
          </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
        <asp:Panel ID="pnURL" runat="server">
       
    
    <table width="100%">
      <tr>
        <td width="120">URL : </td>
        <td>
            <asp:TextBox ID="txtURL" runat="server" Width="500px"></asp:TextBox>
          </td>
      </tr>
    </table>
     </asp:Panel>
        <asp:Panel ID="pnFile" runat="server">
     
    <table width="100%" >
      <tr>
        <td width="120">Link File : </td>
        <td>
            <asp:HyperLink ID="hlnkNews" runat="server" Target="_blank">[hlnkNews]</asp:HyperLink>
          </td>
      </tr>
      <tr>
        <td>Upload File :</td>
        <td>
            <asp:FileUpload ID="FileUpload" runat="server" Width="300px" />
            &nbsp; (ชื่อไฟล์ต้องเป็นภาษาอังกฤษเท่านั้น)</td>
      </tr>
    </table>
       </asp:Panel>
        <asp:Panel ID="pnContent" runat="server">
       
    <table width="100%" >
      <tr>
        <td>เนื้อหาข่าว : </td>
        </tr>
      <tr>
        <td>
            <dx:ASPxHtmlEditor ID="xHtmlDetail" runat="server" Theme="Office2010Blue" Width="100%">
                 <SettingsImageUpload UploadFolder="~/imgNews/" UploadImageFolder="~/imgNews/">
                </SettingsImageUpload>
            </dx:ASPxHtmlEditor>
          </td>
        </tr>
      <tr>
        <td class="mailbox-messages">
            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" 
                Text="Public (แสดงบนหน้าเว็บ)" />
          </td>
      </tr>
    </table>
     </asp:Panel>
    
    </td>
  </tr>
  <tr>
    <td align="center">
       <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button></td>
  </tr>
  <tr>
    <td align="center">&nbsp;</td>
  </tr>
  <tr>
    <td align="center">
        <asp:LinkButton ID="lnkNew" runat="server" CssClass="buttonLink" 
            PostBackUrl="News_List.aspx?m=news&amp;p=news-lst">เพิ่มข่าวใหม่</asp:LinkButton>
 <asp:LinkButton ID="LinkButton2" runat="server" CssClass="buttonLink" 
            PostBackUrl="News_List.aspx?m=news&amp;p=news-lst">หน้ารายการข่าว</asp:LinkButton>
 <asp:HyperLink ID="HyperLink3" runat="server" CssClass="buttonLink" 
            NavigateUrl="Default.aspx" Target="_blank">หน้าแรก</asp:HyperLink>
      </td>
  </tr>
 
</table>

</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

                    
    </section>
</asp:Content>
