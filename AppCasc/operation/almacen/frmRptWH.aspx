<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRptWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmRptWH" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../../js/moment.min.js" type="text/javascript"></script>
    <script src="../../js/operation/almacen/frmRptWH.js?v1.1.150619_1446" type="text/javascript"></script>
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
            <asp:ListItem Text="Resumen" Value="resumen"></asp:ListItem>
            <asp:ListItem Text="Inventario total diario" Value="InvTotDia"></asp:ListItem>
            <asp:ListItem Text="Relación diaria de Entradas" Value="RelDiaEnt"></asp:ListItem>
            <asp:ListItem Text="Relación diaria de Salidas" Value="RelDiaSal"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id="div_periodo" class="hidden">
    <div>
        <label>Fecha Inicial:</label>
        <asp:TextBox runat="server" ID="txt_fecha_ini"></asp:TextBox>
    </div>
    <div>
        <label>Fecha Final:</label>
        <asp:TextBox runat="server" ID="txt_fecha_fin"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btnGetRptAsync" OnClick="clickGetRptAsync" Text="Obtener Reporte" />
    </div>
    </div>
    <div id="div_mensual" class="hidden">
        <asp:HiddenField runat="server" ID="hf_mes" />
        <div>
            <label>A&ntilde;o de Operaci&oacute;n:</label>
            <span class="icon-button-action" id="spn_PrvYear"></span>
            <span class="icon-button-action" id="spn_ActYear"></span>
            <span class="icon-button-action hidden" id="spn_NxtYear"></span>
        </div>
        <div>
            <label>Mes de Operaci&oacute;n:</label>
            <select id="MesOperacion">
                <option value="1">Enero</option>
                <option value="2">Febrero</option>
                <option value="3">Marzo</option>
                <option value="4">Abril</option>
                <option value="5">Mayo</option>
                <option value="6">Junio</option>
                <option value="7">Julio</option>
                <option value="8">Agosto</option>
                <option value="9">Septiembre</option>
                <option value="10">Octubre</option>
                <option value="11">Noviembre</option>
                <option value="12">Diciembre</option>
            </select>
        </div>

        <div>
            <asp:Button runat="server" ID="btnGetRpt" OnClick="clickGetRpt" Text="Obtener Reporte" />
        </div>

    </div>
    
</div>

<%--<asp:UpdatePanel runat="server" ID="up_rpt" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnGetRptAsync" EventName="click" />
</Triggers>
<ContentTemplate>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Visible="false" InteractivityPostBackMode="AlwaysAsynchronous">
</rsweb:ReportViewer>
</ContentTemplate>
</asp:UpdatePanel>--%>

</div>

</asp:Content>
