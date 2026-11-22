<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentModify.aspx.vb" Inherits=".AssessmentModify" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>แก้ไขผลการจัดสรรแหล่งฝึก</h1>   
    </section>

<section class="content">  
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>

              <h3 class="box-title">แก้ไขผลการจัดสรรแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

                  <div class="row">
                        <div class="col-md-1">
          <div class="form-group">
            <label>ID</label>
               <asp:TextBox ID="txtUID" runat="server"  cssclass="form-control text-center" ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox> 
          </div>

        </div>

           <div class="col-md-1">
          <div class="form-group">
            <label>ปีการศึกษา</label>
               <asp:TextBox ID="txtYear" runat="server"  cssclass="form-control text-center" ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox> 
          </div>

        </div>
              <div class="col-md-2">
          <div class="form-group">
            <label>รหัสนิสิต</label>
               <asp:TextBox ID="txtStudentCode" runat="server"  cssclass="form-control text-center" ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox> 
          </div>

        </div>         
    <div class="col-md-3">
          <div class="form-group">
            <label>ชื่อ-สกุล</label>
               <asp:TextBox ID="txtName" runat="server"  cssclass="form-control text-center" ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox> 
          </div>

        </div>    

                       </div>
                  <div class="row">
                               <div class="col-md-1">
          <div class="form-group">
            <label>Course ID</label>
              <asp:TextBox ID="txtCourseID" runat="server" cssclass="form-control text-center"  ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox>     
          </div>

        </div> 

                 <div class="col-md-1">
          <div class="form-group">
            <label>รหัสรายวิชา</label>
              <asp:TextBox ID="txtSubjectCode" runat="server" cssclass="form-control"  ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox>     
          </div>

        </div> 

                      <div class="col-md-10">
          <div class="form-group">
            <label>ชื่อรายวิชา</label>
              <asp:TextBox ID="txtSubjectName" runat="server" cssclass="form-control"  ReadOnly="true" BackColor="#FFD9FF"></asp:TextBox>     
          </div>

        </div>    

                  </div>
                  <div class="row">
                        <div class="col-md-6">
          <div class="form-group">
            <label>แหล่งฝึก</label>
               <asp:DropDownList ID="ddlLocation" runat="server"  cssclass="form-control select2"  placeholder="เลือกแหล่งฝึก">
      </asp:DropDownList>
          </div>

        </div>
     <div class="col-md-6">
          <div class="form-group">
            <label>ผลัดฝึก</label>
               <asp:DropDownList ID="ddlPhase" runat="server"  cssclass="form-control select2"  placeholder="เลือกผลัดฝึก">
      </asp:DropDownList>
          </div>

        </div>   
                   </div>

                                    
</div>
            <div class="box-footer text-center clearfix">
            <asp:Button ID="cmdSave" runat="server"  Text="บันทึก" CssClass="btn btn-primary" Width="100px"/> 
               
          &nbsp;<asp:Button ID="cmdBack" runat="server"  Text="Back" CssClass="btn btn-default" Width="100px"/> 
            </div>
          </div>
    
    </section>
</asp:Content>
