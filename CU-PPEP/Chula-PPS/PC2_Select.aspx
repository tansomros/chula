<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PC2_Select.aspx.vb" Inherits=".PC2_Select" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
     <section class="content-header">
      <h1>การคัดเลือก
        <small></small>
      </h1>    
    </section>

<section class="content">    
      
      <div class="row">
        <div class="col-md-6">
          <div class="small-box bg-blue">
            <div class="inner">
              <h3>Randomization</h3>

              <p>คัดเลือกโดยระบบสุ่ม</p>
            </div>
            <div class="icon">
              <i class="ion ion-load-a"></i>
            </div>
            <a href="PC2_AssignRandom.aspx?ActionType=pc2&ItemType=eva&t=new" class="small-box-footer">Go <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
              
            <div class="col-md-6">
          <div class="small-box bg-purple">
            <div class="inner">
              <h3>Manual</h3>

              <p>คัดเลือกด้วยวิธีจัดสรรโดย admin</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="PC2_AssignManual.aspx?ActionType=pc2&ItemType=eva&t=new" class="small-box-footer">Go <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
      </div>
    
    </section>
</asp:Content>
