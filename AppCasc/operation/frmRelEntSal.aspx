<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRelEntSal.aspx.cs" Inherits="AppCasc.operation.frmRelEntSal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../js/catalog/frmLst.js?v1.1.150619_1446" type="text/javascript"></script>
    <script src="../js/operation/frmRelEntSal.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div id="div_EntSal">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<div id="div_panel">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Entradas y Salidas Generadas</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div class="divForm">
<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega" OnSelectedIndexChanged="ddlBodega_changed" AutoPostBack="true"></asp:DropDownList>
</div>
<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" OnSelectedIndexChanged="ddlCliente_changed" AutoPostBack="true"></asp:DropDownList>
</div>
<div>
    <label>Fecha Inicial:</label>
    <asp:TextBox runat="server" ID="txt_fecha_ini" ></asp:TextBox>
</div>
<div>
    <label>Fecha Final:</label>
    <asp:TextBox runat="server" ID="txt_fecha_fin" ></asp:TextBox>
</div>
<div>
    <asp:Button runat="server" Text="Entradas" ID="btnGetEnt" OnClick="btnGetEnt_click" />
    <asp:Button runat="server" Text="Salidas" ID="btnGetSal" OnClick="btnGetSal_click" />
</div>
</div>
<hr />

<div id="div_catalog">
  <table id="grdCatalog" border="0" cellpadding="2" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th class="bodega">Bodega</th>
            <th>Fecha</th>
            <th>Folio</th>
            <th>Referencia</th>
            <th class="cliente">Cliente</th>
            <th>Mercanc&iacute;a</th>
            <th align="center" class="columnAction">Imprimir</th>
            <th align="center" class="columnAction">Editar</th>
            <th align="center" class="columnAction">Cancelar</th>
            <th class="hidden">&nbsp;</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="repRows" runat="server">
            <ItemTemplate>
                <tr id='<%# "row_" + Eval("id") %>'>
                    <td class="bodega"><%# Eval("BODEGA")%></td>
                    <td class="fecha"><%# Eval("FECHA DE OPERACION", "{0:dd/MM/yyyy}")%></td>
                    <td class="folio"><%# Eval("FOLIO E/S")%></td>
                    <td class="referencia"><%# Eval("REFERENCIA")%></td>
                    <td class="cliente"><%# Eval("CUENTA") %></td>
                    <td class="mercancia"><%# Eval("MERCANCIA") %></td>
                    <td align="center"><asp:LinkButton runat="server" CommandArgument='<%# Eval("MOVIMIENTO") %>' CommandName='<%# Eval("id") %>' ID="lnk_change_status" CssClass="ui-icon ui-icon-print spnIcon" OnCommand="lnk_print_click"></asp:LinkButton> </td>
                    <td align="center" title='<%# Eval("motivo_cancelacion") %>'>
                        <a href='<%# (Convert.ToBoolean(Eval("IsActive")) ? "frmEditMov.aspx?Key=" + Eval("id") + "&Action=" + Eval("MOVIMIENTO") : "#") %>'><span class='<%# (Convert.ToBoolean(Eval("IsActive")) ? "ui-icon ui-icon-pencil spnIcon" : "ui-icon ui-icon-cancel spnIcon") %>'></span></a>
                    </td>
                    <td align="center" title='<%# Eval("motivo_cancelacion") %>'>
                        <a href='<%# (Convert.ToBoolean(Eval("IsActive")) ? "frmCancelDoc.aspx?Key=" + Eval("id") + "&Action=" + Eval("MOVIMIENTO") : "#") %>'><span class='<%# (Convert.ToBoolean(Eval("IsActive")) ? "ui-icon ui-icon-trash spnIcon" : "ui-icon ui-icon-cancel spnIcon") %>'></span></a>
                    </td>
                    <td class="hidden"></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<asp:HiddenField runat="server" ID="hfMotivo" />
</div>
</div>
</div>

</div>

<div id="msgConfirmCancel" title="¿Desea eliminar esta operación?" style="display: none;" >
    <div>
        <label>Motivo:</label>
        <asp:TextBox TextMode="MultiLine" Columns="1" Rows="3" runat="server" ID="txt_motivo" ></asp:TextBox>
        <input type="hidden" id="hfId" />
    </div>    
</div>
</asp:Content>
