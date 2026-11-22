<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucMenu.ascx.vb" Inherits=".ucMenu" %>
    <link rel="stylesheet" type="text/css" href="css/custyles.css"> 
<ul class="sidebar-menu" data-widget="tree">
       <li class="header">MAIN NAVIGATION</li>

        <% If Request("actionType") = "h"  %>
            <li class="active"><a href="Home.aspx?actionType=h"><i class="fa fa-home"></i> <span>หน้าแรก</span></a></li>
        <% Else %>
            <li><a href="Home.aspx?actionType=h"><i class="fa fa-home"></i> <span>หน้าแรก</span></a></li> 
        <% End If %> 

<% If Request.Cookies("ROLE_STD").Value = True Then %>
      
                <% If Request.QueryString("ActionType") = "mnuStudentBio" Then%>
                    <li class="active"><a href="Student_Bio.aspx?ActionType=mnuStudentBio"><i class="fa fa-user"></i> <span>ข้อมูลประวัตินิสิต</span></a></li>
                <% Else%>
                    <li><a href="Student_Bio.aspx?ActionType=mnuStudentBio"><i class="fa fa-user"></i> <span>ข้อมูลประวัตินิสิต</span></a></li>
                <% End If%>         
         
                <% If Request.QueryString("ActionType") = "mnuPracticeHistory" Then%>
                    <li class="active"><a class="active" href="PracticeHistory.aspx?ActionType=mnuPracticeHistory"><i class="fa fa-history"></i> <span>ประวัติการฝึกปฏิบัติงานวิชาชีพ</span></a></li>
                <% Else%>
                    <li><a href="PracticeHistory.aspx?ActionType=mnuPracticeHistory"><i class="fa fa-history"></i> <span>ประวัติการฝึกปฏิบัติงานวิชาชีพ</span></a></li>  
                <% End If%>              
     
                <% If Request.QueryString("ActionType") = "mnuPrintBio" Then%>
                    <li class="active"><a class="active" href="ReportViewerStudentBio.aspx?ActionType=mnuPrintBio" ><i class="fa fa-print"></i> <span>พิมพ์ประวัติ</span></a> </li>
                <% Else%>
                    <li><a href="ReportViewerStudentBio.aspx?ActionType=mnuPrintBio"><i class="fa fa-print"></i> <span>พิมพ์ประวัติ</span></a> </li>
                <% End If%>               

                <% If Request("ActionType") = "loc" Then%>
                    <li class="active"><a href="LocationList.aspx?ActionType=loc"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
                <% Else %>
                    <li><a href="LocationList.aspx?ActionType=loc"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
                <% End If %>  

                <% If Request("ItemType") = "creg" Then%>
                    <li class="active"><a href="Courses_Reg.aspx?ItemType=creg"><i class="fa fa-book"></i>รายวิชาที่ลงทะเบียนฝึก</a></li>
                <% Else %>
                    <li><a href="Courses_Reg.aspx?ItemType=creg"><i class="fa fa-book"></i>รายวิชาที่ลงทะเบียนฝึก</a></li>
                <% End If %>  



                <% If Request.QueryString("ActionType") = "slt" Then  %>
                    <li class="active treeview">
                <% Else %>
                    <li class="treeview">
                <% End If %>        
                    <a href="#">
                        <i class="fa fa-check-square-o"></i> <span>การเลือกแหล่งฝึก</span>
                        <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                        </span>
                    </a>
                    <ul class="treeview-menu">
                        <% If Request("ItemType") = "1" Then%>
                            <li class="active"><a href="StudentLocation_Manage.aspx?ActionType=slt&ItemType=1"><i class="fa fa-check"></i> เลือกแหล่งฝึกงาน </a></li>
                        <% Else %>
                            <li><a href="StudentLocation_Manage.aspx?ActionType=slt&ItemType=1"><i class="fa fa-check"></i> เลือกแหล่งฝึกงาน</a></li>
                        <% End If %>
                 
                      <% If Request("ItemType") = "2" Then%>
                        <li class="active"><a href="StudentLocation_Manage.aspx?ActionType=slt&ItemType=2"><i class="fa fa-th-list"></i>จัดการแหล่งฝึกที่เลือก</a></li>
                      <% Else %>
                        <li><a href="StudentLocation_Manage.aspx?ActionType=slt&ItemType=2"><i class="fa fa-th-list"></i>จัดการแหล่งฝึกที่เลือก</a></li>
                      <% End If %> 

                       <% If Request("ItemType") = "3" Then%>
                        <li class="active"><a href="ResultStudent.aspx?ActionType=slt&ItemType=3"><i class="fa fa-star"></i>ผลการคัดเลือก/จัดสรร</a></li>
                      <% Else %>
                        <li><a href="ResultStudent.aspx?ActionType=slt&ItemType=3"><i class="fa fa-star"></i>ผลการคัดเลือก/จัดสรร</a></li>
                      <% End If %> 

                </ul>     
            </li>

                  <% If Request("ItemType") = "asmst" Then%>
                      <li class="active"><a href="StudentAsmStatus.aspx?ItemType=asmst"><i class="glyphicon glyphicon-heart"></i>สถานะการประเมิน <small class="label bg-red">new</small></a>  </li>
                  <% Else %>
                      <li><a href="StudentAsmStatus.aspx?ItemType=asmst"><i class="glyphicon glyphicon-heart"></i>สถานะการประเมิน <small class="label bg-red">new</small></a>  </li>
                  <% End If %>  

      <!-- menu PC2 for student here --->
         <!-- รายงานสำหรับนิสิต -->
    
       <% If Request("ActionType") = "rpt" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-pie-chart"></i> <span>รายงาน</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
                      
      <% If Request("ItemType") = "rpt1" Then%>
          <li class="active"><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt1"><i class="fa fa-bar-chart"></i>สรุปข้อมูลความต้องการของแหล่งฝึก</a></li>
      <% Else%>
          <li><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt1"><i class="fa fa-bar-chart"></i>สรุปข้อมูลความต้องการของแหล่งฝึก</a></li>
      <% End If%>            
     
      <% If Request("ItemType") = "rpt2" Then%>
          <li class="active"><a href="ReportStudentLocation.aspx?ActionType=rpt&ItemType=rpt2"><i class="fa fa-area-chart"></i>การเลือกแหล่งฝึกของนิสิต</a></li>
      <% Else%>
          <li><a href="ReportStudentLocation.aspx?ActionType=rpt&ItemType=rpt2"><i class="fa fa-area-chart"></i>การเลือกแหล่งฝึกของนิสิต</a></li>
      <% End If%> 

              </ul>
             </li>
            <% If Request("ActionType") = "cert" Then%>
            <li class="active"><a href="ReportViewerStudentBio.aspx?ActionType=cert&m=cert"><i class="glyphicon glyphicon-print"></i>พิมพ์ใบรับรองฯ</a></li>
            <% Else%>
            <li><a href="ReportViewerStudentBio.aspx?ActionType=cert&m=cert"><i class="glyphicon glyphicon-print"></i>พิมพ์ใบรับรองฯ</a></li>
            <% End If%>

    <% End If %> <!-- End Student -->

