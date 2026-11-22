<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationGroup.aspx.vb" Inherits=".EvaluationGroup" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<section class="content-header">
      <h1>จัดการแบบการประเมิน</h1>   
    </section>

<section class="content">          
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข แบบประเมิน</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
                 
<table class="table table-responsive">
                                                <tr>
                                                    <td>
                                                        รหัส</td>
                                                    <td> 
                                                        <asp:Label ID="lblID" runat="server"></asp:Label>                                                    &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ชื่อ</td>
                                                    <td><asp:TextBox ID="txtName" runat="server" 
                                                            Width="500px" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Alias Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAiasName" runat="server"  Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>คะแนนเต็ม</td>
                                                    <td>
                                                        <asp:TextBox ID="txtUnit" runat="server" 
                                                         Width="50px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>ระดับคะแนน</td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                        <asp:TextBox ID="txtMin" runat="server" 
                                                         Width="50px">1</asp:TextBox></td>
                                                                <td>-</td>
                                                                <td>
                                                        <asp:TextBox ID="txtMax" runat="server" 
                                                         Width="50px">5</asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>สำหรับ</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTeacher" runat="server" Text="Advisor" />
                                                        <asp:CheckBox ID="chkPreceptor" runat="server" Text="Preceptor" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>เงื่อนไขพิเศษ</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkCompare" runat="server" Text="ถ้าคะแนนประเมินต่ำกว่าเกณฑ์ต้องระบุเหตุผลก่อนบันทึกทุกครั้ง" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkNoScore" runat="server" Text="ไม่นำคะแนนไปรวมในการตัดเกรด" />
                                                    </td>
                                                </tr> 
    <tr>
                                                    <td valign="top" >
                                                        คำอธิบายคะแนน</td>
                                                    <td>
                                                        <dx:ASPxHtmlEditor ID="txtRemark" runat="server" Height="200px" Theme="Office2010Blue" Width="100%" ToolbarMode="None">
                                                            <Settings AllowHtmlView="False">
                                                            </Settings>
                                                        </dx:ASPxHtmlEditor>
                                                        </td>
                                                </tr>

                                                <tr>
                                                    <td>Status</td>
                                                    <td><asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                                                            Checked="True" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" >
                                                       
                                         <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
                                                                                          </td>
                                                </tr>
  </table>                                                
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการแบบประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">                                      


    <table width="100%">
        <tr>
          <td valign="top">  
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>               
                  <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="70" Text="ทั้งหมด"></asp:Button>              </td>
            </tr>
            </table></td>
      </tr>

        <tr>
          <td align="right" valign="top">
              <asp:Image ID="Image3" runat="server" ImageUrl="images/advisor.png" />
&nbsp;= Advisor ,
              <asp:Image ID="Image4" runat="server" ImageUrl="images/preceptor.png" />
&nbsp;= Preceptor</td>
      </tr>
       
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="แบบการประเมิน">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NameHTML") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="AliasName" HeaderText="Alias Name" />
                <asp:BoundField DataField="FullScore" HeaderText="คะแนนเต็ม">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ScoreRange" HeaderText="ระดับคะแนน">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl="images/advisor.png" 
                                    Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "isAdvisor")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl="images/preceptor.png" 
                                    Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "isPreceptor")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="imgComp" runat="server" ImageUrl="images/alert_icon.png" Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "isCompare")) %>' Width="20px" ToolTip="บังคับให้ระบุเหตุผลหากคะแนนรวมต่ำกว่าเกณฑ์" />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
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
          </asp:GridView></td>
      </tr>
       
    </table>
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>    
</asp:Content>
