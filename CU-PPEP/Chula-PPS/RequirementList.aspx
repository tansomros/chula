<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="RequirementList.aspx.vb" Inherits=".RequirementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    </asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <section class="content-header">
      <h1>ความต้องการส่งนิสิตฝึกปฏิบัติงานฯ</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                 <table>
            <tr>
              <td align="left" >ปี</td>
<td align="left" >
    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2"></asp:DropDownList>                </td>           
             </tr>
                     <tr>   <td align="left" >ค้นหาชื่อแหล่งฝึก</td>
              
<td align="left" ><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>                  </td>
            </tr>
            <tr>
              <td align="center" >&nbsp;</td>
              <td align="left" >    <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button></td>
            </tr>
            

         </table>
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div> 
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการความต้องการที่ร้องขอแต่ละแหล่งฝึก</h3>
             
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
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="LocationID" PageSize="20">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="nRow" HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภทแหล่งฝึก" />
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                     CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                     CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
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
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>   
</asp:Content>
