<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmTrafico.aspx.cs" Inherits="AppCasc.operation.frmTrafico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmTrafico.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />


<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Solicitud de Citas</h3>
<div id="div-solicitud-trafico" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<div>
    <label>Fecha de la Solicitud:</label>
    <asp:TextBox CssClass="cleanAfterApply" runat="server" ID="txt_fecha_solicitud"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_solicitud" ControlToValidate="txt_fecha_solicitud" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Fecha de carga solicitada:</label>
    <asp:TextBox CssClass="cleanAfterApply" runat="server" ID="txt_fecha_carga_solicitada"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_fecha_carga_solicitada" ControlToValidate="txt_fecha_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>
<div>
    <label>Hora de carga solicitada:</label>
    <asp:TextBox runat="server" ID="txt_hora_carga_solicitada" CssClass="horaPicker cleanAfterApply"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_hora_carga_solicitada" ControlToValidate="txt_hora_carga_solicitada" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
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
    <asp:TextBox runat="server" CssClass="txtLarge cleanAfterApply" ID="txt_destino"></asp:TextBox>
    <asp:RequiredFieldValidator CssClass="validator" runat="server" ID="rfv_destino" ControlToValidate="txt_destino" ErrorMessage="Es necesario proporcionar este dato"></asp:RequiredFieldValidator>
</div>

<div>
    <asp:Button runat="server" ID="btn_solicitar_cita" Text="Solicitar Cita"  OnClick="click_solicitar_trafico"/>
</div>
</div>

