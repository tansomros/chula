<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Location.aspx.vb" Inherits=".Location" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
     <section class="content-header">
      <h1>Organization Information Management
          <small></small>
              
      </h1>
      <ol class="breadcrumb">
        <li><i class="fa fa-home"></i> <asp:Label ID="lblMode" runat="server" Text="Mode : Add New"></asp:Label></li>       
      </ol>
    </section>

<section class="content">

 <!-- Small boxes (Stat box) -->


      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
        <section class="col-lg-7 connectedSortable">   
            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">Organization Information</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                
<table width="100%" border="0">
                                                <tr>
                                                     <td width="200" align="left" class="texttopic">รหัส (ID):</td>
                                                  <td align="left" width="250"><asp:Label ID="lblID" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblAuto" runat="server">(Auto Id)</asp:Label></td> 
                                                    <td width="100" align="left" class="texttopic">      </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ชื่อแหล่งฝึก (Name)</td>
                                                    <td align="left" class="texttopic" colspan="3"><asp:TextBox ID="txtName" runat="server" Width="100%" ></asp:TextBox></td>
                                                    
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">ที่อยู่ (Address)</td>
                                                  <td colspan="3" align="left" class="texttopic">
                                                      <asp:TextBox ID="txtAddress" runat="server" Width="100%" ></asp:TextBox>                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        จังหวัด/เมือง (Province)</td>
                                                    <td align="left" class="texttopic" colspan="3">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
            <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"></asp:DropDownList>                                                    <asp:TextBox ID="txtStat" runat="server" Width="200px" ></asp:TextBox></td>
                                                                <td align="right" width="100">ประเทศ (Country)</td>
                                                                <td><asp:TextBox ID="txtCountry" runat="server"  cssclass="form-control" >ประเทศไทย</asp:TextBox></td>
                                                                 <td align="right" class="texttopic">รหัสไปรษณีย์ (Zipcode)</td>
                                                    <td align="left" class="texttopic">
                                                        <asp:TextBox ID="txtZipCode" runat="server" MaxLength="5" cssclass="form-control text-center"                                              ></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">เบอร์โทร (Telephone)</td>
                                                    <td align="left" class="texttopic" width="250">
                                                        <asp:TextBox ID="txtTel" runat="server" CssClass="form-control"></asp:TextBox>                                                    </td>
                                                    <td align="right" class="texttopic">แฟกซ์ (Fax)</td>
                                                    <td align="left" class="texttopic">
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>                                                    </td>
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">ชื่อผู้รับเช็ค(Check Payee Name)</td>
                                                  <td align="left" class="texttopic" colspan="3">
                                                        <asp:TextBox ID="txtReceiveName" runat="server"  CssClass="form-control"></asp:TextBox>                                                    </td>
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">Email (primary)</td>
                                                  <td align="left" class="texttopic" colspan="3">
                                                      <asp:TextBox ID="txtMail" runat="server"  CssClass="form-control"></asp:TextBox>                                                    </td>
                                                </tr>
                                               <tr>
             
              <td align="left" valign="top">ผู้รีบจดหมาย/หนังสือราชการ (Addressee/Recipient Name)</td>     
              <td align="left" valign="top" colspan="3">
              <asp:TextBox ID="txtLetterName" runat="server"  CssClass="form-control"></asp:TextBox> 
              </td>

            </tr>
                                               <tr>
             
              <td align="left" valign="top">Web site</td>     
              <td align="left" valign="top" colspan="3">
              <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox> 
                                                   </td>

            </tr>
    <tr>
             
              <td align="left" valign="top">Facebook</td>     
              <td align="left" valign="top" colspan="3">
              <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control"></asp:TextBox> 
              </td>

            </tr>
    <tr>
             
              <td align="left" valign="top">ละติจูด (Lat)</td>     
              <td align="left" valign="top" colspan="3">
                  <table cellpadding="0" cellspacing="0">
                      <tr>
                          <td>
                              <asp:TextBox ID="txtLat" runat="server"></asp:TextBox>
                          </td>
                          <td>ลองติจูด (Lng)</td>
                          <td>
                              <asp:TextBox ID="txtLng" runat="server"></asp:TextBox>
                          </td>
                      </tr>
                  </table>
              </td>

            </tr>
     <tr>
          
           <td align="left" valign="top">Display Language</td>   
           <td align="left" valign="top">
                 <asp:RadioButtonList ID="optLang" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="TH" Selected="True">ภาษาไทย</asp:ListItem>
                      <asp:ListItem Value="EN">English</asp:ListItem> 
                </asp:RadioButtonList>   
           </td>   
         <td align="right" valign="top">
             <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></td>   
