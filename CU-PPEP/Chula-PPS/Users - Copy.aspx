<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Users.aspx.vb" Inherits=".Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="css/custyles.css"/>
    <link href="css/pagestyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
      <h1>จัดการข้อมูล User Account</h1>     
    </section>
<section class="content">
    <div class="box box-success box-solid">
            <div class="box-header">
              <i class="fa fa-users"></i>

              <h3 class="box-title">Step 1. เลือกกลุ่มผู้ใช้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">       
            <table border="0" cellpadding="0" cellspacing="2">                                               
                                          <tr>
                                          
                                            <td >
                                              <table border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="65" align="left">กลุ่มผู้ใช้                                              </td>
                                                  <td><asp:DropDownList ID="ddlUserGroup" 
                                                    runat="server"  AutoPostBack="True" CssClass="form-control select2">
                                                    <asp:ListItem Selected="True" Value="0">--- เลือกกลุ่มผู้ใช้ ---</asp:ListItem>
                                                    <asp:ListItem Value="1">นิสิต</asp:ListItem>
                                                    <asp:ListItem Value="2">อาจารย์</asp:ListItem>
                                                    <asp:ListItem Value="3">อาจารย์แหล่งฝึก</asp:ListItem>
                                                    <asp:ListItem Value="4">เจ้าหน้าที่/อื่นๆ</asp:ListItem>
                                                  </asp:DropDownList></td>
                                                </tr>
                                              </table></td>
                                          </tr>
                                          <tr>                                          
                                            <td >
<asp:UpdatePanel ID="UpdatePanelSTD" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnStudent" runat="server">
        <table>
     <tr>
     <td width="65" ><asp:Label ID="lblselect" runat="server" Text="นิสิต"></asp:Label></td>
     <td><asp:DropDownList ID="ddlUserID" runat="server" AutoPostBack="True" CssClass="form-control select2" > </asp:DropDownList></td>
    <td> <asp:Image ID="imgArrowUser" runat="server" ImageUrl="images/arrow-orange-icons.png" /></td>
   <td><asp:TextBox ID="txtFindSpec" runat="server" Width="100px"></asp:TextBox></td>
         <td>
             <asp:LinkButton ID="lnkFind" runat="server" CssClass="buttonLink">ค้นหา</asp:LinkButton>
         </td>
   </tr>
   </table>     

        </asp:Panel>
   </ContentTemplate>
     <Triggers>
       <asp:AsyncPostBackTrigger ControlID="ddlUserGroup" EventName="SelectedIndexChanged" />
       <asp:AsyncPostBackTrigger ControlID="lnkFind" EventName="Click" />
     </Triggers>
 </asp:UpdatePanel>

                                            </td>
                                          </tr>
                <tr>                                          
                                            <td >
<asp:UpdatePanel ID="UpdatePanelLocation" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnLocation" runat="server">
      <table border="0">
       <tr>
         <td width="65"><asp:Label ID="lblLocation" runat="server" Text="แหล่งฝึก"></asp:Label></td>
         <td><asp:DropDownList ID="ddlLocation" runat="server"  AutoPostBack="True" CssClass="form-control select2"></asp:DropDownList>
         </td>
         <td><asp:Image ID="imgArrowLocation" runat="server" ImageUrl="images/arrow-orange-icons.png" /></td>
         <td><asp:TextBox ID="txtFindLocation" runat="server" Width="100px"></asp:TextBox></td>
           <td><asp:LinkButton ID="lnkFindLocation" runat="server" CssClass="buttonLink">ค้นหา</asp:LinkButton></td>
      </tr>
     </table>
            </asp:Panel>
 </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlUserGroup" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="lnkFindLocation" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

                                            </td>
                                          </tr>
                <tr>                                          
                                            <td >
<asp:UpdatePanel ID="UpdatePanelPreceptor" runat="server">
                                                <ContentTemplate>
 <asp:Panel ID="pnPreceptor" runat="server">
     <table border="0">
                                                  <tr>
                                                    <td width="65"><asp:Label ID="lblPreceptor" runat="server" Text="อ.แหล่งฝึก"></asp:Label></td>
                                                    <td><asp:DropDownList ID="ddlPreceptor" runat="server" AutoPostBack="True" 
                                                        CssClass="form-control select2" >                                                    </asp:DropDownList></td>
                                                     
                                    </tr>
                                                </table>
