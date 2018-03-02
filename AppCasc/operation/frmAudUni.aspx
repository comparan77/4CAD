<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmAudUni.aspx.cs" Inherits="AppCasc.operation.frmAudUni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/operation/frmAudUni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Bodega</h3>
<div id="div2" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>Bodega:</label>
        <asp:DropDownList runat="server" ID="ddlBodega" OnSelectedIndexChanged="ddlBodega_changed" AutoPostBack="true"></asp:DropDownList>
    </div>
    <asp:UpdatePanel runat="server" ID="up_prev_pred_user" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlBodega" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hf_usr_prv_perd" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Destino</h3>
<div id="div_transporte" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    
    <div>
        <label>C.P.:</label>
        <asp:TextBox runat="server" ID="txt_cp"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_txt_cp" ControlToValidate="txt_cp" ErrorMessage="Es necesario proporcionar el codigo postal"></asp:RequiredFieldValidator>
        <button id="btn_valid_cp">Validar c&oacute;digo postal</button>
    </div>

    <div>
        <label>Calle y n&uacute;mero</label>
        <asp:TextBox runat="server" ID="txt_calle_num"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_calle_num" ControlToValidate="txt_calle_num" ErrorMessage="Es necesario proporcionar la calle y número"></asp:RequiredFieldValidator>        
    </div>

    <div>
        <label>Estado</label>
        <asp:TextBox runat="server" ID="txt_estado"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_estado" ControlToValidate="txt_estado" ErrorMessage="Es necesario proporcionar el estado"></asp:RequiredFieldValidator>        
    </div>

    <div>
        <label>Municipio o Del.</label>
        <asp:TextBox runat="server" ID="txt_municipio"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_municipio" ControlToValidate="txt_municipio" ErrorMessage="Es necesario proporcionar el municipio o delegación"></asp:RequiredFieldValidator>        
    </div>

    <div id="div_colonia" title="Colonias">
        <select id="ddl_colonia"></select>
    </div>

    <div>
        <label>Colonia</label>
        <asp:TextBox runat="server" ID="txt_colonia"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_colonia" ControlToValidate="txt_colonia" ErrorMessage="Es necesario proporcionar la colonia"></asp:RequiredFieldValidator>
    </div>

</div>

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Transporte</h3>
<div id="div1" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

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
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel runat="server" ID="upDatosVehiculo" UpdateMode="Conditional">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
    <asp:HiddenField runat="server" ID="hf_cond_trans" />
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
        <label>Operador:</label>
        <asp:TextBox runat="server" ID="txt_operador"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador"></asp:RequiredFieldValidator>
    </div>

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

    <div id="div_otra_razon" class="hidden">
        <label>En caso de que exista otra raz&oacute;n para no autorizar la carga, favor de indicarla:</label>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_otra_razon"></asp:TextBox>
    </div>

</div>

<div>
    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_click" />
    <asp:HiddenField runat="server" ID="hf_click_save" />
</div>

<div id="div_prev_perdidas" title="Código prevención de pérdidas">
    <div class="divForm">
        <div>
            <label>Autoriza</label>
            <select id="ddl_autoriza" class="txtMedium"></select>
        </div>
    </div>
    <div id="div_pwd_per">
    </div>
    
</div>

</asp:Content>
