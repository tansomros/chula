<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationForm.aspx.vb" Inherits=".EvaluationForm" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<section class="content-header">
      <h1>สร้างแบบฟอร์มประเมิน</h1>   
    </section>

<section class="content">  

     <div class="box box-warning">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เลือกแบบประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                               <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>            
</div> 
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-gear"></i>

              <h3 class="box-title">หัวข้อการประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
               
<table width="100%">
    <tr>
                                                    <td align="left" colspan="2" >
              <asp:GridView ID="grdEvaluation" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="UID" CssClass="table table-hover">
            <columns>

                <asp:TemplateField HeaderText="ลำดับ">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNo" runat="server" CssClass="textno" MaxLength="2" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "SEQNO") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            <asp:BoundField DataField="TopicName" HeaderText="หัวข้อเรื่องประเมิน">                      
                <HeaderStyle HorizontalAlign="Left" />
              <itemstyle HorizontalAlign="Center" />

                <ItemStyle HorizontalAlign="Left" />

            </asp:BoundField>

            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png"                                    
                      
                      CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
                  <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" HorizontalAlign="Left" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#F0F0F0" />
          </asp:GridView>
             
  <asp:Label ID="lblAlert" runat="server" CssClass="validateAlert" Width="100%" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" >
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" Width="80px" />    </td>
</tr>
  </table>        
                                                
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-plus-circle"></i>

              <h3 class="box-title">เลือกเพิ่มหัวข้อการประเมินด้านล่าง</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    

    <table width="100%">         
       
        <tr>
          <td  align="left" valign="top"><table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />
                  <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Text="ดูทั้งหมด" />
                </td>
            </tr>
            </table></td>
      </tr>

        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="UID" PageSize="15" CssClass="table table-hover">
            <RowStyle BackColor="White" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/arrow-up.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="TopicName" HeaderText="หัวข้อเรื่องประเมิน">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="left" />                      </asp:BoundField>

            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True"  
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#F0F0F0" />
          </asp:GridView></td>
      </tr>      
        
       
    </table>
                                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
</asp:Content>