<td align="left" valign="top">
           <div align="left" class="mailbox-messages"><asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />            </div>
    </td>
          </tr>
  </table> 
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
  
               <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-university"></i>

              <h3 class="box-title">ประเภทแหล่งฝึก (Internship Site Type)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body"> 
       <table width="100%" border="0">
            <tr>
              <td width="100" class="texttopic" valign="top">ประเภท (Organization Type)</td>
              <td align="left">
                  <asp:DropDownList ID="ddlLocationGroup" runat="server" AutoPostBack="True" CssClass="form-control select2">
                  </asp:DropDownList>
                         </td>
            </tr>
</table>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
 <table width="100%">
            <tr>
              <td width="100" class="texttopic" valign="top">
                  <asp:Label ID="lblType" runat="server" Text="กลุ่ม (Category)"></asp:Label>                </td>
              <td align="left">
                  <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                  </asp:DropDownList>
                </td>
            </tr>
     </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
  <table width="100%">
      <tr>
              <td class="texttopic" width="100" valign="top">
                  <asp:Label ID="lblDepartment" runat="server" Text="สังกัด (Affiliation)"></asp:Label>
                </td>
              <td align="left">
                      <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="form-control select2" Width="200px">
                        </asp:DropDownList>                </td>
            </tr>
           </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
               <table >
                    <tr>
                        <td style="width: 100px;">
                  <asp:Label ID="lblSpecDept" runat="server" Text="ระบุ"></asp:Label></td>
                        <td>
                  <asp:TextBox ID="txtDepartment" runat="server" Width="200px"></asp:TextBox>
                        </td>
                         
                    </tr>
                     
                </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartment" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
              


                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                  <table >
                      <tr>
                          <td width="100">
                        <asp:Label ID="lblLevel" runat="server" Text="ประเภทร้านยา (Pharmacy Type)"></asp:Label>
                          </td>
                          <td>
                        <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control select2" Width="200px">
                            <asp:ListItem Selected="True" Value="1">ร้านยาทั่วไป</asp:ListItem>
                            <asp:ListItem Value="2">ร้านยาคุณภาพ</asp:ListItem>
                    </asp:DropDownList>                      </td>
                      </tr>
                  </table>
                
   
   </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>


       <table  border="0"  >
 <tr>
              <td width="100">จำนวนเตียง (Bed)</td>
              <td align="left"><asp:TextBox ID="txtBed" runat="server" Width="60px"></asp:TextBox></td>
              <td align="left" >จำนวนสาขา (Branch Count)</td>
      <td>
        <asp:TextBox ID="txtBranch" runat="server" Width="60px"></asp:TextBox>
      </td>
      </tr>  

 <tr>
              <td  colspan="4">
              <table border="0">
  <tr>
    <td>จำนวนเภสัชกรที่ปฏิบัติงานเต็มเวลา ต่อ 1 สาขา (Full-Time Pharmacists per Branch)</td>
    <td> <asp:TextBox ID="txtOfficerNo" runat="server" Width="60px"></asp:TextBox></td>
  </tr>
