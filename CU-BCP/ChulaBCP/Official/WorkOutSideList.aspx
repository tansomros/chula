<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Official/OfficialSite.Master" CodeBehind="WorkOutSideList.aspx.vb" Inherits="WorkOutSideList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>การปฏิบัติงานนอก มทส. 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Ticket</li>
      </ol>
    </section>

<section class="content">
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">ค้นหา</h3>   
          <div class="box-tools pull-right"><asp:Button ID="cmdNew" runat="server" CssClass="buttonlink" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>
               
       
             
                       
        </div>
        <div class="box-body">
            <table>
                <tr>
                    <td width="120">ปี</td>
                    <td><asp:DropDownList ID="ddlYear" runat="server" CssClass="Objcontrol"></asp:DropDownList>
                    </td> 
                </tr>
                <tr>
                    <td>งานด้าน</td>
                    <td><asp:DropDownList ID="ddlWorkType" runat="server" CssClass="Objcontrol"></asp:DropDownList>
                    </td> 
                </tr>
                <tr>
                    <td>หมวดการอบรม</td>
                    <td><asp:DropDownList ID="ddlGroup" runat="server" CssClass="Objcontrol"></asp:DropDownList>
                    </td> 
                </tr>
                    <tr>
                    <td>ผลงานนำเสนอ</td>
                    <td>
                <asp:RadioButtonList ID="optIsPresented" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="">ทั้งหมด</asp:ListItem>
                    <asp:ListItem Value="Y">มี</asp:ListItem>
                    <asp:ListItem Value="N">ไม่มี</asp:ListItem>
                </asp:RadioButtonList>
                        </td> 
                </tr>
                    <tr>
                    <td>ประเทศ</td>
                    <td><asp:DropDownList ID="ddlCountry" runat="server" CssClass="Objcontrol"></asp:DropDownList>
                        </td> 
                </tr>
                    <tr>
                    <td>งบ</td>
                    <td><asp:DropDownList ID="ddlBudget" runat="server" CssClass="Objcontrol"></asp:DropDownList>
                        </td> 
                </tr>
                    <tr>
                    <td>ค้นหา</td>
                    <td><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox> </td> 
                </tr>
                    <tr>
                    <td>&nbsp;</td>
                    <td><asp:Button ID="cmdSearch" runat="server" CssClass="buttonSmall" Text="ค้นหา" Width="70px" />      

                        &nbsp;</td> 
                </tr>
            </table>                                          
    </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->    


                 
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการข้อมูลการปฏิบัติงานนอก มทส.</h3>   
          <div class="box-tools pull-right">
              <asp:Button ID="cmdExport" runat="server" CssClass="buttonOrange" Text="Export to Excel" />  
              <asp:Button ID="cmdAdd" runat="server" CssClass="buttonUpdate" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>
               
       
             
                       
        </div>
        <div class="box-body">
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#34495E" GridLines="Horizontal" Width="100%" AllowPaging="True" BorderStyle="None" CssClass="gridnormal">
                        <RowStyle BackColor="#FFFFFF" VerticalAlign="Top" CssClass="gridrow" />
                        <columns>
                            <asp:TemplateField HeaderText="รายละเอียดการปฏิบัติงาน">
                                <ItemTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                      <table style="width: 100%;">
                                        <tr>
                                            <td class="gridtopic" style="width: 40px;">วันที่:</td>
                                            <td class="gridtext3"  style="width:300px;text-align: left;">
                                                <asp:Label ID="Label1"  runat="server" Text='<%# DisplayRangeDateTH(DataBinder.Eval(Container.DataItem, "StartDate"), DataBinder.Eval(Container.DataItem, "EndDate"))  %>'></asp:Label></td>
                                            <td class="gridtopic"  style="width: 55px;">งานด้าน:</td>
                                            <td class="gridtext3" style="width: 30%;text-align: left;">
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkTypeName") %>'></asp:Label></td>
                                        
                                            <td class="gridtopic"  style="width: 65px;">หมวด:</td>
                                            <td class="gridtext3" style="text-align:left;">
                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GroupName") %>'></asp:Label></td>
                                        </tr>  
                                    </table>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 250px;">
                                                            <asp:Label ID="Label4" runat="server"  CssClass="gridtext1" Text='<%# DataBinder.Eval(Container.DataItem, "PersonName_TH") %>'></asp:Label>
                                                        </td>
                                                        <td class="gridtopic"  style="width: 70px;">ชื่อผลงาน :</td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server"  CssClass="gridtext2"  Text='<%# DataBinder.Eval(Container.DataItem, "PortfolioName") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </td>
                                        </tr>    
                                         <tr>
                                            <td>
                                                 <table style="width: 100%;">
                                                    <tr>
                                                        <td class="gridtopic"  style="width: 80px;">งานประชุม :</td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server"  CssClass="gridtext2" Text='<%# DataBinder.Eval(Container.DataItem, "SeminarName") %>'></asp:Label>
                                                        </td>
                                                        <td  style="width: 200px;">
                                                            <asp:Label ID="Label7" runat="server"  CssClass="gridtext2" Text='<%# "ประเทศ" & DataBinder.Eval(Container.DataItem, "CountryName") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </td>
                                        </tr>   
                                                                         
                                    </table>
                                    
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridheader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="gridalternatingrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                                
    </div>
        <!-- /.box-body -->
        <div class="box-footer">
       
        </div>
        <!-- /.box-footer-->
      </div>
      <!-- /.box -->            

  </section>                  

</asp:Content>
