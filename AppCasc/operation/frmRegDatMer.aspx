<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRegDatMer.aspx.cs" Inherits="AppCasc.operation.frmRegDatMer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">&Aacute;rea de pegado de informaci&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<!-- Copy-paste data <<ini>>-->
<div style="">
<asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"  Width="99%" Rows="20"></asp:TextBox>
</div>
<div>
<asp:Button runat="server" ID="btn_loaddata" Text="Cargar información" OnClick="click_load" />
</div>
</div>

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Pedidos procesados</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<asp:DataGrid runat="server" CssClass="grdCasc" ID="grdProcesados" AutoGenerateColumns="false">
<Columns>
<asp:BoundColumn DataField="Proveedor" HeaderText="Proveedor"></asp:BoundColumn>
<asp:BoundColumn DataField="Trafico" HeaderText="Tráfico" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
<asp:BoundColumn DataField="Pedido" HeaderText="Pedidos" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
<asp:BoundColumn DataField="Piezas" HeaderText="Piezas" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
<asp:BoundColumn DataField="Fecha_confirma" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Confirmado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
</Columns>
</asp:DataGrid>
</div>



</asp:Content>
