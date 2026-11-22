<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentNoRegister.aspx.vb" Inherits=".StudentNoRegister" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1> รายชื่อนิสิตที่ยังไม่ได้เลือกแหล่งฝึก</h1>   
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
                          
<table border="0" align="left" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา :</td>
  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                                                          Width="400px" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
  <td align="left"></td>
</tr>
  </table>   
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
  

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ยังไม่ได้ลงทะเบียนเลือกแหล่งฝึกในรายวิชานี้ทั้งหมด&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
               
                
          <table>
              <tr>
              <td>ค้นหา</td>
              <td width="150">
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="200px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>         </td>
            </tr>
           
          </table>

                
              <asp:GridView ID="grdStudent"  CssClass="table table-hover"
                             runat="server" CellPadding="2" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="Student_Code">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField HeaderText="รหัสนิสิต" DataField="Student_Code">

                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />

                </asp:BoundField>
            <asp:BoundField HeaderText="ชื่อ " DataField="FirstName">                      
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="รายละเอียด" Visible="False">
                <ItemTemplate>
                    <asp:HyperLink ID="hlnkView" runat="server" Target="_blank">ดูรายละเอียด</asp:HyperLink>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
                <asp:TemplateField HeaderText="เลือก" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSelect" runat="server"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Student_Code")  %>' CssClass="buttonModal">เลือก</asp:LinkButton>
                    </ItemTemplate>
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
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"  
                  
                  Text="ไม่พบนิสิตที่ยังไม่ได้แหล่งฝึกในวิชานี้"></asp:Label>       
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
      
             
  
    
    
     
    


    
</asp:Content>
