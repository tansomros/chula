<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Document.aspx.vb" Inherits="Document" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-edit icon-gradient bg-primary"></i>
                                </div>
                                <div> Repository List
                                    <div class="page-title-subheading">ข้อมูลรายการส่งงาน</div>
                                </div>
                            </div>
                        </div>
                    </div>    

<section class="content">
                   
      <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-select icon-gradient bg-success">
            </i>ค้นหา   
             <div class="btn-actions-pane-right">
                     <% If Request.Cookies("ROLE_STD").Value = True Then %>  
                <a href="DocumentUpload.aspx?m=rep" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
                 <% End If %>
            </div>
        </div>
        <div class="card-body">  
             <div class="row">
                  <div class="col-md-2">
                    <div class="form-group">
                    <label>ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2">  </asp:DropDownList>
                    </div>
                   </div>
                  <div class="col-md-4">
                                        <div class="form-group">
                                              <label>&nbsp;</label> 
                                            <br /> 
                                                 <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="100px" />  
                                        </div>
                                    </div>

                                </div>                                     
    </div>
   
        <div class="box-footer text-center">  
        </div>   
      </div>                 
      <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-list icon-gradient bg-success">
            </i>รายการส่งงาน
           
        </div>
        <div class="card-body"> 
             <table id="tbdata" class="table table-hover">
                <thead>
                <tr>      
                  <th>No.</th>
                  <th>รายการ</th>
                  <th>รหัส</th>
                  <th>ชื่อ-นามสกุล</th>   
                  <th>File</th>  
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtRep.Rows %>
                <tr>                 
                  <td><% =String.Concat(row("nRow")) %></td>
                  <td><a  href="DocumentUpload?m=rep&docid=<% =String.Concat(row("UID")) %>" ><% =String.Concat(row("DocumentName")) %></a></td>           
                  <td><% =String.Concat(row("StudentCode")) %></td> 
                  <td><% =String.Concat(row("StudentName")) %></td>
                  <td class="text-center"> 
                      <% If String.Concat(dtRep.Rows(0)("FilePath")) <> "" %>
                      <a href="../Documents/Student/<% =String.Concat(row("FilePath")) %>" target="_blank" class="font-icon-button"><i class="fa fa-file-pdf" aria-hidden="true"></i></a>
                      <% End If %>
                  </td>
                </tr>
            <%  Next %>
                </tbody>               
              </table>    
                                
    </div>
        <div class="box-footer">       
        </div>
      </div>     
  </section>              
</asp:Content>
