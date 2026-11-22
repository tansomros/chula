<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="OfficialSite.Master" CodeBehind="StudentPrintCert.aspx.vb" Inherits=".StudentPrintCert" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <section class="content-header">
      <h1>พิมพ์ใบรับรองการผ่านการฝึกปฏิบัติงาน</h1>   
    </section>

<section class="content">  

     <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">กำหนดค่าพารามิเตอร์</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
     <table>
            <tr>
                <td>ปีการศึกษา</td>
                <td width="100"><asp:TextBox ID="txtEduYear" runat="server" CssClass="text-center" ></asp:TextBox></td>
           
                <td>ลงวันที่ออกใบรับรอง</td>
                <td><asp:TextBox ID="txtCertDate" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
        </table>           
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">ค้นหานิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                 <table width="90%" border="0" align="left" cellpadding="0" cellspacing="2">
       <tr>
         <td>สาขาวิชา</td>
         <td>
            <asp:DropDownList ID="ddlMajor" runat="server" Width="300px" 
                AutoPostBack="True" CssClass="Objcontrol">            </asp:DropDownList>          </td>
         </tr>
       <tr>
         <td>สาขา</td>
         <td><asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
             <asp:DropDownList ID="ddlMinor" runat="server" AutoPostBack="True" 
                        Width="300px" CssClass="Objcontrol"> </asp:DropDownList>
           </ContentTemplate>
           <Triggers>
             <asp:AsyncPostBackTrigger ControlID="ddlMajor" 
                        EventName="SelectedIndexChanged" />
           </Triggers>
         </asp:UpdatePanel></td>
         </tr>
       <tr>
         <td>แขนง</td>
         <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
               <asp:DropDownList ID="ddlSubMinor" runat="server" Width="300px" 
                     CssClass="Objcontrol"> </asp:DropDownList>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddlMinor" 
                        EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel></td>
         </tr>
        <tr>
         <td width="80">อ.ที่ปรึกษา</td>
         <td><asp:DropDownList CssClass="Objcontrol" ID="ddlAdvisor" runat="server" 
                 Width="300px"> </asp:DropDownList>             </td>
         </tr>
          <tr>
         <td width="80">ชั้นปีที่</td>
         <td>
             <asp:DropDownList ID="ddlLevel" runat="server" CssClass="Objcontrol">
                 <asp:ListItem Selected="True" Value="0">--ไม่ระบุ--</asp:ListItem>
                 <asp:ListItem>1</asp:ListItem>
                 <asp:ListItem>2</asp:ListItem>
                 <asp:ListItem>3</asp:ListItem>
                 <asp:ListItem>4</asp:ListItem>
                 <asp:ListItem>5</asp:ListItem>
                 <asp:ListItem>6</asp:ListItem>
             </asp:DropDownList>
              </td>
         </tr>

        <tr>
         <td width="80">รหัส/ชื่อ</td>
         <td><asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox></td>
         </tr>
       <tr>
         <td align="center">&nbsp;</td>
         <td>
             <asp:Button ID="cmdSearch" runat="server" CssClass="buttonFind" Text="ค้นหา" Width="80px" />
           </td>
       </tr>
     </table>                                   
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เลือกนิสิต</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
                         <asp:GridView ID="grdData"  runat="server" CellPadding="0" 
                                                        GridLines="None" 
             AllowPaging="True" CssClass="txtcontent" 
                             Font-Bold="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="Student_ID">
                        <RowStyle BackColor="#F7F7F7" />
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" 
                                                            HorizontalAlign="Center" />                     
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต" >
                            <ItemStyle Width="90px" HorizontalAlign="Center" />                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="ชื่อนิสิต" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="นามสกุล" />
                            <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                            <asp:BoundField DataField="MinorName" HeaderText="สาขา" />
                            <asp:BoundField DataField="SubMinorName" HeaderText="แขนง" />
                            <asp:BoundField DataField="ClassName" HeaderText="ชั้นปีที่">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>                
</div>
            <div class="box-footer text-center">
           <asp:LinkButton ID="lnkPrintAll" runat="server" CssClass="buttonLink">พิมพ์จากผลการค้นหาทั้งหมด</asp:LinkButton>
 <asp:LinkButton ID="lnkPrintSelect" runat="server" CssClass="buttonLink">พิมพ์ที่เลือกทั้งหมด</asp:LinkButton>  
            </div>
          </div>
                       
    </section>  
</asp:Content>
