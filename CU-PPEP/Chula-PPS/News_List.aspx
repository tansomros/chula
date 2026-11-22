<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="News_List.aspx.vb" Inherits=".News_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="content-header">
      <h1>ข่าวประกาศ</h1>   
    </section>

<section class="content"> 
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการข่าวประกาศ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages">  
 <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="NewsID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="หัวข้อข่าว">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlnkNews" runat="server" Target="_blank">[hlnkNews]</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="หน้าแรก">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem,"IsPublic") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# Container.DataItemIndex %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# Container.DataItemIndex %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView> 
      
       
              
</div>
            <div class="box-footer text-center">
             <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
            </div>
          </div>
                       
    </section>    
</asp:Content>
