<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentSubject.aspx.vb" Inherits=".AssessmentSubject" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="content-header">
      <h1>รายการแบบประเมิน</h1>   
    </section>

<section class="content">   
  <asp:Panel ID="pnEdit" runat="server">
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-link"></i>

              <h3 class="box-title">กำหนดแบบประเมินในรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">  
    <table cellSpacing="1" cellPadding="1" border="0">
                                                <tr>
                                                    <td align="left" >
                                                        ปีการศึกษา :</td>
                                                    <td align="left" > 
                                                        <asp:Label ID="lblEduYear" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">รหัสวิชา :<asp:Label ID="lblSubjectID" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblSubjectCode" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" >
                                                        รายวิชา:</td>
                                                    <td align="left" >
                                                        <asp:Label ID="lblSubjectName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top" >แบบประเมิน :</td>
                                                    <td align="left" class="mailbox-messages" >
                                                        <asp:CheckBoxList ID="chkEva" runat="server"> </asp:CheckBoxList>                                            </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" vAlign="top" >
                                                       
                                           <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
                                                                                          </td>
                                                </tr>
    </table> 
                                                
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>
</asp:Panel>
    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-edit"></i>

              <h3 class="box-title">แบบประเมิน</h3>
             
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
              <td>ปี</td>
                 <td >
                     <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" Width="100" AutoPostBack="True">
                     </asp:DropDownList>
                </td>
              <td width="50" align="center" >ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>                </td>
              <td >
                  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>                
                <asp:Button ID="cmdAll" runat="server" CssClass="btn btn-warning" Width="70" Text="ดูทั้งหมด"></asp:Button>               </td>
            </tr>
            </table>


          </td>
      </tr>

        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" AllowPaging="True" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" PageSize="20" AutoGenerateColumns="False" Width="100%">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No." DataField="nRow">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />                      </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัสวิชา">
                <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="SubjectName" HeaderText="รายวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="GroupName" HeaderText="แบบประเมิน">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FullScore" HeaderText="คะแนน" />
                <asp:BoundField DataField="NoScoreTXT" />
            <asp:TemplateField HeaderText="Edit">
              <itemtemplate>
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>'  />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" CssClass="dc_pagination dc_paginationC dc_paginationC11" />             
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
