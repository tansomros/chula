<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="RequestList.aspx.vb" Inherits="RequestList" %>
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
                                <div>Request List
                                    <div class="page-title-subheading">ข้อมูลรายการคำร้องต่างๆ</div>
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
                <a href="Request.aspx?m=Req" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มคำร้องขอใหม่</a>
                 <% End If %>
            </div>
        </div>
        <div class="card-body">  
             <div class="row">
                  <div class="col-md-10">
                    <div class="form-group">
                    <label>หัวข้อเรื่อง</label>
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
            </i>รายการคำร้องขอต่างๆ
           
        </div>
        <div class="card-body"> 
             <table id="tbdata" class="table table-hover">
                <thead>
                <tr>      
                  <th>No.</th>
                  <th>รายการคำร้องขอ</th>
             <th>รหัส</th>
                      <th>ชื่อ-สกุล</th>
                  <th>สถานะ</th>         
                      <th></th>  
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtReq.Rows %>
                <tr>                 
                  <td><% =String.Concat(row("nRow")) %></td>
                  <td><a  href="Request?m=Req&Reqid=<% =String.Concat(row("RegisterID")) %>" ><% =String.Concat(row("TopicName")) %></a></td>    
     <td><% =String.Concat(row("StudentCode")) %></td>
                     <td><% =String.Concat(row("StudentName")) %></td>                
                  <td><% =String.Concat(row("StatusName")) %></td>
                     <td><% If String.Concat(row("StatusFlag")) = "A" Then  %> 
                      <a  href="ReportViewer?R=<% =IIf(String.Concat(row("TopicUID")) = "3", "req", "req") %>&regid=<% =String.Concat(row("RegisterID")) %>&std=<% = Request.Cookies("PersonID").Value.ToString() %>" target="_blank" data-toggle="tooltip" data-placement="top" data-original-title="พิมพ์แบบฟอร์ม"><img src="../images/printer.png" /></a>
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
