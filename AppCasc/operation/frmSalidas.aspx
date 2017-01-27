<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmSalidas.aspx.cs" Inherits="AppCasc.operation.frmSalidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.updatepanel.js" type="text/javascript"></script>
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmSalidas.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div id="div_salida">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />
<asp:HiddenField runat="server" ID="hfEsCompartida" />
<asp:HiddenField runat="server" ID="hfFolio" />

<div id="dialog-confirm" title="Datos de salida">
    <input type="hidden" id="hf-confirmado" value="0" />
    <p id="p_aviso_registro_salida">
    <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
    <span id="spn-aviso-registro"></span>
    </p>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Salida</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlBodega_changed"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvBodega" ControlToValidate="ddlBodega" InitialValue="" ErrorMessage="Es necesario seleccionar una bodega"></asp:RequiredFieldValidator>
</div>
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
<div>
    <label>Cortina:</label>
    <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" OnSelectedIndexChanged="ddlCliente_changed" AutoPostBack="true"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCliente" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Es necesario seleccionar un cliente"></asp:RequiredFieldValidator>
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Documentos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<asp:UpdatePanel runat="server" ID="upDocRequerido" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="txt_referencia" EventName="TextChanged" />
</Triggers>
<ContentTemplate>
<div id="divReferenciaReq">
    <asp:HiddenField runat="server" ID="hfReferencia" />
    <asp:HiddenField runat="server" ID="hfIdDocReq" />
    <asp:HiddenField runat="server" ID="hfMascara" />
    <label id="lblReferencia">Documento:</label>
    <asp:TextBox runat="server" ID="txt_referencia" AutoPostBack="true" OnTextChanged="txt_referencia_TextChanged"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" ID="rfvReferencia" runat="server" ControlToValidate="txt_referencia" ></asp:RequiredFieldValidator>
    <asp:CustomValidator CssClass="validator" ID="cvReferencia" runat="server" ControlToValidate="txt_referencia" OnServerValidate="cvReferencia_ServerValidate"></asp:CustomValidator>
</div>
<div>
    <label>Tipo de Documento Recibido:</label>
    <asp:DropDownList runat="server" ID="ddlDocumento"></asp:DropDownList>
</div>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel runat="server" ID="up_documento_enviado" UpdateMode="Conditional">
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnAdd_documento" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="btnRem_documento" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
</Triggers>
<ContentTemplate>

<div>
    <label>Ref del Documento Enviado:</label>
    <asp:TextBox runat="server" ID="txt_referencia_documento"></asp:TextBox>
    <asp:Button runat="server" ID="btnAdd_documento" Text="Agregar" CausesValidation="false" OnClick="btnAdd_documento_click" />
    <div class="hidden">
        <asp:TextBox runat="server" ID="txt_documentosReq"></asp:TextBox>
    </div>
</div>
<div>
    <label>Documentos Capturados</label>
    <asp:ListBox runat="server" CssClass="documentoEnviado" ID="lst_documento_recibido" ></asp:ListBox>   
</div>
<div id="div_btnRemoveDR" class="hidden">
    <asp:Button runat="server" ID="btnRem_documento" Text="Eliminar Documento Seleccionado" CausesValidation="false" OnClick="btnRem_documento_click" />
</div>
</ContentTemplate>
</asp:UpdatePanel>

