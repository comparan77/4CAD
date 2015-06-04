<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmClienteGrupo.aspx.cs" Inherits="AppCasc.catalog.frmClienteGrupo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/catalog/frm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cliente Grupo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <asp:Panel ID="pnl_configuracion" runat="server" Visible="false">
        <label>Configuraciones:</label>
        <div>
            <ul style="list-style: none">
                <li><asp:HyperLink runat="server" ID="lnkCodigo" Text="Referencias"></asp:HyperLink></li>
                <%--<li><asp:HyperLink runat="server" ID="lnkComprador" Text="Compradores"></asp:HyperLink></li>--%>
                <li><asp:HyperLink runat="server" ID="lnkVendor" Text="Vendor"></asp:HyperLink></li>
                <li><asp:HyperLink runat="server" ID="lnkMercancia" Text="Mercancía"></asp:HyperLink></li>
            </ul>
        </div>
    </asp:Panel>
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
