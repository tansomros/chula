<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="Person.aspx.vb" Inherits="Person" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-user-female icon-gradient bg-primary"></i>
                                </div>
                                <div>Person Management
                                    <div class="page-title-subheading">จัดการข้อมูลบุคลากร</div>
                                </div>
                            </div>
                        </div>
                    </div>    

<section class="content">
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">ค้นหา</h2>   
          <div class="box-tools pull-right">          
          </div>      
        </div>
        <div class="box-body">
             <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ประเภท</label>
                                            <asp:DropDownList ID="ddlPersonType" runat="server" CssClass="form-control select2">
                                <asp:ListItem Selected="True" Value="1">คณะกรรมการบริหารหลักสูตร</asp:ListItem>
                    <asp:ListItem Value="2">คณาจารย์</asp:ListItem>
                  <asp:ListItem Value="3">ศิษย์เก่า</asp:ListItem>
   <asp:ListItem Value="4">บุคลากร/เจ้าหน้าที่</asp:ListItem>
                  </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>ค้นหา</label> 
                                            <asp:TextBox ID="txtSearch" runat="server"   CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                  <div class="col-md-4">
                                        <div class="form-group">
                                              <label>&nbsp;</label> 
                                            <br /> 
                                                 <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="100px" />  
                                        </div>
                                    </div>

                                </div>                                     
    </div>
   
        <div class="box-footer text-center">
  
        </div>   
      </div>                 
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายชื่อสมาชิก</h2>   
          <div class="box-tools pull-right"><asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-primary" Text="เพิ่มข้อมูลใหม่" />          
          </div>                       
        </div>
        <div class="box-body">
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#34495E" GridLines="None" Width="100%"  BorderStyle="None" CssClass="table table-hover" AllowPaging="True">
                        <RowStyle BackColor="#FFFFFF" VerticalAlign="Top" CssClass="gridrow" />
                        <columns>
                            <asp:BoundField DataField="nRow" HeaderText="No." >
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ชื่อ-นามสกุล">
                                <ItemTemplate>
                                    <asp:Label ID="lblNameTH" runat="server" CssClass="NameTH" Text='<%# DataBinder.Eval(Container.DataItem, "FullNameTH") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="PositionNameTH" HeaderText="ตำแหน่ง">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="MajorNameTH" HeaderText="สาขา/ส่วนงาน" >
                             <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                           
                             <asp:TemplateField HeaderText="Public">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="imgStatus" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' CssClass="gridbutton" ImageUrl='<%# IIf(DataBinder.Eval(Container.DataItem, "StatusFlag") = "A", "images/on.png", "images/off.png") %>' />
                                 </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PersonID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle  HorizontalAlign="Center" CssClass="dc_pagination" />
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
