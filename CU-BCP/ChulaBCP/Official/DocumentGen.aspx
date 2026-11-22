<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="DocumentGen.aspx.vb" Inherits=".DocumentGen" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Import Namespace="System.Data" %>
<%@ Register assembly="DevExpress.Web.ASPxRichEdit.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRichEdit" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="fa fa-file-word icon-gradient bg-primary"></i>
                                </div>
            <div class="form-group">
                <asp:Label ID="lblDocumentName" runat="server" Text="เอกสาร"></asp:Label>
                                </div>                                
                            </div>
                        </div> 
                        </div>
<section class="content">     
<div class="row">   
        <div class="box box-primary">
       <div class="box-header">
              <i class="fa fa-edit"></i>
              <h3 class="box-title">สร้างเอกสาร</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                 
            </div>
        <div class="box-body">        
            
      <div class="row" id="divRegisterTopic_Get" runat="server">   
            <div class="col-md-1">
                <div class="form-group">
                  <label>ชั้นปีที่</label>
                    <dx:ASPxSpinEdit ID="txtLevelFrom" CssClass="form-control text-center" runat="server" Number="1" MaxValue="4" MinValue="1" NumberType="Integer" Theme="MetropolisBlue">
                    </dx:ASPxSpinEdit>
                </div>
              </div>
           <div class="col-md-1">
                <div class="form-group">
                  <label>ถึง</label>
                    <dx:ASPxSpinEdit ID="txtLevelTo"  CssClass="form-control text-center"  runat="server" Number="1" MaxValue="4" MinValue="1" NumberType="Integer" Theme="MetropolisBlue">
                    </dx:ASPxSpinEdit>
                </div>
              </div>
             <div class="col-md-2">
                <div class="form-group">
                  <label>ปีการศึกษา</label>
                   <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control text-center"></asp:DropDownList>
                </div>
              </div>
            <div class="col-md-1">
                <div class="form-group">
                  <label>ภาคที่</label>
                      <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control text-center">
   <asp:ListItem Selected="True" Value="1" Text="1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                      </asp:DropDownList> 
                </div>
              </div>
         
            <div class="col-md-2">
                <div class="form-group">
                  <label><asp:Label ID="lblCount" runat="server" Text="จำนวนเงินต่อคน"></asp:Label></label>    
                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control text-center" Text="0"></asp:TextBox>
                </div>
              </div>
            
 </div> 
     <div class="row" id="divRequestTopic_Get" runat="server">               
                  <div class="col-md-2">
                <div class="form-group">
                  <label>วันที่คำร้อง</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtRegDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>
          <div class="col-md-2">
                <div class="form-group">
                  <label>ถึงวันที่</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtRegEndDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>

                  
      
          </div>    
              <div class="row">   
                     <div class="col-md-12">
                <div class="form-group">
                  <label>Work Directory</label>     
                    <asp:Label ID="lblDirectory" runat="server" Text="" CssClass="form-control"></asp:Label>
                </div>
              </div>
                    <div class="col-md-12 text-center"> 
                  <asp:Button ID="cmdCreate" runat="server" CssClass="btn btn-primary" Text="Create" Width="100px" />              
              </div>
                </div>
   </div>
        </div>     
 

</div>
    <div class="row">
      <div class="box box-primary">
      <div class="box-header">
              <i class="fa fa-file-word"></i>

              <h3 class="box-title">Result</h3>
             
                 <div class="box-tools pull-right">
                    
                      <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-success" Text="Export" Width="100px" />                 
              </div>                 
            </div>
        <div class="box-body">
                <div class="row">
                      <div class="col-md-12">
                <div class="form-group">
    <dx:ASPxRichEdit ID="ASPxRichEdit1" runat="server" DataSourceID="SqlDataSource1" ViewMergedData="True" EnableTheming="True" Theme="DevEx" Height="600px" Width="100%" WorkDirectory="~/Document">
                    <SettingsDialogs>
                        <SaveFileDialog DisplaySectionMode="ShowServerSection" />
                    </SettingsDialogs>
                <Settings>
                    <Behavior CreateNew="Disabled" Open="Disabled" Printing="Enabled" />
                    <RangePermissions Visibility="Auto"></RangePermissions>
                </Settings>
             </dx:ASPxRichEdit>  
                </div>
              </div>
                       <div class="col-md-12">
                <div class="form-group">
                  
                </div>
              </div>                   
 </div>
            </div>
        </div>   
</div>
    <div class="row">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
</div>    
</section>     
    <script>
        ASPxRichEdit1.commands.filePrint.execute();
    </script>
</asp:Content>
