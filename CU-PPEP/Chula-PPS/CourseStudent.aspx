<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CourseStudent.aspx.vb" Inherits=".CourseStudent" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">       
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1>กำหนดรายชื่อนิสิตในรายวิชา</h1>   
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
  <td align="left">
                                                      <asp:Label ID="lblNot" runat="server" ForeColor="Red" Text="ไม่พบรายวิชาที่เปิดฝึกในปีนี้"></asp:Label>
    </td>
</tr>
  </table>
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    

 <asp:Panel ID="Panel1" runat="server">  
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ต้องฝึกในรายวิชานี้ทั้งหมด&nbsp; 
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
              <td><asp:Button ID="cmdFindStd" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>              </td>
            </tr>
           
          </table>
 <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" Width="120px"/>

            </asp:BoundField>

                <asp:BoundField DataField="Prefix">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Left" Width="90px"/>
                </asp:BoundField>
            <asp:BoundField HeaderText="ชื่อ" DataField="FirstName">

              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>

                <asp:BoundField DataField="LastName" HeaderText="นามสกุล">
                 <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>

                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปีที่">
                     <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />              
                </asp:BoundField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/icon-delete.png"     CommandArgument='<%# DataBinder.Eval(Container.DataItem, "itemID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" HorizontalAlign="Left" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>

<asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ยังไม่พบนิสิตที่กำหนดให้ในวิชานี้ โปรดเลือกนิสิตที่ต้องการเปิดด้านล่าง" Width="100%"></asp:Label>
  <table border="0" cellspacing="2" cellpadding="0">
            <tr>
              <td>หรือ ลบทั้งสาขาวิชา</td>
              <td><asp:DropDownList ID="ddlMajorDel" runat="server" CssClass="form-control select2"> </asp:DropDownList>              </td>
              <td>ชั้นปี</td>
              <td><asp:DropDownList ID="ddlLevelDel" runat="server" CssClass="form-control select2" Width="50px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                  <asp:ListItem>6</asp:ListItem>
                          <asp:ListItem>7</asp:ListItem>
              </asp:DropDownList></td>
              <td><asp:LinkButton ID="lnkSubmitDel" runat="server"  CssClass="buttonModal">ตกลง</asp:LinkButton></td>
            </tr>
         </table>



  
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

        <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เพิ่มนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
           <table border="0" align="left" cellPadding="1" cellSpacing="1" width="100%">   
    <tr>
          <td valign="top" > 
  <table border="0" cellspacing="1" cellpadding="0" align="left">
            <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
                <td align="center" width="50">สาขา</td>
                  <td>
                      <asp:DropDownList ID="ddlMajorSearch" runat="server" CssClass="form-control select2">
                      </asp:DropDownList>
                </td>
                  <td align="center" width="50">ชั้นปี</td>
                  <td width="60">
                      <asp:DropDownList ID="ddlLevelSearch" runat="server" CssClass="form-control select2" Width="50px">
                          <asp:ListItem>1</asp:ListItem>
                          <asp:ListItem>2</asp:ListItem>
                          <asp:ListItem>3</asp:ListItem>
                          <asp:ListItem>4</asp:ListItem>
                          <asp:ListItem>5</asp:ListItem>
                          <asp:ListItem>6</asp:ListItem>
                          <asp:ListItem>7</asp:ListItem>
                      </asp:DropDownList>
                </td>
              <td > <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>            </td>
            </tr>
            </table>         </td>
        </tr>
    <tr>
          <td align="center" valign="top" class="mailbox-messages">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="False" 
                  DataKeyNames="Student_Code">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top"  Width="90px" />                      </asp:BoundField>
                <asp:BoundField DataField="Prefix">
                <ItemStyle HorizontalAlign="Left" Width="90px"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="ชื่อ" DataField="FirstName">                
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="นามสกุล">
                  <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />        

                </asp:BoundField> 

                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา">
                <ItemStyle HorizontalAlign="Center" />            

                </asp:BoundField> 
                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปีที่">
                 <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />                </asp:BoundField>

            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True"  
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr> 
    <tr>
        <td align="center" valign="top">
            <asp:Label ID="lblNotic" runat="server" CssClass="validateAlert" Text="กรุณาเลือกเงื่อนไขเพื่อค้นหาข้อมูลก่อน" Width="99%"></asp:Label>
        </td>
    </tr>
    <tr>    
          <td align="left" valign="top">
              <table border="0" cellspacing="2" cellpadding="0">
            <tr>
              <td>หรือ เพิ่มทั้งสาขาวิชา</td>
              <td><asp:DropDownList ID="ddlMajorAdd" runat="server" CssClass="form-control select2"> </asp:DropDownList>              </td>
              <td>ชั้นปี</td>
              <td><asp:DropDownList ID="ddlLevelAdd" runat="server" CssClass="form-control select2">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                  <asp:ListItem>6</asp:ListItem>
                          <asp:ListItem>7</asp:ListItem>
              </asp:DropDownList></td>
              <td><asp:LinkButton ID="lnkSubmitAdd" runat="server"  CssClass="buttonModal">ตกลง</asp:LinkButton></td>
            </tr>
          </table></td>
      </tr>       
    <tr>
          <td align="center" valign="top">
         <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>         </td>
        </tr>
  
    
</table>                             
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

 </asp:Panel>   
  
    


    </section>

    
</asp:Content>
