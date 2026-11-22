<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentStudentComment.aspx.vb" Inherits=".AssessmentStudentComment" %>


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
        <div class="row">
            <section class="col-lg-12 connectedSortable">
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-user-circle"></i>

                        <h3 class="box-title">ข้อมูลนิสิต(Student information)</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <table class="table table-responsive">
                            <tr>
                                <td width="150" class="texttopic">รหัสนิสิต(ID)</td>
                                <td>
                                    <asp:Label ID="lblStudentCode" runat="server"></asp:Label>
                                </td>
                                <td width="150" class="texttopic">ชื่อ - นามสกุล(Name)</td>
                                <td>
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="texttopic">ชื่อเล่น(Nickname)</td>
                                <td>
                                    <asp:Label ID="lblNickName" runat="server"></asp:Label></td>
                                <td class="texttopic">สาขา(Major)</td>
                                <td>
                                    <asp:Label ID="lblMajorName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="texttopic">ปีการศึกษา(Year) </td>
                                <td>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label>
                                </td>


                                <td class="texttopic">ผลัดที่ (Batch) </td>
                                <td>
                                    <asp:Label ID="lblPhaseNo" runat="server"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td class="texttopic">ผู้ประเมิน(Evaluator)</td>
                                <td>

                                    <asp:Label ID="lblAssessorName" runat="server"></asp:Label>
                                </td>
                                <td class="texttopic">&nbsp;</td>
                                <td>
                                      <asp:HiddenField ID="hdTimePhaseID" runat="server" />
                                      <asp:HiddenField ID="hdSubjectCode" runat="server" />
                                    <asp:HiddenField ID="hdPhaseID" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="box-footer clearfix">
                    </div>
                </div>
            </section>
        </div>

        <div class="row">
            <section class="col-lg-12 connectedSortable">
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-commenting-o"></i>

                        <h3 class="box-title">กิจกรรมที่นิสิตได้รับการฝึกงานในการฝึกงานนี้
                            <br />
                            <i>Activities that students receive in this internship</i>
                        </h3>
                    </div>
                    <div class="box-body">

                        <asp:TextBox ID="txtWorkDetail" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>

                    </div>

                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-commenting-o"></i>

                        <h3 class="box-title">นิสิตได้ฝึกปฏิบัติงานที่เป็นประโยชน์ต่อแหล่งฝึก หรือไม่ อย่างไร<br />
                            <i>Have students received practical training that is beneficial to the training center or not?</i></h3>
                    </div>
                    <div class="box-body">

                        <asp:TextBox ID="txtComment1" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>

                    </div>

                </div>
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-commenting-o"></i>

                        <h3 class="box-title">ทักษะด้านใดที่นิสิตมีการพัฒนาในขณะฝึกงาน : ความรู้ พฤติกรรม และ/หรือ ทักษะทางวิชาชีพ<br />
                            <i>What skills do students develop during their internship: knowledge, behavior, and/or professional skills? </i></h3>
                    </div>
                    <div class="box-body">

                        <asp:TextBox ID="txtComment2" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>

                    </div>

                </div>

            </section>
        </div>
        <div class="row">
            <section class="col-lg-5 connectedSortable">


                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-thumbs-up"></i>

                        <h3 class="box-title">ประเมินจุดเด่น : ท่านคิดว่านิสิตมีจุดแข็งด้านใด<br />
                            <i>Evaluate strengths: What strengths do you think students have? </i></h3>
                    </div>
                    <div class="box-body mailbox-messages">
                        <asp:CheckBoxList ID="chkPros" runat="server" CssClass="mailbox-messages">
                        </asp:CheckBoxList>
                    </div>
                    <div class="box-footer text-center">
                    </div>
                </div>
            </section>
            <section class="col-lg-5 connectedSortable">
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-thumbs-down"></i>

                        <h3 class="box-title">ประเมินจุดจุดด้อย : ท่านคิดว่านิสิตจำเป็นต้องปรับปรุงด้านใด<br />
                            <i>Evaluate weaknesses: What areas do you think students need to improve? </i></h3>
                    </div>
                    <div class="box-body mailbox-messages">
                        <asp:CheckBoxList ID="chkCons" runat="server" CssClass="mailbox-messages">
                        </asp:CheckBoxList>
                    </div>
                    <div class="box-footer text-center">
                    </div>
                </div>
            </section>
        </div>
        <div class="row">
            <section class="col-lg-12 connectedSortable">

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-star"></i>

                        <h3 class="box-title">ในภาพรวมท่านให้เกรดการฝึกปฏิบัติงานของนิสิตคนนี้อย่างไร?<br />
                            <i>Overall, how would you grade this student's internship? </i></h3>
                    </div>
                    <div class="box-body">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="optGrade" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
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
                                <td>การคิดเกรดในรายวิชาฯ จะนำคะแนนที่อาจารย์แหล่งฝึกประเมินด้านพฤติกรรม ด้านทักษะและความรู้ และด้านทักษะการสื่อสาร การนำเสนอ มาคิดตัดเกรด ซึ่งหากไม่สอดคล้องกับเกรดที่อาจารย์แหล่งฝึกให้ อาจารย์ผู้ประสานงานรายวิชาฯ จะติดต่อกับท่าน เพื่อขอความเห็นในการให้เกรด        
                       <br />
                                    <i>Grading in the course will take into account the scores assessed by the training instructor in terms of behavior, skills and knowledge, and communication and presentation skills. If it does not correspond to the grade given by the training instructor, the course coordinator will contact you to ask for your opinion on the grading. </i>
                                </td>
                            </tr>
                        </table>

                    </div>

                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-star"></i>

                        <h3 class="box-title">ในอนาคตหากท่านมีตำแหน่งงานว่างในหน่วยงานของท่าน ท่านจะรับนิสิตคนนี้เข้าทำงานหรือไม่?<br />
                            <i>In the future, if you have a job vacancy in your organization, will you hire this student? </i></h3>
                    </div>
                    <div class="box-body">
                        <asp:RadioButtonList ID="optIsSure" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="Y">รับ/Yes</asp:ListItem>
                            <asp:ListItem Value="N">ไม่รับ/No</asp:ListItem>
                            <asp:ListItem Value="M">ไม่แน่ใจ/Unsure</asp:ListItem>
                        </asp:RadioButtonList>


                    </div>

                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-star"></i>

                        <h3 class="box-title">ท่านมีความมั่นใจที่จะจ้างนิสิตท่านนี้มาเป็นเภสัชประจำ?<br />
                            <i>Are you confident in hiring this student as a full-time pharmacist? </i></h3>
                    </div>
                    <div class="box-body">
                        <table width="100%">
                            <tr>
                                <td width="180">ระบุ/Specify (%)&nbsp; 0-100 </td>
                                <td>
                                    <asp:TextBox ID="txtSure" runat="server" CssClass="text-center" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">ระบุเหตุผล/State the reason</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtReason" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>

                                </td>
                            </tr>
                        </table>

                    </div>

                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-commenting-o"></i>

                        <h3 class="box-title">คำติชมหรือข้อที่ต้องปรับปรุง อื่นๆ<br />
                            <i>Other criticisms or points for improvement </i></h3>
                    </div>
                    <div class="box-body">

                        <asp:TextBox ID="txtComment" runat="server" Height="100px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>

                    </div>

                </div>
                <div align="center">
                    <asp:Button ID="cmdSave" runat="server" Text="Submit" CssClass="btn btn-pink" Width="100px" />
                    <asp:Button ID="cmdBack" runat="server" Text="Back" CssClass="btn btn-secondary" Width="100px" />
                </div>
                <br />
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-info"></i>

                        <h3 class="box-title">อภิปราย</h3>
                    </div>
                    <div class="box-body">
                        1. แบบประเมินนี้เป็นเพียงกรอบคร่าวๆ ของการประเมินนิสิตเบื่องต้น เพื่อแนะนำนิสิตถึงข้อผิดพลาดในประเด็นต่างๆ ของแต่ละสัปดาห์ เพื่อให้นิสิตมีการพัฒนาการฝึกงานแหล่งฝึกได้อย่างมีประสิทธิภาพ 
                <br />
                        2. คะแนนของแบบประเมินนี้ไม่มีผลต่อแบบประเมินนิสิต
                <br />
                        3. เภสัชกรแหล่งฝึกสามารถประเมินนนิสิตรายบุคล โดยจะคุยเป็นรายบุคคลหรือคุยร่วมกับนิสิตอื่นๆ ก็ได้ ขึ้นอยู่กับ ดุลยพินิจของแต่ละท่านตามความเหมาะสม
