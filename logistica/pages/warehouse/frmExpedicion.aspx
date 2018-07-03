<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmExpedicion.aspx.cs" Inherits="logistica.pages.warehouse.frmExpedicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../js/webcontrols/ajaxInputUpload.js" type="text/javascript"></script>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/warehouse/almacenModel.js" type="text/javascript"></script>
    <script src="../../js/warehouse/expedicion.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-lg-12">
        <h1 class="page-header"><asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/pages/catalog/frmCatalogos.aspx" Text="Almacén"></asp:LinkButton> / Expedición</h1>
    </div>
</div>

<div class="panel panel-default">
        <div class="panel-heading">
        </div>
        <!-- /.panel-heading -->
        <div id="adminTab" class="panel-body">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" id="tab_admin">
                <li class="active"><a href="#car" data-toggle="tab">Carga</a></li>
                <li><a href="#sal" data-toggle="tab">Salida</a></li>
                <li><a href="#imp" data-toggle="tab">Importación de datos</a></li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="doc" style="padding: .5em">
                    <h4></h4>
                </div>
                <div class="tab-pane fade" id="car">
                    
                </div>
                <div class="tab-pane fade" id="sal">
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