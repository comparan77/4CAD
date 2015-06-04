//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmCustodia');
//});

var LstCustodia = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmCustodia');
    }
}

var master = new webApp.Master;
var pag = new LstCustodia();
master.Init(pag);