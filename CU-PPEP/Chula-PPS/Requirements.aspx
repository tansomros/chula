<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Requirements.aspx.vb" Inherits=".Requirements" %>


<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<%@ Register namespace="Microsoft.AspNet.EntityDataSource" tagprefix="EntityDataSource" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
             <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="lnr-users icon-gradient bg-success"></i>
            </div>
            <div>Internship Quota         
                 <div class="page-title-subheading">ความต้องการส่งนิสิตฝึกปฏิบัติงานฯ
                 </div>
            </div>
        </div>
    </div>
</div>  
     

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">1.เลือกเงื่อนไขการกำหนดความต้องการ (Select Requirement Criteria)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                 <table border="0" cellspacing="2" cellpadding="0" class="table table-borderless">
           
            <tr>
              <td width="100">ปี/Year</td>
              <td><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" width="150"
                      CssClass="form-control select2"> </asp:DropDownList></td>
             
            </tr>
            <tr>
              <td>แหล่งฝึก/Site</td>
              <td><asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList> </td>
            </tr>
            </table>
</div>
            <div class="box-footer clearfix">           
              <asp:Label ID="lblValidate" runat="server" 
                  Visible="False" CssClass="validateAlert" Width="99%">กรุณากำหนดผลัดฝึกของปีที่ต้องการก่อน</asp:Label>           
            </div>
          </div>
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">2.ระบุจำนวนนิสิตแต่ละส่วนงาน / Available Internship Slots</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="WorkID" >
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" Width="30px" />                      </asp:BoundField>
                <asp:BoundField HeaderText="ส่วนงาน/Work Field" DataField="WorkName" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="ร้องขอจากคณะ/Faculty Request">
                <ItemTemplate>
                    <asp:TextBox ID="txtQTY1" runat="server" CssClass="txtscore" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "QTY1") %>' ></asp:TextBox>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แหล่งฝึกรับได้/Available Quota">
                <ItemTemplate>
                    <asp:TextBox ID="txtQTY2" runat="server" CssClass="txtscore" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "QTY2") %>'></asp:TextBox>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />          
            </asp:TemplateField>
                <asp:TemplateField HeaderText="ต้องการเพศ/Gender">
                    <ItemTemplate>
                        <asp:RadioButtonList ID="optSex" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="M">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                            <asp:ListItem Selected="True" Value="N">Unspecified</asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th"  Font-Bold="false" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>                                             
</div> 
        <div class="box-footer clearfix">           
           <table border="0" cellspacing="2" cellpadding="0">
                                                            <tr>
                                                              <td>* กรณีไม่ระบุเพศ ต้องการนิสิตเพศเดียวกัน <br /> If gender is not specified, do you want students of the same gender? </td>
                                                              <td class="mailbox-messages">
                                                                  <asp:RadioButtonList ID="optGender" runat="server" 
                                                                      RepeatDirection="Horizontal">
                                                                      <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                      <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                                  </asp:RadioButtonList></td>
                                                            </tr>
                                                          </table>      
            </div>
          </div>

            
  
   
            
<table width="100%" border="0" cellPadding="1" cellSpacing="1">
                                                
     <!--  <tr>
          <td align="left" valign="top" class="MenuSt"> 3. วัน-เวลาที่สามารถให้นิสิตฝึกปฏิบัติงาน</td>
      </tr>
         <tr>
          <td align="left" valign="top"  ><table border="0" cellpadding="0" cellspacing="1">
            <tr>
              <td width="30">วัน</td>
              <td>
              <asp:RadioButtonList ID="optDay" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1" Selected="True">จันทร์-ศุกร์</asp:ListItem>
                  <asp:ListItem Value="2">จันทร์-เสาร์</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ (โปรดระบุ)</asp:ListItem>
                  </asp:RadioButtonList>                </td>
              <td>ระบุ</td>
              <td><asp:TextBox ID="txtDay" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
              <td>เวลา</td>
              <td>
              <asp:RadioButtonList ID="optTime" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1" Selected="True">08.00-16.00 น.</asp:ListItem>
                  <asp:ListItem Value="2">09.00-17.00 น.</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ (โปรดระบุ)</asp:ListItem>
                  </asp:RadioButtonList>                </td>
              <td>ระบุ</td>
              <td>
                      <asp:TextBox ID="txtOfficeTime" runat="server" Width="150px"></asp:TextBox>                    </td>
            </tr>
          </table></td>
      </tr>
         <tr>
          <td align="left" valign="top" class="MenuSt"> 4. การจัดหาที่พักสำหรับนิสิต</td>
      </tr>
         <tr>
          <td align="left" valign="top" ><table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="20">&nbsp;</td>
              <td>
                  <asp:RadioButton ID="optResidence1" runat="server" Text="แหล่งฝึกมีที่พักให้" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td></td>
              <td>
                  <asp:Panel ID="pnRes1" runat="server" HorizontalAlign="Left" class="block_step1">                  
                  <asp:RadioButtonList ID="optIsPay" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="0" Selected="True">ไม่ต้องเสียค่าใช้จ่าย</asp:ListItem>
                      <asp:ListItem Value="1">เสียค่าเช่าโดยประมาณ</asp:ListItem>
                  </asp:RadioButtonList>
                  เดือนละ
                  <asp:TextBox ID="txtPay" runat="server" Width="60px" CssClass="NumberCenter"></asp:TextBox>
