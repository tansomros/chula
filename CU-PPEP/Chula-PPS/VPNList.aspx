<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VPNList.aspx.vb" Inherits=".VPNList" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1>
        VPN Account
      </h1>
       
    </section>

    <!-- Main content -->
<section class="content">  
    
     <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">VPN Import </h3>             
                 <div class="box-tools pull-right">
               <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body">              
                        <div class="form-control">
                   Import from Excel file    <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />               
                               
                <asp:Button ID="cmdImport" runat="server" CssClass="btn btn-pink" Text="Import" Width="100px" />
           
                 
                </div>
             </div>
           <div class="box-footer clearfix">
               <asp:Label ID="lblResult" runat="server"></asp:Label>
            </div>
          </div>

                          
      <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">VPN Account List</h3>
             
                 <div class="box-tools pull-right">
               <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
            <div class="box-body"> 
                      <dx:ASPxGridView ID="grdVPN" ClientInstanceName="grdVPN" runat="server"  Width="100%"  AutoGenerateColumns="False" KeyFieldName="LocationID"  EnableRowsCache="False" Theme="Office2010Blue" Font-Size="9pt">
        <Columns>  
            <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0" />              
            <dx:GridViewDataColumn FieldName="LocationName" VisibleIndex="1">
                <EditFormSettings  Visible="False" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Username" VisibleIndex="2">
                <EditFormSettings VisibleIndex="0" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Password" VisibleIndex="3">
                <EditFormSettings VisibleIndex="1" />
            </dx:GridViewDataColumn>
            
        </Columns>
                          <SettingsPager PageSize="20">
                          </SettingsPager>
        <SettingsEditing EditFormColumnCount="4" Mode="Inline"/>
        <Settings ShowTitlePanel="true" />
                          <SettingsDataSecurity AllowInsert="False" />
                          <SettingsSearchPanel Visible="True" />       

                          <Styles>
                              <Header HorizontalAlign="Center">
                              </Header>
                          </Styles>

                          <Paddings Padding="0px" />

    </dx:ASPxGridView>
            </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

     
</section>
</asp:Content>
