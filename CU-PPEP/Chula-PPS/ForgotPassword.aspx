<%@ Page Title="Forgot Password" Language="vb"  AutoEventWireup="false"  CodeBehind="ForgotPassword.aspx.vb" Inherits=".ForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</title>
  
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">  

<link rel="stylesheet" type="text/css" href="css/loginstyles.css">
<link rel="stylesheet" type="text/css" href="css/uidialog.css">

  <link rel="stylesheet" href="components/bootstrap/dist/css/bootstrap.min.css"> 
  <link rel="stylesheet" href="components/font-awesome/css/font-awesome.min.css">
  <link rel="stylesheet" href="components/Ionicons/css/ionicons.min.css">
  <link rel="stylesheet" href="css/Login.min.css">
  <link rel="stylesheet" href="css/skins/_all-skins.min.css">  

     <link  href="css/royalslider.css" rel="stylesheet" type="text/css" media="all"> 
 <link  href="css/rs-minimal-white.css" rel="stylesheet" type="text/css" media="all"> 

     
<!-- JS -->
<script type="text/javascript" src="css/jquery-1.6.3.min.js"></script>

</head>

<body class="hold-transition skin-blue sidebar-mini">
<form id="form2" name="form1"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="wrapper">

  <header class="main-header">    
    <a href="#" class="logo">
    <span class="logo-mini"><b>PPEP</b></span>
    <span class="logo-lg"><b>ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ</b></span>
    </a>
    <nav class="navbar navbar-static-top">     
        <div class="slk-header" >
            <span  class="header-mini">ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ </span>
           <span  class="header-full">คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</span>
        </div>
    </nav>
  </header>



  <div class="contentCPA">
   
<section class="content">
       <div class="row">                      
       
          <div class="box box-solid">
            <div class="box-header">
              <!-- tools box -->
              <div class="pull-right box-tools">                
               
              </div>
              <!-- /. tools -->

              <i class="fa fa-lock"></i>

              <h3 class="box-title">
                ลืมรหัสผ่าน
              </h3>
            </div>
            <div class="box-body">               
          <table align="center" border="0" cellpadding="0" cellspacing="2" width="100%" >
                
                  <tr>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" width="100%">
                             
                              <tr>
                                <td valign="top">
                                
                                    <asp:Panel ID="pnRequest" runat="server">
                                     
                                <table border="0" align="center" cellpadding="0" cellspacing="2">
                                  <tr>
                                    <td>โปรดกรอก username หรือ email ที่ท่านลงทะเบียนไว้กับระบบ และคลิกปุ่ม ตกลง จากนั้นระบบจะส่งรหัสผ่านของท่านไปยังอีเมลที่ท่านได้ทำการลงทะเบียนไว้</td>
                                  </tr>
                                  <tr>
                                    <td align="center"><table border="0" cellspacing="2" cellpadding="0">
                                      <tr>
                                        <td>Username หรือ E-mail : </td>
                                        <td>
                                            <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>                                          </td>
                                        <td>
                                            <asp:ImageButton ID="cmdSubmit" runat="server" ImageUrl="images/btnOK.png" />                                          </td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                  </asp:Panel>
                                 <asp:Panel ID="pnResult" runat="server">
                                   
                                  <table border="0" align="center" cellpadding="0" cellspacing="2">
                                    <tr>
                                      <td align="center">ระบบส่งรหัสผ่านให้ท่านทางอีเมลเรียบร้อยแล้ว </td>
                                    </tr>
                                    <tr>
                                      <td>กรุณาตรวจสอบอีเมลแล้วเข้าระบบใหม่อีกครั้งด้วยรหัสผ่านที่ท่านได้รับ</td>
                                    </tr>
                                  </table>
                                </asp:Panel>                               </td>
                              </tr>
                          </table>                           </td>
                  </tr>
                    <tr>
                      <td align="center"><span class="menu_Header">คำแนะนำ :</span> หากไม่พบอีเมลจากระบบใน กล่องจดหมายเข้า กรุณาตรวจสอบที่ Junk Mail <br />
                      หากตรวจสอบครบถ้วนแล้วไม่พบกรุณาติดต่อผู้ดูแลระบบ โทร.02-XXXXXX (คุณ....)</td>
                </tr>
                  <tr>
                      <td align="center">
                          <asp:HyperLink ID="HyperLink1" runat="server" CssClass="buttonLogin" 
                              NavigateUrl="Default.aspx" Height="30px">กลับหน้าแรก</asp:HyperLink>                          </td>
                  </tr>
                  <tr>
                      <td>
                          <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                              BackgroundCssClass="dialog_bg" Drag="True" DropShadow="false" 
                              PopupControlID="Panel1" PopupDragHandleControlID="headbox" 
                              TargetControlID="Panel1">                          </cc1:ModalPopupExtender>
                          <asp:Panel ID="Panel1" runat="server" DefaultButton="btnOk" 
                              Style="display: none;">
                              <table align="center" border="0" cellpadding="2" cellspacing="0" 
                                  class="dialog_box" width="300">
                                  <tr>
                                      <td>
                                          <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                  <td ID="headbox" class="dialog_box title">
                                                      ผลการทำงาน</td>
                                              </tr>
                                              <tr>
                                                  <td align="center" class="dialog_content">
                                                      <asp:Label ID="lblAlert" runat="server">ไม่พบ Username  หรือ E-mail ในระบบ</asp:Label>                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="center">
                                                      <asp:Button ID="btnOK" runat="server" CssClass="buttonModal" Text="Close" />                                                  </td>
                                              </tr>
                                          </table>                                      </td>
                                  </tr>
                              </table>
                          </asp:Panel>
                        <script type="text/javascript">

    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
                </script>                      </td>
                  </tr>
              </table>        

                </div>
            <!-- /.box-body-->    
           
          </div>
     
      </div>        
