<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmEntradas.aspx.cs" Inherits="AppCasc.operation.frmEntradas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmEntradas.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<div id="div_entrada">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />
<asp:HiddenField runat="server" ID="hfEsCompartida" />
<asp:HiddenField runat="server" ID="hfFolio" />
<asp:HiddenField runat="server" ID="hflstDocument" />
<asp:HiddenField runat="server" ID="hfTipoEntrada" Value="nueva" />

<!-- Hiden inputs to validate operation <<ini>> -->
<asp:HiddenField runat="server" ID="hf_clienteDocumento" />
<asp:HiddenField runat="server" ID="hf_documentos" />
<!-- Hiden inputs to validate operation <<fin>> -->

<div id="dialog-confirm" title="Datos de entrada">
    <input type="hidden" id="hf-confirmado" value="0" />
    <p id="p_aviso_registro_salida">
    <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
    <span id="spn-aviso-registro"></span>
    </p>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Arribo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlBodega_changed"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvBodega" ControlToValidate="ddlBodega" InitialValue="" ErrorMessage="Es necesario seleccionar una bodega"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha:</label>
    <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
</div>
<div>
    <label>Hora de llegada:</label>
    <asp:TextBox id="txt_hora_llegada" CssClass="horaPicker" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvHora_llegada" ControlToValidate="txt_hora_llegada" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
    <span class="hidden error">Es necesario proporcionar una hora</span>
</div>
<div>
    <label>Cortina:</label>
    <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" OnSelectedIndexChanged="ddlCliente_changed" AutoPostBack="true"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCliente" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Es necesario seleccionar un cliente"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Tipo de Carga:</label>
    <asp:DropDownList runat="server" ID="ddlTipoCarga"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvTipoCarga" ControlToValidate="ddlTipoCarga" InitialValue="" ErrorMessage="Es necesario seleccionar un tipo de carga"></asp:RequiredFieldValidator>
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

<asp:UpdatePanel runat="server" ID="up_documento_recibido" UpdateMode="Conditional">
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnAdd_documento" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="btnRem_documento" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
</Triggers>
<ContentTemplate>
<div>
    <label>Ref del Documento Recibido:</label>
    <asp:TextBox runat="server" ID="txt_referencia_documento"></asp:TextBox>
    <asp:Button runat="server" ID="btnAdd_documento" Text="Agregar" CausesValidation="false" OnClick="btnAdd_documento_click" />
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upDocReq">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
    </ContentTemplate>
    </asp:UpdatePanel>    
</div>
<div>
    <label>Documentos Capturados</label>
    <asp:ListBox runat="server" CssClass="documentoRecibido" ID="lst_documento_recibido" ></asp:ListBox>   
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
                <asp:Button runat="server" ID="btnAdd_pedimento" CausesValidation="false" Text="Agregar" OnClick="btnAdd_pedimento_click" />
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
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Mercanc&iacute;a</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <label>Aduana de Origen:</label>
    <asp:TextBox id="txt_origen" CssClass="txtLarge" runat="server" ToolTip="Anotar el nombre de la Aduana de origen"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_origen" ControlToValidate="txt_origen" ErrorMessage="Es necesario capturar la aduana de origen"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Mercancía:</label>
    <asp:TextBox id="txt_mercancia" runat="server" TextMode="MultiLine" Rows="4" ToolTip="Breve descripción de la mercancía que llega"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_mercancia" ControlToValidate="txt_mercancia" ErrorMessage="Es necesario capturar la descripción de la mercancía"></asp:RequiredFieldValidator>
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Transporte</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<asp:UpdatePanel runat="server" ID="upDatosVehiculo" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="btnAddTransporte" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnRem_transporte" EventName="Click" />
</Triggers>
<ContentTemplate>

<div>
    <label>Línea de Transporte:</label>
    <asp:TextBox runat="server" ID="txt_lineaTransporte"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validatorTransporte" ValidationGroup="vgTransporte" runat="server" ID="rfvLineaTransporte" ControlToValidate="txt_lineaTransporte" ErrorMessage="Es necesario capturar la linea de transporte"></asp:RequiredFieldValidator>
