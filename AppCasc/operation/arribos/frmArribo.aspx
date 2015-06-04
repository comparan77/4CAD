<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmArribo.aspx.cs" Inherits="AppCasc.operation.arribos.frmArribo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />

    <script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../../js/operation/arribos/frmArribo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />
<asp:HiddenField runat="server" ID="hfFolio" />
<asp:HiddenField runat="server" ID="hf_CompPendiente" Value="0" />
<asp:HiddenField runat="server" ID="hf_Documentos" />
<asp:HiddenField runat="server" ID="hf_id_usuario" />
<asp:HiddenField runat="server" ID="hf_click_Compartida" Value="0" />
<asp:HiddenField runat="server" ID="hf_fondeoValido" Value="0" />
<asp:HiddenField runat="server" ID="hf_codigo_cliente" Value="0" />

<div id="dialog-confirm" title="Datos de entrada">
    <input type="hidden" id="hf-confirmado" value="0" />
    <p id="p_aviso_registro_salida">
    <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
    <span id="spn-aviso-registro"></span>
    </p>
</div>


<!-- Información del Arribo -->
<div id="pnl_infArribo">
<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Información del Arribo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Cliente:</label>
    <%--<asp:DropDownList runat="server" ID="ddlCliente"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCliente" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Es necesario seleccionar un cliente"></asp:RequiredFieldValidator>--%>
    <select id="ctl00_body_ddlCliente"></select>
    <asp:HiddenField runat="server" ID="hf_id_cliente" />
    <asp:HiddenField runat="server" ID="hf_clientes" />
    <asp:HiddenField runat="server" ID="hf_facturasAvon" />
</div>
<div>
    <label>Bodega:</label>
    <asp:TextBox runat="server" ID="txt_bodega" Enabled="false"></asp:TextBox>
</div>
<div>
    <label>Cortina:</label>
    <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
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
    <label>Tipo de Carga:</label>
    <asp:DropDownList runat="server" ID="ddlTipoCarga"></asp:DropDownList>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvTipoCarga" ControlToValidate="ddlTipoCarga" InitialValue="" ErrorMessage="Es necesario seleccionar un tipo de carga"></asp:RequiredFieldValidator>
</div>
<div id="div_doc_requerido">
    <label></label>
    <asp:TextBox runat="server" ID="txt_doc_req"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" ID="rfv_doc_req" runat="server" ControlToValidate="txt_doc_req" ErrorMessage="Es necesario"></asp:RequiredFieldValidator>
</div>
</div>
</div>

<asp:Panel runat="server" ID="pnl_busqueda" CssClass="hidden">
<h3 id="div-search" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Pedimento precapturado en fondeo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda">
    <label>No. Referencia:</label>
    <asp:TextBox runat="server" ID="txt_referencia"></asp:TextBox>
    <div>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" ValidationGroup="grp_busqueda" />
    </div>
</div>

</div>
</asp:Panel>

<asp:Panel runat="server" ID="pnl_compartidos"  CssClass="hidden">
<h3 style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Pedimentos compartidos pendientes</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

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
</asp:Panel>

<asp:Panel runat="server" ID="pnl_infoArribo"  CssClass="hidden">
<%--<div style="display: none;">
    <label>Pedimento:</label>
    <asp:TextBox runat="server" ID="txt_referencia" Enabled="false"></asp:TextBox>
</div>--%>

<!-- Documentos -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Documentos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<table class="tblItems center" border="0" cellpadding="2" cellspacing="0">
    <thead>
        <tr>
            <th>Tipo de Documento</th>
            <th>Referencias:</th>
        </tr>
        <tr>
            <th><select id="ddlDocumento"></select></th>
            <th><input type="text" id="txt_documento" /></th>
            <th><button id="btn_add_documento"><span class="ui-icon ui-icon-plus"></span> </button></th>
        </tr>
    </thead>
    <tbody id="tbody_documentos">
        
    </tbody>
</table>
<asp:HiddenField runat="server" ID="hf_entradaDocumento" />

</div>

<!-- Otros pedimentos -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Pedimentos Compartidos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
<asp:Panel runat="server" ID="pnl_addPedimentosCompartidos" Visible="true">
<table class="tblItems center" border="0" cellpadding="2" cellspacing="0">
    <thead>
        <tr>
            <th>Pedimentos Compartidos:</th>
        </tr>
        <tr>
            <th><input type="text" id="txt_pedimento_compartido" /></th>
            <th><button id="btn_add_compartido"><span class="ui-icon ui-icon-plus"></span> </button></th>
        </tr>
    </thead>
    <tbody id="tbody_pedimentos">
        
    </tbody>
</table>
<asp:HiddenField runat="server" ID="hf_arribo_compartido" />
</asp:Panel>
<asp:Panel runat="server" ID="pnl_getPedimentosCompartidos" Visible="false">
<asp:GridView runat="server" ID="grd_compartidos" AutoGenerateColumns="false" CssClass="grdCasc">
    <Columns>
    <asp:BoundField ItemStyle-CssClass="pedComp" ItemStyle-HorizontalAlign="Center" DataField="referencia" HeaderText="Pedimento(s) Compartido(s)" />
    </Columns>
