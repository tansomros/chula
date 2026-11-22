<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationSubject.aspx.vb" Inherits=".EvaluationSubject" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">       
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="content-header">
      <h1>ผูกแบบประเมินกับรายวิชา</h1>   
    </section>

<section class="content">  
    
      <div class="box box-solid">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>
              <h3 class="box-title">เลือกปีการศึกษา</h3>                                             
            </div>
            <div class="box-body">
                  <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="form-control select2" AutoPostBack="True">
                  </asp:DropDownList>
            </div>          
          </div>

    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">เลือกรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
         <table border="0" cellPadding="1" cellSpacing="1" width="100%">          
       <tr>
          <td  valign="top" >
         <table border="0" cellspacing="2" cellpadding="0">
             <tr>
              <td>ค้นหา</td>
              <td>
                  <asp:TextBox ID="txtSearchSubj" runat="server" Width="150px"></asp:TextBox>
                  </td>
              <td><asp:Button ID="cmdFindStd" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>           </td>
            </tr>
           
          </table>
           </td>
      </tr>
       <tr>
          <td valign="top" >
              <table style="width: 100%;">
                  <tr>
                      <td style="width:50%;">
              <asp:GridView ID="grdSubject" runat="server" AutoGenerateColumns="False" CellPadding="0" DataKeyNames="SubjectID" ForeColor="#333333" GridLines="None" Width="100%">
                  <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                  <columns>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:CheckBox ID="chkSubj" runat="server" />
                          </ItemTemplate>
                          <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
                      <headerstyle HorizontalAlign="Left" />
                      <itemstyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="SubjectName" HeaderText="ชื่อวิชา">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                      </asp:BoundField>
                      <asp:BoundField DataField="AliasName" HeaderText="ชื่อย่อ">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                      </asp:BoundField>
                  </columns>
                  <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                  <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <headerstyle CssClass="th" Font-Bold="True" VerticalAlign="Middle" />
                  <EditRowStyle BackColor="#2461BF" />
                  <AlternatingRowStyle BackColor="White" />
              </asp:GridView>
</td>
                      <td>
              <asp:GridView ID="grdSubject2" runat="server" AutoGenerateColumns="False" CellPadding="0" DataKeyNames="SubjectID" ForeColor="#333333" GridLines="None" Width="100%">
                  <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                  <columns>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:CheckBox ID="chkSubj2" runat="server" />
                          </ItemTemplate>
                          <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="SubjectCode" HeaderText="รหัส">
                      <headerstyle HorizontalAlign="Left" />
                      <itemstyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="SubjectName" HeaderText="ชื่อวิชา">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                      </asp:BoundField>
                      <asp:BoundField DataField="AliasName" HeaderText="ชื่อย่อ">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                      </asp:BoundField>
                  </columns>
                  <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                  <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <headerstyle CssClass="th" Font-Bold="True" VerticalAlign="Middle" />
                  <EditRowStyle BackColor="#2461BF" />
                  <AlternatingRowStyle BackColor="White" />
              </asp:GridView>
     </td>
                  </tr>
               
              </table>     

          </td>
      </tr>
      
       <tr>
           <td align="center" valign="top">
               <asp:Label ID="lblNo" runat="server" CssClass="OptionPanels" Text="ท่านทำการผูกรายวิชากับแบบประเมินครบเรียบร้อยแล้ว" Width="100%"></asp:Label>
           </td>
    </tr></table>
                
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เลือกแบบประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages">         
  <table border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="50" >ค้นหค้นหา</td>
              <td ><asp:TextBox ID="txtSearchEva" runat="server" Width="150px"></asp:TextBox>              </td>
              <td >  <asp:Button ID="cmdFind" runat="server" CssClass="btn btn-warning" Width="70" Text="ค้นหา"></asp:Button>             </td>
            </tr>
            </table>     
<br />
 <asp:GridView ID="grdEvaluation" runat="server" CellPadding="0" ForeColor="#333333"   GridLines="None"  AutoGenerateColumns="False" Width="100%"  DataKeyNames="UID">
            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
            <columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkEva" runat="server" />                </ItemTemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />          
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="ชื่อแบบประเมิน">
              <headerstyle HorizontalAlign="Left" />          
              <itemstyle HorizontalAlign="Left" />                      </asp:BoundField>
                <asp:BoundField DataField="FullScore" HeaderText="คะแนนเต็ม">
                <headerstyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px"/>
                </asp:BoundField>

                <asp:BoundField DataField="NoScoreTXT" HeaderText="หมายเหตุ">
                <ItemStyle HorizontalAlign="Center" />
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
<br />
              <asp:Label ID="lblNotic" runat="server" CssClass="OptionPanels" Text="กรุณาเลือกเงื่อนไขเพื่อค้นหาข้อมูลก่อน" Width="99%"></asp:Label>
          
 
 </div>
            <div class="box-footer text-center">
            <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-pink" Width="100" Text="บันทึก"></asp:Button>
    <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-default" Width="100" Text="ยกเลิก"></asp:Button>
            </div>
          </div>
    
     <div class="box box-info">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">Recommendation Tick</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body mailbox-messages"> 
                ท่านสามารถทำการ Copy จากต้นแบบรายปีได้โดย <a href="EvaluationSubjectCopy.aspx?ActionType=eva&ItemType=evas" class="buttonLink">คลิกที่นี่ </a> เพื่อไปหน้า Copy การผูกแบบประเมินกับรายวิชา ระบบจะทำการ Copy ให้เฉพาะรายวิชาที่ยังไม่ได้ทำการกำหนดค่าในปีการศึกษาที่เลือก
                 </div>
            <div class="box-footer text-center">
            </div>
          </div>
    </section>   
</asp:Content>
