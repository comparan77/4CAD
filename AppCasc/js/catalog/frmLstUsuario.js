//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmUsuario');
//});

var LstUsuario = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmUsuario');
    }
}

var master = new webApp.Master;
var pag = new LstUsuario();
master.Init(pag);