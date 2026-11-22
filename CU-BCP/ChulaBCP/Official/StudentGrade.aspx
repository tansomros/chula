<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentGrade.aspx.vb" Inherits=".StudentGrade" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-pen icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>ผลการศึกษา
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
                    </div>

<section class="content">  
       <div class="row">
     <section class="col-lg-12 connectedSortable">
    <div class="main-card mb-3 card">
       <div class="card-header"><i class="header-icon lnr-user icon-gradient bg-success">
            </i>นิสิต/ผู้ฝึกอบรม                 
                 <div class="box-tools pull-right">   
                     <asp:HiddenField ID="hdStudentID" runat="server" /> 
              </div>                 
            </div>
            <div class="card-body">
     <div class="row">
              <div class="col-md-3">
                <div class="form-group">
                  <label>รหัส</label>
                  <asp:Label CssClass="text-blue" ID="lblStdCode" runat="server"></asp:Label>
                </div>
              </div>             
                    <div class="col-md-4">
                <div class="form-group">
                  <label>ชื่อ-นามสกุล</label>
                  <asp:Label CssClass="text-blue" ID="lblStudentName" runat="server"></asp:Label>
                </div>
              </div>
         <div class="col-md-5">
                <div class="form-group">
                  <label>สาขา</label>
                  <asp:Label CssClass="text-blue" ID="lblMajor" runat="server"></asp:Label>
                </div>
              </div>

                </div>
    </div>
    </div>
   <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>
              <h3 class="box-title">ค้นหา</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button> 
              </div>                                 
            </div>
            <div class="box-body">     
                  <div class="row">
    <div class="col-md-3">
                <div class="form-group">
                  <label>ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>
                </div>
              </div>   
              <div class="col-md-3">
                <div class="form-group">
                  <label>ภาคการศึกษา</label>
                    <asp:RadioButtonList ID="optTerm" runat="server"  AutoPostBack="True"  RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ที่ 1</asp:ListItem>
                        <asp:ListItem Value="2">ที่ 2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              </div>
                       <div class="col-md-6">
                <div class="form-group">
                 <br /> 
                    <asp:Button ID="cmdSearch" runat="server" Text="แสดง" CssClass="btn btn-primary" Width="60" />&nbsp;<asp:Button ID="cmdPrint" runat="server" Text="พิมพ์ใบรายงานผล" CssClass="btn btn-success" />        

                </div>
              </div>

</div>
                 
                                      
</div>           
 </div>  
         </section>         
  </div>  
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="modal_loading">
                <div class="center_loading text-center">
                    <img alt="" src="images/loading.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
   <div class="row">
    <section class="col-lg-9 connectedSortable"> 
         <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-trophy"></i>
              <h3 class="box-title">รายการผลการศึกษา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button> 
              </div>                                 
            </div>
            <div class="box-body"> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" PageSize="20" CssClass="table table-hover" GridLines="None">
                            <RowStyle HorizontalAlign="Center" />
                            <columns>
                                <asp:BoundField DataField="nRow" HeaderText="No.">
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectName" HeaderText="ชื่อวิชา">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต" />
                                <asp:BoundField DataField="SubjectTypeCode" HeaderText="ประเภทวิชา">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Grade" HeaderText="ระดับที่ได้" />
                            </columns>
                            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                            <headerstyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                    
                        <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" /> 
                        <asp:AsyncPostBackTrigger ControlID="cmdPrint" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="optTerm" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                                                 
</div>
            <div class="box-footer clearfix">

            </div>
          </div>
    </section>
        <section class="col-lg-3 connectedSortable">
          <div class="main-card mb-3 card">
       <div class="card-header"> ความหมายของประเภทวิชา               </div>
            <div class="card-body">
     <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">C (CREDIT)</label>
                   = รายวิชาที่วัดผลเป็นระดับขั้น

                </div>
              </div>             
                    <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">A (AUDIT)</label>
                 = รายวิชาที่ไม่นับหน่วยกิตเป็น
หน่วยกิตสะสม วัดผลเป็น
สัญลักษณ์ S และ U  
                </div>
              </div>
         <div class="col-md-12">
                <div class="form-group">
                  <label class="text-blue">N (NON-CREDIT)</label>              
 = รายวิชาที่ไม่มีจานวนหน่วยกิต
วัดผลเป็นสัญลักษณ์ G P และ F  
                </div>
              </div>

                </div>
    </div>
    </div>
   </section>
</div>

</section>
</asp:Content>
