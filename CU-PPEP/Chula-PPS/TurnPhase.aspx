<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TurnPhase.aspx.vb" Inherits=".TurnPhase" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<section class="content-header">
      <h1>กำหนดผลัดฝึก</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-plus-circle"></i>

              <h3 class="box-title">เพิ่ม/แก้ไข ผลัดฝึก</h3>
             
                 <div class="box-tools pull-right"><asp:Label ID="lblID" runat="server"></asp:Label>
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                          
<table cellSpacing="1" cellPadding="1" border="0">
 <tr>
                                                    <td align="left" width="80">
                                                        ปีการศึกษา</td>
                                                    <td align="left" width="100"> 
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                      </td>
                                                    <td align="left" width="60"> 
                                                        ผลัดที่ 
                                                        </td>
                                                    <td align="left"> 
                                                        <asp:TextBox ID="txtPhaseNo" runat="server" 
                                                         Width="50px"></asp:TextBox>                                                                                                        
                                                      </td>
                                                    <td align="left"> 
                                                        (ป้อนเฉพาะตัวเลข)</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        ตั้งแต่วันที่</td>
                                                    <td align="left" ><asp:TextBox ID="txtStartDate" runat="server" 
                                                         Width="120px"></asp:TextBox>
                                                      
                                                    </td>
                                                    <td align="left" >ถึงวันที่</td>
                                                    <td align="left" >  <asp:TextBox ID="txtEndDate" runat="server" 
                                                         Width="120px"></asp:TextBox>
                                                        
                                                      
                                                    </td>
                                                    <td align="left" >&nbsp;(วว/ดด/ปปปป)&nbsp;</td>
                                                </tr>
                                                 

                                                <tr>
                                                    <td align="left">
                                                        จำนวนวัน</td>
                                                    <td align="left" class="texttopic">
                                                        <asp:TextBox ID="txtDayCount" runat="server" 
                                                         Width="50px"></asp:TextBox></td>
                                                    <td align="left">
                                                        Status&nbsp;                                                        </td>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkStatus" runat="server" 
                                                            Text="Active" Checked="True" /></td>
                                                    <td align="left">
                                                        &nbsp;</td>
                                                </tr>                                                

                                                <tr>
                                                    <td align="left">
                                                        Remark</td>
                                                    <td align="left" class="texttopic" colspan="4">
                                                      <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                 

                                                <tr>
                                                    <td align="left" class="texttopic">
                                                        &nbsp;</td>
                                                    <td align="left" class="texttopic" colspan="4">
													    <span >
                                          <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
                                        </span>                                                    </td>
                                                </tr>
                                                 

                                                </table>        
  
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div> 

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">รายการผลัดฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
           <table>
                  <tr>
                      <td>ค้นหาตามปีการศึกษา </td>
                      <td width="150"> 
                                                        <asp:DropDownList ID="ddlYearFind" runat="server" AutoPostBack="True" CssClass="form-control select2">
                                                        </asp:DropDownList>
                                                      </td>
                  </tr>
              </table>
                  <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-hover">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="EduYear" HeaderText="ปี">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                </asp:BoundField>
            <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดฝึก">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="วันที่ฝึก">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Descriptions" HeaderText="Description">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
              <itemtemplate>
                <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" 
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "StatusFlag") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PhaseID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PhaseID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
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

</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
                       
    </section> 
    
</asp:Content>
