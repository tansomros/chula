<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentGate.aspx.vb" Inherits=".AssessmentGate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <section class="content-header">
      <h1>สิทธิในการทำแบบประเมิน</h1> <small>ปีการศึกษา : <asp:Label ID="lblYear" runat="server"></asp:Label></small>    
    </section>
<section class="content">
    <asp:Panel ID="pnAdv" runat="server">
       <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-circle-o"></i>

              <h3 class="box-title">แบบประเมินสำหรับอาจารย์ประจำรายวิชา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
 <asp:DataList ID="dlAdvisor" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" >
                  <ItemTemplate>
                       <asp:Button ID="cmdSubj" runat="server" CssClass="buttonMenu" Text='<%# DataBinder.Eval(Container.DataItem, "SubjectTXT") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>' />
                  </ItemTemplate>
              </asp:DataList>
                
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</asp:Panel>
    <asp:Panel ID="pnPct" runat="server">
    <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-md"></i>

              <h3 class="box-title">แบบประเมินสำหรับอาจารย์แหล่งฝึก</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
  <asp:DataList ID="dlLocation" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" >
                  <ItemTemplate>
                       <asp:Button ID="cmdLoc" runat="server" CssClass="buttonRedial" Text='<%# DataBinder.Eval(Container.DataItem, "LocationName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>' />
                  </ItemTemplate>
              </asp:DataList>
                
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
</asp:Panel>
</section>
        
</asp:Content>
