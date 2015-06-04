<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmClienteVendorLst.aspx.cs" Inherits="AppCasc.catalog.frmClienteVendorLst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../js/catalog/frmLst.js" type="text/javascript"></script>
    <script src="../js/catalog/frmLstClienteVendor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />
<asp:HiddenField runat="server" ID="hfFkey" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Listado de Vendor de <asp:HyperLink runat="server" ID="lnkCliente"></asp:HyperLink></h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="div_catalog">
  <table id="grdCatalog" border="0" cellpadding="2" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th>Id Fiscal</th>
            <th>Vendor</th>
            <th>Proveedor</th>
            <th>Direcci&oacute;n</th>
            <th align="center" class="columnAction">Editar</th>
            <th align="center" class="columnAction">Estado</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="repRows" runat="server">
            <ItemTemplate>
                <tr id='<%# "row_" + Eval("id") %>'>
                    <td id="id_fiscal"><%#Eval("id_fiscal") %></td>
                    <td id="codigo"><%# Eval("codigo") %></td>
                    <td id="nombre"><%# Eval("nombre") %></td>
                    <td id="direccion"><%# Eval("direccion") %></td>
                    <td align="center"><a href='<%# "frmClienteVendor.aspx?Key=" + Eval("id") + "&Action=Udt" %>'><span class="ui-icon ui-icon-pencil spnIcon"></span></a></td>
                    <td align="center"><asp:LinkButton runat="server" CommandArgument='<%# Eval("IsActive") %>' CommandName='<%# Eval("id") %>' ID="lnk_change_status" CssClass='<%# "ui-icon ui-icon-circle-" + (Convert.ToBoolean(Eval("IsActive")) ? "check" : "close") + " spnIcon" %>' OnCommand="lnk_change_status_click"></asp:LinkButton> </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
  </table>  
</div>
</div>

</asp:Content>
