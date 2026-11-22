<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentBio.aspx.vb" Inherits=".StudentBio" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
         <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Student
                                    <div class="page-title-subheading">ข้อมูลประวัตินิสิต/เภสัชกรประจำบ้าน</div>
                                </div>
                            </div>
                        </div>
                    </div>
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
        <td width="150" valign="top" >รหัส</td>
        <td valign="top">
            <asp:Label CssClass="text10b_pink" ID="lblStdCode" runat="server"></asp:Label>          </td>
        <td width="180" valign="top" >&nbsp;</td>
        <td valign="top">
            <asp:HiddenField ID="hdStudentID" runat="server" />
          </td>
        <td  rowspan="6" valign="top">
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
           <asp:Label ID="lblFirstName" runat="server"></asp:Label>           </td>
        <td valign="top" >นามสกุล (ภาษาไทย)</td>
        <td valign="top">
           <asp:Label ID="lblLastName" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td valign="top" >ชื่อ (ภาษาอังกฤษ)</td>
        <td valign="top">
           <asp:Label ID="lblFirstNameEN" runat="server"></asp:Label>           </td>
        <td valign="top" >นามสกุล (ภาษาอังกฤษ)</td>
        <td valign="top">
           <asp:Label ID="lblLastNameEN" runat="server"></asp:Label>           </td>
        </tr>
      <tr>
        <td valign="top" >ชื่อเล่น</td>
        <td valign="top"><asp:Label ID="lblNickName" runat="server"></asp:Label> </td>
        <td valign="top" >เพศ</td>
        <td valign="top">
            <asp:Label ID="lblGender" runat="server"></asp:Label>          </td>
        </tr>
     
      <tr>
        <td valign="top" >สาขา</td>
        <td valign="top">
            <asp:Label ID="lblMajorName" runat="server"></asp:Label>          </td>
        
        <td valign="top" >อาจารย์ที่ปรึกษา</td>
        <td valign="top" colspan="2">
            <asp:Label ID="lblAdvisorName" runat="server"></asp:Label>          </td>
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
           <asp:Label ID="lblEmail" runat="server"></asp:Label>           
           </td>
      </tr>
      <tr>
        <td valign="top" >โทรศัพท์บ้าน</td>
        <td valign="top">
           <asp:Label ID="lblTelephone" runat="server"></asp:Label>           </td>
        <td valign="top" >มือถือ</td>
        <td valign="top">
           <asp:Label ID="lblMobile" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td valign="top" >โรคประจำตัว</td>
        <td valign="top">
           <asp:Label ID="lblCongenitalDisease" runat="server"></asp:Label>           </td>
        <td valign="top" >ยาที่ใช้เป็นประจำ</td>
        <td valign="top">
           <asp:Label ID="lblMedicineUsually" runat="server"></asp:Label>           </td>
      </tr>
     
      <tr>
        <td  colspan="2">ประวัติการแพ้ยา</td>
        <td  colspan="2">คําแนะนําในการปฏิบัติตัวเมื่อมีอาการ</td>
      </tr>
     
      <tr>
        <td colspan="2">
           <asp:Label ID="lblMedicalHistory" runat="server"></asp:Label>           </td>
        <td colspan="2">
           <asp:Label ID="lblMedicalRecommend" runat="server" ></asp:Label>           </td>
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
        <td  width="150">ชื่อบิดา</td>
        <td>
           <asp:Label ID="lblFather_FirstName" runat="server"></asp:Label>           </td>
        <td >นามสกุลบิดา</td>
        <td>
           <asp:Label ID="lblFather_LastName" runat="server"></asp:Label>           </td>
      </tr>
       <tr>
        <td >อาชีพบิดา</td>
        <td>
           <asp:Label ID="lblFather_Career" runat="server"></asp:Label>           </td>
        <td >เบอร์โทรศัพท์บิดา</td>
        <td>
           <asp:Label ID="lblFather_Tel" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >ชื่อมารดา</td>
        <td>
           <asp:Label ID="lblMother_FirstName" runat="server"></asp:Label>           </td>
        <td >นามสกุลมารดา</td>
        <td>
           <asp:Label ID="lblMother_LastName" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >อาชีพมารดา</td>
        <td>
           <asp:Label ID="lblMother_Career" runat="server"></asp:Label>           </td>
        <td >เบอร์โทรศัพท์มารดา</td>
        <td>
           <asp:Label ID="lblMother_Tel" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >จำนวนพี่น้อง</td>
        <td>
           <asp:Label ID="lblSibling" runat="server"></asp:Label>           </td>
        <td >เป็นลูกคนที่</td>
        <td>
           <asp:Label ID="lblChildNo" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >อุปนิสัย</td>
        <td colspan="3">
           <asp:Label ID="lblCharacter" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >งานอดิเรก</td>
        <td colspan="3">
           <asp:Label ID="lblHobby" runat="server"></asp:Label>           </td>
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
           <asp:Label ID="lblDistrict" runat="server"></asp:Label>           </td>
        <td >อำเภอ</td>
        <td>
           <asp:Label ID="lblCity" runat="server"></asp:Label>           </td>
      </tr>
      <tr>
        <td >จังหวัด</td>
        <td>
            <asp:Label ID="lblProvince" runat="server"></asp:Label>
          </td>
        <td >รหัสไปรษณีย์</td>
        <td>
           <asp:Label ID="lblZipCode" runat="server"></asp:Label>           </td>
      </tr>
    </table>

                 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
  

            
 </section>

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
        <td>ชื่อ</td>
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
                       วิชาที่ชอบเรียน :<asp:Label ID="lblFavoriteSubject" runat="server" CssClass="form-control"></asp:Label>                   
               </div> 
        
            <div class="form-group">
                ประสบการณ์การทํางาน/กิจกรรมระหว่างศึกษา<br />   
           <asp:Label ID="lblExperience" runat="server" CssClass="form-control"></asp:Label>
   
             </div>
                 <div class="form-group">                
                      ความสามารถพิเศษ<br /><asp:Label ID="lblTalent" runat="server" CssClass="form-control"></asp:Label> 
                     </div>
                 <div class="form-group">                   
           ความคาดหวังในอาชีพ<br /><asp:Label ID="lblExpectations" runat="server" CssClass="form-control"></asp:Label> 
             
               </div> 
                 </div>            
