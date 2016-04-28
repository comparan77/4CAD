<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmArriboWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmArriboWH" %>
<%@ Register src="../../webControls/usrControlClienteMercancia.ascx" tagname="usrControlClienteMercancia" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
<link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
<link href="../../css/jquery.combobox.css" rel="stylesheet" type="text/css" />
    
<script src="../../js/jquery.combobox.js" type="text/javascript"></script>
<script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
<script src="../../js/jquery.maskedinput.min.js" type="text/javascript"></script>
<script src="../../js/webControls/ctrlClienteMercancia.js?v1.1.150619_1446" type="text/javascript"></script>
<script src="../../js/operation/almacen/frmArriboWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos Generales</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>Bodega:</label>
        <asp:DropDownList runat="server" ID="ddlBodega" AutoPostBack="true" OnSelectedIndexChanged="changeBodega" CausesValidation="false"></asp:DropDownList>
    </div>
    <asp:UpdatePanel runat="server" ID="up_bodega" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlBodega" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <label>Cortina:</label>
        <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <label>Fecha del Arribo:</label>
        <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Hora del Arribo:</label>
        <asp:TextBox id="txt_hora_llegada" CssClass="horaPicker" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvHora_llegada" ControlToValidate="txt_hora_llegada" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
        <span class="hidden error">Es necesario proporcionar una hora.</span>
    </div>
    <div>
        <label>C&oacute;digo RR</label>
        <asp:TextBox runat="server" ID="txt_rr"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_codigo_rr" ControlToValidate="txt_rr" ErrorMessage="Es necesario proporcionar el código RR." ></asp:RequiredFieldValidator>
    </div>
</div>

