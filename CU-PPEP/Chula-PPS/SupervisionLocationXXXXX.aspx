<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SupervisionLocationXXXXX.aspx.vb" Inherits=".SupervisionLocationXXXXX" %>


<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="Page_Header">
        จัดการแหล่งนิเทศ</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td align="center" valign="top">&nbsp;</td>
      </tr>
       
        <tr>
            <td align="left" valign="top">
             
            
<table border="0" align="center" cellPadding="0" cellSpacing="2">
<tr>
                                                    <td align="left" class="texttopic">
                                                        ปี :                                                        </td>
                                                    <td align="left" class="texttopic">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                                            CssClass="Objcontrol">                                                        </asp:DropDownList>                                                    </td>
                                                    <td align="left" class="texttopic">&nbsp;</td>
</tr>
<tr>
  <td align="left" class="texttopic">รายวิชา :</td>
  <td align="left" class="texttopic">
                                                      <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                                                          Width="400px" CssClass="Objcontrol">                                                      </asp:DropDownList>                                                    </td>
  <td align="left">&nbsp;</td>
</tr>
  </table>      </td>
      </tr>
       <tr>
          <td align="left" valign="top">&nbsp;</td>
        </tr>
         <tr>
          <td align="left" valign="top"><table width="80%" border="0" cellspacing="2" cellpadding="0">
            <tr>
              <td width="100">แหล่งฝึก</td>
              <td>
                    <dx:ASPxComboBox ID="cboLocation" runat="server" EnableTheming="True" Theme="MetropolisBlue" Font-Names="Segoe UI" Font-Size="11pt" CssClass="Objcontrol" Width="500px">
                <Columns>
                    <dx:ListBoxColumn Caption="แหล่งฝึก" FieldName="LocationName" Width="300px" />
                    <dx:ListBoxColumn Caption="จังหวัด" FieldName="ProvinceName" Width="100px" />
                </Columns>
                        <ItemStyle Font-Names="Segoe UI" Font-Size="11pt" />
                        <CaptionStyle Font-Bold="True" Font-Names="Segoe UI" Font-Size="9.75pt">
                        </CaptionStyle>
            </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
              <td valign="top">ผลัดฝึก</td>
              <td>
                  <dx:ASPxRadioButtonList ID="optTimePhase" runat="server" Theme="MetropolisBlue" ValueType="System.String" Font-Names="Segoe UI" Font-Size="11pt" Visible="False">
                      <Paddings PaddingLeft="0px" />
                      <Border BorderStyle="None" />
                  </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
              <td>สถานะ</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td>Remark</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td><asp:ImageButton ID="cmdSave" runat="server" 
            ImageUrl="images/btnsave.png" />
              <asp:ImageButton ID="cmdClear" runat="server" 
            ImageUrl="images/btnclear.png" /></td>
            </tr>
          </table></td>
        </tr>
       <tr>
           <td  align="left" valign="top">&nbsp;</td>
      </tr>
       <tr>
          <td  align="left" valign="top"  class="MenuSt">
              
              <table border="0" cellspacing="2" cellpadding="0">
          <tr>
              <td colspan="5">รายชื่อแหล่งฝึกที่เลือกแล้วทั้งหมด&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;แหล่ง</td>
            </tr>   <tr>
              <td>จังหวัด</td>
              <td><asp:DropDownList CssClass="Objcontrol" ID="ddlProvince" runat="server"> </asp:DropDownList></td>
              <td>ค้นหา
             </td>
              <td> <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>&nbsp;</td>
              <td><asp:ImageButton ID="cmdFindStd" runat="server" 
                      ImageUrl="images/btnSearch.png" /> </td>
            </tr>
            </table>
           </td>
      </tr>
       <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:BoundField DataField="TimePhaseID" HeaderText="ผลัดที่" />
            <asp:BoundField HeaderText="ชื่อแหล่งฝึก" DataField="LocationName">

                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                </asp:BoundField>
            <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด">                      
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LocationGroupName" HeaderText="ประเภท" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:TemplateField HeaderText="ลบ">
              <itemtemplate>
                <asp:ImageButton ID="imgDel" runat="server" 
                                    ImageUrl="images/icon-delete.png" 
                                    
                      
                      CommandArgument='<%#  Container.DataItemIndex  %>' />                        </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>
              <asp:Label ID="lblNo" runat="server" CssClass="text_red" 
                  
                  
                  Text="ยังไม่พบแหล่งฝึกที่เลือก &lt;br/&gt;  เลือกแหล่งฝึกที่ต้องการเปิดด้านล่าง"></asp:Label>           </td>
      </tr>
      
      <tr>
        <td>&nbsp;</td>	
      </tr>
        <tr>
          <td  align="left" valign="top"  class="Section_Header">เลือกแหล่งฝึกและผลัดฝึก</td>
      </tr>
              <tr>
          <td valign="top"  class="skin_Search"> <table border="0" cellspacing="1" cellpadding="0">
           <tr>
              <td >&nbsp;</td>
              <td >&nbsp;</td>
              <td >&nbsp;</td>
            </tr>  <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td ><asp:ImageButton ID="cmdFind" runat="server" ImageUrl="images/btnSearch.png" />              </td>
            </tr>
           
            </table>         </td>
        </tr>


        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                  DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField HeaderText="เลือก">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />          
            </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="ผลัดฝึกที่">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="ชื่อแหล่งฝึก">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField HeaderText="จำนวนนิสิต" DataField="STDCOUNT">                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" 
                      VerticalAlign="Middle" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView></td>
      </tr>
      
       
    </table>
    
</asp:Content>
