<%@ Page Title="Student Evaluation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentEvaluation.aspx.vb" Inherits=".StudentEvaluation" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-sun icon-gradient bg-success"></i>
                </div>
                <div>
                    <%: Title %>
                    <div class="page-title-subheading">
                       สถานะการส่งคะแนนประเมิน
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-default">
            <div class="box-header">
                <i class="fa fa-asterisk"></i>
                <h3 class="box-title">รายการประเมิน</h3>
                <div class="box-tools pull-right">     
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ปี</label><br />
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 table-responsive">
                        <div class="table-responsive">
                            <table id="tbeval" class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">ปี</th>
                                        <th class="text-center">รหัสนิสิต</th>
                                        <th class="text-left">ชื่อ-สกุล</th>                                       
                                        <th class="text-left">ผู้ประเมิน</th>
                                        <th class="text-center">บทบาท</th>
                                        <th class="text-left">แหล่งฝึก</th>                                     
                                        <th class="text-center">สถานะ</th>   
                                        <th class="text-center">วันที่ยืนยัน</th>
                                        <th class="text-center">ยืนยัน</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% For Each row As DataRow In dtStEva.Rows %>
                                    <tr>
                                        <td class="text-center"><% =String.Concat(row("EduYear")) %></td>
                                        <td class="text-center"><% =String.Concat(row("StudentCode")) %></td>
                                        <td class="text-left"><% =String.Concat(row("StudentName")) %></td>                                      
                                        <td class="text-left"><% =String.Concat(row("AssessorName")) %></td>
                                        <td class="text-center"><% =String.Concat(row("RoleName")) %></td>
                                        <td class="text-left"><% =String.Concat(row("LocationName")) %></td>                               
                                        <td class="text-center">
                                            <% If String.Concat(row("ConfirmStatus")) = "Y" Then%>
                                            <img src="images/icon-ok.png" alt="ยืนยันแล้ว" />
                                            <% End If %>
                                        </td>    
                                        <td class="text-center" width="120" data-order="<% =ConfirmDateSortKey(row) %>"><% =ConfirmDateDisplay(row) %></td>
                                        <td class="text-center">
                                            <% If String.Concat(row("ConfirmStatus")) = "Y" Then%>
                                            <a href="javascript:void(0);" class="text-success" title="เปลี่ยนเป็นยังไม่ยืนยัน" onclick="__doPostBack('toggleConfirm','<% =String.Concat(row("UID")) %>'); return false;"><i class="fa fa-toggle-on" style="font-size:24px; line-height:1;" aria-hidden="true"></i></a>
                                            <% Else %>
                                            <a href="javascript:void(0);" class="text-muted" title="เปลี่ยนเป็นยืนยันแล้ว" onclick="__doPostBack('toggleConfirm','<% =String.Concat(row("UID")) %>'); return false;"><i class="fa fa-toggle-off" style="font-size:24px; line-height:1;" aria-hidden="true"></i></a>
                                            <% End If %>
                                        </td>
                                    </tr>
                                    <%  Next %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </div>
            <div class="box-footer clearfix">
            </div>
        </div>

    </section>
</asp:Content>
