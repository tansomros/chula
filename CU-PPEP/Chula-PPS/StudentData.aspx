<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentData.aspx.vb" Inherits=".StudentData" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <section class="content-header">
      <h1>จัดการข้อมูลนิสิต</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-upload"></i>

              <h3 class="box-title">Import from Excel file</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

    <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-condensed">
       <tr>
         <td height="30">
            <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" /></td>
         </tr>
       <tr>
         <td>
             <asp:Button ID="cmdImport" runat="server" CssClass="btn btn-warning" Text="import" Width="100px" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels" Width="100%"></asp:Label>           </td>
         </tr>
     </table>             
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข ข้อมูลนิสิต</h3>
             
                 <div class="box-tools pull-right">  
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
<table width="100%" border="0" cellpadding="0" cellspacing="2">
       <tr>
         <td width="100">รหัสนิสิต</td>
         <td><asp:TextBox ID="txtCode" runat="server" CssClass="text-center" Width="300px"></asp:TextBox>             
             </td> 
            <td width="100">Ref.</td>
         <td>
              <asp:Label 
                 ID="lblStdCode" runat="server"></asp:Label>             
            </td>
       </tr>
       <tr>
         <td>ชื่อ</td>
         <td>
             <asp:TextBox ID="txtFirstName" runat="server" Width="300px"></asp:TextBox>           </td>
         <td width="100">นามสกุล</td>
         <td><asp:TextBox ID="txtLastName" runat="server" Width="300px"></asp:TextBox></td>
       </tr>
       <tr>
         <td>สาขาวิชา</td>
         <td>
            <asp:DropDownList ID="ddlMajor" runat="server" Width="300px" AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList></td>
         <td>สาขา (แขนง)</td>
         <td><asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
             <asp:DropDownList ID="ddlMinor" runat="server" Width="300px" CssClass="form-control select2"> </asp:DropDownList>
           </ContentTemplate>
           <Triggers>
             <asp:AsyncPostBackTrigger ControlID="ddlMajor" 
                        EventName="SelectedIndexChanged" />
           </Triggers>
         </asp:UpdatePanel></td>
       </tr>
       <tr>
         
         <td>สถานะ</td>
         <td>    <asp:DropDownList CssClass="form-control select2" ID="ddlStudentStatus" runat="server" width="300">            </asp:DropDownList>              
           </td> 
           <td>ชั้นปีที่</td>
         <td> <asp:TextBox ID="txtLevelClass" runat="server" Width="80px"></asp:TextBox>
                       </td>
       </tr>
       <tr>
         <td>อ.ที่ปรึกษา</td>
         <td>
            <asp:DropDownList CssClass="form-control select2" ID="ddlAdvisor" runat="server" Width="300px">            </asp:DropDownList>          </td>
         <td>GPAX</td>
         <td><asp:TextBox ID="txtGPAX" runat="server" Width="80px"></asp:TextBox></td>
       </tr>
       <tr>
         <td colspan="4" align="center"><span class="texttopic">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Width="100" Text="ยกเลิก"></asp:Button>
         </span></td>
         </tr>
     </table>
      
                                                   
</div>          
            
            <div class="box-footer clearfix">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>   
            </div>
          </div>



    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่อนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
 <table width="100%" border="0" cellspacing="2" cellpadding="0">    
       <tr>
          <td  align="left" valign="top">
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:RadioButtonList ID="optStatus" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Selected="True" Value="0">นิสิตทั้งหมด</asp:ListItem>
                      <asp:ListItem Value="10">นิสิตปัจจุบัน</asp:ListItem>
                      <asp:ListItem Value="40">นิสิตสำเร็จการศึกษา</asp:ListItem>
                  </asp:RadioButtonList>
                </td>
              <td >
                   <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>  
                   <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="70" Text="ดูทั้งหมด"></asp:Button>   </td>
            </tr>
            <tr>
                  <td colspan="4" class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก รหัสนิสิต , ชื่อ หรือ  นามสกุล</td>
              </tr>
          </table>


          </td>
      </tr>
       <tr>
          <td align="left" valign="top"  >
              <asp:Label ID="lblStudentCount" runat="server"></asp:Label>           </td>
    </tr>
   <tr>
     <td>
         <asp:GridView ID="grdData"  runat="server" CellPadding="0" 
                                                        GridLines="None" 
             AllowPaging="True" CssClass="table table-hover" 
                             Font-Bold="False" Width="100%" AutoGenerateColumns="False" 
             AllowSorting="True">
                        <RowStyle BackColor="#F7F7F7" />
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            HorizontalAlign="Center" />                     
                        <Columns>
                            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต" >
                            <ItemStyle Width="90px" HorizontalAlign="Center" />                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อนิสิต" />
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
                            <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                            <asp:BoundField DataField="MinorName" HeaderText="สาขา" />
                            <asp:BoundField DataField="SubMinorName" HeaderText="แขนง" />
                            <asp:BoundField DataField="StudentLevelClass" HeaderText="ชั้นปีที่">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Student_Code") %>' ImageUrl="images/view.png" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Student_Code") %>' 
                                        ImageUrl="images/icon-edit.png" />
                                    &nbsp;
                                    <asp:ImageButton ID="imgDel" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Student_Code") %>' 
                                        ImageUrl="images/icon-delete.png" />                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView></td>
    </tr>
 </table>
                                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section>
</asp:Content>
