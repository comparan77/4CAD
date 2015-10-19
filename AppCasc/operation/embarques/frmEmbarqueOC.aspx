﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmEmbarqueOC.aspx.cs" Inherits="AppCasc.operation.embarques.frmEmbarqueOC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.updatepanel.js" type="text/javascript"></script>
    <script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../../js/operation/embarques/frmEmbarqueOC.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<asp:HiddenField runat="server" ID="hfLstDocumento" />

<div>
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Salida</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
<div>
    <label>Bodega:</label>
    <asp:DropDownList runat="server" ID="ddlBodega" AutoPostBack="true" Enabled="false"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvBodega" ControlToValidate="ddlBodega" InitialValue="" ErrorMessage="Es necesario seleccionar una bodega"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha:</label>
    <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
</div>
<div>
    <label>Hora de Salida:</label>
    <asp:TextBox id="txt_hora_salida" CssClass="horaPicker" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvHora_salida" ControlToValidate="txt_hora_salida" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
    <span class="hidden error">Es necesario proporcionar una hora</span>
</div>
<div>
    <label>Cortina:</label>
    <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Cliente:</label>
    <asp:DropDownList runat="server" ID="ddlCliente" OnSelectedIndexChanged="ddlCliente_changed" AutoPostBack="true"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCliente" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Es necesario seleccionar un cliente"></asp:RequiredFieldValidator>
</div>
</div>
</div>

<div style="margin-top: 5px;">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Orden de Carga</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div id="div_busqueda">
    <label>Folio orden de Carga:</label>
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
            <li><asp:LinkButton ID="LinkButton1" CssClass="lnk_result" runat="server" Text='<%# Eval("Folio_orden_carga") %>' CommandArgument='<%# Eval("Id") %>' CausesValidation="false"></asp:LinkButton></li>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label runat="server" ID="lbl_resultados" Visible="false" Text="Sin resultados para el folio proporcionado"></asp:Label>
        </FooterTemplate>
        </asp:Repeater>
    </ol>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

</div>
</div>

<div style="margin-top: 5px;">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Documentos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:UpdatePanel runat="server" ID="up_Rem" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="rep_resultados" EventName="ItemCommand" />
</Triggers>
<ContentTemplate>
    <asp:HiddenField runat="server" ID="hf_id_doc_req_by_cliente" />
    <asp:HiddenField runat="server" ID="hf_id_salida_orden_carga" />
    <div>
        <label>Cita:</label>
        <asp:TextBox ID="txt_folio_cita" runat="server" CssClass="txtNoBorder txtLarge"></asp:TextBox>
    </div>
    <div>
        <label>Fecha y hora de la cita:</label>
        <asp:TextBox runat="server" ID="txt_cita_fecha_hora" CssClass="txtNoBorder txtLarge"></asp:TextBox>
    </div>
    <div>
        <label>Destino:</label>
        <asp:TextBox runat="server" ID="txt_destino" CssClass="txtNoBorder txtLarge"></asp:TextBox>
    </div>
    <hr />
    <asp:GridView runat="server" ID="grd_rem" CssClass="grdCasc" AutoGenerateColumns="false" EmptyDataText="Sin remisiones" OnRowDataBound="grd_rem_databound" CellPadding="3">
    <RowStyle CssClass="revReferencia" />
    <Columns>
        <asp:BoundField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderText="Pedimento" DataField="Referencia" />
        <asp:TemplateField HeaderText="Documentos">
        <ItemTemplate>
            <div>
                <label>Tipo de documento:</label>
                <asp:DropDownList CssClass="txtMedium" runat="server" ID="ddl_documento"></asp:DropDownList>
            </div>    
            <div>
                <label>Referencia del documento:</label>
                <asp:TextBox runat="server" ID="txt_ref_doc"></asp:TextBox>
            </div>
            <div>
                <input type="button" class="add_doc" value="Agregar Documento" />
            </div>
            <div>
                <ul>
                    <li iddoc="3"><span>Remisi&oacute;n:</span>&nbsp;<span><%# Eval("PSalRem.Folio_remision")%></span></li>
                </ul>
                <asp:HiddenField runat="server" ID="hf_JsonDocumentos" />
            </div>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Bultos" ItemStyle-HorizontalAlign="Center" DataField="PSalRem.BultoTotal" DataFormatString="{0:N0}" />
        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Piezas" ItemStyle-HorizontalAlign="Center" DataField="PSalRem.PiezaTotal" DataFormatString="{0:N0}" />

        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Pallets">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_no_pallet" CssClass="txtNumber"></asp:TextBox>
            <br />
            <span class="validator" style="visibility: hidden; color: Red;">Es necesario capturar una cantidad</span>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Mercancía">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_mercancia" Text='<%# Eval("PSalRem.Mercancia") %>' TextMode="MultiLine" CssClass="requerido"></asp:TextBox>
            <br />
            <span class="validator" style="visibility: hidden; color: Red;">Es necesario capturar la descripción de la mercanc&iacute;a</span>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Observaciones">
        <ItemTemplate>
            <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" Columns="1" ID="txt_observaciones"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hf_forma" Value='<%# Eval("PSalRem.Forma") %>' />
        </ItemTemplate>
        </asp:TemplateField>

    </Columns>
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>

