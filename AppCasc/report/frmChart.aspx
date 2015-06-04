<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmChart.aspx.cs" Inherits="AppCasc.report.frmChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/MstCasc.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/frmChart.css" rel="stylesheet" type="text/css" />
    
    <%--<script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>--%>
    <script src="../js/report/frmChart.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div id="div_panel">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Chart</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div class="divForm">
<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega" OnSelectedIndexChanged="ddlBodega_changed" AutoPostBack="true"></asp:DropDownList>
</div>

<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" OnSelectedIndexChanged="ddlCliente_changed" AutoPostBack="true"></asp:DropDownList>
</div>

<div>
    <label>Fecha Inicial:</label>
    <asp:TextBox runat="server" ID="txt_fecha_ini" ></asp:TextBox>
</div>

<div>
    <label>Fecha Final:</label>
    <asp:TextBox runat="server" ID="txt_fecha_fin" ></asp:TextBox>
</div>
</div>

<asp:Button runat="server" Text="Generar Chart" ID="btnGetChart" OnClick="btnGetChart_click" />
<asp:HyperLink runat="server" CssClass="lnkChart" ID="lnkChart" Visible="false"></asp:HyperLink>

</div>
</div>

<div id="divFileChartMovAnt">
    <a class="lnkChart" href="../rpt/chart/RegistroEntradasSalidasAl240313.xls">Descarga aqu&iacute; los movimientos registrados del 01-01-13 al 24-03-13</a>
</div>

</asp:Content>
