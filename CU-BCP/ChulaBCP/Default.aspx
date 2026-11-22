<%@ Page Title="หน้าแรก" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.vb" Inherits="_Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">     
        
<div class="containerPage body-content">
 <section class="content"> 
     <div class="row">
          <div id="slider">
            <div class="royalSlider new-royalslider-1 rsMinW rs-default-template" id="new-royalslider-1">           
            <% For i = 0 To dtS.Rows.Count - 1 %>
                 <% Response.Write("<div class='rsContent'><img  class='rsImg' src='imageslide/" & dtS.Rows(i)("imgpath") & "' width='100%' height='320px' /><a class='rsLink' href='" & dtS.Rows(i)("url") & "' title='" & dtS.Rows(i)("title") & "' target='_blank'></a></div>") %>
            <% Next %>              
        </div>
     </div>
     </div>
 <div class="row">             
 <div class="col-md-12 fullwidth-content text-center"  >
                  <img src="images/linegreen.png"  />  
</div> 
</div> 
 <div class="row">
<h1 class="news-heading text-green"> ข่าวประชาสัมพันธ์</h1>
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
             <div class="row text-center">
                 <a href="news" target="_blank"><h3><i  class="fa fa-arrow-right"></i> อ่านเพิ่มเติม</h3></a>
                 </div>
     <hr /> 
</section>
        </div>
        
    </asp:Content>