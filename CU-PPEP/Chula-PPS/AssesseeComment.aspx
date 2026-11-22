<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssesseeComment.aspx.vb" Inherits=".AssesseeComment" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-thumbs-up icon-gradient bg-warning"></i>
                </div>
                <div>
                    Evaluation of Student’s Strengths/Weaknesses (No Score)      
                 <div class="page-title-subheading">
                     ประเมินจุดเด่น/จุดด้อย นิสิต (ไม่มีคะแนน)
                 </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-filter"></i>

                <h3 class="box-title">ค้นหา</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ปี (Year)</label>
                            <br />
                            <asp:Label ID="lblYear" runat="server" CssClass="form-control text-center"></asp:Label>
                            <asp:Label ID="lblYearEn" runat="server" CssClass="form-control text-center"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>แหล่งฝึก(Site)</label>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>ผลัดฝึก (Batch)</label>
                            <asp:DropDownList ID="ddlPhase" runat="server"
                                CssClass="form-control select2" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Keyword</label>
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <div class="form-group">
                            <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="100" Text="ค้นหา" />
                            <asp:Button ID="cmdNoScore" runat="server" CssClass="btn btn-success" Text="เฉพาะที่ยังไม่ประเมิน" />
                            <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-primary" Width="100" Text="ดูทั้งหมด" />
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

                <h3 class="box-title">รายชื่อนิสิตที่ต้องประเมิน (Student list)</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">

                              <div class="table-responsive">
    <table id="tbuser" class="table table-hover">
        <thead>
            <tr>                                          
                <th class="text-center">No.</th>
                <th class="text-left">รหัสนิสิต(ID)</th>
                <th class="text-left">ชื่อ-สกุล (Name)</th>
                <th class="text-left">รายวิชา (Course)</th>
                <th class="text-center">ผลัดฝึก (Batch)</th>
                <th class="text-center">วันที่ฝึก (Period)</th>
                <th class="text-center">สถานะ (Status)</th>     
                <th class="sorting_asc_disabled sorting_desc_disabled text-center">ประเมิน (Estimate)</th>    
            </tr>
        </thead>
        <tbody>
            <% For Each row As DataRow In dtSwot.Rows %>
            <tr>                
                <td class="text-center"><% =String.Concat(row("nRow")) %></td>               
                <td class="text-center"><% =String.Concat(row("Student_Code")) %></td>
                <td class="text-left"><% =String.Concat(row("StudentName")) %></td>
                <td class="text-left"><% =String.Concat(row("SubjectName")) %></td>  
                <td class="text-center"><% =String.Concat(row("PhaseNo")) %></td>  
                <td class="text-center"><% =String.Concat(row("PhaseName")) %></td>             
                <td class="text-center">
                   <% If String.Concat(row("AssessmentStatus")) = "Y" Then%>
                     <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
                   <% End If %>
                </td> 
                 <td width="50" class="text-center">
                       <a href="AssessmentStudentComment.aspx?std=<% =String.Concat(row("Student_Code")) %>&sj=<% =String.Concat(row("SubjectCode")) %>&tp=<% =String.Concat(row("TimePhaseID")) %>&ph=<% =String.Concat(row("PhaseID")) %>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="Estimate" target="_blank"><i class="fa fa-edit" aria-hidden="true"></i></a>                                                    
</td>
            </tr>
            <%  Next %>
        </tbody>
    </table>
</div>




                <asp:GridView ID="grdData" CssClass="table table-hover"
                    runat="server" CellPadding="0" ForeColor="#333333"
                    GridLines="None"
                    AutoGenerateColumns="False" Width="100%">
                    <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField HeaderText="No." DataField="nRow">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต (ID)">
                            <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล(Name)">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhaseNo" HeaderText="ผลัด (Batch)">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhaseName" HeaderText="วันที่ฝึก(Date)" />
                        <asp:TemplateField HeaderText="สถานะ(Status)">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" Visible='<%# ConvertYN2Boolean(DataBinder.Eval(Container.DataItem, "AssessmentStatus")) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Student_Code") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
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
