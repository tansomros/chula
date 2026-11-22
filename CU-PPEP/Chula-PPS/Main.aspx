<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Main.aspx.vb" Inherits=".Main" %>

<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Orientation="Vertical" SeparatorVisible="False" Theme="MetropolisBlue" Width="100%" AllowResize="False" EnableHierarchyRecreation="False">
            <Panes>
                <dx:SplitterPane>
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                            head
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane>
                    <Panes>
                        <dx:SplitterPane Size="210px" MinSize="210px" AutoHeight="true">
                            <Panes>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                            pic
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                               <dx:ASPxNavBar ID="nbMain" runat="server" Theme="MetropolisBlue">
                                <Groups>
                                    <dx:NavBarGroup Text="ระเบียนประวัติ">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="Student_Bio.aspx?m=1&amp;p=101" Text="ข้อมูลประวัตินิสิต">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="PracticeHistory.aspx?m=1&amp;p=102" Text="ประวัติการฝึกปฏิบัติงานวิชาชีพ">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="ReportViewerStudentBio.aspx?m=1&amp;p=103&amp;id=<% Session('ProfileID') %>" Text="พิมพ์ประวัติ">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="ข้อมูลการฝึกปฏิบัติวิชาชีพ">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="PracticeHistory.aspx?m=1&amp;p=102" Text="ประวัติการฝึกปฏิบัติงานวิชาชีพ">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="เมนูหลัก">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="StudentPrintBio.aspx?m=3&amp;p=301" Text="พิมพ์ประวัตินิสิต">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="CourseStudent.aspx?m=3&amp;p=303" Text="กำหนดนิสิตในรายวิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="CourseLocation.aspx?m=3&amp;p=304" Text="กำหนดแหล่งฝึกในรายวิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="SupervisorLocation.aspx?m=4&amp;p=401" Text="เลือกสถานที่นิเทศ">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl=".aspx?m=4&amp;p=402" Text="ส่งงานนิเทศ">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Locations.aspx?m=3&amp;p=lc" Text="ข้อมูลแหล่งฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="ResultProceptor.aspx?m=3&amp;p=p1" Text="รายชื่อนิสิตที่เข้ามาฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="EditList.aspx?m=pct&amp;p=3" Text="รายงานผลการประเมิน">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="นิสิต">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="StudentData.aspx?m=5&amp;p=501" Text="ข้อมูลนิสิต">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="StudentPrintBio.aspx?m=5&amp;p=502" Text="พิมพ์ประวัตินิสิต">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="อาจารย์">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="TeacherData.aspx?m=6&amp;p=601" Text="ข้อมูลอาจารย์">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="PreceptorData.aspx?m=6&amp;p=pct" Text="ข้อมูลอาจารย์แหล่่งฝึก">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="รายวิชา">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="Subjects.aspx?m=s&amp;p=s01" Text="ข้อมูลรายวิชา (Master)">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="แหล่งฝึก">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="LocationGroup.aspx?m=8&amp;p=lg" Text="ประเภทแหล่งฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Locations.aspx?m=8&amp;p=lc" Text="แหล่งฝึก">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="กำหนดข้อมูลประจำปี">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="TimePhase.aspx?m=7&amp;p=tp" Text="ผลัดฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Courses.aspx?m=7&amp;p=702" Text="กำหนดรายวิชาที่เปิดฝึกงาน">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="CourseCoordinator.aspx?m=7&amp;p=703" Text="กำหนดอาจารย์ประจำวิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="CourseStudent.aspx?m=7&amp;p=704" Text="กำหนดนิสิตในรายวิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="CourseLocation.aspx?m=7&amp;p=705" Text="กำหนดแหล่งฝึกให้รายวิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="RequirementsReg.aspx?m=7&amp;p=rr" Text="ความต้องการของแหล่งฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="RequirementSearch.aspx?m=7&amp;p=rl" Text="ค้นหารายการความต้องการ">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="ข้อมูลการเลือกแหล่งฝึก">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="StudentRegister.aspx?m=15&amp;p=151" Text="ข้อมูลการเลือกแหล่งฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="StudentNoRegister.aspx?m=15&amp;p=152" Text="นิสิตที่ยังไม่ได้เลือกแหล่งฝึก">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="การคัดเลือก">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="AssignRandom.aspx?m=11&amp;p=rnd" Text="คัดเลือกด้วยการสุ่ม (Random)">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="AssignManual.aspx?m=11&amp;p=mnl" Text="คัดเลือกด้วยวิธี Manual">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="StudentNoAssessment.aspx?m=11&amp;p=114" Text="รายชื่อนิสิตที่ยังไม่ได้แหล่งฝึก">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="ReportAssesmentByStudent.aspx?m=11&amp;p=115" Text="ค้นหาผลการคัดเลือก">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="รายงาน">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="ReportREQByLocation.aspx?m=rpt&amp;p=r1" Text="รายงานแหล่งฝึก (รับนิสิต)">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="ReportStudentLocation.aspx?m=rpt&amp;p=r2" Text="รายงานการเลือกแหล่งฝึกของนิสิต">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="ตั้งค่าข้อมูลพื้นฐาน">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="Provinces.aspx?m=12&amp;p=pv" Text="กำหนดจังหวัด">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Prefix.aspx?m=12&amp;p=fx" Text="คำนำหน้าชื่อ">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Positions.aspx?m=12&amp;p=pos" Text="ชื่อตำแหน่ง">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Department.aspx?m=12&amp;p=dept" Text="ชื่อภาควิชา">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="DataConfigLevel.aspx?m=12&amp;p=lvl" Text="กำหนดระดับชั้นนิสิต">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="DataConfig.aspx?m=12&amp;p=cfg" Text="กำหนดค่าเริ่มต้นอื่นๆ">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="จัดการผู้ใช้งาน">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="Roles.aspx?m=9&amp;p=role" Text="กำหนดสิทธิ์ (Role)">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="Users.aspx?m=9&amp;p=ua" Text="เพิ่มผู้ใช้งาน (User)">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="LogfilesByUser.aspx?m=9&amp;p=hs" Text="ประวัติการใช้งานระบบของ user">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                    <dx:NavBarGroup Text="ประกาศข่าว">
                                        <Items>
                                            <dx:NavBarItem NavigateUrl="News_List.aspx?m=news&amp;p=news-lst" Text="รายการข่าวประกาศ">
                                            </dx:NavBarItem>
                                            <dx:NavBarItem NavigateUrl="News_Manage.aspx?m=news&amp;p=news-mng" Text="เพิ่มข่าวประกาศ">
                                            </dx:NavBarItem>
                                        </Items>
                                    </dx:NavBarGroup>
                                </Groups>
                                <CollapseImage IconID="navigation_previous_16x16office2013">
                                </CollapseImage>
                                <ExpandImage IconID="navigation_next_16x16office2013">
                                </ExpandImage>
                            </dx:ASPxNavBar>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server">
                                     
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane>
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server">
                                    main
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                       
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane>
                    <Panes>
                        <dx:SplitterPane Size="210px" MinSize="210px">
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server">
                                    footleft
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane>
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server">
                                    footright
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
    
    </div>
    </form>
</body>
</html> 
			
      