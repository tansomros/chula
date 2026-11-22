<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentList.aspx.vb" Inherits=".DocumentList" %>

<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<section class="content-header">
      <h1>บันทึกข้อความ/จดหมาย</h1>   
    </section>

<section class="content">  
     
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

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
                <asp:TemplateField HeaderText="เลือก">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' ImageUrl="images/arrow-down.png" />
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

              <h3 class="box-title">ตั้งค่าเอกสาร/บันทึกข้อความ/จดหมาย เพื่อส่งออกข้อมูล</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
   
<table border="0"  cellPadding="1" cellSpacing="1" width="100%">
<tr>
                                                    <td width="100"  >
                                                        ปีการศึกษา :                                                        </td>
                                                    <td  >
     
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
     
                                                    </td>
                                                    <td  >&nbsp;</td>
                                                    <td align="right" class="text10">
                                                        <asp:Label ID="Label2" runat="server" Text="Ref.ID : "></asp:Label>
                                                        <asp:Label ID="lblUID" runat="server"></asp:Label>
                                                    </td>
          </tr>
<tr>
  <td  >ชื่อเรื่อง</td>
  <td   colspan="3">
                                                        <asp:Label ID="lblSubjectTitle" runat="server"></asp:Label>
    </td>
  </tr>

<tr>
  <td colspan="4"  >รายละเอียดเอกสาร</td>
  </tr>
<tr>
  <td  >เลขที่เริ่มต้น</td>
  <td  >
     
            <dx:ASPxTextBox ID="txtDocNo" runat="server"  HorizontalAlign="Center"  Theme="MetropolisBlue" Width="70px">
                
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
  <td >ลงวันที่</td>
  <td > 
     
            <dx:ASPxTextBox ID="txtDocDate" runat="server"  HorizontalAlign="Center" Theme="MetropolisBlue" Width="200px">
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
  <td  >นิสิตชั้นปี</td>
  <td  >
     
            <dx:ASPxTextBox ID="txtStdLevel" runat="server"  HorizontalAlign="Center"  Theme="MetropolisBlue" Width="70px">
                
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
  <td >&nbsp;</td>
  <td >
     
            &nbsp;</td>
  </tr>


<tr>
  <td   colspan="4">
      <asp:Label ID="lblNotic" runat="server" CssClass="OptionPanels" Text="หมายเหตุ  :  กรอกแบบไหน แสดงบนเอกสารแบบนั้น" Width="99%"></asp:Label>
    </td>
  </tr>
  
<tr>
  <td  >&nbsp;</td>
  <td   colspan="3">
      &nbsp;</td>
</tr>
<tr>
  <td  >&nbsp;</td>
  <td   colspan="3">
      <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-pink" Text="ส่งออกข้อมูล" />
    &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" />
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
