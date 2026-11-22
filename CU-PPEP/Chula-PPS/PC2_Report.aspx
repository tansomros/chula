<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_Report.aspx.vb" Inherits=".PC2_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
      <section class="content-header">
      <h1>รายงานผลการคัดเลือก PC2</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

          <div class="col-md-2">
          <div class="form-group">
            <label>ปีการศึกษา</label>
     <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"  CssClass="form-control select2"> </asp:DropDownList> 
          </div>

        </div>

                  <div class="col-md-4">
          <div class="form-group">
            <label>รายวิชา</label> 
 <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList>  
          </div>

        </div>


                  <div class="col-md-2">
          <div class="form-group">
            <label> </label>  <br />
  <asp:Button ID="cmdSave" runat="server" text="Export to Excel" CssClass="buttonGreen" />
          </div>

        </div>


            </div>
            <div class="box-footer text-center clearfix">
          
          
           
            </div>
          </div>
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ได้สิทธิ์สอบในวิชานี้แล้ว&nbsp; <asp:Label ID="lblAssmCount" runat="server"></asp:Label>&nbsp;คน </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
         <tr>
          <td valign="top"> 
              
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td ><asp:Button ID="cmdFind" runat="server" text="ค้นหา" CssClass="btn btn-warning" Width="100px"  />              </td>
            </tr>
            </table>         </td>
        </tr>


        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
                <asp:BoundField HeaderText="รหัส" DataField="StudentCode">                
                </asp:BoundField>
                <asp:BoundField DataField="StudentName" HeaderText="ชื่อนิสิต" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขา" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ชั้นปี" DataField="LevelClass">
                </asp:BoundField> 
            <asp:BoundField DataField="SubjectName" HeaderText="ชื่อรายวิชาสอบ">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
       
       
    </table>
                        
</div>
            <div class="box-footer text-center clearfix">
           
            </div>
          </div>

        <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่อนิสิตที่มีสิทธิ์สอบแต่ยังไม่ได้วิชาสอบ ทั้งหมด &nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
 <table border="0" >
            <tr>
              <td>ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server"  text="ค้นหา" CssClass="btn btn-warning" Width="100px" />  </td>
            </tr>
     
      
          </table>
              <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="StudentCode" CssClass="table table-hover" AllowPaging="True" PageSize="5">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" Width="90px" />                      </asp:BoundField>
            <asp:BoundField HeaderText="ชื่อ" DataField="FirstName">

                <HeaderStyle HorizontalAlign="Left" />

              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="นามสกุล" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />                </asp:BoundField>
                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปีที่">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
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
                        
</div>
            <div class="box-footer text-center clearfix"><asp:Label ID="lblNo" runat="server" CssClass="validateAlert"       
                  Text="ไม่พบนิสิตที่ยังไม่ได้รายวิชาสอบ หรือ นิสิตได้รายวิชาสอบครบเรียบร้อยแล้ว"></asp:Label>
            </div>
          </div>


   

     
   </section>
    
</asp:Content>