<% If (Request.Cookies("ROLE_ADM").Value = False) Then %>

        <% If (Request.Cookies("ROLE_TCH").Value = True) Or (Request.Cookies("ROLE_ADV").Value = True) Then %>
                <% If Request("ItemType") = "bio" Then%>
                    <li class="active"><a href="StudentPrintBio.aspx?ActionType=std&ItemType=bio"><i class="fa fa-print"></i>พิมพ์ประวัตินิสิต</a></li>
                <% Else %>
                    <li><a href="StudentPrintBio.aspx?ActionType=std&ItemType=bio"><i class="fa fa-print"></i>พิมพ์ประวัตินิสิต</a></li>
                <% End If %> 

       <% If Request("ActionType") = "viewloc" Then%>
       <li class="active"><a href="LocationList.aspx?ActionType=viewloc"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
   <% Else %>
       <li><a href="LocationList.aspx?ActionType=viewloc"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
   <% End If %>

        <% End If %>

        <% If Request.Cookies("ROLE_PCT").Value = True Then %>

                  <% If Request("ItemType") = "1"  %>
                    <li class="active"><a href="Location.aspx?ActionType=pct&ItemType=1"><i class="fa fa-home"></i>จัดการข้อมูลแหล่งฝึก</a></li>
                  <% Else %>
                  <li><a href="Location.aspx?ActionType=pct&ItemType=1"><i class="fa fa-home"></i>จัดการข้อมูลแหล่งฝึก</a></li>
                  <% End If %> 
        
   
             <% If Request.QueryString("ActionType") = "req" Then  %>
                    <li class="active treeview">
             <% Else %>
                    <li class="treeview">
             <% End If %>
                        
                <a href="#">
                <i class="fa fa-exchange"></i> <span>ความต้องการรับนิสิต</span>
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu">                	        
            
                <% If Request("ItemType") = "add" Then%>
                    <li class="active"><a href="Requirements.aspx?ActionType=req&ItemType=add"> <i class="fa fa-plus"></i>บันทึกจำนวนนิสิตที่รับได้</a></li>
                <% Else %>
                    <li><a href="Requirements.aspx?ActionType=req&ItemType=add"><i class="fa fa-plus"></i>บันทึกจำนวนนิสิตที่รับได้</a></li>
                <% End If %>                  

              </ul>     
                </li>
     


                   <% If Request("ActionType") = "2"  %>
                    <li class="active"><a href="ResultProceptor.aspx?ActionType=2"><i class="fa fa-user-md"></i>รายชื่อนิสิตที่เข้ามาฝึก</a></li>
                  <% Else %>
                  <li><a href="ResultProceptor.aspx?ActionType=2"><i class="fa fa-user-md"></i>รายชื่อนิสิตที่เข้ามาฝึก</a></li>
                  <% End If %>


                    <% If Request("ActionType") = "3"  %>
                    <li class="active"><a href="AssessmentGate.aspx?ActionType=3"><i class="fa fa-check-square-o"></i>ทำแบบประเมิน</a></li>
                  <% Else %>
                  <li><a href="AssessmentGate.aspx?ActionType=3"><i class="fa fa-check-square-o"></i>ทำแบบประเมิน</a></li>
                  <% End If %>

                  <% If Request("ActionType") = "5"  %>
                    <li class="active"><a href="AssesseeComment.aspx?ActionType=5"><i class="fa fa-dot-circle-o"></i>ประเมินจุดเด่น/จุดด้อย</a></li>
                  <% Else %>
                      <li><a href="AssesseeComment.aspx?ActionType=5"><i class="fa fa-dot-circle-o"></i>ประเมินจุดเด่น/จุดด้อย</a></li>
                  <% End If %>
    

                    <% If Request("ActionType") = "4"  %>
                    <li class="active"><a href="AssessmentResultPreceptor.aspx?ActionType=4"><i class="fa fa-th"></i>สรุปคะแนนประเมิน</a></li>
                  <% Else %>
                  <li><a href="AssessmentResultPreceptor.aspx?ActionType=4"><i class="fa fa-th"></i>สรุปคะแนนประเมิน</a></li>
                  <% End If %>    
      
                <% If Request("ActionType") = "rpct" Then  %>
                     <li class="active treeview">
                <% Else %>
                      <li class="treeview">
                <% End If %>

                     <a href="#"><i class="fa fa-pie-chart"></i><span>รายงาน</span>
                         <span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span>
                     </a>
                     <ul class="treeview-menu">
                         <% If Request("ItemType") = "rpct1" Then%>
                             <li class="active"><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct1"><i class="fa fa-bar-chart"></i>คะแนนประเมินตามหัวข้อประเมิน</a></li>
                         <% Else%>
                             <li><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct1"><i class="fa fa-bar-chart"></i>คะแนนประเมินตามหัวข้อประเมิน</a></li>
                         <% End If%>
                     
                         <% If Request("ItemType") = "rpct2" Then%>
                             <li class="active"><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct2"><i class="fa fa-bar-chart"></i>สรุปคะแนนประเมินรายแบบประเมิน</a></li>
                         <% Else%>
                             <li><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct2"><i class="fa fa-bar-chart"></i>สรุปคะแนนประเมินรายแบบประเมิน</a></li>
                         <% End If%>
                     </ul>
                 </li>
    
           <% End If %>
                <!-- end of Preceptor -->     
<% End If %>
    
     <!-- Start of Admin and Super Admin -->        
