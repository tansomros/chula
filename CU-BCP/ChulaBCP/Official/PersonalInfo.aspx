<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="PersonalInfo.aspx.vb" Inherits="PersonalInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>ประวัติส่วนตัว
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ประวัติส่วนตัว</li>
      </ol>
    </section>

      <section class="content">
  <div class="row">  
           <section class="col-lg-7 connectedSortable"> 

        <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">ข้อมูลทั่วไป</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">
                  <table align="center"  class="table no-border">         
          <tr>
              <td width="150">Ref.ID</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
          <tr>
            <td>รหัสพนักงาน</td>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td width="110px">
                <asp:TextBox ID="txtEmployeeCode" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right">ประเภท</td>
                        <td  align="left">
                <asp:RadioButtonList ID="optPersonType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">อาจารย์</asp:ListItem>
                    <asp:ListItem Value="2">บุคลากร</asp:ListItem>
                </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
              </td>
          </tr>
          <tr>
            <td>คำนำหน้าชื่อ</td>
            <td align="left">
                <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="Optioncontrol" Width="200px">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <td>ชื่อ-สกุล (ไทย)</td>
            <td>
                <table style="padding-left:5px;" align="left">
                    <tr>
                       
                        <td>
                            <asp:TextBox ID="txtFnameTH" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLnameTH" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
              </td>
          </tr>
             <tr>
            <td>ชื่อ-สกุล (อังกฤษ)</td>
            <td>
                <table style="padding-left:5px;" align="left">
                    <tr>
                       
                        <td>
                            <asp:TextBox ID="txtFnameEN" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLnameEN" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
              </td>
          </tr>
                     
             <tr>
            <td>เบอร์ภายใน</td>
            <td>
                  <table align="left">
                    <tr>
                       
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" Width="150px"></asp:TextBox>
                        </td>
                         <td   align="right">เบอร์มือถือ</td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
          </tr>
                     
             <tr>
            <td>อีเมล์</td>
            <td align="left">
                  <table>
                      <tr>
                          <td>
                            <asp:TextBox ID="txtMail" runat="server" Width="200px"></asp:TextBox>
                          </td>
                          <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMail" ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                      </tr>
                      </table>
                 </td>
          </tr>
                     
                      </table>

 </div>
        <!-- /.box-body -->
        <div class="box-footer">
         <asp:Panel ID="pnAlert" runat="server">
                  <div class="alert alert-danger alert-dismissible">
                
                <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    <p><asp:Label ID="lblAlert" runat="server"></asp:Label></p>
              </div>
                   </asp:Panel>
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    
 
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">ข้อมูลตำแหน่ง/สังกัด/คุณวุฒิ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
            </div>
        <div class="box-body">             
                
             <table style="width:95%;">
                    <tr>
            <td width="150">ตำแหน่งงาน</td>
            <td>
                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
                   <tr>
                     <td width="80"><asp:Label ID="lblMajor_Label" runat="server" Text="สาขาวิชา"></asp:Label>
                     </td>
                     <td>
                <asp:DropDownList ID="ddlMajor" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList><asp:DropDownList ID="ddlDepartment" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
                     </td>
                 </tr>
                  <tr>
            <td><asp:Label ID="lblPositionPrefix_Label" runat="server" Text="ตำแหน่งทางวิชาการ"></asp:Label>
                      </td>
            <td>
                <asp:DropDownList ID="ddlPositionPrefix" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
                    <tr>
                     <td>คุณวุฒิ</td>
                     <td>
                            <asp:TextBox ID="txtEducated" runat="server" Width="95%"></asp:TextBox>
                     </td>
                 </tr>
                       

          <tr>
            <td>วันที่บรรจุทำงาน</td>
            <td align="left">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  runat="server" ControlToValidate="txtStartDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    
                </table>
              </td>
          </tr>
               
             </table>
             
                
             </div> 
      </div>
      <!-- /.box -->      
 </section>
        <section class="col-lg-5 connectedSortable">

<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">รูปประจำตัว</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">
            
                    <asp:Image ID="imgPerson" class="profile-user-img img-responsive img-circle"  runat="server" Height="150px" ImageUrl="~/MemberPicture/unpic.jpg" Width="150px" />
                    
                            
<br />
             <div class="pull-left">            <asp:FileUpload ID="FileUpload1" runat="server" AllowedFileTypes="jpg,jpeg" /> </div> 
           <div class="pull-right"> <asp:Button ID="cmdSavePicture" runat="server" CssClass="buttonSmall" Text="Save and Preview" />            </div>         
                                 
             </div> 
     <div class="box-footer">  <asp:Label ID="lblNoSuccess" runat="server" ForeColor="Red" Text="Upload รูปไม่สำเร็จ กรุณาตรวจสอบไฟล์ แล้วลองใหม่อีกครั้ง" Visible="False"></asp:Label>
         </div>
      </div>
      <!-- /.box -->  

