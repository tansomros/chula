<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="WorkOutSide_Manage.aspx.vb" Inherits="WorkOutSide_Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>การปฏิบัติงานนอก มทส.
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">การปฏิบัติงานนอก มทส.</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">เพิ่ม/แก้ไข</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
                  <table align="center"  class="table">         
          <tr>
              <td width="150">รหัส</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
          <tr>
            <td>บุคลากร</td>
            <td>
                <asp:DropDownList ID="ddlPerson" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <td>งานด้าน</td>
            <td>
                <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <td>หมวดการอบรม</td>
            <td>
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <td>วันที่เริ่ม</td>
            <td align="left">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        </td>
                        <td>วันสิ้นสุด</td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  runat="server" ControlToValidate="txtStartDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  runat="server" ControlToValidate="txtEndDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    
                </table>
              </td>
          </tr>
          <tr>
            <td>ผลงานนำเสนอ</td>
            <td>
                <asp:RadioButtonList ID="optIsPresented" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="Y">มีผลงานนำเสนอ</asp:ListItem>
                    <asp:ListItem Value="N">ไม่มีผลงานนำเสนอ</asp:ListItem>
                </asp:RadioButtonList>
              </td>
          </tr>
          <tr>
            <td>ชนิดผลงาน</td>
            <td>
                <asp:TextBox ID="txtPresentType" runat="server" Width="50%"></asp:TextBox>
              </td>
          </tr>
          <tr>
            <td>ชื่อผลงาน</td>
            <td><asp:TextBox ID="txtPortfolioName" runat="server" Width="100%"></asp:TextBox></td>
          </tr>
          <tr>
            <td>ชื่องานประชุม/อบรม</td>
            <td>
          <asp:TextBox ID="txtSeminarName" runat="server" Width="100%"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
            <td>ประเทศ</td>
            <td>
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
         
              </td>
          </tr>
          <tr>
            <td>งบประมาณ</td>
            <td>
                <asp:DropDownList ID="ddlBudget" runat="server" CssClass="Optioncontrol">
                </asp:DropDownList>
         
              </td>
          </tr>
          <tr>
            <td>Active</td>
            <td>
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active / ใช้งาน" />
              </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="cmdSave" runat="server" CssClass="buttonlink" Text="บันทึก" Width="100px" />
              &nbsp;<asp:Button ID="cmdCancel" runat="server" CssClass="buttonlink" Text="ยกเลิก" Width="100px" />
              </td>
          </tr>
                      </table>

 </div>
        <!-- /.box-body -->
        <div class="box-footer">
         <asp:Panel ID="pnAlert" runat="server">
                  <div class="alert alert-danger alert-dismissible">
                
                <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    <p><asp:Label ID="lblAlert" runat="server"></asp:Label></p>
              </div>
                   </asp:Panel>
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->        

  </section>                  

</asp:Content>
