<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmMaq.aspx.cs" Inherits="AppCasc.operation.frmMaq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmMaq.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Maquila</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

    <div class="divForm" style="margin-bottom: 1em">
        <div>
            <label>No. orden de trabajo:</label>
            <asp:TextBox runat="server" ID="txt_folio" CausesValidation="true" OnTextChanged="txt_folio_changed" AutoPostBack="true"></asp:TextBox>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="up_info_ot" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txt_folio" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>
            <div id="tabs" style="width: 99%">
                <ul>
                    <li><a href="#tabs-1">Informaci&oacute;n General</a></li>
                    <li><a href="#tabs-2">Proin dolor</a></li>
                    <li><a href="#tabs-3">Aenean lacinia</a></li>
                </ul>
                <div id="tabs-1">
                    <div>
                        <label>Fecha:</label>
                        <asp:TextBox runat="server" ID="txt_fecha" CssClass="txtNoBorder" ReadOnly="true"></asp:TextBox>
                    </div>
                    <asp:GridView CssClass="grdCascSmall" runat="server" ID="grd_servicios" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PServ.Nombre" HeaderText="Servicio" />
                        <asp:BoundField DataField="Ref1" HeaderText="Trafico" />
                        <asp:BoundField DataField="Ref2" HeaderText="Referencia" />
                        <asp:BoundField DataField="Piezas" HeaderText="Piezas" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="PiezasMaq" HeaderText="Piezas Maquiladas"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    </Columns>
                    </asp:GridView>
                </div>
                <div id="tabs-2">
                    <p>Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
                </div>
                <div id="tabs-3">
                    <p>Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate, pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem. Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem enim, pretium nec, feugiat nec, luctus a, lacus.</p>
                    <p>Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit hendrerit.</p>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

</asp:Content>
