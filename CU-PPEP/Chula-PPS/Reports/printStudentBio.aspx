<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="printStudentBio.aspx.vb" Inherits="printStudentBio" %>
<%@ Register assembly="EO.Web" namespace="EO.Web" tagprefix="eo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="css/reports.css" rel="stylesheet" type="text/css" />
      
<title></title>

</head>
<body>
    <form id="form1" runat="server">
       
  <script language="Javascript">
      function doprint() {
          //save existing user's info
          //  var h = factory.printing.header;
          //  var f = factory.printing.footer;
          //hide the button
          document.all("cmdPrint").style.visibility = 'hidden';
          window.print();
          ////  factory.printing.SetMarginMeasure(2); 
          //  factory.printing.portrait = true;
          //  factory.printing.leftMargin = 1.75;
          //  factory.printing.topMargin = 1.75;
          //  factory.printing.rightMargin = 0.75;
          //  factory.printing.bottomMargin = 1.75;


          ////set header and footer to blank
          //  factory.printing.header = "";
          //  factory.printing.footer = "";
          //  //print page without prompt
          //  factory.DoPrint(false);
          //  //restore user's info
          //  factory.printing.header = h;
          //  factory.printing.footer = f;
          //show the print button
          // document.all("prnButton").style.visibility = 'visible';
          //  document.all("AButton").style.visibility = 'visible';
          //  document.all("BButton").style.visibility = 'visible';
          document.all("cmdPrint").style.visibility = 'visible';
      }
   
    </script>


 <br> <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" >
  <tr>
    <td>

<table width="700" border="0" align="center" cellpadding="0" cellspacing="2">
  <tr>
    <td>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
   <tr>
     <td align="center"><table width="100%" border="0" cellspacing="0" cellpadding="0">
       <tr>
         <td rowspan="2" valign="bottom"><table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
           <tr>
             <td align="center" class="Page_Header">ประวัตินิสิต</td>
           </tr>
             <tr>
             <td align="center" class="Page_Header">************************</td>
           </tr>
           <tr>
             <td align="center" class="Page_Header">ฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</td>
           </tr>
        
         </table></td>
         <td width="150" align="center">&nbsp;
             </td>
       </tr>
       <tr>
         <td align="center">
             <asp:Image ID="picStudent" runat="server" Width="120px" Height="130px" />           </td>
       </tr>
     </table></td>
     </tr>
   <tr>
     <td align="center" >&nbsp;</td>
     </tr>