<% If Request.Cookies("ROLE_ADM").Value = True Or Request.Cookies("ROLE_SPA").Value = True Then%>           
    
      <% If Request.QueryString("ActionType") = "std" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-user-md"></i> <span>นิสิต</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "std" Then%>
        <li class="active"><a href="Student.aspx?ActionType=std&ItemType=std"> <i class="fa fa-user "></i>ข้อมูลนิสิต </a></li>
      <% Else %>
        <li><a href="Student.aspx?ActionType=std&ItemType=std"><i class="fa fa-user"></i> ข้อมูลนิสิต</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "bio" Then%>
        <li class="active"><a href="StudentPrintBio.aspx?ActionType=std&ItemType=bio"><i class="fa fa-print"></i>พิมพ์ประวัตินิสิต</a></li>
      <% Else %>
        <li><a href="StudentPrintBio.aspx?ActionType=std&ItemType=bio"><i class="fa fa-print"></i>พิมพ์ประวัตินิสิต</a></li>
      <% End If %> 
    
      <% If Request("ItemType") = "cert" Then%>
          <li class="active"><a href="StudentPrintCert.aspx?ActionType=std&ItemType=cert&m=cert"><i class="fa fa-certificate"></i>พิมพ์ใบรับรอง</a></li>
      <% Else%>
          <li><a href="StudentPrintCert.aspx?ActionType=std&ItemType=cert&m=cert"><i class="fa fa-certificate"></i>พิมพ์ใบรับรอง</a></li>
      <% End If%> 

          </ul>     
            </li>


     <% If Request.QueryString("ActionType") = "tch" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-user-secret"></i> <span>อาจารย์</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "tch" Then%>
        <li class="active"><a href="TeacherData.aspx?ActionType=tch&ItemType=tch"> <i class="fa fa-user-md"></i>ข้อมูลอาจารย์ </a></li>
      <% Else %>
        <li><a href="TeacherData.aspx?ActionType=tch&ItemType=tch"><i class="fa fa-user-md"></i> ข้อมูลอาจารย์</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "pct" Then%>
        <li class="active"><a href="PreceptorList.aspx?ActionType=tch&ItemType=pct"><i class="fa fa-stethoscope"></i>ข้อมูลอาจารย์แหล่่งฝึก</a></li>
      <% Else %>
        <li><a href="PreceptorList.aspx?ActionType=tch&ItemType=pct"><i class="fa fa-stethoscope"></i>ข้อมูลอาจารย์แหล่่งฝึก</a></li>
      <% End If %>  
          </ul>     
            </li>

     
                <% If Request.QueryString("ActionType") = "sj" Then%>
                    <li class="active"><a href="Subjects.aspx?ActionType=sj"><i class="fa fa-book"></i> <span>ข้อมูลรายวิชา (Master)</span></a></li>
                <% Else%>
                    <li><a href="Subjects.aspx?ActionType=sj"><i class="fa fa-book"></i> <span>ข้อมูลรายวิชา (Master)</span></a></li>
                <% End If%>
            



     <% If Request.QueryString("ActionType") = "loc" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-hospital-o"></i> <span>แหล่งฝึก</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "grp" Then%>
        <li class="active"><a href="LocationGroup.aspx?ActionType=loc&ItemType=grp"> <i class="fa fa-bank"></i>ประเภทแหล่งฝึก</a></li>
      <% Else %>
        <li><a href="LocationGroup.aspx?ActionType=loc&ItemType=grp"><i class="fa fa-bank"></i>ประเภทแหล่งฝึก</a></li>
      <% End If %>

               <% If Request("ItemType") = "wrk" Then%>
        <li class="active"><a href="LocationWork.aspx?ActionType=loc&ItemType=wrk"> <i class="fa fa-anchor"></i>ส่วนงานของแหล่งฝึก</a></li>
      <% Else %>
        <li><a href="LocationWork.aspx?ActionType=loc&ItemType=wrk"><i class="fa fa-anchor"></i>ส่วนงานของแหล่งฝึก</a></li>
      <% End If %>

                 
      <% If Request("ItemType") = "zone" Then%>
        <li class="active"><a href="LocationZone.aspx?ActionType=loc&ItemType=zone"><i class="fa fa-map-marker"></i>โซน</a></li>
      <% Else %>
        <li><a href="LocationZone.aspx?ActionType=loc&ItemType=zone"><i class="fa fa-map-marker"></i>โซน</a></li>
      <% End If %>  

        <% If Request("ItemType") = "bra" Then%>
        <li class="active"><a href="LocationOffice.aspx?ActionType=loc&ItemType=bra"><i class="fa fa-flag"></i>กลุ่มสาขา</a></li>
      <% Else %>
        <li><a href="LocationOffice.aspx?ActionType=loc&ItemType=bra"><i class="fa fa-flag"></i>กลุ่มสาขา</a></li>
      <% End If %>  
                <% If Request("ItemType") = "l" Then%>
        <li class="active"><a href="LocationList.aspx?ActionType=loc&ItemType=l"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
      <% Else %>
        <li><a href="LocationList.aspx?ActionType=loc&ItemType=l"><i class="fa fa-hospital-o"></i>แหล่งฝึก</a></li>
      <% End If %>  

          </ul>     
            </li>

    

     <% If Request.QueryString("ActionType") = "phase" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-clock-o"></i> <span>ผลัดฝึก</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "tn" Then%>
        <li class="active"><a href="TurnPhase.aspx?ActionType=phase&ItemType=tn"> <i class="fa fa-hourglass-3"></i>กำหนดผลัดฝึก</a></li>
      <% Else %>
        <li><a href="TurnPhase.aspx?ActionType=phase&ItemType=tn"><i class="fa fa-hourglass-3"></i>กำหนดผลัดฝึก</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "phase" Then%>
        <li class="active"><a href="TimePhase.aspx?ActionType=phase&ItemType=phase"><i class="fa fa-mouse-pointer"></i>กำหนดเปิดผลัดฝึกให้รายวิชา</a></li>
      <% Else %>
        <li><a href="TimePhase.aspx?ActionType=phase&ItemType=phase"><i class="fa fa-mouse-pointer"></i>กำหนดเปิดผลัดฝึกให้รายวิชา</a></li>
      <% End If %>            

          </ul>     
            </li>


     <% If Request.QueryString("ActionType") = "setup" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-database"></i> <span>กำหนดข้อมูลประจำปี</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "y1" Then%>
        <li class="active"><a href="Courses.aspx?ActionType=setup&ItemType=y1"> <i class="fa fa-book"></i>กำหนดรายวิชาที่เปิดฝึกงาน</a></li>
      <% Else %>
        <li><a href="Courses.aspx?ActionType=setup&ItemType=y1"><i class="fa fa-book"></i>กำหนดรายวิชาที่เปิดฝึกงาน</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "y2" Then%>
        <li class="active"><a href="CourseCoordinator.aspx?ActionType=setup&ItemType=y2"><i class="fa fa-user-md"></i>กำหนดอาจารย์ประจำวิชา</a></li>
      <% Else %>
        <li><a href="CourseCoordinator.aspx?ActionType=setup&ItemType=y2"><i class="fa fa-user-md"></i>กำหนดอาจารย์ประจำวิชา</a></li>
      <% End If %>            

  
      <% If Request("ItemType") = "y3" Then%>
        <li class="active"><a href="CourseStudent.aspx?ActionType=setup&ItemType=y3"><i class="fa fa-users"></i>กำหนดนิสิตในรายวิชา</a></li>
      <% Else %>
        <li><a href="CourseStudent.aspx?ActionType=setup&ItemType=y3"><i class="fa fa-users"></i>กำหนดนิสิตในรายวิชา</a></li>
      <% End If %>            


                
      <% If Request("ItemType") = "y4" Then%>
        <li class="active"><a href="CourseLocation.aspx?ActionType=setup&ItemType=y4"><i class="fa fa-hospital-o"></i>กำหนดแหล่งฝึกที่รับฝึก</a></li>
      <% Else %>
        <li><a href="CourseLocation.aspx?ActionType=setup&ItemType=y4"><i class="fa fa-hospital-o"></i>กำหนดแหล่งฝึกที่รับฝึก</a></li>
      <% End If %>            


                
      <% If Request("ItemType") = "y5" Then%>
        <li class="active"><a href="RequirementsReg.aspx?ActionType=setup&ItemType=y5"><i class="fa fa-male"></i>บันทึกจำนวนนิสิตที่แหล่งฝึกรับ</a></li>
      <% Else %>
        <li><a href="RequirementsReg.aspx?ActionType=setup&ItemType=y5"><i class="fa fa-male"></i>บันทึกจำนวนนิสิตที่แหล่งฝึกรับ</a></li>
      <% End If %>            

                          
      <% If Request("ItemType") = "y6" Then%>
        <li class="active"><a href="RequirementSearch.aspx?ActionType=setup&ItemType=y6"><i class="fa fa-search"></i>รายการจำนวนนิสิตที่แหล่งฝึกรับ</a></li>
      <% Else %>
        <li><a href="RequirementSearch.aspx?ActionType=setup&ItemType=y6"><i class="fa fa-search"></i>รายการจำนวนนิสิตที่แหล่งฝึกรับ</a></li>
      <% End If %>  
          </ul>     
            </li>

    
     <% If Request("ActionType") = "sl" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-check-square"></i> <span>ข้อมูลการเลือกแหล่งฝึก</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">

               <% If Request("ItemType") = "sl1" Then%>
        <li class="active"><a href="StudentRegister.aspx?ActionType=sl&ItemType=sl1"> <i class="fa fa-list-ol"></i>ข้อมูลการเลือกแหล่งฝึก</a></li>
      <% Else %>
        <li><a href="StudentRegister.aspx?ActionType=sl&ItemType=sl1"> <i class="fa fa-list-ol"></i>ข้อมูลการเลือกแหล่งฝึก</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "sl2" Then%>
        <li class="active"><a href="StudentNoRegister.aspx?ActionType=sl&ItemType=sl2"> <i class="fa fa-circle-o"></i>นิสิตที่ยังไม่ได้เลือกแหล่งฝึก</a></li>
      <% Else %>
        <li><a href="StudentNoRegister.aspx?ActionType=sl&ItemType=sl2"> <i class="fa fa-circle-o"></i>นิสิตที่ยังไม่ได้เลือกแหล่งฝึก</a></li>
      <% End If %>  
              </ul>
             </li>

    
     <% If Request("ActionType") = "asg" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-filter"></i> <span>การคัดเลือก/จัดสรร</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">

               <% If Request("ItemType") = "rnd" Then%>
        <li class="active"><a href="AssignRandom.aspx?ActionType=asg&ItemType=rnd"> <i class="fa fa-spinner"></i>คัดเลือก/จัดสรรด้วยการสุ่ม (Random)</a></li>
      <% Else %>
        <li><a href="AssignRandom.aspx?ActionType=asg&ItemType=rnd"> <i class="fa fa-spinner"></i>คัดเลือก/จัดสรรด้วยการสุ่ม (Random)</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "man" Then%>
        <li class="active"><a href="AssignManual.aspx?ActionType=asg&ItemType=man"> <i class="fa fa-external-link-square"></i>คัดเลือก/จัดสรรด้วยวิธี Manual</a></li>
      <% Else %>
        <li><a href="AssignManual.aspx?ActionType=asg&ItemType=man"> <i class="fa fa-external-link-square"></i>คัดเลือก/จัดสรรด้วยวิธี Manual</a></li>
      <% End If %>  
      <% If Request("ItemType") = "null" Then%>
        <li class="active"><a href="StudentNoAssessment.aspx?ActionType=asg&ItemType=null"> <i class="fa fa-star-o"></i>รายชื่อนิสิตที่ยังไม่ได้แหล่งฝึก</a></li>
      <% Else %>
        <li><a href="StudentNoAssessment.aspx?ActionType=asg&ItemType=null"> <i class="fa fa-star-o"></i>รายชื่อนิสิตที่ยังไม่ได้แหล่งฝึก</a></li>
      <% End If %>  
      <% If Request("ItemType") = "find" Then%>
        <li class="active"><a href="ReportAssesmentByStudent.aspx?ActionType=asg&ItemType=find"> <i class="fa fa-search"></i>ค้นหาผลการคัดเลือก/จัดสรร</a></li>
      <% Else %>
        <li><a href="ReportAssesmentByStudent.aspx?ActionType=asg&ItemType=find"> <i class="fa fa-search"></i>ค้นหาผลการคัดเลือก/จัดสรร</a></li>
      <% End If %>  
       <% If Request("ItemType") = "byp" Then%>
        <li class="active"><a href="AssignBypass.aspx?ActionType=asg&ItemType=byp"> <i class="fa fa-share-alt"></i>จัดสรรด้วยวิธีลัด (By Pass)</a></li>
      <% Else %>
        <li><a href="AssignBypass.aspx?ActionType=asg&ItemType=byp"> <i class="fa fa-share-alt"></i>จัดสรรด้วยวิธีลัด (By Pass)</a></li>
      <% End If %>  


              </ul>
             </li>
 <% End If %> 
 <% If (Request.Cookies("ROLE_ADM").Value = True) Or (Request.Cookies("ROLE_SPA").Value = True) Or (Request.Cookies("ROLE_ADV").Value = True) Then %>    

        <% If Request("ActionType") = "grdm" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-sort-alpha-asc"></i> <span>จัดการเกณฑ์ตัดเกรด</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
                <% If Request("ItemType") = "gd" Then%>
                    <li class="active"><a href="Grade.aspx?ActionType=grdm&ItemType=gd"><i class="fa fa-sort-alpha-asc"></i>Define Grade</a></li>
                <% Else%>
                    <li><a href="Grade.aspx?ActionType=grdm&ItemType=gd"><i class="fa fa-sort-alpha-asc"></i>Define Grade</a></li>
                <% End If%>    
              
                <% If Request("ItemType") = "gdc" Then%>
                <li class="active"><a href="GradeCopy.aspx?ActionType=grdm&ItemType=gdc"><i class="fa fa-sort-alpha-asc"></i>Grade Copy</a></li>
                <% Else%>
                    <li><a href="GradeCopy.aspx?ActionType=grdm&ItemType=gdc"><i class="fa fa-sort-alpha-asc"></i>Grade Copy</a></li>
                <% End If%>   


               <% If Request("ItemType") = "gdp" Then%>
                    <li class="active"><a href="GradePreceptor.aspx?ActionType=grdm&ItemType=gdp"><i class="fa fa-sort-alpha-asc"></i>Define Grade (แหล่งฝึก)</a></li>
                <% Else%>
                    <li><a href="GradePreceptor.aspx?ActionType=grdm&ItemType=gdp"><i class="fa fa-sort-alpha-asc"></i>Define Grade (แหล่งฝึก)</a></li>
                <% End If%>   

                <% If Request("ItemType") = "gdpc" Then%>
               <li class="active"><a href="GradePreceptorCopy.aspx?ActionType=grdm&ItemType=gdpc"><i class="fa fa-sort-alpha-asc"></i>Grade Copy(แหล่งฝึก)</a></li>
                <% Else%>
                    <li><a href="GradePreceptorCopy.aspx?ActionType=grdm&ItemType=gdpc"><i class="fa fa-sort-alpha-asc"></i>Grade Copy (แหล่งฝึก)</a></li>
                <% End If%> 

                </ul>
             </li>

     <% End If %> 

 <% If (Request.Cookies("ROLE_ADM").Value = True) Or (Request.Cookies("ROLE_SPA").Value = True) Then %>    
             <% If Request("ActionType") = "eva" Then  %>
                <li class="active treeview">
             <% Else %>
                 <li class="treeview">
             <% End If %>        
                  <a href="#">
                    <i class="fa fa-line-chart"></i> <span>จัดการแบบประเมิน</span>
                    <span class="pull-right-container">
                      <i class="fa fa-angle-left pull-right"></i>
                    </span>
                  </a>
                  <ul class="treeview-menu">

     
             
              <% If Request("ItemType") = "ev" Then%>
                <li class="active"><a href="EvaluationTopic.aspx?ActionType=eva&ItemType=ev"><i class="fa fa-trophy"></i>หัวข้อการประเมิน</a></li>
              <% Else %>
                <li><a href="EvaluationTopic.aspx?ActionType=eva&ItemType=ev"><i class="fa fa-trophy"></i>หัวข้อการประเมิน</a></li>
              <% End If %>


              <% If Request("ItemType") = "evg" Then%>
                <li class="active"><a href="EvaluationGroup.aspx?ActionType=eva&ItemType=evg"><i class="fa fa-th-large"></i>กลุ่มแบบประเมิน</a></li>
              <% Else %>
                <li><a href="EvaluationGroup.aspx?ActionType=eva&ItemType=evg"><i class="fa fa-th-large"></i>กลุ่มแบบประเมิน</a></li>
              <% End If %>
        
              <% If Request("ItemType") = "frm" Then%>
                <li class="active"><a href="EvaluationForm.aspx?ActionType=eva&ItemType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% Else %>
                <li><a href="EvaluationForm.aspx?ActionType=eva&ItemType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% End If %>
       

              <% If Request("ItemType") = "evas" Then%>
                <li class="active"><a href="EvaluationSubject.aspx?ActionType=eva&ItemType=evas"><i class="fa fa-paperclip"></i>ผูกแบบประเมินกับรายวิชา</a></li>
              <% Else %>
                <li><a href="EvaluationSubject.aspx?ActionType=eva&ItemType=evas"><i class="fa fa-paperclip"></i>ผูกแบบประเมินกับรายวิชา</a></li>
              <% End If %>

              
              <% If Request("ItemType") = "evl" Then%>
                <li class="active"><a href="AssessmentSubject.aspx?ActionType=eva&ItemType=evl"><i class="fa fa-tasks"></i>รายการแบบประเมิน</a></li>
              <% Else %>
                <li><a href="AssessmentSubject.aspx?ActionType=eva&ItemType=evl"><i class="fa fa-tasks"></i>รายการแบบประเมิน</a></li>
              <% End If %>
     

              <% If Request("ItemType") = "frm" Then%>
                <li class="active"><a href="EvaluationFormList.aspx?ActionType=eva&ItemType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% Else %>
                <li><a href="EvaluationFormList.aspx?ActionType=eva&ItemType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% End If %> 


              </ul>
             </li>

    
