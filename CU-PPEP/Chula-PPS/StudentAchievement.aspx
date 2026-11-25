<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentAchievement.aspx.vb" Inherits=".StudentAchievement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/student-achievement.css?v=5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ach-page">
        <!-- Hero Header -->
        <div class="ach-hero">
            <div class="ach-hero-left">
                <div class="ach-hero-icon">
                    <i class="fa fa-trophy"></i>
                </div>
                <div>
                    <h1 class="ach-hero-title">ผลงานนิสิต</h1>
                    <p class="ach-hero-subtitle">รวบรวมประวัติการแข่งขัน การนำเสนอผลงาน ทุนสนับสนุน และรางวัลเกียรติยศของนิสิต</p>
                </div>
            </div>
            <div class="ach-hero-actions">
                <asp:LinkButton ID="cmdAdd" runat="server" CssClass="ach-btn ach-btn-primary">
                    <i class="fa fa-plus-circle"></i> บันทึกผลงานนิสิต
                </asp:LinkButton>
            </div>
        </div>

        <!-- Student Profile Strip -->
        <div id="pnStudentInfo" runat="server" class="ach-profile-strip">
            <div class="ach-profile-identity">
                <div class="ach-profile-avatar">
                    <i class="fa fa-graduation-cap"></i>
                </div>
                <div>
                    <div class="ach-profile-name">
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div class="ach-profile-meta">
                        <span class="ach-meta-chip ach-meta-chip-code">
                            <i class="fa fa-id-badge"></i>
                            <asp:Label ID="lblStudentCode" runat="server"></asp:Label>
                        </span>
                        <span class="ach-meta-chip">
                            <i class="fa fa-flask"></i>
                            <asp:Label ID="lblMajorName" runat="server"></asp:Label>
                        </span>
                        <span class="ach-meta-chip">
                            <i class="fa fa-bookmark-o"></i>
                            <asp:Label ID="lblStudyYear" runat="server"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <!-- KPI Summary Cards -->
        <div class="row ach-stats-row">
            <div class="col-md-4 col-sm-6">
                <div class="ach-stat-card">
                    <div class="ach-stat-icon ach-stat-icon-pink">
                        <i class="fa fa-trophy"></i>
                    </div>
                    <div>
                        <div class="ach-stat-label">ผลงานที่บันทึกทั้งหมด</div>
                        <div class="ach-stat-value">
                            <asp:Label ID="lblStatTotal" runat="server" Text="0"></asp:Label>
                            <span class="ach-stat-unit">รายการ</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-6">
                <div class="ach-stat-card">
                    <div class="ach-stat-icon ach-stat-icon-blue">
                        <i class="fa fa-calendar-check-o"></i>
                    </div>
                    <div>
                        <div class="ach-stat-label">ช่วงปีการศึกษาที่มีผลงาน</div>
                        <div class="ach-stat-value">
                            <asp:Label ID="lblStatYears" runat="server" Text="0"></asp:Label>
                            <span class="ach-stat-unit">ปีการศึกษา</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12">
                <div class="ach-stat-card">
                    <div class="ach-stat-icon ach-stat-icon-green">
                        <i class="fa fa-money"></i>
                    </div>
                    <div>
                        <div class="ach-stat-label">ทุน / เงินสนับสนุนรวม</div>
                        <div class="ach-stat-value">
                            <asp:Label ID="lblStatGrant" runat="server" Text="0.00"></asp:Label>
                            <span class="ach-stat-unit">บาท</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Structured Filters (matching CU-PFIS students/list) -->
        <div class="ach-card">
            <div class="ach-card-header">
                <h3 class="ach-card-title">
                    <i class="fa fa-sliders"></i> ตัวกรองข้อมูล
                </h3>
            </div>
            <div class="ach-card-body">
                <div class="row">
                    <div class="col-md-3 col-sm-6">
                        <div class="form-group" style="margin-bottom:0;">
                            <label>ปีการศึกษา (พ.ศ.)</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <div class="form-group" style="margin-bottom:0;">
                            <label>ประเภทผลงาน / กิจกรรม</label>
                            <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                <asp:ListItem Value="" Selected="True">-- ทั้งหมด --</asp:ListItem>
                                <asp:ListItem Value="การแข่งขันทางวิชาการ / วิชาชีพ">การแข่งขันทางวิชาการ / วิชาชีพ</asp:ListItem>
                                <asp:ListItem Value="การนำเสนอผลงานวิจัย / วิชาการ">การนำเสนอผลงานวิจัย / วิชาการ</asp:ListItem>
                                <asp:ListItem Value="รางวัลเชิดชูเกียรติ / เยาวชนดีเด่น">รางวัลเชิดชูเกียรติ / เยาวชนดีเด่น</asp:ListItem>
                                <asp:ListItem Value="ทุนการศึกษา / ทุนสนับสนุน">ทุนการศึกษา / ทุนสนับสนุน</asp:ListItem>
                                <asp:ListItem Value="กิจกรรมเสริมหลักสูตร / จิตอาสา / ผู้นำนิสิต">กิจกรรมเสริมหลักสูตร / จิตอาสา / ผู้นำนิสิต</asp:ListItem>
                                <asp:ListItem Value="กีฬา / ศิลปวัฒนธรรม">กีฬา / ศิลปวัฒนธรรม</asp:ListItem>
                                <asp:ListItem Value="อื่นๆ">อื่นๆ</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12">
                        <div class="ach-filter-actions">
                            <asp:LinkButton ID="cmdClear" runat="server" CssClass="ach-btn ach-btn-secondary" style="width:100%;">
                                <i class="fa fa-refresh"></i> ล้างตัวกรอง
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- DataTables.net Card -->
        <div class="ach-card">
            <div class="ach-card-header">
                <h3 class="ach-card-title">
                    <i class="fa fa-list-ul"></i> รายการผลงานและรางวัลนิสิต
                    <span class="ach-count-pill">
                        <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>&nbsp;รายการ
                    </span>
                </h3>
            </div>
            <div class="ach-card-body">
                <div id="pnTableWrap" runat="server" class="ach-dt-wrap">
                    <table id="tbAchievement" class="table dataTable">
                        <thead>
                            <tr>
                                <th class="text-center" style="width:52px;">#</th>
                                <asp:PlaceHolder ID="phStudentHead" runat="server">
                                    <th style="width:165px;">นิสิต</th>
                                </asp:PlaceHolder>
                                <th class="text-center" style="width:95px;">ปีการศึกษา</th>
                                <th class="text-center" style="width:72px;">ชั้นปี</th>
                                <th style="width:170px;">ประเภทผลงาน</th>
                                <th>ชื่องานแข่งขัน / กิจกรรมที่เข้าร่วม</th>
                                <th style="width:210px;">รางวัลที่ได้รับ / ผลการเข้าร่วม</th>
                                <th class="text-center" style="width:108px;">วันที่จัดงาน</th>
                                <th class="text-right" style="width:128px;">ทุน/เงินสนับสนุน</th>
                                <th class="text-center" style="width:118px;">การจัดการ</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptData" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center">
                                            <span class="ach-row-num"><%# Container.ItemIndex + 1 %></span>
                                        </td>
                                        <asp:PlaceHolder ID="phStudentCell" runat="server" Visible='<%# ShowStudentColumn %>'>
                                            <td>
                                                <strong><%# DataBinder.Eval(Container.DataItem, "StudentCode") %></strong>
                                                <span class="ach-subtext"><%# DataBinder.Eval(Container.DataItem, "StudentName") %></span>
                                            </td>
                                        </asp:PlaceHolder>
                                        <td class="text-center" data-order='<%# DataBinder.Eval(Container.DataItem, "AcademicYearBe") %>'>
                                            <span class="ach-year-pill"><%# DataBinder.Eval(Container.DataItem, "AcademicYearBe") %></span>
                                        </td>
                                        <td class="text-center" data-order='<%# DataBinder.Eval(Container.DataItem, "StudyYear") %>'>
                                            <span class="ach-study-pill">ปี <%# DataBinder.Eval(Container.DataItem, "StudyYear") %></span>
                                        </td>
                                        <td>
                                            <span class="ach-cat-pill"><%# DataBinder.Eval(Container.DataItem, "ActivityType") %></span>
                                        </td>
                                        <td>
                                            <div class="ach-event-title"><%# DataBinder.Eval(Container.DataItem, "EventName") %></div>
                                            <%# FormatOptionalSubtext(DataBinder.Eval(Container.DataItem, "EventNameEn")) %>
                                            <%# FormatOrganizerSubtext(DataBinder.Eval(Container.DataItem, "OrganizerName")) %>
                                        </td>
                                        <td>
                                            <div class="ach-award-chip">
                                                <i class="fa fa-trophy"></i>
                                                <span><%# DataBinder.Eval(Container.DataItem, "AwardName") %></span>
                                            </div>
                                            <%# FormatOptionalSubtext(DataBinder.Eval(Container.DataItem, "AwardNameEn")) %>
                                        </td>
                                        <td class="text-center" data-order='<%# FormatDateSortKey(DataBinder.Eval(Container.DataItem, "EventDate")) %>'>
                                            <%# FormatDateThai(DataBinder.Eval(Container.DataItem, "EventDate")) %>
                                        </td>
                                        <td class="text-right" data-order='<%# FormatGrantSortKey(DataBinder.Eval(Container.DataItem, "GrantAmount")) %>'>
                                            <%# FormatGrantAmountHtml(DataBinder.Eval(Container.DataItem, "GrantAmount")) %>
                                        </td>
                                        <td class="text-center">
                                            <div class="ach-action-group">
                                                <a href='<%# BuildItemUrl("view", DataBinder.Eval(Container.DataItem, "AchievementId")) %>'
                                                   class="ach-icon-btn ach-icon-btn-view"
                                                   title="ดูรายละเอียด">
                                                    <i class="fa fa-eye"></i>
                                                </a>
                                                <asp:PlaceHolder ID="phModifyActions" runat="server" Visible='<%# CanModifyItems %>'>
                                                    <a href='<%# BuildItemUrl("mod", DataBinder.Eval(Container.DataItem, "AchievementId")) %>'
                                                       class="ach-icon-btn ach-icon-btn-edit"
                                                       title="แก้ไขข้อมูล">
                                                        <i class="fa fa-pencil"></i>
                                                    </a>
                                                    <asp:LinkButton ID="lnkDel" runat="server"
                                                        CommandName="DeleteItem"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AchievementId") %>'
                                                        CssClass="ach-icon-btn ach-icon-btn-del"
                                                        ToolTip="ลบรายการ"
                                                        OnClientClick="return confirm('ต้องการลบข้อมูลผลงานนิสิตรายการนี้ใช่หรือไม่?');">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
                                                </asp:PlaceHolder>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <!-- Modern Empty State -->
                <div id="pnEmpty" runat="server" class="ach-empty-state" visible="false">
                    <div class="ach-empty-icon">
                        <i class="fa fa-trophy"></i>
                    </div>
                    <h4 class="ach-empty-title">ยังไม่มีข้อมูลผลงานนิสิต</h4>
                    <p class="ach-empty-desc">เริ่มต้นบันทึกประวัติการแข่งขัน การเข้าร่วมกิจกรรม ทุนสนับสนุน หรือรางวัลที่ท่านได้รับ</p>
                    <asp:LinkButton ID="cmdAddEmpty" runat="server" CssClass="ach-btn ach-btn-primary">
                        <i class="fa fa-plus-circle"></i> บันทึกผลงานนิสิตใหม่
                    </asp:LinkButton>
                </div>
            </div>
            <div class="ach-card-footer">
                <div class="ach-info-banner">
                    <i class="fa fa-info-circle"></i>
                    <span>นิสิตสามารถบันทึกและยืนยันข้อมูลผลงาน/รางวัลด้วยตนเองโดยไม่ต้องแนบไฟล์เกียรติบัตร ข้อมูลจะถูกเชื่อมโยงไปยังระบบสารสนเทศคณะเภสัชศาสตร์ (CU-PFIS) เพื่อให้เจ้าหน้าที่ตรวจสอบรับรองต่อไป</span>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            var $tbl = $('#tbAchievement');
            if ($tbl.length && $.fn.DataTable) {
                var dt = $tbl.DataTable({
                    destroy: true,
                    autoWidth: false,
                    pageLength: 10,
                    lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'ทั้งหมด']],
                    order: [],
                    columnDefs: [
                        { orderable: false, searchable: false, targets: [0, -1] }
                    ],
                    language: {
                        search: 'ค้นหา',
                        searchPlaceholder: 'พิมพ์ชื่องาน รางวัล หรือหน่วยงานผู้จัด...',
                        lengthMenu: 'แสดง _MENU_ รายการ',
                        info: 'แสดง _START_ ถึง _END_ จากทั้งหมด _TOTAL_ รายการ',
                        infoEmpty: 'แสดง 0 ถึง 0 จากทั้งหมด 0 รายการ',
                        infoFiltered: '(กรองจากทั้งหมด _MAX_ รายการ)',
                        emptyTable: 'ไม่พบข้อมูลผลงานนิสิต',
                        zeroRecords: 'ไม่พบข้อมูลที่ตรงกับคำค้นหา',
                        paginate: {
                            first: '«',
                            previous: '‹',
                            next: '›',
                            last: '»'
                        }
                    }
                });

                dt.on('order.dt search.dt draw.dt', function () {
                    var info = dt.page.info();
                    dt.column(0, { search: 'applied', order: 'applied', page: 'current' }).nodes().each(function (cell, i) {
                        var $badge = $(cell).find('.ach-row-num');
                        if ($badge.length) {
                            $badge.text(info.start + i + 1);
                        }
                    });
                });
            }
        });
    </script>
</asp:Content>
