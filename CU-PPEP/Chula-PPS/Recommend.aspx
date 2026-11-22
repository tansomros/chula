<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Recommend.aspx.vb" Inherits=".Recommend" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
      <div class="app-page-title">
      <div class="page-title-wrapper">
          <div class="page-title-heading">
              <div class="page-title-icon">
                  <i class="pe-7s-chat icon-gradient bg-success"></i>
              </div>
              <div>คำแนะนำแหล่งฝึกจากนิสิต                  
                   <div class="page-title-subheading">Recommendation</div>
              </div>
          </div>
      </div>
  </div>  
<section class="content">
      <div class="box box-solid">
       <div class="box-header">
        <i class="fa fa-filter"></i>
        <h3 class="box-title">ค้นหา</h3>   
            <div class="box-tools pull-right">
                              <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                  <i class="fa fa-minus"></i>
                              </button>
                          </div>
      </div>
      <div class="box-body">
          <div class="row">
            <div id="pnType" runat="server" class="col-md-2">
                  <div class="form-group">
                      <label>คำแนะนำ</label>
                      <br />
                      <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" AutoPostBack="True">
                          <asp:ListItem Selected="True" Value="0">ทั้งหมด</asp:ListItem>
                          <asp:ListItem Value="1">คำแนะนำของฉัน</asp:ListItem>
                      </asp:DropDownList>
                  </div>
              </div>
              <div  id="pnStatus" runat="server" class="col-md-2">
                  <div class="form-group">
                      <label>สถานะ</label>
                      <br />
                      <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2"  AutoPostBack="True">
                          <asp:ListItem Selected="True" Value="0">ทั้งหมด</asp:ListItem>  
                          <asp:ListItem Value="P">Pending</asp:ListItem>
                          <asp:ListItem Value="A">Approved</asp:ListItem>
                          <asp:ListItem Value="X">Rejected</asp:ListItem>
                      </asp:DropDownList>
                  </div>
              </div>     
                 <div  class="col-md-3">
       <div class="form-group">
           <label>รายวิชา</label>
           <br />
           <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2"  AutoPostBack="True">
           </asp:DropDownList>
       </div>
   </div>  
                            <div  class="col-md-3">
    <div class="form-group">
        <label>แหล่งฝึก</label>
        <br />
        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2"  AutoPostBack="True">
        </asp:DropDownList>
    </div>
</div> 


                 <div class="col-md-2">
       <div class="form-group">
           <label>คำค้นหา</label> 
            <asp:TextBox ID="txtSearch" runat="server" cssclass="form-control text-center" ></asp:TextBox>
       </div>
   </div> 

              <div class="col-md-1 mt-4"> 
                  <asp:LinkButton ID="cmdSearch" runat="server" CssClass="btn btn-warning" Width="100px"><i class="fa fa-search"></i>ค้นหา</asp:LinkButton> 
                  <asp:Button ID="cmdAdd" runat="server" cssclass="btn btn-success"  Text="เพิ่มคำแนะนำใหม่"/>
              </div>
          </div>
      </div>
  </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">รายการคำแนะนำทั้งหมด                 <asp:Label ID="lblCount" runat="server"></asp:Label>
                  &nbsp;รายการ
                </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <div class="table-responsive">
                    <table id="tbdata" class="table table-hover">
                  <thead>
                      <tr>                                   
                          <th class="sorting_asc_disabled sorting_desc_disabled text-center">ดู</th>
                          <th>ชื่อแหล่งฝึก</th>
                          <th class="text-left">จังหวัด</th>
                          <th class="text-left">นิสิตผู้แนะนำ</th>
                          <th class="text-center">รายวิชา</th> 
                          <th class="text-center">คำแนะนำสั้นๆ</th>
                          <th class="text-center">สถานะ</th> 
                      </tr>
                  </thead>
                  <tbody>
                      <% For Each row As DataRow In dtRecLoc.Rows %>
                      <tr>
                          <td width="50" class="text-center">
                              <a href="RecommendModify?ActionType=rcm&ItemType=rcm1&id=<% =String.Concat(row("UID")) %>" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด/แก้ไข" target="_blank"><i class="fa fa-search" aria-hidden="true"></i></a>                                                    
                          </td>
                          <td class="text-left"><% =String.Concat(row("LocationName")) %></td>
                          <td><% =String.Concat(row("ProvinceName")) %></td>
                          <td class="text-left"><% =String.Concat(row("StudentName")) %></td>
                          <td class="text-left"><% =String.Concat(row("SubjectName")) %></td> 
                          <td class="text-center"><% =String.Concat(row("Title")) %></td>  
                          <td class="text-center"><% =String.Concat(row("StatusName")) %></td>                         
                      </tr>
                      <%  Next %>
                  </tbody>
              </table>
          </div>
   
                    
</div>
            <div class="box-footer clearfix">  
            </div>
          </div>
    </section>    
</asp:Content>