<%--                  <% If Request("ActionType") = "pom"  %>
                    <li class="active"><a href="AssesseeComment.aspx?ActionType=pom"><i class="fa fa-dot-circle-o"></i>การประเมินจุดเด่น/จุดด้อย</a></li>
                  <% Else %>
                      <li><a href="AssesseeComment.aspx?ActionType=pom"><i class="fa fa-dot-circle-o"></i>การประเมินจุดเด่น/จุดด้อย</a></li>
                  <% End If %>--%>

<% End If %>  

    
        <% If (Request.Cookies("ROLE_ADV").Value = True) Then %> 
              <% If Request("ActionType") = "frm" Then%>
                <li class="active"><a href="EvaluationFormList.aspx?ActionType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% Else %>
                <li><a href="EvaluationFormList.aspx?ActionType=frm"><i class="fa fa-file-text"></i>แบบประเมิน</a></li>
              <% End If %>
        <% End If %> 

    <!-- End Role  -->
 
<% If (Request.Cookies("ROLE_ADV").Value = True) Then %>  
      <% If Request("ItemType") = "asg" Then%>
        <li class="active"><a href="AssessmentGate.aspx?ActionType=assm&ItemType=asg"> <i class="fa fa-check-square-o"></i>ทำการประเมิน</a></li>
      <% Else %>
        <li><a href="AssessmentGate.aspx?ActionType=assm&ItemType=asg"> <i class="fa fa-check-square-o"></i>ทำการประเมิน</a></li>
      <% End If %>                  
   <% End If %> 

  <% If (Request.Cookies("ROLE_ADV").Value = True) Or (Request.Cookies("ROLE_ADM").Value = True) Then %> 
    
         <% If Request("ActionType") = "ass" Then  %>
            <li class="active treeview">
         <% Else %>
             <li class="treeview">
         <% End If %>        
              <a href="#">
                <i class="fa fa-trophy"></i> <span>ผลการประเมิน</span>
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu">
        <% If (Request.Cookies("ROLE_ADM").Value = True) Then %> 

              <% If Request("ItemType") = "score" Then%>
                <li class="active"><a href="AssesseeListAdmin.aspx?ActionType=ass&ItemType=score"> <i class="fa fa-pencil"></i>ดู/แก้ไข คะแนนประเมิน</a></li>
              <% Else %>
                <li><a href="AssesseeListAdmin.aspx?ActionType=ass&ItemType=score"> <i class="fa fa-pencil"></i>ดู/แก้ไข คะแนนประเมิน</a></li>
              <% End If %>
                 
              <% If Request("ItemType") = "st" Then%>
                <li class="active"><a href="AssesseeStatus.aspx?ActionType=ass&ItemType=st"> <i class="fa fa-check"></i>สถานะผู้ถูกประเมิน</a></li>
              <% Else %>
                <li><a href="AssesseeStatus.aspx?ActionType=ass&ItemType=st"> <i class="fa fa-check"></i>สถานะผู้ถูกประเมิน</a></li>
              <% End If %>  

              <% If Request("ItemType") = "steva" Then%>
     <li class="active"><a href="StudentEvaluation.aspx?ActionType=ass&ItemType=steva"> <i class="fa fa-asterisk"></i>สถานะการประเมิน</a></li>
   <% Else %>
     <li><a href="StudentEvaluation.aspx?ActionType=ass&ItemType=steva"> <i class="fa fa-asterisk"></i>สถานะการประเมิน</a></li>
   <% End If %>
         <% End If %>  

        
          <% If Request("ItemType") = "sum" Then%>
            <li class="active"><a href="AssessmentResult.aspx?ActionType=ass&ItemType=sum"> <i class="fa fa-sitemap"></i>สรุปคะแนนประเมิน</a></li>
          <% Else %>
            <li><a href="AssessmentResult.aspx?ActionType=ass&ItemType=sum"> <i class="fa fa-sitemap"></i>สรุปคะแนนประเมิน</a></li>
          <% End If %>  
      

              </ul>
             </li>
   <% End If %> 

  <% If (Request.Cookies("ROLE_ADM").Value = True) Or (Request.Cookies("ROLE_SPA").Value = True) Or (Request.Cookies("ROLE_ADV").Value = True) Then %> 
        
         <% If Request("ActionType") = "grd" Then  %>
            <li class="active treeview">
         <% Else %>
             <li class="treeview">
         <% End If %>        
              <a href="#">
                <i class="fa fa-list-ol"></i><span>คะแนนประเมิน/เกรด</span>
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu">

              <% If Request("ItemType") = "mod" Then%>
                <li class="active"><a href="GradeModify.aspx?ActionType=grd&ItemType=mod"> <i class="fa fa-pencil"></i>ดู/แก้ไข</a></li>
              <% Else %>
                <li><a href="GradeModify.aspx?ActionType=grd&ItemType=mod"> <i class="fa fa-pencil"></i>ดู/แก้ไข</a></li>
              <% End If %>

              <% If Request("ItemType") = "rpsum" Then%>
                  <li class="active"><a href="ReportCondition.aspx?ActionType=grd&ItemType=rpsum"><i class="fa fa-bar-chart"></i>รายงานสรุปคะแนนประเมิน</a></li>
              <% Else%>
                  <li><a href="ReportCondition.aspx?ActionType=grd&ItemType=rpsum"><i class="fa fa-bar-chart"></i>รายงานสรุปคะแนนประเมิน</a></li>
              <% End If%>                    
                        

             
        <!--
          <% If Request("ItemType") = "sum" Then%>
            <li class="active"><a href="GradeFinal.aspx?ActionType=grd&ItemType=sum"> <i class="fa fa-sort-alpha-asc"></i>สรุปเกรด(Finalize)<small class="label bg-yellow">new</small></a> </li>
          <% Else %>
            <li><a href="GradeFinal.aspx?ActionType=grd&ItemType=sum"> <i class="fa fa-sort-alpha-asc"></i>สรุปเกรด(Finalize)<small class="label bg-yellow">new</small></a> </li>
          <% End If %>  
      -->

              </ul>
             </li>
     <% End If%> 

      <% If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then%>

     <% If Request.QueryString("ActionType") = "req" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-exchange"></i> <span>ความต้องการรับนิสิต</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">                	        
            
      <% If Request("ItemType") = "add" Then%>
        <li class="active"><a href="Requirements.aspx?ActionType=req&ItemType=add"> <i class="fa fa-plus"></i>บันทึกความต้องการส่งนิสิต</a></li>
      <% Else %>
        <li><a href="Requirements.aspx?ActionType=req&ItemType=add"><i class="fa fa-plus"></i>บันทึกความต้องการส่งนิสิต</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "reql" Then%>
        <li class="active"><a href="RequirementList.aspx?ActionType=req&ItemType=reql"><i class="fa fa-envelope-o"></i>รายการความต้องการ</a></li>
      <% Else %>
        <li><a href="RequirementList.aspx?ActionType=req&ItemType=reql"><i class="fa fa-envelope-o"></i>รายการความต้องการ</a></li>
      <% End If %>            

              
      <% If Request("ItemType") = "etm" Then%>
        <li class="active"><a href="ReportREQByLocation.aspx?ActionType=req&ItemType=etm&m=etm"><i class="fa fa-table"></i>รายงานสรุปความต้องการ</a></li>
      <% Else %>
        <li><a href="ReportREQByLocation.aspx?ActionType=req&ItemType=etm&m=etm"><i class="fa fa-table"></i>รายงานสรุปความต้องการ</a></li>
      <% End If %>     


          </ul>     
            </li>

    
         <% If Request("ActionType") = "rpt" Then  %>
            <li class="active treeview">
         <% Else %>
             <li class="treeview">
         <% End If %>        
              <a href="#">
                <i class="fa fa-pie-chart"></i> <span>รายงาน</span>
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu">
                      
      <% If Request("ItemType") = "rpt1" Then%>
          <li class="active"><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt1"><i class="fa fa-bar-chart"></i>สรุปข้อมูลความต้องการของแหล่งฝึก</a></li>
      <% Else%>
          <li><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt1"><i class="fa fa-bar-chart"></i>สรุปข้อมูลความต้องการของแหล่งฝึก</a></li>
      <% End If%>            
     
      <% If Request("ItemType") = "rpt2" Then%>
          <li class="active"><a href="ReportStudentLocation.aspx?ActionType=rpt&ItemType=rpt2"><i class="fa fa-area-chart"></i>การเลือกแหล่งฝึกของนิสิต</a></li>
      <% Else%>
          <li><a href="ReportStudentLocation.aspx?ActionType=rpt&ItemType=rpt2"><i class="fa fa-area-chart"></i>การเลือกแหล่งฝึกของนิสิต</a></li>
      <% End If%> 

              
      <% If Request("ItemType") = "rpt3" Then%>
          <li class="active"><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt3&m=manage"><i class="fa fa-street-view"></i>การจัดสรรนิสิตประจำแหล่งฝึก</a></li>
      <% Else%>
          <li><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt3&m=manage"><i class="fa fa-street-view"></i>การจัดสรรนิสิตประจำแหล่งฝึก</a></li>
      <% End If%> 
              
      <% If Request("ItemType") = "rpt4" Then%>
          <li class="active"><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt4&m=stdassessment"><i class="fa fa-venus-mars"></i>การจัดสรรแหล่งฝึกแยกตามรายชื่อนิสิต</a></li>
      <% Else%>
          <li><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt4&m=stdassessment"><i class="fa fa-venus-mars"></i>การจัดสรรแหล่งฝึกแยกตามรายชื่อนิสิต</a></li>
      <% End If%> 
              
      <% If Request("ItemType") = "rpt5" Then%>
          <li class="active"><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt5&m=docstdlist"><i class="fa fa-file-pdf-o"></i>เอกสารแนบใบส่งตัว</a></li>
      <% Else%>
          <li><a href="ReportREQByLocation.aspx?ActionType=rpt&ItemType=rpt5&m=docstdlist"><i class="fa fa-file-pdf-o"></i>เอกสารแนบใบส่งตัว</a></li>
      <% End If%> 
              
     
              
      <% If Request("ItemType") = "rpt7" Then%>
          <li class="active"><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt7&m=timephase"><i class="fa fa-umbrella"></i>รายงานช่วงเวลาที่มีนิสิตฝึก</a></li>
      <% Else%>
          <li><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt7&m=timephase"><i class="fa fa-umbrella"></i>รายงานช่วงเวลาที่มีนิสิตฝึก</a></li>
      <% End If%> 
              
      <% If Request("ItemType") = "rpt8" Then%>
          <li class="active"><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt8&m=phasecount"><i class="fa fa-tree"></i>สรุปจำนวนนิสิตแต่ละผลัดฝึก</a></li>
      <% Else%>
          <li><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt8&m=phasecount"><i class="fa fa-tree"></i>สรุปจำนวนนิสิตแต่ละผลัดฝึก</a></li>
      <% End If%> 
                   
      <% If Request("ItemType") = "rpt10" Then%>
          <li class="active"><a href="ReportConditionByLevelClass.aspx?ActionType=rpt&ItemType=rpt10&m=hour"><i class="fa fa-hourglass-end"></i>จำนวนชั่วโมงฝึก</a></li>
      <% Else%>
          <li><a href="ReportConditionByLevelClass.aspx?ActionType=rpt&ItemType=rpt10&m=hour"><i class="fa fa-hourglass-end"></i>จำนวนชั่วโมงฝึก</a></li>
      <% End If%> 
            
                  
   <%--   <% If Request("ItemType") = "rpt11" Then%>
          <li class="active"><a href="StudentPrintCert.aspx?ActionType=rpt&ItemType=rpt11&m=cert"><i class="fa fa-certificate"></i>ใบรับรอง</a></li>
      <% Else%>
          <li><a href="StudentPrintCert.aspx?ActionType=rpt&ItemType=rpt11&m=cert"><i class="fa fa-certificate"></i>ใบรับรอง</a></li>
      <% End If%> 

      <% If Request("ItemType") = "rpt12" Then%>
          <li class="active"><a href="StudentPrintBio.aspx?ActionType=rpt&ItemType=rpt12&m=certregis"><i class="fa fa-file-text-o"></i>รายงานสำหรับสอบใบประกอบวิชาชีพ</a></li>
      <% Else%>
          <li><a href="StudentPrintBio.aspx?ActionType=rpt&ItemType=rpt12&m=certregis"><i class="fa fa-file-text-o"></i>รายงานสำหรับสอบใบประกอบวิชาชีพ</a></li>
      <% End If%> --%>




      
               <% If Request("ItemType") = "rpt6" Then%>
                  <li class="active"><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt6&m=payamount"><i class="fa fa-ppaypal"></i>รายงานคำนวณค่าตอบแทน</a></li>
              <% Else%>
                  <li><a href="ReportConditionByYear.aspx?ActionType=rpt&ItemType=rpt6&m=payamount"><i class="fa fa-paypal"></i>รายงานคำนวณค่าตอบแทน</a></li>
              <% End If%>         
              <% If Request("ItemType") = "rpt9" Then%>
                  <li class="active"><a href="ReportConditionByCourse.aspx?ActionType=rpt&ItemType=rpt9&m=paymentcourse"><i class="fa fa-money"></i>สรุปค่าตอบแทนแยกตามรายวิชา</a></li>
              <% Else%>
                  <li><a href="ReportConditionByCourse.aspx?ActionType=rpt&ItemType=rpt9&m=paymentcourse"><i class="fa fa-money"></i>สรุปค่าตอบแทนแยกตามรายวิชา</a></li>
              <% End If%> 

              <% If Request("ItemType") = "rpt13" Then%>
                  <li class="active"><a href="ReportConditionByLocationPhase.aspx?ActionType=rpt&ItemType=rpt13&m=weak"><i class="fa fa-odnoklassniki"></i>รายงานประเมินจุดเด่น/จุดด้อย</a></li>
              <% Else%>
                  <li><a href="ReportConditionByLocationPhase.aspx?ActionType=rpt&ItemType=rpt13&m=weak"><i class="fa fa-odnoklassniki"></i>รายงานประเมินจุดเด่น/จุดด้อย</a></li>
              <% End If%> 



       

              </ul>
             </li>

  <% End If %> 



 <% If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then%>

        
        <% If Request.QueryString("ActionType") = "mail" Then%>
            <li class="active"><a href="AlertSetting.aspx?ActionType=mail"><i class="fa fa-envelope"></i> <span>Email Alerts</span> <small class="label label-danger ml-1">new</small></a></li>
        <% Else%>
            <li><a href="AlertSetting.aspx?ActionType=mail"><i class="fa fa-envelope"></i> <span>Email Alerts</span> <small class="label label-danger ml-1">new</small></a></li>
        <% End If%>    

    
     <% If Request("ActionType") = "doc" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-file"></i> <span>จดหมาย/เอกสาร</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">     

      <% If Request("ItemType") = "doc1" Then%>
        <li class="active"><a href="Document.aspx?ActionType=doc&ItemType=doc1"><i class="fa fa-envelope"></i> จดหมาย/เอกสาร</a></li>
      <% Else %>
        <li><a href="Document.aspx?ActionType=doc&ItemType=doc1"> <i class="fa fa-envelope"></i>จดหมาย/เอกสาร</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "doc2" Then%>
        <li class="active"><a href="DocumentList.aspx?ActionType=doc&ItemType=doc2"><i class="fa fa-file-text-o"></i>สร้างจดหมาย</a></li>
      <% Else %>
        <li><a href="DocumentList.aspx?ActionType=doc&ItemType=doc2"><i class="fa fa-file-text-o"></i>สร้างจดหมาย</a></li>
      <% End If %>  

      <% If Request("ItemType") = "doc3" Then%>
        <li class="active"><a href="DocumentPrint.aspx?ActionType=doc&ItemType=doc3"><i class="fa fa-send"></i>รายการพิมพ์เอกสาร</a></li>
      <% Else %>
        <li><a href="DocumentPrint.aspx?ActionType=doc&ItemType=doc3"><i class="fa fa-send"></i>รายการพิมพ์เอกสาร</a></li>
      <% End If %>  
       <% If Request("ItemType") = "doc4" Then%>
        <li class="active"><a href="DocumentConfig.aspx?ActionType=doc&ItemType=doc4"><i class="fa fa-folder-open"></i>ตั้งค่า Directory path</a></li>
      <% Else %>
        <li><a href="DocumentConfig.aspx?ActionType=doc&ItemType=doc4"><i class="fa fa-folder-open"></i>ตั้งค่า Directory path</a></li>
      <% End If %>     
               

       </ul></li>

     
     <% If Request("ActionType") = "setting" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-gears"></i> <span>ตั้งค่าข้อมูลพื้นฐาน</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">     


      <% If Request("ItemType") = "prov" Then%>
        <li class="active"><a href="Provinces.aspx?ActionType=setting&ItemType=prov"><i class="fa fa-map-marker"></i>จังหวัด</a></li>
      <% Else %>
        <li><a href="Provinces.aspx?ActionType=setting&ItemType=prov"><i class="fa fa-map-marker"></i>จังหวัด</a></li>
      <% End If %>  
       <% If Request("ItemType") = "pre" Then%>
        <li class="active"><a href="Prefix.aspx?ActionType=setting&ItemType=pre"><i class="fa fa-get-pocket"></i>คำนำหน้าชื่อ</a></li>
      <% Else %>
        <li><a href="Prefix.aspx?ActionType=setting&ItemType=pre"><i class="fa fa-get-pocket"></i>คำนำหน้าชื่อ</a></li>
      <% End If %> 
       
      <% If Request("ItemType") = "pos" Then%>
        <li class="active"><a href="Positions.aspx?ActionType=setting&ItemType=pos"> <i class="fa fa-odnoklassniki"></i>ชื่อตำแหน่ง</a></li>
      <% Else %>
        <li><a href="Positions.aspx?ActionType=setting&ItemType=pos"> <i class="fa fa-odnoklassniki"></i>ชื่อตำแหน่ง</a></li>
      <% End If %>
                 
      <% If Request("ItemType") = "dept" Then%>
        <li class="active"><a href="Department.aspx?ActionType=setting&ItemType=dept"><i class="fa fa-briefcase"></i>ชื่อภาควิชา</a></li>
      <% Else %>
        <li><a href="Department.aspx?ActionType=setting&ItemType=dept"><i class="fa fa-briefcase"></i>ชื่อภาควิชา</a></li>
      <% End If %>  



      <% If Request("ItemType") = "lev" Then%>
        <li class="active"><a href="DataConfigLevel.aspx?ActionType=setting&ItemType=lev"><i class="fa fa-stumbleupon"></i>กำหนดระดับชั้นนิสิต</a></li>
      <% Else %>
        <li><a href="DataConfigLevel.aspx?ActionType=setting&ItemType=lev"><i class="fa fa-stumbleupon"></i>กำหนดระดับชั้นนิสิต</a></li>
      <% End If %> 
            <% If Request("ItemType") = "pay" Then%>
        <li class="active"><a href="PaymentConfig.aspx?ActionType=setting&ItemType=pay"><i class="fa fa-dollar"></i>กำหนดค่าตอบแทน</a></li>
      <% Else %>
        <li><a href="PaymentConfig.aspx?ActionType=setting&ItemType=pay"><i class="fa fa-dollar"></i>กำหนดค่าตอบแทน</a></li>
      <% End If %>             
        <% If Request("ItemType") = "sys" Then%>
        <li class="active"><a href="DataConfig.aspx?ActionType=setting&ItemType=sys"><i class="fa fa-gears"></i>กำหนดค่าเริ่มต้นระบบ</a></li>
      <% Else %>
        <li><a href="DataConfig.aspx?ActionType=setting&ItemType=sys"><i class="fa fa-gears"></i>กำหนดค่าเริ่มต้นระบบ</a></li>
      <% End If %>   

     <% If Request("ItemType") = "cert" Then%>
          <li class="active"><a href="StudentCert.aspx?ActionType=setting&ItemType=cert"><i class="fa fa-certificate"></i>คั้งค่าใบรับรอง</a></li>
      <% Else%>
          <li><a href="StudentCert.aspx?ActionType=setting&ItemType=cert"><i class="fa fa-certificate"></i>คั้งค่าใบรับรอง</a></li>
      <% End If%> 

          </ul></li>

    
     <% If Request("ActionType") = "user" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-users"></i> <span>User Account</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">

              
               <% If Request("ItemType") = "role" Then%>
        <li class="active"><a href="Roles.aspx?ActionType=user&ItemType=role"> <i class="fa fa-key"></i>กำหนดสิทธิ์ (Role)</a></li>
      <% Else %>
        <li><a href="Roles.aspx?ActionType=user&ItemType=role"> <i class="fa fa-key"></i>กำหนดสิทธิ์ (Role)</a></li>
      <% End If %>


               <% If Request("ItemType") = "add" Then%>
        <li class="active"><a href="Users.aspx?ActionType=user&ItemType=add"> <i class="fa fa-users"></i>จัดการผู้ใช้งาน</a></li>
      <% Else %>
        <li><a href="Users.aspx?ActionType=user&ItemType=add"> <i class="fa fa-users"></i>จัดการผู้ใช้งาน</a></li>
      <% End If %>
                 
     <% If Request("ItemType") = "user1" Then%>
        <li class="active"><a href="UserStudent.aspx?ActionType=user&ItemType=user1"> <i class="fa fa-users"></i>สำหรับนิสิต</a></li>
      <% Else %>
        <li><a href="UserStudent.aspx?ActionType=user&ItemType=user1"> <i class="fa fa-users"></i>สำหรับนิสิต</a></li>
      <% End If %>

                   <% If Request("ItemType") = "user2" Then%>
        <li class="active"><a href="UserAdvisor.aspx?ActionType=user&ItemType=user2"> <i class="fa fa-user-md"></i>สำหรับอาจารย์และบุคลากร(จุฬา)</a></li>
      <% Else %>
        <li><a href="UserAdvisor.aspx?ActionType=user&ItemType=user2"> <i class="fa fa-user-md"></i>สำหรับอาจารย์และบุคลากร(จุฬา)</a></li>
      <% End If %>

                   <% If Request("ItemType") = "user3" Then%>
        <li class="active"><a href="UserPreceptor.aspx?ActionType=user&ItemType=user3"> <i class="fa fa-stethoscope"></i>Advisor to Preceptor</a></li>
      <% Else %>
        <li><a href="UserPreceptor.aspx?ActionType=user&ItemType=user3"> <i class="fa fa-stethoscope"></i>Advisor to Preceptor</a></li>
      <% End If %>



      <% If Request("ItemType") = "history" Then%>
        <li class="active"><a href="LogfilesByUser.aspx?ActionType=user&ItemType=history"> <i class="fa fa-font"></i>ตรวจสอบประวัติการใช้งาน</a></li>
      <% Else %>
        <li><a href="LogfilesByUser.aspx?ActionType=user&ItemType=history"> <i class="fa fa-font"></i>ตรวจสอบประวัติการใช้งาน</a></li>
      <% End If %>  

        <% If Request("ItemType") = "pass" Then%>
        <li class="active"><a href="ChangeUsername.aspx?ActionType=user&ItemType=pass"> <i class="fa fa-coffee"></i>เปลี่ยน Username</a></li>
      <% Else %>
        <li><a href="ChangeUsername.aspx?ActionType=user&ItemType=pass"> <i class="fa fa-coffee"></i>เปลี่ยน Username</a></li>
      <% End If %>  

              </ul>
             </li> 

     
     <% If Request("ActionType") = "news" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-bullhorn"></i> <span>ข่าวประกาศ</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
             <% If Request("ItemType") = "news" Then%>
        <li class="active"><a href="News_List.aspx?ActionType=news&ItemType=news"> <i class="fa fa-rss"></i>รายการข่าวประกาศ</a></li>
      <% Else %>
        <li><a href="News_List.aspx?ActionType=news&ItemType=news"> <i class="fa fa-rss"></i>รายการข่าวประกาศ</a></li>
      <% End If %>

       <% If Request("ItemType") = "addnews" Then%>
        <li class="active"><a href="News_Manage.aspx?ActionType=news&ItemType=addnews"> <i class="fa fa-share-alt"></i>เพิ่มข่าวประกาศ </a></li>
      <% Else %>
        <li><a href="News_Manage.aspx?ActionType=news&ItemType=addnews"><i class="fa fa-share-alt"></i> เพิ่มข่าวประกาศ</a></li>
      <% End If %>
        </ul></li>

    
    <% End If %>
 
    
