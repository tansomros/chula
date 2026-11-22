<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Event_Manage.aspx.vb" Inherits="Event_Manage" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Event Management
                                    <div class="page-title-subheading">จัดการข้อมูลปฏิทินกิจกรรม</div>
                                </div>
                            </div>
                        </div>
                    </div>      

<section class="content">       
        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">จัดการปฏิทินกิจกรรม</h2>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">

               <div class="row">            

           <div class="col-md-1">
          <div class="form-group">
            <label>ID</label>
              <asp:Label ID="lblUID" runat="server" CssClass="form-control"></asp:Label> 
          </div>
        </div>
      
                   <div class="col-md-3">
          <div class="form-group">
            <label>Category</label>
              <asp:DropDownList ID="ddlCategory" runat="server"  CssClass="form-control select2">
                </asp:DropDownList> 
          </div>
        </div>                  
                   <div class="col-md-2">
          <div class="form-group">
                    <label>Start Date</label>
              <asp:TextBox ID="txtStartDate" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    
                   <div class="col-md-2">
          <div class="form-group">
                    <label>End Date</label>
              <asp:TextBox ID="txtEndDate" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    
                     <div class="col-md-2">
          <div class="form-group">
                    <label>Start Time</label>
                  <table>
                    <tr>
                        <td>
                <asp:DropDownList ID="ddlBHour" runat="server" CssClass="form-control select2">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                </asp:DropDownList>
                        </td>
                        <td>&nbsp;:&nbsp;</td>
                        <td><asp:DropDownList ID="ddlBMinus" runat="server" CssClass="form-control select2">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                </asp:DropDownList></td>
                    </tr>
                    
                </table>

          </div>
        </div>    
                     <div class="col-md-2">
          <div class="form-group">
                    <label>End Time</label>
              
                  <table>
                    <tr>
                        <td>
                             <asp:DropDownList ID="ddlEHour" runat="server" CssClass="form-control select2">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                </asp:DropDownList>
                        </td>
                        <td>&nbsp;:&nbsp;</td>
                        <td><asp:DropDownList ID="ddlEMinus" runat="server" CssClass="form-control select2">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                </asp:DropDownList></td>
                        
                    </tr>
                    
                </table>
          </div>
        </div>    



                    <div class="col-md-12">
          <div class="form-group">
                    <label>Event Name</label>
              <asp:TextBox ID="txtEventName" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    

                    <div class="col-md-12">
          <div class="form-group">
                    <label>สถานที่จัด</label>
              <asp:TextBox ID="txtVenue" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    

                   <div class="col-md-12">
          <div class="form-group">
                    <label>Organizer/ผู้จัด</label>
              <asp:TextBox ID="txtOrganizer" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    

                   <div class="col-md-12">
          <div class="form-group">
                    <label>Detail</label>
                <dx:ASPxHtmlEditor ID="txtDetail" runat="server" Theme="Default" Width="99%">
                     <SettingsDialogs>
                                                <InsertImageDialog>
                                                    <SettingsImageUpload>
                                                        <FileSystemSettings UploadFolder="~/images/events" />
                                                    </SettingsImageUpload>                                                   
                                                </InsertImageDialog>
                    </SettingsDialogs>
                </dx:ASPxHtmlEditor>
          </div>
        </div>    



</div>  


             
                <table>
                    <tr>
                        
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  runat="server" ControlToValidate="txtStartDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  runat="server" ControlToValidate="txtEndDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    
                </table> 

            


 </div>


        <div class="box-footer text-center">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="บันทึก" Width="100px" />
              &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-danger" Text="ยกเลิก" Width="100px" />
         <asp:Panel ID="pnAlert" runat="server">
                  <div class="alert alert-danger alert-dismissible">
                
                <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    <p><asp:Label ID="lblAlert" runat="server"></asp:Label></p>
              </div>
                   </asp:Panel>
        </div> 
      </div>    

  </section>                  

</asp:Content>
