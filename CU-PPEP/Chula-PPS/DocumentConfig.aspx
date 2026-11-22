<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentConfig.aspx.vb" Inherits=".DocumentConfig" MasterPageFile="~/Site.Master"  %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<section class="content-header">
      <h1>ตั้งค่า Path เก็บไฟล์ข้อมูลออกเอกสาร/จดหมาย</h1>   
    </section>

<section class="content"> 
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-gears"></i>

              <h3 class="box-title">ตั้งค่า Directory Path </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                   <table border="0"  cellPadding="1" cellSpacing="1" width="100%">
<tr>
  <td  width="120" >Directory Path :</td>
  <td >
     
            <dx:ASPxTextBox ID="txtPath" runat="server"  Theme="MetropolisBlue" Width="90%" TabIndex="1" Text="D:\THEDEV\Web Application\Chula-PPS\Chula-PPS\Document\Data">
                <NullTextStyle >
                </NullTextStyle>
                <ValidationSettings ErrorText=" Invalid value">
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
                <CaptionStyle >
                </CaptionStyle>
                <RootStyle >
                </RootStyle>
            </dx:ASPxTextBox>
     
    </td>
  </tr>

<tr>
  <td>&nbsp;</td>
  <td >
      <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" TabIndex="3" Width="100px" />
    &nbsp;</td>
</tr>
</table>                      
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>

</asp:Content>