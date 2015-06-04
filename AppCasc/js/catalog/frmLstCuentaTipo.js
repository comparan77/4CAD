//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmCuentaTipo');
//});

var LstCuentaTipo = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmCuentaTipo');
    }
}

var master = new webApp.Master;
var pag = new LstCuentaTipo();
master.Init(pag);