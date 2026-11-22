<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EventModify.aspx.vb" Inherits=".EventModify" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v23.2, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <section class="content-header">
      <h1>Event
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Home.aspx?actionType=h"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Event</li>
      </ol>
    </section>

    <!-- Main content -->   
      <section class="content">  

            <div class="row">
    <section class="col-lg-12 connectedSortable"> 

          <div class="box box-primary">
    <div class="box-header">
      <h3 class="box-title">Event Management<asp:HiddenField ID="hdUID" runat="server" />
        </h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">

          <div class="row">
            <div class="col-md-6">
          <div class="form-group">
            <label>ชื่อกิจกรรม</label>
              <asp:TextBox ID="txtName" runat="server" cssclass="form-control"  placeholder="ชื่อกิจกรรม" ></asp:TextBox>
      
          </div>
        </div>

                 <div class="col-md-2">
          <div class="form-group">
            <label>วันที่เริ่ม</label>
              <div class="input-group">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                  <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask></asp:TextBox>
                </div>            
          </div>

        </div>

  <div class="col-md-2">
          <div class="form-group">
            <label>วันที่สิ้นสุด</label>
              <div class="input-group">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                  <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask></asp:TextBox>
                </div>            
          </div>

        </div>
    </div>
        <div class="row">


             <div class="col-md-12">
          <div class="form-group">
            <label>รายละเอียด</label>
              <dx:ASPxHtmlEditor ID="xHtmlDetail" runat="server" Theme="Office2010Blue" Width="100%">
                 <SettingsImageUpload UploadFolder="~/imgNews/" UploadImageFolder="~/imgNews/">
                </SettingsImageUpload>
            </dx:ASPxHtmlEditor>
          </div>
        </div>

</div>
<div class="row">
<div class="col-md-6">  
      <div class="form-group">
            <label>เลือกสี</label>   

              <div class="btn-group" style="width: 100%; margin-bottom: 10px;">
               <ul class="fc-color-picker" id="color-chooser">
                  <asp:LinkButton ID="btnColor1" runat="server" CssClass="fc-color-picker text-aqua"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor2" runat="server" CssClass="fc-color-picker text-blue"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor3" runat="server" CssClass="fc-color-picker text-light-blue"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor4" runat="server" CssClass="fc-color-picker text-teal"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor5" runat="server" CssClass="fc-color-picker text-yellow"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor6" runat="server" CssClass="fc-color-picker text-orange"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor7" runat="server" CssClass="fc-color-picker text-green"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor8" runat="server" CssClass="fc-color-picker text-lime"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor9" runat="server" CssClass="fc-color-picker text-red"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor10" runat="server" CssClass="fc-color-picker text-purple"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor11" runat="server" CssClass="fc-color-picker text-fuchsia"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor12" runat="server" CssClass="fc-color-picker text-muted"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                  <asp:LinkButton ID="btnColor13" runat="server" CssClass="fc-color-picker text-navy"  Font-size="25px"><i class="fa fa-square"></i></a></asp:LinkButton>  
                </ul>

                  <br />
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>
                        <asp:Label ID="lblColor" runat="server" CssClass="btn btn-primary btn-block" Text="ชื่อกิจกรรม"></asp:Label>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btnColor1" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor2" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor3" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor4" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor5" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor6" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor7" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor8" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor9" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor10" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor11" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor12" EventName="Click"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btnColor13" EventName="Click"></asp:AsyncPostBackTrigger>
                      </Triggers>
                  </asp:UpdatePanel>               
              </div> 
    </div>
</div>



</div>


        <div class="row"> 
<div class="col-md-2">
          <div class="form-group mailbox-messages">                 
            <asp:CheckBox ID="chkStatus" runat="server" Checked="True"   CssClass="mailbox-messages"         Text="Public (แสดงบนปฏิทิน)" />
          
</div>

    </div>


<div class="col-md-6">
          <div class="form-group">
              <br />
<asp:Button ID="cmdAdd" runat="server" Text="Save" Width="100" CssClass="btn btn-primary" />
 </div>
        </div>

</div>
     <div class="row text-center">

         </div>

  <div class="row">
 
                <asp:GridView ID="grdCompany" 
                             runat="server" CellPadding="0" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-striped" 
                             Font-Bold="False" PageSize="15">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' /> 
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/delete.png" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="No." DataField="nRow">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />                            </asp:BoundField>
                        <asp:BoundField DataField="Title" HeaderText="ชื่อกิจกรรม" />
                            <asp:BoundField DataField="Startdate" HeaderText="วันที่เริ่ม" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="วันที่สิ้นสุด" DataField="Enddate" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="StatusFlag" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="สี">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView> 
     </div>        
         
          </div>
    </div>




        </section>
<section class="col-lg-6 connectedSortable"> 
       
        </section>
                </div>
      <div class="row">
        <div class="col-md-9">
        </div>
      </div>

          </section>
</asp:Content>