//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmTransporteTipo');
//});

var LstTransporteTipo = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmTransporteTipo');
    }
}

var master = new webApp.Master;
var pag = new LstTransporteTipo();
master.Init(pag);