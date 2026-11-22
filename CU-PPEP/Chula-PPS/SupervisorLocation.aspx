<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisorLocation.aspx.vb" Inherits=".SupervisorLocation" %>


<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     
    
     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>เลือกแหล่งฝึกที่ต้องการไปนิเทศ</h1>
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">เลือกรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                            
<table border="0"cellPadding="1" cellSpacing="1" width="80%">
<tr>
                                                    <td width="100">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td>
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td align="right" class="text10">
                                                        <asp:Label ID="Label2" runat="server" Text="Ref.ID : "></asp:Label>
                                                        <asp:Label ID="lblUID" runat="server"></asp:Label>
                                                    </td>
          </tr>
<tr>
  <td>รายวิชา :</td>
  <td>
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
  <td align="left">&nbsp;</td>
  <td align="left">&nbsp;</td>
  </tr>

    </table>
               
                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-clock-o"></i>

              <h3 class="box-title">เลือกแหล่งฝึกและผลัดฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
             <table border="0"cellPadding="1" cellSpacing="1" width="80%">

<tr>
  <td>จังหวัด</td>
  <td><asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server" AutoPostBack="True"> </asp:DropDownList></td>
  <td align="left">&nbsp;</td>
  <td align="left">&nbsp;</td>
  </tr>
<tr>
  <td  valign="top">แหล่งฝึก</td>
  <td colspan="3">
                                                      <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    
                    <asp:Label ID="lblLocation" runat="server" Visible="False"></asp:Label>
                </td>
  </tr>
<tr>
  <td valign="top">ผลัดฝึก</td>
  <td colspan="3">
                  <dx:ASPxRadioButtonList ID="optTimePhase" runat="server" Theme="MetropolisBlue" ValueType="System.String" >
                      <Paddings PaddingLeft="0px" />
                      <Border BorderStyle="None" />
                  </dx:ASPxRadioButtonList>
                  <asp:Label ID="lblNoPhase" runat="server" ForeColor="Red"></asp:Label>
                </td>
</tr>
<tr>
  <td>วันที่นิเทศ</td>
  <td colspan="3">
   
    <table  border="0" >
      <tr>
        <td> 
            <dx:ASPxTextBox ID="txtDate" runat="server"  HorizontalAlign="Center" MaxLength="10" Theme="MetropolisBlue" Width="120px">
                <MaskSettings Mask="dd/MM/yyyy" />
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
        <td width="50" align="center">เวลา</td>
        <td><asp:DropDownList CssClass="form-control select2" ID="ddlTime" runat="server">
          <asp:ListItem Selected="True" Value="1">08.00 - 12.00 น.</asp:ListItem>
          <asp:ListItem Value="2">13.00 - 16.00 น.</asp:ListItem>
          </asp:DropDownList></td>
        </tr>
    </table></td>
  </tr>
<tr>
  <td>เดินทางโดย</td>
  <td colspan="3">
                  <dx:ASPxRadioButtonList ID="optTravel" runat="server" Theme="MetropolisBlue"  RepeatDirection="Horizontal" SelectedIndex="0">
                      <Paddings PaddingLeft="0px" />
                      <Items>
                          <dx:ListEditItem Selected="True" Text="เดินทางไปเอง" Value="1" />
                          <dx:ListEditItem Text="ให้คณะเตรียมรถให้" Value="2" />
                      </Items>
                      <Border BorderStyle="None" />
                  </dx:ASPxRadioButtonList>
                </td>
  </tr>
<tr>
  <td>&nbsp;</td>
  <td colspan="3"><asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button></td>
</tr>
</table>      
                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">รายชื่อแหล่งฝึกที่เลือกไปนิเทศทั้งหมด&nbsp; <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;แหล่ง</h3>
             
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
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" VerticalAlign="Middle" />
            <columns>
<asp:BoundField DataField="WorkDate" HeaderText="วันที่" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                <asp:BoundField DataField="WorkTime" HeaderText="เวลา" />
                <asp:BoundField DataField="SubjectCode" HeaderText="รายวิชา" />
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">
                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                       </asp:BoundField>
                <asp:BoundField DataField="PhaseName" HeaderText="ผลัดที่" />
                <asp:BoundField DataField="TravelBy" HeaderText="การเดินทาง" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="แก้ไข">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' ImageUrl="images/icon-edit.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    
                      
                      CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "UID")  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" VerticalAlign="Middle" HorizontalAlign="Center" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
</div>
            <div class="box-footer clearfix">           
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ยังไม่พบแหล่งฝึกที่เลือก" Width="100%"></asp:Label>
            </div>
          </div>
    </section>    
</asp:Content>
