//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmBodega');
//});

var LstBodega = function () {
    
    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmBodega');
    }
}

var master = new webApp.Master;
var pag = new LstBodega();
master.Init(pag);