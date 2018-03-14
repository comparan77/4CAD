<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmAudUni.aspx.cs" Inherits="AppCasc.operation.frmAudUni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/operation/frmAudUni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<div id="tabs">
    <ul>
        <li><a href="#tabs-1">Registro</a></li>
        <li><a href="#tabs-2">Consulta</a></li>
    </ul>
    <div id="tabs-1">
        <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Bodega</h3>
        <div id="div2" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
            <div>
                <label>Bodega:</label>
                <asp:DropDownList runat="server" ID="ddlBodega" OnSelectedIndexChanged="ddlBodega_changed" AutoPostBack="true"></asp:DropDownList>
            </div>
            <asp:UpdatePanel runat="server" ID="up_prev_pred_user" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBodega" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hf_usr_prv_perd" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Destino</h3>
        <div id="div_transporte" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    
            <div>
                <label>C.P.:</label>
                <asp:TextBox runat="server" ID="txt_cp"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_txt_cp" ControlToValidate="txt_cp" ErrorMessage="Es necesario proporcionar el codigo postal"></asp:RequiredFieldValidator>
                <button id="btn_valid_cp">Validar c&oacute;digo postal</button>
            </div>

            <div>
                <label>Calle y n&uacute;mero</label>
                <asp:TextBox runat="server" ID="txt_calle_num"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_calle_num" ControlToValidate="txt_calle_num" ErrorMessage="Es necesario proporcionar la calle y número"></asp:RequiredFieldValidator>        
            </div>

            <div>
                <label>Estado</label>
                <asp:TextBox runat="server" ID="txt_estado"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_estado" ControlToValidate="txt_estado" ErrorMessage="Es necesario proporcionar el estado"></asp:RequiredFieldValidator>        
            </div>

            <div>
                <label>Municipio o Del.</label>
                <asp:TextBox runat="server" ID="txt_municipio"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_municipio" ControlToValidate="txt_municipio" ErrorMessage="Es necesario proporcionar el municipio o delegación"></asp:RequiredFieldValidator>        
            </div>

            <div id="div_colonia" title="Colonias">
                <select id="ddl_colonia"></select>
            </div>

            <div>
                <label>Colonia</label>
                <asp:TextBox runat="server" ID="txt_colonia"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfv_colonia" ControlToValidate="txt_colonia" ErrorMessage="Es necesario proporcionar la colonia"></asp:RequiredFieldValidator>
            </div>

        </div>

        <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Transporte</h3>
        <div id="div1" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

        <div>
                <label>Transporte:</label>
                <%--<asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>--%>
                <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
            </div>
            <div>
                <label>Tipo de Transporte:</label>
            <asp:UpdatePanel runat="server" ID="upTipoTransporte" UpdateMode="Conditional">
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTransporte" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddlTipo_Transporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_Transporte_changed"></asp:DropDownList>        
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>

            <asp:UpdatePanel runat="server" ID="upDatosVehiculo" UpdateMode="Conditional">
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTipo_Transporte" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
            <asp:HiddenField runat="server" ID="hf_cond_trans" />
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
            </ContentTemplate>
            </asp:UpdatePanel>

            <div>
                <label>Operador:</label>
                <asp:TextBox runat="server" ID="txt_operador"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validator" ID="rfvOperador" ControlToValidate="txt_operador" ErrorMessage="Es necesario capturar el operador"></asp:RequiredFieldValidator>
            </div>

            <div style="margin-left: 205px; margin-top: 10px;">
            <asp:HiddenField runat="server" ID="hf_condiciones_transporte" />
            <span style="color: Red; visibility: hidden;" class="validator" id="rfv_condiciones_transporte">Es necesario proporcionar TODAS LAS CONDICIONES del transporte.</span>
                <table class="grdCascSmall">
                    <thead>
                        <tr>
                            <th>&nbsp;</th>
                            <th>Revisi&oacute;n de la unidad</th>
                            <th>S&iacute;</th>
                            <th>No</th>
                        </tr>
                    </thead>
                    <tbody id="tbody_condiciones">
                
                    </tbody>
                </table>
                <asp:HiddenField runat="server" ID="hf_num_cond" />
            </div>

            <div id="div_otra_razon" class="hidden">
                <label>En caso de que exista otra raz&oacute;n para no autorizar la carga, favor de indicarla:</label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_otra_razon"></asp:TextBox>
            </div>

        </div>

        <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top ui-accordion-icons">Estatus</h3>
        <div id="div3" style="margin-bottom: 5px" class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
            <table width="100%">
                <tr>
                    <td>
                        <asp:RadioButton runat="server" ID="rb_aprobada" Text="Carga Aprobada" GroupName="estatus" />    
                    </td>
                    <td>
                        <asp:RadioButton runat="server" ID="rb_noaprobada" Text="Carga No Aprobada" GroupName="estatus" />        
                    </td>
                </tr>
            </table>
            <div id="div_rechazo" class="hidden">
                <asp:TextBox TextMode="MultiLine" CssClass="txtLarge" placeholder="En caso de detectar alguna otra razón para no autorizar la carga, indicarla" Rows="2" runat="server" ID="txt_motivo_rechazo"></asp:TextBox>
            </div>
        </div>

        <div style="clear: both;">
            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_click" />
            <asp:HiddenField runat="server" ID="hf_click_save" />
        </div>

        <div id="div_prev_perdidas" title="Código prevención de pérdidas">
            <div class="divForm">
                <div>
                    <label>Autoriza</label>
                    <select id="ddl_autoriza" class="txtMedium"></select>
                </div>
            </div>
            <div id="div_pwd_per">
            </div>
    
        </div>        
    </div>
    <div id="tabs-2">
        
        <div class="divForm">
            <div>
                <label>Fecha Inicial:</label>
                <asp:TextBox runat="server" ID="txt_fecha_ini" ></asp:TextBox>
            </div>
            <div>
                <label>Fecha Final:</label>
                <asp:TextBox runat="server" ID="txt_fecha_fin" ></asp:TextBox>
            </div>
        </div>

        <asp:Button runat="server" ID="btn_consultar" Text="Actualizar registros" CausesValidation="false" OnClick="btn_consultar_click" />
        <asp:UpdatePanel runat="server" ID="up_consulta" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_consultar" EventName="click" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView runat="server" CssClass="grdCascSmall" ID="grd_consulta" AutoGenerateColumns="false" EmptyDataText="No existen folios para el periodo proporcionado">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" />
                        <asp:BoundField DataField="Folio" HeaderText="Folio" />
                        <asp:BoundField DataField="Operador" HeaderText="Operador" />
                        <asp:BoundField DataField="PTransporte.Nombre" HeaderText="Línea" />
                        <asp:BoundField DataField="PTransTipo.Nombre" HeaderText="Tipo" />
                        <asp:BoundField DataField="Prevencion" HeaderText="Prevención" />
                        <asp:TemplateField HeaderText="Aprobada" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (String.Compare(Eval("Aprovada").ToString() , "True") == 0) ? "Si" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imprimir" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a target="_blank" href='<%# "frmReporter.aspx?rpt=SalAud&_key=" + Eval("Id") %>'><span class="ui-icon ui-icon-print"></span></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
            
    </div>
</div>

</asp:Content>
