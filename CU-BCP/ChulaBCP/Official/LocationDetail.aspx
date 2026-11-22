<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="LocationDetail.aspx.vb" Inherits=".LocationDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
  
<script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCwjs6-YQxdxIJvdUkOV7RFTpACE4dcH-E&callback=initMap">   </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<section class="content-header">
      <h1 align="center"><asp:Label ID="lblName" runat="server"></asp:Label><small></small></h1>     
</section>

<section class="content">       
      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
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
                <table width="100%" border="0"  class="table no-border">
<tr>
                                                  <td width="150" align="left" class="texttopic">ที่อยู่ :</td>
                                                  <td colspan="3" align="left">
                                                      <asp:Label ID="lblAddress" runat="server" ></asp:Label>                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        จังหวัด :                                                        </td>
                                                    <td align="left"><asp:Label ID="lblProvince" runat="server"></asp:Label>
                                                    &nbsp; <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                                    </td>
                                                  <td width="100" align="left" class="texttopic">รหัสไปรษณีย์:</td>
                                                    <td align="left">
                                                  <asp:Label ID="lblZipCode" runat="server" 
                                                        ></asp:Label></td>
          </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">โทรศัพท์ :                                                    </td>
                                                    <td align="left">
                                                  <asp:Label ID="lblTel" runat="server"></asp:Label>                                                    </td>
                                                  <td align="left" class="texttopic">โทรสาร :</td>
                                                    <td align="left">
                                                  <asp:Label ID="lblFax" runat="server"></asp:Label>                                                    </td>
          </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">ชื่อผู้ประสานงาน</td>
                                                  <td align="left">
                                                  <asp:Label ID="lblCoName" runat="server"></asp:Label>                                                    </td>
                                                  <td align="left" class="texttopic">ตำแหน่ง</td>
                                                  <td align="left">
                                                  <asp:Label ID="lblCoPosition" runat="server"></asp:Label>                                                    </td>
          </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">E-mail :</td>
                                                  <td align="left">
                                                  <asp:Label ID="lblCoMail" runat="server"></asp:Label>                                                    </td>
                                                  <td align="left" class="texttopic">โทรศัพท์ :</td>
                                                  <td align="left">
                                                  <asp:Label ID="lblCoTel" runat="server"></asp:Label>                                                    </td>
          </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">Web site</td>
                                                  <td align="left">
                                                      <asp:HyperLink ID="hplWebsite" runat="server" Target="_blank">[hplWebsite]</asp:HyperLink>
                                                    </td>
                                                  <td align="left" class="texttopic">Facebook</td>
                                                  <td align="left">
                                                      <asp:HyperLink ID="hplFacebook" runat="server" Target="_blank">[hplFacebook]</asp:HyperLink>
                                                    </td>
          </tr>
  </table>
         </div>
            <div class="box-footer clearfix">           
                <asp:HiddenField ID="hdLocationID" runat="server" />
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
                   <table width="100%" border="0"  class="table no-border">
            <tr>
              <td width="150" class="texttopic">ประเภท :</td>
              <td align="left"><asp:Label ID="lblGroup" runat="server"></asp:Label>                  </td>
              <td align="left">&nbsp;</td>
            </tr>
            <tr>
              <td class="texttopic">
                  <asp:Label ID="lblType" runat="server" Text="กลุ่ม :"></asp:Label>                </td>
              <td align="left"><asp:Label ID="lblLocationType" runat="server"></asp:Label>                  </td>
              <td align="left"  class="texttopic">
                <asp:Label ID="lblField" runat="server" Text="จำนวนเตียง :"></asp:Label>
                <asp:Label ID="lblFieldCount" runat="server"></asp:Label></td>
            </tr>
           
            <tr>
              <td colspan="3" class="texttopic"><asp:Label ID="lblDepartment" runat="server" Text="สังกัด :"></asp:Label>                &nbsp;<asp:Label ID="lblISO" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td class="texttopic"><asp:Label ID="lblOpen" runat="server" Text="เวลาเปิดทำการ :"></asp:Label></td>
              <td align="left"><asp:Label ID="lblOfficeHour" runat="server"></asp:Label>&nbsp;</td>
              <td align="left"><table border="0"  class="table no-border">
                <tr>
                  <td class="texttopic"><asp:Label ID="lblOfficer" runat="server" 
                          Text="จำนวนเภสัชกรที่ปฏิบัติงานเต็มเวลา (ต่อ 1 สาขา) :"></asp:Label></td>
                  <td><asp:Label ID="lblOfficerNo" runat="server" ></asp:Label>                  </td>
                </tr>
              </table></td>
            </tr>
            
         </table>
 <asp:Panel ID="pnWorkBrunch" runat="server">
          
          <table width="100%" border="0"  class="table no-border">
            <tr>
              <td>
              <asp:Label ID="lblBranch" runat="server"></asp:Label>                </td>
            
            </tr>
            <tr> 
              <td><asp:label ID="lblBrunchDetail" runat="server" 
                     ></asp:label></td>
             
            </tr>
          </table>
             
             </asp:Panel>
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
                            
              <table width="100%" border="0"  class="table no-border">
            <tr>
        <td>
                  <asp:Label ID="lblRoom" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
              <td>
                  <asp:Panel ID="pnRoom1" runat="server" class="block_step1">
                
                  <table width="100%" border="0"  class="table no-border">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:label ID="lblApartmentName1" runat="server"></asp:label>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:label ID="lblApartmentTel1" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:label ID="lblApartmentAddress1" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:label ID="lblTravel1" runat="server"></asp:label>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:label ID="lblDistance1" runat="server"></asp:label>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>
              
              
                  <asp:Panel ID="pnRoom2" runat="server" class="block_step1">
                
                  <table width="100%" border="0"  class="table no-border">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:label ID="lblApartmentName2" runat="server"></asp:label>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:label ID="lblApartmentTel2" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:label ID="lblApartmentAddress2" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:label ID="lblTravel2" runat="server"></asp:label>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:label ID="lblDistance2" runat="server"></asp:label>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>
              
              
                  <asp:Panel ID="pnRoom3" runat="server" class="block_step1">
                
                  <table width="100%" border="0"  class="table no-border">
                <tr>
                  <td width="100">ชื่อที่พัก</td>
                  <td>
                                                      <asp:label ID="lblApartmentName3" runat="server"></asp:label>                                                    </td>
                  <td width="100">เบอร์โทร</td>
                  <td>
                                                        <asp:label ID="lblApartmentTel3" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>ที่อยู่</td>
                  <td colspan="3">
                                                        <asp:label ID="lblApartmentAddress3" runat="server"></asp:label>                                                    </td>
                </tr>
                <tr>
                  <td>การเดินทาง</td>
                  <td>
                                                      <asp:label ID="lblTravel3" runat="server"></asp:label>                                                    </td>
                  <td>ระยะทาง</td>
                  <td>
                                                      <asp:label ID="lblDistance3" runat="server"></asp:label>                                                    </td>
                </tr>
              </table>
              
                </asp:Panel>
              
              
              </td>
            </tr>
          </table>
           

        </div>
          
          </div> 
 </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
        <section class="col-lg-5 connectedSortable">           
 
   <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-clock-o"></i>

              <h3 class="box-title">วัน-เวลาที่สามารถให้นิสิตฝึกปฏิบัติงาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                   <table border="0"  class="table no-border">
            <tr>
              <td width="30"  class="texttopic">วัน</td>
              <td>
                  <asp:label ID="lblDay" runat="server"></asp:label>                </td>
            </tr>
            <tr>
              <td  class="texttopic" >เวลา</td>
              <td>
                      <asp:label ID="lblOfficeTime" runat="server"></asp:label>                    </td>
            </tr>
          </table>

        </div>
        
          </div>
                 
  
 <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-medkit"></i>

              <h3 class="box-title">งานที่สามารถให้นิสิตฝึกปฏิบัติได้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                <asp:Panel ID="pnWorkList" runat="server">       
                 
                     งานที่สามารถให้นิสิตฝึกปฏิบัติได้<br />
                 <asp:Label ID="lblWork" runat="server" class="form-control"></asp:Label>
              
                
                     งานอื่นๆ<br />
                 <asp:Label ID="lblOtherWork" runat="server" class="form-control"></asp:Label>
                </asp:Panel>
