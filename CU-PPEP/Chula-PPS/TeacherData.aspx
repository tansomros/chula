<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TeacherData.aspx.vb" Inherits=".TeacherData" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <section class="content-header">
      <h1>จัดการข้อมูลอาจารย์</h1>   
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
         <table width="100%" border="0" cellspacing="2" cellpadding="0">
      
       <tr>
         <td>
             <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Button ID="cmdImport" CssClass="btn btn-warning" runat="server" Text="import" Width="100" />           </td>
         </tr>
       <tr>
         <td>
             <asp:Label ID="lblResult" runat="server" CssClass="GreenAlert" Width="90%"></asp:Label>           </td>
         </tr>
     </table>

                </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข ข้อมูลอาจารย์</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

 <table width="100%" border="0" cellspacing="2" cellpadding="0"> 
   <tr> 
     <td align="right"  colspan="2">        <asp:Label ID="lblMode" runat="server" Text="Mode : Add New"                 CssClass="block_Mode"></asp:Label>            </td>
   </tr>
   <tr>
     <td width="50%" >
         
         <table width="100%" border="0" align="left" cellpadding="0" cellspacing="2">
      <tr>
         <td width="100">ID</td>
         <td>
                     <asp:Label ID="lblID" runat="server"></asp:Label>&nbsp;&nbsp;<span class="text12_nblue">(กำหนดให้อัตโนติโดยโปรแกรม)</span></td>
         <td width="100">คำนำหน้า</td>
         <td><asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"> </asp:DropDownList></td>
      </tr>
       
      
       <tr>
         <td>ชื่อ</td>
         <td>
             <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>           </td>
         <td>นามสกุล</td>
         <td><asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox></td>
       </tr>
      
       <tr>
         <td>ตำแหน่ง</td>
         <td>
            <asp:DropDownList CssClass="form-control select2" ID="ddlPosition" runat="server">            </asp:DropDownList>           </td>
         <td>สังกัดภาควิชา</td>
         <td>
            <asp:DropDownList CssClass="form-control select2" ID="ddlDepartment" runat="server">            </asp:DropDownList>          
           </td>
       </tr>
      
       <tr>
         <td>&nbsp;</td>
         <td>
             &nbsp;</td>
         <td>สถานะ</td>
         <td>
             <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งานปัจจุบัน (Active)" />
           </td>
       </tr>
      
       <tr>
         <td colspan="4" align="center"><span class="texttopic">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
         </span></td>
         </tr>
       
     </table>


     </td>
    </tr>
   <tr>
     <td align="center">
         <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>   
 </td>
    </tr>
 </table>
                                                  
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
       <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่ออาจารย์</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
               <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                 <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>
    <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="70" Text="ดูทั้งหมด"></asp:Button>               </td><td  class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก 
                      ชื่อ,  นามสกุล หรือ ภาควิชา</td>
            </tr>
            </table>   
                 <asp:Label ID="lblStudentCount" runat="server"></asp:Label> 
         <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False" Width="100%">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                        <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้า" >                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อ" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField HeaderText="ตำแหน่ง" DataField="PositionName">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />                            </asp:BoundField>
                            <asp:BoundField DataField="DepartmentName" HeaderText="สังกัดภาควิชา" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' 
                                        ImageUrl="images/icon-edit.png" />
                                    &nbsp;
                                    <asp:ImageButton ID="imgDel" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' 
                                        ImageUrl="images/icon-delete.png" />                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>                                         
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>         
    </section>
</asp:Content>
