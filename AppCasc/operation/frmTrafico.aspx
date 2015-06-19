<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmTrafico.aspx.cs" Inherits="AppCasc.operation.frmTrafico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmTrafico.js?v1.1.150614_1904" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />


<div class="divForm">

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
    <label>Tipo de Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTipo_Transporte"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_tipo_transporte" InitialValue="" ControlToValidate="ddlTipo_Transporte" ErrorMessage="Es necesario seleccionar un tipo de transporte"></asp:RequiredFieldValidator>
</div>

<div>
    <label>Tipo de carga:</label>
    <asp:DropDownList runat="server" ID="ddlTipoCarga"></asp:DropDownList>
</div>

<div>
    <label>Destino:</label>
    <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_destino"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="rfv_destino" ControlToValidate="txt_destino" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>

<div>
    <asp:Button runat="server" ID="btn_solicitar_cita" Text="Solicitar Cita"  OnClick="click_solicitar_trafico"/>
</div>

<asp:UpdatePanel runat="server" ID="up_trafico" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btn_solicitar_cita" EventName="Click" />
</Triggers>
<ContentTemplate>
    <asp:GridView runat="server" ID="grd_trafico" CssClass="grdCasc" AutoGenerateColumns="false" EmptyDataText="Sin solicitud de citas">
        <Columns>
            <asp:BoundField HeaderText="Fecha Solicitud" DataField="fecha_solicitud" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Fecha Carga Solicitada" DataField="fecha_carga_solicitada" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Hora Carga Solicitada" DataField="hora_carga_solicitada" DataFormatString="{0:HH:mm:ss}" />
            <asp:BoundField HeaderText="Tipo de Transporte" DataField="tipo_transporte" />
            <asp:BoundField HeaderText="Tipo Carga" DataField="tipo_carga" />
            <asp:BoundField HeaderText="Destino" DataField="destino" />
            
            <asp:TemplateField HeaderText="Cita">
                <ItemTemplate>
                    <div class="divForm">
                        <div>
                            <label>Folio Cita:</label>
                            <asp:TextBox CssClass="citaReq" runat="server" ID="txt_folio_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label>Fecha Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_fecha" runat="server" ID="txt_fecha_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label>Hora Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_hora" runat="server" ID="txt_hora_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label>Línea:</label>
                            <span id='<%# "spn_" + Eval("id_transporte_tipo") %>' title="Llenar listado de lineas de transporte" class="icon-button-action ui-icon ui-icon-refresh spn_TransporteGetByTipo"></span>
                            <asp:HiddenField runat="server" ID="hf_id_transporte" />
                            <select class="citaReq" id='<%# "linea_" + Eval("id_transporte_tipo")  %>'><option></option></select>
                        </div>
                        <div>
                            <asp:Button CssClass="activaGuardarCita" runat="server" ID="btn_asignar_cita" Text="Guardar Cita" />
                        </div>
                    </div>
                    
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>


</div>

</asp:Content>
