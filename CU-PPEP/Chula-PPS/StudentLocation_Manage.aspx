<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentLocation_Manage.aspx.vb" Inherits=".StudentLocation_Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <section class="content-header">
      <h1> จัดการแหล่งฝึกที่เลือก</h1>     
    </section>
<section class="content"> 
    <asp:Panel ID="pnYear" runat="server">  
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-Search"></i>

              <h3 class="box-title">เงื่อนไข
                </h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">   
                                  
<table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        เลือกปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                        </asp:DropDownList>                                                    </td>
                                                </tr>
                                                <tr>
                                                  <td align="left" class="texttopic">วิชา</td>
                                                  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                                                          Width="400px" CssClass="form-control select2">                                                      </asp:DropDownList>                                                    </td>
                                                </tr>
  </table>     

     </div>
            <div class="box-footer clearfix">
           
            </div>
          </div> 
 </asp:Panel> 
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">แหล่งฝึกที่เลือกตามลำดับความสำคัญ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">   
    
  
<asp:Panel ID="pnData" runat="server">
<table width="100%">
<tr>
    <td  align="left" valign="top"  class="MenuSt">แหล่งฝึกที่เลือกตามลำดับความสำคัญ</td>
</tr>
<tr>
          <td align="center" valign="top">
             
                      <asp:GridView ID="grdLocation" runat="server" AutoGenerateColumns="False" 
                          CellPadding="0" DataKeyNames="itemID" ForeColor="#333333" GridLines="None" 
                          Width="100%">
                          <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                          <columns>
                              <asp:BoundField HeaderText="อันดับ" DataField="DegreeNo">
                              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />
                              </asp:BoundField>
                              <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดที่" />
                              <asp:BoundField DataField="Name" HeaderText="วันที่" />
                              <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
                              <headerstyle HorizontalAlign="Center" />
                              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />
                              </asp:BoundField>
                              <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="ลำดับ">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="imgUp" runat="server" ImageUrl="images/arrow-up.png"  CommandArgument='<%# Container.DataItemIndex%>'  />
                                      <asp:ImageButton ID="imgDown" runat="server" ImageUrl="images/arrow-down.png"  CommandArgument='<%# Container.DataItemIndex%>' />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="ลบ">
                                  <itemtemplate>
                                      <asp:ImageButton ID="imgDel" runat="server" 
                                          CommandArgument='<%# Container.DataItemIndex%>' 
                                          ImageUrl="images/icon-delete.png" />
                                  </itemtemplate>
                                  <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                              </asp:TemplateField>
                          </columns>
                          <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                          <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                          <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                              VerticalAlign="Middle" />
                          <EditRowStyle BackColor="#2461BF" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                
           </td>
      </tr>
       <tr>
          <td align="center" valign="top">                
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ยังไม่พบแหล่งฝึกฯที่เลือกในรายวิชานี้" Width="100%"></asp:Label>          
              </td>
        </tr>
       <tr>
           <td align="center" valign="top">
               <asp:Label ID="lblFull" runat="server" CssClass="OptionPanels" Width="100%"></asp:Label>
           </td>
    </tr>
       <tr>
          <td align="center" valign="top">
              <asp:LinkButton ID="lnkSelect" runat="server" CssClass="buttonLink">เลือกแหล่งฝึก</asp:LinkButton>
           </td>
      </tr>
       

        </table>
 </asp:Panel> 
    <asp:Label ID="lblNone" runat="server" CssClass="validateAlert" Text="ไม่พบข้อมูลการลงทะเบียนของท่านในระบบ กรุณาติดต่อ จนท.ศูนย์ฝึกฯ" Width="100%"></asp:Label>              
     </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    </section>
</asp:Content>
