<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmEmbarque.aspx.cs" Inherits="AppCasc.operation.frmEmbarque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmEmbarque.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Embarque</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    
    <div>
        <label>Bodega:</label>
        <asp:DropDownList runat="server" ID="ddlBodega" Enabled="false"></asp:DropDownList>
    </div>
    <div>
        <label>Fecha:</label>
        <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Hora de Salida:</label>
        <asp:TextBox id="txt_hora_salida" CssClass="horaPicker" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvHora_salida" ControlToValidate="txt_hora_salida" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
        <span class="hidden error">Es necesario proporcionar una hora</span>
    </div>
    <div>
        <label>Cortina:</label>
        <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
    </div>
    <div>
    <label>Cliente:</label>
        <asp:DropDownList runat="server" ID="ddlCliente"></asp:DropDownList>
    </div>
    
    <div>
    <label id="lblReferencia">Referencia:</label>
    <asp:TextBox runat="server" ID="txt_referencia"></asp:TextBox>
    <div>
        <button id="btn_validar_referencia">Validar</button>
    </div>
    </div>
</div>
</div>

</asp:Content>
