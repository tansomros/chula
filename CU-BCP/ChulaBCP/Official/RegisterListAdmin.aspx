<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="RegisterListAdmin.aspx.vb" Inherits=".RegisterListAdmin" %>
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
                                <div>Register List
                                    <div class="page-title-subheading">ข้อมูลการลงทะเบียน</div>
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
                <a href="Register.aspx?m=reg" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> ลงทะเบียนใหม่</a>
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
                   <div class="col-md-8">
                    <div class="form-group">
                    <label>หัวข้อ</label>
                    <asp:DropDownList ID="ddlTopic" runat="server" CssClass="form-control select2">  </asp:DropDownList>
                    </div>
                   </div>
                  <div class="col-md-2">
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
            </i>รายการการลงทะเบียน
           
        </div>
        <div class="card-body"> 
             <table id="tbdata" class="table table-hover">
                <thead>
                <tr>      
                  <th>No.</th>
                  <th>รายการลงทะเบียน</th>
                  <th>ปี</th>
                  <th>รหัสผู้ลงทะเบียน</th>   
                  <th>ชื่อผู้ลงทะเบียน</th>   
                  <th>สถานะ</th>  
                       <th></th>  
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtReg.Rows %>
                <tr>                 
                  <td><% =String.Concat(row("nRow")) %></td>
                  <td><a  href="Register?m=reg&regid=<% =String.Concat(row("RegisterID")) %>" ><% =String.Concat(row("TopicName")) %></a></td>                 
                  <td><% =String.Concat(row("EduYear")) %></td> 
                    <td><% =String.Concat(row("StudentCode")) %></td> 
                    <td><% =String.Concat(row("StudentName")) %></td> 
                  <td><% =String.Concat(row("StatusName")) %></td>
                    
                     <td><% If String.Concat(row("StatusFlag")) = "A" Then  %> 
                      <a  href="ReportViewer?R=<% =IIf(String.Concat(row("TopicUID")) = "3", "regm", "reg") %>&regid=<% =String.Concat(row("RegisterID")) %>&std=<% =String.Concat(row("StudentID")) %>" target="_blank" data-toggle="tooltip" data-placement="top" data-original-title="พิมพ์แบบบัตรลงทะเบียน"><img src="../images/printer.png" /></a>
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
