<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRemision.aspx.cs" Inherits="AppCasc.operation.frmRemision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/moment.min.js" type="text/javascript"></script>
    <script src="../js/operation/helperRemDetail.js?v1.1.150619_1446" type="text/javascript"></script>
    <script src="../js/operation/frmRemision.js?v1.1.150619_1446" type="text/javascript"></script>
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
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
    <ol>
        <asp:Repeater runat="server" ID="rep_resultados" OnItemCommand="click_result"  OnItemDataBound="rep_resultados_ItemDataBound">
        <ItemTemplate>
            <li><asp:LinkButton ID="LinkButton1" CssClass="lnk_result" runat="server" Text='<%# Eval("folio") + ", " + Eval("referencia") %>' CommandArgument='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton></li>
            <ul style="list-style-type:circle;">
                <asp:Repeater runat="server" ID="repOrdCod">
                <ItemTemplate>
                    <li><asp:LinkButton ID="lnkMaquilado" runat="server" Text='<%# Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") + " " + Eval("lote") + (Convert.ToBoolean(Eval("maquilado")) ? "" : " [Sin Maquila]") %>' Enabled='<%# Convert.ToBoolean(Eval("maquilado")) %>' OnCommand="click_ord_cod" CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>' CausesValidation="false"></asp:LinkButton></li>
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

</div>

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Remisi&oacute;n</h3>
<div id="div-control" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm hidden">
<asp:HiddenField runat="server" ID="hf_id_entrada" Value="0" />
<asp:HiddenField runat="server" ID="hf_id_entrada_inventario" Value="0" />
<asp:HiddenField runat="server" ID="hf_id_cliente" Value="0" />
<asp:HiddenField runat="server" ID="hf_id_entrada_maquila" Value="0" />
<asp:HiddenField runat="server" ID="hf_referencia" Value="" />
<asp:HiddenField runat="server" ID="hf_codigo_cliente" Value="" />
<asp:HiddenField runat="server" ID="hf_codigo" Value="" />
<asp:HiddenField runat="server" ID="hf_orden" Value="" />
<%--<asp:HiddenField runat="server" ID="hf_EST_REM_CON_APROBACION" />--%>
<asp:HiddenField runat="server" ID="hf_idUsuario" />
<asp:HiddenField runat="server" ID="hf_HasLote" Value="false" />

<div id="div_ordenescodigos" title="Órdenes de compra y códigos del pedimento">   
    <ul>
    <asp:Repeater runat="server" ID="rep_oc_by_pedimento">
    <ItemTemplate>
        <li><a href='<%# (Convert.ToBoolean(Eval("maquilado")) ? "?_fk=" + Eval("id_entrada") + "&_pk=" + Eval("id") : "#") %>' style='<%# (Convert.ToBoolean(Eval("maquilado")) ? "" : "color:black; pointer-events: none;") %>'><%# Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") + Eval("lote") + (Convert.ToBoolean(Eval("maquilado")) ? "" : " [Sin Maquila]")%></a></li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>

<div>
    <label>Referencia:</label>
    <span><%= oE.Codigo %></span>
</div>
<div>
    <label>Cliente:</label>
    <span><%= oE.ClienteNombre %></span>
</div>
<div>
    <label>Proveedor:</label>
    <span><%= oCV.Nombre %></span>
</div>
<div>
    <label>Direccion:</label>
    <span title='<%= oCV.Direccion %>' style="cursor: help;"><%= oCV.DireccionCorta %></span>
</div>
<div>
    <label>Pedimento:</label>
    <span><a id="lnk_pedimento" class="icon-button-action" href="#"><%= oE.Referencia %></a></span>
</div>
<div>
    <label>Bultos x Tarima (Estandar):</label>
    <span><%= oEI.Bultosxpallet.ToString() %></span>
</div>
<div>
    <label>Estado de la Maquila:</label>
    <span id="spn_estado_maquila" title='<%= oEI.Maquila_abierta ? "" : "Abrir Maquila" %>' class='<%= "ui-icon ui-icon-" + (oEI.Maquila_abierta ? "unlocked" : "locked icon-button-action") %>'></span>
