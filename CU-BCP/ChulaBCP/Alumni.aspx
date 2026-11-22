<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Site.Master" CodeBehind="Alumni.aspx.vb" Inherits=".Alumni" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
 
     <section class="containerPage">  
 <section class="content-header">
      <h1 class="news-heading text-green"> ศิษย์เก่า
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">เกี่ยวกับองค์กร</li> <li class="active">ศิษย์เก่า</li>
      </ol>
    </section>         

    <div class="box box-default">
        <div class="box-header">        
          <div class="box-tools pull-right">            
          </div>
        </div>
        <div class="box-body">
            <table id="tbdata" class="table table-hover">              
                <tbody>
            <% For Each row As DataRow In dtAlumni.Rows %>
                <tr>
                 <td Width="120" ><img src="<% =String.Concat(row("PicturePath")) %>" class="profile-user-img img-responsive"  Width="100" />
                    </td>
                  <td><% =String.Concat(row("StudentCode")) %> <br />
                      <% =String.Concat(row("StudentName")) %> <br />
                     สาขา : <% =String.Concat(row("MajorName")) %><br />
                     E-mail : <% =String.Concat(row("Email")) %> <br />
                     สถานที่ทำงานปัจจุบัน : <% =String.Concat(row("Workplace")) %> 
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
