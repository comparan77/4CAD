//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmDocumento');
//});

var LstDocumento = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmDocumento');
    }
}

var master = new webApp.Master;
var pag = new LstDocumento();
master.Init(pag);