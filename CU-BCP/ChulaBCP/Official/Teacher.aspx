<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Teacher.aspx.vb" Inherits=".Teacher" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
 <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div><asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label>
                                    <div class="page-title-subheading">จัดการข้อมูลอาจารย์</div>
                                </div>
                            </div>
                        </div>
                    </div>
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
                 <div class="row">
           <div class="col-md-12">
  <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server"  />  
           </div>
                      <div class="col-md-12">
             <asp:Button ID="cmdImport" CssClass="btn btn-success" runat="server" Text="import" Width="100" />  

           </div>
                     </div>       

                </div>
            <div class="box-footer clearfix">
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>  
           <asp:Label ID="lblResult" runat="server" CssClass="GreenAlert" Width="90%"></asp:Label>  
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
               <table border="0" class="table-borderless">
            <tr>
              <td>ค้นหา</td>
              <td>
                  <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>                </td>
              <td >
                 <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="100" Text="ค้นหา"></asp:Button>
    <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="100" Text="ดูทั้งหมด"></asp:Button>  </td><td  class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก                       ชื่อ,  นามสกุล</td>
            </tr>
            </table>   
                <br />
                 <asp:Label ID="lblStudentCount" runat="server"></asp:Label> 
         <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" AllowPaging="True" CssClass="table table-hover" 
                             Font-Bold="False" Width="100%">                        
                        <columns>
                            <asp:BoundField DataField="nRow" HeaderText="No">
                            <HeaderStyle CssClass="text-center" HorizontalAlign="Center" />
                            <ItemStyle CssClass="text-center" HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                        <asp:BoundField DataField="PrefixName" HeaderText="คำนำหน้า" >                            </asp:BoundField>
                            <asp:BoundField DataField="FName" HeaderText="ชื่อ" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField DataField="LName" HeaderText="นามสกุล" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField HeaderText="ตำแหน่ง" DataField="PositionName">
                            <ItemStyle VerticalAlign="Middle" />                            </asp:BoundField>
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
                        <headerstyle Font-Bold="True"   />                     
                         
                     </asp:GridView>                                         
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>         
    </section>
</asp:Content>
