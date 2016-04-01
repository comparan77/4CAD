<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmArriboWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmArriboWH" %>
<%@ Register src="../../webControls/usrControlClienteMercancia.ascx" tagname="usrControlClienteMercancia" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
<link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
<link href="../../css/jquery.combobox.css" rel="stylesheet" type="text/css" />
    
<script src="../../js/jquery.combobox.js" type="text/javascript"></script>
<script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
<script src="../../js/jquery.maskedinput.min.js" type="text/javascript"></script>
<script src="../../js/webControls/ctrlClienteMercancia.js?v1.1.150619_1446" type="text/javascript"></script>
<script src="../../js/operation/almacen/frmArriboWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Datos Generales</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>Bodega:</label>
        <asp:DropDownList runat="server" ID="ddlBodega" AutoPostBack="true" OnSelectedIndexChanged="changeBodega" CausesValidation="false"></asp:DropDownList>
    </div>
    <asp:UpdatePanel runat="server" ID="up_bodega" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlBodega" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <label>Cortina:</label>
        <asp:DropDownList runat="server" ID="ddlCortina"></asp:DropDownList>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvCortina" ControlToValidate="ddlCortina" InitialValue="" ErrorMessage="Es necesario seleccionar una cortina"></asp:RequiredFieldValidator>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <label>Fecha del Arribo:</label>
        <asp:TextBox id="txt_fecha" ReadOnly="true" CssClass="txtDateTime" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Hora del Arribo:</label>
        <asp:TextBox id="txt_hora_llegada" CssClass="horaPicker" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfvHora_llegada" ControlToValidate="txt_hora_llegada" ErrorMessage="Es necesario proporcionar una hora" ></asp:RequiredFieldValidator>
        <span class="hidden error">Es necesario proporcionar una hora</span>
    </div>
    <div>
        <label>C&oacute;digo RR</label>
        <asp:TextBox runat="server" ID="txt_rr"></asp:TextBox>
    </div>
</div>

<h3 id="H1" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Informaci&oacute;n de la Mercanc&iacute;a</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div>
        <label>C&oacute;digo</label>        
        <asp:TextBox runat="server" ID="txt_mercancia_codigo"></asp:TextBox>
        <span class="hidden error">Es necesario proporcionar un c&oacute;digo v&acute;lido</span>
    </div>
    <div>
        <label>Descripci&oacute;n</label>
        <asp:TextBox runat="server" ID="txt_mercancia_desc"></asp:TextBox>
    </div>
    <div>
    
    </div>

</div>



<div>
    <uc1:usrControlClienteMercancia ID="usrControlClienteMercancia1" runat="server" />
</div>

</asp:Content>
