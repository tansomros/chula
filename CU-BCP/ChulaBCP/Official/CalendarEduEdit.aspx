<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="CalendarEduEdit.aspx.vb" Inherits=".CalendarEduEdit" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1>จัดการปฏิทินการศึกษา
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">จัดการปฏิทินการศึกษา</li>
      </ol>
    </section>

<section class="content"> 
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">จัดการปฏิทินการศึกษา</h3>   
          <div class="box-tools pull-right">   
            
          </div>  
        </div>
        <div class="box-body">             
  
                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Theme="Office2003Olive" 
                    Width="100%" Height="800">
                </dx:ASPxHtmlEditor>  
 
              
             </div>
        <!-- /.box-body -->
        <div class="box-footer">

             
            <span class="panel-title" >
                  <span class="panel-title" style="text-align: right" >
                  <asp:Button  Width="100"                     ID="bttNew" runat="server" Text="บันทึก"                       CssClass="buttonSave" />
                  &nbsp;<asp:Button ID="Button1" runat="server" CssClass="buttonSave"     Width="100"                   PostBackUrl="~/CalendarEdu.aspx" Text="ยกเลิก" />
                  </span>
  
  </span>

             
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    

    </section>
</asp:Content>
