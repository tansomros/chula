<%@ Page Title="Forgot Password" Language="vb"  AutoEventWireup="false"  CodeBehind="News.aspx.vb" Inherits=".News" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย</title>
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>
  <link rel="shortcut icon" href="favicon.ico" type="image/x-icon"/>
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
 
<form id="form1" name="form1"  runat="server">

 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  
    
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
   
    <!-- Main content -->
<section class="content">  

     <div class="box box-pink">

            <div class="box-header">
              <i class="fa fa-info-circle"></i>

              <h3 class="box-title">Announcement News</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
      <table border="0" cellpadding="0" cellspacing="2" width="100%">
 <tr><td align="center" valign="top" class="News_Headline">
<asp:Label ID="lblTitle" runat="server"></asp:Label>                                  </td>
                            </tr>
                              <tr>
                                <td align="center" valign="top">
                                  <asp:Label ID="lblContent" runat="server" Width="1000px"></asp:Label>                                  </td>
                            </tr>
                    </table>         
 </div>
            <div class="box-footer clearfix">
            <table border="0" cellspacing="2" cellpadding="0">
                                    <tr>
                                      <td>&nbsp;Announcement date :</td>
                                      <td>
                                          <asp:Label ID="lblDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                  </table>
            </div>
          </div>                   
     </section>
    <!-- /.content -->

</div>
  <!-- /.content-wrapper -->
  <footer class="main-footer-login">
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

<!-- Slimscroll -->
<script src="components/jquery-slimscroll/jquery.slimscroll.min.js"></script>

</body>
</html>
