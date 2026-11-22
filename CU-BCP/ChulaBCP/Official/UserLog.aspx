<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="UserLog.aspx.vb" Inherits="UserLog" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
      <link href="css/DragAndDrop.css" rel="stylesheet" type="text/css" /> 
    
<script language="javascript"  type="text/javascript">
        
        function onUploadControlFileUploadComplete(s, e) {
            if (e.isValid){}
                //document.getElementById("uploadedImage").src = "dist/img/" + e.callbackData;
            //setElementVisible("uploadedImage", e.isValid);
        }
       
       

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>รายละเอียดการใช้งานระบบของผู้ดูแลระบบ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">User Log</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-primary">
        <div class="box-header with-border">
          <h3 class="box-title">เงื่อนไข</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">

               <table border="0" align="center" cellpadding="0" cellspacing="2" class="table50">
      <tr>
        <td width="100">เลือกผู้ใช้ </td>
<td>
                                               
                                               <asp:DropDownList ID="ddlUserID" 
                runat="server" CssClass="Objcontrol" > </asp:DropDownList>
                                             </td>
        <td>
                                              <img src="images/arrow-orange-icons.png" width="14" height="14" /><asp:TextBox 
                                                  ID="txtFind" runat="server" Width="60px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkFind" runat="server" CssClass="buttonSmall">ค้นหา</asp:LinkButton>                                              </td>
      </tr>
      <tr>
        <td>วันที่ตั้งแต่</td>
        <td>
            <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
          </td>
        <td>รูปแบบวันที่ </td>
      </tr>
      <tr>
        <td>ถึง</td>
        <td>
            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
          </td>
        <td>dd/mm/yyyy ปี พ.ศ.</td>
      </tr>
      <tr>
        <td colspan="3">
                 <asp:Panel ID="pnAlert" runat="server">
                  <div class="alert alert-danger alert-dismissible">
                
                <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    <p>  <asp:Label ID="lblAlert" runat="server" Text="Label"></asp:Label></p>
              </div>

                   </asp:Panel>
            </td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>
                <asp:Button ID="cmdSearch" runat="server" CssClass="buttonlink" Text="ค้นหา" Width="70px" />               
          </td>
        <td>&nbsp;</td>
      </tr>
      </table>


 </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->            

                 
     <div class="box box-primary">
        <div class="box-header with-border">
          <h3 class="box-title">รายการใช้งานระบบทั้งหมด</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
                 
            <asp:GridView ID="grdData" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="0"  Font-Bold="False" GridLines="None" PageSize="20" Width="100%">
                <RowStyle BackColor="#ffffff" />
                <columns>
                    <asp:BoundField DataField="LogID" HeaderText="Log ID">
                    <ItemStyle Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Username" HeaderText="Username">
                    </asp:BoundField>
                    <asp:BoundField DataField="LogDate" HeaderText="วันที่" />
                    <asp:BoundField DataField="ActionType" HeaderText="ประเภท" />
                    <asp:BoundField DataField="Description" HeaderText="คำอธิบาย" />
                    <asp:BoundField DataField="Remark" HeaderText="หมายเหตุ">
                    </asp:BoundField>
                </columns>
                <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <headerstyle   Font-Bold="True" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#f7f7f7" />
            </asp:GridView>
                         
                                
                    <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ไม่พบข้อมูล" Width="100%"></asp:Label>
    </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->            

                        
     <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">User Online</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">  
            <asp:Label ID="lblOnline" runat="server" Width="99%"></asp:Label>
    </div>
      </div>
      <!-- /.box -->         

  </section>                  

</asp:Content>
