<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportAssesmentByStudent.aspx.vb" Inherits=".ReportAssesmentByStudent" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/jquery-1.10.2.js"></script> 

<link rel="stylesheet" type="text/css" href="css/jquery-ui.css">
   <script src="js/jquery-1.9.1.js"></script>
  <script src="js/jquery-ui.js"></script>
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>ผลการจัดสรรแหล่งฝึก</h1>   
    </section>

<section class="content">  
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>

              <h3 class="box-title">เลือกเงื่อนไขการค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="center" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" 
                                                            CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา :</td>
  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" 
                                                          Width="400px" CssClass="form-control select2" AutoPostBack="True">                                                      </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td class="texttopic">นิสิต :</td>
   <td class="texttopic">
       <asp:TextBox ID="txtStudent" runat="server" Width="200px"></asp:TextBox>
    </td>
  </tr>
<tr>
 <td class="texttopic"></td>
  <td class="texttopic"> 
 <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>


  </td>
</tr>
  </table>     

            </td>
      </tr>
       <tr>
          <td  align="center" valign="top"  >
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>      </td>
      </tr>
      
        <tr>
         <td  align="center" valign="top" >
              <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="AssessmentID" PageSize="20">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:BoundField DataField="AssessmentID" HeaderText="Seq No.">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต" />
                <asp:TemplateField HeaderText="ชื่อ-สกุล">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") +" " + DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ผลัดที่" DataField="PhaseNo" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PhaseName" HeaderText="วันที่" />
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">

                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                      
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="AliasName" HeaderText="รายวิชา" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="">
              <itemtemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" 
                                    ImageUrl="images/icon-edit.png"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessmentID")  %>' /> 
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                                         
                      CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessmentID")  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
              </td>
       </tr>
       <tr>
         <td  align="center" valign="top"  >
             <asp:Label ID="lblResult" runat="server" CssClass="validateAlert" 
                 Text="ไม่มีข้อมูลตามเงื่อนไขที่ท่านค้นหา" Width="95%"></asp:Label>
           </td>
       </tr>
    
    </table>
    </div>
            <div class="box-footer clearfix">
           
                   
                               
            </div>
          </div>
    </section>
</asp:Content>
