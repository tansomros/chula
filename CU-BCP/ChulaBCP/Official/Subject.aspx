<%@ Page Title="Course Syllabus" MetaDescription="ข้อมูลประมวลรายวิชา" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Subject.aspx.vb" Inherits=".Subject" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
     <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-bookmarks icon-gradient bg-success"></i>
                        </div>
                        <div><%: Title %>    
                            <div class="page-title-subheading"><%: MetaDescription %>   </div>
                        </div>
                    </div>
                </div>
            </div>   

    <section class="content"> 
         <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-book icon-gradient bg-success">
            </i>ข้อมูลรายวิชา
            <div class="btn-actions-pane-right">
                <a href="SubjectModify?m=comp" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
            </div>
        </div>
        <div class="card-body">                  
              <table id="tbdata" class="table table-hover">
                <thead>
                <tr>
                 <th class="sorting_asc_disabled sortting_dsc_disabled"></th>    
                    <th>รหัสวิชา</th>
                  <th>ชื่อรายวิชา</th>
                  <th>หน่วยกิต</th> 
                     <th>ประเภท</th> 
                     <th class="sorting_asc_disabled sortting_dsc_disabled"></th>
                    
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtSj.Rows %>
                <tr>
                 <td width="30px"><a  href="SubjectModify?cid=<% =String.Concat(row("SubjectID")) %>" ><img src="images/icon-edit.png"/></a>
                    </td>
                  <td><% =String.Concat(row("SubjectCode")) %></td>
                  <td><% =String.Concat(row("NameTH")) %> <br /><I><% =String.Concat(row("NameEN")) %></I>    </td>
                  <td><% =String.Concat(row("SubjectUnit")) %></td> 
                    <td class="text-center"><% =String.Concat(row("SubjectTypeCode")) %></td> 
                  <td class="text-center"><% If String.Concat(row("filePath")) <> "" Then %>                      
                      <a href="<% ="../Documents/Subject/" & String.Concat(row("filePath")) %>" target="_blank" class="font-icon-button"><i class="fa fa-file-pdf" aria-hidden="true"></i> ดูรายละเอียด</a> 
                      <% End If %>
                    </td>                                      
                </tr>
            <%  Next %>
                </tbody>               
              </table>                                    
            </div>
            <!-- /.box-body -->
          </div>
  
    </section>
</asp:Content>
