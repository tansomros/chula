<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="COVIDList.aspx.vb" Inherits=".COVIDList" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>รายการรายงานความเสี่ยงต่อการสัมผัสโรค COVID-19</h1>   
    </section>

<section class="content">
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายการคัดกรอง COVID-19</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
 <table width="100%" border="0" cellspacing="2" cellpadding="0">    
       <tr>
          <td  align="left" valign="top">
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
              <td >
                   <asp:Button ID="cmdFind" runat="server" CssClass="buttonFind" Width="70" Text="ค้นหา"></asp:Button>  
                &nbsp;<asp:Button ID="cmdNew" runat="server" CssClass="btn-success" Height="30px" Text="เพิ่มการคัดกรอง" />
                </td>
            </tr>
            <tr>
                  <td colspan="3" class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก รหัสนิสิต , ชื่อ หรือ  นามสกุล</td>
              </tr>
          </table>
          </td>
      </tr>
   <tr>
     <td>
         <asp:GridView ID="grdData"  runat="server" CellPadding="0" 
                                                        GridLines="None" 
             AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False" Width="100%" AutoGenerateColumns="False" 
             AllowSorting="True" PageSize="20">
                        <RowStyle BackColor="#F7F7F7" />
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            HorizontalAlign="Center" />                     
                        <Columns>
                            <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต" >
                            <ItemStyle Width="90px" HorizontalAlign="Center" />                            </asp:BoundField>
                            <asp:BoundField DataField="StudentName" HeaderText="ชื่อนิสิต" > 
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดที่" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/view.png" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                                        ImageUrl="images/icon-edit.png" />
                                    &nbsp;
                                    <asp:ImageButton ID="imgDel" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                                        ImageUrl="images/icon-delete.png" />                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView></td>
    </tr>
 </table>
                                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
</asp:Content>
