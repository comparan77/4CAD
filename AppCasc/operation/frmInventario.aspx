<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmInventario.aspx.cs" Inherits="AppCasc.operation.frmInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/common.js?v1.1.150427_2252" type="text/javascript"></script>
    <script src="../js/operation/frmInventario.js?v1.1.150427_2252" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">B&uacute;squeda de Operaciones</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda">
    <label>Folio &oacute; Pedimento</label>
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
            <li><asp:LinkButton runat="server" Text='<%# (Convert.ToBoolean(Eval("ConFondeo")) == true ? Eval("folio") + ", " + Eval("referencia") : Eval("folio") + ", " + Eval("referencia") + " Sin Fondeo") %>' OnCommand="click_result" CausesValidation="false" CommandArgument='<%# Eval("id") %>' Enabled='<%# Eval("ConFondeo") %>'></asp:LinkButton></li>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label runat="server" ID="lbl_resultados" Visible="false" Text="Sin resultados para el folio o pedimento proporcionado"></asp:Label>
        </FooterTemplate>
        </asp:Repeater>
    </ol>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

</div>

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Información del Arribo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm hidden">
<asp:HiddenField runat="server" ID="hf_id_entrada" Value="0" />
<asp:HiddenField runat="server" ID="hf_referencia_entrada" />
<asp:HiddenField runat="server" ID="hf_codigo" />
<asp:HiddenField runat="server" ID="hf_cliente_grupo" />
<div>
    <label>Cliente</label>
    <span><%= oE.ClienteNombre %></span>
</div>
<div>
    <label>Folio:</label>
    <span><%= oE.Folio %></span>
</div>
<div>
    <label>Fecha:</label>
    <span><%= oE.Fecha.ToString("dd \\de MMMM \\de yyyy")  %></span>
</div>
<div>
    <label>Ubicaci&oacute;n:</label>
    <span><%= oE.Ubicacion %></span>
</div>
<div>
    <label>Referencia:</label>
    <span><%= oE.Codigo %></span>
</div>
<div>
    <label>Pedimento:</label>
    <span><%= oE.Referencia %></span>
</div>
<div>
    <label>Mercanc&iacute;a</label>
    <span><%= oE.Mercancia.Trim() %></span>
</div>
<div>
    <table border="1" cellpadding="5" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th>Entrada</th>
                <th>Inventario</th>
                <th>Maquilado</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Pallet</td>
                <td id="td_pallet_recibido" align="center"><%= oE.No_pallet.ToString() %></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pallet_inventario"><%= oEI.Pallets.ToString() %> </span></p></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pallet_maquilado"><%= oEM.Pallet.ToString() %></span></p></td>
            </tr>
            <tr>
                <td>Bultos Declarados</td>
                <td id="td_bulto_declarado" align="center"><%= oE.No_bulto_declarado.ToString() %></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Bultos Recibidos</td>
                <td id="td_bulto_recibido" align="center"><%= oE.No_bulto_recibido.ToString() %></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_bulto_inventario"><%= oEI.Bultos_recibidos.ToString() %></span></p></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_bulto_maquilado"><%= oEM.Bulto.ToString() %></span></p></td>
                
            </tr>
            <tr>
                <td>Piezas Declaradas</td>
                <td id="td_pieza_declarada" align="center"><%= oE.No_pieza_declarada.ToString() %></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Piezas Recibidas</td>
                <td id="td_pieza_recibido" align="center"><%= oE.No_pieza_recibida.ToString() %></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pieza_inventario"><%= oEI.Piezas_recibidas.ToString() %></span></p></td>
                <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pieza_maquilado"><%= oEM.Pieza.ToString() %></span></p></td>
            </tr>
            <tr>
                <td>Bulto Da&ntilde;ado</td>
                <td align="center"><%= oE.No_bulto_danado.ToString() %></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Bulto Abierto</td>
                <td align="center"><%= oE.No_bulto_abierto.ToString() %></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </tbody>
    </table>
</div>

</div>

