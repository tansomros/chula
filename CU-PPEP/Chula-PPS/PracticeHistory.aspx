
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PracticeHistory.aspx.vb" Inherits=".PracticeHistory" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <section class="content-header">
      <h1>ประวัติการฝึกปฏิบัติงานวิชาชีพ</h1>     
    </section>
<section class="content">
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-registered"></i>

              <h3 class="box-title">ประวัติการฝึกปฏิบัติงานวิชาชีพทั้งหมด <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;รายการ
                </h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
             
              </div>                                 
            </div>
            <div class="box-body">  
                <table id="tbpractice" class="table table-hover table-striped">
                <thead>
                <tr> 
                    <th>ปีการศึกษา</th>
                    <th>รหัสวิชา</th>
                    <th class="text-left">ชื่อรายวิชา</th>
                    <th class="text-left">แหล่งฝึก</th>       
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtHis.Rows %>
                <tr>              
                  <td class="text-center"><% =String.Concat(row("EduYear")) %></td>
                  <td class="text-center"><% =String.Concat(row("SubjectCode")) %>    </td>
                  <td><% =String.Concat(row("SubjectName")) %></td>
                  <td><% =String.Concat(row("LocationName")) %></td>   
                </tr>
            <%  Next %>
                </tbody>               
              </table>  
 </div>
            <div class="box-footer clearfix">
            <asp:Label ID="lblNot" runat="server" CssClass="validateAlert" 
                 Text="ยังไม่มีประวัติการฝึกปฏิบัติงานวิชาชีพ" Width="99%"></asp:Label>
            </div>
          </div>
    </section>
</asp:Content>
