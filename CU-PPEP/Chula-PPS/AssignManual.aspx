<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssignManual.aspx.vb" Inherits=".AssignManual" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
      <section class="content-header">
      <h1>กำหนดแหล่งฝึกให้นิสิต (Manual)</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกเงื่อนไข</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                    <div class="col-md-1">
          <div class="form-group">
            <label>ปีการศึกษา</label>
     <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"  CssClass="form-control select2"> </asp:DropDownList> 
          </div>

        </div>

                  <div class="col-md-11">
          <div class="form-group">
            <label>รายวิชา</label> 
 <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList>  
          </div>

        </div>
            </div>
             <div class="box-footer text-blue text-left clearfix">
           Remark : เป็นการจัดสรรแหล่งฝึกให้นิสิต แบบลัด manual โดยจำเป็นต้องผ่านขั้นตอนการเลือกแหล่งฝึกของนิสิตก่อน ถึงจะมีรายชื่อนิสิตและแหล่งฝึกให้จัดสรร
            </div>
          </div>

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ต้องฝึกในรายวิชานี้แต่ยังไม่ได้แหล่งฝึกทั้งหมด&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
 <table border="0" cellspacing="2" cellpadding="0">
            <tr>
              <td>ค้นหา</td>
              <td >
                  <asp:TextBox ID="txtSearchStd" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server"  text="ค้นหา" CssClass="btn btn-warning" Width="100px" />  </td>
            </tr>
     
      
          </table>
              <asp:GridView ID="grdStudent" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="Student_Code">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStd" runat="server"/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
            <asp:BoundField DataField="Student_Code" HeaderText="รหัสนิสิต">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" Width="90px" />                      </asp:BoundField>
            <asp:BoundField HeaderText="ชื่อ" DataField="FirstName">

                <HeaderStyle HorizontalAlign="Left" />

              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="นามสกุล" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />                </asp:BoundField>
                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปีที่">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
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
              <asp:Label ID="lblNo" runat="server" CssClass="validateAlert"       
                  Text="ไม่พบนิสิตที่ยังไม่ได้แหล่งฝึก หรือ นิสิตได้แหล่งฝึกครบเรียบร้อยแล้ว"></asp:Label>          
                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

    
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-filter"></i>

              <h3 class="box-title">เลือกแหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
          <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
            <div class="box-body"> 
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
         <tr>
          <td valign="top"> 
              
              
              <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหา</td>
              <td ><asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>              </td>
              <td ><asp:Button ID="cmdFind" runat="server" text="ค้นหา" CssClass="btn btn-warning" Width="100px"  />              </td>
            </tr>
            </table>         </td>
        </tr>


        <tr>
          <td align="center" valign="top">
              <asp:GridView ID="grdData" 
                             runat="server" CellPadding="0" ForeColor="#333333" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" 
                  DataKeyNames="LocationID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField>
                <ItemTemplate>
                      <input  type="radio" name="optSelect"
            onclick="document.getElementById('<%=HiddenField1.ClientID%>').value=<%#Container.DataItemIndex%>;" value="" />
                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
                <asp:BoundField DataField="PhaseName" HeaderText="ผลัดฝึก">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
            <asp:BoundField DataField="LocationName" HeaderText="ชื่อแหล่งฝึก">
              <headerstyle HorizontalAlign="Center" />          
              <itemstyle HorizontalAlign="Left" VerticalAlign="Middle" />                      </asp:BoundField>
                <asp:BoundField HeaderText="ประเภท" DataField="LocationGroupName">                
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProvinceName" HeaderText="จังหวัด" />
                <asp:BoundField HeaderText="ชาย" DataField="Man">
                <ItemStyle HorizontalAlign="Center" />                </asp:BoundField> 
                <asp:BoundField DataField="Women" HeaderText="หญิง" />
                <asp:BoundField DataField="NoSpec" HeaderText="ไม่ระบุ" />
                <asp:BoundField HeaderText="ได้แล้ว" />
                <asp:BoundField HeaderText="เหลือ">
                <ItemStyle HorizontalAlign="Center" />                </asp:BoundField>
                <asp:BoundField DataField="TimePhaseID" HeaderText="Ref." />
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
        <tr>
          <td align="center" valign="top">
        <asp:Button ID="cmdSave" runat="server" text="Save" CssClass="btn btn-pink" Width="100px" />
        <asp:Button ID="cmdClear" runat="server" text="Cancel" CssClass="btn btn-pink" Width="100px" />            </td>
        </tr>
       
    </table>
                        
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>

     

     
   </section>
    
</asp:Content>
