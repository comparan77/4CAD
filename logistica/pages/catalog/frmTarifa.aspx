﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTarifa.aspx.cs" Inherits="logistica.pages.catalog.frmTarifa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../vendor/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <script src="../../vendor/select2/select2.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-plugins/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-responsive/dataTables.responsive.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/catalog/catalogosModel.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/datagrid.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/tabCatalog.js" type="text/javascript"></script>
    <script src="../../js/catalog/tarifa.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/pages/catalog/frmCatalogos.aspx" Text="Catálogos"></asp:LinkButton> / Tarifas</h1>
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
                    
                    <div id="div_bread_service" class="hidden">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active" id="li_cliente_tipo"></li>
                                <li class="breadcrumb-item" id="li_servicio" aria-current="page"></li>
                            </ol>
                        </nav>
                    </div>

                    <div id="div_unselected_service">

                        <div class="form-group input-group">
                            <select id="ddl_cliente" name="cliente">
                            </select>
                        </div>

                        <table id="grdCatalog" class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>Servicio</th>
                                    <th>Con tarifa</th>
                                    <th>Sin tarifa</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                          </table>  
                    </div>

                    <div id="div_selected_service" class="hidden">

                        <table id="grdServiceTarifa" class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>SKU</th>
                                    <th>Nombre</th>
                                    <th>Tarifa</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                          </table>  

                    </div>

                </div>
                <div class="tab-pane fade" id="admon">
                    <h4><small id="h4-action"></small></h4>
                    <div id="div-nuevo" class="row hidden">
                      <div class="col-md-3 col-md-offset-5"><button type="button" id="btn_nuevo" class="btn btn-light">Nuevo</button></div>
                    </div>
                    <div class="form-group">
                        <label for="txt_sku">SKU</label>
                        <input type="text" class="form-control" id="txt_sku" placeholder="SKU">
                    </div>
                    <div class="form-group">
                        <label for="txt_upc">UPC</label>
                        <input type="text" class="form-control" id="txt_upc" placeholder="UPC">
                    </div>
                    <div class="form-group">
                        <label for="txt_nombre">Descripción</label>
                        <input type="text" class="form-control" id="txt_nombre" placeholder="Descripción">
                    </div>
                    <div class="form-group">
                        <label for="txt_precio">Precio</label>
                        <input type="text" class="form-control" id="txt_precio" placeholder="Precio">
                    </div>
                    <div class="form-group">
                        <label for="txt_piezas_x_caja">Piezas x caja</label>
                        <input type="text" class="form-control" id="txt_piezas_x_caja" placeholder="Piezas x caja">
                    </div>
                    <div class="form-group">
                        <label for="txt_cajas_x_tarima">Cajas x tarima</label>
                        <input type="text" class="form-control" id="txt_cajas_x_tarima" placeholder="Cajas x tarima">
                    </div>
                    <button type="button" id="btn_save" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>
</asp:Content>