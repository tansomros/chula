<%@ Page Title="อาจารย์แหล่งฝึก (Preceptor)" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PreceptorList.aspx.vb" Inherits=".PreceptorList" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>รายชื่ออาจารย์แหล่งฝึก (Preceptor List) </h1>
    </section>

    <section class="content">
        <div class="box box-default">
            <div class="box-header">
                <i class="fa fa-user-md"></i>
                <h3 class="box-title">อาจารย์แหล่งฝึกทั้งหมด</h3>
                <div class="box-tools pull-right">
                    <a href="PreceptorData.aspx?ActionType=tch&ItemType=pct" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>แหล่งฝึก</label><br />
                            <asp:DropDownList ID="ddlLocationFind" runat="server" AutoPostBack="true" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="form-group">
                            <label>ค้นหา</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="สามารถค้นหาได้จาก ชื่อ, นามสกุล หรือ ชื่อแหล่งฝึก"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="Search" />
                        </div>
                    </div>
                </div>
              
                <br />
                <div class="row">
                    <div class="col-md-12 table-responsive">
                        <div class="table-responsive">
                            <table id="tbpreceptor" class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">Id</th>
                                        <th class="text-left">คำนำหน้า</th>
                                        <th class="text-left">ชื่อ</th>
                                        <th class="text-left">นามสกุล</th>
                                        <th class="text-left">ตำแหน่ง</th>
                                        <th class="text-left">สังกัดแหล่งฝึก</th>
                                        <th class="text-center">ส่วนงาน</th>
                                        <th class="text-center">E-mail</th>                                       
                                        <th class="text-center">เบอร์โทร</th>   
                                        <th class="text-center">CPE สะสม</th>
                                        <th class="text-center">อบรมแหล่งฝึก</th> 
                                        <th class="text-center">CV</th>
                                        <th class="text-center">Last update</th>
                                        <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% Dim rowIndex As Integer = 1 %>
                                    <% For Each row As DataRow In dtPreceptor.Rows%>
                                    <tr>
                                        <td class="text-center"><%= String.Concat(row("PersonID")) %></td>
                                        <td class="text-left"><%= String.Concat(row("PrefixName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("FirstName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("LastName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("PositionName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("LocationName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("WorkName")) %></td>
                                        <td class="text-left"><%= String.Concat(row("Email")) %></td>
                                        <td class="text-left"><%= String.Concat(row("Telephone")) %></td>
                                        <td class="text-left"><%= String.Concat(row("Credits")) %></td>
                                      
                                        <td class="text-center">
                                             <% If String.Concat(row("IsTraining")) = "Y" Then%>
                                             <asp:Image ID="Image1" runat="server" ImageUrl="images/icon-ok.png" />
                                             <% End If%>
                                        </td>
                                        <td class="text-left">       
                                              <% If String.Concat(row("CvPath")) <> "" Then%>
                                            <a href='<%= String.Concat(row("CvPath")) %>' class="text-info" data-toggle="tooltip" data-placement="top" data-original-title="คลิกเพื่อดู" target="_blank" ><i class="fa fa-paper-plane" aria-hidden="true"></i></a>
                                             <% End If%>
                                        </td>    
                                        
                                          <td class="text-left"><%= String.Concat(row("MWhen")) %></td>
                                        <td class="text-center">
                                            <a href="PreceptorData?ActionType=tch&ItemType=pct&id=<%= String.Concat(row("PersonID")) %>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                        </td>
                                    </tr>
                                    <% rowIndex += 1 %>
                                    <% Next%>
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