<h3 id="H1" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Informaci&oacute;n de la Mercanc&iacute;a</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>C&oacute;digo de la mercanc&iacute;a:</label>        
        <asp:TextBox runat="server" ID="txt_mercancia_codigo"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_mercancia_codigo" ControlToValidate="txt_mercancia_codigo" ErrorMessage="Es necesario proporcionar el código de de la Mercancía." ></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Descripci&oacute;n:</label>
        <asp:TextBox runat="server" ID="txt_mercancia_desc" CssClass="txtLarge"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_mercancia_descripcion" ControlToValidate="txt_mercancia_desc" ErrorMessage="Es necesario proporcionar el código de de la Mercancía." ></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Negocio:</label>
        <asp:TextBox runat="server" ID="txt_negocio" CssClass="txtSmall" ReadOnly="true"></asp:TextBox>
        <asp:RegularExpressionValidator runat="server" ID="rgv_negocio" ControlToValidate="txt_negocio" ValidationExpression="^(PT|PR)$" ErrorMessage="El negocio sólo puede ser PR o PT"></asp:RegularExpressionValidator>
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hf_vendor" />
        <label>Proveedor:</label>
        <asp:TextBox runat="server" ID="txt_proveedor" CssClass="txtLarge"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_proveedor" ControlToValidate="txt_proveedor" ErrorMessage="Es necesario proporcionar el proveedor la Mercancía." ></asp:RequiredFieldValidator>
    </div>
    <hr style="border-color: transparent" />
    <div>
        <label>Pallets Armados:</label>
        <asp:TextBox CssClass="txtNumber" id="txt_no_pallet" runat="server" ToolTip="Número de pallets o tarimas"></asp:TextBox>
        <asp:RangeValidator CssClass="validator" runat="server" Type="Integer" ID="rv_no_pallet" ControlToValidate="txt_no_pallet" ErrorMessage="Es necesario capturar un número." MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>
    </div>
    <hr style="border-color: transparent" />
    <div>
        <label>Cajas Declaradas:</label>
        <asp:TextBox CssClass="txtNumber calculaDif confirmValue" id="txt_no_bulto_declarado" runat="server" ToolTip="Número de Bultos reportados en los documentos." Text="0"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_bulto_declarado" ControlToValidate="txt_no_bulto_declarado" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_declarado" ControlToValidate="txt_no_bulto_declarado" ErrorMessage="Es necesario capturar un número." MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>
    <div>
        <label>Piezas Declaradas:</label>
        <asp:TextBox CssClass="txtNumber calculaDif confirmValue" id="txt_no_pieza_declarada" runat="server" ToolTip="Número de piezas reportados en los documentos." Text="0"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un número." MinimumValue="1" MaximumValue="1000000"></asp:RangeValidator>
    </div>
    
    <hr style="border-color: transparent" />
    <div>
        <label>Cajas Recibidas:</label>
        <asp:TextBox CssClass="txtNumber calculaDif confirmValue" id="txt_no_bulto_recibido" runat="server" ToolTip="Conteo del número de los bultos en la descarga." Text="0"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_recibido" ControlToValidate="txt_no_bulto_recibido" ErrorMessage="Es necesario capturar un número." MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
    </div>
    <%--<div>
        <label>Piezas Declaradas:</label>
        <asp:TextBox CssClass="txtNumber calculaDif confirmValue" id="txt_no_pieza_declarada" runat="server" ToolTip="Número de Piezas declaradas en los documentos." Text="0"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>
    <div>
        <label>Piezas Recibidas:</label>
        <asp:TextBox CssClass="txtNumber calculaDif confirmValue" id="txt_no_pieza_recibida" runat="server" ToolTip="Número de Piezas recibidas." Text="0"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_recibida" ControlToValidate="txt_no_pieza_recibida" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>--%>
    <hr style="border-color: transparent" />
    <div>
        <label>Piezas por Caja:</label>
        <asp:TextBox Text="0" CssClass="txtNumber calculaStd confirmValue" id="txt_pza_x_bulto" runat="server" ToolTip="Número de Piezas por bulto."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_pza_x_bulto" ControlToValidate="txt_pza_x_bulto" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_pza_x_bulto" ControlToValidate="txt_pza_x_bulto" ErrorMessage="Es necesario capturar un número." MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>
    <div>
        <label>Cajas por Tarima:</label>
        <asp:TextBox Text="0" CssClass="txtNumber calculaStd confirmValue" id="txt_bto_x_pallet" runat="server" ToolTip="Número de bultos por pallet."></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_bto_x_pallet" ControlToValidate="txt_bto_x_pallet" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
        <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_bto_x_pallet" ControlToValidate="txt_bto_x_pallet" ErrorMessage="Es necesario capturar un número." MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
    </div>

    <div style="position: absolute; top: 15px; right: 15px;">
        <table id="tbl_dif" class="grdCascSmall">
            <thead>
                <tr>
                    <th>&nbsp;</th>
                    <th>Faltantes</th>
                    <th>Sobrantes</th>
                </tr>
            </thead>
            <tbody>
                <tr id="tr_pza_dif">
                    <td>Pza</td>
                    <td align="center"><span id="pza_faltante">0</span></td>
                    <td align="center"><span id="pza_sobrante">0</span></td>
                </tr>
                <tr id="tr_bto_dif">
                    <td>Cja</td>
                    <td align="center"><span id="bto_faltante">0</span></td>
                    <td align="center"><span id="bto_sobrante">0</span></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div id="div_build_tarima" style="position: absolute; top: 160px; right: 15px">
        <table class="grdCascSmall">
            <tbody id="tbody_build_tarima">
            </tbody>
        </table>
    </div>
    <hr style="border-color: transparent" />
    <div>
        <button id="btn_restos">Restos</button>
    </div>
    <div id="div_restos" title="Captura de restos por tarima" class="divForm">
        <div>
            <label>Cajas:</label>
            <input type="text" class="txtNumber" id="caja_resto" value="0" />
        </div>
        <div>
            <label>Piezas por Caja:</label>
            <input type="text" class="txtNumber" id="piezaXcaja_resto" value="0" />
        </div>
        <div>
            <button id="addResto">Agregar Resto a la tarima</button>
        </div>
        <hr style="border-color: transparent" />
        <table class="grdCascSmall">
            <thead>
                <tr>
                    <th>Cajas</th>
                    <th>Piezas X Caja</th>
                    <th>Piezas</th>
                    <th><span class="ui-icon ui-icon-trash"></span></th>
                </tr>
            </thead>
            <tbody id="t_resto">
            </tbody>
        </table>
        <hr style="border-color: transparent;" />
        <div>
            <button id="addTarima_resto">Agregar Tarima</button>
            <asp:HiddenField runat="server" ID="hf_resto" />
        </div>
    </div>
    <div id="div_resto_tarima" style="position: absolute; bottom: 60px; right: 15px">
        <table class="grdCascSmall">
            <tbody id="tbody_resto_tarima"></tbody>
        </table>
        <asp:HiddenField runat="server" ID="hf_restos" />
    </div>
    <hr style="border-color: transparent" />
    <div>
        <button id="btn_show_cantidadesProblema">Bultos con Incidencias</button>
    </div>

    <div class="hidden" id="cantidadesProblema">
        <div>
            <label>Cajas Dañadas:</label>
            <asp:TextBox Text="0" CssClass="txtNumber" id="txt_no_bulto_danado" runat="server" ToolTip="Numero de bultos encontrados con daños físicos."></asp:TextBox>
            <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_danado" ControlToValidate="txt_no_bulto_danado" ErrorMessage="Es necesario capturar un número entre 0 y 10,000" MinimumValue="0" MaximumValue="10000"></asp:RangeValidator>
        </div>
        <div>
            <label>Cajas Abiertas:</label>
            <asp:TextBox Text="0" CssClass="txtNumber" id="txt_no_bulto_abierto" runat="server" ToolTip="Numero de bultos encontrados abiertos "></asp:TextBox>
            <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_bulto_abierto" ControlToValidate="txt_no_bulto_abierto" ErrorMessage="Es necesario capturar un número entre 0 y 10,000" MinimumValue="0" MaximumValue="10000"></asp:RangeValidator>
        </div>
    </div>

