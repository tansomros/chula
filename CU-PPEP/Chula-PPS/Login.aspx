<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits=".Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
<link rel="stylesheet" type="text/css" href="css/loginstyles.css">
<link rel="stylesheet" type="text/css" href="css/cu_button.css">
<link rel="stylesheet" type="text/css" href="css/uidialog.css">
<title>ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</title>
 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
<script src="js/WaterMark.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=txtUserName], [id*=txtPassword]").WaterMark();
    });
</script>

<script type="text/javascript">
<!--
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>
    <style type="text/css">
        .style1
        {
            height: 77px;
        }
    </style>
</head>

<body  id="login_Sheet" onload="MM_preloadImages('images/login_forgot_pass_hover.png')">
  
<form id="form1" name="form1"  runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<div id="login_Sheet">

<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="600">
        
    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="center"><img src="images/login_logo.png" width="411" height="294" /></td>
      </tr>
      <tr>
        <td align="center"><table width="95%" border="0" cellspacing="5" cellpadding="0">
          <tr>
            <td align="center">
                                     
                <asp:TextBox ID="txtUserName" runat="server"  type="login" ToolTip="Username"  Width="300px" CssClass="txtlogin"></asp:TextBox>            </td>
          </tr>
          <tr>
            <td align="center">
              <asp:TextBox ID="txtPassword" runat="server"  type="login"  CssClass="txtlogin" ToolTip="Password" 
                    Width="300px" TextMode="password"></asp:TextBox>            </td>
          </tr>
        </table></td>
      </tr>      
      <tr>
        <td align="center"> 
            <asp:ImageButton ID="cmdLogin" runat="server" ImageUrl="images/btnlogin.png" />          </td>
      </tr>
      <tr>
        <td align="center"><a href="#" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('mnu1','','images/login_forgot_pass_hover.png',1)"><img src="images/login_forgot_pass.png" name="mnu1" width="81" height="28" border="0" id="mnu1" /></a></td>
      </tr>
       <tr>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><img src="images/browser.png" width="235" height="33" /></td>
      </tr>
      <tr>
        <td align="center" class="style1"><table border="0" cellspacing="2" cellpadding="0">
          <tr>
            <td align="center"><a href="http://www.google.com/intl/th/chrome/" target="_blank"><img src="images/Google_Chrome_icon.png" width="46" height="46" /></a></td>
            <td width="20">&nbsp;</td>
            <td align="center"><a href="http://www.mozilla.org/en-US/firefox/new/" target="_blank"><img src="images/Mozilla_Firefox_icon.png" width="46" height="46" /></a></td>
          </tr>
          <tr>
            <td class="linkdowload"><a href="http://www.google.com/intl/th/chrome/" target="_blank">Download Chrome</a></td>
            <td>&nbsp;</td>
            <td class="linkdowload"><a href="http://www.mozilla.org/en-US/firefox/new/" target="_blank">Download Firfox</a></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td align="center">
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                 TargetControlID="Panel1"
    PopupControlID="Panel1" 
    DropShadow="false"
    PopupDragHandleControlID="headbox" BackgroundCssClass="dialog_bg" Drag="True" >
            </cc1:ModalPopupExtender>
       <asp:Panel ID="Panel1" runat="server" Style="display: none;"   DefaultButton="btnOk">
         
  <table width="300" border="0" align="center" cellpadding="2" cellspacing="0" class="dialog_box">
    <tr>
      <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td class="dialog_box title" id="headbox">ผลการทำงาน</td>
        </tr>
        <tr>
          <td align="center" class="dialog_content">
              <asp:Label ID="lblAlert" runat="server">Username  หรือ Password ไม่ถูกต้อง</asp:Label>
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

        
        </td>
      </tr>
    </table>
    

       
    

    </td>
  </tr>
</table>





<div id="footerLogin">
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center">คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย 254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330 โทร. 02-215-0871-7 โทรสาร. 02-255-8227</td>
  </tr>
</table>
</div>

          
</div>  
  
 
    </form>
</body>
</html>
