<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Subjects.aspx.vb" Inherits=".Subjects" %>

<%@ Register Assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    
      <section class="content-header">
      <h1>จัดการรายวิชา</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-plus-circle"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข รายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" EnableTheming="True" Theme="Material" Width="100%" BackColor="White">
                    <TabPages>
                        <dx:TabPage Text="General">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                   <table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" >
                                                        รหัส : 
                                                        <asp:Label ID="lblID" runat="server"></asp:Label>                                                    </td>
                                                    <td align="left" ><asp:TextBox ID="txtCode" runat="server" 
                                                         Width="89px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >
                                                        ชื่อภาษาไทย:</td>
                                                    <td align="left" ><asp:TextBox ID="txtNameTH" runat="server" 
                                                            Width="500px" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                  <td align="left" >ชื่อภาษาอังกฤษ :</td>
                                                  <td align="left" >
                                                      <asp:TextBox ID="txtNameEN" runat="server" Width="500px"></asp:TextBox>                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >Alias Name :</td>
                                                    <td align="left" >
                                                      <asp:TextBox ID="txtAliasName" runat="server" Width="300px"></asp:TextBox>                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >หน่วยกิต :</td>
                                                    <td align="left" >
                                                        <asp:TextBox ID="txtUnit" runat="server" 
                                                         Width="50px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >ชั้นปี :</td>
                                                    <td align="left" >
                                                        <asp:TextBox ID="txtLevelClass" runat="server" 
                                                         Width="50px" MaxLength="1">4</asp:TextBox>&nbsp;<span class="text9_blue">(เปิดฝึกนิสิตชั้นปีใด << ใช้คำนวน ชม.ฝึก)</span> </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >Status : </td>
                                                    <td align="left" ><asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                                                            Checked="True" /></td>
                                                </tr>                                               
  </table>  
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Certificate">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                      <table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" colspan="2" >
                                                        ชื่อภาษาไทย</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">บรรทัดที่ 1</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNameCert1TH" runat="server" MaxLength="60" Width="300px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >
                                                        บรรทัดที่ 2</td>
                                                    <td align="left" ><asp:TextBox ID="txtNameCert2TH" runat="server" 
                                                            Width="300px" ></asp:TextBox></td>
                                                </tr>
                                           <tr>
                                                    <td align="left" colspan="2" >
                                                        ชื่อภาษาอังกฤษ</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">Line 1</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNameCert1EN" runat="server" MaxLength="60" Width="300px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >
                                                        Line 2</td>
                                                    <td align="left" ><asp:TextBox ID="txtNameCert2EN" runat="server" 
                                                            Width="300px" ></asp:TextBox></td>
                                                </tr>
                                          </table>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
                
</div>
            <div class="box-footer clearfix text-center">
           
                <asp:Button runat="server" Text="บันทึก" CssClass="btn btn-pink" Width="100px" ID="cmdSave"></asp:Button>
                <asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-pink" Width="100px" ID="cmdClear"></asp:Button>
           
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-book"></i>

              <h3 class="box-title">รายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
       
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
       
        <tr>
          <td  align="left" valign="top">
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Text="ค้นหา" />                  
                  <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Text="ดูทั้งหมด" />                </td>
          
                  <td colspan="3" class="text12_nblue"><strong>&nbsp;*</strong>คำค้นหาสามารถค้นหาได้จาก ชื่อ หรือ รหัสวิชา</td>
              </tr>
          </table>

          </td>
      </tr>    
        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="ชื่อรายวิชา">
                    <ItemTemplate>                       
                                    <asp:Label ID="lblTH" runat="server" CssClass="NameTH" Text='<%# DataBinder.Eval(Container.DataItem, "NameTH") %>'></asp:Label>
                                <br />
                                    <asp:Label ID="lblEN" runat="server" CssClass="NameEN"  Text='<%# DataBinder.Eval(Container.DataItem, "NameEN") %>'></asp:Label>
                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:TemplateField>

<asp:BoundField DataField="AliasName" HeaderText="Alias Name">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectUnit" HeaderText="หน่วยกิต">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                
                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปี">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem,"IsPublic") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SubjectCode") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SubjectCode") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
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
