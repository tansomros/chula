<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AlertSetting.aspx.vb" Inherits=".AlertSetting" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript">
      function openModalSend(title, message) {
          $("#spnHeader").text(title);
          $("#spnBodyMsg").text(message);
          $('#modal-window-send').modal('show');
          $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){}, 50);");
          return false;      }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-envelope icon-gradient bg-success"></i>
                </div>
                <div>
                    Email Alert        
                 <div class="page-title-subheading">
                     ส่งอีเมล์แจ้งแหล่งฝึกให้ประเมินนิสิต
                 </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-Search"></i>

                <h3 class="box-title">ค้นหา</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>ปีการศึกษา</label>
                            <asp:Label ID="lblYear" runat="server" CssClass="form-control text-center"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="form-group">
                            <label>ผลัดฝึก</label>
                            <asp:DropDownList ID="ddlPhase" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>รายวิชา</label>
                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>สถานะ</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="">ทั้งหมด</asp:ListItem>
                                <asp:ListItem Value="Y">ส่งแล้ว</asp:ListItem>
                                <asp:ListItem Value="N">ยังไม่ส่ง</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label></label>
                            <br />
                            <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-pink" Text="ค้นหา" Width="100px" />

                        </div>
                    </div>

                </div>

            </div>
            <div class="box-footer clearfix text-center">
                 <asp:Button ID="cmdSend" runat="server" CssClass="btn btn-primary" Text="ส่งจากผลการค้นหา (20 เมล์)"   />
            </div>
        </div>


        <div class="box box-primary">
            <div class="box-header">
                <i class="fa fa-user-circle-o"></i>

                <h3 class="box-title">รายการสถานะการส่งอีเมล์</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="table-responsive">
                    <table id="tbmail" class="table table-hover">
                        <thead>
                            <tr>
                                <th class="text-center">No.</th>
                                <th class="text-center">แหล่งฝึก</th>
                                <th class="text-left">ผลัดที่</th>
                                <th class="text-left">ผลัดฝึก</th>
                                <th class="text-center">ชื่อ</th>
                                <th class="text-left">E-mail</th>
                                <th class="text-center">วันที่ส่งล่าสุด</th>
                                <th class="text-center">Lang</th>
                                <th class="text-center">สถานะ</th>
                                <th class="sorting_asc_disabled sorting_desc_disabled text-center">ส่ง</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtMail.Rows %>
                            <tr>

                                <td class="text-left"><% =String.Concat(row("nRow")) %></td>
                                <td class="text-left"><% =String.Concat(row("LocationName")) %></td>
                                <td class="text-center"><% =String.Concat(row("PhaseNo")) %></td>
                                <td class="text-center"><% =String.Concat(row("PhaseName")) %></td>
                                <td><% =String.Concat(row("DisplayName")) %></td>
                                <td class="text-left"><% =String.Concat(row("Email")) %></td>
                                <td class="text-center"><% =String.Concat(row("SendDttm")) %></td>

                                <td class="text-center">
                                    <% If String.Concat(row("Lang")) = "EN" Then%>
                                    <asp:Image ID="imgEn" runat="server" ImageUrl="images/en.png" Width="20" />
                                    <% Else %>
                                    <asp:Image ID="imgTh" runat="server" ImageUrl="images/th.png" Width="20" />
                                    <% End If %>
                                    <% =String.Concat(row("Lang")) %>    
                                </td>
                                <td class="text-center">
                                    <% If String.Concat(row("SendStatus")) = "Y" Then%>
                                    <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
                                    <% End If %>
                                </td>
                                <td width="50" class="text-center">
                                    <a href="AlertSetting?ActionType=mail&t=send&pid=<% =String.Concat(row("PersonID"))%>&ph=<% =String.Concat(row("PhaseID"))%>&lid=<% =String.Concat(row("LocationID"))%>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ส่งอีเมล์"><i class="fa fa-envelope" aria-hidden="true"></i></a>
                                </td>
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                </div>                 

            </div>
            <div class="box-footer clearfix">
            </div>
        </div>

         <div id="modal-window-send" class="modal fade" role="dialog" data-backdrop="static">
     <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                     <span aria-hidden="true">&times;</span></button>
                 <div class="modal-title"><span id="spnHeader"></span></div>
             </div>
             <div class="modal-body">
                 <h6>&nbsp;<span id="spnBodyMsg"></span>&nbsp;</h6>
             </div>
             <div class="modal-footer">
                 <asp:Button ID="cmdConfirm" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="120" />
                 <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="120">Close</button>
             </div>
         </div>
     </div>
 </div>

    </section>
</asp:Content>
