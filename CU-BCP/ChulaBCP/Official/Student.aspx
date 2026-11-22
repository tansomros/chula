<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Student.aspx.vb" Inherits=".Student" %>
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
                                <div>Student
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
                 <th class="sorting_asc_disabled"></th>    
                  <th>รหัส</th>
                  <th>คำนำหน้าชื่อ</th>
                  <th>ชื่อ-นามสกุล</th> 
                 <th>สถานภาพ</th> 
                  <th>Active</th>                    
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtStd.Rows %>
                <tr>
                 <td><a  href="StudentReg?m=pm&std=<% =String.Concat(row("StudentID")) %>" ><img src="images/icon-edit.png"/></a>
                     <a  href="StudentBio?m=pm&std=<% =String.Concat(row("StudentID")) %>" ><img src="images/view.png"/></a>
                    </td>
                  <td><% =String.Concat(row("StudentCode")) %></td>
                  <td><% =String.Concat(row("PrefixName")) %>    </td>
                  <td><% =String.Concat(row("StudentName")) %></td> 
                     <td><% =String.Concat(row("StudentStatusName")) %></td> 
                    <td><% =IIf(String.Concat(row("StatusFlag")) = "A", "<img src='images/icon-ok.png'>", "") %> </td>                   
                </tr>
            <%  Next %>
                </tbody>               
              </table>         
                                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-upload"></i>

              <h3 class="box-title">Import from Excel file</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

                              <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-condensed">
       <tr>
         <td height="30">
            <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Button ID="cmdImport" runat="server" CssClass="btn btn-success" Text="import" Width="100px" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels" Width="100%"></asp:Label>           </td>
         </tr>
     </table>             
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                        
    </section>
</asp:Content>
