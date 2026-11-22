<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentResultPreceptor.aspx.vb" Inherits=".AssessmentResultPreceptor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
                     <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="lnr-star icon-gradient bg-warning"></i>
            </div>
            <div>Assessment Results       
                 <div class="page-title-subheading">ผลการประเมิน
                 </div>
            </div>
        </div>
    </div>
</div>   

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไข/Filter</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

                <table border="0" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td class="texttopic">
                                                        ปีการศึกษา (Year):                                                        </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="form-control select2" width="150">                                                        </asp:DropDownList>                                                    
                                                    </td>
                                                    <td class="texttopic">&nbsp;</td>
</tr>
<tr>
  <td class="texttopic">รายวิชา (Course) :</td>
  <td>
                                                      <asp:DropDownList ID="ddlCourse" runat="server" 
                                                            CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>                                                    </td>
  <td></td>
</tr>
<tr>
  <td class="texttopic">&nbsp;</td>
  <td>
                  <asp:Button ID="cmdView" runat="server" CssClass="btn btn-pink" text="View" Width="100px" />                  
                  <asp:Button ID="cmdExportScore" runat="server" CssClass="btn btn-primary" Width="100px" text="Export" />                  
                  <asp:Button ID="cmdExportScoreAll" runat="server" CssClass="btn btn-success" text="Export for Verify Score(s)" />                  
    </td>
  <td>&nbsp;</td>
</tr>
  </table>  
     
                                   
</div>
            <div class="box-footer clearfix">
            
            </div>
          </div> 
              

              
    


 <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายชื่อนิสิต/Student List</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">          
                       
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >Keyword</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" text="ค้นหา" Width="100px" />                  
                  <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" text="ดูทั้งหมด" Width="100px" />                </td>
            </tr>
            </table>


              <asp:GridView ID="grdData" CssClass="table table-hover"
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
            <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต (ID)">
                <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล (Name)">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา (Major)" />
                <asp:BoundField DataField="GradScore" HeaderText="คะแนนรวม (Score)" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Grade" HeaderText="Standing" />
                <asp:BoundField DataField="AssessorName" HeaderText="ชื่อผู้ประเมิน (Evaluator)" />
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th"  Font-Bold="false" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>                
</div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels" 
                 Text="ยังไม่ได้รับคัดเลือกแหล่งฝึกให้" Width="99%"></asp:Label>
            </div>
          </div>
</section>     
</asp:Content>
