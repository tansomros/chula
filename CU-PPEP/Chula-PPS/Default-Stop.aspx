<%@ Page Title="Home Page" Language="vb"  AutoEventWireup="false"  CodeBehind="Default.aspx.vb" Inherits="._Default" %>
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
  <link rel="stylesheet" href="dist/css/Login.min.css">
  <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">  

     <link  href="css/royalslider.css" rel="stylesheet" type="text/css" media="all"> 
 <link  href="css/rs-minimal-white.css" rel="stylesheet" type="text/css" media="all"> 

     
<!-- JS -->
<script type="text/javascript" src="css/jquery-1.6.3.min.js"></script>

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

 <div align="center">
                <table style="width: 100%; background-color:#ffa2ab;" align="center">
                    <tr>
                        <td align="center"><img alt="" src="imgNews/bannerStop.jpg" /></td> 
                    </tr>
                  
                    
                </table>
      </div>
            <br />
       
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
                <asp:HyperLink ID="HyperLink2" class="buttonSave pull-right" runat="server" NavigateUrl="NewsAll.aspx"><i class="fa fa-plus"></i> อ่านข่าวทั้งหมด</asp:HyperLink>    
            </div>
          </div>
        
        
      </div>
      <!-- /.row (main row) -->
    <div class="row">
 
    </div>
      
    </section>
    <!-- /.content -->
 

  <table align="center" border="0" cellpadding="0" cellspacing="2" >   
                  <tr>
                      <td>
                      <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
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
                  <asp:Label ID="lblAlert" runat="server" CssClass="text-maroon">Username  หรือ Password ไม่ถูกต้อง</asp:Label>                                                 
              </div>
            <!-- /.chat -->
               <div class="box-footer clearfix no-border" align="center"> 
                   <asp:Button ID="btnOK" runat="server" CssClass="buttonSave" Text="Close" />  
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
<script src="dist/js/adminlte.min.js"></script>

<!-- Slimscroll -->
<script src="components/jquery-slimscroll/jquery.slimscroll.min.js"></script>

</body>
</html>

