<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_StudentSelection.aspx.vb" Inherits=".PC2_StudentSelection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <section class="content-header">
      <h1>ลงทะเบียนวิชาสอบ</h1>     
    </section>
<section class="content"> 
    <asp:Panel ID="pnYear" runat="server">  
         
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">ลงทะเบียนเลือกวิชาที่ต้องการ</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 

                  <div class="row">

          <div class="col-md-2">
          <div class="form-group">
            <label>ปีการศึกษา</label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList>
          </div>
        </div>
          <div class="col-md-8">
          <div class="form-group">
            <label>วิชา</label> 
 <asp:DropDownList ID="ddlSubject" runat="server"  CssClass="form-control select2"></asp:DropDownList>
          </div>

        </div>
 
 <div class="col-md-1">
          <div class="form-group">
            <label></label> 
              <br />
                 <asp:Button ID="cmdAdd" runat="server" CssClass="buttonGreen" Width="100" Text="บันทึก"></asp:Button>
          </div>

        </div>

   </div>
              
</div>
            <div class="box-footer clearfix">           
              <asp:Label ID="lblValidate" runat="server" Visible="False" CssClass="validateAlert" Width="99%"></asp:Label>
           
            </div>
          </div>

 </asp:Panel> 
      <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-hospital-o"></i>

              <h3 class="box-title">วิชาที่เลือกตามลำดับความสำคัญ</h3>
             
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
          <td align="center" valign="top">
             
                      <asp:GridView ID="grdSubject" runat="server" AutoGenerateColumns="False" 
                          CellPadding="0" DataKeyNames="UID" ForeColor="#333333" GridLines="None" 
                          Width="100%" CssClass="table table-hover">
                          <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                          <columns>
                              <asp:TemplateField HeaderText="อันดับ">
                                  <ItemTemplate>
                                       <asp:Image ID="imgDegree" runat="server" ImageUrl='<%# "images/" & DataBinder.Eval(Container.DataItem, "DegreeNo") & ".png" %>' />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="NameTH" HeaderText="วิชา">
                              <headerstyle HorizontalAlign="Left" />
                              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="ลำดับ">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="imgUp" runat="server" ImageUrl="images/arrow-up.png"  CommandArgument='<%# Container.DataItemIndex%>'  />
                                      <asp:ImageButton ID="imgDown" runat="server" ImageUrl="images/arrow-down.png"  CommandArgument='<%# Container.DataItemIndex%>' />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="ลบ">
                                  <itemtemplate>
                                      <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# Container.DataItemIndex%>' ImageUrl="images/icon-delete.png" />
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
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ยังไม่พบวิชาฯที่เลือกในรายวิชานี้" Width="100%"></asp:Label>          
              </td>
        </tr>
       <tr>
           <td align="center" valign="top">
               <asp:Label ID="lblFull" runat="server" CssClass="OptionPanels" Width="100%"></asp:Label>
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