<asp:Panel runat="server" ID="pnl_CapturaManual" CssClass="hidden">
<h3 style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Control de Piso
    <span id="spn_move_end" style="float: right; margin-right: 5px; cursor: pointer;" class="ui-icon ui-icon-seek-end"></span>
    <span id="spn_move_nex" style="float: right; margin-right: 5px; cursor: pointer;" class="ui-icon ui-icon-seek-next"></span>
    <input type="hidden" id="h_stepnumber" value="0" />
    <span id="spn_move_pre" style="float: right; margin-right: 5px; cursor: pointer;" class="ui-icon ui-icon-seek-prev"></span>
    <span id="spn_move_fir" style="float: right; margin-right: 5px; cursor: pointer;" class="ui-icon ui-icon-seek-first"></span>
    </h3>
<div id="div-control-piso" style="clear: right;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection hidden">

<%--<asp:HiddenField runat="server" ID="hf_cat_ubicacion" />--%>
<%--<asp:HiddenField runat="server" ID="hf_cat_comprador" />--%>
<asp:HiddenField runat="server" ID="hf_cat_vendor" />
<asp:HiddenField runat="server" ID="hf_cat_nom" />

<asp:UpdatePanel runat="server" ID="up_codigo_mercancia" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="lnkMercancia" EventName="click" />
</Triggers>
<ContentTemplate>
    <asp:HiddenField runat="server" ID="hf_cat_codigo_mercancia" Value="[]" />      
    <asp:HiddenField runat="server" ID="hf_codigo_proporcionado" />                  
</ContentTemplate>
</asp:UpdatePanel>


<div>
<asp:UpdatePanel runat="server" ID="up_piso" UpdateMode="Conditional" ChildrenAsTriggers="false">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="lnk_add_piso" EventName="click" />
    <asp:PostBackTrigger ControlID="btn_save" />
    <%--<asp:AsyncPostBackTrigger ControlID="btn_save" EventName="click" />--%>
</Triggers>
<ContentTemplate>
<asp:HiddenField runat="server" ID="hf_consec_inventario" Value="0" />
<input type="hidden" id="hf_clienteMercanciaClase" />

<div class="hidden">
<%--<asp:CustomValidator runat="server" ID="cval_comprador" ValidationGroup="grp_catalogos" ControlToValidate="txt_comprador" Display="Static" ErrorMessage="Es necesario seleccionar un comprador de la lista" OnServerValidate="validate_catalog" ClientValidationFunction="validateCatalog" ValidateEmptyText="true"></asp:CustomValidator>--%>
<asp:CustomValidator runat="server" ID="cval_vendor" ValidationGroup="grp_catalogos" ControlToValidate="txt_vendor" Display="Static" ErrorMessage="Es necesario seleccionar un vendor de la lista" OnServerValidate="validate_catalog" ClientValidationFunction="validateCatalog" ValidateEmptyText="true"></asp:CustomValidator>
<%--<asp:CustomValidator runat="server" ID="cval_mercancia" ValidationGroup="grp_catalogos" ControlToValidate="txt_mercancia" Display="Static" ErrorMessage="Es necesario seleccionar una mercancia de la lista" OnServerValidate="validate_catalog" ClientValidationFunction="validateCatalog" ValidateEmptyText="true"></asp:CustomValidator>--%>
<asp:CustomValidator runat="server" ID="cval_nom" ValidationGroup="grp_catalogos" ControlToValidate="txt_nom" Display="Static" ErrorMessage="Es necesario seleccionar una NOM de la lista" OnServerValidate="validate_catalog" ClientValidationFunction="validateCatalog" ValidateEmptyText="true"></asp:CustomValidator>

<asp:Panel runat="server" ID="pnl_hf_catalogs">
<%--<asp:HiddenField runat="server" ID="hf_comprador" Value="0" />--%>
<asp:HiddenField runat="server" ID="hf_vendor" Value="0" />
<%--<asp:HiddenField runat="server" ID="hf_mercancia" Value="0" />--%>
<asp:HiddenField runat="server" ID="hf_nom" Value="0" />
<asp:HiddenField runat="server" ID="hf_codigo_mercancia" Value="0" />
</asp:Panel>
</div>