<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title"></h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
       <table style="width:100%;">
           
                
                 </table>
                      
             </div> 

     <div class="box-footer"><small>
        </small>
        </div>

      </div>
      <!-- /.box -->      
                
        </section>
</div>
        <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">การศึกษา</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanelEduAdd" runat="server">
                    <ContentTemplate>                    
                  <table style="width: 100%;">
                    <tr>
                        <th>&nbsp;</th>
                        <th>วุฒิการศึกษา</th>
                        <th>สาขาวิชา</th>
                        <th>สถาบัน/มหาวิทยาลัย</th>
                        <th>ปีที่จบ</th>
                    </tr>
                    <tr>
                        <td  style="width: 80px;">ภาษาไทย</td>
                        <td>
                            <asp:TextBox ID="txtDegreeTH" runat="server"  Width="99%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMajorTH" runat="server" Width="99%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUniversityTH" runat="server"  Width="99%"></asp:TextBox>
                        </td>
                        <td width="45">
                            <asp:TextBox ID="txtEduYearTH" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>English</td>
                        <td>
                            <asp:TextBox ID="txtDegreeEN" runat="server"  Width="99%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMajorEN" runat="server"  Width="99%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUniversityEN" runat="server"  Width="99%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEduYearEN" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td> 
                            <asp:Button ID="cmdAddEdu" runat="server"  CssClass="buttonlink"  Text="เพิ่ม" Width="100px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdEdu" EventName="RowCommand">
                        </asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
             <div class="form-group">

                 <asp:UpdatePanel ID="UpdatePanelEducation" runat="server">
                     <ContentTemplate>
                         <asp:GridView ID="grdEdu" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%">
                             <RowStyle BackColor="White" VerticalAlign="Top" />
                             <columns>
                                 <asp:TemplateField HeaderText="วุฒิการศึกษา">
                                     <ItemTemplate>
                                         <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DegreeTH") %>'></asp:Label>
                                         <br />
                                         <asp:Label ID="Label3" runat="server"  CssClass="NameEN"  Text='<%# DataBinder.Eval(Container.DataItem, "DegreeEN") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="สาขาวิชา">
                                     <ItemTemplate>
                                         <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MajorTH") %>'></asp:Label>
                                         <br />
                                         <asp:Label ID="Label5" runat="server"  CssClass="NameEN"  Text='<%# DataBinder.Eval(Container.DataItem, "MajorEN") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="สถาบัน">
                                     <ItemTemplate>
                                         <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UniversityTH") %>'></asp:Label>
                                         <br />
                                         <asp:Label ID="Label7" runat="server"  CssClass="NameEN"  Text='<%# DataBinder.Eval(Container.DataItem, "UniversityEN") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ปีที่จบ">
                                     <ItemTemplate>
                                         <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "YearTH") %>'></asp:Label>
                                         <br />
                                         <asp:Label ID="lblYEN" runat="server"  CssClass="NameEN"  Text='<%# DataBinder.Eval(Container.DataItem, "YearEN") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="แก้ไข">
                                     <itemtemplate>
                                         <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-edit.png" />
                                     </itemtemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ลบ">
                                     <itemtemplate>
                                         <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' CssClass="gridbutton" ImageUrl="images/icon-delete.png" />
                                     </itemtemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                 </asp:TemplateField>
                             </columns>
                             <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                             <headerstyle CssClass="gridheader" Font-Bold="True" VerticalAlign="Middle" />
                             <EditRowStyle BackColor="#2461BF" />
                             <AlternatingRowStyle BackColor="#EBEBEB" />
                         </asp:GridView>
                     </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="cmdAddEdu" EventName="Click">
                         </asp:AsyncPostBackTrigger>                        
                     </Triggers>
                 </asp:UpdatePanel>
                         
                                
            </div>
          </div>
             <div class="box-footer">

        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->  

<!--- ผลงาน วิจัย อบรม--->
          <asp:Panel ID="pnCV" runat="server">
<h3>ผลงาน/รางวัล/การอบรม</h3>
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">ผลงานการวิจัยด้านวิชาชีพ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
        
            <table class="table">
                
                <tr>
                    <td>การตีพิมพ์ ด้านวิชาชีพ<br />
                         <small>กรุณาใส่ผลงานการตีพิมพ์เป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) (ผู้แต่ง ชือวารสาร ปีที่พิมพ์)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfessional_Research_Pub" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>การนำเสนอแบบ oral/poster ด้านวิชาชีพ<br /><small>กรุณาใส่ผลงานการตีพิมพ์เป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) (ประเภทการนำเสนอ ชื่อผลงาน ชื่อการประชุม ปีที่นำเสนอ)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfessional_Research_Present" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   

