<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="COVIDScreening.aspx.vb" Inherits=".COVIDScreening" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>การคัดกรองความเสี่ยงต่อการสัมผัสโรค COVID-19</h1>   
    </section>

<section class="content">  
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

                <h3 class="box-title">แบบรายงานความเสี่ยงต่อการสัมผัสโรค COVID-19</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table>
       <tr>
         <td width="100">ข้าพเจ้า</td>
         <td  >
             <asp:Label ID="lblStudentName" runat="server" Font-Bold="True"></asp:Label>
           </td>        
       
         <td  width="100" >รหัสนิสิต</td>
         <td  >
             <asp:Label ID="lblStudentCode" runat="server" Font-Bold="True"></asp:Label>
           </td>
         
       </tr>
       <tr>
         <td>สังกัด</td>
         <td colspan="3">
             <asp:Label ID="lblDepartment" runat="server" Font-Bold="True"></asp:Label>
           </td>
       </tr>
       <tr>
         <td>ฝึกงานผลัดที่</td>
         <td>
            <asp:DropDownList CssClass="form-control select2" ID="ddlPhase" runat="server" Width="100px" AutoPostBack="True" Font-Bold="True"></asp:DropDownList>           </td>
         <td>ชื่อแหล่งฝึก</td>
         <td>
           
             &nbsp;<asp:Label ID="lblLocationName" runat="server" Font-Bold="True"></asp:Label>
           
           &nbsp;ระหว่างวันที่
           
             <asp:Label ID="lblPhaseTime" runat="server" Font-Bold="True"></asp:Label>
           
           </td>
       </tr>
       <tr>
         <td colspan="4">ขอให้ ข้อมูลความเสี่ยงต่อการสัมผัสโรค และยืนยันการคัดกรองด้วยตนเองต่อโรค COVID-19 ในช่วง 14 วันที่ผ่านมา 
ดังนี</td>
       </tr>
       <tr>
           
         <td>1. อาการไข้</td>
         <td colspan="3">   
              <div class="form-group">
             <asp:RadioButtonList ID="optSymptom1" runat="server" RepeatDirection="Horizontal" Font-Bold="True">
                 <asp:ListItem Selected="True" Value="N">ไม่มีไข้</asp:ListItem>
                 <asp:ListItem Value="Y">มีไข้</asp:ListItem>
             </asp:RadioButtonList>
          </div> 

         </td>
       
       </tr>
     <tr>
         <td>2. อาการอื่น ๆ</td>
         <td colspan="3">
           <div class="form-group">  <asp:CheckBox ID="chkSymptom2" runat="server"  Text="ไอ" Font-Bold="True" /><asp:CheckBox ID="chkSymptom3" runat="server"  Text="เจ็บคอ" Font-Bold="True" />
             <asp:CheckBox ID="chkSymptom4" runat="server"  Text="น้ำมูกไหล" Font-Bold="True" /><asp:CheckBox ID="chkSymptom5" runat="server"  Text="เหนื่อยหอบ" Font-Bold="True" />
             <asp:CheckBox ID="chkNoSymptom" runat="server"  Text="ไม่มีอาการข้างต้น 4 อาการข้างต้น" Font-Bold="True" />
               </div>
         </td>
       </tr>
     <tr>
         <td colspan="4"><div class="form-group">3.ประวัติการเดินทางจากประเทศเกาหลีใต้สาธารณรัฐประชาชนจีนไต้หวันมาเก๊าฮ่องกงญี่ปุ่นมาเลเซีย
