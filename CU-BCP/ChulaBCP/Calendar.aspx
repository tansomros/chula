<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Calendar.aspx.vb" Inherits=".Calendar" %>
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
<div class="col-sm-4">                
    <dx:ASPxCalendar ID="ASPxCalendar1" runat="server" Theme="Material" AutoPostBack="true"></dx:ASPxCalendar>
           <br />
</div>
        <!-- /.col -->      
<div class="col-lg-8">

        <div class="box box-success">
        <div class="box-header">
          <h2 class="box-title"><asp:Label ID="lblEventHeader" runat="server"></asp:Label></h2>
        </div>
        <div class="box-body">              
            <asp:Label ID="lblAlbumTitle" runat="server"></asp:Label>
            <br />
             <asp:GridView ID="grdData" runat="server"  AutoGenerateColumns="False"   Width="98%" Font-Bold="False" PageSize="20" ShowHeader="False" BorderStyle="None" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="4" ForeColor="Black" CssClass="table table-hover" >
        
        <Columns>       

            <asp:BoundField DataField="EventDate" HeaderText="Date">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
            </asp:BoundField>
            <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>                   
                    <a href='EventDetail.aspx?uid=<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                        target="_blank">
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EventName") %>'></asp:Label><br />
                    </a>
                    Venue: <asp:Label ID="Label7" runat="server" Text='<%# Bind("Venue") %>'></asp:Label><br />
                    Event Category:
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryNameTH") %>'></asp:Label>
                  
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            </asp:TemplateField>
           
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle Font-Bold="True" Height="25px" BackColor="#333333" BorderStyle="None" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />       
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
      
        
      
                 <SortedAscendingCellStyle BackColor="#F7F7F7" />
                 <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                 <SortedDescendingCellStyle BackColor="#E5E5E5" />
                 <SortedDescendingHeaderStyle BackColor="#242121" />
      
        
      
    </asp:GridView>
  

            <br />
  
     

        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->
</div>
        <!-- /.col -->


</div>
     

</section>
</div>
</asp:Content>