<% If (Request.Cookies("ROLE_TCH").Value = True) Or (Request.Cookies("ROLE_ADV").Value = True) Or (Request.Cookies("ROLE_ADM").Value = True) Or (Request.Cookies("ROLE_SPA").Value = True) Then %>
      
            <% If Request("ActionType") = "spv" Then  %>
                <li class="active treeview">
            <% Else %>
                <li class="treeview">
            <% End If %>        
          <a href="#">
            <i class="fa fa-skyatlas"></i> <span>การนิเทศ</span> 
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>

          <ul class="treeview-menu">
              <% If Request("ItemType") = "spv3" Then%>
                <li class="active"><a href="SupervisorSelect.aspx?ActionType=spv&ItemType=spv3"><i class="fa fa-location-arrow"></i>เลือกสถานที่นิเทศ</a></li>
                <% Else %>
                    <li><a href="SupervisorSelect.aspx?ActionType=spv&ItemType=spv3"><i class="fa fa-location-arrow"></i>เลือกสถานที่นิเทศ</a></li>
                <% End If %>   
              
                <% If Request("ItemType") = "spv4" Then%>
                <li class="active"><a href="SupervisionPlan.aspx?ActionType=spv&ItemType=spv4"><i class="fa fa-paper-plane-o"></i>แผนการนิเทศของท่าน</a></li>
                <% Else %>
                    <li><a href="SupervisionPlan.aspx?ActionType=spv&ItemType=spv4"><i class="fa fa-paper-plane-o"></i>แผนการนิเทศของท่าน</a></li>
                <% End If %>  

           
        <% If (Request.Cookies("ROLE_ADM").Value = True) Or (Request.Cookies("ROLE_SPA").Value = True) Then %>    

                <% If Request("ItemType") = "spv1" Then%>
                    <li class="active"><a href="SupervisionLocation.aspx?ActionType=spv&ItemType=spv1"><i class="fa fa-map-marker"></i> จัดการแหล่งฝึกที่ต้องนิเทศ </a></li>
                <% Else %>
                    <li><a href="SupervisionLocation.aspx?ActionType=spv&ItemType=spv1"><i class="fa fa-map-marker"></i>จัดการแหล่งฝึกที่ต้องนิเทศ</a></li>
                <% End If %>
                                       
                   <% If Request("ItemType") = "spv2" Then%>
                    <li class="active"><a href="SupervisorManage.aspx?ActionType=spv&ItemType=spv2"><i class="fa fa-car"></i>จัดสรรการออกนิเทศ (Manual)</a></li>
                  <% Else %>
                    <li><a href="SupervisorManage.aspx?ActionType=spv&ItemType=spv2"><i class="fa fa-car"></i>จัดสรรการออกนิเทศ (Manual)</a></li>
                  <% End If %>
                

                  <% If Request("ItemType") = "spv5" Then%>
                <li class="active"><a href="SupervisorSend.aspx?ActionType=spv&ItemType=spv5"><i class="fa fa-send"></i>ส่งงานนิเทศ</a></li>
              <% Else %>
                <li><a href="SupervisorSend.aspx?ActionType=spv&ItemType=spv5"><i class="fa fa-send"></i>ส่งงานนิเทศ</a></li>
              <% End If %>
              
        <% End If %> 
        
          </ul></li>
       <% End If %> 


