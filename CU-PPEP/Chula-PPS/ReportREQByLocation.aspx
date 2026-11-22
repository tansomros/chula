<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportREQByLocation.aspx.vb" Inherits=".ReportREQByLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    
<section class="content-header">
      <h1>รายงานการรับนิสิตแต่ละแหล่งฝึก</h1>   
    </section>

<section class="content">  

         <div class="box box-success">

            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไขรายงาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" >
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" >
                                                        <asp:DropDownList ID="ddlYear" runat="server" 
                                                            CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" >ประเภทแหล่งฝึก :</td>
  <td align="left" >
                                                      <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td colspan="2" align="center" >
      <asp:Button ID="cmdView" runat="server" CssClass="btn btn-warning" Text="ดูรายงาน" />
    </td>
  </tr>
  </table>  
</div>
            <div class="box-footer clearfix">
             <asp:Label ID="lblResult" runat="server" CssClass="validateAlert" Width="95%"></asp:Label>
            </div>
          </div>
      
     </section>
</asp:Content>