<div style="text-align: center">
<span style="display: block; color: Red; font-weight: normal; cursor: pointer;" id="spn_err_catalog"></span>
</div>

<table border="1" cellpadding="5" cellspacing="0" width="100%" id="tbl_control_piso">
        <thead>
            <tr id="tr_header_piso">
                <th align="center" style="width: 150px;">Vendor</th>
                <th align="center" style="width: 150px;">C&oacute;digo</th>
                <th align="center" style="width: 150px;">
                    <span>Descripci&oacute;n<asp:LinkButton runat="server" ID="lnkMercancia" CssClass="ui-icon ui-icon-plus floatRight" ToolTip="Agregar Código-Mercancía" OnClick="lnkMercanciaClick"></asp:LinkButton></span>
                </th>
                <th align="center" style="width: 150px;">&Oacute;rden de Compra</th>
                <th align="center" style="width: 150px;">Lote</th>
                <th class="hidden" align="center" style="width: 150px;">NOM</th>
                <th class="hidden" align="center" style="width: 150px;">Solicitud</th>
                <th class="hidden" align="center" style="width: 150px;">Factura</th>
                <th class="hidden" align="center" style="width: 150px;">Bultos</th>
                <th class="hidden" align="center" style="width: 150px;">Pzas por Bulto</th>
                <th class="hidden" align="center" style="width: 150px;">Valor Unitario</th>
                <th class="hidden" align="center" style="width: 150px;">Valor Factura</th>
                <th class="hidden" align="center" style="width: 150px;">Pzas Recibidas</th>
                <th class="hidden" align="center" style="width: 150px;">Bultos Recibidos</th>
                <th class="hidden" align="center" style="width: 150px;">Pallets</th>
                <th class="hidden" align="center" style="width: 150px; color: Gray;">Piezas Falt.</th>
                <th class="hidden" align="center" style="width: 150px; color: Gray;">Piezas Sobr.</th>
                <th class="hidden" align="center" style="width: 150px; color: Gray;">Bultos Falt.</th>
                <th class="hidden" align="center" style="width: 150px; color: Gray;">Bultos Sobr.</th>
                <th class="hidden" align="center" style="width: 150px;">Notas</th>
                <th class="hidden" align="center" style="width: 150px;">&nbsp;</th>
            </tr>
            <tr id="tr_input_piso">
                <td class="tdMove tdMoveStop" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_vendor"></asp:TextBox></td>
                <td class="tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_codigo_mercancia"></asp:TextBox></td>
                <td class="tdMove" style="background-color: Window;" align="center"><asp:TextBox runat="server" ID="txt_mercancia"></asp:TextBox></td>
                <td class="tdMove" style="background-color: Window;" align="center"><asp:TextBox runat="server" ID="txt_ordencompra"></asp:TextBox></td>
                <td class="tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_lote"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_nom"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_solicitud"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_factura"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_bulto" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_pza_bulto" Text="0"></asp:TextBox><asp:TextBox CssClass="txtNumber hidden" runat="server" ID="txt_pieza"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_val_unitario"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumberLarge" runat="server" ReadOnly="true" ID="txt_valor_factura"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_pieza_recibida" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_bulto_recibido" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" runat="server" ID="txt_pallet" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" ReadOnly="true" runat="server" ID="txt_pieza_falt" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" ReadOnly="true" runat="server" ID="txt_pieza_sobr" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" ReadOnly="true" runat="server" ID="txt_bulto_falt" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox CssClass="txtNumber" ReadOnly="true" runat="server" ID="txt_bulto_sobr" Text="0"></asp:TextBox></td>
                <td class="hidden tdMove" align="center" style="background-color: Window;"><asp:TextBox runat="server" ID="txt_observaciones"></asp:TextBox></td>
                <td class="hidden tdMove tdMoveStop" align="center" style="background-color: Window;"><asp:LinkButton ID="lnk_add_piso" runat="server" CssClass="ui-icon ui-icon-plus sphIcon" OnClick="add_piso" ValidationGroup="grp_catalogos"></asp:LinkButton>
                </td>
            </tr>
        </thead>
        <tbody id="tbody_val_piso">
            <asp:Repeater runat="server" ID="rep_piso">
            <ItemTemplate>
                <tr>
                    <td align="center"><%#Eval("Proveedor") %></td>
                    <td align="center"><%#Eval("Codigo")%></td>
                    <td align="center"><%#Eval("Mercancia") %></td>
                    <td class="unique" align="center"><%#Eval("Orden_compra")%></td>
                    <td class="hidden" align="center"><%#Eval("Nom") %></td>
                    <td class="hidden" align="center"><%#Eval("Solicitud") %></td>
                    <td class="hidden" align="center"><%#Eval("Factura") %></td>
                    <td class="hidden" align="center"><%#Eval("Bultos", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Piezas", "{0:N0}") + " Pzas."%></td>
                    <td class="hidden" align="center"><%#Eval("Valor_unitario", "{0:$#,##0.00}") %></td>
                    <td class="hidden" align="center"><%#Eval("Valor_factura", "{0:$#,##0.00}") %></td>
                    <td class="hidden" align="center"><%#Eval("Piezas_recibidas", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Bultos_recibidos", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Pallets", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Piezas_falt", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Piezas_sobr", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Bultos_falt", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("Bultos_sobr", "{0:N0}")%></td>
                    <td class="hidden" align="center"><%#Eval("observaciones") %></td>
                    <td class="hidden" align="center"><asp:LinkButton ID="LinkButton1" runat="server" CssClass="ui-icon ui-icon-minus sphIcon" ToolTip='<%# "Eliminar " + Eval("orden_compra") + ", " + Eval("codigo") + "?" %>' CommandArgument='<%#Eval("consec") %>' OnCommand="rem_piso"></asp:LinkButton>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>    
        <tfoot>
            <tr id="tr_footer_piso">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td align="center"><asp:Button runat="server" ID="btn_save" Text="Guardar Datos" OnClick="save_inventario" /></td>
                <td class="hidden">&nbsp;</td>
                <td class="hidden">&nbsp;</td>
                <td class="hidden">&nbsp;</td>
                <td class="hidden" id="td_bulto_total" align="center"><%= TotalBulto.ToString("N0") %><span class="ui-icon"><input type="hidden" value='<%=TotalBulto.ToString() %>' /></span></td>
                <td class="hidden" id="td_pieza_total" align="center"><%= TotalPieza.ToString("N0")%><span class="ui-icon"><input type="hidden" value='<%=TotalPieza.ToString() %>' /></span></td>
                <td class="hidden" id="td_valuni_total" align="center"> </td>
                <td class="hidden" id="td_valfact_total" align="center"><%= TotalFactura.ToString("$#,##0.00") %></td>
                <td class="hidden" id="td_pieza_recibida_total" align="center"><%= TotalPiezaRecibida.ToString("N0") %></td>
                <td class="hidden" id="td_bulto_recibido_total" align="center"><%= TotalBultoRecibido.ToString("N0") %></td>
                <td class="hidden" id="td_pallet_total" align="center"><%= TotalPallet.ToString("N0")%><span class="ui-icon"><input type="hidden" value='<%=TotalPallet.ToString() %>' /></span></td>
                <td class="hidden" id="td_pieza_falt_total" align="center"><%= TotalPiezaFalt.ToString() %><span><input type="hidden" value='<%=TotalPiezaFalt.ToString() %>' /></span></td>
                <td class="hidden" id="td_pieza_sobr_total" align="center"><%= TotalPiezaSobr.ToString() %><span><input type="hidden" value='<%=TotalPiezaSobr.ToString() %>' /></span></td>
                <td class="hidden" id="td_bulto_falt_total" align="center"><%= TotalBultoFalt.ToString() %><span><input type="hidden" value='<%=TotalBultoFalt.ToString() %>' /></span></td>
                <td class="hidden" id="td_bulto_sobr_total" align="center"><%= TotalBultoSobr.ToString() %><span><input type="hidden" value='<%=TotalBultoSobr.ToString() %>' /></span></td>
                <td class="hidden">&nbsp;</td>
                <td class="hidden"></td>
            </tr>
        </tfoot>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</div>

