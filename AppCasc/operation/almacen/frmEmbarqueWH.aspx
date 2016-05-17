<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmEmbarqueWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmEmbarqueWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.updatepanel.js" type="text/javascript"></script>
    <script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../../js/operation/almacen/frmEmbarqueWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Orden de Carga</h3>
<div id="div_orde_carga" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>Folio de Orden de Carga:</label>
        <input type="text" id="txt_folio_oc" />
    </div>
    <div style="margin-top: 10px;">
        <button id="btnBuscaOC">Cargar orden de Compra</button>
    </div>
    <div>
        <span style="color: Red; visibility: hidden;" class="validator" id="spn_rfv_oc">Es necesario proporcionar la orden de carga.</span>
    </div>
    <div style="margin-top: 10px;">
        <table class="grdCascSmall">
            <thead>
                <tr>
                    <th>Remisi&oacute;n</th>
                    <th>Mercanc&iacute;a</th>
                    <th>Descripci&oacute;n</th>
                    <th>Tarimas</th>
                    <th>Cajas</th>
                    <th>Piezas</th>
                </tr>
            </thead>
            <tbody id="tbodyRemByOc">
            
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3">&nbsp;</td>
                    <td><span id="spn_tarimas"></span></td>
                    <td><span id="spn_cajas"></span></td>
                    <td><span id="spn_piezas"></span></td>
                </tr>
            </tfoot>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hf_id_entrada" />
    <asp:HiddenField runat="server" ID="hf_id_orden_carga" />
</div>

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Datos Generales</h3>
<div id="div_generales" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <%--<div>
        <label>Bodega:</label>
        <asp:TextBox runat="server" ID="txt_bodega" CssClass="txtLarge" ></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvBodega" ControlToValidate="txt_bodega" ErrorMessage="Es necesario proporcionar una bodega"></asp:RequiredFieldValidator>
    </div>--%>
    <div>
        <label>Fecha:</label>
        <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Hora de Salida:</label>
        <asp:TextBox id="txt_hora_salida" CssClass="horaPicker" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvHora_salida" ControlToValidate="txt_hora_salida" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
        <span class="hidden error">Es necesario proporcionar una hora</span>
    </div>
    <%--<div>
        <label>Cliente:</label>
        <asp:TextBox runat="server" ID="txt_cliente" CssClass="txtLarge"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCliente" ControlToValidate="txt_cliente" ErrorMessage="Es necesario seleccionar un cliente"></asp:RequiredFieldValidator>
    </div>--%>
    <div>
        <asp:HiddenField runat="server" ID="hf_destino" />
        <%--<label>Destino:</label>
        <asp:DropDownList runat="server" ID="ddlDestino"></asp:DropDownList>--%>
    </div>
</div>

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Informaci&oacute;n del Transporte</h3>
<div id="div_transporte" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

    <div>
        <label>Transporte:</label>
        <%--<asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>--%>
        <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
    </div>
    <div>
        <label>Tipo de Transporte:</label>
    <asp:UpdatePanel runat="server" ID="upTipoTransporte" UpdateMode="Conditional">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTransporte" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
            <asp:DropDownList runat="server" ID="ddlTipo_Transporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_Transporte_changed"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_tipo_transporte" InitialValue="" ControlToValidate="ddlTipo_Transporte" ErrorMessage="Es necesario seleccionar un tipo de transporte"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel runat="server" ID="upDatosVehiculo" UpdateMode="Conditional">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
    <div>
        <label>Placa:</label>
        <asp:TextBox id="txt_placa" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_placa" ControlToValidate="txt_placa" ErrorMessage="Es necesario capturar la placa para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Caja:</label>
        <asp:TextBox id="txt_caja" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja" ControlToValidate="txt_caja" ErrorMessage="Es necesario capturar la placa de la caja para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Contenedor 1:</label>
        <asp:TextBox id="txt_caja_1" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_1" ControlToValidate="txt_caja_1" ErrorMessage="Es necesario capturar el número de contenedor 1 para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Contenedor 2:</label>
        <asp:TextBox id="txt_caja_2" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_2" ControlToValidate="txt_caja_2" ErrorMessage="Es necesario capturar el número de contenedor 2 para este tipo de vehículo"></asp:RequiredFieldValidator>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    <div>
        <label>Sello:</label>
        <asp:TextBox id="txt_sello" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvSello" ControlToValidate="txt_sello" ErrorMessage="Es necesario capturar el sello"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Carta Porte:</label>
        <asp:TextBox id="txt_talon" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCartaPorte" ControlToValidate="txt_talon" ErrorMessage="Es necesario capturar la carta porte"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Custodia:</label>
        <asp:DropDownList runat="server" ID="ddlCustodia"></asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCustodia" ControlToValidate="ddlCustodia" InitialValue="" ErrorMessage="Es necesario seleccionar una custodia"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Operador:</label>
        <asp:TextBox runat="server" ID="txt_operador"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador de la custodia"></asp:RequiredFieldValidator>
    </div>

    <hr style="border-color: transparent" />
    
    <div style="margin-left: 205px; margin-top: 10px;">
    <asp:HiddenField runat="server" ID="hf_condiciones_transporte" />
    <span style="color: Red; visibility: hidden;" class="validator" id="rfv_condiciones_transporte">Es necesario proporcionar TODAS LAS CONDICIONES del transporte.</span>
        <table class="grdCascSmall">
            <thead>
                <tr>
                    <th>Condiciones del Transporte</th>
                    <th>S&iacute;</th>
                    <th>No</th>
                </tr>
            </thead>
            <tbody id="tbody_condiciones">
                
            </tbody>
        </table>
    </div>

</div>

<h3 id="H3" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Informaci&oacute;n Adicional</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>Vigilante:</label>
        <asp:TextBox CssClass="txtMedium" runat="server" ID="txt_vigilante" ToolTip="Nombre del vigilante en turno que supervisa la descarga"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvVigilante" ControlToValidate="txt_vigilante" ErrorMessage="Es necesario proporcionar vigilante" ></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Observaciones:</label>
        <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" Columns="1" ID="txt_observaciones" ToolTip="Anotaciones de lo observado en la mercancía y que pudiera ser relevante."></asp:TextBox>
    </div>
</div>

<div>
    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_click" />
    <asp:HiddenField runat="server" ID="hf_click_save" />
</div>

</asp:Content>
