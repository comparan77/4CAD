<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmPreArribo.aspx.cs" Inherits="AppCasc.operation.frmPreArribo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
     <script src="../../js/jquery.maskedinput.min.js" type="text/javascript"></script>
     <script src="../../js/operation/frmPreArribo.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Precarga de Informaci&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda" style="position: relative;">
    <label>No. Referencia:</label>
    <asp:TextBox runat="server" ID="txt_referencia" AutoPostBack="true" OnTextChanged="validar_referencia"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_referencia" ControlToValidate="txt_referencia" ErrorMessage="Es necesario capturar la referencia"></asp:RequiredFieldValidator>
    <asp:UpdatePanel runat="server" ID="up_valida_ref" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txt_referencia" EventName="TextChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:Label CssClass="error" runat="server" ID="lbl_msg_valida"></asp:Label>
        <asp:Panel ID="pnl_right_data" runat="server" Visible="false">
            <span class="ui-icon ui-icon-check" style="position: absolute; top: 5px; left: 360px;"></span>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

</div>

<h3 id="H2" style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos Generales</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega"></asp:DropDownList>
</div>

<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" AutoPostBack="true" OnSelectedIndexChanged="change_cliente"></asp:DropDownList>
</div>

<asp:UpdatePanel runat="server" ID="upEjecutivo" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
</Triggers>
    <ContentTemplate>
        <label>Ejecutivo:</label>
        <asp:DropDownList runat="server" ID="ddlEjecutivo"></asp:DropDownList>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvEjecutivo" ControlToValidate="ddlEjecutivo" InitialValue="" ErrorMessage="Es necesario seleccionar un ejecutivo"></asp:RequiredFieldValidator>
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<h3 id="H1" style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos de la Unidad</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div class="divForm">
    <div>
        <label>Nombre del operador:</label>
        <asp:TextBox CssClass="txtLarge" runat="server" ID="txt_operador"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_operador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el nombre del operador"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Tipo:</label>
        <asp:DropDownList runat="server" ID="ddlTipo_Transporte"></asp:DropDownList>
    </div>
    <div>
        <label>Placa de la Unidad:</label>
        <asp:TextBox id="txt_placa" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_placa" ControlToValidate="txt_placa" ErrorMessage="Es necesario capturar la placa para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Placa de la Caja:</label>
        <asp:TextBox id="txt_caja" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja" ControlToValidate="txt_caja" ErrorMessage="Es necesario capturar la placa de la caja para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>No. Contenedor 1:</label>
        <asp:TextBox id="txt_caja_1" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_1" ControlToValidate="txt_caja_1" ErrorMessage="Es necesario capturar el número de contenedor 1 para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>No. Contenedor 2:</label>
        <asp:TextBox id="txt_caja_2" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_2" ControlToValidate="txt_caja_2" ErrorMessage="Es necesario capturar el número de contenedor 2 para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>

    <div>
        <label><input type="checkbox" id="chk_sello" checked="checked" /><span>Con sello</span></label>
        <asp:TextBox id="txt_sello" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvSello" ControlToValidate="txt_sello" ErrorMessage="Es necesario capturar el sello"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Observaciones</label>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_observaciones"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_save" Text="Guardar" OnClick="save_click" />
    </div>
</div>
</div>


</div>

</asp:Content>
