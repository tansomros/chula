<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Request.aspx.vb" Inherits=".Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-pen icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Request
                                    <div class="page-title-subheading">ลงทะเบียนคำร้องขอ/แสดงความจำนง</div>
                                </div>
                            </div>
                        </div>
                    </div>
<section class="content">     
         <div class="row">    
            <section class="col-lg-12 connectedSortable">
<div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-pencil icon-gradient bg-success">
            </i>ข้อมูลการลงทะเบียนคำร้องขอ/แสดงความจำนง                   
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdStudentID" runat="server" />
            <asp:HiddenField ID="hdRequestID" runat="server" />
              </div>                 
            </div>
            <div class="card-body">
    <div class="row">
              <div class="col-md-2">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-5">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStudentName" runat="server"></asp:Label>
                </div>
              </div>
          <div class="col-md-5">
                <div class="form-group">
                  <label>สาขา</label>
                  <asp:Label CssClass="form-control text-center" ID="lblMajor" runat="server"></asp:Label>
                </div>
              </div>
                </div>
                  <div class="row">                  
               <div class="col-md-2">
                <div class="form-group">
                  <label>วันที่คำร้อง</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtReqDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>
                <div class="col-md-10">
                <div class="form-group">
                  <label>เรื่องที่ลงทะเบียน</label><asp:HiddenField ID="hdTopicUID" runat="server" />
                     <asp:Label ID="lblTopic" runat="server"  CssClass="form-control text-primary"  Text=""></asp:Label>                     
                </div>
              </div>  
                </div>

            <div id="pnThesis" runat="server" class="row">               
                    <div class="col-md-12">
                <div class="form-group">
                  <label>ชื่อวิทยานิพนธ์<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtThesisName" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div> 

            </div>
              <div class="row">
                    <div class="col-md-2">
                <div class="form-group">
                  <label>วันที่เข้าสอบ</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtAdmitDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>
                    <div class="col-md-3">
                <div class="form-group">
                  <label>ตั้งแต่เวลา<span class="text-red">*</span></label><br />
                    <asp:DropDownList ID="ddlStartTimeHour" runat="server" CssClass="form-control select2" Width="70px">
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                    </asp:DropDownList>:
                     <asp:DropDownList ID="ddlStartTimeMinute" runat="server" CssClass="select2" Width="70px">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                    </asp:DropDownList>
              
                </div>
              </div>  
                    <div class="col-md-3">
                <div class="form-group">
                  <label>ถึงเวลา<span class="text-red">*</span></label><br />
                    <asp:DropDownList ID="ddlEndTimeHour" runat="server" CssClass="form-control select2" Width="70px">
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                    </asp:DropDownList>:
                     <asp:DropDownList ID="ddlEndTimeMinute" runat="server" CssClass="select2" Width="70px">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                    </asp:DropDownList>
                </div>
              </div>

              </div>  
          <div class="row"> 
                    <div class="col-md-3">
                <div class="form-group">
                  <label>ณ ห้อง<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtRoomNo" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                    <div class="col-md-3">
                <div class="form-group">
                  <label>ฝ่าย/แผนก/ภาควิชา<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                    <div class="col-md-2">
                <div class="form-group">
                  <label>ชั้น<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtFloor" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                    <div class="col-md-4">
                <div class="form-group">
                  <label>อาคาร<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtBuilding" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div> 
                </div>  
          <div class="row"> 
                    <div class="col-md-6">
                <div class="form-group">
                  <label>รพ./คณะ/มหาวิทยาลัย<span class="text-red">*</span></label>
                    <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                    <div class="col-md-3">
                <div class="form-group">
                  <label>ถนน</label>
                    <asp:TextBox ID="txtRoad" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                   <div class="col-md-3">
                <div class="form-group">
                  <label>ตำบล/แขวง</label>
                    <asp:TextBox ID="txtSubdistrict" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                   <div class="col-md-3">
                <div class="form-group">
                  <label>อำเภอ/เขต</label>
                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                   <div class="col-md-3">
                <div class="form-group">
                  <label>จังหวัด</label>
                      <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>  
                   <div class="col-md-2">
                <div class="form-group">
                  <label>รหัสไปรษณีย์</label>
                    <asp:TextBox ID="txtZipcode" runat="server" CssClass="form-control text-center" MaxLength="5" ></asp:TextBox>
                </div>
              </div>  
               
            </div>



    </div>