<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Solicitudes Sin Folio de Cita Asignado</h3>
<div id="div-citas" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:UpdatePanel runat="server" ID="up_trafico" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btn_solicitar_cita" EventName="Click" />
</Triggers>
<ContentTemplate>
    <asp:GridView runat="server" ID="grd_trafico_sin_citas" CssClass="grdCasc" AutoGenerateColumns="false" EmptyDataText="Sin solicitud de citas"
    DataKeyNames="id, id_transporte_tipo" OnRowCommand="grd_trafico_citas_row_command" OnRowDataBound="grd_trafico_citas_row_databound">
        <Columns>
            <asp:BoundField HeaderText="Fecha Solicitud" DataField="fecha_solicitud" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Fecha Carga Solicitada" DataField="fecha_carga_solicitada" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Hora Carga Solicitada" DataField="hora_carga_solicitada" DataFormatString="{0:HH:mm:ss}" />
            <asp:BoundField HeaderText="Tipo Solicitado" DataField="Transporte_tipo" />
            <asp:BoundField HeaderText="Tipo Carga" DataField="tipo_carga" />
            <asp:BoundField HeaderText="Destino" DataField="destino" />
            
            <asp:TemplateField HeaderText="Sin Cita Proporcionada">
            <ItemTemplate>
                <p><span>Solicita:</span>&nbsp;<span class="spn_title"><%# Eval("solicitante") %></span></p>
                <ol>
                    <li><span>Folio:</span>&nbsp;<a class="clsLnkFolioCita" id='<%# "lnk_folio_cita_" + Eval("Id") %>' href="#"><%# Eval("Folio_cita") %></a></li>
                    <li><span>Fecha:</span>&nbsp;<span id='<%# "spn_fecha_cita_" + Eval("Id") %>'><%# Eval("Fecha_cita", "{0:dd/MM/yyyy}") %></span></li>
                    <li><span>Hora:</span>&nbsp;<span id='<%# "spn_hora_cita_" + Eval("Id") %>'><%# Eval("Hora_cita") %></span></li>
                    <li><span>Línea:</span>&nbsp;<span><%# Eval("Transporte") %></span></li>
                    <li><span>Tipo:</span>&nbsp;<span><%# Eval("Transporte_tipo_cita") %></span></li>
                    <li class="hidden"><span>Operador:</span>&nbsp;<span><%# Eval("Operador") %></span></li>
                    <li class="hidden"><span>Placas:</span>&nbsp;<span><%# Eval("Placa") %></span></li>
                    <li class="hidden"><span>Caja:</span>&nbsp;<span><%# Eval("Caja") %></span></li>
                    <li class="hidden"><span>Contenedor 1:</span>&nbsp;<span><%# Eval("Caja1") %></span></li>
                    <li class="hidden"><span>Contenedor 2:</span>&nbsp;<span><%# Eval("Caja2") %></span></li>
                </ol>
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Capturar Nueva Cita">
                <ItemTemplate>
                    <div class="divForm">
                        <div>
                            <label id='<%# "lbl_folio_cita_" + Eval("Id") %>'>Folio Cita:</label>
                            <asp:TextBox CssClass="citaReq" runat="server" ID="txt_folio_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label id='<%# "lbl_fecha_cita_" + Eval("Id") %>'>Fecha Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_fecha" runat="server" ID="txt_fecha_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label id='<%# "lbl_hora_cita_" + Eval("Id") %>'>Hora Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_hora" runat="server" ID="txt_hora_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label>Tipo Transporte:</label>
                            <input type="hidden" value='<%# Eval("id") %>' id='<%# "spn_id_trafico_" + Eval("id") %>' />
                            <asp:DropDownList CssClass="classTransporte_tipo" runat="server" ID="ddl_transporte"></asp:DropDownList>
                        </div>
                        <div>
                            <label>Línea:</label>
                            <span title="Llenar listado de lineas de transporte" class="icon-button-action ui-icon ui-icon-refresh spn_TransporteGetByTipo"></span>
                            <asp:HiddenField runat="server" ID="hf_id_transporte" />
                            <select class="citaReq"><option></option></select>
                        </div>
                        <div>
                            <label>Operador:</label>
                            <asp:TextBox CssClass="txt_operador" runat="server" ID="txt_operador"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Placa:</label>
                            <asp:TextBox CssClass="citaNoReq txt_placa" runat="server" ID="txt_placa"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Caja:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja" runat="server" ID="txt_caja"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Contenedor 1:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja1" runat="server" ID="txt_caja1"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Contenedor 2:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja2" runat="server" ID="txt_caja2"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Button CausesValidation="false" CssClass="activaGuardarCita" runat="server" ID="btn_asignar_cita" Text="Guardar Cita" CommandName="udt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> 
                            <asp:Button CausesValidation="false" CssClass="eliminarCitaSinAsignar" runat="server" ID="btn_eliminar_cita" Text="Eliminar Cita" CommandName="dlt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> 
                        </div>
                    </div>
                    
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>


</div>

<h3 style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Solicitudes Con Folio de Cita Asignada</h3>
<div id="div1" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

