<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmCancelDoc.aspx.cs" Inherits="AppCasc.operation.frmCancelDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />--%>

   <%-- <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>--%>
    <script src="../js/operation/frmCancelDoc.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<div id="div_panel">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cancelaciones</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div class="divForm">
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
        <label>Referencias Compartidas:</label>
        <asp:ListBox runat="server" ID="lstCompartida"></asp:ListBox>
    </div>
    <div>
        <label>Tipo:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtTipo"></asp:TextBox>
    </div>
    <hr />
    <div>
        <label>Autoriza:</label>
        <asp:TextBox runat="server" Enabled="false" ID="txtAutorizaUsuario"></asp:TextBox>
    </div>
    <div>
        <label>Motivo:</label>
        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" Columns="1" ID="txtMotivo"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvMotivo" ControlToValidate="txtMotivo" ErrorMessage="Es necesario proporcionar un motivo de cancelación."></asp:RequiredFieldValidator>
    </div>
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnCancelFolio" Text="" OnClick="btnCancelFolio_click" />
        <asp:Button runat="server" ID="btnCancelRef" Text="" Visible="false" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CausesValidation="false" OnClick="btnRegresar_click" />
    </div>
</div>
</div>
</div>

</asp:Content>
