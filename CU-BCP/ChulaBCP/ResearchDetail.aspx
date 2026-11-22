<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResearchDetail.aspx.vb" Inherits=".ResearchDetail" %>
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
        <li>ฐานข้อมูลงานวิจัย</li> <li class="active">รายละเอียด</li>
            </ol>
    </section> 
  <div class="row">
      
  <div class="col-md-10">

              <!-- Default box -->
      <div class="box box-default">
        <div class="box-header with-border">
          <h2 class="box-title">รายละเอียดงานวิจัย</h2>
             <div class="box-tools pull-right">          
          </div>
        </div>
        <div class="box-body"> 
            <table border="0" width="99%" class="table with-border" >
  <tr>
    <td width="130">ชื่อเรื่อง/Title</td>
    <td>
        <asp:Label ID="lblTitle" runat="server"></asp:Label>
    </td>
  </tr>
                <tr>
    <td>ผู้แต่ง/Author</td>
    <td><asp:Label ID="lblAuthor" runat="server"></asp:Label></td>
  </tr>
                <tr>
    <td>ปี/Year</td>
    <td><asp:Label ID="lblYear" runat="server"></asp:Label></td>
  </tr>              
  <tr>
    <td>สาขางานวิจัย</td>
    <td><asp:Label ID="lblType" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td>บทคัดย่อ</td>
    <td>
        <asp:HyperLink ID="lnkAbstact" runat="server" CssClass="btn btn-primary" Visible="False" Width="100px">คลิกดูบทคัดย่อ</asp:HyperLink>
      </td>
  </tr>
</table>

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
                    <a href='ResearchSearch.aspx?y=<%# DataBinder.Eval(Container.DataItem, "RYear") %>'>
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
