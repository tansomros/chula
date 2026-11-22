<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationTopic.aspx.vb" Inherits=".EvaluationTopic" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>จัดการหัวข้อการประเมิน</h1>
    </section>

    <section class="content">

        <div class="box box-pink">

            <div class="box-header">
                <i class="fa fa-filter"></i>

                <h3 class="box-title">เพิ่ม/แก้ไข หัวข้อการประเมิน</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <table cellspacing="1" cellpadding="1" border="0" width="98%">
                    <tr>
                        <td align="left" width="100">รหัส :</td>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server"></asp:Label>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">หัวข้อ:</td>
                        <td align="left">
                            <asp:TextBox ID="txtName" runat="server"
                                Width="100%" CssClass="form-control" Height="52px" TextMode="MultiLine" MaxLength="1000"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left">น้ำหนักคะแนน:</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlWeight" runat="server" CssClass="form-control select2" Width="100px">
                                <asp:ListItem Value="0.25">0.25</asp:ListItem>
                                <asp:ListItem Value="0.50">0.50</asp:ListItem>
                                <asp:ListItem Value="0.75">0.75</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                <asp:ListItem Value="1.25">1.25</asp:ListItem>
                                <asp:ListItem Value="1.50">1.50</asp:ListItem>
                                <asp:ListItem Value="1.75">1.75</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="2.25">2.25</asp:ListItem>
                                <asp:ListItem Value="2.50">2.50</asp:ListItem>
                                <asp:ListItem Value="2.75">2.75</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                            </asp:DropDownList>


                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">Remark :</td>
                        <td align="left">
                            <asp:TextBox ID="txtRemark" runat="server"
                                Width="100%" CssClass="form-control" Height="70px" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Status : </td>
                        <td align="left" class="mailbox-messages">
                            <asp:CheckBox ID="chkStatus" runat="server" Text="Active"
                                Checked="True" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" valign="top">

                            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Text="บันทึก" Width="80px" />
                            &nbsp;<asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Text="ยกเลิก" Width="80px" />
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

                <h3 class="box-title">หัวข้อการประเมิน</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">

                    <tr>
                        <td align="center" valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">

                            <table border="0" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td width="50">ค้นหา</td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />
                                        &nbsp;<asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Text="ดูทั้งหมด" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" valign="top">
                            <asp:GridView ID="grdData"
                                runat="server" CellPadding="0" ForeColor="#333333"
                                GridLines="None"
                                AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="15">
                                <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField HeaderText="No." DataField="nRow">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TopicName" HeaderText="หัวข้อ">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" /> 
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Weight" HeaderText="นน.">
     <HeaderStyle HorizontalAlign="center" />
     <ItemStyle HorizontalAlign="center" /> 
 </asp:BoundField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png"
                                                Visible='<%# ConvertStatusFlag2Boolean(DataBinder.Eval(Container.DataItem, "StatusFlag")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDel" runat="server"
                                                ImageUrl="images/icon-delete.png"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle HorizontalAlign="Center" CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="th" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="box-footer clearfix">
            </div>
        </div>

    </section>
</asp:Content>
