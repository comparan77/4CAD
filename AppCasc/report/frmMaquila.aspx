<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmMaquila.aspx.cs" Inherits="AppCasc.report.frmMaquila" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/report/frmMaquila.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Reporte de Maquila</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

<asp:GridView runat="server" ID="grd_maquila" AutoGenerateColumns="false" CssClass="grdCasc" OnRowCommand="click_row_detail" DataKeyNames="id_inventario" EmptyDataText="Sin Movimientos">
<Columns>
    <asp:BoundField DataField="fecha_arribo" HeaderText="Fecha de Arribo" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
    <asp:TemplateField HeaderText="Referencia" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <asp:LinkButton runat="server" ID="lnkReferencia" ToolTip='<%# Eval("orden_compra") + "," + Eval("codigo") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Bind("referencia") %>' ></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="pza_recibida" HeaderText="Pzas Recibidas" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
    <asp:BoundField DataField="fecha_inventario" HeaderText="Fecha de Inventario" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="pza_inventario" HeaderText="Pzas Inventario" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
    <asp:BoundField DataField="pza_maquilado" HeaderText="Pzas Maquila" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
    <asp:BoundField DataField="pza_no_maquilado" HeaderText="Pzas sin Maquila" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
    <asp:BoundField DataField="ult_fecha_trabajo" HeaderText="Ultima Fecha de Trabajo" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" NullDisplayText="-" />
</Columns>
</asp:GridView>

<asp:UpdatePanel runat="server" ID="up_maquila" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grd_maquila" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
    <h5><asp:Label runat="server" ID="lbl_codigo_orden"></asp:Label></h5>
    <asp:GridView runat="server" ID="grd_detail" EmptyDataText="Mercancía sin maquilar" CssClass="grdCasc" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="fecha_trabajo" HeaderText="Fecha de Trabajo" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="pallet" HeaderText="Pallets" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
        <asp:BoundField DataField="bulto" HeaderText="Bultos" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
        <asp:BoundField DataField="Pieza" HeaderText="Piezas" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="number" />
    </Columns>
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>

</div>
</div>

</asp:Content>
