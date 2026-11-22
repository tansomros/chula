<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Location.aspx.vb" Inherits=".Location" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server"></asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-home icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>แหล่งฝึก
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
</div>
<section class="content">
      <div class="row">
        <section class="col-lg-7 connectedSortable">   
            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">ข้อมูลแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
<div class="row"> 
    <div class="col-md-2">
        <div class="form-group">
            <label>ID</label>
           <asp:Label ID="lblID" CssClass="form-control text-center" runat="server"></asp:Label>             
        </div>
    </div>
    <div class="col-md-10">
        <div class="form-group">
            <label>ชื่อแหล่งฝึก</label>
           <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row"> 
    <div class="col-md-12">
        <div class="form-group">
            <label>ที่อยู่</label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" ></asp:TextBox>   
        </div>
    </div>
</div>
<div class="row"> 
    <div class="col-md-4">
        <div class="form-group">
            <label>จังหวัด/เมือง</label> 
            <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"></asp:DropDownList> 
            <asp:TextBox ID="txtStat" runat="server" ></asp:TextBox>
            
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            <label>ประเทศ</label>
            <asp:TextBox ID="txtCountry" runat="server"  Text="ประเทศไทย" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            <label>รหัสไปรษณีย์</label>
           <asp:TextBox ID="txtZipCode" CssClass="form-control"  runat="server" MaxLength="5"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            <label>โทรศัพท์</label>
           <asp:TextBox ID="txtTel" runat="server"  CssClass="form-control"></asp:TextBox> 
        </div>
    </div>
     <div class="col-md-4">
        <div class="form-group">
            <label>โทรสาร</label>
            <asp:TextBox ID="txtFax"  CssClass="form-control" runat="server"></asp:TextBox>  
        </div>
    </div>
     <div class="col-md-4">
        <div class="form-group">
            <label>ชื่อผู้รับเช็ค</label>
             <asp:TextBox ID="txtReceiveName" runat="server" CssClass="form-control" ></asp:TextBox>  
        </div>
    </div>
     <div class="col-md-6">
        <div class="form-group">
            <label>อีเมล์</label>
           <asp:TextBox ID="txtMail" runat="server"  CssClass="form-control" ></asp:TextBox>   
        </div>
    </div>
     <div class="col-md-6">
        <div class="form-group">
            <label>หัวจดหมาย/หนังสือราชการ</label>
            <asp:TextBox ID="txtLetterName" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox> 
        </div>
    </div>
     <div class="col-md-3">
        <div class="form-group">
            <label>Web site</label>
           <asp:TextBox ID="txtWebsite" runat="server"  MaxLength="100" CssClass="form-control" ></asp:TextBox> 
        </div>
    </div>
     <div class="col-md-3">
        <div class="form-group">
            <label>Facebook</label>
            <asp:TextBox ID="txtFacebook" runat="server" maxLength="100" CssClass="form-control" ></asp:TextBox> 
        </div>
    </div>
      <div class="col-md-3">
        <div class="form-group">
            <label>ละติจูด (Lat)</label>
            <asp:TextBox ID="txtLat" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>
      <div class="col-md-3">
        <div class="form-group">
            <label>ลองติจูด (Lng)</label>
 <asp:TextBox ID="txtLng" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>