<% If Request.Cookies("ROLE_STD").Value = True Or Request.Cookies("ROLE_ADM").Value = True Or Request.Cookies("ROLE_ADV").Value = True Or Request.Cookies("ROLE_TCH").Value = True Then%>
    
     <% If Request("ActionType") = "rcm" Then  %>
        <li class="active treeview">
     <% Else %>
         <li class="treeview">
     <% End If %>        
          <a href="#">
            <i class="fa fa-comments"></i> <span>Recommend</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
             <% If Request("ItemType") = "rcm1" Then%>
        <li class="active"><a href="Recommend.aspx?ActionType=rcm&ItemType=rcm1"><i class="lnr-apartment"></i> คำแนะนำแหล่งฝึก</a></li>
      <% Else %>
        <li><a href="Recommend.aspx?ActionType=rcm&ItemType=rcm1"><i class="lnr-apartment"></i> คำแนะนำแหล่งฝึก</a></li>
      <% End If %>

      <% If Request("ItemType") = "rcm2" Then%>
        <li class="active"><a href="RoomRecommend.aspx?ActionType=rcm&ItemType=rcm2"><i class="fa fa-home"></i>คำแนะนำที่พัก</a></li>
      <% Else %>
        <li><a href="RoomRecommend.aspx?ActionType=rcm&ItemType=rcm2"><i class="fa fa-home"></i> คำแนะนำที่พัก</a></li>
      <% End If %>
        </ul></li>  

     <% End If %>


