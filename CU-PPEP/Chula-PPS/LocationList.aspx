<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LocationList.aspx.vb" Inherits=".LocationList" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">        
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    
     <section class="content-header">
      <h1>แหล่งฝึก
          <small></small>              
      </h1>
     
    </section>

<section class="content">                 
 <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-home"></i>

              <h3 class="box-title">รายการแหล่งฝึกทั้งหมด</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
                
<table width="100%" border="0" cellPadding="1" cellSpacing="1">
       <tr>
          <td align="left" valign="top">
              
              <table border="0" cellspacing="2" cellpadding="2">
            <tr>
              <td width="60">ประเภท</td>
              <td >
                  <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" AutoPostBack="True">
                  </asp:DropDownList>
                </td>
              <td width="80" align="right">ชื่อแหล่งฝึก</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox></td>
              <td ><asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="60px" /></td>
                <td>
                    <asp:Button ID="cmdNew" runat="server" CssClass="btn btn-success" Text="เพิ่มแหล่งฝึกใหม่" />
                </td>
            </tr>
            
         </table></td>
      </tr>
        <tr>
          <td>
              
                    <div class="table-responsive">
                        <table id="tbuser" class="table table-hover">
                            <thead>
                                <tr>                                                                 
                                    <th class="text-center">No.</th>
                                    <th class="text-left">Id.</th>
                                    <th class="text-left">Name</th>
                                    <th class="text-left">Province</th>
                                    <th class="text-center">Group</th> 
                                    <th class="text-center">Lang</th>
                                    <th class="text-center">Active</th>
                                    <%   If Request.Cookies("ROLE_ADM").Value = True Then %>
                                    <th class="sorting_asc_disabled sorting_desc_disabled text-center">View</th>
                                    <th class="sorting_asc_disabled sorting_desc_disabled text-center">Edit</th> 
                                   <% End If %>
                                </tr>
                            </thead>
                            <tbody>
                                <% For Each row As DataRow In dtLoc.Rows %>
                                <tr>
                                   
                                    <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                                    <td><% =String.Concat(row("LocationID")) %></td>
                                    <td><% =String.Concat(row("LocationName")) %></td>
                                    <td class="text-left"><% =String.Concat(row("ProvinceName")) %></td>
                                    <td class="text-center"><% =String.Concat(row("LocationGroupName")) %></td>   
                                     
                                    <td class="text-center">
                                      
                                            <% If String.Concat(row("Lang")) = "EN" Then%>
      <img src="images/en.png" width="20" />
<% Else %>
<img src="images/th.png" width="20" />
    <% End If %>                <% =String.Concat(row("Lang")) %>                  
                                     </td> 
                                    <td class="text-center">
                                       <% If String.Concat(row("IsPublic")) = "1" Then%>
                                         <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
                                       <% End If %>
                                    </td> 
                                         
                                                                        <td width="50" class="text-center">
    <a href="LocationDetail.aspx?ActionType=loc&ItemType=l&lid=<% =String.Concat(row("LocationID")) %>" class="text-info" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด" target="_blank"><i class="fa fa-search" aria-hidden="true"></i></a>                                                    
</td>   
                                     <%   If Request.Cookies("ROLE_ADM").Value = True Then %>
                                     <td width="50" class="text-center">
     <a href="Location.aspx?ActionType=loc&ItemType=l&lid=<% =String.Concat(row("LocationID")) %>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข" target="_blank"><i class="fa fa-edit" aria-hidden="true"></i></a>                                                    
 </td>
                                      <% End If %>
                                </tr>
                                <%  Next %>
                            </tbody>
                        </table>
                    </div>
               </td>
      </tr>
</table> 
    </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</section>   
    
</asp:Content>
