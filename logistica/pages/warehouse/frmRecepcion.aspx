<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRecepcion.aspx.cs" Inherits="logistica.pages.warehouse.frmRecepcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../vendor/full-calendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../../vendor/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../vendor/qtip/jquery.qtip.min.css" rel="stylesheet" type="text/css" />
    <script src="../../vendor/select2/select2.min.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/ajaxInputUpload.js" type="text/javascript"></script>
    <script src="../../js/catalog/catalogosModel.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../vendor/moment/moment.min.js" type="text/javascript"></script>
    <script src="../../vendor/qtip/jquery.qtip.min.js" type="text/javascript"></script>
    <script src="../../vendor/full-calendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../../vendor/full-calendar/es.js" type="text/javascript"></script>
    <script src="../../js/warehouse/almacenModel.js" type="text/javascript"></script>
    <script src="../../js/process/procesosModel.js" type="text/javascript"></script>
    <script src="../../js/warehouse/recepcion.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/pages/catalog/frmCatalogos.aspx" Text="Almacén"></asp:LinkButton> / Recepción</h1>
    </div>
</div>

<div class="panel panel-default">
        <div class="panel-heading">
            
           <%-- <div id="pnl_ddl" class="hidden">
                <div class="form-group input-group">
                    <select class="form-control" style="width: 100%" id="ddl_cliente" name="cliente">
                    <option></option>
                    </select>
                </div>
                <div id="div_bodega" class="form-group input-group">
                    <select class="form-control" style="width: 100%" id="ddl_bodega" name="bodega">
                    <option></option>
                    </select>
                </div>
            </div>
            <div id="pnl_lbl" class="hidden">
                <div class="form-group input-group">
                    <span id="spn_cliente"></span>
                </div>
                <div class="form-group input-group">
                    <span id="spn_bodega"></span>
                </div>
            </div>--%>

        </div>
        <!-- /.panel-heading -->
        <div id="adminTab" class="panel-body">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs">
                <li><a href="#asn" data-toggle="tab">Calendario ASN</a></li>
                <li><a href="#imp" data-toggle="tab">Importación de datos</a></li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade" id="asn">
                    
                    <nav id="nav_asn_folio" class="hidden" aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item active" id="li_calendario">Citas</li>
                            <li class="breadcrumb-item" id="li_folio" aria-current="page"></li>
                        </ol>
                    </nav>

                    <div id="calendar_asn"></div>

                    <div id="div_folio_info" class="hidden">
                    
                        <div class="form-group" disabled="">
                            <label for="txt_cliente">Cliente</label>
                            <input type="text" class="form-control" id="txt_cliente" disabled="" placeholder="Cliente">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_bodega">Almac&eacute;n</label>
                            <input type="text" class="form-control" id="txt_bodega" disabled="" placeholder="Almacén">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_referencia">Referencia</label>
                            <input type="text" class="form-control" id="txt_referencia" disabled="" placeholder="Referencia">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_transporte">Transporte</label>
                            <input type="text" class="form-control" id="txt_transporte" disabled="" placeholder="Transporte">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_sello">No. Sello</label>
                            <input type="text" class="form-control" id="txt_sello" disabled="" placeholder="No Sello">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_operador">No. Sello</label>
                            <input type="text" class="form-control" id="txt_operador" disabled="" placeholder="Operador">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_tarima">No. Tarimas</label>
                            <input type="text" class="form-control" id="txt_tarima" disabled="" placeholder="No de Tarimas">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_caja">No. Cajas</label>
                            <input type="text" class="form-control" id="txt_caja" disabled="" placeholder="No de Cajas">
                        </div>

                        <div class="form-group" disabled="">
                            <label for="txt_pieza">No. Piezas</label>
                            <input type="text" class="form-control" id="txt_pieza" disabled="" placeholder="No de Piezas">
                        </div>

                        <div id="div_cortina">
                            <div class="form-group input-group">
                                <select class="form-control" style="width: 100%" id="ddl_cortina" name="cortina">
                                <option></option>
                                </select>
                            </div>
                            <button type="button" id="btn_asignar_cortina" class="btn">Asignar Cortina</button>
                        </div>

                    </div>

                    <%--<input type="hidden" id="hf_id_cortina_disponible" />
                    <input type="hidden" id="hf_id_cliente" />

                    <div id="div_cortina" class="hidden">
                        <div class="form-group input-group">
                            <select class="form-control" style="width: 100%" id="ddl_cortina" name="cortina">
                            <option></option>
                            </select>
                        </div>
                        <button type="button" id="btn_tomar_cortina" class="btn">Tomar Cortina</button>
                    </div>

                    <div id="div_cortina_ocupada" class="hidden">
                        <p id="h4-action-des" class="text-info">This is an example of primary text.</p>
                        <div id="div_mercancia_cliente">
                            <div class="form-group input-group">
                                <select class="form-control" style="width: 100%" id="ddl_mercancia_cliente" name="mercancia_cliente">
                                <option></option>
                                </select>
                            </div>
                            <fieldset disabled>
                                <div class="form-group">
                                    <label>Piezas por Caja</label>
                                    <input id="txt_pieza_x_caja" class="form-control" >
                                </div>
                                <div class="form-group">
                                    <label>Cajas por Tarima</label>
                                    <input id="txt_cajas_x_tarima" class="form-control" >
                                </div>
                            </fieldset>
                            <div class="form-group">
                                <label>Tarimas declaradas</label>
                                <input id="txt_tarima_declarada" type="number" min="1" max="10" step="1" VALUE="1" SIZE="6" class="form-control">
                            </div>
                            <fieldset disabled>
                                <div class="form-group">
                                    <label>Tarimas por descargar</label>
                                    <input id="txt_tarima_x_descargar" value="0" class="form-control" >
                                </div>
                            </fieldset>                            
                            <button type="button" id="btn_tarima_descargada" class="btn btn-primary">Agergar tarima descargada</button>
                            <fieldset disabled>
                                <div class="form-group">
                                    <label>Tarimas descargadas</label>
                                    <input id="txt_tarima_descargada" value="0" class="form-control" >
                                </div>
                            </fieldset>
                        </div>
                        <button type="button" id="btn_liberar_cortina" class="btn">Liberar Cortina</button>
                    </div>--%>

                </div>
                
                <div class="tab-pane fade" id="imp">
                    <h4></h4>
                    <div id="fileUploadAjax"></div>
                </div>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>
</asp:Content>