</div>                
    </div>           
          </div>
  
               <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-university"></i>

              <h3 class="box-title">ประเภทแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body"> 
            <div class="row">
                 <div class="col-md-6">
        <div class="form-group">
            <label>ประเภท</label>
  <asp:DropDownList ID="ddlLocationGroup" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>
        </div>
    </div>
            <div class="col-md-6">
        <div class="form-group">      
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>                               
            <label><asp:Label ID="lblType" runat="server" Text="กลุ่ม"></asp:Label></label>
             <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                  </asp:DropDownList>       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>             
        </div>
    </div>            
           <div class="col-md-6">
        <div class="form-group">      
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        
            <label><asp:Label ID="lblDepartment" runat="server" Text="สังกัด"></asp:Label></label>
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2" >
                        </asp:DropDownList>  
     
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
           </div>
    </div>      
            <div class="col-md-6">
        <div class="form-group">                             
            <label>ระบุ</label>
            <asp:TextBox ID="txtDepartment" runat="server"  CssClass="form-control" ></asp:TextBox>              
  </div>
    </div>
 <div class="col-md-12">
        <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>                        
            <label>ประเภทร้านยา <small>(กรณีร้านยา)</small></label>
 <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control select2" >
                            <asp:ListItem Selected="True" Value="1">ร้านยาทั่วไป</asp:ListItem>
                            <asp:ListItem Value="2">ร้านยาคุณภาพ</asp:ListItem>
                    </asp:DropDownList> 
             
   </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
 </div>
    </div>    
     </div>
 <div class="row">
                  <div class="col-md-2">
        <div class="form-group">
            <label>จำนวนเตียง</label>
            <asp:TextBox ID="txtBed" runat="server" CssClass="form-control text-center" ></asp:TextBox>
        </div>
    </div>
                  <div class="col-md-2">
        <div class="form-group">
            <label>จำนวนสาขา</label>
             <asp:TextBox ID="txtBranch" runat="server"  CssClass="form-control text-center" ></asp:TextBox>
        </div>
    </div>
                  <div class="col-md-4">
        <div class="form-group">
            <label>จำนวนเภสัชกร</label>
            <asp:TextBox ID="txtOfficerNo" runat="server" CssClass="form-control text-center" ></asp:TextBox>
            <small>ที่ปฏิบัติงานเต็มเวลา (ต่อ 1 สาขา)</small>
        </div>
    </div>
                  <div class="col-md-4">
        <div class="form-group">
            <label>เวลาเปิดทำการ</label>
            <asp:TextBox ID="txtOfficeHour" runat="server" CssClass="form-control text-center" ></asp:TextBox> 
        </div>
    </div>       
        </div> 

            </div>           
          </div>

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">ผู้ประสานงาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                <div class="row">
                     <div class="col-md-6">
        <div class="form-group">
            <label>ชื่อ-สกุล</label>
<asp:TextBox ID="txtCoName" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>  
                     <div class="col-md-6">
        <div class="form-group">
            <label>ตำแหน่ง</label>
<asp:TextBox ID="txtCoPosition" runat="server" width="90%" CssClass="form-control" ></asp:TextBox>  
        </div>
    </div>       
                      
                     <div class="col-md-6">
        <div class="form-group">
            <label>อีเมล</label>
<asp:TextBox ID="txtCoMail" runat="server"   CssClass="form-control" ></asp:TextBox>
        </div>
    </div>       
                     <div class="col-md-6">
        <div class="form-group">
            <label>เบอร์โทรศัพท์</label>
<asp:TextBox ID="txtCoTel" runat="server" width="90%" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>     
                    
                 <div class="col-md-6">
        <div class="form-group">
            <label>ส่วนงาน</label>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                          <ContentTemplate>
                                                              <asp:DropDownList ID="ddlWork" runat="server" CssClass="form-control select2">
                                                              </asp:DropDownList>
                                                              <asp:Label ID="lblNoWork" runat="server" CssClass="validateAlert" Text="ยังไม่ได้กำหนดส่วนงาน กรุณาไปกำหนดก่อน" Visible="False"></asp:Label>
                                                          </ContentTemplate>
                                                          <Triggers>
                                                              <asp:AsyncPostBackTrigger ControlID="ddlLocationGroup" EventName="SelectedIndexChanged" />
                                                          </Triggers>
                                                      </asp:UpdatePanel>
        </div>
    </div>         
                     <div class="col-md-6">
        <div class="form-group">
            <label>รายวิชา</label><asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>

        </div>
    </div>     
                     <div class="col-md-12">
        <div class="form-group mailbox-messages">
            <label></label>
<asp:CheckBox ID="chkCoMain" runat="server" Text="ผู้ประสานงานหลัก" />
        </div>
    </div>       
                     <div class="col-md-12 text-center">
        <div class="form-group">
            <label></label>
<asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-success" Text="เพิ่มผู้ประสานงาน" />
        </div>
    </div>       
                     

                </div>
             
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                      <dx:ASPxGridView ID="grdCoordinator2" ClientInstanceName="grdCoordinator2" runat="server" KeyFieldName="UID" OnCustomCallback="grdCoordinator2_CustomCallback" OnCustomButtonCallback="grdCoordinator2_CustomButtonCallback" Width="100%"  AutoGenerateColumns="False" Theme="Office2010Blue">
                          <Settings GridLines="None"  GroupFormat="{0} {1} {2}" />
                          <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                          <SettingsSearchPanel Visible="True" />                           

        <Columns>          
            <dx:GridViewCommandColumn ShowNewButton="true" VisibleIndex="5" ButtonRenderMode="Image" Caption="ลบ">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Del">
                        <Image ToolTip="ลบรายชื่อ" Url="images/icon-delete.png" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataColumn FieldName="SubjectName" VisibleIndex="0" Caption=" " SortIndex="0" SortOrder="Descending"/>
            <dx:GridViewDataColumn FieldName="Name" VisibleIndex="1" Caption="ชื่อ-นามสกุล"/>
            <dx:GridViewDataColumn FieldName="PositionName" VisibleIndex="2" Caption="ตำแหน่ง"/>
            <dx:GridViewDataColumn FieldName="WorkName" VisibleIndex="3" Caption="ส่วนงานที่ดูแล"/>
            <dx:GridViewDataColumn FieldName="Email" VisibleIndex="4" Caption="อีเมล"/>           
            <dx:GridViewDataColumn FieldName="Tel" VisibleIndex="5" Caption="เบอร์โทร"/>            
        </Columns>
                          <Styles>
                              <Header Font-Bold="True"  Font-Size="12px" HorizontalAlign="Center" CssClass="th">
                              </Header>
                              <GroupRow BackColor="#ecf0f5"   Font-Size="12px">
                              </GroupRow>
                              <Row  Font-Size="12px">
                              </Row>
                              <AlternatingRow BackColor="#ffdfef"></AlternatingRow>
                          </Styles>
    </dx:ASPxGridView>
                      <asp:GridView ID="grdCoordinator" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="txtcontent" Font-Bold="False" GridLines="None" Width="100%" DataKeyNames="UID">
                          <RowStyle BackColor="White"/>
                          <columns>
                              <asp:BoundField DataField="Name" HeaderText="ชื่อ-สกุล">
                              <HeaderStyle HorizontalAlign="Left" />
                              <ItemStyle Width="200px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="PositionName" HeaderText="ตำแหน่ง">
                              <HeaderStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Email" HeaderText="อีเมล"></asp:BoundField>
                              <asp:BoundField HeaderText="โทรศัพท์" DataField="Tel" />
                              <asp:BoundField HeaderText="ประสานงานวิชา" DataField="SubjectName" />                             
                              <asp:TemplateField HeaderText="ลบ">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="cmdDel" runat="server" ImageUrl="images/icon-delete.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>'  />
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                          </columns>
                          <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                          <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                          <headerstyle CssClass="th" Font-Bold="True" />
                          <EditRowStyle BackColor="#2461BF" />
                          <AlternatingRowStyle BackColor="#ffdfef"/>
                      </asp:GridView>
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="cmdAdd" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="grdCoordinator" EventName="RowCommand" />
                      <asp:AsyncPostBackTrigger ControlID="grdCoordinator2" EventName="RowCommand" />
                  </Triggers>
              </asp:UpdatePanel>
    </div>
            
          </div>
 
 </section>
        
        <section class="col-lg-5 connectedSortable">
         
      
             <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-clock-o"></i>

              <h3 class="box-title">วัน-เวลาที่สามารถให้ฝึกปฏิบัติงาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                   <div class="row"> 
         <div class="col-md-8">
             <div class="form-group">
                 <label>วัน</label>
                 <asp:RadioButtonList RepeatDirection="Horizontal" ID="optDay" runat="server">
                  <asp:ListItem Value="1" Selected="True">จันทร์-ศุกร์</asp:ListItem>
                  <asp:ListItem Value="2">จันทร์-เสาร์</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                  </asp:RadioButtonList>     
             </div>
             </div>
                         <div class="col-md-4">
             <div class="form-group">
                 <label>ระบุ</label>
                 <asp:TextBox ID="txtDay" runat="server" cssclass="form-control text-center" placeholder=""></asp:TextBox>
             </div>
             </div>

                        <div class="col-md-8">
             <div class="form-group">
                 <label>เวลา</label>
                <asp:RadioButtonList ID="optTime" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1" Selected="True">08.00-16.00 น.</asp:ListItem>
                  <asp:ListItem Value="2">09.00-17.00 น.</asp:ListItem>
                  <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                  </asp:RadioButtonList>   

             </div>
             </div>
                        <div class="col-md-4">
             <div class="form-group">
                 <label>ระบุ</label>
                 <asp:TextBox ID="txtOfficeTime" runat="server" cssclass="form-control text-center" placeholder=""></asp:TextBox>
             </div>
             </div>
         </div>  
                 <asp:Panel ID="pnWorkBrunch" runat="server">
           <div class="row"> 
         <div class="col-md-8">
             <div class="form-group">
                 <label>เงื่อนไข</label>
                    <asp:RadioButtonList ID="optWorkBrunch" runat="server" 
                      RepeatDirection="Horizontal">
                      <asp:ListItem Value="1">นิสิตต้องหมุนเวียนฝึกทุกสาขา</asp:ListItem>
                      <asp:ListItem Value="2" Selected="True">อยู่ประจำสาขาเดียว</asp:ListItem>
                  </asp:RadioButtonList>   
             </div>
             </div>
                <div class="col-md-4">
             <div class="form-group">
                 <label>ระบุรายละเอียด</label>
                 <asp:TextBox ID="txtBrunchDetail" runat="server" Width="90%" Height="55px" 
                      TextMode="MultiLine" CssClass="form-control" ></asp:TextBox>
             </div>
             </div>
     </div>        
              </asp:Panel>
    </div>          
          </div>

               <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-cloud"></i>
              <h3 class="box-title">โซน/กลุ่ม</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">

                 <div class="row"> 
         <div class="col-md-6">
             <div class="form-group">
                 <label>โซนมหาวิทยาลัย</label>
                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
             </div>
             </div>
                <div class="col-md-6">
             <div class="form-group">
                 <label>กลุ่มแหล่งฝึก</label>
                 <asp:DropDownList ID="ddlOfficeGroup" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
             </div>
             </div>
     </div>    
        </div>
           
          </div>

              <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-dollar"></i>

              <h3 class="box-title">ค่าตอบแทน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                <table class="mailbox-messages">
                    <tr>
                        <td>แหล่งฝึกประสงค์ต้องการรับค่าตอบแทนจากคณะ</td>
                        <td><asp:RadioButtonList ID="optReceive" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y">รับ</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">ไม่รับ</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td>แหล่งฝึกมีค่าตอบแทนให้นิสิต</td>
                        <td>
                            
                            <asp:RadioButtonList ID="optPay" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y">มี</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">ไม่มี</asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>
                    </tr>
                </table>
        </div>           
          </div>     

              <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-medkit"></i>

              <h3 class="box-title"><asp:Label ID="lblStep5" runat="server" Text="งานที่สามารถให้นิสิตฝึกปฏิบัติได้"></asp:Label> </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body mailbox-messages">
                <table width="100%">                                  
                        <tr>
          <td align="left"class="align-text-top" > 
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
         <div class="form-control no-border">**ถ้ามีงานอื่นๆนอกจากตัวเลือกข้างบน ให้ไปเพิ่มงานที่เมนู ส่วนงานของแหล่งฝึก ก่อน
         </div>   
            </asp:Panel>          </td>
      </tr>
        <tr>
          <td align="left" class="align-text-top" >งานเด่นของแหล่งฝึก</td>
      </tr>
        <tr>
          <td align="left" class="align-text-top" >
                                                      <asp:TextBox ID="txtTopWork" 
                  runat="server" Width="100%" TextMode="MultiLine" Height="55px" CssClass="form-control" ></asp:TextBox>                                                    </td>
      </tr>
         <tr>
          <td align="left" class="align-text-top">สิ่งอื่นๆที่นิสิตควรทราบหรือเตรียมตัวก่อนมาฝึกปฏิบัติงาน</td>
      </tr>
         <tr>
          <td align="left" class="align-text-top">
                                                      <asp:TextBox ID="txtRemark" 
                  runat="server" Width="100%" Height="55px" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox>                                                    </td>
      </tr>
         

                </table>



    </div>
           
          </div>   

             <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-hotel"></i>

              <h3 class="box-title">การจัดหาที่พักสำหรับนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
              
              <table class="no-border" width="100%">
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence1" CssClass="text-bold" runat="server" Text="แหล่งฝึกมีที่พักให้" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
         <asp:Panel ID="pnRes1" runat="server"   HorizontalAlign="Left"  BorderWidth="1">  
                     <table  width="100%">
            <tr>
              <td  > <asp:RadioButtonList ID="optIsPay" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="0" Selected="True">ไม่ต้องเสียค่าใช้จ่าย</asp:ListItem>
                      <asp:ListItem Value="1">เสียค่าเช่าโดยประมาณเดือนละ</asp:ListItem>
                  </asp:RadioButtonList>
                  </td> 
              
                <td>
                    <asp:TextBox ID="txtPay" runat="server" CssClass="form-control text-center" Width="60px"></asp:TextBox>
                </td>    <td>  &nbsp;บาท </td>
                </tr>
