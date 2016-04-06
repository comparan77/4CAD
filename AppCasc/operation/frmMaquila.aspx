<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmMaquila.aspx.cs" Inherits="AppCasc.operation.frmMaquila" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.combobox.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.combobox.js" type="text/javascript"></script>
    <script src="../js/moment.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmMaquila.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">B&uacute;squeda de Operaciones</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<span id="spn_Search" class="ui-icon ui-icon-close icon-button-search icon-button-action"></span>

<div id="div_busqueda">
    <label>Folio &oacute; Pedimento</label>
    <asp:TextBox runat="server" ID="txt_dato"></asp:TextBox>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" CausesValidation="false" />
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
    <ol>
        <asp:Repeater runat="server" ID="rep_resultados" OnItemCommand="click_result"  OnItemDataBound="rep_resultados_ItemDataBound">
        <ItemTemplate>
            <li><asp:LinkButton runat="server" Text='<%# Eval("folio") + ", " + Eval("referencia") %>' CommandArgument='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton></li>
            <ul style="list-style-type:circle;">
                <asp:Repeater runat="server" ID="repOrdCod" OnItemDataBound="repOrdCod_ItemDataBound">
                <ItemTemplate>
                    <li><asp:LinkButton runat="server" Text='<%# Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") %>' OnCommand="click_ord_cod" CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>' CausesValidation="false"></asp:LinkButton></li>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lbl_repOrdCod" Visible="false" Text="Sin inventario capturado"></asp:Label>
                </FooterTemplate>
                </asp:Repeater>
            </ul>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label runat="server" ID="lbl_resultados" Visible="false" Text="Sin resultados para el folio o pedimento proporcionado"></asp:Label>
        </FooterTemplate>
        </asp:Repeater>
    </ol>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div id="div_workDay" style="height: 150px">
        <fieldset class="floatLeft" style="background: inherit; width: 490px;">
        <legend>Maquilas por trabajar</legend>
        <div style="overflow-y: scroll; height: 100px;">
            <ul style="list-style-type: circle; margin-left: 20px;">
            <asp:Repeater runat="server" ID="rep_WorkDay" OnItemDataBound="repOrdCod_ItemDataBound">
                <ItemTemplate>
                    <li><asp:LinkButton ID="lnk_maqWork" runat="server" Text='<%# Eval("FolioEntrada") + ", " + Eval("referencia") + ", " + Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") %>' OnCommand="click_ord_cod" CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>' CausesValidation="false"></asp:LinkButton></li>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lbl_repOrdCod" Visible="false" Text="Sin maquila pendiente por trabajar"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>
            </ul>
        </div>
        </fieldset>

        <fieldset class="floatRight" style="background: inherit; width: 490px; margin-right: 5px;">
        <legend>Maquilas por Cerrar</legend>
        <div style="overflow-y: scroll; height: 100px;">
            <ul style="list-style-type: circle; margin-left: 20px;">
            <asp:Repeater runat="server" ID="rep_MqXCerrar" OnItemDataBound="repOrdCod_ItemDataBound">
                <ItemTemplate>
                    <li><asp:LinkButton ID="lnk_maqWork" runat="server" Text='<%# Eval("FolioEntrada") + ", " + Eval("referencia") + ", " + Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") + (Convert.ToInt32(Eval("id_estatus")) >= Inv_con_aprobacion ? "" : " [PTE-AUT]" ) %>' OnCommand="click_ord_cod" CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>' CausesValidation="false" ToolTip='<%# (Convert.ToInt32(Eval("id_estatus")) >= Inv_con_aprobacion ? "" : "Pendiente de Autorización" ) %>' Enabled='<%# (Convert.ToInt32(Eval("id_estatus")) >= Inv_con_aprobacion) %>' ></asp:LinkButton></li>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lbl_repOrdCod" Visible="false" Text="Sin maquila pendiente por cerrar"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>
            </ul>
        </div>
        </fieldset>

</div>

</div>

<h3 id="div-floor-control" style="clear:left; cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Orden de Trabajo</h3>
<div id="div-control" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm hidden">

<asp:HiddenField runat="server" ID="hf_id_entrada" Value="0" />
<asp:HiddenField runat="server" ID="hf_id_entrada_inventario" Value="0" />
<asp:HiddenField runat="server" ID="hf_id_cliente" Value="0" />
<asp:HiddenField runat="server" ID="hf_HasLote" Value="false" />

<asp:HiddenField runat="server" ID="hf_referencia" />
<asp:HiddenField runat="server" ID="hf_ordencompra" />
<asp:HiddenField runat="server" ID="hf_codigo" />
<asp:HiddenField runat="server" ID="hf_pieza_faltante" />
<asp:HiddenField runat="server" ID="hf_pieza_sobrante" />
<input type="hidden" id="hf_maquila_abierta" />
<%--<asp:HiddenField runat="server" ID="hf_EST_MAQ_PAR_CERRADA" />--%>

