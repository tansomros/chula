<%@ Page Title="User Account" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Users.aspx.vb" Inherits=".Users" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-users icon-gradient bg-success"></i>
                </div>
                <div>
                    <%: Title %>
                    <div class="page-title-subheading">
                        <%: MetaDescription %>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-default">
            <div class="box-header">
                <i class="fa fa-users"></i>
                <h3 class="box-title">ผู้ใช้งานทั้งหมด</h3>
                <div class="box-tools pull-right">
                    <a href="UserModify?m=u&s=u" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i>เพิ่มใหม่</a>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>User Group</label><br />
                            <asp:DropDownList ID="ddlGroupFind" runat="server" AutoPostBack="true" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>ค้นหา</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="สามารถค้นหาได้จาก username,ชื่อ,ชื่อร้านยา"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="Search" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 table-responsive">
                        <div class="table-responsive">
                            <table id="tbuser" class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                                        <th class="text-center">Username</th>
                                        <th class="text-left">Person Name</th>
                                        <th class="text-left">Display Name</th>
                                        <th class="text-left">E-mail</th>
                                        <th class="text-center">Organization</th>
                                        <th class="text-center">Role</th>
                                        <th class="text-center">Last Login</th>
                                        <th class="text-center">Lang</th>
                                        <th class="text-center">Active</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% For Each row As DataRow In dtUser.Rows %>
                                    <tr>
                                        <td width="50" class="text-center">
                                            <a href="UserModify?ActionType=user&ItemType=add&uid=<% =String.Concat(row("UserID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข" target="_blank"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                        </td>
                                        <td class="text-left"><% =String.Concat(row("Username")) %></td>
                                        <td><a href="UserModify?ActionType=user&ItemType=add&uid=<% =String.Concat(row("UserID")) %>"><% =String.Concat(row("PersonName")) %> </a></td>
                                        <td><a href="UserModify?ActionType=user&ItemType=add&uid=<% =String.Concat(row("UserID")) %>"><% =String.Concat(row("DisplayName")) %> </a></td>
                                        <td class="text-left"><% =String.Concat(row("Email")) %></td>
                                        <td class="text-left"><% =String.Concat(row("LocationName")) %></td>
                                        <td class="text-center"><% =String.Concat(row("ProfileName")) %></td>
                                        <td class="text-center"><% =String.Concat(row("LastLogin")) %></td>
                                        <td class="text-center">
                                            <% If String.Concat(row("Lang")) = "EN" Then%>
                                            <asp:Image ID="imgEn" runat="server" ImageUrl="images/en.png" Width="20" />
                                            <% Else %>
                                            <asp:Image ID="imgTh" runat="server" ImageUrl="images/th.png" Width="20" />
                                            <% End If %>
                                            <% =String.Concat(row("Lang")) %>    
                                        </td>
                                        <td class="text-center">
                                            <% If String.Concat(row("StatusFlag")) = "A" Then%>
                                            <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
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
