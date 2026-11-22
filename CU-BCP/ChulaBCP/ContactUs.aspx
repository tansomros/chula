<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ContactUs.aspx.vb" Inherits=".ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <section class="containerPage">   
   <section class="content">     
       <section class="content-header">
       <h1 class="news-heading text-green">ติดต่อสอบถาม
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ติดต่อสอบถาม</li>
      </ol>
    </section> 
    
               <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title"></h2>
          <div class="box-tools pull-right">
               <asp:Button ID="bttEdit" runat="server" CssClass="buttonOrange"   Text="ปรับปรุง" PostBackUrl="~/Official/AboutUpdate.aspx?Code=th&p=ContactUs" />
           
          </div>
        </div>
        <div class="box-body">   
  
     
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblThai" runat="server" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
              <h1 class="text-green"><i class="fa fa-location-arrow" style="color: #666;"></i> แผนที่</h1>
                <br />
                <asp:Label ID="lblMaps" runat="server" ForeColor="Black" Width="100%"></asp:Label>
            </td>
        </tr>
      
    </table>
  
        
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
         
        </div>
        <!-- /.box-footer-->
      </div>
  </section>       
  </section>                  
</asp:Content>