เวียดนามสิงคโปร์อิตาลีอิหร่านสหรัฐอเมริกาและพื้นที่หรือประเทศที่มีการระบาดของโรค COVID-19 (อ้างอิง
ตามประกาศของกระทรวงสาธารณสุขประเทศไทยและฉบับเพิ่มเติม) ในช่วงเวลา 14 วัน ที่ผ่านมา</div></td>
       </tr>
     <tr>
         <td colspan="4">             
         <div class="form-group">    <asp:RadioButtonList ID="optTravel" runat="server" RepeatDirection="Horizontal" Font-Bold="True">
                 <asp:ListItem Selected="True" Value="N">ไม่มี ประวัติ การเดินทางไปในกลุ่มประเทศดังกล่าว</asp:ListItem>
                 <asp:ListItem Value="Y">มีประวัติ การเดินทางไปกลุ่มประเทศดังกล่าว</asp:ListItem>
             </asp:RadioButtonList>
          </div> </td>
       </tr>
     <tr>
         <td colspan="4">
             <table width="100%">
             <tr>
                 <td width="350px">ประเทศ (ระบุรวมประเทศที่แวะพัก ทางผ่านหรือเปลี่ยนเครื่อง)</td>
                 <td><asp:TextBox ID="txtTravelDesc" runat="server" Width="90%" Font-Bold="True"></asp:TextBox></td>
             </tr>
             </table> 
         </td>
       </tr>
     <tr>
         <td colspan="4">             
          <div class="form-group">   <table cellpadding="0" cellspacing="0">
                 <tr>
                     <td>ช่วงเวลาที่เดินทาง</td>
                     <td>
                         <asp:TextBox ID="txtStartDate" runat="server" Font-Bold="True"></asp:TextBox>
                     </td>
                 
                     <td>กลับถึงประเทศไทยวันที่</td>
                     <td>
                         <asp:TextBox ID="txtEndDate" runat="server" Font-Bold="True"></asp:TextBox>
                     </td>
                 </tr>
                 
             </table>
              </div>
           </td>
       </tr>
     <tr>
         <td colspan="4">
             <div class="form-group">
             4. มีประวัติสัมผัสใกล้ชิดกับผู้ป่วยที่ต้องสงสัยการติดเชื้อ COVID-19 หรือไม่
             </div>    
                 </td>
       </tr>
     <tr>
         <td colspan="4">
             <table>
                 <tr>
                     <td>             
             <asp:RadioButtonList ID="optHistory" runat="server" RepeatDirection="Horizontal" Font-Bold="True">
                 <asp:ListItem Selected="True" Value="N">ไม่มีประวัติ</asp:ListItem>
                 <asp:ListItem Value="Y">มีประวัติ ระบุ</asp:ListItem>
             </asp:RadioButtonList>
                     </td>
                     <td>
                         <asp:TextBox ID="txtHistoryDesc" runat="server" Width="300px" Font-Bold="True"></asp:TextBox>
                     </td>
                 </tr>
             </table>
         </td>
       </tr>
       <tr>
         <td colspan="4" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ข้าพเจ้าขอรับรองข้อมูลการรายงานความเสี่ยงและประเมินคัดกรองโรค COVID-19 ของข้าพเจ้าข้างต้นว่า ข้อมูลดังกล่าวเป็นจริงทุกประการ และหากหลังจากเข้ารับการฝึกปฏิบัติงานแล้วเกิดความปกติใดๆ ที่ต้องสงสัยว่าเป็นผู้โรค COVID-19 และ/หรือโรคอื่นๆ ที่เป็นอันตรายต่อสถานที่และบุคคลใกล้ชิด ข้าพเจ้ายินดีเข้าสู่กระบวนการประเมิน คัดกรอง และสืบสวนโรคตามเกณฑ์มาตรฐานสากลเกณฑ์ที่กระทรวงสาธารณสุข หรือเกณฑ์ที่แหล่งฝึกกำหนด</td>
         </tr>
       <tr>
         <td colspan="4" height="50" align="center">&nbsp;</td>
         </tr>
       <tr>
         <td colspan="4" align="center">( <asp:Label ID="lblStudentName2" runat="server" Font-Bold="True"></asp:Label>
             &nbsp;)</td>
         </tr>
       <tr>
         <td colspan="4" align="center"><span class="texttopic">
            <asp:Button ID="cmdSave" runat="server" CssClass="buttonSave" Width="100" Text="บันทึก"></asp:Button>
         </span></td>
         </tr>
     </table>
      
                                                   
</div>            
            <div class="box-footer clearfix text12_nblue">
                ปรับจาก:   กรมควบคุมโรค กระทรวงสาธารณสุข. (2563). แนวทางการเฝ้าระวังและสอบสวนโรคติดเชื้อไวรัสโคโรนาสายพันธุ์ใหม่ 2019 ฉบับ 30 มกราคม2563. ค้นจาก  https://ddc.moph.go.th/viralpneumonia/guidelines.php
คณะสาธารณสุขศาสตร์ มหาวิทยาลัยเชียงใหม่. (2563). แบบคัดกรองตนเองสำหรับผู้สงสัยโรคไวรัสโคโรนา 19 (COVID-19). ค้นจาก https://cmsdm.net/Self-Screening/
             <asp:HiddenField ID="hdUID" runat="server" />
             <asp:HiddenField ID="hdLocationID" runat="server" />
            </div>
          </div>
                      
</section>
</asp:Content>