ข้อสังเกต: หากเป็นเรื่องบุคลิกภาพ หรือเรื่องที่คิดว่ากระทบต่อจิตใจนิสิต เภสัชกรแหล่งฝึกควรคุยเป็นรายบุคคล
                <br />
                        4. ระวังเรื่อง การพูด negative ต่อนิสิต
                <br />
                        5. เภสัชกรแหล่งฝึกสามารถประเมินนิวิตทุกสัปดาห์ หรือตามดุลยพินิจและความเหมาะสมแต่ละแหล่งฝึก

                 <br />

                        <br />
                        1. This evaluation form is only a rough outline of the initial student evaluation to guide students on mistakes in various issues each week so that students can develop their internship effectively.
 <br />
                        2. The score of this evaluation form does not affect the student evaluation.
 <br />
                        3. The pharmacist in the training site can evaluate students individually, either individually or with other students, depending on the discretion of each individual as appropriate. Note: If it is a matter of personality or a matter that is thought to affect the student's mind, the pharmacist in the training site should discuss it individually.
 <br />
                        4. Be careful about speaking negatively to students.
 <br />
                        5. The pharmacist in the training site can evaluate students' behavior weekly or at the discretion and appropriateness of each training site.
                    </div>

                </div>
            </section>
        </div>
    </section>
</asp:Content>
