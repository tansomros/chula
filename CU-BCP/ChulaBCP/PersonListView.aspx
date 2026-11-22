<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PersonListView.aspx.vb" Inherits=".PersonListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="containerPage">  
 <section class="content-header">
      <h1 class="news-heading text-green"> <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">เกี่ยวกับองค์กร</li>
        <li class="active">
            <asp:Label ID="lblbreadcrumb" runat="server" Text=""></asp:Label></li>
      </ol>
    </section>

    
       <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title">&nbsp;</h2>
          <div class="box-tools pull-right">
            
          </div>
        </div>
        <div class="box-body text-center" align="center">          
              <div class="grid-post grid-template-col-1 grid-template-list-xs grid-gap-post"> 
           
<% For i = 0 To dtBoard.Rows.Count - 1 %>
               <div id="post-b-<% Response.Write(i) %>"  class="grid-post-item">       
       
                    <table align="center">
                        <tr>
                            <td align="center">
                                 <a href='<% Response.Write("PersonDetail.aspx?pid=" & dtBoard.Rows(i)("PersonID")) %>'  target="_blank">
                    <img src="<% Response.Write(dtBoard.Rows(i)("ImagePath")) %>" 
                        class="profile-user-img img-responsive img-circle"  Width="200" Height="200"  />
                    </a>
                            </td>
                             
                        </tr>
                        <tr>
                            <td align="center">
                                <a href='<% Response.Write("PersonDetail.aspx?pid=" & dtBoard.Rows(i)("PersonID")) %>' target="_blank">
                    <% Response.Write(dtBoard.Rows(i)("FullnameTH")) %>
                    </a> 
                    <br /><% Response.Write(dtBoard.Rows(i)("PositionNameTH")) %>               
                            </td>
                            
                        </tr>
                      <tr>
                            <td align="center">&nbsp;</td></tr>   
                    </table>          
       
</div>
       
<% Next %>  

           </div>  

     <br />

             <div class="grid-post grid-template-col-3 grid-template-list-xs grid-gap-post"> 
           
<% For i = 0 To dtStaff.Rows.Count - 1 %>
               <div id="post-s-<% Response.Write(i) %>"  class="grid-post-item">       
       
                    <table align="center">
                        <tr>
                            <td align="center">
                                 <a href='<% Response.Write("PersonDetail.aspx?pid=" & dtStaff.Rows(i)("PersonID")) %>'  target="_blank">
                    <img src="<% Response.Write(dtStaff.Rows(i)("ImagePath")) %>" 
                        class="profile-user-img img-responsive img-circle"  Width="200" Height="200"  />
                    </a>
                            </td>
                             
                        </tr>
                        <tr>
                            <td align="center">
                                <a href='<% Response.Write("PersonDetail.aspx?pid=" & dtStaff.Rows(i)("PersonID")) %>' target="_blank">
                    <% Response.Write(dtStaff.Rows(i)("FullnameTH")) %>
                    </a> 
                    <br /><% Response.Write(dtStaff.Rows(i)("PositionNameTH")) %>               
                            </td>
                            
                        </tr>
                      <tr>
                            <td align="center">&nbsp;</td></tr>   
                    </table>          
       
</div>
       
<% Next %>  

           </div>   
      </div>
</div>


</section>
</asp:Content>
