<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucMenuEn.ascx.vb" Inherits=".ucMenuEn" %>
    <link rel="stylesheet" type="text/css" href="css/custyles.css"> 
<ul class="sidebar-menu" data-widget="tree">
       <li class="header">MAIN NAVIGATION</li>

        <% If Request("actionType") = "h"  %>
            <li class="active"><a href="Home.aspx?actionType=h"><i class="fa fa-home"></i> <span>Home</span></a></li>
        <% Else %>
            <li><a href="Home.aspx?actionType=h"><i class="fa fa-home"></i> <span>Home</span></a></li> 
        <% End If %> 

        <% If Request.Cookies("ROLE_PCT").Value = True Then %>

                  <% If Request("ActionType") = "org"  %>
                    <li class="active"><a href="Location.aspx?ActionType=org"><i class="fa fa-home text-maroon"></i>Organization</a></li>
                  <% Else %>
                  <li><a href="Location.aspx?ActionType=org"><i class="fa fa-home"></i>Organization</a></li>
                  <% End If %> 
        
   
             <% If Request.QueryString("ActionType") = "req" Then  %>
                    <li class="active treeview">
             <% Else %>
                    <li class="treeview">
             <% End If %>
                        
                <a href="#">
                <i class="fa fa-exchange"></i> <span>Internship Requirements</span>
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu">                	        
            
                <% If Request("ItemType") = "add" Then%>
                    <li class="active"><a href="Requirements.aspx?ActionType=req&ItemType=add"> <i class="fa fa-plus text-maroon"></i>Set Internship Quota</a></li>
                <% Else %>
                    <li><a href="Requirements.aspx?ActionType=req&ItemType=add"><i class="fa fa-plus"></i>Set Internship Quota</a></li>
                <% End If %>                  

              </ul>     
                </li>   
                   <% If Request("ActionType") = "2"  %>
                    <li class="active"><a href="ResultProceptor.aspx?ActionType=2"><i class="fa fa-user-md text-maroon"></i>Intern List</a></li>
                  <% Else %>
                  <li><a href="ResultProceptor.aspx?ActionType=2"><i class="fa fa-user-md"></i>Intern List</a></li>
                  <% End If %>


                    <% If Request("ActionType") = "3"  %>
                    <li class="active"><a href="AssessmentGate.aspx?ActionType=3"><i class="fa fa-check-square-o text-maroon"></i>Evaluation</a></li>
                  <% Else %>
                  <li><a href="AssessmentGate.aspx?ActionType=3"><i class="fa fa-check-square-o"></i>Evaluation</a></li>
                  <% End If %>

                  <% If Request("ActionType") = "5"  %>
                    <li class="active"><a href="AssesseeComment.aspx?ActionType=5"><i class="fa fa-dot-circle-o text-maroon"></i>Strengths and Weaknesses</a></li>
                  <% Else %>
                      <li><a href="AssesseeComment.aspx?ActionType=5"><i class="fa fa-dot-circle-o"></i>Strengths and Weaknesses</a></li>
                  <% End If %>
    

                    <% If Request("ActionType") = "4"  %>
                    <li class="active"><a href="AssessmentResultPreceptor.aspx?ActionType=4"><i class="fa fa-th text-maroon"></i>Evaluation Summary</a></li>
                  <% Else %>
                  <li><a href="AssessmentResultPreceptor.aspx?ActionType=4"><i class="fa fa-th"></i>Evaluation Summary</a></li>
                  <% End If %>    
      
                <% If Request("ActionType") = "rpct" Then  %>
                     <li class="active treeview">
                <% Else %>
                      <li class="treeview">
                <% End If %>

                     <a href="#"><i class="fa fa-pie-chart"></i><span>Reports</span>
                         <span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span>
                     </a>
                     <ul class="treeview-menu">
                         <% If Request("ItemType") = "rpct1" Then%>
                             <li class="active"><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct1"><i class="fa fa-bar-chart"></i>Evaluation Item Scores</a></li>
                         <% Else%>
                             <li><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct1"><i class="fa fa-bar-chart"></i>Evaluation Item Scores</a></li>
                         <% End If%>
                     
                         <% If Request("ItemType") = "rpct2" Then%>
                             <li class="active"><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct2"><i class="fa fa-bar-chart"></i>Evaluation Summary</a></li>
                         <% Else%>
                             <li><a href="ReportConditionBySubject.aspx?ActionType=rpct&ItemType=rpct2"><i class="fa fa-bar-chart"></i>Evaluation Summary</a></li>
                         <% End If%>
                     </ul>
                 </li>
    
           <% End If %>
                <!-- end of Preceptor -->    

        <% If Request.QueryString("ActionType") = "chg" Then%>
            <li class="active"><a href="ChangePassword.aspx?ActionType=chg"><i class="fa fa-key"></i> <span>Change Password</span></a></li>
        <% Else%>
            <li><a href="ChangePassword.aspx?ActionType=chg"><i class="fa fa-key"></i> <span>Change Password</span></a></li>
        <% End If%> 
                 
        <li><a href="Default.aspx?logout=YES"><i class="fa fa-power-off"></i> <span>Logout</span></a></li>
</ul>