<asp:UpdatePanel runat="server" ID="up_trafico_con_cita" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btn_solicitar_cita" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="grd_trafico_sin_citas" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
    <asp:GridView runat="server" ID="grd_trafico_con_citas" CssClass="grdCasc" AutoGenerateColumns="false" EmptyDataText="Sin solicitud de citas"
    DataKeyNames="id, id_transporte_tipo" OnRowCommand="grd_trafico_citas_row_command" OnRowDataBound="grd_trafico_citas_row_databound">
        <Columns>
            <asp:BoundField HeaderText="Fecha Solicitud" DataField="fecha_solicitud" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Fecha Carga Solicitada" DataField="fecha_carga_solicitada" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Hora Carga Solicitada" DataField="hora_carga_solicitada" DataFormatString="{0:HH:mm:ss}" />
            <asp:BoundField HeaderText="Tipo Solicitado" DataField="Transporte_tipo" />
            <asp:BoundField HeaderText="Tipo Carga" DataField="tipo_carga" />
            <asp:BoundField HeaderText="Destino" DataField="destino" />
            
            <asp:TemplateField HeaderText="Cita Proporcionada">
            <ItemTemplate>
                <p>
                    <span>Solicita:</span>&nbsp;<span class="spn_title"><%# Eval("solicitante") %></span>
                    <br />
                    <span>Asigna:</span>&nbsp;<span class="spn_title"><%# Eval("asignante") %></span>
                </p>

                <ol>
                    <li><span>Folio:</span>&nbsp;<a class="clsLnkFolioCita" id='<%# "lnk_folio_cita_" + Eval("Id") %>' href="#"><%# Eval("Folio_cita") %></a></li>
                    <li><span>Fecha:</span>&nbsp;<span id='<%# "spn_fecha_cita_" + Eval("Id") %>'><%# Eval("Fecha_cita", "{0:dd/MM/yyyy}") %></span></li>
                    <li><span>Hora:</span>&nbsp;<span id='<%# "spn_hora_cita_" + Eval("Id") %>'><%# Eval("Hora_cita") %></span></li>
                    <li><span>Línea:</span>&nbsp;<span><%# Eval("Transporte") %></span></li>
                    <li><span>Tipo:</span>&nbsp;<span><%# Eval("Transporte_tipo_cita") %></span></li>
                    <li class="hidden"><span>Operador:</span>&nbsp;<span><%# Eval("Operador") %></span></li>
                    <li class="hidden"><span>Placas:</span>&nbsp;<span><%# Eval("Placa") %></span></li>
                    <li class="hidden"><span>Caja:</span>&nbsp;<span><%# Eval("Caja") %></span></li>
                    <li class="hidden"><span>Contenedor 1:</span>&nbsp;<span><%# Eval("Caja1") %></span></li>
                    <li class="hidden"><span>Contenedor 2:</span>&nbsp;<span><%# Eval("Caja2") %></span></li>
                </ol>
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Modificar datos de la cita">
                <ItemTemplate>
                    <div class="divForm">
                        <div>
                            <label id='<%# "lbl_folio_cita_" + Eval("Id") %>'>Folio Cita:</label>
                            <asp:TextBox Enabled="false" CssClass="citaReq txtNoBorder" runat="server" ID="txt_folio_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label id='<%# "lbl_fecha_cita_" + Eval("Id") %>'>Fecha Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_fecha" runat="server" ID="txt_fecha_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label id='<%# "lbl_hora_cita_" + Eval("Id") %>'>Hora Cita:</label>
                            <asp:TextBox CssClass="citaReq txt_hora" runat="server" ID="txt_hora_cita"></asp:TextBox>
                        </div>
                        <div>
                            <label>Tipo Transporte:</label>
                            <input type="hidden" value='<%# Eval("id") %>' id='<%# "spn_id_trafico_" + Eval("id") %>' />
                            <asp:DropDownList CssClass="classTransporte_tipo" runat="server" ID="ddl_transporte"></asp:DropDownList>
                        </div>
                        <div>
                            <label>Línea:</label>
                            <span title="Llenar listado de lineas de transporte" class="icon-button-action ui-icon ui-icon-refresh spn_TransporteGetByTipo"></span>
                            <asp:HiddenField runat="server" ID="hf_id_transporte" />
                            <select class="citaReq"><option></option></select>
                        </div>
                        <div>
                            <label>Operador:</label>
                            <asp:TextBox CssClass="txt_operador" runat="server" ID="txt_operador"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Placa:</label>
                            <asp:TextBox CssClass="citaNoReq txt_placa" runat="server" ID="txt_placa"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Caja:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja" runat="server" ID="txt_caja"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Contenedor 1:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja1" runat="server" ID="txt_caja1"></asp:TextBox>
                        </div>
                        <div class="hidden">
                            <label>Contenedor 2:</label>
                            <asp:TextBox CssClass="citaNoReq txt_caja2" runat="server" ID="txt_caja2"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Button CausesValidation="false" CssClass="activaGuardarCita" runat="server" ID="btn_asignar_cita" Text="Guardar Cita" CommandName="udt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> 
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
