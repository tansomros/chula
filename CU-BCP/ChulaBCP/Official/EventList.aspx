<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="EventList.aspx.vb" Inherits="EventList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>Event
                                    <div class="page-title-subheading">จัดการข้อมูลปฏิทินกิจกรรม</div>
                                </div>
                            </div>
                        </div>
                    </div>

<section class="content">
                   
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">ค้นหา</h2>   
          <div class="box-tools pull-right"><asp:Button ID="cmdNew" runat="server" CssClass="btn btn-primary" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>                            
        </div>
        <div class="box-body">

              <div class="row">
                  

           <div class="col-md-3">
          <div class="form-group">
            <label>Start Date</label>
               <asp:TextBox ID="txtStartDate" runat="server"   CssClass="form-control"></asp:TextBox>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group">
                    <label>End Date</label>
              <asp:TextBox ID="txtEndDate" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    
                   <div class="col-md-6">
          <div class="form-group">
            <label>Category</label>
              <asp:DropDownList ID="ddlCategory" runat="server"  CssClass="form-control select2">
                </asp:DropDownList> 
          </div>
        </div>                  
                 
</div>  
            


             <div class="row">
                   <div class="col-md-12">
          <div class="form-group">
            <label>Event Name</label>
              <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control"></asp:TextBox>            
          </div>
        </div>
                 </div>    
          <div class="row">
                   <div class="col-md-3">
          <div class="form-group">            
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  runat="server" ControlToValidate="txtStartDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>                           
          </div>
        </div>
                   <div class="col-md-3">
          <div class="form-group">          
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  runat="server" ControlToValidate="txtEndDate" CssClass="text-red" ErrorMessage="รูปแบบวันที่ไม่ถูกต้อง (วว/ดด/ปปปป)" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator>              
          </div>
        </div>

                  </div>    

    </div> 
        <div class="box-footer text-center">
       <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-warning" Text="ค้นหา" Width="70px" />  
        </div> 
      </div>  
                     
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการกิจกรรม</h2>   
          <div class="box-tools pull-right"><asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-primary" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>        
        </div>
        <div class="box-body">
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#34495E" GridLines="Horizontal" Width="100%" AllowPaging="True" BorderStyle="none"  class="table table-borderless">
                        <RowStyle BackColor="#FFFFFF" VerticalAlign="Top"  />
                        <columns>
                            <asp:TemplateField HeaderText="รายละเอียดกิจกรรม">
                                <ItemTemplate>
                                    <table style="width: 100%;"  >
                                        <tr>
                                            <td>
                                      <table style="width: 100%;">
                                        <tr>
                                            <td class="gridtopic" style="width: 40px;">วันที่:</td>
                                            <td class="gridtext3"  style="width:300px;text-align: left;">
                                                <asp:Label ID="Label1"  runat="server" Text='<%# DisplayRangeDateTH(DataBinder.Eval(Container.DataItem, "StartDate"), DataBinder.Eval(Container.DataItem, "EndDate"))  %>'></asp:Label></td>
                                            <td class="gridtopic"  style="width: 55px;">Category:</td>
                                            <td class="gridtext3" style="width: 30%;text-align: left;">
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryNameTH") %>'></asp:Label></td>
                                        
                                        </tr>  
                                    </table>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server"  CssClass="gridtext1" Text='<%# DataBinder.Eval(Container.DataItem, "EventName") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </td>
                                        </tr>                          
                                    </table>
                                    
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="แก้ไข">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-edit.png" CssClass="gridbutton" />
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="ลบ">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' ImageUrl="images/icon-delete.png" CssClass="gridbutton" />
                                </itemtemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <pagerstyle ForeColor="White" HorizontalAlign="Center" CssClass="dc_pagination" />
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
