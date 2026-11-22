<%@ Page Title="Home Page" Language="VB" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="Login" %>
<!DOCTYPE html>
<html>
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <meta name="description" content="">
        <meta name="author" content="">
        <title> Login</title>
        <link href="https://fonts.googleapis.com/css?family=Sarabun:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round" rel="stylesheet">
        <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
        <!-- Bootstrap 3.3.7 
  < link rel="stylesheet" href="components/bootstrap/assets/css/bootstrap.min.css">-->
        <!-- Font Awesome -->
        <link rel="stylesheet" href="components/font-awesome/css/font-awesome.min.css">
  
        <!-- Theme style -->
        <link rel="stylesheet" href="assets/css/AdminLTE.min.css">

        <link href="css/sb-admin.min.css" rel="stylesheet">
        <link rel="stylesheet" href="css/rajchasistyles.css">

       
        <!-- Google Font -->
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">

        <style type="text/css">
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
            
            .modal-confirm .form-control,
            .modal-confirm .btn {
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
            
            .modal-confirm .btn:hover,
            .modal-confirm .btn:focus {
                background: #eda645;
                outline: none;
            }
            
            .trigger-btn {
                display: inline-block;
                margin: 100px auto;
            }
        </style>
        <link rel="stylesheet" href="components/bootstrap/assets/css/bootstrap.min-login.css">
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

    <body>
        <form id="form1" class="user" runat="server">
            <div class="container-login">                 
                <div class="row"> 
                    <section class="col-lg-12 connectedSortable">
                        <!-- Outer Row -->
                        <div class="justify-content-center">
                            <div class="col-lg-12">
                                <div class="card o-hidden border-0 shadow-lg my-5">
                                    <div class="card-body p-0">
                                        <!-- Nested Row within Card Body -->
                                        <div class="row">
                                            <div class="col-lg-6 d-none d-lg-block bg-login-image"></div>
                                            <div class="col-lg-6">
                                                <div class="p-5">
                                                    <div class="text-center">
                                                         <div class="login-logo text-center">
                                                             <a href="#" target="_blank"> <img src="../images/logo.png" width="100%" /></a>  
                                                         </div>
                                                        <div class="login-logo">
                                                            <b>User Login</b><br />
                                                        </div>
                                                    </div>
                                                    <div class="form-group has-feedback">
                                                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                                                        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" ToolTip="Username" type="login" cssclass="form-control form-control-user"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group has-feedback">
                                                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                                                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" cssclass="form-control form-control-user" TextMode="Password" ToolTip="Password" type="login">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center">
                                                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-user btn-block" Text="Login" />

                                                        </div>
                                                    </div>
                                                    <hr>
                                                    <div class="text-center">
                                                        <a class="small" href="forgot-password.html">Forgot Password?</a>
                                                    </div>

                                                    <br /> <br />
                                                    <br /> <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>

                </div>
                <!--
                <div class="row">
                    <section class="col-lg-12 connectedSortable">
                        <div class="row text-center">
                            <div class="col-md-3">
                                <div>
                                    <a href="#"><img src="images/phone.png" alt="" width="64"></a>
                                </div>
                                <div class="header-text"><a href="#">038-977777<br /> 061-3454646</a></div>
                            </div>
                            <div class="col-md-3">
                                <div>
                                    <div>
                                        <a href="mailto:Narutchai.c@npc-se.co.th"><img src="images/message.png" alt="" width="64"></a>
                                    </div>
                                    <div class="header-text"><a href="mailto:Narutchai.c@npc-se.co.th">Narutchai.c@npc-se.co.th</a></div>
                                </div>
                                </a>
                            </div>
                            <div class="col-md-3">

                                <div>
                                    <div>
                                        <a href="#"><img src="images/user_manual.png" alt="" width="64"></a>
                                    </div>
                                    <div class="header-text"> <a href="#">คู่มือการใช้งาน</a></div>
                                </div>

                            </div>
                            <div class="col-md-3">

                                <div>
                                    <div>
                                        <a href="#"><img src="images/play_button.png" alt="" width="64"></a>
                                    </div>
                                    <div class="header-text"> <a href="#">VDO วิธีการใช้งาน</a></div>
                                </div>

                            </div>

                        </div>
                    </section>
                </div>
               --> <!-- Modal HTML -->
                <div id="modalPopUp" class="modal fade" role="dialog" data-backdrop="static">
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
                <div class="footer text-center">
                    <footer class="main-footer-login">
                        <div class="text-center hidden-xs">
                            <strong>Copyright &copy; 2021-2022 <a href="#">chulabcp</a>.</strong> All rights reserved.
                            <br /> <b>Version</b>
                            <asp:Label ID="Label1" runat="server" Text="1.0.0"></asp:Label>&nbsp;&nbsp;<b>Release</b> 2021.08.28
                        </div>


                    </footer>
                </div>
            </div>

        </form>

        <!-- jQuery 3 -->
        <script src="components/jquery/assets/jquery.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="components/bootstrap/assets/js/bootstrap.min.js"></script>
        
    </body>

    </html>