</table>

              </td>              
              </tr>  

                <tr>
                     <td class="texttopic"  width="100">เวลาเปิดทำการ (Business Hours)</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtOfficeHour" runat="server" Width="200px"></asp:TextBox>                    </td> 
                </tr>
              </table>          
            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายวิชาที่เปิดฝึก (Course Training)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                  <table width="100%" >             
                     <tr>
                      <td class="texttopic" width="120">รายวิชา (Course)</td>
                      <td>
                         <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2"></asp:DropDownList>                                                              
                      </td>   
                      <td  width="100">
                          <asp:Button ID="cmdAddSubject" runat="server" CssClass="btn btn-warning" Text="เพิ่ม/Add" />
                      </td> 
                  </tr>
              </table>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                  <ContentTemplate>      
                        <dx:ASPxGridView ID="grdSubject" ClientInstanceName="grdSubject" runat="server" KeyFieldName="PersonID" OnCustomButtonCallback="grdSubject_CustomButtonCallback" Width="100%"  AutoGenerateColumns="False" Theme="Office2010Blue" CssClass="table table-hover">
                      <Settings GridLines="None"  GroupFormat="{0} {1} {2}" />
                      <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                      <SettingsSearchPanel Visible="True" />                           

    <Columns>          
        <dx:GridViewCommandColumn ShowNewButton="true" VisibleIndex="2" ButtonRenderMode="Image" Caption="Del" Width="30px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="DelSubject">
                    <Image ToolTip="ลบรายวิชา" Url="images/icon-delete.png" />
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataColumn FieldName="SubjectCode" VisibleIndex="0" Caption="รหัส/Code "/>
        <dx:GridViewDataColumn FieldName="SubjectName" VisibleIndex="1" Caption="ชื่อรายวิชา/Name"/>
          
    </Columns>
                      <Styles>
     <Header Font-Bold="True"  Font-Size="12px" HorizontalAlign="Center" CssClass="texttopic text-bold">
     </Header>                          
     <Row CssClass="texttopic" Font-Size="12px">
     </Row>
     <AlternatingRow BackColor="#edf5fb"></AlternatingRow>                    

 </Styles>
</dx:ASPxGridView> 
                     
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="cmdAddSubject" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="grdSubject" EventName="CustomButtonCallback" /> 
                  </Triggers>
              </asp:UpdatePanel>
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 
 <div id="pnPreceptor" style="display:block" class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">อาจารย์แหล่งฝึก (Preceptor)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                  <table width="100%" >
                  <tr>
                      <td class="texttopic" width="150">ชื่อ-สกุล (Name)</td>
                      <td> 
                           <table width="100%" >
 <tr>
     <td width="150"> <asp:DropDownList ID="ddlPPrefix" runat="server" CssClass="form-control select2" width="150">
</asp:DropDownList></td>
      <td><asp:TextBox ID="txtPFname" runat="server" CssClass="form-control"></asp:TextBox></td>
      <td><asp:TextBox ID="txtPLname" runat="server" CssClass="form-control"></asp:TextBox></td>
     </tr></table>

                         
                          </td>
                    </tr>
                     <tr>
                      <td class="texttopic" width="100">ตำแหน่ง(Position)</td>
                      <td>
                                                      <asp:TextBox ID="txtPposition" runat="server"  CssClass="form-control"></asp:TextBox>                                                    </td>
                  </tr>
                     <tr>
                      <td class="texttopic" width="100">ส่วนงาน (Work Field)</td>
                      <td>
                                                      <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                          <ContentTemplate>
                                                              <asp:DropDownList ID="ddlWork" runat="server" CssClass="form-control select2">
                                                              </asp:DropDownList>
                                                              <asp:Label ID="lblNoWork" runat="server" CssClass="validateAlert" Text="ยังไม่ได้กำหนดส่วนงาน กรุณาไปกำหนดก่อน" Visible="False"></asp:Label>
                                                          </ContentTemplate>
                                                          <Triggers>
                                                              <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                                                          </Triggers>
                                                      </asp:UpdatePanel>
                         </td>
                  </tr>
                  <tr>
                      <td class="texttopic">อีเมล (E-mail)</td>
                      <td><asp:TextBox ID="txtPmail" runat="server" CssClass="form-control"></asp:TextBox></td>
                     </tr>
                     <tr>  <td class="texttopic">เบอร์โทรศัพท์ (Tel.)</td>
                      <td><asp:TextBox ID="txtPtel" runat="server" CssClass="form-control" ></asp:TextBox></td>
                  </tr>                               
                  <tr>
                      <td class="texttopic">&nbsp;</td>
                      <td>
                          <asp:Button ID="cmdAddPreceptor" runat="server" CssClass="btn btn-warning" Text="เพิ่ม/Add" />
                      </td>
                      <td>&nbsp;</td>
                  </tr>
              </table>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                  <ContentTemplate>
                      <dx:ASPxGridView ID="grdPreceptor" ClientInstanceName="grdPreceptor" runat="server" KeyFieldName="PersonID" OnCustomButtonCallback="grdPreceptor_CustomButtonCallback" Width="100%"  AutoGenerateColumns="False" Theme="Office2010Blue" CssClass="table table-hover">
                      <Settings GridLines="None"  GroupFormat="{0} {1} {2}" />
                      <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                      <SettingsSearchPanel Visible="True" />                           

    <Columns>          
        <dx:GridViewCommandColumn ShowNewButton="true" VisibleIndex="7" ButtonRenderMode="Image" Caption="Del" Width="30px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="DelPreceptor">
                    <Image ToolTip="ลบรายชื่อ" Url="images/icon-delete.png" />
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataColumn FieldName="nRow" VisibleIndex="0" Caption="#" SortIndex="0" SortOrder="Ascending" Width="10px"/>
        <dx:GridViewDataColumn FieldName="Name" VisibleIndex="1" Caption="ชื่อ-นามสกุล/Name"/>
        <dx:GridViewDataColumn FieldName="PositionName" VisibleIndex="2" Caption="ตำแหน่ง/Position"/>
        <dx:GridViewDataColumn FieldName="WorkName" VisibleIndex="3" Caption="ส่วนงานที่ดูแล/Work Field"/>
        <dx:GridViewDataColumn FieldName="Email" VisibleIndex="4" Caption="อีเมล/e-mail"/>           
        <dx:GridViewDataColumn FieldName="Telephone" VisibleIndex="5" Caption="เบอร์โทร/Tel."/>      
        <dx:GridViewDataColumn FieldName="StatusName" VisibleIndex="6"  Caption="Status" CellStyle-HorizontalAlign="Center" />    
    </Columns>
                      <Styles>
                          <Header Font-Bold="True"  Font-Size="12px" HorizontalAlign="Center" CssClass="texttopic text-bold">
                          </Header>                          
                          <Row CssClass="texttopic" Font-Size="12px">
                          </Row>
                          <AlternatingRow BackColor="#edf5fb"></AlternatingRow>
                      </Styles>
