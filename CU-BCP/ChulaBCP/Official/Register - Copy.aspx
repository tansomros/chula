<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Register.aspx.vb" Inherits=".Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Register
                                    <div class="page-title-subheading">ลงทะเบียน</div>
                                </div>
                            </div>
                        </div>
                    </div>
<section class="content">     
         <div class="row">    
            <section class="col-lg-12 connectedSortable">
<div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>ข้อมูลการลงทะเบียน                    
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdStudentID" runat="server" />
            <asp:HiddenField ID="hdRegisterID" runat="server" />
              </div>                 
            </div>
            <div class="card-body">
    <div class="row">
              <div class="col-md-2">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-6">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล</label>
                  <asp:Label CssClass="form-control text-center" ID="lblStudentName" runat="server"></asp:Label>
                </div>
              </div>
                </div>

                  <div class="row">
    <div class="col-md-2">
                <div class="form-group">
                  <label>ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control text-center"></asp:DropDownList>
                </div>
              </div>   
              <div class="col-md-2">
                <div class="form-group">
                  <label>ภาคการศึกษา</label>
                    <asp:RadioButtonList ID="optTerm" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ที่ 1</asp:ListItem>
                        <asp:ListItem Value="2">ที่ 2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              </div>
                  
               <div class="col-md-3">
                <div class="form-group">
                  <label>วันที่ลงทะเบียน</label>
                      <div class="input-group">                                             
                         <asp:TextBox ID="txtRegDate" runat="server" CssClass="form-control" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-language="th-th" data-provide="datepicker" onkeyup="chkstr(this,this.value)"></asp:TextBox>
                         <div class="input-group-append">
                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                         </div>
                       </div>
                </div>
              </div>
                <div class="col-md-12">
                <div class="form-group">
                  <label>เลือกเรื่องที่ลงทะเบียน<span class="text-red">*</span></label>
                    <asp:DropDownList ID="ddlTopicID" runat="server" CssClass="form-control mb-2 mr-2 dropdown-toggle btn btn-primary text-left" AutoPostBack="True"></asp:DropDownList>
                </div>
              </div>  
                </div>
    </div>
</div>
   </section>
   </div>
    <asp:Panel ID="pnSubject" runat="server">
         <div class="row">
      <section class="col-lg-12 connectedSortable">   
      
 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-graduation-cap"></i>
              รายวิชาที่ลงทะเบียน           
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
               
              </div>                 
            </div>
            <div class="box-body">
                  <div class="row">
 <div class="col-md-10">
                <div class="form-group">
                  <label>รายวิชา</label>
                 <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>
                  
                     <div class="col-md-2">
                <div class="form-group">
                  <br />
                  <asp:Button ID="cmdAddSubject" runat="server" Text="+ เพิ่มรายวิชา" CssClass="btn btn-success" />
                </div>
              </div>
                      </div>
                  <div class="row">  
                          <div class="col-md-12">
              <asp:GridView ID="grdSubject" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา">
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
<asp:BoundField HeaderText="ตอน">
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectName" HeaderText="ชื่อวิชา" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField HeaderText="ประเภท" DataField="SubjectTypeCode">
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" />                      
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:TemplateField>
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
         </div>
                          </div>
     <div class="row">  
           <div class="col-md-12 text-right">
                <div class="form-group">
                  <label>หน่วยกิตรวม</label>
                  <asp:Label CssClass="text-center text-blue text-bold" ID="lblTotalUnit" runat="server" Width="100"></asp:Label>
                </div>
              </div>
             </div>
    </div>
</div>
 
</section>
   </div>               
  </asp:Panel>  
  <asp:Panel ID="pnWorkSend" runat="server">
       <div class="row">
      <section class="col-lg-12 connectedSortable">   
          <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>ส่งงาน                    
                 <div class="box-tools pull-right">                      
              </div>                 
            </div>
            <div class="card-body">
                  <div class="row">  
                    <div class="col-md-6">
                <div class="form-group">
                  <label>เลือกเอกสาร</label>
                    <asp:DropDownList ID="ddlDocName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>     
                   <div class="col-md-6">
                     <div class="form-group">
                        <label>Upload เอกสาร</label> 
                         <br />
                      <asp:FileUpload ID="FileUploadFile" runat="server" /> 
                         &nbsp;<asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-success" Text="Upload" />
                     </div>
                       </div>     
       </div>
                      <div class="row"> 
              <div class="col-md-12">
                <div class="form-group">
                  <label></label>                   
                    <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-hover" ForeColor="#333333" GridLines="None" Width="100%">
                        <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                        <columns>
                            <asp:BoundField DataField="nRow" HeaderText="No." >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:HyperLinkField HeaderText="Document Name" DataNavigateUrlFields="DocumentLink" DataTextField="DocumentName" Target="_blank" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:TemplateField>
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel_Doc" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" />
                                </itemtemplate>
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" VerticalAlign="Middle" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                   
                </div>
              </div>   
                </div>

  </div>
</div>
      </section>
   </div>  
  </asp:Panel>  

        <div align="center">
<asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Next" Width="100px" />
        <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-success" Text="พิมพ์" Width="100px" />
            <asp:Button ID="cmdDelete"  runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" />
        </div>

</section>
</asp:Content>
 

