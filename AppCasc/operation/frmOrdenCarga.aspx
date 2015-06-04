<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdenCarga.aspx.cs" Inherits="AppCasc.operation.frmOrdenCarga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/operation/helperRemDetail.js" type="text/javascript"></script>
    <script src="../js/operation/frmOrdenCarga.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Orden de Carga</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Fecha de la Solicitud:</label>
    <asp:TextBox runat="server" ID="txt_fecha_solicitud"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_fecha_solicitud" ControlToValidate="txt_fecha_solicitud" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha de carga solicitada:</label>
    <asp:TextBox runat="server" ID="txt_fecha_carga_solicitada"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_fecha_carga_solicitada" ControlToValidate="txt_fecha_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Hora de carga solicitada:</label>
    <asp:TextBox runat="server" ID="txt_hora_carga_solicitada" CssClass="horaPicker"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_hora_carga_solicitada" ControlToValidate="txt_hora_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha de cita:</label>
    <asp:TextBox runat="server" ID="txt_fecha_cita"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_fecha_cita" ControlToValidate="txt_fecha_cita" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Hora de cita:</label>
    <asp:TextBox runat="server" ID="txt_hora_cita"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_hora_cita" ControlToValidate="txt_hora_cita" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Tipo de carga:</label>
    <asp:DropDownList runat="server" ID="ddlTipoCarga"></asp:DropDownList>
</div>
<div>
    <label>Destino:</label>
    <asp:TextBox runat="server" ID="txt_destino"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_destino" ControlToValidate="txt_destino" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvTransporte" ControlToValidate="ddlTransporte" InitialValue="" ErrorMessage="Es necesario seleccionar un transporte"></asp:RequiredFieldValidator>
</div>
<div>
<asp:UpdatePanel runat="server" ID="upTipoTransporte" UpdateMode="Conditional">
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlTransporte" EventName="SelectedIndexChanged" />
</Triggers>
<ContentTemplate>
    <label>Tipo de Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTipo_Transporte"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_tipo_transporte" InitialValue="" ControlToValidate="ddlTipo_Transporte" ErrorMessage="Es necesario seleccionar un tipo de transporte"></asp:RequiredFieldValidator>
</ContentTemplate>
</asp:UpdatePanel>
</div>


</div>


<h3 style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Documentos</h3>
<div id="div-control-piso" style="clear: right;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

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
            <li><asp:LinkButton ID="LinkButton1" runat="server" CssClass="lnk_result" Text='<%# Eval("folio") + ", " + Eval("referencia") %>' CommandArgument='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton></li>
            <ul style="list-style-type:circle;">
                
                <asp:Repeater runat="server" ID="repOrdCod" OnItemDataBound="repOrdCod_ItemDataBound">
                <ItemTemplate>
                    <li><asp:LinkButton ID="lnkMaquilado" runat="server" Text='<%# Eval("orden_compra") + ", " + Eval("codigo") + ", " + Eval("mercancia") %>' CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>'></asp:LinkButton></li>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lbl_repOrdCod" Visible="false" Text="Sin inventario capturado"></asp:Label>
                </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="repRemision" Visible="false" OnItemDataBound="repRemision_itemDataBound" OnItemCreated="repRemision_itemCreated">
                    <ItemTemplate>
                        <li>
                            <asp:HiddenField runat="server" ID="hf_estatus" Value='<%# Eval("Id_estatus") %>' />
                            <span><%# Eval("folio_remision") + ", " + Eval("orden") + ", " + Eval("codigo") + ", " + Eval("mercancia") %></span>&nbsp;
                            <asp:LinkButton ID="lnkRemision" runat="server" Text="Agregar" OnCommand="addRemision" CommandArgument='<%# Eval("id") + "|" + Eval("id_entrada")%>' CausesValidation="false"></asp:LinkButton>
                        </li>
                    </ItemTemplate>
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

<div id="div-ordenes-carga">

<asp:UpdatePanel runat="server" ID="up_ordenes" UpdateMode="Always">
<ContentTemplate>
    <asp:GridView runat="server" ID="grd_ordenCarga" AutoGenerateColumns="false" CssClass="grdCasc" DataKeyNames="id" EmptyDataText="Sin Operaciones agregadas" OnDataBound="grd_ordenCarga_DataBound">
    <Columns>
        <asp:BoundField DataField="codigo_cliente" HeaderText="Folio Cliente" HeaderStyle-HorizontalAlign="Left" />
        
        <asp:TemplateField HeaderText="Folio Remisión" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <a href="#" class="lnk-folio-remision" title='<%#Eval("fecha_remision","{0:dd/MM/yyyy}") %>' ><%# Eval("folio_remision") %></a>
                <%--<input type="hidden" value='<%#Eval("bulto", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezaxbulto", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("pieza", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("bultoinc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezaxbultoinc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezainc", "{0:N0}") %>' />
                <input type="hidden" value='<%#Eval("piezatotal", "{0:N0}") %>' />--%>
                <input type="hidden" value='<%#Eval("dano_especifico") %>' />
                <input type="hidden" value='<%#Eval("id") %>' />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="referencia" HeaderText="Referencia" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="codigo" HeaderText="Código" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="orden" HeaderText="Orden de compra" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="mercancia" HeaderText="Mercancía" HeaderStyle-HorizontalAlign="Left" />
        <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
            <asp:LinkButton CssClass="ui-icon ui-icon-trash" runat="server" ID="lnkDeleteRem" CommandArgument='<%#Eval("id") %>' OnCommand="deleteRem" CausesValidation="false"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="hf_remisiones_count" Value="0" />
</ContentTemplate>
</asp:UpdatePanel>

<div style="margin-top: 15px">
    <asp:Button runat="server" ID="btnSaveOrden" Text="Guardar Orden de Carga" OnClick="click_saveOrden" />
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
        
    </div>

</div>

</div>


</div>



</asp:Content>
