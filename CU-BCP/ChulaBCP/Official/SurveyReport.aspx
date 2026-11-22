<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="SurveyReport.aspx.vb" Inherits="SurveyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>สรุปคะแนนแบบประเมินความพึงพอใจ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Report</li>
      </ol>
    </section>

<section class="content">
 
        <div class="box box-primary">
        <div class="box-header with-border">
          <h2 class="box-title"><asp:Label ID="lblSubjectName" runat="server"></asp:Label></h3>

        </div>       
       
      </div>
      <!-- /.box --> 
   
 <h3>ข้อมูลผู้ตอบแบบประเมิน จำนวนทั้งหมด <asp:Label ID="lblExamCount" runat="server"></asp:Label> &nbsp;คน
        <small></small>
      </h3>
  <div class="row">  
           <section class="col-lg-6 connectedSortable"> 
               <div class="box box-info">
        <div class="box-header with-border">
          <h2 class="box-title">เพศ</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body"> 

             <asp:GridView ID="grdSex" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table50" GridLines="None">
                 <Columns>
                     <asp:BoundField DataField="SexName" HeaderText="เพศ" />
                     <asp:BoundField DataField="nCount" HeaderText="จำนวน" />
                     <asp:BoundField DataField="Result" HeaderText="ร้อยละ" DataFormatString="{0:##.##}" />
                 </Columns>
                 <HeaderStyle HorizontalAlign="Center" />
                 <RowStyle HorizontalAlign="Center" />
             </asp:GridView>

             </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
        <!-- /.box-footer-->
      </div>
            </section>
        <section class="col-lg-6 connectedSortable">
             <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">สถานะผู้ตอบแบบประเมิน</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body"> 
         
             <table class="table50">
                 <tr>
                     <th>สถานะ</th>
                     <th>จำนวน</th>
                     <th>ร้อยละ</th>
                 </tr>
                 <tr>
                     <td>อาจารย์</td>
                     <td align="center">
                         <asp:Label ID="lblCount1" runat="server"></asp:Label>
                     </td>
                     <td align="center">
                       <asp:Label ID="lblResult1" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td>บุคลากร</td>
                     <td align="center">
                         <asp:Label ID="lblCount2" runat="server"></asp:Label>
                     </td>
                     <td align="center">
                       <asp:Label ID="lblResult2" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td>ลูกจ้าง</td>
                     <td align="center">
                         <asp:Label ID="lblCount3" runat="server"></asp:Label>
                     </td>
                     <td align="center">
                       <asp:Label ID="lblResult3" runat="server"></asp:Label>
                     </td>
                 </tr>
                  <tr>
                     <td>บุคคลทั่วไป</td>
                     <td align="center">
                         <asp:Label ID="lblCount4" runat="server"></asp:Label>
                      </td>
                     <td align="center">
                       <asp:Label ID="lblResult4" runat="server"></asp:Label>
                      </td>
                 </tr>  
                 <tr>
                     <td>อื่นๆ</td>
                     <td align="center">
                         <asp:Label ID="lblCount5" runat="server"></asp:Label>
                     </td>
                     <td align="center">
                       <asp:Label ID="lblResult5" runat="server"></asp:Label>
                     </td>
                 </tr>

             </table>
         
             </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
        <!-- /.box-footer-->
      </div>
            </section>
      </div>
  

            <div class="box box-primary">
        <div class="box-header with-border">
          <h2 class="box-title">สรุปผลคะแนน</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body">
        
              <asp:GridView ID="grdScore" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%">
                  <Columns>
                      <asp:BoundField DataField="Descriptions" HeaderText="ประเด็น" >
                      <ItemStyle HorizontalAlign="Left" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="พอใจมากที่สุด">
                          <ItemTemplate>
                              <asp:Label ID="lblScore5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score5") %>'></asp:Label>
                              <br />
                              <asp:Label ID="lblScoreResult5" runat="server"  Text='<%# "(" & DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreResult5")).ToString("##.##") & ")" %> '></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="พอใจมาก">
                           <ItemTemplate>
                              <asp:Label ID="lblScore4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score4") %>'></asp:Label>
                              <br />
                              <asp:Label ID="lblScoreResult4" runat="server" Text='<%# "(" & DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreResult4")).ToString("##.##") & ")" %> '></asp:Label>
                          </ItemTemplate>

                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ปานกลาง">
                           <ItemTemplate>
                              <asp:Label ID="lblScore3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score3") %>'></asp:Label>
                              <br />
                              <asp:Label ID="lblScoreResult3" runat="server" Text='<%# "(" & DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreResult3")).ToString("##.##") & ")" %> '></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ไม่พอใจมาก">
                           <ItemTemplate>
                              <asp:Label ID="lblScore2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score2") %>'></asp:Label>
                              <br />
                              <asp:Label ID="lblScoreResult2" runat="server" Text='<%# "(" & DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreResult2")).ToString("##.##") & ")" %> '></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ไม่พอใจมากที่สุด">
                           <ItemTemplate>
                              <asp:Label ID="lblScore1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score1") %>'></asp:Label>
                              <br />
                              <asp:Label ID="lblScoreResult1" runat="server" Text='<%# "(" & DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreResult1")).ToString("##.##") & ")" %> '></asp:Label>
                          </ItemTemplate>

                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ค่าเฉลี่ย">
                           <ItemTemplate>
                              <asp:Label ID="lblScoreAVG" runat="server" Text='<%# DBNull2Dbl(DataBinder.Eval(Container.DataItem, "ScoreAVG")).ToString("##.##") %>'></asp:Label>
                              
                          </ItemTemplate>

                      </asp:TemplateField>
                      <asp:BoundField HeaderText="สรุประดับความพึงพอใจ" DataField="Result" />
                  </Columns>
                  <HeaderStyle HorizontalAlign="Center" Height="35px" />
                  <RowStyle HorizontalAlign="Center" />
              </asp:GridView>
        
              </div>
        <!-- /.box-body -->
        <div class="box-footer">
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->  
        
 
       

  </section>                  



</asp:Content>
