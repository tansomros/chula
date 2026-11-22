<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentList.aspx.vb" Inherits=".StudentList" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>เภสัชกรประจำบ้าน/ผู้เข้าอบรม
                                    <div class="page-title-subheading">จัดการข้อมูลเภสัชกรประจำบ้าน</div>
                                </div>
                            </div>
                        </div>
</div>

<section class="content">  

    <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>ข้อมูลเภสัชกร
            <div class="btn-actions-pane-right">
                <a href="StudentReg?m=pm" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
            </div>
        </div>
        <div class="card-body">  

                 <table id="tbdata" class="table table-hover">
                <thead>
                <tr>
                
                  <th>รหัส</th>
                  <th>ชื่อ</th>
                  <th>นามสกุล</th> 
                  <th>สาขา</th>      
                  <th>ชั้นปีที่</th>  
                  <th>สถานะ</th> 
                  <th class="sorting_asc_disabled">ผลการศึกษา</th>    
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtStd.Rows %>
                <tr>
                     <td><% =String.Concat(row("StudentCode")) %></td>
                     <td><% =String.Concat(row("FirstName")) %>    </td>
                     <td><% =String.Concat(row("LastName")) %></td> 
                     <td><% =String.Concat(row("MajorName")) %></td> 
                     <td><% =String.Concat(row("LevelClass")) %></td> 
                     <td><% =String.Concat(row("StudentStatusName")) %></td>    
                     <td><a  href="StudentGradeAdmin?m=res&std=<% =String.Concat(row("StudentID")) %>" ><img src="images/icon-edit.png"/></a>
                     <a  href="StudentGrade?m=grd&std=<% =String.Concat(row("StudentID")) %>" target="_blank" ><img src="images/view.png"/></a>
                    </td>
                </tr>
            <%  Next %>
                </tbody>               
              </table>         
                                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>                        
    </section>
</asp:Content>
