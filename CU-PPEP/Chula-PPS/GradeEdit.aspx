<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GradeEdit.aspx.vb" Inherits=".GradeEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>แก้ไขคะแนนประเมิน/เกรด นิสิต</h1>   
    </section>

<section class="content">  
<div class="row">  
    <section class="col-lg-12 connectedSortable">
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-circle"></i>

              <h3 class="box-title">ข้อมูลนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <table width="100%" border="0" cellPadding="1" cellSpacing="1" >
<tr>
                                                    <td width="80" class="texttopic">
                                                        รหัสนิสิต</td>
                                                    <td>
                                                        <asp:Label ID="lblStudentCode" runat="server"></asp:Label>
                                                    </td>                                                  
                                                    <td width="80" class="texttopic">ชื่อ - นามสกุล</td>
                                                    <td>
                                                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                                    </td>
</tr>
<tr>
  <td class="texttopic">สาขา</td>
  <td><asp:Label ID="lblMajorName" runat="server"></asp:Label></td>
  <td class="texttopic">ผู้แก้ไข</td>
  <td>
                                                        
                                                        <asp:Label ID="lblEditorName" runat="server"></asp:Label>
                                                    </td>
</tr>

</table>  
                         
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 </section>
        </div>
<div class="row">
 <section class="col-lg-5 connectedSortable">
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-star"></i>

              <h3 class="box-title">คะแนนประเมินและเกรด</h3>
                 <div class="box-tools pull-right">
                 Ref.ID : <asp:Label ID="lblUID" runat="server"></asp:Label>
              </div>  
            </div>
            <div class="box-body">  
                   <table>
                    <tr>
                        <td>ปีการศึกษา</td>
                         <td> 
                                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                        </td>
                        </tr>
                      <tr>
                        <td>รายวิชา</td>
                         <td>
                             <asp:Label ID="lblSubjectCode" runat="server"></asp:Label>
&nbsp;<asp:Label ID="lblSubjectName" runat="server"></asp:Label>
                                                    </td>
                        </tr>
                       <tr>
                        <td>คะแนนรวม</td>
                         <td> 
              <asp:TextBox ID="txtScore" runat="server" Width="50px" BackColor="#FFFFC1" Font-Bold="True"></asp:TextBox>
                             <asp:HiddenField ID="hdScore" runat="server" />
                           </td>
                        </tr>
                       <tr>
                        <td>เกรด</td>
                         <td> 
              <asp:TextBox ID="txtGrade" runat="server" Width="50px" BackColor="#FFFFC1" Font-Bold="True" MaxLength="2" TabIndex="1"></asp:TextBox>
                             <asp:HiddenField ID="hdGrade" runat="server" />
                           </td>
                        </tr>
                    </table>
</div>
           
          </div>
</section>
 <section class="col-lg-5 connectedSortable">
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-star"></i>

              <h3 class="box-title">ระบุเหตุผลที่ต้องการแก้ไขคะแนนประเมิน/เกรด</h3>
            </div>
            <div class="box-body"> 
                <asp:TextBox ID="txtReason" runat="server" Height="100px" TextMode="MultiLine" Width="100%" BackColor="#FFE1E1" CssClass="form-control select2"></asp:TextBox>
                       
</div>
           
          </div>
</section>
    </div>
<div class="row">
    <section class="col-lg-12 connectedSortable">     
    <div align="center"> <asp:Button ID="cmdSave" runat="server" Text="บันทึก" CssClass="btn btn-pink" Width="100px" /> 
                <asp:Button ID="cmdBack" runat="server" Text="<< กลับหน้ารายชื่อนิสิต" CssClass="btn btn-warning" />
     </div> 
 </section>
</div>
</section>    
</asp:Content>
