<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Student_Bio.aspx.vb" Inherits=".Student_Bio" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link rel="stylesheet" type="text/css" href="css/custyles.css">
<link rel="stylesheet" type="text/css" href="css/uidialog.css">
<link rel="stylesheet" type="text/css" href="css/dc_table.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>ข้อมูลประวัตินิสิต
          <small></small>              
      </h1> 
    </section>
    <section class="content">
      <!-- Main row -->
<div class="row">
        <!-- Left col -->
<section class="col-lg-8 connectedSortable"> 
             <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-info-circle"></i>

              <h3 class="box-title">ข้อมูลทั่วไป</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
        
        <table class="table table-condensed">
      <tr>
        <td width="150" valign="top" >รหัสนิสิต</td>
        <td valign="top">
            <asp:Label CssClass="text10b_pink" ID="lblStdCode" runat="server"></asp:Label>          </td>
        <td width="150" valign="top" >&nbsp;</td>
        <td valign="top">
            <asp:HiddenField ID="hdStudentID" runat="server" />
          </td>
        <td width="150" rowspan="6" valign="top">
                </td>
      </tr>
      <tr>
        <td valign="top" >คำนำหน้า</td>
        <td valign="top">
            <asp:Label ID="lblPrefix" runat="server"></asp:Label>          </td>
        <td valign="top" >&nbsp;</td>
        <td valign="top">&nbsp;</td>
        </tr>
      <tr>
        <td valign="top" >ชื่อ (ภาษาไทย)</td>
        <td valign="top">
           <asp:Label ID="lblFirstName" runat="server" Width="200px"></asp:Label>           </td>
        <td valign="top" >นามสกุล (ภาษาไทย)</td>
        <td valign="top">
           <asp:Label ID="lblLastName" runat="server" Width="200px"></asp:Label>           </td>
        </tr>
      <tr>
        <td valign="top" >ชื่อ (ภาษาอังกฤษ)</td>
        <td valign="top">
           <asp:Label ID="lblFirstNameEN" runat="server" Width="200px"></asp:Label>           </td>
        <td valign="top" >นามสกุล (ภาษาอังกฤษ)</td>
        <td valign="top">
           <asp:Label ID="lblLastNameEN" runat="server" Width="200px"></asp:Label>           </td>
        </tr>
      <tr>
        <td valign="top" >ชื่อเล่น</td>
        <td valign="top"><asp:Label ID="lblNickName" runat="server"></asp:Label> </td>
        <td valign="top" >เพศ</td>
        <td valign="top">
            <asp:Label ID="lblGender" runat="server"></asp:Label>          </td>
        </tr>
     
      <tr>
        <td valign="top" >สาขาวิชา</td>
        <td valign="top">
            <asp:Label ID="lblMajorName" runat="server"></asp:Label>          </td>
        <td valign="top" >สาขา (แขนง)</td>
        <td valign="top">
            <asp:Label ID="lblMinorName" runat="server"></asp:Label>          </td>
        </tr>
      <tr>
      
        <td valign="top" >อาจารย์ที่ปรึกษา (Academic Advisor)</td>
        <td valign="top" colspan="2">
            <asp:Label ID="lblAdvisorName" runat="server"></asp:Label>          </td>

            <td valign="top" >อาจารย์ที่ปรึกษาหลักโครงการปริญญานิพนธ์ (Senior Project)</td>

  <td valign="top"> <asp:Label ID="lblSeniorProject" runat="server"></asp:Label>   
            </td>
        </tr>
      <tr>
        <td valign="top" >GPAX</td>
        <td valign="top">
            <asp:Label ID="lblGPAX" runat="server"></asp:Label>          </td>
        <td valign="top" >สถานะ</td>
        <td valign="top" colspan="2">
            <asp:Label ID="lblStudentStatus" runat="server"></asp:Label>          </td>
        </tr>
      
    </table>


 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-asterisk"></i>

              <h3 class="box-title">ข้อมูลส่วนตัว</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
                 
            </div>
            <div class="box-body">
                 <table width="100%" border="0" class="table table-condensed">
       <tr>
        <td >วันเกิด</td>
        <td>  
            <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
           </td>
        <td >กรุ๊ปเลือด</td>
        <td>
            <asp:Label ID="lblBloodGroup" runat="server"></asp:Label>
           </td>
      </tr>
       <tr>
        <td >ศาสนา</td>
        <td>  
            <asp:Label ID="lblReligion" runat="server"></asp:Label>
           </td>
        <td >อีเมล</td>
        <td>
           <asp:Label ID="lblEmail" runat="server" Width="200px"></asp:Label>           
           </td>
      </tr>
      <tr>
        <td valign="top" >โทรศัพท์บ้าน</td>
        <td valign="top">
           <asp:Label ID="lblTelephone" runat="server" Width="200px"></asp:Label>           </td>
        <td valign="top" >มือถือ</td>
        <td valign="top">
           <asp:Label ID="lblMobile" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
      <tr>
        <td valign="top" >โรคประจำตัว</td>
        <td valign="top">
           <asp:Label ID="lblCongenitalDisease" runat="server" Width="200px"></asp:Label>           </td>
        <td valign="top" >ยาที่ใช้เป็นประจำ</td>
        <td valign="top">
           <asp:Label ID="lblMedicineUsually" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
     
      <tr>
        <td  colspan="2">ประวัติการแพ้ยา</td>
        <td  colspan="2">คําแนะนําในการปฏิบัติตัวเมื่อมีอาการ</td>
      </tr>
     
      <tr>
        <td colspan="2">
           <asp:Label ID="lblMedicalHistory" runat="server" Width="90%"></asp:Label>           </td>
        <td colspan="2">
           <asp:Label ID="lblMedicalRecommend" runat="server" Width="90%"></asp:Label>           </td>
      </tr>
      <tr>
        <td >สิทธิการรักษา</td>
        <td colspan="3">
           <asp:Label ID="lblClaim" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >ชื่อสถานพยาบาล</td>
        <td>
           <asp:Label ID="lblHospitalName" runat="server"></asp:Label>           </td>
        <td >ชื่อแพทย์ที่รักษา</td>
        <td>
           <asp:Label ID="lblDoctorName" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >&nbsp;</td>
        <td>
            &nbsp;</td>
        <td >&nbsp;</td>
        <td>
            &nbsp;</td>
      </tr>
      <tr>
        <td >ชื่อบิดา</td>
        <td>
           <asp:Label ID="lblFather_FirstName" runat="server" Width="200px"></asp:Label>           </td>
        <td >นามสกุลบิดา</td>
        <td>
           <asp:Label ID="lblFather_LastName" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
       <tr>
        <td >อาชีพบิดา</td>
        <td>
           <asp:Label ID="lblFather_Career" runat="server" Width="200px"></asp:Label>           </td>
        <td >เบอร์โทรศัพท์บิดา</td>
        <td>
           <asp:Label ID="lblFather_Tel" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
      <tr>
        <td >ชื่อมารดา</td>
        <td>
           <asp:Label ID="lblMother_FirstName" runat="server" Width="200px"></asp:Label>           </td>
        <td >นามสกุลมารดา</td>
        <td>
           <asp:Label ID="lblMother_LastName" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
      <tr>
        <td >อาชีพมารดา</td>
        <td>
           <asp:Label ID="lblMother_Career" runat="server" Width="200px"></asp:Label>           </td>
        <td >เบอร์โทรศัพท์มารดา</td>
        <td>
           <asp:Label ID="lblMother_Tel" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
      <tr>
        <td >จำนวนพี่น้อง</td>
        <td>
           <asp:Label ID="lblSibling" runat="server" Width="62px"></asp:Label>           </td>
        <td >เป็นลูกคนที่</td>
        <td>
           <asp:Label ID="lblChildNo" runat="server" Width="46px"></asp:Label>           </td>
      </tr>
      <tr>
        <td >อุปนิสัย</td>
        <td colspan="3">
           <asp:Label ID="lblCharacter" runat="server" Width="95%"></asp:Label>           </td>
      </tr>
      <tr>
        <td >งานอดิเรก</td>
        <td colspan="3">
           <asp:Label ID="lblHobby" runat="server" Width="95%"></asp:Label>           </td>
      </tr>
     
      <tr>
        <td >ที่อยู่ปัจจุบันระหว่างศึกษา</td>
        <td colspan="3">
           <asp:Label ID="lblAddressLive" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td >โทรศัพท์</td>
        <td colspan="3">
           <asp:Label ID="lblTelLive" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td >ที่อยู่ตามบัตร ปชช. </td>
        <td colspan="3">
           <asp:Label ID="lblAddress" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td >ตำบล</td>
        <td>
           <asp:Label ID="lblDistrict" runat="server" Width="200px"></asp:Label>           </td>
        <td >อำเภอ</td>
        <td>
           <asp:Label ID="lblCity" runat="server" Width="200px"></asp:Label>           </td>
      </tr>
      <tr>
        <td >จังหวัด</td>
        <td>
            <asp:Label ID="lblProvince" runat="server"></asp:Label>
          </td>
        <td >รหัสไปรษณีย์</td>
        <td>
           <asp:Label ID="lblZipCode" runat="server" Width="70px" MaxLength="5"></asp:Label>           </td>
      </tr>
    </table>

                 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
           <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-graduation-cap"></i>

              <h3 class="box-title">ประวัติการศึกษา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
                 
            </div>
            <div class="box-body">
                 <table align="center" class="table dc_table_s3">
   
    <thead>
      <tr>
        <th scope="col">ระดับ</th>
        <th scope="col">สถานศึกษา</th>
        <th scope="col">จังหวัด</th>
        <th scope="col">ปีที่จบ</th>
        </tr>
    </thead>
    <tfoot>
    </tfoot>
    <tbody>
      <tr >
        <th align="left" scope="row">ประถมศึกษา</th>
        <td>
           <asp:Label ID="lblPrimarySchool" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblPrimaryProvince" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblPrimaryYear" runat="server" MaxLength="4" Width="77px"></asp:Label> 
          </td>
        </tr>
      <tr class="odd">
        <th align="left" scope="row">มัธยมศึกษาตอนต้น</th>
        <td>
           <asp:Label ID="lblSecondarySchool" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblSecondaryProvince" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblSecondaryYear" runat="server" MaxLength="4" Width="77px"></asp:Label> 
          </td>
        </tr>
      <tr >
        <th align="left" scope="row">มัธยมศึกษาตอนปลาย</th>
        <td>
           <asp:Label ID="lblHighSchool" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblHighProvince" runat="server" Width="200px"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblHighYear" runat="server" MaxLength="4" Width="77px"></asp:Label> 
          </td>
        </tr>
    </tbody>
  </table>
    
    <br />            

