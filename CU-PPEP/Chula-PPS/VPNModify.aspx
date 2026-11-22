<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VPNModify.aspx.vb" Inherits=".VPNModify" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1>
        VPN Modify
      </h1>
       
    </section>

    <!-- Main content -->
<section class="content">  
   
     <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">VPN Update</h3>             
                 <div class="box-tools pull-right">
               <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">              
                        <div class="form-control">
                    แหล่งฝึก : 
                    <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                    <br />
                    Username : 
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <br />
                    Password : 
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    <br />
                    Status :
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" />
                            <asp:HiddenField ID="hdLocationID" runat="server" />
                </div>
             </div>
            <div class="box-footer clearfix">
           
                <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="Save" Width="100px" />
           
                <asp:Button ID="cmdBack" runat="server"  CssClass="btn btn-pink" Text="<<Back" />
           
            </div>
          </div>

     
</section>
</asp:Content>
