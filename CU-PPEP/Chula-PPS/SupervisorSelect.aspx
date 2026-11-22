<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisorSelect.aspx.vb" Inherits=".SupervisorSelect" %>


<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     
    
     
    </asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<section class="content-header">
      <h1>เลือกแผนนิเทศของอาจารย์</h1>   
    </section>

<section class="content">  
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เลือกสถานที่นิเทศ ปีการศึกษา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            &nbsp;<asp:Label ID="lblYear" runat="server"></asp:Label>
            </div>
            <div class="box-body"> 
     <table width="100%" border="0">
<tr>
         <td class="MenuSt">ค้นหา</td>
    </tr>
      <tr>
         <td>
               <table border="0">
<tr>
  <td >จังหวัด</td>
  <td ><asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server" AutoPostBack="True"> </asp:DropDownList></td>
  </tr>
<tr>
  <td   >ผลัดฝึก</td>
  <td >
                                                      <asp:DropDownList ID="ddlPhase" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    
                </td>
  </tr>
<tr>
  <td   >ค้นหา</td>
  <td >
                              <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                            
                </td>
  </tr>
<tr>
  <td   >&nbsp;</td>
  <td >
                 <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>              
                </td>
  </tr>

          </table>
         </td>
      </tr>     
      <tr>
        <td><asp:GridView ID="grdLocation" runat="server" CellPadding="0" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" CssClass="txtcontent" DataKeyNames="LocationID" Width="100%" PageSize="5">
          <RowStyle BackColor="#F7F7F7" />
          <columns>
              <asp:TemplateField>
                  <ItemTemplate>
                      <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "ApproveStatus")) %>'  />
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" Width="20px" />
              </asp:TemplateField>

            <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
              <headerstyle HorizontalAlign="Left" />        
            </asp:BoundField>
              <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภท" >
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
              <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" >
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
              <asp:BoundField DataField="stdCount" HeaderText="นิสิต" >
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
              <asp:BoundField DataField="Remark" HeaderText="หมายเหตุ" >
              <HeaderStyle HorizontalAlign="Left" />
              </asp:BoundField>
           <asp:TemplateField HeaderText="เลือก">
            <ItemTemplate>                        
                    <asp:Button ID="imgSelect" runat="server" CommandArgument='<%# Container.DataItemIndex %>'  Text="เลือก" Width="45px"  CssClass="buttonGreen"/>
                              
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                
                            </asp:TemplateField>

            </columns>
          <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />        
          <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC01" />        
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
          <headerstyle CssClass="th" Font-Bold="True" 
                                                            />        
          <EditRowStyle BackColor="#2461BF" />
          <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
     <asp:Label ID="lblNoLocation" runat="server" CssClass="validateAlert" Text="ยังไม่พบแหล่งฝึกในผลัดที่ท่านค้นหา" Width="100%"></asp:Label>

          </td>
      </tr>
    </table>

</div> 
        <div class="box-footer clearfix">   
            <asp:Image ID="imgStatus2" runat="server" ImageUrl="images/icon-ok.png" /> &nbsp;หมายถึง ประธานคณะกรรมการได้อนุมัติให้อาจารย์ออกนิเทศแหล่งฝึกนี้แล้ว
            </div>
          </div>
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">แหล่งฝึกที่เลือกไว้&nbsp;<asp:Label ID="lblCount" runat="server"></asp:Label></h3>
             
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
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">

                <HeaderStyle HorizontalAlign="Left" />

                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                </asp:BoundField>
<asp:BoundField DataField="LocationGroupName" HeaderText="ประเภทแหล่งฝึก">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                      
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="PhaseName" HeaderText="ผลัดที่" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="ลบ">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="images/icon-delete.png" />
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
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
     <asp:Label ID="lblNoSelect" runat="server" CssClass="validateAlert" Text="ท่านยังไม่ได้เลือกแหล่งฝึกที่ต้องการไปนิเทศ" Width="100%"></asp:Label>

</div> 
      <div class="box-footer text-center">    
           
                         
           
          <asp:Label ID="lblNOSpv" runat="server" Text="เลือกสถานที่ที่ต้องการไปนิเทศครั้งนี้ จากการค้นหาข้างบน"></asp:Label>
          </div>
          </div>
                           
  <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-clock-o"></i>

              <h3 class="box-title">กำหนดวันนิเทศและการเดินทาง</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
      <table border="0" cellPadding="1" cellSpacing="1" width="100%">
<tr>
  <td>วันที่นิเทศ</td>
  <td>
   
    <table  border="0" cellspacing="2" cellpadding="0">
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
        <td><asp:DropDownList CssClass="form-control select2" ID="ddlTime" runat="server" Width="150px">
          <asp:ListItem Selected="True" Value="1">08.00 - 12.00 น.</asp:ListItem>
          <asp:ListItem Value="2">13.00 - 16.00 น.</asp:ListItem>
          </asp:DropDownList></td>
        </tr>
    </table></td>
  </tr>
<tr>
  <td>เดินทางโดย</td>
  <td>
                  <dx:ASPxRadioButtonList ID="optTravel" runat="server" RepeatDirection="Horizontal" SelectedIndex="0" EnableTheming="True" Theme="Material" CssClass="text12">
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
  <td valign="top">หมายเหตุ / อื่นๆ</td>
  <td>
                  <asp:TextBox ID="txtRemark" runat="server" Height="80px" TextMode="MultiLine" Width="90%" CssClass="form-control select2"></asp:TextBox>
                </td>
  </tr>

          </table>
</div>
           
       <div class="box-footer text-center">           
       <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>         
      </div>

          </div>

      </section>  
</asp:Content>
