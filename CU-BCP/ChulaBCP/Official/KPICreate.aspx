<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="KPICreate.aspx.vb" Inherits=".KPICreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>KPI Setup
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">KPI Setup</li>
      </ol>
    </section>

      <section class="content">

        <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">สร้างชุดข้อมูลพื้นฐาน (Common data set)</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                    title="Collapse">
              <i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove">
              <i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">   
      <table>
          <tr>
              <td class="style6">
                  <asp:Label ID="Label3" runat="server" Text="ปีการศึกษา:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="TextBox1" runat="server" Width="90px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="**" 
                      ForeColor="Red" ValidationGroup="kpi"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td class="style6">
                  <asp:Label ID="Label4" runat="server" Text="ปีการศึกษา และคำอธิบาย:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                      ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="**" 
                      ForeColor="Red" ValidationGroup="kpi"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td class="style6">
                  &nbsp;</td>
              <td>
                  <span class="panel-title" >
                  <span class="panel-title" style="text-align: right" >
                  <asp:Button 
                      ID="bttNew" runat="server" Text="สร้าง" CssClass="buttonSave" 
                      ValidationGroup="kpi" />
                  <asp:Button 
                      ID="cmdCancel" runat="server" Text="ยกเลิก" CssClass="buttonSmall" PostBackUrl="~/KPIHome.aspx" Font-Bold="False" />
                  </span>
  
                  <asp:Label ID="lblErr" runat="server" BackColor="Red" Font-Italic="True" 
                      Font-Size="Small" ForeColor="White"></asp:Label>
  
  </span></td>
          </tr>
          <tr>
              <td class="style5" colspan="2">
              <hr />
              รายการทั้งหมด
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                      CellPadding="4" DataKeyNames="year_code" DataSourceID="SqlDataSource1" 
                      ForeColor="#333333" GridLines="None">
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                      <asp:TemplateField>
                      <ItemTemplate>
                       <asp:LinkButton ID="lnkRemove" runat="server" 
                          CommandArgument='<%# Eval("year_code")%>' OnClick="DeleteItem" 
                          OnClientClick="return confirm('ต้องการลบข้อมูลของรายการนี้ ใช่หรือไม่?')" Text="ลบ" CssClass="buttonRed" ForeColor="White"></asp:LinkButton>
                      </ItemTemplate>
                        
                      </asp:TemplateField>
                     

                          <asp:BoundField DataField="year_code" HeaderText="ปีการศึกษา" ReadOnly="True" 
                              SortExpression="year_code" />
                          <asp:BoundField DataField="year_name" HeaderText="ปีการศึกษาและคำอธิบาย" 
                              SortExpression="year_name" />
                      </Columns>
                      <EditRowStyle BackColor="#7C6F57" />
                      <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                      <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                      <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                      <RowStyle BackColor="#E3EAEB" />
                   
                    
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:strConn %>" 
                      SelectCommand="SELECT [year_code], [year_name] FROM [qa_year_code] ORDER BY [year_code]">
                  </asp:SqlDataSource>
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

</section>
</asp:Content>
