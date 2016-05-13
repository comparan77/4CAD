<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmTraficoWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmTraficoWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../../js/operation/almacen/frmTraficoWH.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos de la cita</h3>
<div id="div-solicitud-trafico" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Fecha de la Solicitud:</label>
    <asp:TextBox CssClass="cleanAfterApply" runat="server" ID="txt_fecha_solicitud"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_solicitud" ControlToValidate="txt_fecha_solicitud" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha de carga solicitada:</label>
    <asp:TextBox CssClass="cleanAfterApply" runat="server" ID="txt_fecha_carga_solicitada"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_carga_solicitada" ControlToValidate="txt_fecha_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Hora de carga solicitada:</label>
    <asp:TextBox runat="server" ID="txt_hora_carga_solicitada" CssClass="horaPicker cleanAfterApply"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_hora_carga_solicitada" ControlToValidate="txt_hora_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>

<hr style="border-color: transparent;" />

<div>
    <label>Folio de la Cita:</label>
    <asp:TextBox runat="server" ID="txt_folio_cita"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_folio_cita" ControlToValidate="txt_folio_cita" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>

<div>
    <label>Fecha de la cita:</label>
    <asp:TextBox CssClass="cleanAfterApply" runat="server" ID="txt_fecha_cita"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_cita" ControlToValidate="txt_fecha_cita" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Hora de la cita:</label>
    <asp:TextBox runat="server" ID="txt_hora_cita" CssClass="horaPicker cleanAfterApply"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_hora_cita" ControlToValidate="txt_hora_cita" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>

</div>

<h3 id="H1" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos del Transporte</h3>
<div id="div1" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Operador:</label>
    <asp:TextBox runat="server" ID="txt_operador" CssClass="txtLarge"></asp:TextBox>
    <%--<asp:RequiredFieldValidator runat="server" ID="rfv_opeardor" ControlToValidate="txt_operador" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>--%>
</div>    
    
<div>
    <label>Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvTransporte" ControlToValidate="ddlTransporte" InitialValue="" ErrorMessage="Es necesario seleccionar un transporte"></asp:RequiredFieldValidator>
</div>
        
    <asp:UpdatePanel runat="server" ID="upTipoTransporte" UpdateMode="Conditional">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTransporte" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <label>Tipo de Transporte:</label>        
        <asp:DropDownList runat="server" ID="ddlTipo_Transporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_Transporte_changed"></asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_tipo_transporte" InitialValue="" ControlToValidate="ddlTipo_Transporte" ErrorMessage="Es necesario seleccionar un tipo de transporte"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>

    <div class="hidden">
    <asp:UpdatePanel runat="server" ID="upDatosVehiculo" UpdateMode="Conditional">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
    <div>
        <label>Placa:</label>
        <asp:TextBox id="txt_placa" runat="server" MaxLength="50"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_placa" ControlToValidate="txt_placa" ErrorMessage="Es necesario capturar la placa para este tipo de vehículo"></asp:RequiredFieldValidator>--%>
    </div>
    <div>
        <label>Caja:</label>
        <asp:TextBox id="txt_caja" runat="server" MaxLength="50"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja" ControlToValidate="txt_caja" ErrorMessage="Es necesario capturar la placa de la caja para este tipo de vehículo"></asp:RequiredFieldValidator>--%>
    </div>
    <div>
        <label>Contenedor 1:</label>
        <asp:TextBox id="txt_caja_1" runat="server" MaxLength="50"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_1" ControlToValidate="txt_caja_1" ErrorMessage="Es necesario capturar el número de contenedor 1 para este tipo de vehículo"></asp:RequiredFieldValidator>--%>
    </div>
    <div>
        <label>Contenedor 2:</label>
        <asp:TextBox id="txt_caja_2" runat="server" MaxLength="50"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_2" ControlToValidate="txt_caja_2" ErrorMessage="Es necesario capturar el número de contenedor 2 para este tipo de vehículo"></asp:RequiredFieldValidator>--%>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

<div>
    <label>Tipo de carga:</label>
    <asp:DropDownList runat="server" CssClass="txtMedium" ID="ddlTipoCarga"></asp:DropDownList>
</div>

<div>
    <label>Destino:</label>
    <asp:DropDownList CssClass="txtMedium" runat="server" ID="ddlDestino"></asp:DropDownList>
</div>

<div>
    <asp:Button runat="server" ID="btn_guardar_trafico" Text="Guardar" OnClick="guardar_trafico" />
</div>

</div>


</asp:Content>
