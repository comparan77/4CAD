<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmASN.aspx.cs" Inherits="logistica.pages.process.frmASN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../vendor/datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="../../vendor/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../vendor/clockpicker/bootstrap-clockpicker.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../../vendor/datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../vendor/datepicker/locales/bootstrap-datepicker.es.min.js" type="text/javascript"></script>
    <script src="../../vendor/select2/select2.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-plugins/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-responsive/dataTables.responsive.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/process/procesosModel.js" type="text/javascript"></script>
    <script src="../../js/catalog/catalogosModel.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/datagrid.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/monthPicker.js" type="text/javascript"></script>
    <script src="../../vendor/moment/moment.min.js" type="text/javascript"></script>
    <script src="../../vendor/clockpicker/bootstrap-clockpicker.min.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/tabCatalog.js" type="text/javascript"></script>
    <script src="../../js/process/asn.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Advanced Shipping Notice</h1>
    </div>
</div>

<div class="panel panel-default">
        <div class="panel-heading">
            
        </div>
        <!-- /.panel-heading -->
        <div id="adminTab" class="panel-body">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#list" data-toggle="tab">Listado</a>
                </li>
                <li><a href="#admon" data-toggle="tab">Edición</a>
                </li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="list" style="padding: .5em">
                    <table id="grdCatalog" class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>Folio</th>
                                <th>Fecha</th>
                                <th>Cliente</th>
                                <th>Referencia</th>
                                <th>Tarimas</th>
                                <th>Cajas</th>
                                <th>Piezas</th>
                                <th>Descargada</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                      </table>  
                </div>
                <div class="tab-pane fade" id="admon">
                    <h4 id="h4-action"></h4>
                    <div id="div-nuevo" class="row hidden">
                      <div class="col-md-3 col-md-offset-5"><button type="button" id="btn_nuevo" class="btn btn-light">Nuevo</button></div>
                    </div>
                    
                    <div class="form-group">
                        <label for="ddl_cliente">Cliente
                            <select id="ddl_cliente" style="width: 100%" class="form-control"></select>
                        </label>
                    </div>

                    <div class="form-group">
                        <label for="ddl_bodega">Almac&eacute;n
                            <select id="ddl_bodega" style="width: 100%" class="form-control"></select>
                        </label>
                    </div>

                    <div class="form-group">
                        <label for="txt_fecha">Fecha y Hora</label>
                        <input type="text" class="form-control" id="txt_fecha" placeholder="Fecha">
                    </div>

                    <div class="form-group">
                        <div class="input-group clockpicker">
                            <input type="text" id="txt_hora" class="form-control" value="">
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-time"></span>
                            </span>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="ddl_transporte">Transporte
                            <select id="ddl_transporte" style="width: 100%" class="form-control"></select>
                        </label>
                    </div>

                    <div class="form-group">
                        <label for="ddl_transporte_tipo">Tipo de Transporte
                            <select id="ddl_transporte_tipo" style="width: 100%" class="form-control">
                                <option></option>
                            </select>
                        </label>
                    </div>

                    <div class="form-group">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Sellos
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <td>No</td>
                                                <td>Contenedor / Caja</td>
                                                <td>Sello</td>
                                            </tr>
                                        </thead>
                                        <tbody id="tbody_sellos"></tbody>
                                    </table>
                                </div>
                            </div>
                    </div>

                    <div class="form-group">
                        <label class="radio-inline">
                            <input id="nacional" name="tipo" checked="" type="radio" value="nacional" />
                            Nacional
                        </label>
                        <label class="radio-inline">
                            <input id="extranjero" name="tipo" type="radio" value="extranjero" />
                            Extranjero
                        </label>
                    </div>

                    <div id="div_extranjero" class="hidden">

                        <div class="form-group">
                            <label for="ddl_aduana">Aduana
                                <select id="ddl_aduana" style="width: 100%" class="form-control">
                                
                                </select>
                            </label>
                        </div>

                        <div class="form-group">
                            <label for="txt_anio">A&ntilde;o</label>
                            <input type="text" maxlength="4" class="form-control" id="txt_anio" placeholder="Año">
                        </div>
                        <div class="form-group">
                            <label for="txt_patente">Patente</label>
                            <input type="text" maxlength="4" class="form-control" id="txt_patente" placeholder="Patente">
                        </div>
                        
                        <div class="form-group">
                            <label for="txt_documento">Documento</label>
                            <input type="text" maxlength="6" class="form-control" id="txt_documento" placeholder="Documento">
                        </div>

                    </div>

                    <div class="form-group">
                        <label for="txt_operador">Transportista</label>
                        <input type="text" class="form-control" id="txt_operador" placeholder="Nombre del transportista">
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Partidas
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th align="center">
                                                No
                                            </th>
                                            <th align="center">
                                                <select id="ddl_cliente_mercancia"></select>
                                                <hr />
                                                <label>SKU</label>
                                            </th>
                                            <th align="center">
                                                <input type="number" min="0" id="txt_tarima" />
                                                <hr />
                                                <label>Tarimas</label>
                                            </th>
                                            <th align="center">
                                                <input type="number" min="0" id="txt_caja" />
                                                <hr />
                                                <label>Cajas</label>
                                            </th>
                                            <th align="center">
                                                <input type="number" min="0" id="txt_pieza" />
                                                <hr />
                                                <label>Piezas</label>
                                            </th>
                                            <th>
                                                <button type="button" id="btn_add" class="btn-link">+</button>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="t_body_partidas"></tbody>
                                    <tfoot class="tbl_foot_total" id="t_foot_partidas"></tfoot>
                                </table>
                            </div>
                        </div>
                    </div>

                    <button type="button" id="btn_save" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>

</asp:Content>