</div>
<div style="width: 450px; padding: 10px; position: relative; margin-bottom: 70px;">
    <label>Detalle de Maquila</label>
    <asp:GridView runat="server" ID="grdDetMaq" CssClass="grdCasc" AutoGenerateColumns="false" CellPadding="3">
    <Columns>
    <asp:TemplateField HeaderText="Estado">
    <ItemStyle HorizontalAlign="Center" />
    <ItemStyle CssClass="icon-button-action selectLink" />


    <ItemTemplate>
    <%# (Convert.ToBoolean (Eval("danado")) ? "Dañado" : "Indemne") %>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="bultos" HeaderText="Bto Maq." ItemStyle-HorizontalAlign="Right" />
    <asp:BoundField DataField="BultoSR" HeaderText="Bto Rem." ItemStyle-HorizontalAlign="Right" />
    <asp:BoundField DataField="BultoD" HeaderText="Bto Disp." ItemStyle-HorizontalAlign="Right" />
    <asp:BoundField DataField="piezasxbulto" HeaderText="Piezas por bulto" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
    <asp:BoundField DataField="piezastotales" HeaderText="Total Disp." DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
    <asp:BoundField HeaderStyle-CssClass="hasLote" ItemStyle-CssClass="hasLote" DataField="lote" HeaderText="Lote." />
    <asp:TemplateField>
        <HeaderStyle CssClass="hidden" />
        <ItemStyle CssClass="hidden" />
        <ItemTemplate>
            <input type="hidden" value='<%# Eval("id") %>' />
        </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>

<div style="position: absolute; top: 31px; left: 530px; width: 400px;">

<h6 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top" style="text-align: center; ">Seleccione una maquila del panel izquierdo</h6>
<table cellpadding="3" border="1" cellspacing="0" width="100%">
<thead>
    <tr>
        <th>Rengl&oacute;n</th>
        <th class="hasLote">Lote</th>
        <th>Pzas x Bto</th>
        <th>Bultos</th>
        <th>Agregar</th>
        <th>Limpiar</th>
    </tr>
</thead>
<tbody>
    <tr id="renglon-1">
        <td align="center">1<input type="hidden" id="hf_id_maquila_detail_1" /></td>
        <td align="center" class="hasLote"><span id="spn_lote-1"></span></td>
        <td align="center"><span id="spn_pzaXbulto-1"></span><input type="hidden" id="hf_danado-1" value="false" /></td>
        <td align="center"><input type="text" id="txt_bulto-1" class="txtNumber" /><input type="hidden" id="hf_max_bulto-1" /></td>
        <td align="center"><span id="spn_add-1" class="ui-icon ui-icon-plus icon-button-action"></span></td>
        <td align="center"><span id="spn_del-1" class="ui-icon ui-icon-trash icon-button-action"></span></td>
    </tr>
    <tr id="renglon-2">
        <td align="center">2<input type="hidden" id="hf_id_maquila_detail_2" /></td>
        <td align="center" class="hasLote"><span id="spn_lote-2"></span></td>
        <td align="center"><span id="spn_pzaXbulto-2"></span><input type="hidden" id="hf_danado-2" value="false" /></td>
        <td align="center"><input type="text" id="txt_bulto-2" class="txtNumber" /><input type="hidden" id="hf_max_bulto-2" /></td>
        <td align="center"><span id="spn_add-2" class="ui-icon ui-icon-plus icon-button-action"></span></td>
        <td align="center"><span id="spn_del-2" class="ui-icon ui-icon-trash icon-button-action"></span></td>
    </tr>
</tbody>
</table>
</div>

