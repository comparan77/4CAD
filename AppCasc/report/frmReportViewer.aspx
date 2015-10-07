<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmReportViewer.aspx.cs" Inherits="AppCasc.report.frmReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/moment.min.js" type="text/javascript"></script>
    <script src="../js/report/frmReportViewer.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Par&aacute;metros</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

<div class="divForm">
    <div>
        <label>Reporte:</label>
        <asp:DropDownList runat="server" ID="ddl_reporte">
            <asp:ListItem Text="Maquila" Value="Maquila"></asp:ListItem>
            <asp:ListItem Text="Piso" Value="Piso"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Fecha Inicial:</label>
        <asp:TextBox runat="server" ID="txt_fecha_ini"></asp:TextBox>
    </div>
    <div>
        <label>Fecha Final:</label>
        <asp:TextBox runat="server" ID="txt_fecha_fin"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btnGetRpt" OnClick="clickGetRpt" Text="Obtener Reporte" />
    </div>
</div>

</div>

<asp:UpdatePanel runat="server" ID="up_rpt" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnGetRpt" EventName="click" />
</Triggers>
<ContentTemplate>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Visible="false" InteractivityPostBackMode="AlwaysAsynchronous">
</rsweb:ReportViewer>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
