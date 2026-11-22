<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EventDetail.aspx.vb" Inherits=".EventDetail" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
     <div class="containerPage">
<section class="content">  
     <section class="content-header">
      <h1 class="news-heading text-green">ปฏิทินกิจกรรม
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ปฏิทินกิจกรรม</li>
      </ol>
    </section>
     
  <div class="row">
<div class="col-lg-9">

        <div class="box box-success">
        <div class="box-header">
          <h2 class="box-title">Event Infomation</h2>
        </div>
        <div class="box-body">              
          <asp:Label ID="lblEventName" runat="server" CssClass="text-blue"></asp:Label> 
          
            <hr />
  

            Time : <asp:Label ID="lblTime" runat="server"></asp:Label><br />
            Venue : <asp:Label ID="lblVenue" runat="server"></asp:Label><br />
            Organizer : <asp:Label ID="lblOrganizer" runat="server"></asp:Label><br />
            Event Category : <asp:Label ID="lblCategoryName" runat="server"></asp:Label>  
             <hr />
            <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
            <br />
  
     

        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->
</div>
        <!-- /.col -->

<div class="col-sm-3">    
    <div align="center">         
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#009999" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="265px">
        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
        <NextPrevStyle Font-Size="8pt" ForeColor="#FFFFFF" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
        <TitleStyle BackColor="#009999" BorderColor="#009999" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
        <WeekendDayStyle BackColor="#CCCCFF" />
    </asp:Calendar>
         </div>     <br />
    
        <div class="box box-success">
        <div class="box-header">
          <h2 class="box-title">Category</h2>
        </div>
        <div class="box-body">              

             <asp:GridView ID="grdCategory" runat="server"  AutoGenerateColumns="False" ShowHeader="False" GridLines="None" >
        <Columns>        
                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='Calendar.aspx?cat=<%# DataBinder.Eval(Container.DataItem, "UID") %>'>
                        <asp:Label ID="Label8" CssClass="text-blue" runat="server" Text='<%# Bind("NameTH") %>'></asp:Label>
                    </a>                     
                </ItemTemplate>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </asp:TemplateField>           
        </Columns>
        <HeaderStyle Height="25px" />     
        
      
    </asp:GridView>

  
     

        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->
</div>
        <!-- /.col -->      

</div>
      <!-- /.row -->  

</section>
         </div>
</asp:Content>