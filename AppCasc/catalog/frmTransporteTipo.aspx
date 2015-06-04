<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmTransporteTipo.aspx.cs" Inherits="AppCasc.catalog.frmTransporteTipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Transporte Tipo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Peso M&aacute;ximo (Kg):</label>
        <asp:TextBox runat="server" ID="txt_peso_maximo"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvPeso_maximo" ControlToValidate="txt_peso_maximo" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" runat="server" ControlToValidate="txt_peso_maximo" ID="rvPeso_maximo" MinimumValue="1" MaximumValue="100000" ErrorMessage="Es necesario capturar un número entre 1 y 100,000"></asp:RangeValidator>
    </div>
    <div style="clear: left;">
        <label>Requiere Captura de Placa:</label>
        <asp:CheckBox runat="server" ID="chkPlaca" Checked="true" />
    </div>
    <div style="clear: left;">
        <label>Requiere Captura de Caja:</label>
        <asp:CheckBox runat="server" ID="chkCaja" OnCheckedChanged="chkCaja_checked" AutoPostBack="true" />
    </div>
    <div style="clear: left;">
        <label>Requiere Captura de Contenedor 1:</label>
        <asp:CheckBox runat="server" ID="chkCaja1" OnCheckedChanged="chkCaja1_checked" AutoPostBack="true" />
    </div>
    <div style="clear: left;">
        <label>Requiere Captura de Contenedor 2:</label>
        <asp:CheckBox runat="server" ID="chkCaja2" OnCheckedChanged="chkCaja2_checked" AutoPostBack="true"/>
    </div>
    <div style="clear: left;"></div>
    <hr />
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" OnClick="btnCancel_click" CausesValidation="false" />
    </div>
</div>
</div>
</asp:Content>