<% If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then%>  
    <!-- menu PC2 here --->

        <% If Request.QueryString("ActionType") = "timeline" Then%>
            <li class="active"><a href="TimelineSetting.aspx?ActionType=timeline"><i class="fa fa-clock-o"></i> <span>กำหนด Timeline</span></a></li>
        <% Else%>
            <li><a href="TimelineSetting.aspx?ActionType=timeline"><i class="fa fa-clock-o"></i> <span>กำหนด Timeline</span></a></li>
        <% End If%> 
    <!--
        <% If Request.QueryString("ActionType") = "vpn" Then%>
            <li class="active"><a href="VPNList.aspx?ActionType=vpn"><i class="fa fa-cloud"></i> <span>VPN Manage</span></a></li>
        <% Else%>
            <li><a href="VPNList.aspx?ActionType=vpn"><i class="fa fa-cloud"></i> <span>VPN Manage</span></a></li>
        <% End If%>
    -->
<% End If%>  
    
       

        <% If Request.QueryString("ActionType") = "chg" Then%>
            <li class="active"><a href="ChangePassword.aspx?ActionType=chg"><i class="fa fa-key"></i> <span>เปลี่ยนรหัสผ่าน</span></a></li>
        <% Else%>
            <li><a href="ChangePassword.aspx?ActionType=chg"><i class="fa fa-key"></i> <span>เปลี่ยนรหัสผ่าน</span></a></li>
        <% End If%> 
                 
        <li><a href="Default.aspx?logout=YES"><i class="fa fa-power-off"></i> <span>ออกจากระบบ</span></a></li>
</ul>
