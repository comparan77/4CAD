<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmClienteMercancia.aspx.cs" Inherits="AppCasc.catalog.frmClienteMercancia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Mercanc&iacute;a de <%=clienteGrupoNombre %></h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Clase:</label>
        <asp:DropDownList runat="server" ID="ddl_clase">
            <asp:ListItem Text="F&H" Value="F&H"></asp:ListItem>
            <asp:ListItem Text="COS" Value="COS"></asp:ListItem>
            <asp:ListItem Text="COM" Value="COM"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Negocio:</label>
        <asp:TextBox runat="server" ID="txt_negocio" MaxLength="2"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfv_negocio" ControlToValidate="txt_negocio" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>C&oacute;digo:</label>
        <asp:TextBox runat="server" ID="txt_codigo"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfv_codigo" ControlToValidate="txt_codigo" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfv_Nombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Unidad:</label>
        <asp:TextBox runat="server" ID="txt_unidad" MaxLength="2"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfv_unidad" ControlToValidate="txt_unidad" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
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
