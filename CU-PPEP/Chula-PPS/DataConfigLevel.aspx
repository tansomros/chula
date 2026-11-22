<%@ Page Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site.Master" CodeBehind="DataConfigLevel.aspx.vb" Inherits=".DataConfigLevel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"><title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Page_Header">
กำหนดระดับชั้นให้นิสิต
</div>

 <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
          <tr>
            <td height="16" background="images/block_02.gif"></td>
          </tr>
          <tr>
            <td bgcolor="#FFFFFF">
            
            
            
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
<TBODY>
					<TR>
						<TD align="left">
						  <TABLE id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%" border="0">
			     <TR>
				     <TD height="21" valign="top" class="MenuSt">กำหนดทั้งหมดแบบครั้งเดียว</TD>
				     </TR>
                 <TR>
				     <TD height="21" valign="top"><table border="0" cellspacing="2" cellpadding="0">
                       <tr>
                         <td>กำหนดระดับชั้นให้กับนิสิตแบบเลื่อนชั้นไป 1 ระดับชั้นทุกคน โดยทำการคลิกปุ่ม &quot;เลื่อนระดับชั้น&quot; ด้านล่าง</td>
                       </tr>
                       <tr>
                         <td><strong><u>ข้อควรระวัง</u></strong> ควรกดปุ่มเลื่อนระดับชั้น ปีละครั้งเท่านั้น และควรตรวจสอบความถูกต้องจากข้อมูลในตารางข้างล่าง</td>
                       </tr>
                       <tr>
                         <td><asp:LinkButton ID="lnkSubmitAll" runat="server"  CssClass="buttonModal">&nbsp;&nbsp;&nbsp;เลื่อนระดับชั้น&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                           </td>
                       </tr>
                     </table></TD>
				     </TR>
                      <TR>
				     <TD height="21" valign="top" class="MenuSt">กำหนดทีละรหัส</TD>
				     </TR>
                      <TR>
									<TD height="21" align="center" valign="top"><table cellSpacing="1" cellPadding="1" border="0">
											<tr>
												<td align="left">รหัสขึ้นต้น</td>
						              
												<td align="left">
                                                    <asp:TextBox ID="txtCode" runat="server" 
                                                         Width="60px"></asp:TextBox></td>
						            
												<td align="left">ชั้นปีที่</td>
						              
												<td align="left">
                                                    <asp:TextBox ID="txtValues" runat="server" Width="60px" 
                                                        ></asp:TextBox>                                                </td>
						              
												<td align="left">
                                                    หรือ</td>
						              
												<td align="left" class="mailbox-messages">
                                                    <asp:CheckBox ID="chkPass" runat="server" Text="สำเร็จการศึกษา" />
                                                </td>
						                        <td align="left"><asp:Button ID="cmdSave" runat="server"  Text="บันทึก" CssClass="btn-primary" Width="100px" ></asp:button></td>
									  </tr>


					</table></TD>
</TR>
				<TR>
				     <TD height="21" valign="top" class="MenuSt">ข้อมูลรหัสนิสิตปัจจุบัน</TD>
				     </TR>
                       <TR>
				     <TD valign="top">
                         <asp:GridView ID="grdData" 
                             runat="server" CellPadding="4" ForeColor="#333333" 
                                                        GridLines="None" CssClass="table table-hover" 
                      AutoGenerateColumns="False" Width="100%">
                        <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                        <columns>
                        <asp:BoundField HeaderText="รหัส" DataField="stdCode" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />                            </asp:BoundField>
                            <asp:BoundField DataField="LevelClass" 
                                HeaderText="ชั้นปีที่" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />                            </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# Container.DataItemIndex %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" HorizontalAlign="Center" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>  </TD>
				     </TR>
				  <TR>
				     <TD height="21" valign="top">&nbsp;</TD>
				     </TR>
								</TABLE>					  </TD>
		  </TR>
				</TBODY>
			</TABLE>            </td>
          </tr>
         
        </table>                  
</asp:Content>