</div>

<div>
    <label>Tipo de Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTipo_Transporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_Transporte_changed"></asp:DropDownList>
</div>

<div>
    <label>Placa:</label>
    <asp:TextBox id="txt_placa" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validatorTransporte" ValidationGroup="vgTransporte" runat="server" ID="rfv_placa" ControlToValidate="txt_placa" ErrorMessage="Es necesario capturar la placa para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Contenedor 1:</label>
    <asp:TextBox id="txt_caja_1" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validatorTransporte" ValidationGroup="vgTransporte" runat="server" ID="rfv_caja_1" ControlToValidate="txt_caja_1" ErrorMessage="Es necesario capturar la placa de la caja 1 para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Contenedor 2:</label>
    <asp:TextBox id="txt_caja_2" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validatorTransporte" ValidationGroup="vgTransporte" runat="server" ID="rfv_caja_2" ControlToValidate="txt_caja_2" ErrorMessage="Es necesario capturar la placa de la caja 2 para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <asp:Button runat="server" ID="btnAddTransporte" ValidationGroup="vgTransporte" Text="Agregar Transporte" OnClick="btnAddTransporte_click" />
</div>

<div>
    <label>Transporte(s):</label>
    <asp:ListBox runat="server" ID="lstTransportes" Width="400" CssClass="transporteAgregado"></asp:ListBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvlstTransportes" ControlToValidate="lstTransportes" InitialValue="" ErrorMessage="Es necesario agregar por lo menos un transporte"></asp:RequiredFieldValidator>
</div>

<div id="div_btnRemTransporte" class="hidden">
    <asp:Button runat="server" ID="btnRem_transporte" Text="Eliminar Transporte" CausesValidation="false" OnClick="btnRem_transporte_click" />
</div>

</ContentTemplate>
</asp:UpdatePanel>
<hr />
<div>
    <label>Sello/Candado:</label>
    <asp:TextBox id="txt_sello" runat="server" ToolTip="En caso de no contar con sello realizar un acta informativa"></asp:TextBox>
</div>
<div>
    <label>Carta Porte:</label>
    <asp:TextBox id="txt_talon" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCartaPorte" ControlToValidate="txt_talon" ErrorMessage="Es necesario capturar la carta porte"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Custodia:</label>
    <asp:DropDownList runat="server" ID="ddlCustodia"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCustodia" ControlToValidate="ddlCustodia" InitialValue="" ErrorMessage="Es necesario seleccionar una custodia"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Operador:</label>
    <asp:TextBox runat="server" ID="txt_operador" ToolTip="Nombre del operador de la unidad transportista"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador de la custodia"></asp:RequiredFieldValidator>
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cantidades</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="cantidadesNormal">
    <div>
        <label>Pallets Armados:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pallet" runat="server" ToolTip="Número de pallets reportados en la descarga de mercancía"></asp:TextBox>
        <asp:RangeValidator CssClass="validator" runat="server" Type="Integer" ID="rv_no_pallet" ControlToValidate="txt_no_pallet" ErrorMessage="Es necesario capturar un número entre 0 y 1000" MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>
    </div>
    <div>
        <label>Bultos Declarados:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_bulto_declarado" runat="server" ToolTip="Número de Bultos reportados en la descarga."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_bulto_declarado" ControlToValidate="txt_no_bulto_declarado" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_declarado" ControlToValidate="txt_no_bulto_declarado" ErrorMessage="Es necesario capturar un número entre 1 y 10,000" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>

    <div>
        <label>Bultos Recibidos:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_bulto_recibido" runat="server" ToolTip="Conteo del número de los bultos en la descarga."></asp:TextBox>
        <%--<button id="btn_bulto_declarado">Mismos Declarados</button>--%>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un número entre 1 y 10,000" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>

    <div>
        <label>Piezas Declaradas:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pieza_declarada" runat="server" ToolTip="Número de Piezas declaradas en el pedimento."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>

    <div>
        <label>Piezas Recibidas:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pieza_recibida" runat="server" ToolTip="Número de Piezas recibidas en el pedimento."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>

