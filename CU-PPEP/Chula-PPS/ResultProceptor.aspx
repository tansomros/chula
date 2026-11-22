<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResultProceptor.aspx.vb" Inherits=".ResultProceptor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
  
  <script> $(function() {
       $( "#dialog-message" ).dialog({
      autoOpen: false,
      height: 600,
      width: 600,
      modal: true,
      buttons: {
        Close: function() {
          $( this ).dialog( "close" );
        }
      },
      close: function() {
        
      }
    });
 
    $( "#user-login" )
      .button()
      .click(function() {
        $( "#dialog-message" ).dialog( "open" );
      });
  });
  </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                 <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="lnr-users icon-gradient bg-success"></i>
            </div>
            <div>Internship Student List        
                 <div class="page-title-subheading">รายชื่อนิสิตที่เข้ามาฝึกปฏิบัติวิชาชีพ
                 </div>
            </div>
        </div>
    </div>
</div>   

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไข (Select Filter)</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                  <div class="row">
      <div class="col-md-3">
          <div class="form-group">
              <label> ปีการศึกษา (Year)</label>    
              <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
          </div>
      </div>
      <div class="col-md-4">
          <div class="form-group">
              <label>แหล่งฝึก(Site)</label>
              <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" 
          AutoPostBack="True"></asp:DropDownList>   
          </div>
      </div>
       <div class="col-md-5">
       <div class="form-group">
           <label>ผลัดฝึก (Batch)</label>
             <asp:DropDownList ID="ddlPhase" runat="server" CssClass="form-control select2" AutoPostBack="True">
      </asp:DropDownList>
       </div>
   </div>
      <div class="col-md-7">
          <div class="form-group">
              <label>รายวิชา (Course)</label>
                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>  
          </div>
      </div>
                     
               
                    
                      <div class="col-md-3 pt-4">
    <div class="form-group">
 <asp:Button ID="cmdFind" runat="server"  Text="ค้นหา" CssClass="btn btn-warning" Width="100px"/>         
    </div>
</div>

  </div>     
                               
</div>
            <div class="box-footer clearfix text-center">
           
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>    
            </div>
          </div>

 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายชื่อนิสิต / Student List</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="Student_ID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต (ID)">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />                      </asp:BoundField>
                <asp:BoundField HeaderText="ชื่อ-สกุลนิสิต (Name)" DataField="StudentName">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectName" HeaderText="รายวิชา(Course)" />
                <asp:BoundField HeaderText="ผลัดฝึก (Batch)_" DataField="TimePhaseName" />
                <asp:BoundField HeaderText="วันที่ฝึกปฏิบัติ (Date)" DataField="TimePhaseDesc" />
                <asp:BoundField HeaderText="View" />
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>


             
                                   
</div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels" 
                 Text="ยังไม่ได้รับคัดเลือกแหล่งฝึกให้" Width="95%"></asp:Label>
            </div>
          </div>
</section>         
</asp:Content>
