<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LocationZone.aspx.vb" Inherits=".LocationZone" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    </asp:Content>    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
   
    <section class="content-header">
      <h1>โซน
          <small>กลุ่มมหาวิทยาลัยที่ดูแล</small>              
      </h1>
     
    </section>

<section class="content">            
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-info-circle"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข โซน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">
   
            
<table cellSpacing="1" cellPadding="1" border="0" width="99%">
                                                <tr>
                                                    <td align="left" width="40">
                                                        ID : 
                                                        </td>
                                                    <td align="left" >
                                                        <asp:TextBox ID="txtCode" runat="server" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        ชื่อ :</td>
                                                    <td align="left" class="texttopic"><asp:TextBox ID="txtName" runat="server" Width="60%" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="texttopic">&nbsp;                                                    </td>
                                                    <td align="left" class="texttopic">
													    <span >
                                          <asp:Button ID="cmdSave" runat="server" Text="บันทึก" CssClass="buttonSave" Width="80px"></asp:Button>
                                          <asp:Button ID="cmdClear" runat="server" Text="ยกเลิก" CssClass="buttonDisable" Width="80px"></asp:Button>
                                        </span>                                                    </td>
                                                </tr>
                                               
  </table>        
  
         </div>
      
          </div>
                
 <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-info-circle"></i>

              <h3 class="box-title">รายการโซนทั้งหมด</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>

                 
            </div>
            <div class="box-body">    
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="99%" AllowPaging="True" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="nRow"  HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                      </asp:BoundField>
            <asp:BoundField DataField="ZoneID" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                      </asp:BoundField>
            <asp:BoundField DataField="ZoneName" HeaderText="โซน">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
            <asp:TemplateField HeaderText="แก้ไข">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ZoneID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ZoneID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
 </div>
           
          </div>

</section>
</asp:Content>
