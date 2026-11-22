<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentGradeAdmin.aspx.vb" Inherits=".StudentGradeAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-pen icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>จัดการผลการศึกษา
                                    <div class="page-title-subheading">เพิ่ม / แก้ไข ผลการศึกษา (สำหรับ admin)</div>
                                </div>
                            </div>
                        </div>
                    </div>
<section class="content">  
       <div class="row">
     <section class="col-lg-12 connectedSortable">
    <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-user icon-gradient bg-success">
            </i>ข้อมูลนิสิต/ผู้เข้าอบรม              
                 <div class="box-tools pull-right">   
   <asp:HiddenField ID="hdStudentID" runat="server" /> 
              </div>                 
            </div>
            <div class="card-body">
         <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="text-blue" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-4">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล</label>
                  <asp:Label CssClass="text-blue" ID="lblStudentName" runat="server"></asp:Label>
                </div>
              </div>
         <div class="col-md-5">
                <div class="form-group">
                  <label>สาขา</label>
                  <asp:Label CssClass="text-blue" ID="lblMajor" runat="server"></asp:Label>
                </div>
              </div>

                </div>
</div>  
        </div>
         </section>
           </div>

       <div class="row">
     <section class="col-lg-6 connectedSortable">
    <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-pencil icon-gradient bg-success">
            </i>บันทึกผลการศึกษา               
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdUID" runat="server" />                   
              </div>                 
            </div>
            <div class="card-body">
          
     <div class="row">
                    <div class="col-md-12">
                <div class="form-group">
                  <label>รายวิชา</label>                   
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
              </div>
     </div>
 
                  <div class="row">
    <div class="col-md-3">
                <div class="form-group">
                  <label>ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>   
              <div class="col-md-3">
                <div class="form-group">
                  <label>ภาคการศึกษา</label>
                    <asp:RadioButtonList ID="optTerm" runat="server"  AutoPostBack="True"  RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ที่ 1</asp:ListItem>
                        <asp:ListItem Value="2">ที่ 2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              </div>
                          <div class="col-md-3">
                <div class="form-group">
                  <label>หน่วยกิต</label>
                    <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control text-center"></asp:TextBox>
                </div>
              </div>
                            <div class="col-md-3">
                <div class="form-group">
                  <label>เกรดที่ได้</label>
                      <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control text-center"></asp:TextBox>
                </div>
              </div>
                        </div>
 
                  <div class="row text-center">
                       <div class="col-md-12">
                <div class="form-group">          
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary" Width="100" />  
                      <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" Width="100" />  
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-danger" Width="100" />   
                </div>
              </div>

</div>                
                                      
</div>           
 
            <div class="box-footer clearfix">
                  <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">C (CREDIT)</label>
                   = รายวิชาที่วัดผลเป็นระดับขั้น

                </div>
              </div>             
                    <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">A (AUDIT)</label>
                 = รายวิชาที่ไม่นับหน่วยกิตเป็น
หน่วยกิตสะสม วัดผลเป็น
สัญลักษณ์ S และ U  
                </div>
              </div>
         <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">N (NON-CREDIT)</label>              
 = รายวิชาที่ไม่มีจานวนหน่วยกิต
วัดผลเป็นสัญลักษณ์ G P และ F  
                </div>
              </div>

                </div>
            </div>
    </div>  
         </section>         
           
    <section class="col-lg-6 connectedSortable"> 
      <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-screen icon-gradient bg-success">
            </i>ผลการศึกษา               
                 <div class="box-tools pull-right">                     
              </div>                 
            </div>
            <div class="card-body">              
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" PageSize="20" CssClass="table table-hover" GridLines="None">
                            <RowStyle HorizontalAlign="Center" />
                            <columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                                        ImageUrl="images/icon-edit.png" />                                   
                                        <asp:ImageButton ID="imgDel" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                                        ImageUrl="images/icon-delete.png" />   
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectName" HeaderText="ชื่อวิชา">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต" />
                                <asp:BoundField DataField="SubjectTypeCode" HeaderText="ประเภทวิชา">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Grade" HeaderText="ระดับที่ได้" />
                            </columns>
                            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                            <headerstyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                  
                                                 
</div>
          </div>
    </section>
</div>

</section>
</asp:Content>
