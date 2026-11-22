<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_AssignRandom.aspx.vb" Inherits=".PC2_AssignRandom" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
  
  <script> $(function() {
       $( "#dialog-message" ).dialog({
      autoOpen: false,
      height: 600,
      width: 600,
      modal: true,
      buttons: {
        Close: function() {
          $( this ).dialog( "close" );
        }
      },
      close: function() {
        
      }
    });
 
    $( "#user-login" )
      .button()
      .click(function() {
        $( "#dialog-message" ).dialog( "open" );
      });
  });
  </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>คัดเลือกนิสิตสอบ PC2 (Random)</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-random"></i>

              <h3 class="box-title">Randomize</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                  
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
       
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" 
                                                            CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา :</td>
  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
  <td align="left">&nbsp;</td>
</tr>
<tr>
  <td align="left" class="texttopic">&nbsp;</td>
  <td align="left" class="texttopic">
        <asp:Button ID="cmdRandom" runat="server" CssClass="btn btn-pink"    Text="Randomize" />    </td>
  <td align="left">&nbsp;</td>
</tr>
  </table>      </td>
      </tr>
       <tr>
          <td  align="center" valign="top"  >
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>      </td>
      </tr>
       <tr>
         <td  align="center" valign="top"  >

                  <asp:Panel ID="pnSuccess" runat="server" Visible="false">  
            <div class="alert alert-success fade in alert-dismissible">
                <a href="#" class="close" data-dismiss="alert" aria-label="close" title="close">&times;</a>
  <strong>Success! </strong> Randomize Complete.
</div>
    
                     </asp:Panel>  

             
           </td>
       </tr>
       
    </table>
    
                </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

   
    </section>
</asp:Content>
