<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdenTrabajo.aspx.cs" Inherits="AppCasc.operation.frmOrdenTrabajo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../js/operation/frmOrdenTrabajo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Orden de Trabajo</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">

    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Captruar Nueva</a></li>
            <li><a href="#tabs-2">Consultar </a></li>
        </ul>
        <div id="tabs-1">
            <div class="divForm">
                
                <div>
                    <label>Pedimento:</label>
                    <asp:TextBox runat="server" ID="txt_referencia" Text="" AutoPostBack="true" OnTextChanged="change_referencia"></asp:TextBox>
                    
                </div>
                <asp:UpdatePanel runat="server" ID="up_trafico" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txt_referencia" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                <div>
                    <label>No Tr&aacute;fico:</label>
                    <asp:TextBox runat="server" ID="txt_trafico" Text=""></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hf_pedidos" />
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>

                <div id="accordion">
                    <asp:Repeater runat="server" ID="rep_servicios"  OnItemDataBound="rep_serv_data_bound">
                        <ItemTemplate>
                        <h3><%# Eval("Nombre") %></h3>
                        <div>

                        <asp:UpdatePanel runat="server" ID="up_servicios" UpdateMode="Conditional">
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_add_pedido" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_add_nom" EventName="click" />
                        </Triggers>
                            
                            <ContentTemplate>
                            <asp:Panel runat="server" ID="pnl_precio" Visible="false">
                            
                                <label>No Pedido:</label>
                                <asp:TextBox runat="server" ID="txt_pedido" CssClass="txtPedidos" MaxLength="8"></asp:TextBox>

                                <div style="padding: 1em">
                                    <span></span>
                                    <span></span>
                                </div>

                                <asp:Panel runat="server" ID="pnl_pedido" CssClass="hidden" >
                                    <div>
                                        <label>Tipo de etiqueta</label>
                                        <asp:DropDownList runat="server" ID="ddl_eti_tipo_precio"></asp:DropDownList>
                                    </div>
                                    <div>
                                        <label>Piezas a precio</label>
                                        <asp:TextBox runat="server" ID="txt_pedido_pieza"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfv_pedido_pieza" ControlToValidate="txt_pedido_pieza" ValidationGroup="vg_pedido" ErrorMessage="Es necesario proporcionar las piezas"></asp:RequiredFieldValidator>
                                    </div>
                                    <div>
                                        <asp:Button runat="server" ID="btn_add_pedido" OnCommand="addServicio" CommandName="precio" CommandArgument='<%#Eval("Id") %>' ValidationGroup="vg_pedido" Text="Agregar servicio" />
                                    </div>
                                </asp:Panel>

                                <asp:CustomValidator runat="server" ID="cv_pedido" ControlToValidate="txt_pedido" ValidationGroup="vg_pedido" OnServerValidate="validatePedido" ErrorMessage="El pedido y código proporcionado no existe"></asp:CustomValidator>
                                
                            </asp:Panel>                                   
                        
                        <asp:Panel runat="server" ID="pnl_uva" Visible="false">
                            <label>No Solicitud:</label>
                            <asp:TextBox runat="server" ID="txt_solicitud"></asp:TextBox>
                            <div>
                                <label>Tipo de etiqueta</label>
                                <asp:DropDownList runat="server" ID="ddl_eti_tipo_uva"></asp:DropDownList>
                            </div>
                            <div>
                                <label>Piezas a NOM</label>
                                <asp:TextBox runat="server" ID="txt_sol_pieza"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_sol_pieza" ControlToValidate="txt_sol_pieza" ValidationGroup="vg_nom" ErrorMessage="Es neceasrio proporcionar el número de piezas"></asp:RequiredFieldValidator>
                            </div>
                            
                            <asp:Button runat="server" ID="btn_add_nom" OnCommand="addServicio" CommandArgument='<%#Eval("Id") %>' CommandName="nom" Text="Agregar servicio" ValidationGroup="vg_nom" />
                        </asp:Panel>

                        <asp:HiddenField runat="server" ID="hf_id_servicio" Value='<%#Eval("Id") %>' />
                        </div>                        

                        </ContentTemplate>
                            </asp:UpdatePanel>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>                        
            </div>    
            
            <hr />
            <asp:UpdatePanel runat="server" ID="up_xguardar" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView runat="server" CssClass="grdCascSmall" ID="grd_ordenesXGuardar" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="PServ.Nombre" HeaderText="Servicio Solicitado" />
                    <asp:BoundField DataField="PEtiquetaTipo.Nombre" HeaderText="Tipo de Etiqueta" />
                    <asp:BoundField DataField="REf1" HeaderText="Trafico" />
                    <asp:BoundField DataField="Ref2" HeaderText="Referencia" />
                    <asp:BoundField DataField="Piezas" HeaderText="Pzs Sol" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                </Columns>
                </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>

            <div class="divForm" style="margin-top: 1em">
                <div>
                    <label>Supervisor:</label>
                    <asp:TextBox runat="server" CssClass="txtLarge" ID="txt_supervisor" ValidationGroup="vg_orden_trabajo"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"  ValidationGroup="vg_orden_trabajo" ID="rfv_supervisor" ControlToValidate="txt_supervisor" ErrorMessage="Es necesario capturar el supervisor"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button runat="server" ID="btn_guardar" Text="Guardar Orden de Trabajo" OnClick="guardar_ot" />
                </div>    
            </div>
        </div>
        <div id="tabs-2">
            <asp:Button runat="server" ID="btn_consultar" Text="Consultar" CausesValidation="false" OnClick="btn_consultar_click" />
            <asp:UpdatePanel runat="server" ID="up_consulta" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_consultar" EventName="click" />
                </Triggers>
                <ContentTemplate>
                    <asp:GridView runat="server" CssClass="grdCascSmall" ID="grd_ordenes" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Folio">
                                <ItemTemplate>
                                    <a href='<%# "frmMaq.aspx?folio=" + Eval("Folio") %>' ><%#Eval("Folio") %></a>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:BoundField DataField="Supervisor" HeaderText="Supervisor Asignado" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" />
                            <asp:BoundField DataField="Referencia" HeaderText="Trafico"/>
                            <asp:BoundField DataField="Servicios" HeaderText="Servicios" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField HeaderText="Estatus">
                                <ItemTemplate>
                                    <span class='<%# "ui-icon ui-icon-" + (Convert.ToBoolean(Eval("Cerrada")) ? "locked icon-button-action" : "unlocked") %>'></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    

</div>

</asp:Content>
