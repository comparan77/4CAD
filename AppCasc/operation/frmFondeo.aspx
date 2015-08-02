<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmFondeo.aspx.cs" Inherits="AppCasc.operation.frmFondeo" %>
<%@ Register src="../webControls/usrControlClienteMercancia.ascx" tagname="usrControlClienteMercancia" tagprefix="uc1" %>
<%@ Register src="../webControls/usrControlClienteVendor.ascx" tagname="usrControlClienteVendor" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>

    <link href="../css/jquery.combobox.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.combobox.js" type="text/javascript"></script>
    <script src="../js/webControls/ctrlClienteMercancia.js?v1.1.150619_1446" type="text/javascript"></script>
    <script src="../js/webControls/ctrlClienteVendor.js?v1.1.150619_1446" type="text/javascript"></script>

    <script src="../js/operation/frmFondeo.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Carga de fondeos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:HiddenField runat="server" ID="hf_MaxRequestLen" />

<asp:UpdatePanel runat="server" ID="up_ImportStep" UpdateMode="Conditional">
<Triggers>
    <asp:PostBackTrigger ControlID="btn_importar" />
    <asp:AsyncPostBackTrigger ControlID="btn_processFile" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:FileUpload runat="server" ID="fu_Folios" title="Selecciona un arcihvo XLS de Fondeo, posteriormente da clic en el Botón 'Importar Archivo'."/>
<asp:HiddenField runat="server" ID="hf_path" />
<asp:HiddenField runat="server" ID="hf_fecha_consulta" />
<asp:HiddenField runat="server" ID="hf_anio" />

<asp:UpdateProgress ID="upg_ImportStep" runat="server" AssociatedUpdatePanelID="up_ImportStep">
    <ProgressTemplate>
        Procesando archivo...
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:Panel runat="server" ID="pnl_datosFondeo" Visible="false">
<div class="divForm">
    <div>
        <label>Fecha:</label>
        <asp:TextBox runat="server" CssClass="txtFecha" ID="txt_fecha_fact"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_fact" ControlToValidate="txt_fecha_fact" ValidationGroup="datosFactura" ErrorMessage="Es necesario capturar la fecha de la factura."></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Importador:</label>
        <asp:DropDownList runat="server" ID="ddl_importador">
            <asp:ListItem>AVON COSMETICS MANUFACTURING S. DE R.L. DE C.V.</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Aduana de arribo:</label>
        <asp:DropDownList runat="server" ID="ddl_aduana"></asp:DropDownList>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_aduana" ControlToValidate="ddl_aduana" InitialValue="" ValidationGroup="datosFactura" ErrorMessage="Es necesario seleccionar la aduana de arribo."></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Button runat="server" ValidationGroup="datosFactura" Text="Procesar Archivo" ID="btn_processFile" OnClick="click_btn_processFile" />
    </div>
</div>
</asp:Panel>

<div id="divActions">
<asp:Button runat="server" CssClass="hidden" Text="Importar Archivo" ID="btn_importar" OnClick="click_btn_importar" />
</div>

<div>
    <uc1:usrControlClienteMercancia ID="usrControlClienteMercancia1" runat="server" />
    <uc1:usrControlClienteVendor ID="usrControlClienteVendor1" runat="server" />
</div>

<div>

<div><asp:Label runat="server" ID="lbl_NoFolios"></asp:Label></div>
<div>
    <asp:Label runat="server" ID="lbl_NoFoliosMsg"></asp:Label>
    <asp:HyperLink runat="server" ID="lnkFileDup" Visible="false"></asp:HyperLink>
</div>

</div>

<asp:GridView runat="server" Width="100%" CssClass="mGrid" ID="grd_reviewFile" AllowPaging="true" PageSize="25" AutoGenerateColumns="false"></asp:GridView>

</ContentTemplate>
</asp:UpdatePanel>

</div>

<h3 id="H1" style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">B&uacute;squeda de Fondeos</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<span id="spn_Search" class="ui-icon ui-icon-search icon-button-search icon-button-action"></span>

<div id="div_busqueda" style="display: none;">
    <label>Pedimento o folio de Fondeo:</label>
    <asp:TextBox runat="server" ID="txt_dato"></asp:TextBox>
    <asp:Button runat="server" ID="btn_buscar" OnClick="btn_buscar_click" Text="Buscar" CausesValidation="false" />
    <asp:UpdatePanel runat="server" ID="up_resultados" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
    </Triggers>
    <ContentTemplate>
        <asp:GridView runat="server" ID="grd_fondeo" CssClass="grdCasc" EmptyDataText="El Pedimento no cuenta con fodeo" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="folio" HeaderText="Folio" />
            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="importador" HeaderText="Importador" />
            <asp:BoundField DataField="aduana" HeaderText="Aduana" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="referencia" HeaderText="Referencia" />
            <asp:BoundField DataField="factura" HeaderText="Factura" />
            <asp:BoundField DataField="codigo" HeaderText="Código" />
            <asp:BoundField DataField="orden" HeaderText="Orden" />
            <asp:BoundField DataField="vendor" HeaderText="Vendor" />
            <asp:BoundField DataField="piezas" HeaderText="Piezas" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="valorfactura" HeaderText="Valor" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right"/>
        </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
    
</div>

</asp:Content>
