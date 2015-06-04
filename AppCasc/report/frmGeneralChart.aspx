<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmGeneralChart.aspx.cs" Inherits="AppCasc.report.frmGeneralChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/MstCasc.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/jquery.jqplot.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmChart.css" rel="stylesheet" type="text/css" />
    
    <%--<script src="../js/jquery.js" type="text/javascript"></script>--%>
    <script src="../js/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <!--[if lt IE 9]>
        <script src="../js/jqPlot/excanvas.js" type="text/javascript"></script>
    <![endif]-->
    <script src="../js/jqPlot/jquery.jqplot.min.js" type="text/javascript"></script>
    <script src="../js/jqPlot/jqplot.barRenderer.min.js" type="text/javascript"></script>
    <script src="../js/jqPlot/jqplot.categoryAxisRenderer.min.js" type="text/javascript"></script>
    <script src="../js/jqPlot/jqplot.pointLabels.min.js" type="text/javascript"></script>

    <script src="../js/report/frmGeneralChart.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Gr&aacute;fica General</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div class="divForm">
    <div>
        <label>Año de Operaci&oacute;n:</label>
        <asp:LinkButton runat="server" ID="lnkPrevYear" OnClick="lnkPrevYear_click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lnkActYear"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lnkNexYear" OnClick="lnkNexYear_click" Visible="false"></asp:LinkButton>
    </div>
    <div>
        <label>Mes de Operaci&oacute;n:</label>
        <asp:DropDownList runat="server" ID="ddlMesOperacion" AutoPostBack="true" OnSelectedIndexChanged="parameters_changed">
            <asp:ListItem Value="1">Enero</asp:ListItem>
            <asp:ListItem Value="2">Febrero</asp:ListItem>
            <asp:ListItem Value="3">Marzo</asp:ListItem>
            <asp:ListItem Value="4">Abril</asp:ListItem>
            <asp:ListItem Value="5">Mayo</asp:ListItem>
            <asp:ListItem Value="6">Junio</asp:ListItem>
            <asp:ListItem Value="7">Julio</asp:ListItem>
            <asp:ListItem Value="8">Agosto</asp:ListItem>
            <asp:ListItem Value="9">Septiembre</asp:ListItem>
            <asp:ListItem Value="10">Octubre</asp:ListItem>
            <asp:ListItem Value="11">Noviembre</asp:ListItem>
            <asp:ListItem Value="12">Diciembre</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Cuenta:</label>
        <asp:DropDownList runat="server" ID="ddlCliente" AutoPostBack="true" OnSelectedIndexChanged="parameters_changed">
        </asp:DropDownList>
    </div>
</div>
</div>

<div>
    <asp:HiddenField runat="server" ID="hfsBultoEntrada" />
    <asp:HiddenField runat="server" ID="hfsBultoSalida" />
    <asp:HiddenField runat="server" ID="hfsPiezaEntrada" />
    <asp:HiddenField runat="server" ID="hfsPiezaSalida" />
    <asp:HiddenField runat="server" ID="hfMesOperacion" />
    <div id="divBultos" style="height:300px; width: 450px; float: left;"></div>
    <div id="divPiezas" style="height:300px; width: 450px; float: right;"></div>
</div>
</asp:Content>