</asp:GridView>
</asp:Panel>
</div>

<!-- Mercancia -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Mercanc&iacute;a</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Aduana de Origen:</label>
    <asp:TextBox id="txt_origen" Enabled="false" CssClass="txtLarge" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_mercancia" ErrorMessage="Es necesario capturar el origen"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Mercancía:</label>
    <asp:TextBox id="txt_mercancia" runat="server" TextMode="MultiLine" Rows="4" ToolTip="Breve descripción de la mercancía que llega"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_mercancia" ControlToValidate="txt_mercancia" ErrorMessage="Es necesario capturar la descripción de la mercancía"></asp:RequiredFieldValidator>
</div>

</div>

<!-- Transporte -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Transporte</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:Panel runat="server" ID="pnl_addTransportes" Visible="true">
<table class="tblItems" border="0" cellpadding="2" cellspacing="0">
    <thead>
        <tr>
            <th>L&iacute;nea:</th>
            <th>Tipo:</th>
            <th>Placa:</th>
            <th>Caja:</th>
            <th>Contenedor 1:</th>
            <th>Contenedor 2:</th>
        </tr>
        <tr>
            <th><input type="text" id="txt_linea" /></th>
            <th><asp:DropDownList runat="server" ID="ddlTipo_Transporte"></asp:DropDownList></th>
            <th><input class="txtSmall" type="text" id="txt_placa" /></th>
            <th><input class="txtSmall" type="text" id="txt_caja" /></th>
            <th><input class="txtSmall" type="text" id="txt_caja1" /></th>
            <th><input class="txtSmall" type="text" id="txt_caja2" /></th>
            <th><button id="btn_add_tipoTransporte"><span class="ui-icon ui-icon-plus"></span> </button></th>
        </tr>
    </thead>
    <tbody id="tbody_transporte">
        
    </tbody>
</table>
<asp:HiddenField runat="server" ID="hf_entradaTransporte" />
<span style="color: Red; visibility: hidden;" class="validator" id="rfv_entradaTransporte">Es necesario agregar al menos un transporte</span>
</asp:Panel>

<asp:Panel runat="server" ID="pnl_getTransportes" Visible="false">
<asp:GridView runat="server" ID="grd_transportes" AutoGenerateColumns="false" CssClass="grdCasc">
<Columns>
<asp:BoundField DataField="Transporte_linea" HeaderText="Línea" />
<asp:BoundField DataField="Transporte_tipo" HeaderText="Tipo" />
<asp:BoundField DataField="placa" HeaderText="Placa" />
<asp:BoundField DataField="caja" HeaderText="Caja" />
<asp:BoundField DataField="caja1" HeaderText="Contenedor 1" />
<asp:BoundField DataField="caja2" HeaderText="Contenedor 2" />
</Columns>
</asp:GridView>
</asp:Panel>

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
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador"></asp:RequiredFieldValidator>
</div>

</div>

<!-- Cantidades -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cantidades</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
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
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un número entre 1 y 10,000" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>

    <div>
        <label>Piezas Declaradas:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pieza_declarada" runat="server" ToolTip="Número de Piezas declaradas en el pedimento." Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>

    <asp:Panel runat="server" ID="pnl_cantParciales" Visible="false">
        <label>Piezas Por Recibir:</label>
        <asp:TextBox CssClass="txtNumber" runat="server" ID="txt_no_pieza_por_recibir" Enabled="false"></asp:TextBox>
    </asp:Panel>

    <div>
        <label>Piezas Recibidas:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pieza_recibida" runat="server" ToolTip="Número de Piezas recibidas en el pedimento."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>

</div>

<div>
    <button id="btn_show_cantidadesProblema">Bultos con Incidencias</button>
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

<!-- Tipo de entrada -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Tipo de Entrada</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
<div id="radioTipoEntrada">
    <asp:RadioButton CssClass="rbTipoEntrada" runat="server" ID="rb_unica" GroupName="tipoEntrada" Text="Única" Checked="true" />
    <asp:RadioButton CssClass="rbTipoEntrada" runat="server" ID="rb_parcial" GroupName="tipoEntrada" Text="Parcial" />
</div>
<div style="clear: both">
    <span id="spnTipoEntrada"></span>  
    <div id="divParcialidad" class="hidden">
        <hr />
        <span id="spnNoEntradaParcial"></span>
        <asp:HiddenField runat="server" ID="hf_no_entrada_parcial" Value="1" />
        <div style="height: 50px;">
        <asp:CheckBox Text="En caso de ser la ultima parcialidad, marca la casilla" runat="server" ID="chk_ultima" Visible="false" />
        </div>
    </div>
</div>
</div>

<!-- Revison -->
<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Revisi&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
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

<asp:Button runat="server" ID="btn_save" Text="Guardar" OnClick="save_arribo" />
</asp:Panel>

</asp:Content>
