<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRecepcion.aspx.cs" Inherits="logistica.pages.warehouse.frmRecepcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../vendor/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <script src="../../vendor/select2/select2.min.js" type="text/javascript"></script>
    <script src="../../js/webcontrols/ajaxInputUpload.js" type="text/javascript"></script>
    <script src="../../js/catalog/catalogosModel.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/warehouse/almacenModel.js" type="text/javascript"></script>
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
            <div id="pnl_ddl" class="hidden">
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
            </div>
        </div>
        <!-- /.panel-heading -->
        <div id="adminTab" class="panel-body">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#doc" data-toggle="tab">Documentación</a></li>
                <li><a href="#des" data-toggle="tab">Descarga</a></li>
                <li><a href="#ent" data-toggle="tab">Entrada</a></li>
                <li><a href="#imp" data-toggle="tab">Importación de datos</a></li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="doc" style="padding: .5em">
                    <h4></h4>
                </div>
                <div class="tab-pane fade" id="des">
                    
                    <input type="hidden" id="hf_id_cortina_disponible" />
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
                    </div>
                </div>
                <div class="tab-pane fade" id="ent">
                    <h4></h4>
                   
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