</div>
<div>
    <label>Remisiones:</label>
    <ul class="diasRemision">
        <asp:Repeater runat="server" ID="rep_remisiones" >
        <ItemTemplate>
            <li>
                <a href="#" class="lnk-folio-remision" title='<%#Eval("fecha_remision","{0:dd/MM/yyyy}") %>' ><%# Eval("folio_remision") %></a>
                <%--<input type="hidden" value='<%#Eval("bulto", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezaxbulto", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("pieza", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("bultoinc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezaxbultoinc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezainc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezatotal", "{0:N0}") %>' />--%>
                <input type="hidden" value='<%#Eval("dano_especifico") %>' />
                <input type="hidden" value='<%#Eval("etiqueta_rr") %>' />
                <input type="hidden" value='<%#Eval("fecha_recibido", "{0:dd/MM/yyyy}") %>' />
                <input type="hidden" value='<%#Eval("PTrafico.Folio_cita") %>' />
                <input type="hidden" value='<%#Eval("tieneOrdenCarga") %>' />
                <input type="hidden" value='<%#Eval("id") %>' />
            </li>
        </ItemTemplate>
        </asp:Repeater>
    </ul>

    <div class="hidden">
        <asp:Button runat="server" ID="btnDltRemision" CausesValidation="false" OnClick="btnDltRemision_click" />
        <asp:HiddenField runat="server" ID="hf_id_remision" Value="0" />
        <asp:HiddenField runat="server" ID="hf_motivo_cancelacion" />
    </div>

    <div id="div-tbl-folio-remision">
        <table border="1" cellpadding="5" cellspacing="0" width="100%">
             <tbody>
                <tr>
                    <td>
                        <label>Total CTNS:</label>
                    </td>
                    <td>
                        <span id="spn-bulto-1"></span>
                        <label>Cartones</label>
                    </td>
                    <td>
                        <span id="spn-piezaxbulto-1"></span>
                        <label>Piezas C/U</label>
                    </td>
                    <td>
                        <span id="spn-pieza-1"></span>
                    </td>
                    <td>
                        <span id="spn-estado-1"></span>
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Total CTNS:</label>
                    </td>
                    <td>
                        <span id="spn-bulto-2"></span>
                        <label>Cartones</label>
                    </td>
                    <td>
                        <span id="spn-piezaxbulto-2"></span>
                        <label>Piezas C/U</label>
                    </td>
                    <td>
                        <span id="spn-pieza-2"></span>
                    </td>
                    <td>
                        <span id="spn-estado-2"></span>
                    </td>
                </tr>

            </tbody>
            <tfoot>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">Total Piezas</td>
                    <td><span id="spn-piezatotal"></span></td>
                    <td>&nbsp;</td>
                </tr>
            </tfoot>
        </table>
        <div style="padding: 5px;">
            <span style="background-color: Black; color: White;" id="spn-dano_especifico" ></span>
        </div>
        <div style="padding: 5px;">
            <label>Etiqueta RR:</label>
            <span id="spn-etiqueta_rr"></span>
        </div>
        <div style="padding: 5px;">
            <label>Fecha Recibido:</label>
            <span id="spn-fecha_recibido"></span>
        </div>
        <div style="padding: 5px;">
            <label>Folio Cita:</label>
            <span id="spn-folio_cita" class="icon-button-action" style="color: #0d5fb3"></span>
        </div>
        <div>
            <label>&nbsp;</label>
            <span class="hidden" id="spn-dlt"></span>
            <span class="hidden" id="spn-tieneOrdenCarga"></span>
            <span class="hidden" id="spn-estatus"></span>
            <button class="floatLeft" id="imprimir-remision">Imprimir Remisi&oacute;n</button>
            <button class="floatRight" id="eliminar-remision">Eliminar Remisi&oacute;n</button>
        </div>
    </div>

</div>

<div id="div_cantidades_remision">
    <table id="dif-fecha" border="0" cellpadding="5" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th class="t_border_br t_border_tl">Inventario</th>
                <th class="t_border_br t_border_t">Maquilado</th>
                <th class="t_border_br t_border_t">Disponible</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="t_border_br t_border_tl">Bto</td>
                <td align="center" class="t_border_br"><%=oEI.Bultos %></td>
                <td align="center" class="t_border_br"><%=oEM.Bulto %></td>
                <td align="center" class="t_border_br"><%=oSR.BultoTotal %></td>
            </tr>
            <tr>
                <td class="t_border_br t_border_l">Pza</td>
                <td align="center" class="t_border_br"><%=oEI.Piezas %></td>
                <td align="center" class="t_border_br"><%=oEM.Pieza %></td>
                <td align="center" class="t_border_br"><%=oSR.PiezaTotal %></td>
            </tr>
        </tbody>
    </table>
</div>

<div id="div_udt_mercancia" title="Cambio de la descripción de la mercancía">
    <div class="divForm">
    <div>
        <label>Descripción Actual:</label>
        <input type="text" id="txt_old_description" readonly="readonly" class="txtNoBorder txtLarge" value='<%=oEI.Mercancia %>' />
    </div>
    <div style="margin-top: 5px;">
        <label>Descripción Nueva:</label>
        <input type="text" id="txt_new_description" class="txtLarge" />
    </div>
    <div style="margin-top: 5px;">
        <input type="hidden" id="hf_codigo_udt" value='<%=oEI.Codigo %>' />
        <button class="floatRight" style="margin-right: 10px;" id="btn_udt_description">Actualizar</button>
    </div>
    </div>
</div>

<div style="margin-top: 25px; clear: left;">