</dx:ASPxGridView>
                                         
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="cmdAddPreceptor" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="grdPreceptor" EventName="CustomButtonCallback" /> 
                  </Triggers>
              </asp:UpdatePanel>
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 

 </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
        <section class="col-lg-5 connectedSortable">
         
      
             <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-clock-o"></i>

              <h3 class="box-title">วัน-เวลาที่สามารถให้นิสิตฝึกปฏิบัติงาน (Available Days/Hours for Internship)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
   <table border="0"   width="100%" class="mailbox-messages">
            <tr>
              <td valign="top" width="60">วัน (Days)</td>
              <td width="320px">
              <asp:RadioButtonList ID="optDay" runat="server">
                  <asp:ListItem Value="1" Selected="True">จันทร์-ศุกร์ (Mon - Fri)</asp:ListItem>
                  <asp:ListItem Value="2">จันทร์-เสาร์ (Mon - Sat)</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ (โปรดระบุ) (Others (Please Specify))</asp:ListItem>
                  </asp:RadioButtonList>                </td>
              <td  width="150"  valign="bottom">ระบุ (Please Specify)</td>
              <td valign="bottom"><asp:TextBox ID="txtDay" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
              <td  valign="top">เวลา (Time)</td>
              <td>
              <asp:RadioButtonList ID="optTime" runat="server" RepeatDirection="Vertical">
                  <asp:ListItem Value="1" Selected="True">08.00-16.00 น.</asp:ListItem>
                  <asp:ListItem Value="2">09.00-17.00 น.</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ (โปรดระบุ)(Others (Please Specify))</asp:ListItem>
                  </asp:RadioButtonList>                </td>
              <td valign="bottom">ระบุ (Please Specify)</td>
              <td valign="bottom">
                      <asp:TextBox ID="txtOfficeTime" runat="server" Width="100%"></asp:TextBox>                    </td>
            </tr>
            
          </table>
                 <asp:Panel ID="pnWorkBrunch" runat="server">
          
          <table width="100%" border="0"  >
            <tr>
              <td>
                  <asp:RadioButtonList ID="optWorkBrunch" runat="server" 
                      RepeatDirection="Horizontal">
                      <asp:ListItem Value="1">นิสิตต้องหมุนเวียนฝึกทุกสาขา (Rotation Through All Branches Required)</asp:ListItem>
                      <asp:ListItem Value="2" Selected="True">อยู่ประจำสาขาเดียว (Stationed at a Single Branch)</asp:ListItem>
                  </asp:RadioButtonList>                </td>
            </tr>
            <tr>
              <td>ระบุรายละเอียด (Detail)</td>
            </tr>
            <tr>
              <td><asp:TextBox ID="txtBrunchDetail" runat="server" Width="90%" Height="55px" 
                      TextMode="MultiLine"></asp:TextBox></td>
            </tr>
          </table>
             
              </asp:Panel>
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

               <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-cloud"></i>

              <h3 class="box-title">โซน/กลุ่ม (Zone/Group)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                <table  Width="99%">
                    <tr>
                        <td>โซนมหาวิทยาลัย (Zone)</td>
                        <td> 
                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control select2" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>กลุ่มแหล่งฝึก (Group)</td>
                        <td>                                                     
                            <asp:DropDownList ID="ddlOfficeGroup" runat="server" CssClass="form-control select2" Width="95%">
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                </table>


        </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

              <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-dollar"></i>

              <h3 class="box-title">ค่าตอบแทน(Compensation)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">


                <table class="mailbox-messages">
                    <tr>
                        <td>แหล่งฝึกประสงค์ต้องการรับค่าตอบแทนจากคณะ <br /> (Would the training site like to receive compensation from the faculty?)</td>
                        <td width="180" valign="bottom"><asp:RadioButtonList ID="optReceive" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y">รับ/Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">ไม่รับ/No</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td>แหล่งฝึกมีค่าตอบแทนให้นิสิต <br />(Does the training site provide compensation to students?)</td>
                        <td valign="bottom">
                            
                            <asp:RadioButtonList ID="optPay" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y">มี/Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">ไม่มี/No</asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>
                    </tr>
                </table>


        </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
      

              <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-medkit"></i>

              <h3 class="box-title">งานที่สามารถให้นิสิตฝึกปฏิบัติได้ / Which tasks can students perform during the internship?</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body mailbox-messages">
                <table width="100%">                                  
                        <tr>
          <td align="left" valign="top" >              
              
              <asp:Panel ID="pnWorkList" runat="server">
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                          <asp:CheckBoxList ID="chkWorkList" runat="server" CssClass="mailbox-messages">
                          </asp:CheckBoxList>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                      </Triggers>
                  </asp:UpdatePanel>
         <div class="form-control no-border">**ถ้ามีงานอื่นๆนอกจากตัวเลือกข้างบน ให้ไปเพิ่มงานที่เมนู ส่วนงานของแหล่งฝึก ก่อน <br />
             If there are other tasks besides the options above, please add them first in the “Department Tasks” menu of the training site.
         </div>   
            </asp:Panel>          </td>
      </tr>
        <tr>
          <td align="left" valign="top" >งานเด่นของแหล่งฝึก (Key Tasks of the Training Site)</td>
      </tr>
        <tr>
          <td align="left" valign="top" >
                                                      <asp:TextBox ID="txtTopWork" 
                  runat="server"  CssClass="form-control" TextMode="MultiLine" Height="55px"></asp:TextBox>                                                    </td>
      </tr>
         <tr>
          <td align="left" valign="top">สิ่งอื่นๆที่นิสิตควรทราบหรือเตรียมตัวก่อนมาฝึกปฏิบัติงาน <br /> (Other Information Students Should Know or Prepare Before the Internship)</td>
      </tr>
         <tr>
          <td align="left" valign="top">
              <asp:TextBox ID="txtRemark" 
                  runat="server"  CssClass="form-control" Height="55px" TextMode="MultiLine"></asp:TextBox>                                                    </td>
      </tr>
         

                </table>



    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>   

             <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-hotel"></i>

              <h3 class="box-title">การจัดหาที่พักสำหรับนิสิต (Does the training site provide accommodation?)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
              
              <table width="100%" border="0"  >
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence1" runat="server" Text="แหล่งฝึกมีที่พักให้ (Provided)" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
                  <asp:Panel ID="pnRes1" runat="server" HorizontalAlign="Left" class="block_step1">                  
                  <asp:RadioButtonList ID="optIsPay" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="0" Selected="True">ไม่ต้องเสียค่าใช้จ่าย(No Cost)</asp:ListItem>
                      <asp:ListItem Value="1">เสียค่าเช่าโดยประมาณ (Approximate Rent)</asp:ListItem>
                  </asp:RadioButtonList>
                  เดือนละ (per month)
                  <asp:TextBox ID="txtPay" runat="server" Width="60px" CssClass="NumberCenter"></asp:TextBox>
