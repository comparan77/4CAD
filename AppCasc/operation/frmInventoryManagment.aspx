<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmInventoryManagment.aspx.cs" Inherits="AppCasc.operation.frmInventoryManagment" %>
<%@ Register src="../webControls/usrControlClienteMercancia.ascx" tagname="usrControlClienteMercancia" tagprefix="uc1" %>
<%@ Register src="../webControls/usrControlClienteVendor.ascx" tagname="usrControlClienteVendor" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.combobox.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.combobox.js" type="text/javascript"></script>
    <script src="../js/webControls/ctrlClienteMercancia.js?v1.1.150619_1446" type="text/javascript"></script>
    <script src="../js/webControls/ctrlClienteVendor.js?v1.1.150619_1446" type="text/javascript"></script>

    <script src="../js/operation/frmInventoryManagment.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">B&uacute;squeda de Referencias</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda">
    <label>Referencia:</label>
    <asp:TextBox runat="server" ID="txt_dato"></asp:TextBox>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" CausesValidation="false" />
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
    <ol>
        <asp:Repeater runat="server" ID="rep_resultados" OnItemDataBound="rep_resultados_ItemDataBound">
        <ItemTemplate>
            <li>
                <a class="icon-button-action referencias" href="#"><%# Eval("aduana") + "-" + Eval("referencia") %></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label runat="server" ID="lbl_resultados" Visible="false" Text="Sin resultados para la referencia proporcionada"></asp:Label>
        </FooterTemplate>
        </asp:Repeater>
    </ol>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

</div>

<h3 style="cursor: n-resize; margin-top: 5px" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Kardex</h3>
<div style="position: relative; padding-right: 5px;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

<input type="hidden" id="h_referencia" />

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">Inf. General</a></li>
    <li><a href="#tabs-2">C&oacute;digos y O.C.</a></li>
    <li><a href="#tabs-3">Historial de Mov.</a></li>
  </ul>
  <div id="tabs-1" class="divForm">
    <div>
        <label>Referencia Seleccionada:</label>
        <input type="text" class="txtNoBorder txtClear" id="txt_referencia" />
    </div>
  </div>
  <div id="tabs-2">
    <div id="div-codigos">
    <table border="1" cellpadding="2" cellspacing="0">
        <thead>
            <tr>
                <th>C&oacute;digo</th>
                <th>Orden de Compra</th>
                <th>Seleccionar</th>
            </tr>
        </thead>
        <tbody id="tbdy_oc_cod">
            
        </tbody>
    </table>
    </div>

    <div class="divForm floatLeft" style="margin-left: 10px;">
        <div>
            <label>Importador:</label>
            <input type="text" id="txt_importador" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>
        <div>
            <label>Factura:</label>
            <input type="text" id="txt_factura" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>
        <div>
            <label>Vendor C&oacute;digo:</label>
            <input type="text" id="txt_codigo_vendor" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
            <span id="spn_edit_vendor" class="ui-icon ui-icon-pencil icon-button-action floatLeft" title="Cambiar vendor"></span>
            <div id="div_udt_vendor" class="hidden">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cambio de Vendor <span style="float: right;" id="spn_close_vendor" class="ui-icon ui-icon-close icon-button-action"></span></h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
                    <div>
                        <label>Nuevo C&oacute;digo:</label>
                        <input type="text" id="txt_new_vendor" class="txtMedium txtClear" />
                    </div>
                    <div>
                        <label>Motivo:</label>
                        <textarea class="txtClear" id="txt_obs_vendor" rows="3" cols="3"></textarea>
                    </div>
                    <div>
                        <button id="btn_save_vendor">Guardar cambios</button>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <label>Vendor Nombre:</label>
            <input type="text" id="txt_nombre_vendor" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>
        <div>
            <label>Piezas:</label>
            <input type="text" id="txt_pieza_declarada" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>
        <div>
            <label>Valor Factura:</label>
            <input type="text" id="txt_val_fact" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>

        <div>
            <label>Mercanc&iacute;a C&oacute;digo:</label>
            <input type="text" id="txt_mer_cod" readonly="readonly" class="txtNoBorder txtClear txtMedium" />
            <span id="spn_edit_codigo" class="ui-icon ui-icon-pencil icon-button-action floatLeft" title="Cambiar código"></span>
            <div id="div_udt_codigo" class="hidden">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cambio de C&oacute;digo <span style="float: right;" id="spn_close_cod" class="ui-icon ui-icon-close icon-button-action"></span></h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
                    <div>
                        <label>Nuevo C&oacute;digo:</label>
                        <input type="text" id="txt_new_code" class="txtMedium txtClear" />
                    </div>
                    <div>
                        <label>Motivo:</label>
                        <textarea class="txtClear" id="txt_obs_code" rows="3" cols="3"></textarea>
                    </div>
                    <div>
                        <button id="btn_save_code">Guardar cambios</button>
                    </div>
                </div>
            </div>
        </div>

        <div>
            <label>Mercanc&iacute;a Descripci&oacute;n:</label>
            <input type="text" id="txt_mer_nombre" readonly="readonly" class="txtNoBorder txtClear txtLarge" />
        </div>
        
        <div>
            <label>Orden de Compra:</label>
            <input type="text" id="txt_mer_ord" readonly="readonly" class="txtNoBorder txtClear txtMedium" />
            <span id="spn_edit_orden" class="ui-icon ui-icon-pencil icon-button-action floatLeft" title="Cambiar orden"></span>
            <div id="div_udt_orden" class="hidden">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Cambio de Orden de compra<span style="float: right;" id="spn_close_ord" class="ui-icon ui-icon-close icon-button-action"></span></h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
                    <div>
                        <label>Nueva orden de compra:</label>
                        <input type="text" id="txt_new_orden" class="txtMedium txtClear" />
                    </div>
                    <div>
                        <label>Motivo:</label>
                        <textarea class="txtClear" id="txt_obs_orden" rows="3" cols="3"></textarea>
                    </div>
                    <div>
                        <button id="btn_save_orden">Guardar cambios</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div style="clear: left;">&nbsp;</div>

    <div>
        <uc1:usrControlClienteMercancia ID="usrControlClienteMercancia1" runat="server" />
        <uc1:usrControlClienteVendor ID="usrControlClienteVendor1" runat="server" />

    </div>

  </div>
  <div id="tabs-3">
    <p>En construcci&oacute;n...</p>
  </div>
</div>

</div>

</asp:Content>
