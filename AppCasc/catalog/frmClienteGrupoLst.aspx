﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmClienteGrupoLst.aspx.cs" Inherits="AppCasc.catalog.frmClienteGrupoLst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />

    <%--<script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>--%>
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../js/catalog/frmLst.js" type="text/javascript"></script>
    <script src="../js/catalog/frmLstClienteGrupo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Listado de Grupos de Cliente</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="div_catalog">
  <table id="grdCatalog" border="0" cellpadding="2" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th>Grupo</th>
            <th align="center" class="columnAction">Editar</th>
            <th align="center" class="columnAction">Estado</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="repRows" runat="server">
            <ItemTemplate>
                    <tr id='<%# "row_" + Eval("id") %>'>
                    <td><%# Eval("nombre") %></td>
                    <td align="center"><a href='<%# "frmClienteGrupo.aspx?Key=" + Eval("id") + "&Action=Udt" %>'><span class="ui-icon ui-icon-pencil spnIcon"></span></a></td>
                    <td align="center"><asp:LinkButton runat="server" CommandArgument='<%# Eval("IsActive") %>' CommandName='<%# Eval("id") %>' ID="lnk_change_status" CssClass='<%# "ui-icon ui-icon-circle-" + (Convert.ToBoolean(Eval("IsActive")) ? "check" : "close") + " spnIcon" %>' OnCommand="lnk_change_status_click"></asp:LinkButton> </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
  </table>  
</div>
</div>

</asp:Content>