<div>
    <div>
        <label id="lblConsolidada">NO Consolidada</label>
        <input type="checkbox" id="chkConsolidado" />
        <asp:HiddenField runat="server" ID="hfConsolidada" Value="false" />
    </div>
    <div id="pnl_consolidada" class="hidden">
        <asp:UpdatePanel runat="server" ID="up_consolidada" UpdateMode="Conditional">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdd_pedimento" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div>
                <label>Pedimento:</label>
                <asp:TextBox runat="server" ID="txt_pedimento_consolidado"></asp:TextBox>
                <asp:CustomValidator CssClass="validator" ID="cvPedimentoConsolidado" runat="server" ControlToValidate="txt_pedimento_consolidado" OnServerValidate="cvReferencia_ServerValidate"></asp:CustomValidator>
                <asp:Button runat="server" ID="btnAdd_pedimento" ValidationGroup="vgPedimentos" CausesValidation="false" Text="Agregar" OnClick="btnAdd_pedimento_click" />
            </div>
            <div>
                <asp:Label runat="server" CssClass="validator" ID="cvPedimento" Visible="false" Text="El pedimento ya fue agregado" style="color: Red"></asp:Label>
            </div>
            <div>
                <label>Pedimentos Agregados:</label>
                <asp:ListBox runat="server" CssClass="pedimentoAgregado" ID="lst_pedimentos_consolidados"></asp:ListBox>
            </div>
            <div id="div_btnRemovePT" class="hidden">
                <asp:Button runat="server" ID="btnRem_pedimento" Text="Eliminar Pedimento Seleccionado" CausesValidation="false" OnClick="btnRem_pedimento_click" />
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Mercanc&iacute;a y Transporte</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <div>
        <ul id="ul_destinos" style="margin: 0; padding: 0;">
            <li style="float: left; margin-right: 10px"><a destino="R1 - AV. GUERRERO N° 149, COL. RANCHO SECO CELAYA, GUANAJUATO." href="#">R1</a></li>
            <li style="float: left; margin-right: 10px"><a destino="R2 - AV. GUERRERO N° 149, COL. RANCHO SECO CELAYA, GUANAJUATO." href="#">R2</a></li>
            <li style="float: left; margin-right: 10px"><a destino="AVONOVA - AV. GUERRERO N° 149, COL. RANCHO SECO CELAYA, GUANAJUATO." href="#">AVONOVA</a></li>
            <li style="float: left; margin-right: 10px"><a destino="VICA - CALLE LAUREL 103 COL. FRACCIONAMIENTO INDUSTRIAL EL VERGEL. MUNICIPIO: CELAYA, GUANAJUATO. C.P. 38110" href="#">VICA</a></li>
            <li style="float: left; margin-right: 10px"><a destino="JASCER - PRIVADA RENOVACIÓN 131, LAS FLORES, 38090 CELAYA, GTO." href="#">JASCER</a></li>
            <li style="float: left; margin-right: 10px"><a destino="VALEX - VALEX MAQ SA DE CV FUERZAS ZAPATISTAS 108-B COL.EMILIANO ZAPATA,CELAYA MEX,GUANAJUATO MX CP 38030" href="#">VALEX</a></li>
        </ul>
    </div>
    <div style="clear: left;"></div>
    <label>Destino:</label>
    <asp:UpdatePanel runat="server" ID="up_Destino" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:TextBox id="txt_destino" CssClass="txtLarge" runat="server"></asp:TextBox>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_destino" ControlToValidate="txt_destino" ErrorMessage="Es necesario capturar el destino de la mercancía"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Mercancía:</label>
    <asp:TextBox id="txt_mercancia" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_mercancia" ControlToValidate="txt_mercancia" ErrorMessage="Es necesario capturar la descripción de la mercancía"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvTransporte" ControlToValidate="ddlTransporte" InitialValue="" ErrorMessage="Es necesario seleccionar un transporte"></asp:RequiredFieldValidator>
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
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cantidades</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="cantidadesNormal">
    <div>
        <label>Pallets:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pallet" runat="server"></asp:TextBox>
        <asp:RangeValidator runat="server" CssClass="validator" Type="Integer" ID="rv_no_pallet" ControlToValidate="txt_no_pallet" ErrorMessage="Es necesario capturar un número entre 0 y 1000" MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>
    </div>
    <div>
        <label>Bultos:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_bulto" runat="server" AutoPostBack="true" OnTextChanged="txt_no_bulto_txtChanged"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_no_bulto" ControlToValidate="txt_no_bulto" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" runat="server" CssClass="validator" ID="rv_no_bulto" ControlToValidate="txt_no_bulto" ErrorMessage="Es necesario capturar un número entre 1 y 10,000" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>
    <div>
        <label>Piezas:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pieza" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_no_pieza" ControlToValidate="txt_no_pieza" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" runat="server" CssClass="validator" ID="rv_no_pieza" ControlToValidate="txt_no_pieza" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>
    <div>
        <label>Peso x Bulto (Kg):</label>
        <asp:TextBox CssClass="txtNumber" ID="txt_peso_unitario" runat="server" AutoPostBack="true" OnTextChanged="txt_peso_unitario_txtChanged"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_peso_unitario" ControlToValidate="txt_peso_unitario" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Double" runat="server" CssClass="validator" ID="rv_peso_unitario" ControlToValidate="txt_peso_unitario" ErrorMessage="Es necesario capturar un número entre 1 y 500" MinimumValue="1" MaximumValue="500"></asp:RangeValidator>
    </div>
    <div>
        <label>Total de Carga (Kg):</label>
        <asp:UpdatePanel runat="server" ID="upTotalCarga" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txt_peso_unitario" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txt_no_bulto" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTransporte" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div id="upCantTotalCarga">
                <asp:TextBox runat="server" ID="txt_total_carga" Text="0" Enabled="false"></asp:TextBox>
            </div>
            <asp:RangeValidator Type="Double" runat="server" CssClass="validator" ID="rv_total_carga_max" ControlToValidate="txt_total_carga" ErrorMessage="El peso excede el máximo de carga para la unidad seleccionada" MinimumValue="0" MaximumValue="50000"></asp:RangeValidator>
            <asp:RangeValidator Type="Double" runat="server" CssClass="validator" ID="rv_total_carga_min" ControlToValidate="txt_total_carga" ErrorMessage="Favor de proporcionar bultos y/o peso unitario" MinimumValue="1" MaximumValue="500000"></asp:RangeValidator>
        </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Tipo Salida</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="div_tipo_salida">
    <asp:UpdatePanel runat="server" ID="upChkSalida" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chk_tipo_salida" EventName="CheckedChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:CheckBox runat="server" ID="chk_tipo_salida" Text="Salida Única" AutoPostBack="true" Checked="true" OnCheckedChanged="chk_tipo_salida_checked" />
        <asp:RequiredFieldValidator CssClass="validator" runat="server" Enabled="false" ID="rfv_refCompartifda" ControlToValidate="txt_referencia" ErrorMessage="Es necesario capturar un pedimento para compartir la salida"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div id="div_no_salida">
        <asp:Label runat="server" ID="lbl_no_salida" Visible="false"></asp:Label>
    </div>
    <asp:CheckBox runat="server" ID="chk_ultima" Text="Última" Visible="false" />
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Revisi&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <label>Hora carga:</label>
    <asp:TextBox CssClass="horaPicker" id="txt_hora_carga" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_hora_carga" ControlToValidate="txt_hora_carga" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
    <span class="hidden error">Es necesario proporcionar una hora</span>