<div id="div_ordenescodigos" title="Órdenes de compra y códigos del pedimento">   
    <ul>
    <asp:Repeater runat="server" ID="rep_oc_by_pedimento">
    <ItemTemplate>
        <li><a href='<%# "?_fk=" + Eval("id_entrada") + "&_pk=" + Eval("id") %>'><%# Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia")%></a></li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>

<div>
    <label>Cliente:</label>
    <span><%= oE.ClienteNombre %></span>
</div>
<div>
    <label>Art&iacute;culo:</label>
    <span><%= oEI.Mercancia %></span>
</div>
<div>
    <label>Pedimento:</label>
    <span><a id="lnk_pedimento" class="icon-button-action" href="#"><%= oE.Referencia %></a></span>
</div>
<div>
    <label>Referencia:</label>
    <span><%= oE.Codigo %></span>
</div>
<div>
    <label>Orden:</label>
    <span><%= oEI.Orden_compra %></span>
</div>
<div>
    <label>C&oacute;digo:</label>
    <span><%= oEI.Codigo %></span>
</div>

<div>
    <label>Fecha Arribo:</label>
    <span><%= oE.Fecha.ToString("dd/MM/yyyy") %></span>
</div>

<div id="diferencias-total">
    <table id="dif-total" border="0" cellpadding="5" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th class="t_border_br t_border_tl">Faltantes</th>
                <th class="t_border_br t_border_t">Sobrantes</th>
                <th class="t_border_br t_border_t">Dañados</th>
            </tr>
        </thead>

        <tbody>
            <tr>
                <td class="t_border_br t_border_tl ">Bultos</td>
                <td class="t_border_br" id="td1" align="center"><%= oEM.Bulto_faltante.ToString("N0") %></td>
                <td class="t_border_br" id="td2" align="center"><%= oEM.Bulto_sobrante.ToString("N0")%></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="t_border_br t_border_l">Piezas</td>
                <td class="t_border_br" id="td3" align="center"><%= oEM.Pieza_faltante.ToString("N0")%></td>
                <td class="t_border_br" id="td4" align="center"><%= oEM.Pieza_sobrante.ToString("N0")%></td>
                <td class="t_border_br t_border_t" id="td5" align="center"><%= oEM.Pieza_danada.ToString("N0")%></td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:Button runat="server" ID="btn_cerrar_maquila" CommandArgument="True" CommandName="ConInc" Text="Cerrar Orden" OnCommand="click_cerrar_maquila" CausesValidation="false" />
    </div>
</div>

<div>
    <label>D&iacute;as Trabajados:</label>
    <ul class="diasTrabajados">
        <asp:Repeater runat="server" ID="rep_dias_trabajados" >
        <ItemTemplate>
            <li id='<%# "liDiaTrabajado_" + Eval("id") %>'>
                <a id='<%# "lnkDiaTrabajado_" + Eval ("id") %>' href="#" ><%# Eval("fecha_trabajo","{0:dd/MM/yyyy HH:mm:ss}") %></a>
                <span id='<%# "dltDiaTrabajado_" + Eval("id")%>' class="ui-icon ui-icon-trash icon-button-action dltDiaTrabajado floatRight"></span>
            </li>
        </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<asp:HiddenField runat="server" ID="hf_bultos" Value="0" />
<asp:HiddenField runat="server" ID="hf_piezasInventario" Value="0" />
<%--<asp:HiddenField runat="server" ID="hf_pzasXbulto" Value="0" />--%>
<asp:HiddenField runat="server" ID="hf_bulto_maquilado" Value="0" />

<table border="1" cellpadding="5" cellspacing="0" width="100%">
        <thead>
        <tr>
            <th>
                Detalle</th>
            <th>
                &nbsp;</th>
            <th>
                Inventario</th>
            <th>
                Maquilado</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td rowspan="4">
            
            <asp:GridView runat="server" ID="grdDetInv" CssClass="grdCasc" AutoGenerateColumns="false">
                    <Columns>
                    <asp:BoundField DataField="bultos" HeaderText="Bultos" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="piezasxbulto" HeaderText="Piezas x Bulto"  ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="piezastotales" HeaderText="Piezas"  ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                    </Columns>
                    </asp:GridView>
            
            </td>
            <td>Pallets</td>
            <td id="td_pallet_inventario" align="center"><%= oEI.Pallets.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pallet_maquilado"><%= oEM.Pallet.ToString() %></span></p></td>
        </tr>
        <tr>
            <td>Bultos</td>
            <td id="td_bulto_inventario" align="center"><%= oEI.Bultos_recibidos.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_bulto_maquilado"><%= oEM.Bulto.ToString() %></span></p></td>
        </tr>
        <tr>
            <td>Piezas Totales</td>
            <td id="td_pieza_inventario" align="center"><%= oEI.Piezas_recibidas.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pieza_maquilado"><%= oEM.Pieza.ToString() %></span></p></td>
        </tr>
    </tbody>
