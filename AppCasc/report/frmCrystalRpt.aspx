<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmCrystalRpt.aspx.cs" Inherits="AppCasc.report.frmCrystalRpt" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/moment.min.js" type="text/javascript"></script>
    <script src="../js/report/frmCrystalRpt.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />
<asp:HiddenField runat="server" ID="hfPageLoaded" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Par&aacute;metros</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

<div class="divForm">
<div>
    <label>Reporte:</label>
    <asp:DropDownList runat="server" ID="ddl_rpt">
        <asp:ListItem Value="personal_empresa" Text="Listado de personal por Empresa"></asp:ListItem>
        <asp:ListItem Value="119" Text="Lista de personal CASC-119"></asp:ListItem>
    </asp:DropDownList>
</div>
<div id="sede">
    <label>Sede:</label>
    <asp:DropDownList runat="server" ID="ddl_sede"></asp:DropDownList>
</div>
<div id="periodo">
    <div>
        <label>Fecha Inicial:</label>
        <asp:TextBox runat="server" ID="txt_fecha_ini"></asp:TextBox>
    </div>
    <div>
        <label>Fecha Final:</label>
        <asp:TextBox runat="server" ID="txt_fecha_fin"></asp:TextBox>
    </div>
</div>
<asp:Button runat="server" ID="btn_get_rpt" Text="Obtener Reporte" />
</div>
</div>


    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="true" ToolPanelView="None" HasDrilldownTabs="False" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" />
</asp:Content>