</div>
   </section>
   </div>
    <asp:Panel ID="pnCommittee" runat="server">
         <div class="row">
      <section class="col-lg-12 connectedSortable">   
      
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-graduation-cap"></i>
             รายชื่อกรรมการควบคุมการสอบฯ          
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
               
              </div>                 
            </div>
            <div class="box-body">
                <h5 class="text-primary">เลือกจากคณาจารย์ภายใน</h5>
                        <div class="row">
                        <div class="col-md-5">
                <div class="form-group">
                  <label>เลือกกรรมการ</label>
                     <asp:DropDownList ID="ddlAdvisor" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div> 
                 <div class="col-md-5">
                <div class="form-group">
                  <label>ตำแหน่ง</label>
                 <asp:DropDownList ID="ddlPosition1" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>
                 <div class="col-md-2">
                <div class="form-group">
                  <br />
                  <asp:Button ID="cmdAddCommittee1" runat="server" Text="+ เพิ่มคณะกรรมการ" CssClass="btn btn-success" />
                </div>
              </div>
                      </div>
                 <h5 class="text-primary">คณะกรรมจากภายนอก</h5>
                  <div class="row">
                        <div class="col-md-3">
                <div class="form-group">
                  <label>ชื่อ-สกุล</label>
                    <asp:TextBox ID="txtCommitteeName" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
                        <div class="col-md-3">
                <div class="form-group">
                  <label>สังกัด</label>
                    <asp:TextBox ID="txtCommitteeDepartment" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
              </div>  
 <div class="col-md-3">
                <div class="form-group">
                  <label>ตำแหน่ง</label>
                 <asp:DropDownList ID="ddlPosition2" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>
                  
                     <div class="col-md-2">
                <div class="form-group">
                  <br />
                  <asp:Button ID="cmdAddCommittee2" runat="server" Text="+ เพิ่มคณะกรรมการ" CssClass="btn btn-success" />
                </div>
              </div>
                      </div>
<h5 class="text-primary">รายชื่อกรรมการควบคุมการสอบ</h5>
                  <div class="row table-responsive">                            
                          <div class="col-md-12">
              <asp:GridView ID="grdCommittee" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="CommitteeName" HeaderText="ชื่อ-สกุล">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
<asp:BoundField HeaderText="สังกัด" DataField="DepartmentName">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PositionName" HeaderText="ตำแหน่ง" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField>
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
         </div>
                          </div>
     
    </div>
</div>
 
</section>
   </div>               
  </asp:Panel>  
  <asp:Panel ID="pnWorkSend" runat="server">
       <div class="row">
      <section class="col-lg-12 connectedSortable">   
          <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>เอกสารแนบ                    
                 <div class="box-tools pull-right">                      
              </div>                 
            </div>
            <div class="card-body">
                  <div class="row">                    
                   <div class="col-md-6">
                     <div class="form-group">
                        <label>Upload เอกสาร</label> 
                         <br />
                      <asp:FileUpload ID="FileUploadFile" runat="server" /> 
                         &nbsp;<asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-success" Text="Upload" />
                     </div>
                       </div>     
       </div>
                      <div class="row"> 
              <div class="col-md-12">
                <div class="form-group">
                  <label></label>                   
                    <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-hover" ForeColor="#333333" GridLines="None" Width="100%">
                        <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                        <columns>
                            <asp:BoundField DataField="nRow" HeaderText="No." >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:HyperLinkField HeaderText="Document Name" DataNavigateUrlFields="DocumentLink" DataTextField="DocumentName" Target="_blank" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel_Doc" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" />
                                </itemtemplate>
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" VerticalAlign="Middle" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                   
                </div>
              </div>   
                </div>

  </div>
</div>
      </section>
   </div>  
  </asp:Panel>  

        <div align="center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Next" Width="100px" />
        <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-success" Text="พิมพ์" Width="100px" />
            <asp:Button ID="cmdDelete"  runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" />
        </div>

</section>
</asp:Content>
 

