<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Download.aspx.vb" Inherits=".Download" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="containerPage">
<section class="content">  
     <section class="content-header">
      <h1 class="news-heading text-green">ดาวน์โหลด
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ดาวน์โหลด</li>
      </ol>
    </section>
 
              <div class="box box-default">
            <div class="box-header">
              <i class="fa fa-cloud-download"></i>

              <h3 class="box-title">เอกสารดาวน์โหลด</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
              </div>                                 
            </div>
            <div class="box-body"> 
           <!-- Custom Tabs -->
          <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_4a" data-toggle="tab">สำหรับเภสัชกรประจำบ้าน</a></li>
              <li><a href="#tab_4b" data-toggle="tab">สำหรับบุคคลทั่วไป</a></li>
              <li><a href="#tab_4c" data-toggle="tab">เอกสารอื่น ๆ</a></li>           
            </ul>
            <div class="tab-content">
              <div class="tab-pane active" id="tab_4a">                  
              <table id="tbdata" class="table table-bordered table-striped">               
                <tbody>
            <% For Each row As DataRow In dtDow.Rows %>
                <tr>
                 <td class="text-center" width="30px">                      
                     <a href="documents/<% =String.Concat(row("filepath")) %>" ><img src="images/icon/<% =String.Concat(row("fileicon")) %>"/></a>            
                    </td>
                  <td> <a href="documents/<% =String.Concat(row("filepath")) %>" target="_blank" ><% =String.Concat(row("Descriptions")) %></a></td>
                </tr>
            <%  Next %>
                </tbody>               
              </table>    
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_4b">


              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_4c">


              </div>
              <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->
        </div>
        <!-- /.col -->       
      
          </div>
  </section>                  
</div>
</asp:Content>
