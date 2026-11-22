<%@ Page Title="Privacy Policy" Language="VB" AutoEventWireup="true" CodeBehind="PrivacyPolicy.aspx.vb" Inherits=".PrivacyPolicy" %>


   
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
    <link rel="stylesheet" type="text/css" href="css/custyles.css">
<link rel="stylesheet" type="text/css" href="css/uidialog.css">

  <link rel="stylesheet" href="components/bootstrap/dist/css/bootstrap.min.css"> 
  <link rel="stylesheet" href="components/font-awesome/css/font-awesome.min.css">
  <link rel="stylesheet" href="components/Ionicons/css/ionicons.min.css">
  <link rel="stylesheet" href="css/Login.min.css">
  <link rel="stylesheet" href="css/skins/_all-skins.min.css">  
          
         <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

       <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
        <script type="text/javascript">
            function openModalWarningAlert(sender, title, message) {
                $("#spnTitleW").text(title);
                $("#spnMsgW").text(message);
                $('#modal-warningalert').modal('show');
                $('#btnConfirm').attr('onclick', "$('#modal-warningalert').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
                return false;
            }
            function openModalWarningInfo(sender, title, message) {
                $("#spnTitleInfo").text(title);
                $("#spnMsgInfo").text(message);
                $('#modal-warninginfo').modal('show');
                $('#btnConfirm').attr('onclick', "$('#modal-warninginfo').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
                return false;
            }
            function openModalSuccess(sender, title, message) {
                $("#spnTitleS").text(title);
                $("#spnMsgS").text(message);
                $('#modal-success').modal('show');
                $('#btnConfirm').attr('onclick', "$('#modal-success').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
                return false;
            }
        </script>

        <!-- End modal -->
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
                    <section class="col-lg-12 connectedSortable">
                  
                        <div class="justify-content-center">
                            <div class="col-lg-12">
                                <div class="box box-solid mb-3">
<div class="box-body">
<h3 class="box-title text-center">แบบฟอร์มการขอความยินยอมให้เก็บรวบรวมและประมวลผลข้อมูลส่วนบุคคล </h3>
<div class="scroll-area-lg">

					<div class="txt-prg">
						1. ภายใต้พระราชบัญญัติคุ้มครองข้อมูลส่วนบุคคล พ.ศ. 2562 คณะเภสัชศาสตร์ จุฬาลงกรณืมหาวิทยาลัย ถือเป็นผู้ควบคุมข้อมูลส่วนบุคคลที่มีหน้าที่ตามกฎหมายในการคุ้มครองข้อมูลส่วนบุคคลที่อยู่ในการครอบครองหรือควบคุมของบริษัท ท่านสามารถดูนโยบายการคุ้มครองข้อมูลส่วนบุคคลของจุฬาลงกรณืมหาวิทยาลัย ได้ที่<a href="https://www.chula.ac.th/about/overview/personal-data-protection-policy/" target="_blank">https://www.chula.ac.th/about/overview/personal-data-protection-policy</a>  <br />

                        2. ข้อมูลส่วนบุคคล ได้แก่ ข้อมูลเกี่ยวกับบุคคลซึ่งทำให้สามารถระบุตัวบุคคลนั้นได้ ไม่ว่าทางตรงหรือ ทางอ้อม แต่ไม่รวมถึงข้อมูลของผู้ถึงแก่กรรมโดยเฉพาะ<br />

                        3. การเก็บรวบรวม ใช้ หรือเปิดเผยข้อมูลส่วนบุคคลของท่านจะเป็นไปเพื่อวัตถุประสงค์ในการใช้ใน ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย เท่านั้น <br />
4. ข้อมูลส่วนบุคคลดังกล่าวจะถูกประมวลผลโดยบุคคลที่มีอำนาจที่เกี่ยวข้องกับวัตถุประสงค์ที่กล่าวไว้เท่านั้น<br />
5. การไม่ยินยอมให้ข้อมูลส่วนบุคคลดังกล่าวจะทำให้คณะเภสัชศาสตร์ ไม่สามารถดำเนินการตามวัตถุประสงค์ดังกล่าวกับท่านและทำให้ท่านไม่สามารถได้รับประโยชน์จากการดำเนินการดังกล่าว<br />
6. ท่านมีสิทธิที่จะถอนการยินยอมในการให้เก็บรวบรวม ใช้ หรือเปิดเผยข้อมูลส่วนบุคคล โดยท่านสามารถติดต่อเจ้าหน้าที่คุ้มครองข้อมูลส่วนบุคคลที่ <a href="#" target="_blank"> info@pharm.chula.ac.th</a><br />
7. คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย รับรองว่ามีการดำเนินการรักษาความปลอดภัยที่มีมาตรฐาน และจัดให้มีมาตรการด้านเทคนิคและการจัดการเพื่อป้องกันการเข้าถึงข้อมูลส่วนบุคคลของท่านโดยมิชอบ<br />

                        <br />
                                                 
					 
					</div>  <!-- End txt-prg -->
</div>

    <div class="text-center">
    <asp:CheckBox ID="chkAllow" runat="server" Text="&nbsp;&nbsp;โดยข้าพเจ้าได้อ่านประกาศฉบับนี้และ <b>ให้ความยินยอม</b> ในการให้ข้อมูลส่วนบุคคลกับ คณะเภสัชศาสตร์ เพื่อการดำเนินการตามวัตถุประสงค์ข้างต้น" />
        <br />
    </div>
    <br />
     <div class="row justify-content-center text-center">
        <asp:Button ID="cmdSubmit" runat="server" Text="ยินยอม" Width="120px" CssClass="btn btn-primary" />
         </div>
      <br />  <br /> 
</div>
                                    
</div>
                                
                            </div>
                        </div>
                    </section>

                </div>
            
                <!-- Modal HTML --> 
               <div id="modal-warninginfo" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog modal-warninginfo">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="icon-box">
                                 <i class="material-icons warning">&#xe002;</i>
                                </div>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            </div>
                            <div class="modal-body text-center">
                                 <h4><span id="spnTitleInfo"></span></h4>
                                <p><span id="spnMsgInfo"></span></p>
                                <button class="btn" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>  
                <!--- End Modal --->              
            
     
</section>
  
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
  