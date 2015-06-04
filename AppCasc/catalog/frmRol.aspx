
<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRol.aspx.cs" Inherits="AppCasc.catalog.frmRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<fieldset>
<legend>Bodega</legend>
<div id="frmCatalog">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <hr />
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" OnClick="btnCancel_click" CausesValidation="false" />
    </div>
</div>
</fieldset>
</asp:Content>
