<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="User.aspx.vb" Inherits=".User" %>
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
                                <div>Users
                                    <div class="page-title-subheading">ผู้ใช้งาน </div>
                                </div>
                            </div>
                        </div>
                    </div>            

<section class="content">
    
    <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>รายการผู้ใช้งานทั้งหมด
            <div class="btn-actions-pane-right">
                <a href="UserModify?m=u" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
            </div>
        </div>
        <div class="card-body"> 
                 <table id="tbdata" class="table table-hover">
                <thead>
                <tr>
                 <th class="sorting_asc_disabled"></th>    
                  <th>User ID</th>
                  <th>Display name</th>
                  <th>Username</th> 
                     <th>User Group</th> 
                      <th>Role</th> 
                  <th>Active</th>                    
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtUser.Rows %>
                <tr>
                 <td width="30px"><a  href="UserModify?m=u&uid=<% =String.Concat(row("UserID")) %>" ><img src="images/icon-edit.png"/></a>                    </td>
                  <td><% =String.Concat(row("UserID")) %></td>
                  <td><% =String.Concat(row("NameOfUser")) %>    </td>
                  <td><% =String.Concat(row("Username")) %></td> 
                   <td><% =String.Concat(row("UserGroupName")) %></td> 
                     <td><% =String.Concat(row("RoleName")) %></td> 
                    <td><% =IIf(String.Concat(row("StatusFlag")) = "A", "<img src='images/icon-ok.png'>", "") %> </td>                   
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
