<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssessmentResult.aspx.vb" Inherits=".AssessmentResult" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <section class="content-header">
      <h1>ผลการประเมิน</h1>   
    </section>

<section class="content">  

     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-search"></i>

              <h3 class="box-title">ค้นหา</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body">     
                
                  <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>ปีการศึกษา</label>
                             <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"  CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label>รายวิชา</label>
                              <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label>ค้นหา</label>
                              <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-md-2">
                        <div class="form-group">         
                            <br />
                 <asp:Button ID="cmdSearch" runat="server" Text="ค้นหา" CssClass="btn btn-warning" Width="60" />&nbsp;<asp:Button ID="cmdSearchAll" runat="server" Text="ดูทั้งหมด" CssClass="btn btn-warning" />
                        </div>
                    </div>

             </div> 
                 
                                      
</div>
            <div class="box-footer text-center">
               <asp:Button ID="cmdExport" runat="server" Text="รายงานสรุปเกรด" CssClass="btn btn-pink" />
               <asp:Button ID="cmdScore" runat="server" Text="รายงานสรุปคะแนน" CssClass="btn btn-pink" />
            </div>
 </div>       
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="modal_loading">
                <div class="center_loading">
                    <img alt="" src="images/loading.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="box box-primary">
            <div class="box-header">
              <i class="fa fa-user-circle-o"></i>

              <h3 class="box-title">รายการผลการประเมิน</h3>
             
                 <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>                                 
            </div>
            <div class="box-body"> 
                <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" PageSize="20" Width="100%"  CssClass="table table-hover" AllowPaging="True">
                            <RowStyle BackColor="#F7F7F7" HorizontalAlign="Center" />
                            <columns>
                                <asp:BoundField DataField="nRow" HeaderText="No.">
                                <itemstyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentCode" HeaderText="รหัสนิสิต">
                                <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText="ชื่อ - สกุล">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MajorName" HeaderText="สาขาวิชา" />
                                <asp:BoundField DataField="LocationName" HeaderText="แหล่งฝึก">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PhaseNo" HeaderText="ผลัดที่">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PhaseDesc" HeaderText="วันที่ฝึก" />
                                <asp:BoundField DataField="ASCORE" HeaderText="Advisor" />
                                <asp:BoundField DataField="PSCORE" HeaderText="Preceptor" />
                                <asp:BoundField DataField="GradeScore" HeaderText="Total">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Grade" HeaderText="Standing" />
                            </columns>
                            <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <pagerstyle CssClass="dc_pagination dc_paginationC dc_paginationC11"  HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <headerstyle CssClass="th" Font-Bold="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCourse" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmdSearchAll" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmdScore" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmdExport" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                              </div>                   
</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>



</section>
</asp:Content>