<table border="1" cellpadding="5" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td align="center" id="codigo"><span class="spn_title">C&oacute;digo:<br /></span> <%=oEI.Codigo %> <br />
                <img id="img-codigo" src="" alt=""  />
                <asp:HiddenField runat="server" ID="hf_img_codigo" Value="" />
            </td>
            <td align="center" id="orden_compra"><span class="spn_title">Orden de Compra:<br /></span><%=oEI.Orden_compra %> <br />
                <img id="img-orden" src="" alt="" />
                <asp:HiddenField runat="server" ID="hf_img_orden" Value="" />
            </td>
            <td align="center" id="mercancia"><span id="spn_mercancia" class="spn_title">Descripci&oacute;n<br /></span><a id="lnk_mercancia" href="#"><%=oEI.Mercancia %></a><br /><span id="spnLotes"></span></td>
            <td align="center" id="vendor"><span class="spn_title">No. de Proveedor<br /></span><%=oCV.Codigo %><br />
                <img id="img-vendor" src="" alt="" />
                <asp:HiddenField runat="server" ID="hf_img_vendor" Value="" />
            </td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="4" align="center">
                <div id="disabledCantidadesSalida">
                <table id="tbl_cantidades_salida" cellpadding="5" cellspacing="0">
                    <tbody>
                        <tr>
                            <td>
                                <label>Total CTNS:</label>
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="hf_mercancia_danada" Value="false" />
                                <%--<asp:HiddenField runat="server" ID="hf_id_entrada_maquila_detail_1" />--%>
                                <asp:TextBox runat="server" ID="txt_bulto" CssClass="txtNumber"  Text="0"></asp:TextBox>
                                <label>Cartones</label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txt_piezasXbulto" CssClass="txtNumber" Text="0"  ></asp:TextBox>
                                <label>Piezas C/U</label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txt_piezas" CssClass="txtNumber" Text="0"  ></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hf_lote_1" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Total CTNS:</label>
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="hf_mercancia_danadaInc" Value="false" />
                                <%--<asp:HiddenField runat="server" ID="hf_id_entrada_maquila_detail_2" />--%>
                                <asp:TextBox runat="server" ID="txt_bultoInc" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                <label>Cartones</label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txt_piezasXbultoInc" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                <label>Piezas C/U</label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txt_piezasInc" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hf_lote_2" />
                            </td>
                        </tr>

                    </tbody>
                    <tfoot>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right">Total Piezas</td>
                            <td><asp:TextBox runat="server" Enabled="false" ID="txt_piezaTotal" CssClass="tdCantSalidaTotalPieza txtNumber" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <asp:RangeValidator CssClass="validator" MinimumValue="1" Type="Integer" runat="server" ID="rv_piezas" ControlToValidate="txt_piezaTotal" ErrorMessage="Es necesario seleccionar una maquila y proporcionar la cantidad de bultos"></asp:RangeValidator>
                        </tr>
                    </tfoot>
                </table>
                </div>
            </td>
        </tr>
    </tfoot>
</table>

</div>

<div id="div-datos">
    <div id="div-danada" class="hidden">
        <label>Mercanc&iacute;a Da&ntilde;ada: Da&ntilde;o Espec&iacute;fico:</label>
        <asp:TextBox runat="server" ID="txt_dano" TextMode="MultiLine" Columns="2" Text="SinDano"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_dano" ControlToValidate="txt_dano" ErrorMessage="Es necesario proporcionar una descripción" ></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Elabor&oacute;:</label>
        <asp:TextBox runat="server" ID="txt_elaboro"></asp:TextBox>
    </div>
    <div>
        <label>Autoriz&oacute;:</label>
        <asp:DropDownList runat="server" ID="ddl_autorizo">
            <asp:ListItem Text="Judith Cortes" Value="25"></asp:ListItem>
            <asp:ListItem Text="Sandra Rodriguez" Value="30"></asp:ListItem>
        </asp:DropDownList>
    </div>
   <%-- <div>
        <label>Fecha:</label>
        <asp:TextBox runat="server" ID="txt_fecha_remision"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_remision" ErrorMessage="Es necesario proporcionar la fecha de remision" ControlToValidate="txt_fecha_remision"></asp:RequiredFieldValidator>
    </div>--%>
    <div>
        <label>Folio Cita:</label><span id="spn_folio_cita" class="ui-icon ui-icon-calendar icon-button-action"></span><span id="spn_cita_sel"></span>
        <asp:TextBox CssClass="hidden" runat="server" ID="txt_folio_cita"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_folio_cita" ErrorMessage="Es necesario proporcionar el folio de la cita" ControlToValidate="txt_folio_cita"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_save" Text="Guardar Remisión" OnClick="save_remision" />
    </div>
</div>

</div>

</div>

<!-- Listado de citas -->
<div id="div_citas" title="Listado de Citas">
    <ul id="ul_citas" style="list-style-type: none;">
        
    </ul>
</div>

</asp:Content>