</div>

<div>
    <button id="btn_show_cantidadesProblema">Bultos Dañados</button>
</div>

<div class="hidden" id="cantidadesProblema">
    <div>
        <label>Bultos Dañados:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_bulto_danado" runat="server" ToolTip="Numero de bultos encontrados con daños físicos."></asp:TextBox>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_danado" ControlToValidate="txt_no_bulto_danado" ErrorMessage="Es necesario capturar un número entre 0 y 10,000" MinimumValue="0" MaximumValue="10000"></asp:RangeValidator>
    </div>
    <div>
        <label>Bultos Abiertos:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_bulto_abierto" runat="server" ToolTip="Numero de bultos encontrados abiertos "></asp:TextBox>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_abierto" ControlToValidate="txt_no_bulto_abierto" ErrorMessage="Es necesario capturar un número entre 0 y 10,000" MinimumValue="0" MaximumValue="10000"></asp:RangeValidator>
    </div>
</div>

</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Tipo Entrada</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="div_tipo_entrada">
    <asp:UpdatePanel runat="server" ID="upChkEntrada" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chk_tipo_entrada" EventName="CheckedChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:CheckBox runat="server" ID="chk_tipo_entrada" Text="Entrada Única" AutoPostBack="true" Checked="true" OnCheckedChanged="chk_tipo_entrada_checked" />
        <asp:RequiredFieldValidator CssClass="validator" runat="server" Enabled="false" ID="rfv_refEntrada" ControlToValidate="txt_referencia" ErrorMessage="Es necesario capturar una referencia para compartir una entrada"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div id="div_no_entrada">
        <asp:Label runat="server" ID="lbl_no_entrada" Visible="false"></asp:Label>
    </div>
    <asp:CheckBox runat="server" ID="chk_ultima" Text="Última" Visible="false" />
</div>
</div>
</div>

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Revisi&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div>
    <label>Hora descarga:</label>
    <asp:TextBox CssClass="horaPicker" id="txt_hora_descarga" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvHoraDescarga" ControlToValidate="txt_hora_descarga" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
    <span class="hidden error">Es necesario proporcionar una hora</span>
</div>
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
<label>Entradas Compartidas Pendientes</label>

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
<div id="entradasParciales">
    <label>Entradas Parciales Pendientes</label>
    <ul id="ulParcial">
    <asp:Repeater runat="server" ID="repEntPar">
    <ItemTemplate>
    <li>
        <asp:Button runat="server" ID="btnEntPar" CommandArgument='<%# Eval("id_entrada") %>' Text='<%# Eval("referencia") %>' OnCommand="btnEntPar_click" CausesValidation="false" />
        <asp:HiddenField runat="server" ID="hfNoEntrada" Value='<%# Eval("no_entrada") %>' />
    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>
<hr />
<div id="entradasCapturadas">
    <div id="divPnlActions" title="Acciones">
        <button id="btnPrint">Imprimir</button>
        <button id="btnDlt">Eliminar</button>
    </div>
    <label>Entradas Capturadas Hoy</label>
    <ul id="ulEntHoy">
    <asp:Repeater runat="server" ID="repEntHoy">
    <ItemTemplate>
    <li>
    <div>
        <asp:Button runat="server" ID="btnFolios" CommandArgument='<%# Eval("id_entrada") %>' Text='<%# Eval("folio") %>' OnCommand="entradaHoy_click" CausesValidation="false" />
    </div>
    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>
<hr />
</div>
</div>

<div id="save-cache-info" class="hidden">
<asp:Button runat="server" ID="btnSaveCache" OnClick="btnSaveCache_click" CausesValidation="false" />
<asp:UpdatePanel runat="server" ID="up_SaveCache" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSaveCache" EventName="click" />
</Triggers>
<ContentTemplate>
<div></div>
</ContentTemplate>
</asp:UpdatePanel>

</div>

</asp:Content>