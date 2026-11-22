<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DataConfig.aspx.vb"
  Inherits=".DataConfig" %>
<%@ Import Namespace="System.Data" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
      <title> </title>
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-config icon-gradient bg-mean-fruit"></i>
            </div>
            <div>Data Configuration Controls<div class="page-title-subheading">ตั้งค่าเริ่มต้นระบบ</div></div>
        </div>
   </div>
</div>
<section class="content">
    <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>รายการค่าเริ่มต้นระบบ
            <div class="btn-actions-pane-right">              
            </div>
        </div>
        <div class="card-body"> 
                 <table id="tbdata" class="table table-hover">
                <thead>
                <tr>
                 <th class="sorting_asc_disabled"></th>    
                 <th>No.</th> 
                 <th>Code</th>
                 <th>Name</th>
                 <th>Description</th> 
                 <th>Value</th>                                      
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtConfig.Rows %>
                <tr>
                 <td width="100px" class="text-center">
                    <a href="DataConfigEdit?m=sys&uid=<% =String.Concat(row("UID")) %>" class="btn btn-success"><i class="fa fa-edit"></i> Edit</a></td>
                  <td><% =String.Concat(row("nRow")) %></td>
                  <td><% =String.Concat(row("CodeConfig")) %>    </td>
                  <td><% =String.Concat(row("Name")) %></td> 
                  <td><% =String.Concat(row("Description")) %></td> 
                  <td><% =String.Concat(row("ValueConfig")) %> </td>                   
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