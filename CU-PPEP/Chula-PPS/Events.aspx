<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Events.aspx.vb" Inherits=".Events" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <section class="content-header">
      <h1>
        Event Calendar
        <small>ปฏิทินกิจกรรม</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ปฏิทินกิจกรรม</li>
      </ol>
    </section>
    
    <!-- Main content -->
    <section class="content">
      <!-- /.row -->
      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
        <section class="col-lg-12 connectedSortable">   
            
            
        <div class="row no-print">
        <div class="col-xs-12">
            <asp:HyperLink ID="hlkManage" CssClass="btn btn-primary pull-right" runat="server" NavigateUrl="EventModify?ActionType=news&ItemType=evmod"><i class="fa fa-plus-circle"></i> จัดการปฏิทินกิจกรรม</asp:HyperLink>    
        </div>
      </div>
        <br />

           <!-- Custom Tabs -->
          <div class="nav-tabs-custom">


            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_4a" data-toggle="tab">Calendar</a></li>
              <li><a href="#tab_4b" data-toggle="tab">List</a></li>           
            </ul>
            <div class="tab-content">
              <div class="tab-pane active" id="tab_4a">
<!-- THE CALENDAR -->
              <div id="calendar"></div>
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_4b">
               <asp:GridView ID="grdMedia4b" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="UID" CssClass="table table-hover" ShowHeader="False">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
                <asp:TemplateField HeaderText="ชื่อกิจกรรม">
                    <ItemTemplate>                       
                            <asp:Image ID="imgNews" runat="server"  Width="20px" /> 
                                &nbsp;<asp:HyperLink ID="hlnkMedia4b" runat="server" Target="_blank"></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle  HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
              </div>
              <!-- /.tab-pane -->
         
            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->                 

        </section>
        <!-- /.Left col --> 
        
      </div>
      <!-- /.row (main row) -->

    </section>
    <!-- /.content -->

  <!-- /.content-wrapper -->
  <div class="control-sidebar-bg"></div>

</asp:Content>
