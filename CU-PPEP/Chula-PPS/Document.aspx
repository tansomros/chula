<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Document.aspx.vb" Inherits=".Document" MasterPageFile="~/Site.Master"  %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <section class="content-header">
      <h1>บันทึกข้อความ/จดหมาย (Master)</h1>   
    </section>

<section class="content">   

      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-bookmark"></i>

              <h3 class="box-title">รายการเอกสาร/บันทึกข้อความ/จดหมาย ทั้งหมด&nbsp; <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;รายการ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">                                                   
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:BoundField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="DocumentCode" HeaderText="Code" />
                <asp:BoundField DataField="DocumentName" HeaderText="ชื่อเอกสาร" >
                </asp:BoundField>
                <asp:BoundField DataField="MailMergeName" HeaderText="ไฟล์เอกสาร">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FileDataName" HeaderText="ไฟล์ข้อมูล" />
                <asp:TemplateField HeaderText="แก้ไข">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' ImageUrl="images/icon-edit.png" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
                                                   
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>
<asp:Panel ID="pnDetail" runat="server">
 <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-gear"></i>

              <h3 class="box-title">ตั้งค่าเอกสาร/บันทึกข้อความ/จดหมาย</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">
<table border="0"cellPadding="1" cellSpacing="1" width="100%">
   
<tr>
                                                    <td width="100">
                                                        Code :                                                        </td>
                                                    <td>
     
            <dx:ASPxTextBox ID="txtDocCode" runat="server"  HorizontalAlign="Center"  Theme="MetropolisBlue">
                
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
                                                    <td>&nbsp;</td>
                                                    <td align="right" class="text10">
                                                        <asp:Label ID="Label2" runat="server" Text="Ref.ID : "></asp:Label>
                                                        <asp:Label ID="lblUID" runat="server"></asp:Label>
                                                    </td>
          </tr>
<tr>
  <td>ชื่อเอกสาร :</td>
  <td colspan="3">
     
            <dx:ASPxTextBox ID="txtName" runat="server"  Theme="MetropolisBlue" Width="90%" TabIndex="1">
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
  <td>ชื่อเรื่อง</td>
  <td colspan="3">
     
            <dx:ASPxTextBox ID="txtSubjectTitle" runat="server"  Theme="MetropolisBlue" Width="90%" TabIndex="2">
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
  <td>ชื่อไฟล์เอกสาร</td>
  <td width="200" >
     
                                                        <asp:Label ID="lblFileDocName" runat="server"></asp:Label>
     
     </td>
  <td width="100">ชื่อไฟล์ข้อมูล</td>
  <td> 
     
                                                        <asp:Label ID="lblFileDataName" runat="server"></asp:Label>
     
    </td>
  </tr>
<tr>
  <td>&nbsp;</td>
  <td colspan="3">&nbsp;</td>
</tr>

<tr>
  <td>&nbsp;</td>
  <td colspan="3">
      <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" TabIndex="3" Width="100px" />
    &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" TabIndex="4" Width="100px" />
    </td>
</tr>
</table>    
            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
   </asp:Panel>                    
    </section>  

</asp:Content>