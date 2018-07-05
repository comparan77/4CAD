<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmServicio.aspx.cs" Inherits="logistica.pages.catalog.frmServicio" %>
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
    <script src="../../js/catalog/servicio.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton runat="server" PostBackUrl="~/pages/catalog/frmCatalogos.aspx" Text="Catálogos"></asp:LinkButton> / Servicios</h1>
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
                                <th>Nombre</th>
                                <th>Descripción</th>
                                <th>Periodo</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                      </table>  
                </div>
                <div class="tab-pane fade" id="admon">
                    <h4 id="h4-action"></h4>
                    <div id="div-nuevo" class="row hidden">
                      <div class="col-md-3 col-md-offset-5"><button type="button" id="btn_nuevo" class="btn btn-light">Crear Nuevo registro</button></div>
                    </div>
                    <div class="form-group">
                        <label for="txt_nombre">Nombre</label>
                        <input type="text" class="form-control" id="txt_nombre" placeholder="Nombre del almacén">
                    </div>
                    <div class="form-group">
                        <label for="txt_descripcion">Descripción</label>
                        <input type="text" class="form-control" id="txt_descripcion" placeholder="Descripción del servicio">
                    </div>
                    <div class="form-group">
                        <label for="ddl_periodo">Periodo</label>
                        <select class="form-control" style="width: 100%" id="ddl_periodo" name="periodo">
                            <option></option>
                        </select>
                    </div>
                    <div id="div_dias" class="form-group hidden">
                        <label>Días</label>
                        <input id="txt_dias" type="number" min="1" max="366" step="1" VALUE="1" SIZE="6" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="txt_formula_cantidad">F&oacute;rmula cantidad</label>
                        <textarea class="form-control" rows="3" style="width:100%; height: 40px" id="txt_formula_cantidad" cols="2"></textarea>
                    </div>
                    <div class="form-group">
                        <label for="txt_formula_importe">F&oacute;rmula importe</label>
                        <textarea class="form-control" rows="3" style="width:100%; height: 40px" id="txt_formula_importe" cols="2"></textarea>
                    </div>
                    <button type="button" id="btn_save" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>

</asp:Content>