</table>

</div>


<h3 style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Captura de Trabajo</h3>
<div id="div-control-piso" style="clear: right;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection hidden divForm">

<div>
    <label>Fecha de Trabajo:</label>
    <asp:TextBox CssClass="txtNoBorder" runat="server" ID="txt_fecha_trabajo" Enabled="false"></asp:TextBox>
    <a href="#" id="lnk_limpiarCaptura">Limpiar informaci&oacute;n</a>
</div>

<div id="up_cantidades">
<asp:HiddenField runat="server" ID="hf_id_maquilado" Value="0" />
<input type="hidden" id="hf_piezasMaquiladasXDia" />
<asp:HiddenField runat="server" ID="hf_dia_pallet" Value="0" />
<asp:HiddenField runat="server" ID="hf_dia_bulto" Value="0" />
<asp:HiddenField runat="server" ID="hf_dia_pieza" Value="0" />
<div>
    <label>Pallet(s):</label>
    <asp:TextBox runat="server" CssClass="txtNumber" ID="txt_pallet" Text="0"></asp:TextBox>
    <asp:RangeValidator CssClass="validator" runat="server" Type="Integer" ID="rv_pallet" ControlToValidate="txt_pallet" ErrorMessage="Es necesario capturar un número entre 0 y 1000" MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>
</div>

<div style="padding: 20px;">
        <table class="tblItems" id="tblDetailBultos" cellpadding="2" cellspacing="2">
            <thead>
                <tr>
                    <th></th>
                    <th>Estado</th>
                    <th>Bultos</th>
                    <th>Piezas X Bulto</th>
                    <th class="hasLote">Lote</th>
                    <th>Total</th>
                </tr>
                <tr>
                    <th id="th_estados" style="padding-right: 7px">
                        <ul style="margin: 0; padding: 0;">
                            <li><input class="rbEstado" type="radio" id="rb_indemne" name="estado" checked="checked" /><label class="txtSmall" for="rb_indemne">Indemne</label></li>
                            <li><input class="rbEstado" type="radio" id="rb_danado" name="estado" /><label class="txtSmall" for="rb_danado">Da&ntilde;ado</label></li>
                        </ul>
                    </th>
                    <th id="thEstado">Indemne</th>
                    <th><input class="txtNumber" type="text" id="th_bultos" /></th>
                    <th><input class="txtNumberLarge" type="text" id="th_piezasxbulto" /></th>
                    <th style="width: 160px" align="left" class="hasLote"><select id="ddl_lote" class="txtSmall"><%=optLote %></select></th>
                    <th>&nbsp;</th>
                    <th><button id="btn_add_detalle"><span class="ui-icon ui-icon-plus"></span> </button></th>
                </tr>
            </thead>
            <tbody>
                  
            </tbody>
            <tfoot>
                <tr>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th id="thPzasTotal"></th>
                    <th>&nbsp;</th>
                </tr>
                <tr class="hidden">
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th><input disabled="disabled" type="text" style="border: none;" id="bultos" /></th>
                    <th><input disabled="disabled" type="text" style="border: none;" id="piezas" /></th>
                    <th>&nbsp;</th>
                </tr>
            </tfoot>
        </table>
    </div>

<div id="diferencias-fecha">
    <table id="dif-fecha" border="0" cellpadding="5" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th class="t_border_br t_border_tl">Faltantes</th>
                <th class="t_border_br t_border_t">Sobrantes</th>
            </tr>
        </thead>

        <tbody>
            <tr class="hidden">
                <td class="t_border_br t_border_tl">Bultos</td>
                <td id="td_bulto_faltante_fecha" class="t_border_br" align="center"></td>
                <td id="td_bulto_sobrante_fecha" class="t_border_br" align="center"></td>
            </tr>
            <tr>
                <td class="t_border_br t_border_tl">Piezas</td>
                <td id="td_pieza_faltante_fecha" class="t_border_br" align="center"></td>
                <td id="td_pieza_sobrante_fecha" class="t_border_br" align="center"></td>
            </tr>
        </tbody>
    </table>
</div>

</div>

<div>
    <asp:HiddenField runat="server" ID="hf_entrada_maquila_detail" />
    <asp:Button runat="server" ID="btn_save" Text="Guardar Trabajo Realizado" OnClick="save_orden" />
</div>

</div>

</div>
</asp:Content>
