<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisorSend.aspx.vb" Inherits=".SupervisorSend" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<section class="content-header">
      <h1>บันทึกส่งงานอาจารย์นิเทศ</h1>   
    </section>

<section class="content">  

     <div class="box box-pink">
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
                                                    <td align="left">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left">&nbsp;</td>
</tr>
<tr>
  <td align="left">รายวิชา :</td>
  <td align="left">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
  <td align="left">&nbsp;</td>
</tr>
  </table>         
</div>
            <div class="box-footer">
           <asp:Label ID="lblNot" runat="server"  Text="ไม่พบรายวิชาที่เปิดฝึกในปีนี้" CssClass="validateAlert"></asp:Label>
            </div>
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">รายชื่ออาจารย์ที่ส่งงานนิเทศในวิชานี้แล้วทั้งหมด&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
       <table border="0" cellspacing="2" cellpadding="0">
           <tr>
              <td>ค้นหา</td>
              <td width="150">
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>            </td>
            </tr>
           
          </table>
    


              <asp:GridView ID="grdSend" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" DataKeyNames="UID">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField DataField="nRow" HeaderText="No.">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" />                      </asp:BoundField>
            <asp:BoundField HeaderText="ชื่อ-สกุล" DataField="PersonName">

                </asp:BoundField>
                <asp:BoundField DataField="DepartmentName" HeaderText="ภาควิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>
                <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึกออกนิเทศ" />
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    
                      
                      CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
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
                  
                  Text="ยังไม่พบรายการส่งงาน" Width="100%"></asp:Label>                                                  
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เลือกอาจารย์ส่งงาน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 

        <table>
            <tr>
              <td >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td > <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>              </td>
            </tr>
            </table>       
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="UID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
                <asp:BoundField HeaderText="ชื่อ-สกุล" DataField="PersonName">                
                </asp:BoundField>
                <asp:BoundField DataField="DepartmentName" HeaderText="ภาควิชา" />
            <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึกออกนิเทศ">
              <itemstyle HorizontalAlign="Left" />                      
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
                                 
</div>
            <div class="box-footer text-center">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>  
            </div>
          </div>
                       
    </section> 
</asp:Content>
