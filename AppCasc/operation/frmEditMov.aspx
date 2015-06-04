<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmEditMov.aspx.cs" Inherits="AppCasc.operation.frmEditMov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmEditMov.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<fieldset>
<legend>Modificaciones</legend>
<div id="frmCatalog">
    <div>
        <label>Movimiento:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtMovimiento"></asp:TextBox>
    </div>
    <div>
        <label>Folio:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtFolio"></asp:TextBox>
    </div>
    <div>
        <label>Referencia:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtReferencia"></asp:TextBox>
    </div>
    <div>
        <label>Captur&oacute;:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtUsuario"></asp:TextBox>
    </div>
    <div>
        <label>Tipo:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtTipo"></asp:TextBox>
    </div>
</div>
<hr />

    <asp:Panel runat="server" ID="pnlEntrada" Visible="false">
    <div id="div_entrada">
        <fieldset>
        <legend>Entrada</legend>
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
        </fieldset>

        <fieldset>
        <legend>Mercancía</legend>
        <div>
            <label>Origen:</label>
            <asp:TextBox id="txt_origen" CssClass="txtLarge" runat="server" ToolTip="Anotar el nombre de origen de la Aduana"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_origen" ControlToValidate="txt_origen" ErrorMessage="Es necesario capturar el origen de la mercancía"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Mercancía:</label>
            <asp:TextBox id="txt_mercancia" runat="server" TextMode="MultiLine" Rows="4" ToolTip="Breve descripción de la mercancía que llega"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_mercancia" ControlToValidate="txt_mercancia" ErrorMessage="Es necesario capturar la descripción de la mercancía"></asp:RequiredFieldValidator>
        </div>
        </fieldset>

        <fieldset>
        <legend>Otros Doc</legend>
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
        </fieldset>

        <fieldset>
        <legend>Cantidades</legend>
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
                <asp:TextBox CssClass="txtNumber" id="txt_no_pieza_declarada" runat="server" ToolTip="Número de Piezas declaradas en el pedimento."></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un valor"></asp:RequiredFieldValidator>
                <asp:RangeValidator Type="Integer" CssClass="validator" runat="server" ID="rv_no_pieza_declarada" ControlToValidate="txt_no_pieza_declarada" ErrorMessage="Es necesario capturar un número entre 1 y 5,000,000" MinimumValue="1" MaximumValue="5000000"></asp:RangeValidator>
            </div>

        </div>
        <div id="cantidadesProblema">
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
        </fieldset>
        
        <fieldset>
        <legend>Revisión</legend>
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
        </fieldset>
    </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlSalida" Visible="false">
    <div id="div_salida">
        <fieldset>
        <legend>Salida</legend>
        <div>
            <label>Hora de Salida:</label>
            <asp:TextBox id="txt_hora_salida" CssClass="horaPicker" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvHora_salida" ControlToValidate="txt_hora_salida" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
            <span class="hidden error">Es necesario proporcionar una hora</span>
        </div>
        <div>
            <label>Cortina:</label>
            <asp:DropDownList runat="server" ID="ddlCortinaSalida"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvddlCortinaSalida" ControlToValidate="ddlCortinaSalida" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
        </div>
        </fieldset>

        <fieldset>
        <legend>Mercancía</legend>
        <div>
            <label>Destino:</label>
            <asp:TextBox id="txt_destino" CssClass="txtLarge" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_destino" ControlToValidate="txt_destino" ErrorMessage="Es necesario capturar el destino de la mercancía"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Mercancía:</label>
            <asp:TextBox id="txt_mercanciaSalida" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_mercanciaSalida" ControlToValidate="txt_mercanciaSalida" ErrorMessage="Es necesario capturar la descripción de la mercancía"></asp:RequiredFieldValidator>
        </div>
        </fieldset>

        <fieldset>
        <legend>Otros Doc</legend>

        <div>
            <label>Sello:</label>
            <asp:TextBox id="txt_selloSalida" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Carta Porte:</label>
            <asp:TextBox id="txt_talonSalida" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_talonSalida" ControlToValidate="txt_talonSalida" ErrorMessage="Es necesario capturar la carta porte"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Custodia:</label>
            <asp:DropDownList runat="server" ID="ddlCustodiaSalida"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_ddlCustodiaSalida" ControlToValidate="ddlCustodiaSalida" InitialValue="" ErrorMessage="Es necesario seleccionar una custodia"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Operador:</label>
            <asp:TextBox runat="server" ID="txt_operadorSalida"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_operadorSalida" ControlToValidate="txt_operadorSalida" ErrorMessage="Es necesario capturar el operador de la custodia"></asp:RequiredFieldValidator>
        </div>

        </fieldset>

        <fieldset>
        <legend>Cantidades</legend>
        <div id="cantidadesNormalSalida">
            <div>
                <label>Pallets:</label>
                <asp:TextBox CssClass="txtNumber" id="txt_no_palletSalida" runat="server"></asp:TextBox>
                <asp:RangeValidator runat="server" CssClass="validator" Type="Integer" ID="rv_no_palletSalida" ControlToValidate="txt_no_palletSalida" ErrorMessage="Es necesario capturar un número entre 0 y 1000" MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>
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
                <label>Total de Carga:</label>
                <asp:UpdatePanel runat="server" ID="upTotalCarga" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txt_peso_unitario" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txt_no_bulto" EventName="TextChanged" />
                    
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
        </fieldset>

        <fieldset>
        <legend>Revisión</legend>
        <div>
            <label>Hora carga:</label>
            <asp:TextBox CssClass="horaPicker" id="txt_hora_carga" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv_hora_carga" ControlToValidate="txt_hora_carga" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
            <span class="hidden error">Es necesario proporcionar una hora</span>
        </div>
        <div>
            <label>Vigilante:</label>
            <asp:TextBox CssClass="txtMedium" runat="server" ID="txt_vigilanteSalida"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_vigilanteSalida" ControlToValidate="txt_vigilanteSalida" ErrorMessage="Es necesario proporcionar vigilante" ></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Observaciones:</label>
            <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" Columns="1" ID="txt_observacionesSalida"></asp:TextBox>
        </div>
        </fieldset>

    </div>
    </asp:Panel>

<div style="clear: both"></div>
<hr />
    <div>   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnActFolio" Text="Modificar datos Capturados" OnClick="btnActFolio_click" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CausesValidation="false" OnClick="btnRegresar_click" />
    </div>

</fieldset>

</asp:Content>