<div class="MenuSt">วุฒิการศึกษาอื่น</div>
              <asp:GridView ID="grdEdu" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="CourseName" HeaderText="หลักสูตร">
                </asp:BoundField>
                <asp:BoundField DataField="Institute" HeaderText="สถานศึกษา" />
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" /> 
                </asp:BoundField>
            <asp:BoundField HeaderText="ปีที่จบ" DataField="EduYear">
              <itemstyle HorizontalAlign="Center" />                      </asp:BoundField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView> 
</div>

<div class="box-footer clearfix"></div>
</div>

            
 </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
<section class="col-lg-4 connectedSortable">

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-picture-o"></i>

              <h3 class="box-title">รูปประจำตัว</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body text-center">
                <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" Width="150px" />

                 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>     

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-ambulance"></i>

              <h3 class="box-title">ผู้ติดต่อกรณีฉุกเฉิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
  <table border="0"  class="table table-condensed">
      <tr>
        <td width="80px">ชื่อ</td>
        <td>
           <asp:Label ID="lblContactName" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >เบอร์โทร</td>
        <td><asp:Label ID="lblContactTel" runat="server"></asp:Label></td>
      
      </tr>
      <tr>
        <td >ที่อยู่</td>
        <td>
           <asp:Label ID="lblContactAddress" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td >เกี่ยวข้องเป็น</td>
        <td>
           <asp:Label ID="lblContactRelation" runat="server" ></asp:Label>           </td>
      </tr>
    </table>

                 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-soccer-ball-o"></i>

              <h3 class="box-title">ประสบการณ์/กิจกรรม/ความสามารถพิเศษ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
               <div class="form-group">
                   <div class="form-control">
                       วิชาที่ชอบเรียน :<asp:Label ID="lblFavoriteSubject" runat="server"></asp:Label>
                </div>
                   
               </div> 
        
            <div class="form-group">
                   <div class="form-control">     
             
                ประสบการณ์การทํางาน/กิจกรรมระหว่างศึกษา<br />
   
           <asp:Label ID="lblExperience" runat="server"></asp:Label>
            </div>
             </div>
                 <div class="form-group">
                <div class="form-control">     
                      ความสามารถพิเศษ<br />         
           <asp:Label ID="lblTalent" runat="server" Width="100%"></asp:Label>       
               </div>
                     </div>
                 <div class="form-group">
                     <div class="form-control">
           ความคาดหวังในอาชีพ<br /><asp:Label ID="lblExpectations" runat="server" Width="100%"></asp:Label> 
                    </div>
               </div> 
                 </div>            
