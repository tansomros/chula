<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Programs.aspx.vb" Inherits=".Programs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="containerPage">
<section class="content">  
     <section class="content-header">
      <h1 class="news-heading text-green">แนะนำหลักสูตร
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">แนะนำหลักสูตร</li>
      </ol>
    </section>
  
  <div class="box box-default">
        <div class="box-header with-border">
        

          <div class="box-tools pull-right">
               <asp:Button ID="bttEdit" runat="server" CssClass="buttonOrange"   Text="ปรับปรุง" PostBackUrl="~/Official/AboutUpdate.aspx?Code=th&p=History" />
           
          </div>
        </div>
        <div class="box-body">   
  
    <asp:Label ID="lblDetail" runat="server" Width="100%"></asp:Label>
        
   
        
        </div>
        <!-- /.box-body -->
        <div class="box-footer small">
          <img alt="" src="images/i/view.png" />
    <asp:Label ID="lblUpdate" runat="server"></asp:Label>
&nbsp;<asp:Label ID="lblRead" runat="server"></asp:Label>
        </div>
        <!-- /.box-footer-->
      </div>
         
  </section>                  
</div>
</asp:Content>
