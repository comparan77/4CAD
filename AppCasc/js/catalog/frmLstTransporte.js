//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmTransporte');
//});

var LstTransporte = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmTransporte');
    }
}

var master = new webApp.Master;
var pag = new LstTransporte();
master.Init(pag);