</div>

<h3 id="H2" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Informaci&oacute;n del transporte</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    
    <div style="margin-bottom: 15px">
        <label>Folio Cita:</label>
        <asp:TextBox runat="server" ID="txt_folio_cita_transporte"></asp:TextBox>
    </div>

    <asp:Panel runat="server" ID="pnl_addTransportes" Visible="true">
    <table class="grdCascSmall">
        <thead>
            <tr>
                <th>L&iacute;nea:</th>
                <th>Tipo de Transporte:</th>
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
    <span style="color: Red; visibility: hidden;" class="validator" id="rfv_entradaTransporte">Es necesario agregar al menos un transporte.</span>
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
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCustodia" ControlToValidate="ddlCustodia" InitialValue="" ErrorMessage="Es necesario seleccionar una custodia"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Operador:</label>
        <asp:TextBox CssClass="txtLarge" runat="server" ID="txt_operador" ToolTip="Nombre del operador de la unidad transportista"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador"></asp:RequiredFieldValidator>
    </div>
    <hr style="border-color: transparent" />
    <div style="margin-left: 205px; margin-top: 10px;">
    <asp:HiddenField runat="server" ID="hf_condiciones_transporte" />
    <span style="color: Red; visibility: hidden;" class="validator" id="rfv_condiciones_transporte">Es necesario proporcionar las condiciones del transporte.</span>
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

<div>
    <asp:Button runat="server" ID="btn_save" Text="Guardar Entrada" OnClick="save_entrada" />
</div>

<div>
    <uc1:usrControlClienteMercancia ID="usrControlClienteMercancia1" runat="server" />
</div>

</asp:Content>