</div>
</div>


<div style="margin-top: 5px;">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Transporte</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:UpdatePanel runat="server" ID="up_transporte" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="rep_resultados" EventName="ItemCommand" />
</Triggers>
<ContentTemplate>

<div>
    <label>Linea:</label>
    <asp:DropDownList runat="server" ID="ddl_linea"></asp:DropDownList>
</div>
<div>
    <label>Tipo:</label>
    <asp:DropDownList runat="server" ID="ddl_tipo"></asp:DropDownList>
</div>
<div>
    <label>Placa:</label>
    <asp:TextBox id="txt_placa" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_placa" ControlToValidate="txt_placa" ErrorMessage="Es necesario capturar la placa para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Caja:</label>
    <asp:TextBox id="txt_caja" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja" ControlToValidate="txt_caja" ErrorMessage="Es necesario capturar la placa de la caja para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Contenedor 1:</label>
    <asp:TextBox id="txt_caja_1" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_1" ControlToValidate="txt_caja_1" ErrorMessage="Es necesario capturar el número de contenedor 1 para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Contenedor 2:</label>
    <asp:TextBox id="txt_caja_2" runat="server" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_caja_2" ControlToValidate="txt_caja_2" ErrorMessage="Es necesario capturar el número de contenedor 2 para este tipo de vehículo"></asp:RequiredFieldValidator>
</div>

<div>
    <label>Sello:</label>
    <asp:TextBox id="txt_sello" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvSello" ControlToValidate="txt_sello" ErrorMessage="Es necesario capturar el sello"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Carta Porte:</label>
    <asp:TextBox id="txt_talon" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCartaPorte" ControlToValidate="txt_talon" ErrorMessage="Es necesario capturar la carta porte"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Custodia:</label>
    <asp:DropDownList runat="server" ID="ddlCustodia"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvCustodia" ControlToValidate="ddlCustodia" InitialValue="" ErrorMessage="Es necesario seleccionar una custodia"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Operador:</label>
    <asp:TextBox runat="server" ID="txt_operador"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador de la custodia"></asp:RequiredFieldValidator>
</div>

</ContentTemplate>
</asp:UpdatePanel>

</div>
</div>

<div style="margin-top: 5px;">
<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Revisi&oacute;n</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Hora carga:</label>
    <asp:TextBox CssClass="horaPicker" id="txt_hora_carga" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_hora_carga" ControlToValidate="txt_hora_carga" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
    <span class="hidden error">Es necesario proporcionar una hora</span>
</div>
<div>
    <label>Vigilante:</label>
    <%--<asp:DropDownList runat="server" ID="ddlVigilante"></asp:DropDownList>--%>
    <asp:TextBox CssClass="txtMedium" runat="server" ID="txt_vigilante"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvVigilante" ControlToValidate="txt_vigilante" ErrorMessage="Es necesario proporcionar vigilante" ></asp:RequiredFieldValidator>
</div>

</div>
</div>

<div>
    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_click" />
</div>

</asp:Content>