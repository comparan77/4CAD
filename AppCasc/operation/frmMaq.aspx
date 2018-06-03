<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmMaq.aspx.cs" Inherits="AppCasc.operation.frmMaq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmMaq.js?v1.1.150619_1446" type="text/javascript"></script>
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
            <asp:PostBackTrigger ControlID="grd_servicios" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hf_id_orden_trabajo" />
            
            <div class="divForm">
                <div>
                    <label>Fecha:</label>
                    <asp:TextBox runat="server" ID="txt_fecha" CssClass="txtNoBorder" ReadOnly="true"></asp:TextBox>
                </div>
                <div>
                    <label>Estatus:</label>
                    <span id="spn_estado_ot" title='<%= VSOrdTbj.Cerrada ? "Abrir Maquila" : "Cerrar Maquila" %>' class='<%= "icon-button-action ui-icon ui-icon-" + (VSOrdTbj.Cerrada ? "locked icon-button-action" : "unlocked") %>'></span>
                </div>
            </div>
            <hr style="border-color: transparent" />
            <div style="clear: both;">
                <asp:GridView DataKeyNames="Id" 
                CssClass="grdCascSmall" 
                runat="server" 
                ID="grd_servicios" 
                AutoGenerateColumns="false" 
                OnRowCommand="grd_servicios_row_command" 
                SelectedRowStyle-BackColor="CornflowerBlue"
                ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="PServ.Nombre" HeaderText="Servicio" />
                    <asp:BoundField DataField="Ref1" HeaderText="Trafico" />
                    <asp:BoundField DataField="Ref2" HeaderText="Referencia" />
                    <asp:BoundField DataField="Parcial" HeaderText="Parcial" />
                    <asp:BoundField DataField="Piezas" HeaderText="Piezas Solicitadas" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="PalletMaq" HeaderText="Pallets Maquilados" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="BultosMaq" HeaderText="Bultos Maquilados" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="PiezasMaq" HeaderText="Piezas Maquiladas"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="Faltantes" ItemStyle-CssClass="ot_faltantes" HeaderText="Piezas Faltantes" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="Sobrantes" ItemStyle-CssClass="ot_sobrantes" HeaderText="Piezas Sobrantes" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:ButtonField ButtonType="Link" DataTextField="PasosMaq" HeaderText="Pasos" CommandName="lnkPasos" ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:N0}" ItemStyle-CssClass="icon-button-action" />
                    <asp:ButtonField ButtonType="Image" ControlStyle-CssClass="ui-icon ui-icon-print" CommandName="lnkPrint" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                </asp:GridView>

                <hr style="border-color: transparent" />

                <asp:GridView runat="server" ID="grd_pasos" AutoGenerateColumns="false" CssClass="grdCascSmall">
                <Columns>
                    <asp:BoundField DataField="NumPaso" HeaderText="Num Paso" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Foto">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image Width="25%" ID="imgPaso" runat="server" ImageUrl='<%# @"~/rpt/maqpas/" + Eval("Foto64") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                </Columns>
                </asp:GridView>
            </div>
                
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

</asp:Content>