</div>
<div>
    <label>Vigilante:</label>
    <%--<asp:DropDownList runat="server" ID="ddlVigilante"></asp:DropDownList>--%>
    <asp:DropDownList runat="server" ID="ddlVigilante" ToolTip="Nombre del vigilante en turno que supervisa la descarga"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvVigilante" ControlToValidate="ddlVigilante" InitialValue="" ErrorMessage="Es necesario seleccionar un vigilante"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Observaciones:</label>
    <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" Columns="1" ID="txt_observaciones"></asp:TextBox>
</div>
</div>
</div>

<div>
    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_click" />
</div>
</div>

<div id="div_panel">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Panel</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
<label>Operador: </label>
<asp:Label runat="server" ID="lblUsrName"></asp:Label>
</div>
<hr />
<div>
<label>Salidas Compartidas Pendientes:</label>

<ul>
<asp:Repeater runat="server" ID="repFoliosPendientes" OnItemDataBound="repFoliosPendientes_ItemDataBound">
<ItemTemplate>
<li><span>Folio: </span><asp:Label ID="lblFolioCompartido" runat="server" Text='<%# Eval("Folio") %>'></asp:Label>
<ul class="ulCompartida">
<asp:Repeater runat="server" ID="repReferencias">
<ItemTemplate>
<li><asp:Button runat="server" ID="btnRefCompartido" CommandArgument='<%# Eval("Folio") %>' Text='<%# Eval("Referencia") %>' OnCommand="referenciaCompartido_click" CausesValidation="false" /></li>
</ItemTemplate>
</asp:Repeater>
</ul>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
<hr />
<div id="salidasParciales">
    <label>Salidas Parciales:</label>
    <ul id="ulParcial">
    <asp:Repeater runat="server" ID="repSalPar">
    <ItemTemplate>
    <li>
        <asp:Button runat="server" ID="btnSalPar" CommandArgument='<%# Eval("id_salida") %>' Text='<%# Eval("referencia") %>' OnCommand="btnSalPar_click" CausesValidation="false" />
        <asp:HiddenField runat="server" ID="hfNoSalida" Value='<%# Eval("no_salida") %>' />
    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>
<hr />
<div id="salidasCapturadas">
    <label>Salidas Capturadas Hoy:</label>
    <ul id="ulSalHoy">
    <asp:Repeater runat="server" ID="repSalHoy">
    <ItemTemplate>
    <li>
    <div>
        <asp:Button runat="server" ID="btnFolios" CommandArgument='<%# Eval("id_salida") %>' Text='<%# Eval("folio") %>' OnCommand="salidaHoy_click" CausesValidation="false" />
    </div>
    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>
<hr />
</div>
</div>

</asp:Content>