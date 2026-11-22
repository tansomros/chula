<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.vb" Inherits=".Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <section class="content-header">
      <h1>
        Dashboard
        <small>Control panel</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Dashboard</li>
      </ol>
    </section>
    
    <!-- Main content -->
    <section class="content">

 <% If Request.Cookies("ROLE_PCT").Value = True Then%>
 <!-- Small boxes (Stat box) -->
    <%-- <h3></h3> 
<br />
  <!-- for member -->
      <div class="row">
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Label2" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>Credit Balance</p>
            </div>
            <div class="icon">
              <i class="ion ion-social-usd"></i>
            </div>
          <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->     
      
      </div>--%>
        

<% End If %>

 <% If Request.Cookies("ROLE_ADM").Value = True Then%>
 <!-- Small boxes (Stat box) -->
 <h3>จำนวนนิสิตในระบบแยกตามชั้นปี</h3> 
<br />
      <!-- for member -->
      <div class="row">
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="lblMember1" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 1</p>
            </div>
            <div class="icon">
              <i class="ion ion-android-contact"></i>
            </div>
          <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="lblMember2" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 2</p>
            </div>
            <div class="icon">
              <i class="ion ion-android-contacts"></i>
            </div>
          <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-maroon">
            <div class="inner">
              <h3><asp:Label ID="lblMember3" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 3</p>
            </div>
            <div class="icon">
              <i class="ion ion-happy"></i>
            </div>
           <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
 <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3><asp:Label ID="lblMember4" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 4</p>
            </div>
            <div class="icon">
              <i class="ion ion-star"></i>
            </div>
           <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->
      
      </div>
        
         <div class="row">

 

        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-purple">
            <div class="inner">
              <h3><asp:Label ID="lblMember5" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 5</p>
            </div>
            <div class="icon">
              <i class="ion ion-trophy"></i>
            </div>
          <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>

        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-primary">
            <div class="inner">
              <h3><asp:Label ID="lblMember6" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 6</p>
            </div>
          <div class="icon">
              <i class="ion ion-ribbon-b"></i>
            </div>
          <!--  <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
     
       <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-teal">
            <div class="inner">
              <h3><asp:Label ID="lblMember7" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>ปี 7</p>
            </div>
            <div class="icon">
              <i class="ion ion-ios-contact"></i>
            </div>
            <!-- <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->

              <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-olive">
            <div class="inner">
              <h3><asp:Label ID="lblMember8" runat="server" Text="0"></asp:Label><sup style="font-size: 20px"></sup></h3>

              <p>จบการศึกษา</p>
            </div>
            <div class="icon">
              <i class="ion ion-ribbon-a"></i>
            </div>
            <!-- <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a> -->
          </div>
        </div>
        <!-- ./col -->
      </div>  

<% End If %>
     
    
      <!-- /.row -->
      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
        <section class="col-lg-7 connectedSortable">    
            <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-newspaper-o"></i>

              <h3 class="box-title">Announcement News</h3>
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
                                <asp:HyperLink ID="hlnkNews" runat="server" CssClass="txtcontent"  Target="_blank">[hlnkNews]</asp:HyperLink>
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
            <pagerstyle ForeColor="White" HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle Font-Bold="False" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
          </asp:GridView>


            </div>
            <!-- /.chat -->
               <div class="box-footer clearfix no-border">
                <asp:HyperLink ID="HyperLink2" class="buttonDisable pull-right" runat="server" NavigateUrl="NewsAll.aspx"><i class="fa fa-plus"></i> Read more</asp:HyperLink>    
            </div>
          </div> 
               <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">User Manual</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
             
