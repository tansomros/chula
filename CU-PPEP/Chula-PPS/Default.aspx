<%@ Page Title="Home Page" Language="vb"  AutoEventWireup="false"  CodeBehind="Default.aspx.vb" Inherits="._Default" %>

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

    
<!-- Modal -->
<link href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round" rel="stylesheet">
<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <style type="text/css">
    body {
		 
	}
	.modal-confirm {		
		color: #434e65;
		width: 525px;
	}
	.modal-confirm .modal-content {
		padding: 20px;
		font-size: 16px;
		border-radius: 5px;
		border: none;
	}
	.modal-confirm .modal-header {
		background: #e85e6c;
		border-bottom: none;   
        position: relative;
		text-align: center;
		margin: -20px -20px 0;
		border-radius: 5px 5px 0 0;
		padding: 35px;
	}
	.modal-confirm h4 {
		text-align: center;
		font-size: 36px;
		margin: 10px 0;
	}
	.modal-confirm .form-control, .modal-confirm .btn {
		min-height: 40px;
		border-radius: 3px; 
	}
	.modal-confirm .close {
        position: absolute;
		top: 15px;
		right: 15px;
		color: #fff;
		text-shadow: none;
		opacity: 0.5;
	}
	.modal-confirm .close:hover {
		opacity: 0.8;
	}
	.modal-confirm .icon-box {
		color: #fff;		
		width: 95px;
		height: 95px;
		display: inline-block;
		border-radius: 50%;
		z-index: 9;
		border: 5px solid #fff;
		padding: 15px;
		text-align: center;
	}
	.modal-confirm .icon-box i {
		font-size: 58px;
		margin: -2px 0 0 -2px;
	}
	.modal-confirm.modal-dialog {
		margin-top: 80px;
	}
    .modal-confirm .btn {
        color: #fff;
        border-radius: 4px;
		background: #eeb711;
		text-decoration: none;
		transition: all 0.4s;
        line-height: normal;
		border-radius: 30px;
		margin-top: 10px;
		padding: 6px 20px;
		min-width: 150px;
        border: none;
    }
	.modal-confirm .btn:hover, .modal-confirm .btn:focus {
		background: #eda645;
		outline: none;
	}
	.trigger-btn {
		display: inline-block;
		margin: 100px auto;
	}
</style>


       <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
  <script type="text/javascript">
        function openModals(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }      

    </script>
<!-- End modal -->
</head>

<body class="hold-transition skin-blue sidebar-mini">
<form id="form2" name="form1"  runat="server">

    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
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

 <!-- Small boxes (Stat box) -->


      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
        <section class="col-lg-3 connectedSortable">    

