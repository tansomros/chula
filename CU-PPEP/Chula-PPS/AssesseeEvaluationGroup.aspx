<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssesseeEvaluationGroup.aspx.vb" Inherits=".AssesseeEvaluationGroup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
            <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-check icon-gradient bg-success"></i>
            </div>
            <div>Select an assessment           
                 <div class="page-title-subheading">เลือกแบบประเมิน
                 </div>
            </div>
        </div>
    </div>
</div>  


    <section class="content">
        <div class="box box-primary">

    <div class="box-header">
        <i class="fa fa-user-circle"></i>
        <h3 class="box-title">ข้อมูลนิสิต (Student information)</h3>

        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove">
                <i class="fa fa-times"></i>
            </button>
        </div>
    </div>

    <div class="box-body">

        <div class="row">

            <!-- รูป -->
            <div class="col-md-2 col-sm-3 text-center">
                <asp:Image ID="picStudent" runat="server" ImageUrl="images/unpic.jpg" CssClass="img-responsive" Style="max-width:120px;" />
                <asp:HiddenField ID="hdAssessmentID" runat="server" />
                <asp:HiddenField ID="hdTimePhaseID" runat="server" />
                <asp:HiddenField ID="hdPhaseID" runat="server" />
            </div>

            <!-- ข้อมูล -->
            <div class="col-md-10 col-sm-9">

                <div class="row p-1">

                    <div class="col-md-2 text-bold">
                        รหัสนิสิต(Student ID)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-1 text-bold">
                        ชื่อ(Name)
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
 <div class="col-md-2 text-bold">
                        ชื่อเล่น(Nickname)
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblNickName" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row p-1">

                    <div class="col-md-2 text-bold">
                        สาขา(Major)
                    </div>
                    <div class="col-md-10">
                        <asp:Label ID="lblMajorName" runat="server"></asp:Label>
                    </div>

                   

                </div>

                <div class="row p-1">

                    <div class="col-md-2 text-bold">
                        ปีการศึกษา(Year)
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblYearEn" runat="server"></asp:Label>
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-1 text-bold">
                        รายวิชา(Course)
                    </div>
                    <div class="col-md-5">
                        <asp:Label ID="lblSubjectCode" runat="server"></asp:Label>
                        <asp:Label ID="lblSubjectName" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-2 text-bold">
                        ผลัดที่(Batch)
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblPhaseNo" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row p-1">

                    <div class="col-md-2 text-bold">
                        แหล่งฝึก(Site)
                    </div>
                    <div class="col-md-7">
                        <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                        (<asp:Label ID="lblLocationID" runat="server"></asp:Label>)
                    </div>

                    <div class="col-md-1 text-bold">
                        ตำแหน่ง(Role)
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblAssessorRole" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row p-1">

                    <div class="col-md-2 text-bold">
                        ผู้ประเมิน(Evaluator)
                    </div>

                    <div class="col-md-4">
                        <asp:TextBox ID="txtAssessorName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>

            </div>

        </div>

    </div>

    <div class="box-footer clearfix"></div>

</div>

        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-filter"></i>

                <h3 class="box-title">เลือกแบบประเมิน (Select an assessment)</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="table-responsive">               
                <asp:GridView ID="grdData" runat="server" CellPadding="0" ForeColor="#333333"  cssclass="table table-hover"  GridLines="None" AutoGenerateColumns="False">
                    <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center"/>
                    <Columns>
                        <asp:BoundField HeaderText="No." DataField="nRow">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Evaluation Form">
                            <HeaderStyle HorizontalAlign="left" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullScore" HeaderText="Full Score">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Score" DataField="Score">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EvaluationGroupUID") %>' />
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
                 <div class="row text-right">
                     <table align="right">
                         <tr>
                             <td>คะแนนรวม&nbsp;</td>
                             <td class="text-center" width="60"><asp:Label ID="lblFullScore" runat="server" cssclass="text-bold" Text=""></asp:Label></td>
                             <td class="text-center" width="50">ได้</td>
                             <td width="110" class="text-left">
                                 <asp:Label ID="lblScore" runat="server" cssclass="text-bold" Text=""></asp:Label>

                             </td>
                         </tr>
                         
                     </table>
                     
 </div>

            </div>
            <div class="box-footer text-center">
                <div class="col-md-12 text-right">
                    <asp:Label ID="lblNote" runat="server" Text="Note : คะแนนที่แสดง เป็นคะแนนประเมินครั้งที่ 2 เท่านั้น"></asp:Label>

                </div>

            </div>
        </div>

        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-star"></i>

                <h3 class="box-title">ความคิดเห็นหรือข้อเสนอแนะ (Comments or Suggestions)</h3>
            </div>
            <div class="box-body">
                <table width="100%">
                    <tr>
                        <td class="text-bold">ในภาพรวมท่านให้เกรดการฝึกปฏิบัติงานของนิสิตคนนี้อย่างไร?   <br /> Overall, how would you grade this student's internship performance?    
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optGrade" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>A</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>B</asp:ListItem>
                                <asp:ListItem>C+</asp:ListItem>
                                <asp:ListItem>C</asp:ListItem>
                                <asp:ListItem>D+</asp:ListItem>
                                <asp:ListItem>D</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>ความคิดเห็นและ/หรือข้อเสนอแนะอื่นๆ (Additional Comments and/or Suggestions)</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtComment" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </div>

        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" runat="server" Text="บันทึกความคิดเห็น" CssClass="btn btn-primary" />
                <asp:Button ID="cmdSend" runat="server" Text="ยืนยันส่งคะแนน" CssClass="btn btn-success" />
                <asp:Button ID="cmdBack" runat="server" Text="หน้ารายชื่อนิสิต" CssClass="btn btn-warning" />
            </div>
        </div>
    </section>
</asp:Content>
