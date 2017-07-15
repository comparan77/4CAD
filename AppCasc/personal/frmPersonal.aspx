<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmPersonal.aspx.cs" Inherits="AppCasc.personal.frmPersonal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/catalog/frm.js?v1.1.150619_1446" type="text/javascript"></script>
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Personal</h3>
<div style="position: relative;" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="frmCatalog" class="divForm">
    <div>
        <label>Nombre:</label>
        <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_nombre"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txt_nombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Paterno:</label>
        <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_paterno"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvPaterno" ControlToValidate="txt_paterno" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>Materno:</label>
        <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_materno"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvMaterno" ControlToValidate="txt_materno" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label>R.F.C.:</label>
        <asp:TextBox runat="server" MaxLength="13" ID="txt_rfc"></asp:TextBox>
        <asp:CustomValidator ID="cvrfc" ControlToValidate="txt_rfc" ErrorMessage="RFC no Válido" ClientValidationFunction="validarRFC" ValidateEmptyText="true" runat="server"></asp:CustomValidator>
        
    </div>
    <div>
        <label>C.U.R.P.:</label>
        <asp:TextBox runat="server" MaxLength="18" ID="txt_curp"></asp:TextBox>
        <asp:CustomValidator ID="cvcurp" ControlToValidate="txt_curp" ErrorMessage="CURP no Válido" ClientValidationFunction="validarCURP" ValidateEmptyText="true" runat="server"></asp:CustomValidator>
    </div>
    <div>
        <label>NSS:</label>
        <asp:TextBox runat="server" MaxLength="11" ID="txt_nss"></asp:TextBox>
        <asp:CustomValidator ID="cvnss" ControlToValidate="txt_nss" ErrorMessage="NSS no Válido" ClientValidationFunction="validarNSS" ValidateEmptyText="true" runat="server"></asp:CustomValidator>
    </div>
    <div>
        <label>Género:</label>
        <asp:DropDownList runat="server" ID="ddl_genero">
            <asp:ListItem Value="1" Text="Hombre"></asp:ListItem>
            <asp:ListItem Value="0" Text="Mujer"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Empresa:</label>
        <asp:DropDownList runat="server" ID="ddl_empresa">
        </asp:DropDownList>
    </div>
    <div>
        <label>Rol:</label>
        <asp:DropDownList runat="server" ID="ddl_rol">
        </asp:DropDownList>
    </div>
    <div>
        <label>Boletinado:</label>
        <asp:CheckBox runat="server" ID="chk_boletinado" />
    </div>
    
    <div style="position: absolute; right: 5px; top: 5px;">

        <asp:Button CausesValidation="false" runat="server" ID="btn_udt_foto" Text="Actualiza Foto" OnClick="udtFoto_click" />
    <hr />

        <div class="portrait">
            <asp:UpdatePanel runat="server" ID="up_foto" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_udt_foto" EventName="click" />
            </Triggers>
            <ContentTemplate>
            <asp:Image runat="server" ID="img_photo" AlternateText="Sin Foto" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
            
    </div>

    <div>
        
        <label>QR Asignado:</label>
        <asp:UpdatePanel runat="server" ID="upQr" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_start" EventName="click" />
        </Triggers>
        <ContentTemplate>
            <asp:Label runat="server" ID="lbl_qr"></asp:Label>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button runat="server" ID="btn_start" Text="Actualizar QR" OnClick="udtQr_click" CausesValidation="false" />
    </div>

    <hr />
    <div id="divActions">   
        <asp:HiddenField runat="server" ID="hfAction" />
        <asp:HiddenField runat="server" ID="hfId" />
        <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" OnClick="btnCancel_click" CausesValidation="false" />
    </div>
</div>
</div>

</asp:Content>
