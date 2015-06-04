//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmCortina');
//});

var LstCortina = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmCortina');
    }
}

var master = new webApp.Master;
var pag = new LstCortina();
master.Init(pag);