<tr>
              <td>  นิสิตต้องเข้าพัก ณ ที่พักที่แหล่งฝึกจัดหาให้
                  </td> 
                
              <td  colspan="2">
                  <asp:RadioButtonList ID="optForce" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="1">ใช่</asp:ListItem>
                      <asp:ListItem Selected="True" Value="0">ไม่ใช่</asp:ListItem>
                  </asp:RadioButtonList>
              </td>
                
    </tr>
                         
<tr>
    <td>  นิสิตต้องติดต่อเพื่อเข้าพักที่แหล่งฝึกจัดหาให้ล่วงหน้า                   </td>
        <td>  <asp:TextBox ID="txtConfirm" runat="server" Width="50px" CssClass="form-control text-center"></asp:TextBox></td>
     <td>  &nbsp;เดือน </td>
    </tr>
                     </table>  

         </asp:Panel>
              </td>
            </tr>
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence2" runat="server" CssClass="text-bold" 
                      Text="แหล่งฝึกไม่มีที่พักให้" AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
                  <asp:RadioButton ID="optResidence3" runat="server" CssClass="text-bold" 
                      Text="แหล่งฝึกไม่มีที่พักให้ แต่สามารถแนะนำที่พักให้ได้" 
                      AutoPostBack="True" />                </td>
            </tr>
            <tr>
              <td>
                  <asp:Panel ID="pnRes3" runat="server" BorderWidth="1">
                
                  <table class="table no-border" >
                <tr>
                  <td>ชื่อที่พัก</td>
                  <td><asp:TextBox ID="txtApartmentName1"  CssClass="form-control" runat="server"></asp:TextBox></td>
                  <td>เบอร์โทร</td>
                  <td><asp:TextBox ID="txtApartmentTel1"  CssClass="form-control" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3"><asp:TextBox ID="txtApartmentAddress1"  CssClass="form-control" runat="server" 
                         ></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel1" runat="server"  CssClass="form-control"></asp:TextBox>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance1" runat="server" CssClass="form-control"></asp:TextBox>                                                    </td>
                </tr>
              </table>
                         <table class="table no-border">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName2" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel2" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress2"  CssClass="form-control" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel2" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance2" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                </tr>
              </table>
                         <table class="table no-border">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:TextBox ID="txtApartmentName3" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:TextBox ID="txtApartmentTel3" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:TextBox ID="txtApartmentAddress3"  CssClass="form-control" runat="server" 
                          Width="90%"></asp:TextBox>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtTravel3" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:TextBox ID="txtDistance3" runat="server"  CssClass="form-control" ></asp:TextBox>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>              </td>
            </tr>
          </table>
    </div>           
          </div>
   </section>
      </div>
    <div class="row text-center">          
         <div class="col-md-12">        
        <div  class="mailbox-messages"><asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" /></div>
         <div class="text-center">
              <asp:Label ID="lblValidate" runat="server"  Visible="False" CssClass="validateAlert" Width="99%"></asp:Label>
        </div>
       <div class="text-center"> 
            <asp:Button ID="cmdSave" runat="server" Text="บันทึก" CssClass="btn btn-primary" Width="100px"></asp:Button>
            <asp:Button ID="cmdClear" runat="server" text="ยกเลิก" CssClass="btn btn-secondary" Width="100px"></asp:Button>       
      </div>
    </div>
      </div>
    </section>
   
</asp:Content>
