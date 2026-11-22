<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssesseeStatus.aspx.vb" Inherits=".AssesseeStatus" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>ตรวจสอบสถานะการประเมิน</h1>
    </section>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-Search"></i>

                <h3 class="box-title">ค้นหา</h3>
 <label>ปีการศึกษา</label>
                            <asp:Label ID="lblYear" runat="server"></asp:Label>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body"> 
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>รายวิชา</label>
                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>แหล่งฝึก</label>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
             </div>
              <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>สถานะการประเมิน</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                <asp:ListItem Selected="True" Value="">ทั้งหมด</asp:ListItem>
                                <asp:ListItem Value="Y">ครบแล้ว</asp:ListItem>
                                <asp:ListItem Value="N">ยังไม่ครบ</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>ค้นหานิสิต</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            &nbsp;<span class="text10_blue">(ค้นหาจากรหัสนิสิต ,ชื่อ หรือ นามสกุล)</span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label></label>
                            <br />
                            <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-pink" Text="ค้นหา" Width="100px" />

                        </div>
                    </div>

                </div>

            </div>
            <div class="box-footer clearfix">
            </div>
        </div>


        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-user-circle-o"></i>

                <h3 class="box-title">รายการผลการประเมิน</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <asp:GridView ID="grdData"
                    runat="server" CellPadding="0" ForeColor="#333333"
                    GridLines="None"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20" CssClass="table table-hover">
                    <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField HeaderText="No." DataField="nRow">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต">
                            <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                        <asp:BoundField DataField="SubjectName" HeaderText="รายวิชาที่ฝึก" />
                        <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดที่">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Preceptor">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgPStatus" runat="server" ImageUrl="images/icon-ok.png" Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "PStatus")) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Advisor">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgAStatus" runat="server" ImageUrl="images/icon-ok.png" Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "AStatus")) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="th" Font-Bold="false" HorizontalAlign="Center"
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
