<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PersonDetail.aspx.vb" Inherits=".PersonDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="containerPage">   
   <section class="content">     
       <section class="content-header">
       <h1 class="news-heading text-green">ข้อมูลอาจารย์/บุคลากร
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">อาจารย์/บุคลากร</li>
      </ol>
    </section> 
        
  <div class="row">
        <div class="col-md-4">

          <!-- Profile Image -->
          <div class="box box-default">
            <div class="box-body box-profile">
                <asp:Image ID="imgPerson" class="profile-user-img img-responsive img-circle"  runat="server" Height="150px" Width="150px" ImageUrl="~/images/persons/nopic.jpg" />
                             
<p></p>
              <h3 class="profile-username text-center">
                 <asp:Label ID="lblPersonName" runat="server"></asp:Label></h3>
 
              <p class="text-muted text-center">
                  <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                  </p>
 <p class="text-muted text-center"><asp:HyperLink ID="cmdPrint" runat="server" Target="_blank" NavigateUrl="PrintCV.aspx" cssClass="btn-default">Print CV</asp:HyperLink></p>
                                
            </div> 
          </div> 
        </div>
     
        <div class="col-md-8">
             
      <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title"><asp:Label ID="lblName" runat="server"></asp:Label></h2>

        </div>
        <div class="box-body">
    <table>          
           <tr>
            <td>
                ตำแหน่ง
            </td>
            <td>
                <asp:Label ID="lblPosition" runat="server"></asp:Label>
            </td>
        </tr>
      <tr>
            <td >
                <asp:Label ID="lblMajor_Label" runat="server" Text="สาขาวิชา"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMajor" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
              <td width="150" class="texttopic"><asp:Label ID="lblPosition_Edu_label" runat="server" Text="ตำแหน่งทางวิชาการ"></asp:Label></td>
              <td class="textcontent">
                  <asp:Label ID="lblPositionEdu" runat="server"></asp:Label>
              </td>
          </tr>

        <tr>
            <td >
                คุณวุฒิ</td>
            <td>
                <asp:Label ID="lblEducate" runat="server"></asp:Label>
            </td>
        </tr>
       <tr>
            <td>เบอร์ติดต่อ</td>
            <td>
                <table>
                    <tr>
                        <td>
                <asp:Label ID="lblTel" runat="server"></asp:Label>
                        </td>
                        <td width="100" align="right">
                <asp:Label ID="lblMobileLabel" runat="server" Text="เบอร์มือถือ"></asp:Label>
                        </td>
                        <td>
                <asp:Label ID="lblMobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

          <tr>
              <td width="150" class="texttopic">
         
             
                  อีเมล์</td>
              <td class="textcontent">
                  <asp:Label ID="lblMail" runat="server"></asp:Label>
              </td>
          </tr>
        
               
         
          </table>  
        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->

        </div>
        <!-- /.col -->
</div>
      <!-- /.row -->
  <!-- Custom Tabs -->
          <div class="nav-tabs-custom tab-success">
            <ul class="nav nav-tabs">
            <li class="active"><a href="#tab_Edu" data-toggle="tab">การศึกษา</a></li>
              <li><a href="#tab_Subject" data-toggle="tab">รายวิชาที่สอน</a></li>
              <li><a href="#tab_researh" data-toggle="tab">ผลงานด้านการวิจัย</a></li>
              <li><a href="#tab_award" data-toggle="tab">รางวัลที่ได้รับ</a></li>
                <li><a href="#tab_train" data-toggle="tab">การอบรม</a></li>

           <!--   <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#">การอบรม <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#tab_train1">การอบรมทางวิชาชีพ</a></li>
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">การอบรมด้านแพทยศาสตรศึกษา</a></li>
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">การอบรมด้านบริหารและประกันคุณภาพ</a></li>
                </ul>
              </li> -->
            </ul>
            <div class="tab-content">

              <div class="tab-pane active" id="tab_Edu">
                  <asp:GridView ID="grdEdu" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="None" ShowHeader="false">
                <Columns>
                    <asp:BoundField DataField="EduNameTH" />
                </Columns>
                <HeaderStyle  HorizontalAlign="Center" />
            </asp:GridView>                             
                   
              </div>
              <!-- /.tab-pane -->

                <div class="tab-pane"  id="tab_Subject">
                    <p class="text-success text-bold">  ผู้ประสานงานหลัก</p>
                    <asp:Label ID="lblAdvisorSubject" runat="server" Text=""></asp:Label>
                    <br /><br />
                 <p class="text-success text-bold">   ผู้ประสานงานร่วมสอน</p>
                    <asp:Label ID="lblCoordinatorSubject" runat="server" Text=""></asp:Label>
                 
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_researh">                   
                     <asp:GridView ID="grdResearch" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="99%" BorderColor="#CCCCCC">
                                    <RowStyle BackColor="White" VerticalAlign="Top" />
                                    <columns>
                                        <asp:BoundField DataField="nRow" HeaderText="No.">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="ชื่องานวิจัย">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TitleTH") %>'></asp:Label>                                                
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RYear" HeaderText="ปี">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PresentName" HeaderText="การนำเสนอ">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupName" HeaderText="ด้าน">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="ประเภท">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:TemplateField>
                                    </columns>
                                    <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <headerstyle Font-Bold="True" VerticalAlign="Middle" HorizontalAlign="Center" BackColor="#006600" ForeColor="White" Height="28px" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="#EBEBEB" />
                                </asp:GridView>
                              

              </div>
              <!-- /.tab-pane -->

              <div class="tab-pane" id="tab_award">
                  <asp:Label ID="lblAward" runat="server" Text=""></asp:Label>
              </div>
              <!-- /.tab-pane -->

                 <div class="tab-pane"  id="tab_train">
                    <asp:GridView ID="grdTrain" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="99%" BorderColor="#CCCCCC">
                                    <RowStyle BackColor="White" VerticalAlign="Top" />
                                    <columns>
                                        <asp:TemplateField HeaderText="ปี">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RYear") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หัวข้อการอบรม">
                                            <ItemTemplate>
                                                <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TopicName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="หมวด" DataField="CategoryName" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ด้าน" DataField="GroupName" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </columns>
                                    <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <pagerstyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <headerstyle Font-Bold="True" VerticalAlign="Middle" BackColor="#006600" ForeColor="White" Height="28px" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="#EBEBEB" />
                                </asp:GridView>
              </div>
              <!-- /.tab-pane -->

            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->

  

</section>
        </div>
</asp:Content>
