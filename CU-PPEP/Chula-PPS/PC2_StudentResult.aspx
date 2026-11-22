<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_StudentResult.aspx.vb" Inherits=".PC2_StudentResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <section class="content-header">
      <h1>ผลการคัดเลือกวิชาสอบ PC2</h1>     
    </section>
<section class="content"> 
    <asp:Panel ID="pnYear" runat="server">  
         
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">ค้นหา</h3>
             
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
 
 <div class="col-md-1">
          <div class="form-group">
            <label></label> 
              <br />
                 <asp:Button ID="cmdFind" runat="server" CssClass="buttonGreen" Width="100" Text="แสดง"></asp:Button>
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

              <h3 class="box-title">ผลการคัดเลือก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">      
  
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
                          </columns>
                          <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                          <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                          <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                              VerticalAlign="Middle" />
                          <EditRowStyle BackColor="#2461BF" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                             
            <asp:Label ID="lblNo" runat="server" CssClass="validateAlert" Text="ไม่พบข้อมูลการคัดเลือกของท่านในระบบ กรุณาติดต่อ จนท.ศูนย์ฝึกฯ" Width="100%"></asp:Label>              
     </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    </section>
</asp:Content>