&nbsp;บาท<br /> นิสิตต้องเข้าพัก ณ ที่พักที่แหล่งฝึกจัดหาให้ (Are students required to stay at the accommodation provided by the training site?)
                      
                      <asp:RadioButtonList ID="optForce" runat="server" RepeatDirection="Horizontal">
                          <asp:ListItem Value="1">ใช่/Yes</asp:ListItem>
                          <asp:ListItem Value="0" Selected="True">ไม่ใช่/No</asp:ListItem>
                      </asp:RadioButtonList>
                      <br />
                นิสิตต้องติดต่อเพื่อเข้าพักที่แหล่งฝึกจัดหาให้ล่วงหน้า (How many months in advance must students contact the training site to arrange accommodation?)
                  <asp:TextBox ID="txtConfirm" runat="server" Width="50px" CssClass="NumberCenter"></asp:TextBox>
&nbsp;เดือน (Month)
</asp:Panel></td>
            </tr>
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence2" runat="server" 
                      Text="แหล่งฝึกไม่มีที่พักให้ (Not provided)" AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence3" runat="server" 
                      Text="แหล่งฝึกไม่มีที่พักให้ แต่สามารถแนะนำที่พักให้ได้ (No accommodation provided, but recommendations available)" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
                  <asp:Panel ID="pnRes3" runat="server" class="block_step1">
                
                  <table width="100%" border="0"  >
                <tr>
                  <td width="100">ชื่อที่พัก/Accommodation Name</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName1" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร/Tel.</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel1" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่/Address</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress1" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง/Transportation</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel1" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td>ระยะทาง/Distance to Training Site</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance1" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
              </table>

                      <br />

                         <table width="100%" border="0"  >
                <tr>
                  <td width="100">ชื่อที่พัก/Accommodation Name</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName2" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร/Tel.</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel2" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่/Address</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress2" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง/Transportation</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel2" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td>ระยะทาง/Distance to Training Site</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance2" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
              </table>

                      <br />
                         <table width="100%" border="0"  >
                <tr>
                  <td width="100">ชื่อที่พัก/Accommodation Name</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName3" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร/Tel.</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel3" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่/Address</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress3" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง/Transportation</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel3" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                  <td>ระยะทาง/Distance to Training Site</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance3" runat="server" Width="200px"></asp:TextBox>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>              </td>
            </tr>
          </table>        
    
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">อาจารย์ประสานงานแหล่งฝึก (Coordinator)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                  <table width="100%" >
                  <tr>
                      <td class="texttopic" width="150">ชื่อ-สกุล (Name)</td>
                      <td>
                            <asp:DropDownList ID="ddlPerson" runat="server"  CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>  
                      </td>
                    </tr>
              </table>   
                         <asp:UpdatePanel ID="UpdatePanelCoor" runat="server">
       <ContentTemplate> 

            <table width="100%" >
 <tr>
                      <td class="texttopic" width="150">ตำแหน่ง(Position)</td>
                      <td>
                          <asp:TextBox ID="txtCuPosition" runat="server" readonly="True" CssClass="form-control bg-transparent"></asp:TextBox>
                      </td>
                  </tr>             
                  <tr>
                      <td class="texttopic">อีเมล (E-mail)</td>
                      <td><asp:TextBox ID="txtCuMail" runat="server" readonly="true" CssClass="form-control bg-transparent"></asp:TextBox></td>
                     </tr>
                     <tr>  <td class="texttopic">เบอร์โทรศัพท์ (Tel.)</td>
                      <td><asp:TextBox ID="txtCuTel" runat="server" readonly="true" CssClass="form-control bg-transparent"></asp:TextBox></td>
                  </tr>
                </table>
       </ContentTemplate>
       <Triggers>
           <asp:AsyncPostBackTrigger ControlID="ddlPerson" EventName="SelectedIndexChanged" />
       </Triggers>
   </asp:UpdatePanel>
          
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 
            
   </section>
        <!-- right col -->
      </div>
      <!-- /.row (main row) -->
    <div class="row">           
        
      

         <div align="center">
              <asp:Label ID="lblValidate" runat="server" 
                  Visible="False" CssClass="validateAlert" Width="99%"></asp:Label>
        </div>

       <div align="center"><span class="texttopic">
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-pink" Width="100px"></asp:Button>
            <asp:Button ID="cmdClear" runat="server" text="Cancel" CssClass="btn btn-secondary" Width="100px"></asp:Button>
          </span>
      </div>
    </div>
      
    </section>
   
</asp:Content>