</div>
            
    
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">ข้อมูลอื่นๆ</h3>
             
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
                  <label>เลขที่บัญชีธนาคาร</label>                   
                  <asp:label ID="lblAccount" runat="server" CssClass="form-control"></asp:label>
                </div>
              </div>
                    <div class="col-md-6">
                <div class="form-group">
                  <label>ธนาคาร</label>                   
                    <asp:label ID="lblBank" runat="server"></asp:label>
                </div>
              </div>

                </div>
                      <div class="row">
                     <div class="col-md-6">
                <div class="form-group">
                  <label>สถานที่ทำงาน</label>                   
                  <asp:label ID="lblWorkPlace" runat="server" CssClass="form-control"></asp:label>
                </div>
              </div>
                    <div class="col-md-3">
                <div class="form-group">
                  <label>Size เสื้อกาวน์</label>                   
                  <asp:label ID="lblSize" runat="server" CssClass="form-control"></asp:label>
                </div>
              </div>
                           <div class="col-md-3">
                <div class="form-group">
                  <label>กรมธรรม์ Covid-19</label>                   
                    <asp:label ID="lblCovid" runat="server" > 
                    </asp:label>
                </div>
              </div>

                </div>
                
     
    </div>
</div>

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">Resume/Social</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">
                 <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:HyperLink ID="hlnkCV" runat="server" CssClass="label label-success" NavigateUrl="#" Target="_blank">คลิกดู Personal CV Link</asp:HyperLink></td>                       
                        <td><asp:HyperLink ID="hplLineID" runat="server" NavigateUrl="#" Target="_blank" ImageUrl="images/line.png"></asp:HyperLink></td>
                    </tr>
                  
                </table>

               
    </div>
</div>


</section>
        
</div>
  <div class="row"> 
<section class="col-lg-12 connectedSortable"> 
  
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
   <table align="center" class="table no-border">
   
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
           <asp:Label ID="lblPrimarySchool" runat="server"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblPrimaryProvince" runat="server"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblPrimaryYear" runat="server" MaxLength="4" Width="77px"></asp:Label> 
          </td>
        </tr>
      <tr class="odd">
        <th align="left" scope="row">มัธยมศึกษาตอนต้น</th>
        <td>
           <asp:Label ID="lblSecondarySchool" runat="server"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblSecondaryProvince" runat="server"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblSecondaryYear" runat="server" MaxLength="4" Width="77px"></asp:Label> 
          </td>
        </tr>
      <tr >
        <th align="left" scope="row">มัธยมศึกษาตอนปลาย</th>
        <td>
           <asp:Label ID="lblHighSchool" runat="server"></asp:Label>           </td>
        <td align="center">
           <asp:Label ID="lblHighProvince" runat="server"></asp:Label>           </td>
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
      </div>


<div class="row justify-content-center">   
          <asp:HyperLink ID="hlnkBack" runat="server" CssClass="btn btn-warning">แก้ไขประวัติ</asp:HyperLink>&nbsp;<asp:HyperLink ID="hlnkPrint" runat="server"   CssClass="btn btn-warning" Target="_blank">พิมพ์ประวัติ</asp:HyperLink>

   </div>
      
</section>
</asp:Content>
 

