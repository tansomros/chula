<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StudentCert.aspx.vb" Inherits=".StudentCert" %>
<%@ Register assembly="FUA" namespace="Subgurim.Controles" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    <section class="content-header">
      <h1>ตั้งค่าใบรับรองการผ่านการฝึกปฏิบัติงาน</h1>   
    </section>
<section class="content">
     <div class="box box-solid">
            <div class="box-header">
              <i class="fa fa-gear"></i>
              <h3 class="box-title">กำหนดค่า</h3>             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">
                <h4 class="timeline-header">วันที่เปิดให้ดาวน์โหลดใบรับรอง</h4>
                                <div class="row">
    <div class="col-md-3">
        <div class="form-group"> 
            <div class="input-group">
                <asp:TextBox ID="txtAnnouncedDate" runat="server" CssClass="form-control text-center"
                    autocomplete="off" data-date-format="dd/mm/yyyy"
                    data-date-language="th-th" data-provide="datepicker"
                    onkeyup="chkstr(this,this.value)"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                </div>
            </div>
        </div>
    </div>
 <div class="col-md-3">
      <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="Save" width="100" />
     </div>
</div>

                 <h4 class="timeline-header">กำหนดวันที่ออกใบรับรอง</h4>
                   <div class="row">
       <div class="col-md-2">
           <div class="form-group">
               <label>ปีการศึกษา</label>
                <asp:TextBox ID="txtEduYear" runat="server" CssClass="form-control text-center"></asp:TextBox>
           </div>
       </div>
       <div class="col-md-3">
           <div class="form-group">
               <label>ลงวันที่ออกใบรับรอง</label>
               <div class="input-group">
                   <asp:TextBox ID="txtCertDate" runat="server" CssClass="form-control text-center"
                       autocomplete="off" data-date-format="dd/mm/yyyy"
                       data-date-language="th-th" data-provide="datepicker"
                       onkeyup="chkstr(this,this.value)"></asp:TextBox>
                   <div class="input-group-append">
                       <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                   </div>
               </div>
           </div>
       </div>    
                       <div class="col-md-2 pt-4">
      <asp:Button ID="cmdSaveCert" CssClass="btn btn-primary" runat="server" Text="Save" width="100" />
     </div>
   </div>
   <div class="row">
       <div class="col-md-6">
           <asp:GridView ID="grdData" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="False">
               <RowStyle HorizontalAlign="Center" />
               <Columns>
                   <asp:BoundField HeaderText="ปีการศึกษา" DataField="EduYear">
                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="CertDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่ออกใบรับรอง">
                       <HeaderStyle HorizontalAlign="center" />
                       <ItemStyle HorizontalAlign="center" />
                   </asp:BoundField>
                   <asp:TemplateField HeaderText="แก้ไข">
                       <ItemTemplate>
                           <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="images/icon-edit.png"
                               CommandArgument='<%# Container.DataItemIndex %>' />
                       </ItemTemplate>
                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"/>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="ลบ">
                       <ItemTemplate>
                           <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/icon-delete.png"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EduYear") %>'  />
                       </ItemTemplate>
                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                   </asp:TemplateField>
               </Columns>
               <PagerStyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
           </asp:GridView>
       </div>
   </div>
</div>
            <div class="box-footer clearfix">
              
            </div>
          </div>                       
    </section>  
</asp:Content>