<div class="login-box">
  <div class="login-logo">
   <b>PPEP</b> Login
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">  
       <p class="login-box-msg"> Login เข้าสู่ระบบ</p>


      <div class="form-group has-feedback">
         <span class="glyphicon glyphicon-user form-control-feedback"></span> 
          <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"  ToolTip="Username" type="login" Width="100%" placeholder="Username"></asp:TextBox>                
      </div>
      <div class="form-group has-feedback">  <span class="glyphicon glyphicon-lock form-control-feedback"></span>
       <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"   TextMode="password"  placeholder="Password" ToolTip="Password" type="login" Width="100%"></asp:TextBox>
      
      </div>
      <div class="row" align="center">
        <asp:Button ID="cmdLogin" runat="server" text="Login" CssClass="buttonLogin" Width="100px" />
      </div>   
         <a href="ForgotPassword.aspx" >ลืมรหัสผ่าน</a>
  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->
       
 
 <div class="box box-success display-control">
            <div class="box-header">
              <i class="ion ion-earth"></i>

              <h3 class="box-title">Web Browser Recommended</h3>
             <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
                 
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                 
                                                      <table border="0" cellpadding="0" cellspacing="2" align="center">
                                                          <tr>
                                                              <td align="center">
                                                                  <a href="http://www.google.com/intl/th/chrome/" target="_blank">
                                                              <img src="images/Google_Chrome_icon.png" width="46" height="46" /></a></td>
                                                             
                                                        </tr>
                                                          <tr>
                                                              <td>
                                                                  <a href="http://www.google.com/intl/th/chrome/" target="_blank">Download Chrome</a></td>
                                                            
                                                          </tr>
                        </table>
                                 
            </div>
            <!-- /.box-body -->
            
          </div>
          <!-- /.box -->
 
  <!-- /.box (chat box) -->
          <div class="box box-success display-control">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">User Online : <asp:Label ID="lblUserOnlineCount" runat="server" Text=""></asp:Label></h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
            <asp:Label ID="lblOnline" runat="server" Width="99%"></asp:Label>
            </div>
           
          </div>  


            <!--
 <div class="box box-danger">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title"> Admin'Message : ประกาศจากผู้ดูแลระบบ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body text-center">
            <h3 class="text-red">ขณะนี้ ผู้ดูแลระบบกำลังปรับปรุงระบบอยู่<br />
         เพื่อป้องกันข้อมูลสูญหาย ให้ท่านหลีกเลี่ยงการบันทึกแบบฟอร์มทุกโครงการ <br />แต่ท่านยังสามารถดูข้อมูลหรือรายงานในระบบได้ปกติ</h3><br />
      ท่านสามารถเข้ามาบันทึกข้อมูลได้อีกครั้ง ภายใน 10 นาที หรือ หลังจากข้อความประกาศนี้หายไป <br />
         ขออภัยในความไม่สะดวก

            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
          
          <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-heart"></i>

              <h3 class="box-title">สนับสนุนโดย</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                   
            </div>
            <div class="box-footer clearfix no-border">
           
            </div>
          </div>
      
 -->    

           
        </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
        <section class="col-lg-9 connectedSortable">
 <div class="slide-control">
            <div class="box box-solid">       
            <div class="box-body">
              <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                  <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                  <li data-target="#carousel-example-generic" data-slide-to="1" class=""></li>
                  <li data-target="#carousel-example-generic" data-slide-to="2" class=""></li>
                </ol>
                <div class="carousel-inner">
                  <div class="item active">
                    <img src="slide/slide-01.png" alt="First slide">

                    <div class="carousel-caption">
                    
                    </div>
                  </div>
                  <div class="item">
                    <img src="slide/slide-02.png" alt="Second slide">

                    <div class="carousel-caption">
                     
                    </div>
                  </div>
                  <div class="item">
                    <img src="slide/slide-03.png" alt="Third slide">

                    <div class="carousel-caption">
                  
                    </div>
                  </div>
                </div>
                <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                  <span class="fa fa-angle-left"></span>
                </a>
                <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                  <span class="fa fa-angle-right"></span>
                </a>
              </div>
            </div>
            <!-- /.box-body -->
          </div>


</div>
          <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-newspaper-o"></i>

              <h3 class="box-title">ข่าวประกาศ</h3>
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>

            <div class="box-body chat" id="chat-box">
                 <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="NewsID" Font-Bold="False" ShowHeader="False">
            <RowStyle HorizontalAlign="Center" />
            <columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        
                        <div class="item">
                            <asp:Image ID="imgNews" runat="server"  />
                            <p class="message">
                                <a class="name" href="#"><small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <asp:Label ID="Label1" runat="server" Text='<%# DisplayDateTH(DataBinder.Eval(Container.DataItem, "NewsDate")) %>'></asp:Label></small>
                                <asp:HyperLink ID="hlnkNews" runat="server" Target="_blank">[hlnkNews]</asp:HyperLink>
                                </a>
                                <br />
                                <asp:Label ID="lblShortNews" runat="server" Text=""></asp:Label>
                            </p>
                     
                        </div>
                        <!-- /.item -->                    
                        
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" ForeColor="White" HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle Font-Bold="False" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
          </asp:GridView>


            </div>
            <!-- /.chat -->
               <div class="box-footer clearfix no-border">
                <asp:HyperLink ID="HyperLink2" class="btn btn-pink pull-right" runat="server" NavigateUrl="NewsAll.aspx"><i class="fa fa-plus"></i> อ่านข่าวทั้งหมด</asp:HyperLink>    
            </div>
          </div>
        
        
             
  
       
        </section>
        <!-- right col -->
      </div>
      <!-- /.row (main row) -->
    <div class="row">
 
    </div>
      
    </section>
    <!-- /.content -->
 
 
 <!-- Modal HTML -->
        <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog modal-confirm">
		<div class="modal-content">
			<div class="modal-header">
				<div class="icon-box">
					<i class="material-icons">&#xE5CD;</i>
				</div>
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
			</div>
			<div class="modal-body text-center">
				<h4><span id="spnTitle"></span></h4>	
				<p><span id="spnMsg"></span>.</p>
				<button class="btn btn-success" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
            </div>
<!--- End Modal --->

</div>
  <!-- /.content-wrapper -->
  <footer class="main-footer-login">
       <div class="pull-right hidden-xs">
       <strong>Copyright © 2018</strong> All rights reserved.  <strong>Designed and developed by Big Lion Team.</strong>
       <br/><b>Version</b> <asp:Label ID="lblVersion" runat="server" Text="2.0.0"></asp:Label>&nbsp;&nbsp;<b>Release</b> 2018.10.01
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

