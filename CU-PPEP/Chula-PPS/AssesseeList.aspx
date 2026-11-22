<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssesseeList.aspx.vb" Inherits=".AssesseeList" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-check icon-gradient bg-success"></i>
            </div>
            <div>Evaluate Student Internship                  
                 <div class="page-title-subheading">
<asp:Label ID="lblFor" runat="server"></asp:Label>
                 </div>
            </div>
        </div>
    </div>
</div>  
     

<section class="content">  
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกรายวิชา (Select a course)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                  <div class="row">
      <div class="col-md-2">
          <div class="form-group">
              <label> ปีการศึกษา (Year)</label>
               <asp:Label ID="lblYearEn" runat="server"></asp:Label>
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
          </div>
      </div>
                       <div class="col-md-5">
     <div class="form-group">
         <label>ผลัดฝึก (Batch) </label>
           <asp:DropDownList ID="ddlPhase" runat="server" CssClass="form-control select2" AutoPostBack="True" > </asp:DropDownList>
     </div>

 </div>

                       <div class="col-md-5">
     <div class="form-group">
         <label>รายวิชา (Course)</label>
           <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True" ></asp:DropDownList>
     </div>

 </div>

 </div>
                                        
</div>
         <div class="box-footer clearfix text-danger">
             <asp:Label ID="lblNote" runat="server" Text="เลือกผลัด/รายวิชา เพื่อดูรายชื่อนิสิตที่ต้องการทำประเมิน และจะแสดงเฉพาะรายวิชาที่รับฝึก/รับผิดชอบ"></asp:Label>
            </div>
          </div>

   
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-circle-o"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ต้องประเมิน (List of students to be evaluated)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

                            <div class="row">
                                <div class="col-md-4">
    <div class="form-group">
        <label>Status</label>
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" AutoPostBack="True">      
                      <asp:ListItem Value="A" Selected="True">All</asp:ListItem>      
                      <asp:ListItem  Value="P">Pending</asp:ListItem>
                      <asp:ListItem Value="C">Completed</asp:ListItem>
                </asp:DropDownList>
    </div>
</div>
                                <div class="col-md-4">
    <div class="form-group">
        <label> Search</label>
         <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>   
    </div>
</div>
                                                                <div class="col-md-4">
    <div class="form-group"> 
         <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-primary" Text="Search" width="100" />
    </div>
</div>
                                </div>
 <div class="row table-responsive">             
     <table id="tbuser" class="table table-hover">
         <thead>
             <tr>                                          
                 <th class="text-center">No.</th>
                 <th class="text-left">รหัสนิสิต(ID)</th>
                 <th class="text-left">ชื่อ-สกุล (Name)</th>
                 <th class="text-left">แหล่งฝึก (Internship Site)</th>
                 <th class="text-center">ผลัดฝึก (Batch)</th>
                 <th class="text-center">วันที่ฝึก (Period)</th>
                 <th class="text-center">สถานะ (Status)</th>     
                 <th class="sorting_asc_disabled sorting_desc_disabled text-center">ประเมิน (Estimate)</th>    
             </tr>
         </thead>
         <tbody>
             <% For Each row As DataRow In dtAssessee.Rows %>
             <tr>                
                 <td class="text-center"><% =String.Concat(row("nRow")) %></td>               
                 <td class="text-center"><% =String.Concat(row("Student_Code")) %></td>
                 <td class="text-left"><% =String.Concat(row("StudentName")) %></td>
                 <td class="text-left"><% =String.Concat(row("LocationName")) %></td>  
                 <td class="text-center"><% =String.Concat(row("PhaseNo")) %></td>  
                 <td class="text-center"><% =String.Concat(row("PhaseName")) %></td>             
                 <td class="text-center">
                    <% If String.Concat(row("AssessmentStatus")) = "Y" Then%>
                      <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
                    <% End If %>
                 </td> 
                  <td width="50" class="text-center">
                        <a href="AssesseeEvaluationGroup.aspx?p=a&std=<% =String.Concat(row("Student_Code")) %>&sj=<% =String.Concat(row("SubjectCode")) %>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="Estimate" target="_blank"><i class="fa fa-edit" aria-hidden="true"></i></a>                                                    
 </td>
             </tr>
             <%  Next %>
         </tbody>
     </table>
         </div>                         
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    



</section>    
</asp:Content>
