<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDestino.aspx.cs" Inherits="logistica.pages.catalog.frmDestino" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="../../vendor/datatables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-plugins/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-responsive/dataTables.responsive.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/catalog/catalogosModel.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/datagrid.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/tabCatalog.js" type="text/javascript"></script>
    <script src="../../js/catalog/destino.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/pages/catalog/frmCatalogos.aspx" Text="Catálogos"></asp:LinkButton> / Destinos</h1>
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
                                <th>Destino</th>
                                <th>Direcci&oacute;n</th>
                                <th>Activo</th>
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
                        <label for="txt_destino">Destino</label>
                        <input type="text" class="form-control" id="txt_destino" placeholder="Destino">
                    </div>

                    <div class="form-group">
                        <label for="txt_cp">C.P.</label>
                        <input type="text" class="form-control" id="txt_cp" placeholder="C.P.">
                    </div>
                    <button type="button" id="btn_cp" class="btn btn-default">Validar C.P.</button>
                    <div class="form-group">
                        <label for="txt_calle_num">Calle y N&uacute;mero</label>
                        <input type="text" class="form-control" id="txt_calle_num" placeholder="Calle y número">
                    </div>
                    <div class="form-group">
                        <label for="txt_estado">Estado</label>
                        <input type="text" class="form-control" id="txt_estado" placeholder="Estado">
                    </div>
                    <div class="form-group">
                        <label for="txt_municipio">Municipio o Del</label>
                        <input type="text" class="form-control" id="txt_municipio" placeholder="Municipio o Delegación">
                    </div>
                    <div class="form-group" id="div_colonia">
                        <label>Asentamiento</label>
                        <select id="ddl_colonia" class="form-control"></select>
                    </div>
                    <div class="form-group" id="div_txt_col">
                        <label for="txt_colonia">Asentamiento</label>
                        <input type="text" class="form-control" id="txt_colonia" placeholder="Colonia">
                    </div>

                    <div class="form-group">
                        <label for="txt_direccion">Dirección</label>
                        <input type="text" class="form-control" id="txt_direccion" placeholder="Dirección del almacén">
                    </div>
                    <div class="form-group">
                        <div id="div_active_opt" class="btn-group btn-group-toggle" data-toggle="buttons">
                          <label class="btn btn-secondary active">
                            <input type="radio" name="activo" id="opt_active" autocomplete="off" checked> Activo
                          </label>
                          <label class="btn btn-secondary">
                            <input type="radio" name="activo" id="opt_inactive" autocomplete="off"> Inactivo
                          </label>
                        </div>
                    </div>
                    <button type="button" id="btn_save" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>
</asp:Content>
