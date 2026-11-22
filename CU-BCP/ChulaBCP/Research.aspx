<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Research.aspx.vb" Inherits=".Research" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<section class="containerPage">  
 <section class="content-header">
      <h1> <asp:Label ID="lblHeader" runat="server" Text="ฐานข้อมูลงานวิจัย"></asp:Label>
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">ฐานข้อมูลงานวิจัย</li> <li class="active"> 
      </ol>
    </section> 
  <div class="row">
      
  <div class="col-md-10">

      <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title">ค้นหางานวิจัย</h2>
             <div class="box-tools pull-right"><asp:Button ID="cmdNew" runat="server" CssClass="btn-default" Text="เพิ่มข้อมูล" Width="100px" />          
          </div>
        </div>
        <div class="box-body"> 

              <div class="row">
           <div class="col-md-4">
          <div class="form-group">
            <label>ชื่อเรื่อง</label>
              <asp:TextBox ID="txtTitle" runat="server" placeholder="Title" CssClass="form-control"></asp:TextBox>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group">
                    <label>ผู้แต่ง</label>
              <asp:TextBox ID="txtAuthor" runat="server" placeholder="Author" CssClass="form-control"></asp:TextBox>
          </div>
        </div>    
                     <div class="col-md-1">
          <div class="form-group">
            <label>ปี</label>
              <asp:TextBox ID="txtYear" runat="server" placeholder="Year" CssClass="form-control"></asp:TextBox>
          </div>
        </div>
                    <div class="col-md-4">
          <div class="form-group">
            <label>คำค้นหา</label>
              <asp:TextBox ID="txtSearch" runat="server" placeholder="Keyword" CssClass="form-control"></asp:TextBox>
          </div>
        </div>
      </div>
 <div class="row text-center">
     <br />
     <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="100px" />
     </div>
                      

        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->
        <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title">ผลการค้นหา</h2>

        </div>
        <div class="box-body"> 
            ค้นพบจำนวน 
            <asp:Label ID="lblCount" runat="server"></asp:Label>
&nbsp;รายการ<br />
             <asp:GridView ID="grdData" runat="server"  AutoGenerateColumns="False"  Width="99%" AllowPaging="True" PageSize="20" CssClass="table table-hover" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>       

            <asp:BoundField DataField="nRow" HeaderText="No.">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="ชื่อเรื่อง/ผู้แต่ง/ปี"  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" CssClass="text-bold" Text='<%# DataBinder.Eval(Container.DataItem, "RYear") & " - " %>'></asp:Label>
                    <a href='ResearchDetail.aspx?pid=<%# DataBinder.Eval(Container.DataItem, "UID") %>' 
                        target="_blank">
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TitleTH") & "-" & DataBinder.Eval(Container.DataItem, "TitleEN") %>'></asp:Label><br />
                    </a>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Author") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            </asp:TemplateField>
           
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />

        <PagerStyle BackColor="#CCCCCC" ForeColor="#333333" HorizontalAlign="Center" Font-Bold="False" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
      
        
      
    </asp:GridView>
  

        </div>
        <!-- /.box-body -->      
      </div>
      <!-- /.box -->
        </div>
        <!-- /.col -->
  <div class="col-md-2">

          <!-- Profile Image -->
          <div class="box box-default">
                <div class="box-header with-border">
          <h2 class="box-title">งานวิจัยตามปี</h2>

        </div>

            <div class="box-body">
<div align="center">
             <asp:GridView ID="grdYear" runat="server"  AutoGenerateColumns="False" ShowHeader="False" Font-Bold="False" PageSize="20" >
        <Columns>
          

            <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='?y=<%# DataBinder.Eval(Container.DataItem, "RYear") %>'>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("RYear") %>'></asp:Label>
                    </a>                     
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <ItemStyle HorizontalAlign="Center" />

            </asp:TemplateField>
           
        </Columns>
        <HeaderStyle Height="25px" />
      
        
      
    </asp:GridView>
  </div>

            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

        </div>
        <!-- /.col -->

</div>
      <!-- /.row -->  

</section>
</asp:Content>
