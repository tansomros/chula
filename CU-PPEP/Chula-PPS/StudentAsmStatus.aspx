<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentAsmStatus.aspx.vb" Inherits=".StudentAsmStatus" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>สถานะการประเมิน</h1>     
    </section>
<section class="content"> 
 


<table border="0" align="center"  >
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปีการศึกษา :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" Width="150"
                                                            CssClass="form-control select2" AutoPostBack="True">                                                        </asp:DropDownList>                                                    </td>
</tr>
  </table>  
    
    <div align="center">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
             <ProgressTemplate>
<img alt="" src="images/progress_bar.gif" height="25" />             </ProgressTemplate>
         </asp:UpdateProgress>  
</div>
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-tachometer"></i>

              <h3 class="box-title">สถานะการประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">   
                                <div class="table-responsive">
                  <table id="tbdata" class="table table-bordered table-striped">
                <thead>
                <tr> 
                    <th>No.</th>
                    <th>รหัสวิชา</th>                 
                    <th>ชื่อรายวิชา</th>                     
                    <th>แหล่งฝึก</th>
                    <th>จังหวัด</th> 
                    <th>ผลัดฝึก</th> 
                    <th>Advisor</th> 
                    <th>Proceptor</th> 
                </tr>
                </thead>
                <tbody>
            <% For Each row As DataRow In dtAsm.Rows %>
                <tr>              
                  <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                  <td><% =String.Concat(row("SubjectCode")) %>    </td>                 
                  <td><% =String.Concat(row("SubjectName")) %></td>    
                  <td><% =String.Concat(row("LocationName")) %></td> 
                  <td><% =String.Concat(row("ProvinceName")) %></td> 
                  <td><% =String.Concat(row("PhaseName")) %></td>     
                  <td class="text-center"><% =IIf(String.Concat(row("AStatus")) = "Y", "<img src='images/icon-ok.png'>", "") %> </td>
                  <td class="text-center"><% =IIf(String.Concat(row("PStatus")) = "Y", "<img src='images/icon-ok.png'>", "") %> </td>
                </tr>
            <%  Next %>
                </tbody>               
              </table>  
                </div>  

              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:BoundField HeaderText="No.">
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" />                      </asp:BoundField>
            <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />                      </asp:BoundField>
                <asp:TemplateField HeaderText="ชื่อรายวิชา">
                    <ItemTemplate>                       
                                    <asp:Label ID="lblTH" runat="server" CssClass="NameTH" height="15"></asp:Label>
                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึกที่ได้">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField DataField="TimePhaseName" HeaderText="ผลัดฝึก" />
                <asp:BoundField DataField="TimePhaseDesc" HeaderText="วันที่ฝึกปฏิบัติ" >
                </asp:BoundField>
            <asp:TemplateField HeaderText="รายละเอียด">
                <ItemTemplate>
                    <asp:HyperLink ID="hlnkView" runat="server" ImageUrl="images/view.png" Target="_blank"><i class="fa fa-info-circle text-primary"></i>ข้อมูลแหล่งฝึก</asp:HyperLink>
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" />          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" Visible="False">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SubjectCode") %>' />                        </itemtemplate>
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
           <asp:Label ID="lblResult" runat="server" CssClass="OptionPanels"  Text="ไม่พบข้อมูลการฝึกของท่าน" Width="100%"></asp:Label>    
            </div>
          </div>
    </section>
</asp:Content>
