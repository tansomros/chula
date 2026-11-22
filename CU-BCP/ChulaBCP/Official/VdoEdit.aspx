<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="VdoEdit.aspx.vb" Inherits=".VdoEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>จัดการวิดีโอ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">จัดการวิดีโอ</li>
      </ol>
    </section>

<section class="content">
                   
      <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">จัดการวิดีโอเด่น</h3>   
          <div class="box-tools pull-right">
           
          </div>                       
        </div>
        <div class="box-body">
           
    <table  style="width:100%">
       
        <tr>
            <td   style="width:120px">
                <asp:Label ID="Label8" runat="server" Font-Bold="False" 
                    Text="Youtube Code:"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCode" ErrorMessage="**" ForeColor="Red" 
                    ValidationGroup="A">**</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" BackColor="#FFFFCC" Font-Bold="True" 
                    style="font-size: large" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Label ID="Label9" runat="server" Font-Bold="False" 
                    Text="ชื่อเรื่อง:"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtTitle" ErrorMessage="**" ForeColor="Red" 
                    ValidationGroup="A">**</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" BackColor="#FFFFCC" Font-Bold="True" 
                    style="font-size: medium" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  valign="top" class="style7">
                &nbsp;</td>
            <td>
                <asp:CheckBox ID="chkFirstPage" runat="server" ForeColor="Red" Text="ใช้เป็น vdo เริ่มต้น" Checked="True" Visible="False" />
            <br />


              </td>
        </tr>
        <tr>
            <td  valign="top" class="style7">
                &nbsp;</td>
            <td>
       
              <asp:Button ID="bttSave" runat="server" CssClass="buttonlink" 
                    Text="บันทึก" ValidationGroup="D" Width="100px" />
            &nbsp;<asp:Button ID="bttCancel" runat="server" CssClass="buttonlink" 
        Text="ยกเลิก/กลับ" Font-Bold="False" PostBackUrl="~/Default.aspx" />
       

              </td>
        </tr>
    </table>

               </div>
        <!-- /.box-body -->
       
      </div>
      <!-- /.box -->            

      <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการวิดีโอทั้งหมด</h3>   
          <div class="box-tools pull-right">
    &nbsp;</div>
               
       
             
                       
        </div>
        <div class="box-body">


                    <asp:GridView ID="grdVDO" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#34495E" GridLines="Horizontal" Width="99%" BorderStyle="None" CssClass="gridnormal" BorderColor="#CCCCCC" PageSize="20">
                        <RowStyle BackColor="#FFFFFF" VerticalAlign="Top" CssClass="gridrow" />
                        <columns>
                            <asp:BoundField HeaderText="No.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ชื่อเรื่อง">
                                <ItemTemplate>
                                     <a href='https://www.youtube.com/watch?v=<%# Eval("vdoCode") %>' 
                              target="_blank">   
                                    <asp:Label ID="lblVdo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "vdoName") %>'></asp:Label>
                                     </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "vdoCode") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="gridalternatingrow" />
                    </asp:GridView>


              </div>
        <!-- /.box-body -->
       
      </div>
      <!-- /.box -->            

  </section>                 
</asp:Content>