&nbsp;บาท<br /> นิสิตต้องเข้าพัก ณ ที่พักที่แหล่งฝึกจัดหาให้<asp:RadioButtonList ID="optForce" runat="server" RepeatDirection="Horizontal">
                          <asp:ListItem Value="1">ใช่</asp:ListItem>
                          <asp:ListItem Value="0" Selected="True">ไม่ใช่</asp:ListItem>
                      </asp:RadioButtonList>
                      <br />
                นิสิตต้องติดต่อเพื่อเข้าพักที่แหล่งฝึกจัดหาให้ล่วงหน้า 
                  <asp:TextBox ID="txtConfirm" runat="server" Width="50px" CssClass="NumberCenter"></asp:TextBox>
&nbsp;เดือน
</asp:Panel></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>
                  <asp:RadioButton ID="optResidence2" runat="server" 
                      Text="แหล่งฝึกไม่มีที่พักให้" AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>
                  <asp:RadioButton ID="optResidence3" runat="server" 
                      Text="แหล่งฝึกไม่มีที่พักให้ แต่สามารถประสานงานจัดหาให้ได้" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>
                  <asp:RadioButton ID="optResidence4" runat="server" 
                      Text="แหล่งฝึกไม่มีที่พักให้ แต่สามารถแนะนำที่พักให้ได้" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td></td>
              <td>
                  <asp:Panel ID="pnRes4" runat="server" class="block_step1">
                
                  <table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>              </td>
            </tr>
          </table>          </td>
      </tr>
         <tr>
          <td align="left" valign="top" class="MenuSt"> 5.
              <asp:Label ID="lblStep5" runat="server" 
                  Text="งานที่สามารถให้นิสิตฝึกปฏิบัติได้"></asp:Label>             </td>
      </tr>
         <tr>
          <td align="left" valign="top" >
           <asp:Panel ID="pnWorkBrunch" runat="server">
          
          <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td>
                  <asp:RadioButtonList ID="optWorkBrunch" runat="server" 
                      RepeatDirection="Horizontal">
                      <asp:ListItem Value="1">นิสิตต้องหมุนเวียนฝึกทุกสาขา</asp:ListItem>
                      <asp:ListItem Value="2" Selected="True">อยู่ประจำสาขาเดียว</asp:ListItem>
                  </asp:RadioButtonList>                </td>
            </tr>
            <tr>
              <td>ระบุรายละเอียด</td>
            </tr>
            <tr>
              <td><asp:TextBox ID="txtBrunchDetail" runat="server" Width="600px" Height="55px" 
                      TextMode="MultiLine"></asp:TextBox></td>
            </tr>
          </table>
             
              </asp:Panel>
              <asp:Panel ID="pnWorkList" runat="server">
            
              <asp:CheckBoxList ID="chkWorkList" runat="server">
                  <asp:ListItem Value="1">งานบริการผู้ป่วยนอกและผู้ป่วยใน</asp:ListItem>
                  <asp:ListItem Value="2">งานด้านเภสัชกรรมคลีนิก</asp:ListItem>
                  <asp:ListItem Value="3">งานด้านคลับเวชภัณฑ์</asp:ListItem>
                  <asp:ListItem Value="4">งานบริการวิชาการ</asp:ListItem>
                  <asp:ListItem Value="5">งานการผลิตยา</asp:ListItem>
                  <asp:ListItem Value="6">งานคุ้มครองผู้บริโภคในหน่วยงาน</asp:ListItem>
                  <asp:ListItem Value="7">ศึกษาดูงานคุ้มครองผู้บริโภค ณ สำนักงานสาธารณสุขจังหวัด</asp:ListItem>
                  <asp:ListItem Value="8">อื่นๆ (โปรดระบุ)</asp:ListItem>
              </asp:CheckBoxList>
              ระบุ
              <asp:TextBox ID="txtSpecWork" runat="server" Width="600px"></asp:TextBox>
            </asp:Panel>          </td>
      </tr>
        <tr>
          <td align="left" valign="top" >งานเด่นของแหล่งฝึก</td>
      </tr>
        <tr>
          <td align="left" valign="top" >
                                                      <asp:TextBox ID="txtTopWork" 
                  runat="server" Width="600px" TextMode="MultiLine" Height="55px"></asp:TextBox>                                                    </td>
      </tr>
         <tr>
          <td align="left" valign="top" class="MenuSt"> 6. สิ่งอื่นๆที่นิสิตควรทราบหรือเตรียมตัวก่อนมาฝึกปฏิบัติงาน</td>
      </tr>
         <tr>
          <td align="left" valign="top">
                                                      <asp:TextBox ID="txtRemark" 
                  runat="server" Width="600px" Height="55px" TextMode="MultiLine"></asp:TextBox>                                                    </td>
      </tr>
      <tr>
          <td align="left" valign="top">Status : &nbsp;<span class="texttopic"><asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" 
                                                     />
          </span></td>
      </tr>
         -->
      
       <tr>
          <td align="center" valign="top">
              <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="120" Text="Submit"></asp:Button>
          </td>
      </tr>
         
    </table>
  </section>    
</asp:Content>
