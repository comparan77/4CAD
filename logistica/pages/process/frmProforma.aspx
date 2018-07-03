<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmProforma.aspx.cs" Inherits="logistica.pages.process.frmProforma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../vendor/datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../../vendor/datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../vendor/datepicker/locales/bootstrap-datepicker.es.min.js" type="text/javascript"></script>

    <script src="../../vendor/datatables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-plugins/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../vendor/datatables-responsive/dataTables.responsive.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/process/procesosModel.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/datagrid.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/monthPicker.js" type="text/javascript"></script>
    <script src="../../vendor/moment/moment.min.js" type="text/javascript"></script>
    <script src="../../js/process/proforma.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton runat="server" PostBackUrl="~/pages/process/frmProcesos.aspx" Text="Procesos"></asp:LinkButton> / Proformas</h1>
    </div>
</div>

<!-- /.panel-heading -->
<div id="adminTab" class="panel-body">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" id="tab_admin">
        <li class="active"><a href="#prc" data-toggle="tab">Procesar</a></li>
        <li><a href="#xap" data-toggle="tab">Por aplicar</a></li>
        <li><a href="#apl" data-toggle="tab">Aplicadas</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div class="tab-pane fade in active" id="prc">
            <h4></h4>
            <button type="button" id="btn_procesar" class="btn">Procesar proformas</button>                    
        </div>
        <div class="tab-pane fade" style="padding-top: 1em" id="xap">
            
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item active" id="li_por_aplicar">Por aplicar</li>
                    <li class="breadcrumb-item hidden" id="li_cliente" aria-current="page"></li>
                </ol>
            </nav>

            <div id="div_profConcentrado">

                <table id="tblProfConcentrado" class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>Cliente</th>
                            <th>Por Aplicar</th>
                            <th>Fecha Inicial</th>
                            <th>Fecha Final</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>

            </div>

            <div id="div_profByCliente" class="hidden">
                <div class="form-group">
                    <label for="txt_cliente">Cliente</label>
                    <input type="text" class="form-control" id="txt_cliente" disabled="" placeholder="Cliente">
                </div>
                <div class="form-group">
                    <label for="txt_fecha_ini">Fecha Inicial</label>
                    <input type="text" class="form-control" id="txt_fecha_ini" disabled="" placeholder="Fecha Inicial">
                </div>
                <div class="form-group">
                    <label for="txt_fecha_corte">Fecha Corte</label>
                    <input type="text" class="form-control" id="txt_fecha_corte" placeholder="Fecha Corte">
                </div>
                <div class="form-group">
                    <label for="txt_monto_corte">Monto al corte</label>
                    <input type="text" class="form-control" id="txt_monto_corte" disabled="" placeholder="Monto al corte">
                </div>
                <button type="button" id="btn_aplicar" class="btn btn-primary disabled">Aplicar</button>
            </div>

        </div>
        <div class="tab-pane fade" id="apl">
            
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item active" id="li_por_aplicarApp">Aplicadas</li>
                    <li class="breadcrumb-item hidden" id="li_clienteApp" aria-current="page"></li>
                </ol>
            </nav>

            <div id="div_profConcentradoApp">

                <table id="tblProfConcentradoApp" class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>Cliente</th>
                            <th>Aplicado</th>
                            <th>Proformas</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>

            </div>

            <div id="div_profByClienteApp" class="hidden">
                <table id="tblProfConcentradoAppFolio" class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>Fecha Inicial</th>
                            <th>Fecha Final</th>
                            <th>Cantidad</th>
                            <th>xls</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>

        </div>
    </div>
</div>
    
</asp:Content>
