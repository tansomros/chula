<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Academic.aspx.vb" Inherits=".Academic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="containerPage">
<section class="content">   
     <section class="content-header">     
<h1 class="news-heading text-green">บริการวิชาการ</h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">บริการวิชาการ</li>
      </ol>
    </section>    
        
            <div class="row" align="right">   
               <table>
                <tr>                    
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="ค้นหา"></asp:Label>
                        <asp:TextBox ID="txtFind" runat="server" CssClass="tb10" Width="350px"></asp:TextBox>
                    </td>
                    <td>            
                        <asp:ImageButton ID="imgSearch" runat="server"  ImageUrl="~/Images/icon/search.png" />     
                    </td>                  
                </tr>
            </table>
</div>
    <br />
            <div class="row">
           <div class="grid-post grid-template-col-4 grid-template-list-xs grid-gap-post"> 
           
<% For i = 0 To dtNew.Rows.Count - 1 %>
               <div id="post-<% Response.Write(i) %>"  class="grid-post-item">  
            <div class="card card-sm card-post h-100">
                <div class="card-media">
                    <a target="_blank" href='<% Response.Write(dtNew.Rows(i)("NewsLink")) %>' title='<% Response.Write(dtNew.Rows(i)("NewsHead")) %>' rel="bookmark">
                            <img src="<% Response.Write(ImageCoverNews + dtNew.Rows(i)("CoverimagePath")) %>" class="attachment-post-thumbnail size-post-thumbnail wp-post-image" alt="">
                    </a>  
                </div>
                <div class="card-body p-0 px-sm-6 py-sm-5">
                    <h3 class="display-posttitle mb-2">
                        <a target="_blank" href='<% Response.Write(dtNew.Rows(i)("NewsLink")) %>' title='<% Response.Write(dtNew.Rows(i)("NewsHead")) %>' rel="bookmark"><% Response.Write(dtNew.Rows(i)("NewsHead")) %></a>
                    </h3>                  
                </div>              
            </div>       
</div>
       
<% Next %>  

           </div>
</div>

          <br />
        <div class="row">
          <center>
                <table id="tblPaging" runat="server">
                    <tr>
                        <td style="padding-right: 7px" valign="top">
                            <asp:LinkButton ID="lnkbtnPrevious" runat="server" 
                                OnClick="lnkbtnPrevious_Click">ก่อนหน้า</asp:LinkButton>
                        </td>
                        <td valign="top">
                            <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" 
                                OnItemDataBound="dlPaging_ItemDataBound" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnPaging" runat="server" 
                                        CommandArgument='<%# Eval("PageIndex") %>' CommandName="lnkbtnPaging" 
                                        Text='<%# Eval("PageText") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                        <td style="padding-left: 7px" valign="top">
                            <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">ต่อไป</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </center>
        </div>         
  </section>                  
</div>
</asp:Content>