งานเด่นของแหล่งฝึก :<br />
             <asp:label ID="lblTopWork" runat="server" class="form-control"></asp:label>                                                  
         สิ่งอื่นๆที่นิสิตควรทราบหรือเตรียมตัวก่อนมาฝึกปฏิบัติงาน :<br />
            <asp:label ID="lblRemark"                   runat="server" class="form-control"></asp:label> 
       

        </div>
          
          </div>
            <% If lat > 0 %>
         <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-map-marker"></i>

              <h3 class="box-title">แผนที่</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                   <div id='map' style='width:100%; height:300px; '></div>

    <script>
        var jsonObj = <%=json%>

        function initMap() {
            var mapOptions = {
                center: { lat:  <%=lat%>, lng:  <%=lng%> },
                zoom: 15,
            }

            var maps = new google.maps.Map(document.getElementById("map"), mapOptions);

            var marker, info;

            $.each(jsonObj, function (i, item) {

                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(item.lat, item.lng),
                    map: maps,
                    title: item.name
                });

                info = new google.maps.InfoWindow();

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        info.setContent(item.name);
                        info.open(maps, marker);
                    }
                })(marker, i));

            });

        }
    </script>
     

        </div>
       
          </div>
            <% End If %>
            </section>
        <!-- right col -->
      </div>
      <!-- /.row (main row) -->
    <div class="row">
           <section class="col-lg-12 connectedSortable">      