</div>
</asp:Panel>

<h3 id="div-data-stock" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Contorl de Piso</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection hidden">

<div id="div-codigos" style="width: 350px;">
<%--<ul>
<asp:Repeater runat="server" ID="rep_codigos">
<ItemTemplate>
    <li><a id='<%#Eval("id") %>' href="#"><%# Eval("vendor") + " - " + Eval("codigo") + " - " + Eval("orden") %></a></li>
</ItemTemplate>
</asp:Repeater>
</ul>--%>

<asp:GridView runat="server" ID="grdCodigos" CssClass="grdCasc" AutoGenerateColumns="false">
<RowStyle CssClass="selectCodigo" />
<Columns>
<asp:TemplateField HeaderText="Vendor" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
    <span style="display: block; float: left;"><%#Eval("vendor") %></span>
    <input type="hidden" id='<%#Eval("Id") %>' />
    <span class="ui-icon ui-icon-arrowthick-1-e icon-button-action" style="display: block; float: left;"></span>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="codigo" ItemStyle-HorizontalAlign="Center" HeaderText="Código" />
<asp:BoundField DataField="orden" ItemStyle-HorizontalAlign="Center" HeaderText="Orden" />
<asp:BoundField DataField="Estatus" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus" />
</Columns>
</asp:GridView>

