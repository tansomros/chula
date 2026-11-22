<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Student.aspx.vb" Inherits=".Student" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
   
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>ข้อมูลนิสิต</h1>   
    </section>

<section class="content">  
   
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                         
                   <div class="row">

 <div class="col-md-4">
              <div class="form-group">
                <label>สาขา</label>
                  <asp:DropDownList ID="ddlMajor" runat="server"  CssClass="form-control select2"></asp:DropDownList>
                 </div> 
            </div>

            <div class="col-md-3">
              <div class="form-group">
                <label>ชั้นปี</label>
                  <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control select2 text-center">
                      <asp:ListItem Selected="True" Value="0">ทั้งหมด</asp:ListItem>
                      <asp:ListItem>1</asp:ListItem>
                      <asp:ListItem>2</asp:ListItem>
                      <asp:ListItem>3</asp:ListItem>
                      <asp:ListItem>4</asp:ListItem>
                      <asp:ListItem>5</asp:ListItem>
                      <asp:ListItem>6</asp:ListItem>
                      <asp:ListItem>7</asp:ListItem>
                      <asp:ListItem Value="40">สำเร็จการศึกษา</asp:ListItem>
                  </asp:DropDownList> 
                 </div> 
            </div>           
                
                      <div class="col-md-3">
              <div class="form-group">
                <label>คำค้นหา</label> <asp:TextBox ID="txtSearch" runat="server" cssclass="form-control" placeholder=""></asp:TextBox> 
                 </div>
            </div>
 <div class="col-md-2">
              <div class="form-group">
                <label></label> 
                  <br />
                   <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>  
                   <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="70" Text="ดูทั้งหมด"></asp:Button> 

                 </div>
            </div> 

</div>
                                      
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div> 
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่อนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <div class="table-responsive">
                  <table id="tbdata" class="table table-bordered table-striped">
                <thead>
                <tr> 
                    <th>รหัสนิสิต</th>
                    <th>ชื่อนิสิต</th>
                    <th>สาขา</th>
                    <th>ชั้นปีที่</th>                     
                    <th>Active</th>
                    <th class="sorting_asc_disabled">ดูประวัติ</th> 
                    <th class="sorting_asc_disabled">ประวัติฝึกฯ</th> 
                    <th class="sorting_asc_disabled">แก้ไข</th> 
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtStd.Rows %>
                <tr>              
                  <td class="text-center"><% =String.Concat(row("Student_Code")) %></td>
                  <td><% =String.Concat(row("StudentName")) %>    </td>
                  <td class="text-center"><% =String.Concat(row("MajorName")) %></td>
                  <td class="text-center"><% =String.Concat(row("ClassName")) %></td>              
                  <td class="text-center"><% =IIf(String.Concat(row("StatusFlag")) = "A", "<img src='images/icon-ok.png'>", "") %> </td>
                  <td class="text-center"> 
                      <a class="editemp" href="Student_Bio?ActionType=std&stdcode=<% =String.Concat(row("Student_Code")) %>" target="_blank"><img src="images/view.png" data-toggle="tooltip" data-placement="top" data-original-title="ดูประวัติ"  /></a>
</td>
                  <td class="text-center"> 
                      <a class="editemp" href="PracticeHistory?ActionType=std&ItemType=pthistory&stdcode=<% =String.Concat(row("Student_Code")) %>" target="_blank"><img src="images/cup.png" data-toggle="tooltip" data-placement="top" data-original-title="ประวัติฝึกฯ" /></a>
</td>
                  <td class="text-center"> 
                       <a class="editemp" href="Student_Reg?ActionType=std&ItemType=std&stdcode=<% =String.Concat(row("Student_Code")) %>" target="_blank"><img src="images/icon-edit.png" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข" /></a>                  
                      
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
                       
    </section>
</asp:Content>
