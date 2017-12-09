<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdenTrabajo.aspx.cs" Inherits="AppCasc.operation.frmOrdenTrabajo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/operation/frmOrdenTrabajo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Orden de Trabajo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

    <div class="divForm">
        <div>
            <label>No Tr&aacute;fico:</label>
            <asp:TextBox runat="server" ID="txt_trafico" Text="LT8036IM17"></asp:TextBox>
        </div>
        <div>
            <label style="border:none;">Servicio:</label>
            <asp:CheckBoxList runat="server" ID="chklst_servicio"></asp:CheckBoxList>
        </div>

        <asp:UpdatePanel runat="server" ID="up_pedido">
        <ContentTemplate>
        <div id="div_pedido" class="hidden">
            <label>No Pedido:</label>
            <asp:TextBox runat="server" ID="txt_pedido" Text="6141769" CausesValidation="true" AutoPostBack="true" OnTextChanged="pedido_changed"></asp:TextBox>

            <div>
                <asp:Label runat="server" ID="lbl_pedido_info"></asp:Label>
                <asp:Label runat="server" ID="lbl_pedido_piezas"></asp:Label>
                <asp:TextBox runat="server" ID="txt_pedido_pieza" Visible="false" placeholder="Piezas a precio"></asp:TextBox>
            </div>

            <asp:CustomValidator runat="server" ID="cv_pedido" ControlToValidate="txt_pedido" OnServerValidate="validatePedido" ErrorMessage="El pedido y código proporcionado no existe"></asp:CustomValidator>

        </div>
        
        </ContentTemplate>
        </asp:UpdatePanel>

        <div id="div_uva" class="hidden">
            <label>No Solicitud:</label>
            <asp:TextBox runat="server" ID="txt_solicitud"></asp:TextBox>
            <asp:TextBox runat="server" ID="txt_sol_pieza" placeholder="Piezas a NOM"></asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" ID="btn_guardar" Text="Guardar Orden de Trabajo" OnClick="guardar_ot" />
        </div>
        
    </div>

</div>

</asp:Content>