</table>
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
     
      <tr>
        <td align="left" valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0">
       <tr>
         <td width="50" class="texttopic">ชื่อ-สกุล</td>
         <td class="TextInputUnderline">
           <asp:Label ID="lblName" runat="server"></asp:Label>         </td>
         <td width="40" class="texttopic">ชื่อเล่น</td>
         <td width="120" class="TextInputUnderline">
             <asp:Label ID="lblNickName" 
                 runat="server" Width="100%"></asp:Label></td>
         </tr>
     </table></td>
      </tr>
           <tr>
        <td valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="50">E-Mail</td>
            <td  class="TextInputUnderline">
              <asp:Label ID="lblMail" runat="server"></asp:Label>
            </td>
          </tr>
        </table></td>
        </tr>
      <tr>
        <td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            
            <td width="90" class="texttopic">รหัสประจำตัว</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblStudentCode" runat="server"></asp:Label></td>
            <td width="50" align="center" class="texttopic">GPAX</td>
            <td width="50" align="center" class="TextInputUnderline"><asp:Label ID="lblGPAX" runat="server"></asp:Label></td>
            <td width="60" align="center" class="texttopic">หมู่โลหิต</td>
            <td width="100" align="center" class="TextInputUnderline">
              <asp:Label ID="lblBloodGroup" runat="server"></asp:Label>            </td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
           
            <td width="80" class="texttopic">ที่อยู่ปัจจุบัน</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblAddress" runat="server"></asp:Label>
              </td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="bottom"><table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td width="80" class="texttopic">เบอร์มือถือ</td>
            <td width="250" class="TextInputUnderline"><asp:Label ID="lblMobile" runat="server"></asp:Label></td>
            <td width="80" align="center" class="texttopic">เบอร์บ้าน</td>
            <td class="TextInputUnderline"><asp:Label ID="lblTel" runat="server"></asp:Label></td>
          </tr>
        </table></td>
      </tr>
       
     
      <tr>
        <td valign="bottom">
        <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            
             <td width="100" class="texttopic"  >โรคประจำตัว</td>
            <td class="TextInputUnderline"><asp:Label ID="lblCongenitalDisease" runat="server"></asp:Label></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="bottom"><table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
                  
            <td width="100" class="texttopic">สาขาวิชา</td>
            <td class="TextInputUnderline" >
                <asp:Label ID="lblMajor" runat="server"></asp:Label>              </td>
            </tr>
        </table></td>
      </tr>
        <tr>
        <td  valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="50">วันเกิด</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
            </td>
            <td width="100">มีพี่น้องทั้งหมด</td>
            <td width="50" align="center" class="TextInputUnderline">
              <asp:Label ID="lblSibling" runat="server"></asp:Label>
            </td>
            <td width="100">คน เป็นคนที่</td>
            <td width="50" align="center" class="TextInputUnderline">
              <asp:Label ID="lblChildNo" runat="server"></asp:Label>
            </td>
          </tr>
        </table></td>
      </tr>
        <tr>
        <td  valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="55">ชื่อบิดา</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblFname" 
                    runat="server"></asp:Label>
            </td>
            <td width="40">อาชีพ</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblFCareer" runat="server" CssClass="textnormal"></asp:Label>
            </td>
            <td width="30">โทร.</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblFTel" runat="server" CssClass="textnormal"></asp:Label>
            </td>
          </tr>
          <tr>
            <td>ชื่อมารดา</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblMname" 
                     runat="server"></asp:Label>
            </td>
            <td>อาชีพ</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblMCareer" runat="server" CssClass="textnormal"></asp:Label>
            </td>
            <td>โทร.</td>
            <td class="TextInputUnderline">
              <asp:Label ID="lblMTel" runat="server" CssClass="textnormal"></asp:Label>
              </td>
          </tr>
        </table></td>
      </tr>
        <tr>
        <td  valign="bottom">&nbsp; </td>
      </tr>
        <tr>
        <td  valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" class="dc_table_s3">
          <tr>
            <td width="140" align="center">ประวัติการศึกษา</td>
            <td align="center">สถานศึกษา</td>
            <td align="center">จังหวัด</td>
            <td align="center">ปีที่จบ</td>
          </tr>
          <tr>
            <td>ประถมศึกษา</td>
            <td class="textnormal">
              <asp:Label ID="lblPrimarySchool" runat="server" CssClass="textnormal"></asp:Label>            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblPrimaryProvince" runat="server" CssClass="textnormal"></asp:Label>
            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblPrimaryYear" 
                    runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td>มัธยมศึกษาตอนต้น</td>
            <td class="textnormal">
              <asp:Label ID="lblSecondarySchool" runat="server" CssClass="textnormal"></asp:Label>
            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblSecondaryProvince" 
                    runat="server"></asp:Label>
            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblSecondaryYear" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td>มัธยมศึกษาตอนปลาย</td>
            <td class="textnormal">
              <asp:Label ID="lblHighSchool" runat="server" CssClass="textnormal"></asp:Label>
            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblHighProvince" 
                    runat="server"></asp:Label>
            </td>
            <td align="center" class="textnormal">
              <asp:Label ID="lblHighYear" runat="server"></asp:Label>
            </td>
          </tr>
        </table></td>
      </tr>
        <tr>
        <td  valign="bottom">&nbsp; </td>
      </tr>
     
      
      <tr>
        <td valign="bottom"><table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>  <td width="100" class="texttopic">งานอดิเรก</td>
            <td class="TextInputUnderline"><asp:Label ID="lblHobby" runat="server" 
                    CssClass="textnormal"></asp:Label></td>
            </tr>
        </table></td>
      </tr>
       <tr>
        <td  valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="110">ความสามารถพิเศษ</td>
            <td class="TextInputUnderline" ><asp:Label ID="lblTalent" runat="server" 
                    CssClass="textnormal"></asp:Label></td>
            </tr>
        </table></td>
      </tr>
      
      <tr>
        <td valign="bottom"><table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td width="250" class="texttopic">ความคาดหวังในการประกอบอาชีพในอนาคต</td>
            <td class="TextInputUnderline">&nbsp;</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td align="left" valign="bottom" class="TextInputUnderline">&nbsp; 
          <asp:Label ID="lblExpectations" runat="server" CssClass="textnormal"></asp:Label>
        </td>
      </tr>
      <tr>
        <td  valign="bottom">&nbsp; </td>
      </tr>
      
        <tr>
        <td  valign="bottom">ข้อมูลของผู้ที่ติดต่อได้ในกรณีฉุกเฉิน </td>
      </tr>
       
      
      <tr>
        <td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          
          <tr>
            <td class="texttopic" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="55" valign="top">ชื่อ-สกุล</td>
                <td align="left" class="TextInputUnderline">
            <asp:Label ID="lblContactName" runat="server" CssClass="textnormal"></asp:Label>        </td>
                <td width="75" align="left" class="textnormal">ความสัมพันธ์</td>
                <td width="150" align="left" class="TextInputUnderline">
                    <asp:Label ID="lblContactRelation" runat="server" CssClass="textnormal"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top">ที่อยู่</td>
                <td colspan="3" align="left" class="TextInputUnderline"><div align="left">
            <asp:Label ID="lblContactAddress" runat="server" CssClass="textnormal"></asp:Label>    </div>        </td>
                </tr>
              <tr>
                <td valign="top">เบอร์โทร.</td>
                <td align="left" class="TextInputUnderline">
                  <asp:Label ID="lblContactTel" 
                    runat="server"></asp:Label>                   </td>
                <td align="left" >&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
            </table></td>
          </tr>
          
        </table></td>
      </tr>
     
      <tr>
        <td height="30" valign="bottom">&nbsp;</td>
      </tr>
      <tr>
        <td valign="bottom"><table border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td class="texttopic" >(ลงชื่อนิสิต)</td> 
            <td width="250" class="TextInputUnderline" >&nbsp;</td>
            </tr>
          
          

        </table></td>
      </tr>
      <tr>
        <td valign="bottom"></td>
      </tr>
    </table></td>
  </tr>
  
   
  <tr>
   <td align="center"> 
       <input  type="button" value="Print" id="cmdPrint"  onclick="doprint();" /> </td>
  </tr>
</table>
</table>  
    
    </td>
  </tr>
</table>
</form>
</body>
</html>