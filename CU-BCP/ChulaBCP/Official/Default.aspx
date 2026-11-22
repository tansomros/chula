<%@ Page Language="vb" Title="Home Dashboard" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Home"  MasterPageFile="OfficialSite.Master" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
       <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <img src="images/logochula.png" width="50" /> 
                                </div>
                                <div>Dashboard
                                    <div class="page-title-subheading">ระบบฐานข้อมูลหลักสูตรเภสัชกรประจำบ้าน</div>
                                </div>
                            </div>
                        </div>
                    </div>
    <section class="content">       
     
      <!-- Main row -->
      <div class="row">
        <!-- Left col -->
        <section class="col-lg-12">     
             
          <div class="main-card mb-3 card">
            <div class="card-header">
              <i class="header-icon lnr-graduation-hat icon-gradient bg-happy-itmeo"></i>
              Control Panel
            </div>
            <div class="card-body">
             
                <asp:DataList ID="grdControlPanel" runat="server" RepeatColumns="3">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width:70px"><a href='<%# Eval("ControlPage") %>'>
                                    <asp:Image ID="img" runat="server" Height="64px" ImageUrl='<%# Eval("ControlImage") %>' ToolTip='<%# Eval("ControlDescription") %>' Width="64px" />
                                    </a></td>
                                <td style="text-align: left; vertical-align:middle; width:400px;"><a href='<%# Eval("ControlPage") %>'><b>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("ControlName") %>'></asp:Label>
                                    </b></a>
                     <br />
                                    <asp:Label ID="Label1" runat="server" Font-Italic="True"  Text='<%# Eval("ControlDescription") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
             
            </div>
            <!-- /.chat -->
            <div class="card-footer">
          
            </div>
          </div>
            <asp:Panel ID="pnUserOnline" runat="server">
  <div class="main-card mb-3 card">
            <div class="card-header">
              <i class="header-icon lnr-users icon-gradient bg-happy-itmeo "></i> User Online                  
            </div>
            <div class="card-body">
            <asp:Label ID="lblOnline" runat="server" Width="99%"></asp:Label>
            </div>
            <div class="card-footer clearfix">
           
            </div>
          </div>
      </asp:Panel>

        </section>
        <!-- /.Left col -->
      
      </div>
      <!-- /.row (main row) -->

    </section>
    <!-- /.content -->

  <!-- /.content-wrapper -->
  <div class="control-sidebar-bg"></div>


</asp:Content>