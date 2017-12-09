<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmCliente.aspx.cs" Inherits="AppCasc.catalog.frmCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/catalog/frm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cliente</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>RFC:</label>
        <asp:TextBox runat="server" ID="txt_rfc"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvRfc" ControlToValidate="txt_rfc" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Raz&oacute;n:</label>
        <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_razon"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvRazon" ControlToValidate="txt_razon" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Documentos Requeridos:</label>
        <asp:CheckBoxList runat="server" ID="chkbxlstDocumento"></asp:CheckBoxList>
    </div>
    <div>
        <label>Documento Principal:</label>
        <asp:DropDownList runat="server" ID="ddlDocPrincipal"></asp:DropDownList>
    </div>
    <div>
        <label>Tipo de Cuenta:</label>
        <asp:DropDownList runat="server" ID="ddlCuentaTipo"></asp:DropDownList>
    </div>
    <div>
        <label>Grupo:</label>
        <asp:DropDownList runat="server" ID="ddlGrupo"></asp:DropDownList>
    </div>
    <hr />
    <div>
        <label>Impresiones por Operaci&oacute;n</label>
        <asp:DropDownList runat="server" ID="ddlOperacion" AutoPostBack="true" OnSelectedIndexChanged="changeOperacion">
            <asp:ListItem Value="1" Text="Arribo"></asp:ListItem>
            <asp:ListItem Value="2" Text="Embarque"></asp:ListItem>
        </asp:DropDownList>
        <asp:UpdatePanel runat="server" ID="upCopias" UpdateMode="Conditional">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlOperacion" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:CheckBoxList runat="server" ID="lstCopias" OnSelectedIndexChanged="selectCopie" AutoPostBack="true"></asp:CheckBoxList>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <hr />
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" OnClick="btnCancel_click" CausesValidation="false" />
    </div>
</div>
</div>
</asp:Content>
