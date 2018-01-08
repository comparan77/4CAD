<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdenTrabajo.aspx.cs" Inherits="AppCasc.operation.frmOrdenTrabajo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <label>No Tr&aacute;fico:</label>
                    <asp:TextBox runat="server" ID="txt_trafico" Text="LT8036IM17"></asp:TextBox>
                </div>


                <div id="accordion">
                    <asp:Repeater runat="server" ID="rep_servicios"  OnItemDataBound="rep_serv_data_bound">
                        <ItemTemplate>
                        <h3><%# Eval("Nombre") %></h3>
                        <div>

                        <asp:UpdatePanel runat="server" ID="up_pedido">
                            <ContentTemplate>
                            <div id="div_pedido">
                                <label>No Pedido:</label>
                                <asp:TextBox runat="server" ID="txt_pedido" Text="6141769" CausesValidation="true" AutoPostBack="true" OnTextChanged="pedido_changed"></asp:TextBox>

                                <div>
                                    <asp:Label runat="server" ID="lbl_pedido_info"></asp:Label>
                                    <asp:Label runat="server" ID="lbl_pedido_piezas"></asp:Label>
                        
                                </div>

                                <asp:Panel runat="server" ID="pnl_pedido" Visible="false">
                                    <div>
                                        <label>Tipo de etiqueta</label>
                                        <asp:DropDownList runat="server" ID="ddl_eti_tipo_precio"></asp:DropDownList>
                                    </div>
                                    <div>
                                        <label>Piezas a precio</label>
                                        <asp:TextBox runat="server" ID="txt_pedido_pieza"></asp:TextBox>
                                    </div>
                                </asp:Panel>

                                <asp:CustomValidator runat="server" ID="cv_pedido" ControlToValidate="txt_pedido" OnServerValidate="validatePedido" ErrorMessage="El pedido y código proporcionado no existe"></asp:CustomValidator>

                            </div>
        
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        
                        <asp:Panel runat="server" ID="up_uva" Visible="false">
                            <label>No Solicitud:</label>
                            <asp:TextBox runat="server" ID="txt_solicitud"></asp:TextBox>
                            <div>
                                <label>Tipo de etiqueta</label>
                                <asp:DropDownList runat="server" ID="ddl_eti_tipo_uva"></asp:DropDownList>
                            </div>
                            <div>
                                <label>Piezas a NOM</label>
                                <asp:TextBox runat="server" ID="txt_sol_pieza"></asp:TextBox>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField runat="server" ID="hf_id_servicio" Value='<%#Eval("Id") %>' />
                        </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>


                <%--<div>
                    <label style="border:none;">Servicio:</label>
                    <asp:CheckBoxList runat="server" ID="chklst_servicio"></asp:CheckBoxList>
                </div>

                <asp:UpdatePanel runat="server" ID="up_pedido">
                <ContentTemplate>
                <div id="div_pedido" class="hidden">
                    <label>No Pedido:</label>
                    <asp:TextBox runat="server" ID="txt_pedido" Text="6141769" CausesValidation="true" AutoPostBack="true" OnTextChanged="pedido_changed"></asp:TextBox>

                    <div>
                        <asp:Label runat="server" ID="lbl_pedido_info"></asp:Label>
                        <asp:Label runat="server" ID="lbl_pedido_piezas"></asp:Label>
                        
                    </div>

                    <asp:Panel runat="server" ID="pnl_pedido" Visible="false">
                        <div>
                            <label>Tipo de etiqueta</label>
                            <asp:DropDownList runat="server" ID="ddl_eti_tipo_precio"></asp:DropDownList>
                        </div>
                        <div>
                            <label>Piezas a precio</label>
                            <asp:TextBox runat="server" ID="txt_pedido_pieza"></asp:TextBox>
                        </div>
                    </asp:Panel>

                    <asp:CustomValidator runat="server" ID="cv_pedido" ControlToValidate="txt_pedido" OnServerValidate="validatePedido" ErrorMessage="El pedido y código proporcionado no existe"></asp:CustomValidator>

                </div>
        
                </ContentTemplate>
                </asp:UpdatePanel>

                <div id="div_uva" class="hidden">
                    <label>No Solicitud:</label>
                    <asp:TextBox runat="server" ID="txt_solicitud"></asp:TextBox>
                    <div>
                        <label>Tipo de etiqueta</label>
                        <asp:DropDownList runat="server" ID="ddl_eti_tipo_uva"></asp:DropDownList>
                    </div>
                    <div>
                        <label>Piezas a NOM</label>
                        <asp:TextBox runat="server" ID="txt_sol_pieza"></asp:TextBox>
                    </div>
                </div>--%>
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
                    <asp:GridView runat="server" ID="grd_ordenes" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='<%# "frmMaq.aspx?folio=" + Eval("Folio") %>' ><%#Eval("Folio") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" />
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
