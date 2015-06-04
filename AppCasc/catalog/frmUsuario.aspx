<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="AppCasc.catalog.frmUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />--%>

    <%--<script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>--%>
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Usuario</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

<div id="frmCatalog" class="divForm">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Clave:</label>
        <asp:TextBox runat="server" ID="txt_clave"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvClave" ControlToValidate="txt_clave" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Contraseña:</label>
        <asp:TextBox runat="server" ID="txt_contrasenia"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvContrasenia" ControlToValidate="txt_contrasenia" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        <asp:HiddenField runat="server" ID="hf_old_pwd" />
    </div>
    <div>
        <label>email:</label>
        <asp:TextBox runat="server" ID="txt_email"></asp:TextBox>
    </div>
    <div>
        <label>Bodega:</label>
        <asp:DropDownList runat="server" ID="ddlBodega"></asp:DropDownList>
    </div>
    <div>
        <label>Rol:</label>
        <asp:CheckBoxList runat="server" ID="chkLstRol"></asp:CheckBoxList>
        <%--<asp:DropDownList runat="server" ID="ddlRol"></asp:DropDownList>--%>
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