<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">ผลงานการวิจัยด้านแพทยศาสตรศึกษา</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
        <table class="table">
                
                <tr>
                    <td>การตีพิมพ์ ด้านแพทยศาสตรศึกษา<br />
                         <small>กรุณาใส่ผลงานการตีพิมพ์เป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) (ผู้แต่ง ชือวารสาร ปีที่พิมพ์)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Pub" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>การนำเสนอแบบ oral/poster ด้านแพทยศาสตรศึกษา<br /><small>กรุณาใส่ผลงานการตีพิมพ์เป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) (ประเภทการนำเสนอ ชื่อผลงาน ชื่อการประชุม ปีที่นำเสนอ)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Present" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
          
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">รางวัลที่ได้รับ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
       <small> กรุณาใส่รางวัลที่ได้รับเป็นลำดับข้อลงมา (ตั้งอดีต-ปัจจุบัน) รายละเอียด ชื่อผู้รับ ชื่อรางวัล ปี</small>
                        <asp:TextBox ID="txtAward" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
        
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
          
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">การอบรมทางวิชาชีพ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
          <table class="table">
               
                <tr>
                    <td>การอบรมทางวิชาชีพในประเทศ<br />
                         <small>กรุณาใส่หัวข้อการอบรมเป็นลำดับข้อลงมา (หัวข้อการอบรม สถานที่ ปี)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfessional_Training_In" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>การอบรมทางวิชาชีพต่างประเทศ<br /><small>กรุณาใส่หัวข้อการอบรมเป็นลำดับข้อลงมา (หัวข้อการอบรม สถานที่ ปี)</small></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfessional_Training_Out" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
       
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">การอบรมด้านแพทยศาสตรศึกษาในประเทศ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
          <table class="table">
                <tr>
                    <td><small>กรุณาใส่หัวข้อการอบรมเป็นลำดับข้อลงมา (หัวข้อการอบรม สถานที่ ปี )(3 ปีย้อนหลัง)</small></td>
                </tr>
                <tr>
                    <td>การเรียนการสอน</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_In1" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>การวัดและประเมินผล</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_In2" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td>การวิจัยด้านแพทยศาสตรศึกษา</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_In3" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td>การอบรมจิตตปัญญาศึกษา</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_In4" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
                  
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">การอบรมด้านแพทยศาสตรศึกษาต่างประเทศ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
          <table class="table">
                <tr>
                    <td><small>กรุณาใส่หัวข้อการอบรมเป็นลำดับข้อลงมา (หัวข้อการอบรม สถานที่ ปี )(3 ปีย้อนหลัง)</small></td>
                </tr>
                <tr>
                    <td>การเรียนการสอน</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_Out1" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>การวัดและประเมินผล</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_Out2" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td>การวิจัยด้านแพทยศาสตรศึกษา</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_Out3" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td>การอบรมจิตตปัญญาศึกษา</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMedical_Training_Out4" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
 <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">การอบรมด้านบริหารและประกันคุณภาพ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
        
            <table width="100%">
                <tr>
                    <td rowspan="2" width="200">   
        
            <asp:CheckBoxList ID="chkQA_Training" runat="server">
                <asp:ListItem Value="QA1">การบริหาร MSE</asp:ListItem>
                <asp:ListItem Value="QA2">การบริหารการศึกษา EC</asp:ListItem>
                <asp:ListItem Value="QA3">การบริหารงานคุณภาพ EdPEx</asp:ListItem>
                <asp:ListItem Value="QA4">การบริหารงานคุณภาพ QA</asp:ListItem>
                <asp:ListItem Value="QA5">การบริหารงานคุณภาพWFM</asp:ListItem>

            </asp:CheckBoxList>
        
                    </td>
                    <td>อื่นๆ</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtQA_Training_Other" runat="server" Height="100px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
        
<div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">รายวิชาที่สอน</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <div class="box-body">   
          <table class="table">
               <tr>
                    <td><small>กรุณากรอกใส่ ตามลำดับ (ชื่อรายวิชา รหัสวิชา)</small></td>
                </tr>
                <tr>
                    <td>ผู้ประสานงานหลัก</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCourse_Main" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>ผู้ประสานผู้ร่วมสอน</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCourse_Coordinatior" runat="server" Height="100px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div> 

     <div class="box-footer">
       
     </div>

      </div>
      <!-- /.box -->   
</asp:Panel>

<div align="center">

              Status :  <asp:CheckBox ID="chkStatus" runat="server" Text="Active / ใช้งาน" />
    <br />

                <br />

          <asp:Button ID="cmdSave" runat="server" CssClass="buttonlink" Text="บันทึก" Width="100px" />
              &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="buttonlink" Text="ยกเลิก" Width="100px" />
</div>
  </section>                  

</asp:Content>