</asp:Panel>  
</ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLocation" 
                                                        EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlUserGroup" 
                                                        EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="lnkFindLocation" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>                                          </td>
                                          </tr>

                <tr>                                          
                                            <td ><small class="label bg-success text-blue">
                                                *จะแสดงเฉพาะคนที่ยังไม่มี User เท่านั้น</small></td>
                                          </tr>

                                        </table>
                                        

 </div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblvalidate1" runat="server" CssClass="validateAlert" 
                                                Visible="False" Width="99%"></asp:Label>
            </div>
          </div>

       <div class="row">
        <!-- Left col -->
        <section class="col-lg-7 connectedSortable">  

     <div class="box box-primary box-solid">
            <div class="box-header">
              <i class="fa fa-user"></i>

              <h3 class="box-title">Step 2. บันทึกข้อมูลผู้ใช้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">       
                
                                            <table border="0" cellpadding="1" cellspacing="1">
                                         
                                          <tr>
                                            <td width="120" align="left" >UserID : </td>
                                            <td align="left" class="Normal"><asp:Label ID="lblID" runat="server"></asp:Label></td>
                                            <td align="left" class="Normal">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Username :</td>
                                            <td align="left" class="Normal"><asp:TextBox ID="txtUsername" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox></td>
                                            <td align="left" class="Normal"><asp:Image 
                                                      ID="imgAlert" runat="server" Height="16px" ImageUrl="images/alert_icon.png" 
                                                      Width="16px" /></td>
                                          </tr>
                                          <tr>
                                            <td align="left" >Password :</td>
                                            <td align="left" class="Normal"><span >
                                              <asp:TextBox ID="txtPassword" runat="server" 
                                                        CssClass="text" Width="200px"></asp:TextBox>
                                            </span></td>
                                            <td align="left" class="Normal">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >ชื่อ :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtFirstName" runat="server" 
                                                      Width="200px"></asp:TextBox></td>
                                            <td align="left" valign="top" class="Normal">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left" valign="top" >นามสกุล :</td>
                                            <td align="left" valign="top" class="Normal"><asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>                                            </td>
                                            <td align="left" valign="top" class="Normal">&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td align="left"  valign="top">อีเมล :</td>
                                            <td align="left" class="Normal"><asp:TextBox ID="txtMail" runat="server" 
                                                      Width="200px"></asp:TextBox>
                                              
                                              </td>
                                            <td align="left" class="Normal"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtMail" CssClass="validateAlert" 
                                                ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                ></asp:RegularExpressionValidator></td>
                                          </tr>
                                          <tr>
                                            <td colspan="3" align="left" >                                            </td>
                                          </tr>
                                        </table>
 </div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblvalidate2" runat="server" CssClass="validateAlert" 
                                                Visible="False" Width="99%"></asp:Label>
                                              </div>
          </div>
 </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
        <section class="col-lg-5 connectedSortable">
         
     <div class="box box-info box-solid">
            <div class="box-header">
              <i class="fa fa-lock"></i>

              <h3 class="box-title">Step 3. กำหนดสิทธิ์ให้ผู้ใช้</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">       
                 
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2">      
                                          <tr>
                                            <td align="left" class="texttopic" >สิทธิ์การใช้งาน :</td>
                                          </tr>
                                          <tr>
                                            <td align="left"><asp:CheckBoxList ID="chkRoles" runat="server" RepeatColumns="2"> </asp:CheckBoxList>                                            </td>
                                          </tr>
                                          <tr>
                                            <td><span class="texttopic">Status : </span>
                                              <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" /></td>
                                          </tr>                                          
                                        </table>
 </div>
            <div class="box-footer clearfix">
           <asp:Label ID="lblvalidate3" runat="server" CssClass="validateAlert" 
                                                    Visible="False" Width="99%"></asp:Label>
            </div>
          </div>
</section>
           </div>
 <div class="row">
    <div align="center">
     <asp:Button ID="cmdSave" runat="server" CssClass="buttonSave" Text="บันทึก" Width="100px"></asp:Button>

                                          <asp:Button ID="cmdClear" runat="server" 
                                                        CssClass="buttonSave" Text="ยกเลิก" Width="100px"></asp:Button></div>
     <br />
</div>

     <div class="row">
      
        <section class="col-lg-12 connectedSortable">  
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-key"></i>

              <h3 class="box-title">รายการผู้ใช้งานทั้งหมด</h3>
             
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
               <td width="50" > <asp:DropDownList ID="ddlGroupFind" 
                                                    runat="server"  AutoPostBack="True" 
                       CssClass="form-control select2" Width="150px">
                                                    <asp:ListItem Selected="True" Value="0">--- ทั้งหมด ---</asp:ListItem>
                                                    <asp:ListItem Value="1">นิสิต</asp:ListItem>
                                                    <asp:ListItem Value="2">อาจารย์</asp:ListItem>
                                                    <asp:ListItem Value="3">อาจารย์แหล่งฝึก</asp:ListItem>
                                                    <asp:ListItem Value="4">เจ้าหน้าที่/อื่นๆ</asp:ListItem>
                                                  </asp:DropDownList> </td>

              <td>
                  <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
              <td>
                  <asp:Button ID="cmdFind" runat="server" CssClass="buttonFind" Text="Search" Width="100px" />                </td>
            </tr>
            <tr>
                  <td colspan="3" class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก username , ชื่อ-สกุล , ชื่อแหล่งฝึก</td>
              </tr>
          </table>

                <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" CssClass="table table-hover" 
                             Font-Bold="False" PageSize="15">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField HeaderText="No." DataField="nRow">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />                            </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                            <asp:BoundField HeaderText="ชื่อ" DataField="Name" />
                            <asp:BoundField DataField="EMail" HeaderText="อีเมล" />
                            <asp:BoundField DataField="ProfileName" HeaderText="กลุ่ม" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                        <asp:BoundField DataField="LocationName" HeaderText="หน่วยงาน" >
                            <HeaderStyle HorizontalAlign="Left" />                            </asp:BoundField>
                            <asp:BoundField DataField="LastLogin" HeaderText="Last Login" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem,"IsPublic") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView> 
 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</section>
  </div>
     </section>
</asp:Content>
