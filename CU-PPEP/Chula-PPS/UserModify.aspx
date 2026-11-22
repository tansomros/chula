<%@ Page Title="User Account" MetaDescription="จัดการข้อมูลผู้ใช้งาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserModify.aspx.vb" Inherits=".UserModify" %>

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

        <div class="row">
            <section class="col-lg-7 connectedSortable">
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-user"></i>
          <h3 class="box-title">ข้อมูลทั่วไป </h3>
 <div class="box-tools pull-right"> 
            </div>
                    </div>
                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>User ID.</label>
                                    <asp:Label ID="lblUserID" runat="server" text="0" CssClass="form-control text-center"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>ชื่อ</label>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="ชื่อ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>นามสกุล</label>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="นามสกุล"></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                         <div class="col-md-6">
     <div class="form-group">
         <label>ชื่อที่แสดง</label>
         <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" placeholder="ชื่อที่ใช้แสดงในระบบ"></asp:TextBox>
     </div>
 </div>  
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>E-mail</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="อีเมล"></asp:TextBox>
                                </div>
                            </div>
                              
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>กลุ่ม</label>
                                    <asp:DropDownList ID="ddlUserGroup" runat="server" CssClass="form-control select2" Width="100%" AutoPostBack="True">
                                        <asp:ListItem Value="1">นิสิต</asp:ListItem>
                                        <asp:ListItem Value="2">อาจารย์</asp:ListItem>
                                        <asp:ListItem Value="3">แหล่งฝึก</asp:ListItem>
                                        <asp:ListItem Value="4">จนท./อื่นๆ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="pnLocation" runat="server">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>เชื่อมโยงอาจารย์แหล่งฝึก</label>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="pnPerson" runat="server">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>เชื่อมโยงอาจารย์</label>
                                    <asp:DropDownList ID="ddlPerson" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                  
                      
                    </div>
                </div>

            </section>
            <section class="col-lg-5 connectedSortable">
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-key"></i>
                                 <h3 class="box-title">รหัสผู้ใช้งาน</h3>
                      
          <div class="box-tools pull-right">
            </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control text-center" placeholder="ชื่อ Login"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control text-center" placeholder="รหัสผ่าน"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-header">
                        <i class="fa fa-lock"></i>
                  <h3 class="box-title">สิทธิ์การใช้งาน</h3>
<div class="box-tools pull-right"> 
            </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>ระดับสิทธิ์</label>
                                    <asp:CheckBoxList ID="chkRoles" runat="server" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                              <div class="col-md-3">
      <div class="form-group">
          <label>สถานะ Active</label>
           <div class="button r" id="button-1">
     <input id="chkStatus" type="checkbox" class="checkbox"  runat="server" />
     <div class="knobs"></div>
     <div class="layer"></div>
 </div>
      </div>
  </div>

                        </div>

                    </div>
                </div>
            </section>
        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="Save" Width="120px" />
                <asp:Button ID="cmdClear" CssClass="btn btn-secondary" runat="server" Text="Clear" Width="120px" />
                <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
            </div>
        </div>

    </section>
</asp:Content>
