<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CourseLocation.aspx.vb" Inherits=".CourseLocation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1>กำหนดแหล่งฝึกที่เปิดรับฝึกวิชาชีพ</h1>   
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
<table border="0" cellPadding="1" cellSpacing="1">
<tr>
                                                    <td class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
</tr>
<tr>
  <td  class="texttopic">รายวิชา :</td>
  <td  class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
</tr>
  </table>  
                
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายชื่อแหล่งฝึกในรายวิชานี้ทั้งหมด&nbsp;<asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;แหล่ง</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table width="100%" border="0" cellspacing="0" cellpadding="0">    
       <tr>
          <td   valign="top">              
              <table border="0" >
            <tr>
              <td>ค้นหา</td>
              <td width="150">
                  <asp:TextBox ID="txtSearchLocation" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td> 

                   <asp:Button ID="cmdFindLocationInCourse" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>


              </td>
            </tr>
           
          </table>
           </td>
      </tr>
       <tr>
          <td  valign="top">
              <asp:GridView ID="grdCourseLocation" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="itemID">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">

                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                      
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภท" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png"                                 
                      
                      CommandArgument='<%# DataBinder.Eval(Container.DataItem, "itemID") %>' />                        </itemtemplate>
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
                  
                  Text="ยังไม่พบแหล่งฝึกที่กำหนดให้ในวิชานี้ เลือกแหล่งฝึกที่ต้องการเปิดด้านล่าง"></asp:Label>           </td>
      </tr>
                            </table>
</div>
            <div class="box-footer clearfix">
            <table border="0" >
            <tr>
              <td>หรือ ลบทั้งประเภท</td>
              <td><asp:DropDownList ID="ddlLocationTypeDel" runat="server" CssClass="form-control select2"> </asp:DropDownList>
              </td>
              <td><asp:LinkButton ID="lnkSubmitDel" runat="server"  CssClass="buttonLink">ตกลง</asp:LinkButton>
              </td>
            </tr>
          </table>
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่มแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">
     <table width="100%">       
              <tr>
          <td valign="top" >
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td > <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>             </td>
            </tr>
            </table>         </td>
        </tr>


        <tr>
          <td valign="top" class="mailbox-messages">
              <asp:GridView ID="grdLocation" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เพิ่ม">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="LocationName" HeaderText="ชื่อแหล่งฝึก">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField HeaderText="ประเภท" DataField="LocationGroupName">                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
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
       <tr>
          <td valign="top"><table border="0" >
            <tr>
              <td>หรือ เพิ่มทั้งประเภท</td>
              <td><asp:DropDownList ID="ddlLocationTypeAdd" runat="server" CssClass="form-control select2"> </asp:DropDownList>
              </td>
              <td><asp:LinkButton ID="lnkSubmitAdd" runat="server"  CssClass="buttonLink">ตกลง</asp:LinkButton>
              </td>
            </tr>
          </table></td>
      </tr>
        
        <tr>
          <td align="center" valign="top">
        <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>    </td>
        </tr>
       
    </table>
  </div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

   
                       
    </section>   
</asp:Content>
