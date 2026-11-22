<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EvaluationFormList.aspx.vb" Inherits=".EvaluationFormList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<section class="content-header">
      <h1>แบบฟอร์มประเมิน</h1>   
    </section>

<section class="content">  

     <div class="box box-pink">
            <div class="box-header">
              <i class="fa fa-list"></i>

              <h3 class="box-title">เลือกแบบประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                               <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" CssClass="form-control select2">                                                      </asp:DropDownList>            
</div> 
          </div>

    <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-gear"></i>

              <h3 class="box-title">หัวข้อการประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
              <asp:GridView ID="grdEvaluation" CssClass="table table-hover" runat="server" CellPadding="0" GridLines="None" AutoGenerateColumns="False" Width="100%" DataKeyNames="UID">
            <columns>
                <asp:BoundField DataField="SEQNO" HeaderText="ลำดับที่">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px" />
                </asp:BoundField>
            <asp:BoundField DataField="TopicName" HeaderText="หัวข้อเรื่องประเมิน">                      
                <HeaderStyle HorizontalAlign="Left" />
              <itemstyle HorizontalAlign="Center" />

                <ItemStyle HorizontalAlign="Left" />

            </asp:BoundField>

            </columns>
            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />          
            <pagerstyle HorizontalAlign="Center" 
                      CssClass="dc_pagination dc_paginationC dc_paginationC11" />          
                  <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <headerstyle CssClass="th" Font-Bold="True" 
                      VerticalAlign="Middle" HorizontalAlign="Left" />          
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#F0F0F0" />
          </asp:GridView>
                                                             
</div> 
        <div class="box-footer clearfix">           
                
  <asp:Label ID="lblAlert" runat="server" CssClass="validateAlert" Width="100%" Visible="False"></asp:Label>
                
            </div>
          </div>
                           
    </section>
</asp:Content>
