//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmCliente');
//});

var LstCliente = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmCliente');
    }
}

var master = new webApp.Master;
var pag = new LstCliente();
master.Init(pag);