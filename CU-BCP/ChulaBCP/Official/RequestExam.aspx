<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="RequestExam.aspx.vb" Inherits=".RequestExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-pen icon-gradient bg-mean-fruit"></i>
                </div>
                <div>
                    Request<div class="page-title-subheading">ลงทะเบียนคำร้องขอ/แสดงความจำนง</div>
                </div>
            </div>
        </div>
    </div>
    <section class="content">
        <div class="row">
            <section class="col-lg-12 connectedSortable">
                <div class="main-card mb-3 card">
                    <div class="card-header">
                        <i class="header-icon lnr-user icon-gradient bg-success"></i>ผู้ยื่นคำร้อง               
                 <div class="box-tools pull-right">
                     <asp:HiddenField ID="hdStudentID" runat="server" />
                 </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>รหัส</label>
                                    <asp:Label CssClass="text-primary text-center" ID="lblStdCode" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>ชื่อ-นามสกุล</label>
                                    <asp:Label CssClass="text-primary text-center" ID="lblStudentName" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>สาขา</label>
                                    <asp:Label CssClass="text-primary text-center" ID="lblMajor" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="main-card mb-3 card">
                    <div class="card-header text-primary">
                        <asp:Label ID="lblTopic" runat="server"></asp:Label>
                        <div class="box-tools pull-right">
                            <asp:HiddenField ID="hdTopicUID" runat="server" />
                            <asp:HiddenField ID="hdRequestID" runat="server" />
                        </div>
                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>วันที่คำร้อง</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtReqDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-10">
                                <div class="form-group">
                                    <label>เข้าสอบ/ไม่เข้าสอบ<span class="text-red">*</span></label>
                                    <asp:RadioButtonList ID="optYesOrNo" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="Y">เข้าสอบ</asp:ListItem>
                                        <asp:ListItem Value="N">ไม่เข้าสอบ</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div id="pnNo" runat="server" class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>กรณีไม่เข้าสอบเนื่องจาก</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtRemarkNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="pnYes1" runat="server" class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>โดยข้อสอบในสาขาเน้น</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEmphasize" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="pnYes2" runat="server" class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ประเภท<span class="text-red">*</span></label>
                                    <asp:RadioButtonList ID="optType" runat="server">
                                        <asp:ListItem Selected="True" Value="1">ประเภทที่ 1 การสอบปากเปล่าโดยใช้กรณีศึกษา (Oral Examination)*</asp:ListItem>
                                        <asp:ListItem Value="2">ประเภทที่ 2 การสอบข้อเขียน (Written Examination)</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>องค์ความรู้ที่<span class="text-red">*</span></label>
                                    <asp:RadioButtonList ID="optKnowledgeNo" runat="server">
                                        <asp:ListItem Selected="True" Value="1">องค์ความรู้ที่ 1 ด้านเภสัชบำบัด (Pharmacotherapy; PT)</asp:ListItem>
                                        <asp:ListItem Value="2">องค์ความรู้ที่ 2 ด้านความรู้อื่นๆที่เกี่ยวข้อง เช่น การประเมินวรรณกรรม เน้นสถิติ และระเบียบวิธีวิจัย และระบบยาในโรงพยาบาล (Non-pharmacotherapy; N-PT)</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div id="pnYes3" runat="server" class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>สอบครั้งที่</label>
                                    <asp:RadioButtonList ID="optExamRound" runat="server">
                                        <asp:ListItem Selected="True" Value="1">เป็นการสอบครั้งแรก</asp:ListItem>
                                        <asp:ListItem Value="2">เป็นการสอบครั้งที่ 2 เป็นต้นไป**</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>สถานที่สอบ<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            หมายเหตุ – * เภสัชกรประจำบ้านต้องเตรียมกรณีศึกษา 3 กรณีศึกษา โดยนาส่งให้ประธานหลักสูตรฯ พิจารณาก่อนนำส่ง ให้กับคณะอนุกรรมการสอบฯ 
               <br />
                            ** อัตราค่าธรรมเนียมการขอสอบครั้งที่ 2 เป็นต้นไป ราคา 500 บาท/ครั้ง โดยจะต้องแนบสำเนาใบโอนเงินพร้อมกับ ใบสมัครสอบประเมินผลฯ 
                        </div>
                    </div>
                </div>
                <div align="center">
                    <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Next" Width="100px" />
                    <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-success" Text="พิมพ์" Width="100px" />
                    <asp:Button ID="cmdDelete" runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" />
                </div>
            </section>
        </div>
    </section>
</asp:Content>