</div>
            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">Resume/CV</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                <asp:HyperLink ID="hlnkCV" runat="server" CssClass="label label-success" NavigateUrl="#" Target="_blank">คลิกดู Personal CV Link</asp:HyperLink>
    </div>
</div>

</section>
        <!-- right col -->
</div>
      <!-- /.row (main row) -->
<div class="row" align="center">
         <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                 TargetControlID="Panel1"
    PopupControlID="Panel1" 
    DropShadow="false"
    PopupDragHandleControlID="headbox" BackgroundCssClass="dialog_bg" Drag="True" 
            OkControlID="btnOK" >
            </cc2:ModalPopupExtender>
       <asp:Panel ID="Panel1" runat="server" Style="display: none;"   DefaultButton="btnOk">
         
  <table width="300" border="0" align="center" cellpadding="2" cellspacing="0" class="dialog_box">
    <tr>
      <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td class="dialog_box title" id="headbox">ผลการทำงาน</td>
        </tr>
        <tr>
          <td align="center" class="dialog_content">
              <asp:Label ID="lblAlert" runat="server">บันทึกข้อมูลเรียบร้อยแล้ว</asp:Label>
  </td>
        </tr>
        <tr>
          <td align="center">
              <asp:Button ID="btnOK" runat="server" Text="Close" CssClass="buttonModal" />
            </td>
        </tr>
      </table>
      
      </td>
    </tr>
  </table>
      </asp:Panel>
            
      <script type="text/javascript">
          function fnClickOK(sender, e) {
              __doPostBack(sender, e);
          }
</script>

          <asp:HyperLink ID="hlnkBack" runat="server" CssClass="buttonLink">แก้ไขประวัติ</asp:HyperLink>
<asp:HyperLink ID="hlnkPrint" runat="server"   CssClass="buttonLink" Target="_blank">พิมพ์ประวัติ</asp:HyperLink>

   </div>
      
</section>
</asp:Content>
 