<asp:Panel ID="pnSelectPhase" runat="server">   
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-leaf"></i>

              <h3 class="box-title">ข้อมูลการรับนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body"> 
              <asp:GridView ID="grdREQ" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="TimePhaseID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="ผลัด" DataField="PhaseNo">
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="PhaseName" HeaderText="วันที่ฝึก">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="Man" HeaderText="ชาย" />
                <asp:BoundField DataField="Women" HeaderText="หญิง" />
                <asp:BoundField DataField="NoSpec" HeaderText="ไม่ระบุเพศ*" />
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>                   

        </div>
            <div class="box-footer clearfix">
           * กรณีไม่ระบุเพศ ต้องการนิสิตเพศเดียวกัน : &nbsp;<asp:Label ID="lblSameGender" runat="server" Font-Bold="True" 
                                                                      Font-Underline="True"></asp:Label>
            </div>
          </div>
      
   <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-mouse-pointer"></i>

              <h3 class="box-title">เลือกผลัดฝึกที่ต้องการ</h3>
             
                 <div class="box-tools pull-right">
               
              </div>

                 
            </div>
            <div class="box-body">

                   <table border="0"  class="table no-border" align="center">
                <tr>
                  <td>
                      <asp:Label ID="lblselect" runat="server" Text="เลือกผลัดฝึก"></asp:Label>
&nbsp;</td>
                  <td>
                      <asp:DropDownList ID="ddlPhase" runat="server" CssClass="Objcontrol">
                      </asp:DropDownList>
                    </td>
                  <td><asp:Button ID="lnkSelect" CssClass="buttonSave" runat="server" Width="80px" Text="ตกลง"></asp:Button>
            </td>
                </tr>
              </table>

        </div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblMsg" runat="server" Visible="False" CssClass="text14_red"></asp:Label> 
            </div>
          </div>
       </asp:Panel>       

           </section>    
        </div>
      
    </section>
</asp:Content>
