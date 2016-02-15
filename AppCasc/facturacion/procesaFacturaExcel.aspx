<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="procesaFacturaExcel.aspx.cs" Inherits="AppCasc.facturacion.procesaFacturaExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/facturacion/procesaFacturaExcel.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Importaci&oacute; de Archivo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

    <div id="importarFactruracion">
        <asp:HiddenField runat="server" ID="hf_MaxRequestLen" />
        <asp:HiddenField runat="server" ID="hf_path" />
        <asp:FileUpload runat="server" ID="fileup_facturacion" />
        <asp:Button runat="server" CssClass="hidden" Text="Importar Archivo" ID="btn_importar" OnClick="click_btn_importar" />
    </div>

</div>

<h3 style="margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Elaboraci&oacute;n de Archivo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

    <asp:Button runat="server" ID="btn_processFile" Visible="false" Text="Procesar Archivo" OnClick="click_btn_processFile" />
    <asp:UpdatePanel runat="server" ID="up_ImportStep" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_processFile" EventName="click" />
    </Triggers>
    <ContentTemplate>
        <asp:HyperLink runat="server" ID="lnkFile"></asp:HyperLink>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="up_procesa_prgss">
    <ProgressTemplate>
        
        Generando el archivo...<span class="ui-icon ui-icon-clock" ></span>
        
    </ProgressTemplate>
    </asp:UpdateProgress>

</div>

</asp:Content>
