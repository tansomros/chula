<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_StudentRegister.aspx.vb" Inherits=".PC2_StudentRegister" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">       
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

<section class="content-header">
      <h1>รายชื่อนิสิตที่ยังไม่ลงทะเบียนเลือกวิชาสอบ</h1>   
    </section>

<section class="content">  

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

          <div class="col-md-1">
          <div class="form-group">
            <label>ปีการศึกษา</label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList>
          </div>

        </div>
         
</div>
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
    
 
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search-plus"></i>

              <h3 class="box-title">รายชื่อนิสิตที่ยังไม่ลงทะเบียนเลือกวิชาสอบ&nbsp; 
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           &nbsp;คน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>     
            </div>
            <div class="box-body"> 



 <asp:GridView ID="grdStudent"  runat="server" CellPadding="0" GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20">
            <RowStyle BackColor="#F7F7F7" />
            <columns>
            <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต">                      
                <HeaderStyle HorizontalAlign="Center" />
              <itemstyle HorizontalAlign="Center" Width="120px"/>

            </asp:BoundField>

                <asp:BoundField DataField="StudentName" HeaderText="ชื่อนิสิต">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </asp:BoundField>
                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />                </asp:BoundField>

                <asp:BoundField DataField="LevelClass" HeaderText="ชั้นปีที่">
                     <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />              
                </asp:BoundField>
            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" HorizontalAlign="Left" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
          </asp:GridView>

<asp:Label ID="lblNo" runat="server" CssClass="alert-success text-center" Text="ไม่พบข้อมูล" Width="100%" Font-Size="12pt" Height="30px"></asp:Label>
   
</div> 
        <div class="box-footer clearfix">           
                
            </div>
          </div>
    
    </section>

    
</asp:Content>
