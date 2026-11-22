<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="MediaAdd.aspx.vb" Inherits="MediaAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>Media Upload
        <small>อัพโหลดสื่อการสอน</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">อัพโหลดสื่อการสอน</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">อัพโหลดสื่อการสอน</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
           
          </div>
        </div>
        <div class="box-body">
                  <table align="center" cellpadding="2" cellspacing="4"  class="table50" >         
          <tr>
              <td width="100">Ref. ID</td>
              <td>
                  <asp:Label ID="lblUID" runat="server"></asp:Label>
         
              </td>
          </tr>
          <tr>
              <td>สาขาวิชา</td>
              <td>
                  <asp:DropDownList ID="ddlMajor" runat="server" CssClass="Optioncontrol">
                  </asp:DropDownList>
         
              </td>
          </tr>
          <tr>
              <td>รหัสวิชา</td>
              <td>
                  <asp:TextBox ID="txtSubjectCode" runat="server" Width="80%" MaxLength="10"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
              <td>ชื่อวิชา</td>
              <td>
                  <asp:TextBox ID="txtSubjectName" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
              <td>ชื่อผู้สอน</td>
              <td>
                  <asp:TextBox ID="txtInstructor" runat="server" MaxLength="100" Width="80%"></asp:TextBox>
         
              </td>
          </tr>
          <tr>
            <td valign="top">ชื่อเรื่อง</td>
            <td><asp:TextBox ID="txtDescriptions" runat="server" Width="80%" MaxLength="500"></asp:TextBox></td>
          </tr>
          <tr>
            <td>Link สื่อ</td>
            <td>
                <asp:TextBox ID="txtLink" runat="server" Width="100%" MaxLength="1000"></asp:TextBox>
              </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td class="textnotic">
                <asp:HyperLink ID="hlnkDoc" runat="server" Target="_blank">[hlnkDoc]</asp:HyperLink>
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
        
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->   

          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />

  </section>                  



</asp:Content>
