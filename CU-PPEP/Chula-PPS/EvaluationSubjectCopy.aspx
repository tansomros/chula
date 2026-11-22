<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationSubjectCopy.aspx.vb" Inherits=".EvaluationSubjectCopy" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">       
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>Copy การผูกแบบประเมินกับรายวิชา</h1>   
    </section>

<section class="content">       

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">กำหนดเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 

    
     
        
  <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td >ปีต้นแบบ</td>
              <td >
                  <asp:DropDownList ID="ddlYear" runat="server" Width="80px">
                  </asp:DropDownList>
                </td>
            </tr>
            <tr>
              <td  >ปีที่กำหนด/Destination</td>
              <td ><asp:TextBox ID="txtDestinationYear" runat="server" Width="68px" MaxLength="4"></asp:TextBox>              </td>
          
            </tr>
            </table>     
<br /><asp:Label ID="lblNotic" runat="server" CssClass="validateAlert" Text="กรุณาเลือกระบุเงื่อนไขให้ครบถ้วนก่อน" Width="99%"></asp:Label>
          
 
 </div>
            <div class="box-footer clearfix text-left">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100px" Text="บันทึก"></asp:Button>
            </div>
          </div>
                       
    </section>   
</asp:Content>
