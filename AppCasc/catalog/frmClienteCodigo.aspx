<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmClienteCodigo.aspx.cs" Inherits="AppCasc.catalog.frmClienteCodigo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">C&oacute;digo para el cliente&nbsp;<asp:HyperLink runat="server" ID="lnkCliente"></asp:HyperLink></h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Clave:</label>
        <asp:TextBox runat="server" ID="txt_clave" MaxLength="5"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvClave" ControlToValidate="txt_clave" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>D&iacute;gitos:</label>
        <asp:TextBox runat="server" CssClass="txtNumber" ID="txt_digito"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvDigito" ControlToValidate="txt_digito" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator runat="server" ID="rvDigito" ControlToValidate="txt_digito" ErrorMessage="Campo numérico" MinimumValue="1" MaximumValue="10" Type="Integer"></asp:RangeValidator>
    </div>
    <div>
        <label>Consecutivo arribo:</label>
        <asp:TextBox runat="server" CssClass="txtNumber" ID="txt_consec_arribo"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvConsec_arribo" ControlToValidate="txt_consec_arribo" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator runat="server" ID="rvConsec_arribo" ControlToValidate="txt_consec_arribo" ErrorMessage="Campo numérico" MinimumValue="1" MaximumValue="1000000" Type="Integer"></asp:RangeValidator>
    </div>
    <div>
        <label>Año actual:</label>
        <asp:TextBox runat="server" CssClass="txtNumber" ID="txt_anio"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvAnio" ControlToValidate="txt_anio" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator runat="server" ID="rvAnio" ControlToValidate="txt_anio" ErrorMessage="Campo numérico" MinimumValue="2014" MaximumValue="2014" Type="Integer"></asp:RangeValidator>
    </div>
    <div>
        <label>C&oacute;digo en salida:</label>
        <asp:CheckBox runat="server" ID="chk_dif_codigo" />
    </div>
    <div>
        <label>Consecutivo enbarque:</label>
        <asp:TextBox runat="server" CssClass="txtNumber" ID="txt_consec_embarque"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvConsec_embarque" ControlToValidate="txt_consec_embarque" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator runat="server" ID="rvConsec_embarque" ControlToValidate="txt_consec_embarque" ErrorMessage="Campo numérico" MinimumValue="1" MaximumValue="1000000" Type="Integer"></asp:RangeValidator>
    </div>
    <hr />
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:HiddenField runat="server" ID="hfFkey" />
        <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" OnClick="btnCancel_click" CausesValidation="false" />
    </div>
</div>
</div>

</asp:Content>
