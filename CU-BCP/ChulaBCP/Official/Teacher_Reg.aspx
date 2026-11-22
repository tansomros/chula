<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Teacher_Reg.aspx.vb" Inherits=".Teacher_Reg" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/pagestyles.css">
<link rel="stylesheet" type="text/css" href="css/custyles.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="content-header">
      <h1>แก้ไขข้อมูลอาจารย์/เจ้าหน้าที่</h1>   
    </section>

<section class="content">  

         <div class="box box-pink">

            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">กรุณากรอกข้อมูลให้ครบทุกช่อง</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                



 <table width="100%" border="0" cellspacing="0" cellpadding="0">
   
  <tr>
    <td valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="0">
      <tr>
        <td width="130" valign="top" class="texttopic">ID.</td>
        <td valign="top">
            <asp:Label ID="lblStdCode" runat="server"></asp:Label>          </td>
        <td width="130" valign="top" class="texttopic">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top" class="texttopic">คำนำหน้า</td>
        <td valign="top">
            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="Objcontrol">
            </asp:DropDownList>
          </td>
        <td valign="top" class="texttopic">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top" class="texttopic">ชื่อ</td>
        <td valign="top">
            <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>          </td>
        <td valign="top" class="texttopic">นามสกุล</td>
        <td valign="top">
            <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>          </td>
      </tr>
      <tr>
        <td valign="top" class="texttopic">ชื่อเล่น</td>
        <td valign="top"><asp:TextBox ID="txtNickName" runat="server"></asp:TextBox></td>
        <td valign="top" class="texttopic">เพศ</td>
        <td valign="top">
            <asp:RadioButtonList ID="optGender" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="M">ชาย</asp:ListItem>
                <asp:ListItem Value="F">หญิง</asp:ListItem>
            </asp:RadioButtonList>          </td>
      </tr>
     
      <tr>
        <td valign="top" class="texttopic">สังกัดภาควิชา</td>
        <td valign="top">
            <asp:DropDownList CssClass="Objcontrol" ID="ddlDepartment" runat="server">            </asp:DropDownList>          
          </td>
        <td valign="top" class="texttopic">ตำแหน่ง</td>
        <td valign="top">
            <asp:DropDownList CssClass="Objcontrol" ID="ddlPosition" runat="server">            </asp:DropDownList>          
          </td>
      </tr>
      

    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="2" cellpadding="0">
       <tr>
        <td width="130" valign="top" class="texttopic">อีเมล</td>
        <td valign="top">
            <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>          </td>
        <td colspan="2" valign="top" class="texttopic">&nbsp;               </td>
        </tr>
      <tr>
        <td valign="top" class="texttopic">โทรศัพท์</td>
        <td valign="top">
            <asp:TextBox ID="txtTelephone" runat="server" Width="200px"></asp:TextBox>          </td>
        <td width="130" valign="top" class="texttopic">มือถือ</td>
        <td valign="top">
            <asp:TextBox ID="txtMobile" runat="server" Width="200px"></asp:TextBox>          </td>
      </tr>
      <tr>
        <td>ที่อยู่ :</td>
        <td colspan="3">
            <asp:TextBox ID="txtAddress" runat="server" Width="370px"></asp:TextBox>          </td>
        </tr>
      <tr>
        <td>ตำบล</td>
        <td>
            <asp:TextBox ID="txtDistrict" runat="server" Width="200px"></asp:TextBox>          </td>
        <td>อำเภอ</td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>          </td>
      </tr>
      <tr>
        <td>จังหวัด</td>
        <td>
            <asp:DropDownList CssClass="Objcontrol" ID="ddlProvince" runat="server">            </asp:DropDownList>          </td>
        <td>รหัสไปรษณีย์</td>
        <td>
            <asp:TextBox ID="txtZipCode" runat="server" Width="70px" MaxLength="5"></asp:TextBox>          </td>
      </tr>
    </table></td>
  </tr>
 
 
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="2" cellpadding="0">
      <tr>
        <td width="100">รูปภาพ</td>
        <td>
            <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />
          </td>
        <td rowspan="3" valign="top">คำแนะนำ<br />
            1. ชนิดไฟล์รูปภาพเป็น JPG เท่านั้น และ ขนาดไม่เกิน 500 KB <br />
              
          ควรมีความกว้างประมาณ 150 pixel และความสูงประมาณ 180 pixel<br />
          2. ชื่อไฟล์ต้องเป็นภาษาอังกฤษ<br />
          3. ควรเป็นภาพทางการ และสุภาพ  <br />
             
          (งดเว้นภาพโพสต์ท่าทาง เช่น ใส่หมวก, แว่นตาแฟชั่น, ชูสองนิ้ว ฯลฯ)</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" 
                Width="150px" />
          </td>
        </tr>
      
    </table></td>
  </tr>
   
  <tr>
    <td align="center">
       <asp:Button ID="cmdSave" runat="server" CssClass="buttonSave" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="buttonDisable" Width="100" Text="ยกเลิก"></asp:Button>
      </td>
  </tr>
</table>

</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

     
                       
    </section> 
</asp:Content>
 

