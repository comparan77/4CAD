<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRemWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmRemWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../../js/moment.min.js" type="text/javascript"></script>
    <script src="../../js/operation/almacen/frmRemWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">B&uacute;squeda de Operaciones</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
<div id="div_busqueda">
    <label>C&oacute;digo Mercanc&iacute;a</label>
    <asp:TextBox runat="server" ID="txt_dato"></asp:TextBox>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" CausesValidation="false" />
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
        <hr style="border-color: transparent; clear: both;" />
        <asp:GridView runat="server" ID="grd_find_result" CssClass="grdCascSmall" AutoGenerateColumns="false" EmptyDataText="No existe mercancía para el código proporcionado">
        <Columns>
        <asp:BoundField DataField="mercancia_codigo" HeaderText="Código" />
        <asp:TemplateField HeaderText="Descripción de la Mercancía">
            <ItemTemplate>
                <span title='<%# Eval("mercancia_nombre") %>'><%# (Eval("mercancia_nombre").ToString().Length > 50) ? Eval("mercancia_nombre").ToString().Substring(0, 46) + "..." : Eval("mercancia_nombre") %></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="tarimas" HeaderText="Tar. Disp." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>
        <asp:BoundField DataField="bultos" HeaderText="Cjs. Disp." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>
        <asp:BoundField DataField="piezas" HeaderText="Pzs. Disp." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>

        <asp:BoundField DataField="TarRem" HeaderText="Tar. Rem." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>
        <asp:BoundField DataField="BtoRem" HeaderText="Cjs. Rem." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>
        <asp:BoundField DataField="PzaRem" HeaderText="Pzs. Rem." ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}"/>

        <asp:TemplateField HeaderText="Seleccionar" HeaderStyle-HorizontalAlign="Center">
        <ItemStyle HorizontalAlign="Center" />
        <ItemTemplate>
            <asp:LinkButton runat="server" CssClass="ui-icon ui-icon-arrow-1-w" CausesValidation="false" OnCommand="select_codigo" CommandArgument='<%# Eval("mercancia_codigo") %>'></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:GridView>

        <asp:Panel runat="server" ID="pnl_remisionesXCodigo" Visible="false">
        <label>Remisiones:</label>
        <ul>
            <asp:Repeater runat="server" ID="rep_rem">
            <ItemTemplate>
            <li class="floatLeft" style="margin-right: 5px;">
                <span class="icon-button-action rem" id='<%# "idRem_" + Eval("id") %>'><%# Eval("folio") %></span>
            </li>
            </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div style="clear: both;" ></div>
        </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</div>

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Remisi&oacute;n</h3>
<div id="div-control" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm hidden">

<asp:HiddenField runat="server" ID="hf_codigo" Value="" />
<asp:HiddenField runat="server" ID="hf_idUsuario" />
<div style="margin-left: 10px">

<asp:GridView runat="server" ID="grd_mercancia_disp" AutoGenerateColumns="false" CssClass="grdCascSmall" AllowSorting="true" OnSorting="sortMercanciaDisp" >
<Columns>
    <asp:BoundField DataField="mercancia_codigo" HeaderText="Código de Mercancía" />
    <asp:BoundField DataField="rr" HeaderText="Código RR" />
    <asp:BoundField DataField="mercancia_nombre" HeaderText="Descripción de la Mercancía" />
    <asp:BoundField DataField="estandar" HeaderText="Estandar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="tarimas" HeaderText="Tarimas" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
    <asp:BoundField DataField="bultos" HeaderText="Cajas" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
    <asp:BoundField DataField="piezas" HeaderText="Piezas" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
    <asp:TemplateField>
        <HeaderTemplate>
            <span class="ui-icon ui-icon-arrow-1-e"></span>
        </HeaderTemplate>
        <ItemTemplate>
            <span class="icon-button-action ui-icon ui-icon-arrow-1-e addTarima"></span>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Tarimas Rem.">
        <ItemStyle HorizontalAlign="Center" CssClass="totTarima"/>
        <ItemTemplate>
            0
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Cajas Rem.">
        <ItemStyle HorizontalAlign="Center" CssClass="totCaja"/>
        <ItemTemplate>
            0
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Piezas Rem.">
        <ItemStyle HorizontalAlign="Center" CssClass="totPieza"/>
        <ItemTemplate>
            0
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField>
        <ItemStyle HorizontalAlign="Center" />
        <HeaderTemplate>
            <span class="ui-icon ui-icon-trash"></span>
        </HeaderTemplate>
        <ItemTemplate>
            <span class="ui-icon ui-icon-trash icon-button-action remTarima hidden"></span>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField>
    <HeaderStyle CssClass="hidden" /> 
    <ItemStyle CssClass="hidden" />
    <ItemTemplate>
        <%#Eval("id_entrada") %>
    </ItemTemplate>
    </asp:TemplateField>
</Columns>
</asp:GridView>
</div>

<div style="margin-top: 10px;">
    <div>
        <label>Elabor&oacute;:</label>
        <asp:TextBox runat="server" ID="txt_elaboro"></asp:TextBox>
    </div>
    <div>
        
    </div>
    <div>
        <label>Folio Cita:</label><span id="spn_folio_cita" class="ui-icon ui-icon-calendar icon-button-action"></span><span id="spn_cita_sel"></span>
        <asp:TextBox CssClass="hidden" runat="server" ID="txt_folio_cita"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_folio_cita" ErrorMessage="Es necesario proporcionar el folio de la cita" ControlToValidate="txt_folio_cita"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hf_tarima_remision" />
        <asp:Button runat="server" ID="btn_save" Text="Guardar Remisión" OnClick="save_remision" />
    </div>
</div>

</div>

<!-- Listado de citas -->
<div id="div_citas" title="Listado de Citas">
    <ul id="ul_citas" style="list-style-type: none;">
        
    </ul>
</div>

<!-- Remisione elaborada -->
<div class="divForm" id="div_rem_info" style="position: relative;">
    <table class="grdCascSmall">
        <thead>
            <tr>
                <th align="center">RR</th>
                <th align="center">Estandar</th>
                <th align="center">Tarimas</th>
                <th align="center">Cajas</th>
                <th align="center">Piezas</th>
            </tr>
        </thead>
        <tbody id="tbl_rem_detail">
        </tbody>
    </table>

    <div style="margin-top: 10px">
        <label class="txtSmall">Tarimas:</label>
        <input type="text" class="txtNoBorder txtSmall" id="txt_tot_tar_read" />
    </div>

    <div style="margin-top: 0px">
        <label class="txtSmall">Folio Cita:</label>
        <input type="text" class="txtNoBorder txtSmall" id="txt_folio_cita_read" />
    </div>

    <div style="margin-top: 0px">
        <label class="txtSmall">Operador:</label>
        <input type="text" class="txtNoBorder txtSmall" id="txt_operador_read" />
    </div>

    <div style="margin-top: 0px">
        <label class="txtSmall">Transporte:</label>
        <input type="text" style="width: 150px;" class="txtNoBorder" id="txt_transporte_read" />
    </div>

    <div style="position: absolute; bottom: 15px; left: 10px;">
        <button id="print_rem">Imprimir Remisi&oacute;n</button>
    </div>
</div>

</asp:Content>
