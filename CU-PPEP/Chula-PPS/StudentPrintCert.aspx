<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentPrintCert.aspx.vb" Inherits=".StudentPrintCert" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>พิมพ์ใบรับรองการผ่านการฝึกปฏิบัติงาน</h1>
    </section>

    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-search-plus"></i>

                <h3 class="box-title">ค้นหานิสิต</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ปีการศึกษา</label>
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>สาขาวิชา</label>
                            <asp:DropDownList ID="ddlMajor" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ชั้นปีที่</label>
                            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control select2">
                                <asp:ListItem Selected="True" Value="0">--ไม่ระบุ--</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem Value="7">สำเร็จการศึกษา</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>รหัส/ชื่อ</label>
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2 pt-3">
                        <div class="form-group">
                            <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="80px" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer clearfix">
            </div>
        </div>

        <div class="box box-success">
            <div class="box-header">
                <i class="fa fa-list"></i>

                <h3 class="box-title">เลือกนิสิต</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body mailbox-messages">
                <asp:GridView ID="grdData" runat="server" CellPadding="0"
                    GridLines="None"
                    AllowPaging="True" CssClass="txtcontent"
                    Font-Bold="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="Student_ID">
                    <RowStyle BackColor="#F7F7F7" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Center"
                        CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="th" Font-Bold="True"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstName" HeaderText="ชื่อนิสิต">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
                        <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                        <asp:BoundField DataField="MinorName" HeaderText="สาขา" />
                        <asp:BoundField DataField="SubMinorName" HeaderText="แขนง" />
                        <asp:BoundField DataField="ClassName" HeaderText="ชั้นปีที่">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
            <div class="box-footer text-center">
                <asp:LinkButton ID="lnkPrintAll" runat="server" CssClass="btn btn-danger"><img src='images/pdffile.png?v=1' width="20"/> พิมพ์จากผลการค้นหาทั้งหมด</asp:LinkButton>
                <asp:LinkButton ID="lnkPrintSelect" runat="server" CssClass="btn btn-danger"><img src='images/pdffile.png?v=1' width="20"/> พิมพ์ที่เลือกทั้งหมด</asp:LinkButton>

                  <asp:LinkButton ID="lnkExportAll" runat="server" CssClass="btn btn-primary"><img src='images/word.png'width="20"/> Export Word จากผลการค้นหาทั้งหมด</asp:LinkButton>
  <asp:LinkButton ID="lnkExportSelect" runat="server" CssClass="btn btn-primary"><img src='images/word.png'width="20"/> Export Word ที่เลือกทั้งหมด</asp:LinkButton>
            </div>
        </div>

    </section>
</asp:Content>