</section>
 

  <table align="center" border="0" cellpadding="0" cellspacing="2" >   
                  <tr>
                      <td>
                      <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                              BackgroundCssClass="dialog_bg" Drag="True" DropShadow="false" 
                              PopupControlID="pnAlert" PopupDragHandleControlID="headbox" 
                              TargetControlID="pnAlert">                          </cc1:ModalPopupExtender>
                          <asp:Panel ID="pnAlert" runat="server" DefaultButton="btnOk" 
                              Style="display: none;">

                                <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-expeditedssl"></i>

              <h3 class="box-title">ผลการทำงาน</h3>
                 <div class="box-tools pull-right">               
              </div>
            </div>
            <div class="box-body chat" id="alert-box">
                  <asp:Label ID="Label2" runat="server" CssClass="text-maroon">Username  หรือ Password ไม่ถูกต้อง</asp:Label>                                                 
              </div>
            <!-- /.chat -->
               <div class="box-footer clearfix no-border" align="center"> 
                   <asp:Button ID="Button1" runat="server" CssClass="btn btn-pink" Text="Close" />  
            </div>
          </div>            
                  </asp:Panel>

<script type="text/javascript">

    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
                </script></td>
                </tr>               
               
              </table>    


</div>
  <!-- /.content-wrapper -->
  <footer class="main-footer-bottom">
       <div class="pull-right hidden-xs">
       <strong>Copyright &copy; 2018-2020 <a href="http://www.tansomros.com">tansomros</a>.</strong> All rights
    reserved.
       <br />     <b>Version</b> <asp:Label ID="lblVersion" runat="server" Text="2.0.0"></asp:Label>&nbsp;&nbsp;<b>Release</b> 2018.10.01
    </div>
       

      <b>หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</b>
        <br />254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330
          โทรศัพท์  02-218-8450-1 โทรสาร 02-218-8450
    
  </footer>
  
</div>
<!-- ./wrapper --> 
</form>
    
<script src="components/jquery/dist/jquery.min.js"></script>
<script src="components/jquery-ui/jquery-ui.min.js"></script>
<script src="components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="js/adminlte.min.js"></script>
     
</body>
</html>
    
  
 