</div>

<div id="div-info-codigo" class="divForm">
    <div>
        <input type="hidden" id="idEntInv" value="0" />
    </div>
    <div>
        <label for="vendor">Vendor:</label>
        <select id="vendor"></select>
    </div>
    <div>
        <label for="mercancia">Mercanc&iacute;a</label>
        <select id="mercancia"></select>
    </div>
    <div>
        <label for="orden">Orden:</label>
        <input type="text" disabled="disabled" id="orden" />
    </div>
    <div>
        <label for="nom">NOM:</label>
        <asp:DropDownList runat="server" ID="nom"></asp:DropDownList>
        <span id="spnNomDesc" class="ui-icon ui-icon-info floatRight" style="cursor: pointer;"></span>
    </div>
    <div>
        <label for="solicitud">Solicitud:</label>
        <input type="text" id="solicitud" />
    </div>
    <div>
        <label for="factura">Factura:</label>
        <input type="text" disabled="disabled" id="factura" />
    </div>

    <div style="padding: 20px 0 20px 20px; width: 300px;">
    <h6 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Desglose de Lotes<span id="spn_desglose_lotes" class="ui-icon ui-icon-arrowthick-1-s floatRight icon-button-action"></span></h6>
        <table class="tblItems hidden" id="tblDetailLotes">
            <thead>
                <tr>
                    <th>Lote</th>
                    <th>Piezas</th>
                </tr>
                <tr>
                    <th><input class="txtSmall" type="text" id="th_lote" /></th>
                    <th><input class="txtNumber" type="text" id="th_piezasLote" /></th>
                    <th><button id="btn_add_lote" disabled="disabled"><span class="ui-icon ui-icon-plus"></span> </button></th>
                </tr>
            </thead>
            <tbody>
            
            </tbody>
            <tfoot>
                <tr>
                    <th>&nbsp;</th>
                    <th>
                        <span id="th_totLote">0</span>
                        <asp:TextBox CssClass="hidden" runat="server" ID="txt_sumLotes" Text="0"></asp:TextBox>
                    </th>
                </tr>
                <tr>
                    <th colspan="2">
                        <asp:RangeValidator runat="server" ID="rangeVal_sumLotes" ControlToValidate="txt_sumLotes" Type="Integer" MinimumValue="0" MaximumValue="0" ErrorMessage="El desglose de lotes no coincide con el de la nota de remisión" ></asp:RangeValidator>
                    </th>
                </tr>
            </tfoot>
        </table>
    </div>

    <div style="padding: 0 20px 20px 20px; width: 300px;">
    <h6 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Desglose de Nota de Remis&oacute;n</h6>
        <table class="tblItems" id="tblDetailBultos">
            <thead>
                <tr>
                    <th>Bultos</th>
                    <th>Piezas X Bulto</th>
                </tr>
                <tr>
                    <th><input class="txtNumber" type="text" id="th_bultos" /></th>
                    <th><input class="txtNumberLarge" type="text" id="th_piezasxbulto" /></th>
                    <th><button id="btn_add_detalle" disabled="disabled"><span class="ui-icon ui-icon-plus"></span> </button></th>
                </tr>
            </thead>
            <tbody>
                  
            </tbody>
            <tfoot class="hidden">
                <tr>
                    <th><input disabled="disabled" type="text" style="border: none;" id="bultos" /></th>
                    <th><input disabled="disabled" type="text" style="border: none;" id="piezas" /></th>
                    <th>&nbsp;</th>
                </tr>
            </tfoot>
        </table>
    </div>
    <div>
        <label for="valorunitario">Valor Unitario:</label>
        <input type="text" class="txtCurrency" disabled="disabled" id="valorunitario" />
    </div>
    <div>
        <label for="valorfactura">Valor Factura:</label>
        <input type="text" class="txtCurrency" disabled="disabled" id="valorfactura" />
    </div>
    <div>
        <label for="piezasdeclaradas">Piezas Declaradas:</label>
        <input type="text" disabled="disabled" id="piezasdeclaradas" />
    </div>
    <div>
        <label for="piezasrecibidas">Piezas Recibidas:</label>
        <input type="text" id="piezasrecibidas" runat="server" />
        <asp:RequiredFieldValidator runat="server" ID="rfv_piezasrecibidas" ControlToValidate="piezasrecibidas" ErrorMessage="Es necesario proporcionar un valor"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label for="bultosrecibidos">Bultos Recibidos:</label>
        <input type="text" id="bultosrecibidos" runat="server" />
        <asp:RequiredFieldValidator runat="server" ID="rfv_bultosrecibidos" ControlToValidate="bultosrecibidos" ErrorMessage="Es necesario proporcionar un valor"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label for="pallets">Pallets:</label>
        <input type="text" id="pallets" />
    </div>
    <div>
        <label for="piezasfaltantes">Piezas Faltantes:</label>
        <input type="text" disabled="disabled" id="piezasfaltantes" value="0" />
    </div>
    <div>
        <label for="piezassobrantes">Piezas Sobrantes:</label>
        <input type="text" disabled="disabled" id="piezassobrantes" value="0"/>
    </div>
    <div>
        <label for="bultosfaltantes">Bultos Faltantes:</label>
        <input type="text" disabled="disabled" id="bultosfaltantes" value="0"/>
    </div>
    <div>
        <label for="bultossobrantes">Bultos Sobrantes:</label>
        <input type="text" disabled="disabled" id="bultossobrantes" value="0"/>
    </div>
    <div class="hidden">
        <label for="fechamaquila">Fecha de Maquila:</label>
        <asp:TextBox runat="server" ID="fechamaquila"></asp:TextBox>
        <asp:RequiredFieldValidator Enabled="false" runat="server" ID="rfv_fechamaquila" ControlToValidate="fechamaquila" ErrorMessage="Es necesario proporcionar una fecha"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label for="notas">Notas:</label>
        <input type="text" id="notas" />
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hf_entrada_inventario" />
        <asp:HiddenField runat="server" ID="hf_entrada_inventario_detail" />
        <asp:HiddenField runat="server" ID="hf_entrada_inventario_lote" />
        <input type="hidden" id="hf_id_entrada_fondeo" />
        <asp:Button runat="server" ID="btnSaveCodigo" Text="Guardar información" OnClick="clickSaveCodigo" />
        <%--<asp:UpdatePanel runat="server" ID="up_savecodigo">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSaveCodigo" EventName="click" />
        </Triggers>
        <ContentTemplate>
        <div></div>
        </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</div>
<div style="clear: both;"></div>

</div>

</div>

</asp:Content>
