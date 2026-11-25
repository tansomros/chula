<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PracticeHistory.aspx.vb" Inherits=".PracticeHistory" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/student-achievement.css?v=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ach-page">
        <!-- Hero Header -->
        <div class="ach-hero">
            <div class="ach-hero-left">
                <div class="ach-hero-icon">
                    <i class="fa fa-history"></i>
                </div>
                <div>
                    <h1 class="ach-hero-title">ประวัติการฝึกปฏิบัติงานวิชาชีพ</h1>
                    <p class="ach-hero-subtitle">รวบรวมประวัติรายวิชาและแหล่งฝึกปฏิบัติงานวิชาชีพเภสัชกรรมที่นิสิตเคยเข้าร่วม</p>
                </div>
            </div>
            <div class="ach-hero-actions">
                <% If IsStudentRole Then %>
                <a href="Student_Bio.aspx?ActionType=mnuStudentBio" class="ach-btn ach-btn-secondary">
                    <i class="fa fa-user"></i> ข้อมูลประวัตินิสิต
                </a>
                <a href="StudentAchievement.aspx?ActionType=port" class="ach-btn ach-btn-primary">
                    <i class="fa fa-trophy"></i> ผลงานนิสิต
                </a>
                <% ElseIf TargetStudentCode <> "" Then %>
                <a href="Student_Bio.aspx?stdcode=<%= HttpUtility.UrlEncode(TargetStudentCode) %>" class="ach-btn ach-btn-secondary">
                    <i class="fa fa-user"></i> ข้อมูลประวัตินิสิต
                </a>
                <% End If %>
            </div>
        </div>

        <!-- Student Profile Strip -->
        <% If TargetStudentCode <> "" Then %>
        <div class="ach-profile-strip">
            <div class="ach-profile-identity">
                <div class="ach-profile-avatar">
                    <i class="fa fa-graduation-cap"></i>
                </div>
                <div>
                    <div class="ach-profile-name">
                        <%= HttpUtility.HtmlEncode(If(StudentFullName <> "", StudentFullName, TargetStudentCode)) %>
                    </div>
                    <div class="ach-profile-meta">
                        <span class="ach-meta-chip ach-meta-chip-code">
                            <i class="fa fa-id-badge"></i>
                            <%= HttpUtility.HtmlEncode(TargetStudentCode) %>
                        </span>
                        <span class="ach-meta-chip">
                            <i class="fa fa-flask"></i>
                            <%= HttpUtility.HtmlEncode(StudentMajorName) %>
                        </span>
                        <span class="ach-meta-chip">
                            <i class="fa fa-bookmark-o"></i>
                            <%= HttpUtility.HtmlEncode(StudentStudyYear) %>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <% End If %>

        <!-- KPI Summary Cards -->
        <div class="row ach-stats-row">
            <div class="col-md-4 col-sm-6">
                <div class="ach-stat-card">
                    <div class="ach-stat-icon ach-stat-icon-pink">
                        <i class="fa fa-stethoscope"></i>
                    </div>
                    <div>
                        <div class="ach-stat-label">การฝึกปฏิบัติงานทั้งหมด</div>
                        <div class="ach-stat-value">
                            <%= TotalPracticeCount.ToString("N0") %>
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
                        <div class="ach-stat-label">ช่วงปีการศึกษาที่เข้าฝึก</div>
                        <div class="ach-stat-value">
                            <%= DistinctYearCount.ToString("N0") %>
                            <span class="ach-stat-unit">ปีการศึกษา</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12">
                <div class="ach-stat-card">
                    <div class="ach-stat-icon ach-stat-icon-green">
                        <i class="fa fa-hospital-o"></i>
                    </div>
                    <div>
                        <div class="ach-stat-label">แหล่งฝึกที่เคยปฏิบัติงาน</div>
                        <div class="ach-stat-value">
                            <%= DistinctLocationCount.ToString("N0") %>
                            <span class="ach-stat-unit">แห่ง</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Practice History DataTable Card -->
        <div class="ach-card">
            <div class="ach-card-header">
                <h3 class="ach-card-title">
                    <i class="fa fa-list-ul"></i> รายการประวัติการฝึกปฏิบัติงานวิชาชีพ
                    <span class="ach-count-pill">
                        <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>&nbsp;รายการ
                    </span>
                </h3>
                <% If DistinctYears.Count > 1 Then %>
                <div class="prh-year-filter" id="prhYearFilter" role="group" aria-label="กรองตามปีการศึกษา">
                    <button type="button" class="prh-year-btn active" data-year="">ทั้งหมด</button>
                    <% For Each yr As String In DistinctYears %>
                    <button type="button" class="prh-year-btn" data-year="<%= HttpUtility.HtmlAttributeEncode(yr) %>"><%= HttpUtility.HtmlEncode(yr) %></button>
                    <% Next %>
                </div>
                <% End If %>
            </div>
            <div class="ach-card-body">
                <% If dtHis.Rows.Count > 0 Then %>
                <div class="ach-dt-wrap">
                    <table id="tbPracticeHistory" class="table dataTable">
                        <thead>
                            <tr>
                                <th class="text-center" style="width:56px;">#</th>
                                <th class="text-center" style="width:120px;">ปีการศึกษา</th>
                                <th class="text-center" style="width:140px;">รหัสวิชา</th>
                                <th>ชื่อรายวิชา</th>
                                <th>แหล่งฝึกปฏิบัติงาน</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                Dim rowIdx As Integer = 0
                                For Each row As DataRow In dtHis.Rows
                                    rowIdx += 1
                                    Dim eduYear As String = String.Concat(row("EduYear")).Trim()
                                    Dim subjectCode As String = String.Concat(row("SubjectCode")).Trim()
                                    Dim subjectName As String = String.Concat(row("SubjectName")).Trim()
                                    Dim locationName As String = String.Concat(row("LocationName")).Trim()
                            %>
                            <tr>
                                <td class="text-center">
                                    <span class="ach-row-num"><%= rowIdx %></span>
                                </td>
                                <td class="text-center" data-order="<%= HttpUtility.HtmlAttributeEncode(eduYear) %>" data-search="<%= HttpUtility.HtmlAttributeEncode(eduYear) %>"><span class="ach-year-pill"><%= HttpUtility.HtmlEncode(eduYear) %></span></td>
                                <td class="text-center">
                                    <span class="prh-subj-code"><%= HttpUtility.HtmlEncode(subjectCode) %></span>
                                </td>
                                <td>
                                    <div class="ach-event-title"><%= HttpUtility.HtmlEncode(subjectName) %></div>
                                </td>
                                <td>
                                    <div class="prh-location-cell">
                                        <span class="prh-loc-icon"><i class="fa fa-hospital-o"></i></span>
                                        <span class="prh-loc-name"><%= HttpUtility.HtmlEncode(locationName) %></span>
                                    </div>
                                </td>
                            </tr>
                            <% Next %>
                        </tbody>
                    </table>
                </div>
                <% End If %>

                <!-- Modern Empty State -->
                <div class="ach-empty-state" style='<%= If(dtHis.Rows.Count > 0, "display:none;", "") %>'>
                    <div class="ach-empty-icon">
                        <i class="fa fa-history"></i>
                    </div>
                    <h4 class="ach-empty-title">
                        <asp:Label ID="lblNot" runat="server" Text="ยังไม่มีประวัติการฝึกปฏิบัติงานวิชาชีพ"></asp:Label>
                    </h4>
                    <p class="ach-empty-desc">ข้อมูลประวัติรายวิชาและแหล่งฝึกปฏิบัติงานจะแสดงโดยอัตโนมัติเมื่อมีการจัดสรรแหล่งฝึกในระบบ</p>
                </div>
            </div>
            <div class="ach-card-footer">
                <div class="ach-info-banner">
                    <i class="fa fa-info-circle"></i>
                    <span>ข้อมูลประวัติการฝึกปฏิบัติงานวิชาชีพเชื่อมโยงจากระบบจัดสรรแหล่งฝึกและประเมินผลการฝึกปฏิบัติงานของคณะเภสัชศาสตร์ หากพบข้อมูลไม่ครบถ้วนกรุณาติดต่อหน่วยฝึกปฏิบัติงานวิชาชีพ</span>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            var $tbl = $('#tbPracticeHistory');
            if ($tbl.length && $.fn.DataTable) {
                var dt = $tbl.DataTable({
                    destroy: true,
                    autoWidth: false,
                    pageLength: 10,
                    lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'ทั้งหมด']],
                    order: [[1, 'desc'], [2, 'asc']],
                    columnDefs: [
                        { orderable: false, searchable: false, targets: [0] }
                    ],
                    language: {
                        search: 'ค้นหา',
                        searchPlaceholder: 'พิมพ์รหัสวิชา ชื่อรายวิชา หรือแหล่งฝึก...',
                        lengthMenu: 'แสดง _MENU_ รายการ',
                        info: 'แสดง _START_ ถึง _END_ จากทั้งหมด _TOTAL_ รายการ',
                        infoEmpty: 'แสดง 0 ถึง 0 จากทั้งหมด 0 รายการ',
                        infoFiltered: '(กรองจากทั้งหมด _MAX_ รายการ)',
                        emptyTable: 'ยังไม่มีประวัติการฝึกปฏิบัติงานวิชาชีพ',
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

                $('#prhYearFilter').on('click', '.prh-year-btn', function () {
                    var $btn = $(this);
                    var yr = ($btn.attr('data-year') || '').trim();
                    $('#prhYearFilter .prh-year-btn').removeClass('active');
                    $btn.addClass('active');
                    if (yr) {
                        var safeYr = yr.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
                        dt.column(1).search('^\\s*' + safeYr + '\\s*$', true, false).draw();
                    } else {
                        dt.column(1).search('').draw();
                    }
                });
            }
        });
    </script>
</asp:Content>
