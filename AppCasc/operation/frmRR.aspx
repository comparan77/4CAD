<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmRR.aspx.cs" Inherits="AppCasc.operation.frmRR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/operation/frmRR.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Remisiones</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda">
    <label>Remisi&oacute;n (a&ntilde;o-n&uacute;mero)</label>
    <asp:TextBox runat="server" ID="txt_dato"></asp:TextBox>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" CausesValidation="false" />
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlNotFound" Visible="false" runat="server">
            <span class="error">No existe el n&uacute;mero de remisi&oacute;n proporcionado.</span>
        </asp:Panel>

        <asp:HiddenField runat="server" ID="hf_id_salida_remision" />

        <asp:Panel ID="pnlFound" Visible="false" runat="server">
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
                <span><%= oE.Referencia %></span>
            </div>
            <div>
                <label>Remisi&oacute;n:</label>
                <span class="spn_title"><%= oSR.Folio_remision %></span>
            </div>

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
                        <td align="center" id="mercancia"><span class="spn_title">Descripci&oacute;n<br /></span><%=oEI.Mercancia %></td>
                        <td align="center" id="vendor"><span class="spn_title">No. de Proveedor<br /></span><%=oCV.Codigo %><br />
                            <img id="img-vendor" src="" alt="" />
                            <asp:HiddenField runat="server" ID="hf_img_vendor" Value="" />
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="4" align="center">
                            <table id="tbl_cantidades_salida" cellpadding="5" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <label>Total CTNS:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_bulto_1" CssClass="txtNumber"  Text="0"></asp:TextBox>
                                            <label>Cartones</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_piezasXbulto_1" CssClass="txtNumber" Text="0"  ></asp:TextBox>
                                            <label>Piezas C/U</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_piezas_1" CssClass="txtNumber" Text="0"  ></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <label>Total CTNS:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_bulto_2" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                            <label>Cartones</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_piezasXbulto_2" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                            <label>Piezas C/U</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="true" runat="server" ID="txt_piezas_2" CssClass="txtNumber" Text="0" ></asp:TextBox>
                                        </td>
                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="right">Total Piezas</td>
                                        <td><asp:TextBox ReadOnly="true" runat="server" ID="txt_piezaTotal" CssClass="tdCantSalidaTotalPieza txtNumber" Text="0"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div>
                                                <label>RR:</label>
                                                <asp:TextBox runat="server" ID="txt_RR"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div>
                                                <label>Fecha:</label>
                                                <asp:TextBox runat="server" ID="txt_fecha_rr"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="4">
                                        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_rr" ControlToValidate="txt_RR" ErrorMessage="Es necesario proporcionar la etiqueta RR"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_rr" ControlToValidate="txt_fecha_rr" ErrorMessage="Es necesario proporcionar la fecha"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2">
                                            <asp:UpdatePanel runat="server" ID="up_saveData" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger  ControlID="btn_guardar" EventName="click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btn_guardar" Text="Guardar RR" OnClick="guardar_RR" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </td>
                    </tr>
                </tfoot>
            </table>

        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

</div>

</div>

</asp:Content>