<%  If Request.Cookies("ROLE_ADM").Value = True Then %>
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2">
        <tr>
          <td colspan="2" align="left"  class="MenuSt">คู่มือสำหรับ Admin</td>
        </tr>
         <tr>
          <td align="center"><img src="images/zip.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/AdminManual.zip" target="_blank" class="text12_blue">ดาวน์โหลดคู่มือทุกโมดูลรวมใน zip ไฟล์เดียว</a></td>
        </tr>
        
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Starter.pdf" target="_blank" class="text12_blue">เริ่มต้นใช้งาน</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Student.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลนิสิต</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Teacher.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลอาจารย์</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Subject.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลรายวิชา</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Location.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลแหล่งฝึก</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Phase.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลผลัดฝึก</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_SetupDataYearly.pdf" target="_blank" class="text12_blue">การกำหนดข้อมูลประจำปี</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Selection.pdf" target="_blank" class="text12_blue">การจัดการข้อมูลการเลือกแหล่งฝึก</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Random.pdf" target="_blank" class="text12_blue">การคัดเลือก</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Supervisor.pdf" target="_blank" class="text12_blue">การนิเทศ</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Assessment.pdf" target="_blank" class="text12_blue">การจัดการแบบประเมินและผลการประเมิน</a></td>
        </tr>        
        <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Report.pdf" target="_blank" class="text12_blue">การออกรายงาน</a></td>
        </tr>
        <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_Setting.pdf" target="_blank" class="text12_blue">การตั้งค่าข้อมูลพื้นฐาน</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_User.pdf" target="_blank" class="text12_blue">จัดการผู้ใช้งาน</a></td>
        </tr>
         <tr>
          <td align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Admin_News.pdf" target="_blank" class="text12_blue">การประกาศข่าว</a></td>
        </tr>
        <tr>
          <td width="3%" align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/MailMergeManual.pdf" target="_blank" class="text12_blue">คู่มือการส่งออกและพิมพ์จดหมาย</a></td>
        </tr>
        <tr>
          <td width="3%" align="center"><img src="images/zip.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Document.zip" target="_blank" class="text12_blue">ดาวน์โหลดไฟล์จดหมาย</a></td>
        </tr>
     
        <tr>
          <td width="3%" align="center"><img src="images/excel.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Students.xls" target="_blank" class="text12_blue">ดาวน์โหลดไฟล์ Template นิสิต</a></td>
        </tr>
         <tr>
          <td width="3%" align="center"><img src="images/excel.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Teacher.xls" target="_blank" class="text12_blue">ดาวน์โหลดไฟล์  Template อาจารย์</a></td>
        </tr>
     
      </table>
  <% End If %>  

      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2">
        <tr>
          <td colspan="2" align="left"  class="MenuSt">User Manual</td>
        </tr>
        <tr>
          <td width="3%" align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Manual_Student.pdf" target="_blank" class="text12_blue">Student manual</a></td>
        </tr>
           <tr>
          <td width="3%" align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Manual_Location.pdf" target="_blank" class="text12_blue">Organization manual</a></td>
        </tr>
           <tr>
          <td width="3%" align="center"><img src="images/pdf_download.png" width="20" height="20" /></td>
          <td align="left"><a href="Documents/Manual_Advisor.pdf" target="_blank" class="text12_blue">Course Cooperation manual</a></td>
        </tr>
          
        <tr>
          <td>&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>
      </table>


            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

             
 <!-- VPN  
          < % If Request.Cookies("UserProfileID").Value = 3 Then %>
          <div class="box box-success box-solid">
            <div class="box-header">
              <i class="fa fa-cloud"></i>
              <h3 class="box-title">VPN Account</h3>
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <div class="box-body" id="vpn-box">      
                    Username : <asp:Label ID="lblVPNUser" runat="server" Text=""></asp:Label>
                <br />
                    Password : <asp:Label ID="lblVPNPass" runat="server" Text=""></asp:Label>
            </div>     
          </div>            
    < % End If %>       
           .VPN -->     

        </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
        <section class="col-lg-5 connectedSortable">

             <div class="box box-success">
            <div class="box-body no-padding">
              <!-- THE CALENDAR -->
              <div id="calendar"></div>
            </div>
            <!-- /.box-body -->
          </div>

     

            <!-- quick email widget -->
          <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">User Online</h3>
             
                 <div class="box-tools pull-right">
               <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body"> 
                <asp:DataList id="dtlMember" RepeatDirection="Horizontal"                    runat="server" CellPadding="5" CellSpacing="10">
                            <ItemTemplate>
<div class="pull-left image">
            <asp:Image ID="imgUser" runat="server" class="profile-user-img img-responsive img-circle" Height="50px" Width="50px" 
                ToolTip='<%# DataBinder.Eval(Container.DataItem, "Name") %>' 
                ImageUrl='<%#  "~/" + DataBinder.Eval(Container.DataItem, "PicturePath") %>' />
 
                                 </div>
                            </ItemTemplate>
                        </asp:DataList>
                <asp:Label ID="lblOnline" runat="server" Width="99%"></asp:Label>
            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>


        </section>
        <!-- right col -->
      </div>
      <!-- /.row (main row) -->

    </section>
    <!-- /.content -->

  <!-- /.content-wrapper -->
  <div class="control-sidebar-bg"></div>

</asp:Content>
