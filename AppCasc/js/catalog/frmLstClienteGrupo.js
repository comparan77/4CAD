//$(document).ready(function () {
//    var oCatalog = new Catalog();
//    oCatalog.CreateGrid($('#grdCatalog'), 'frmClienteGrupo');
//});

var LstClienteGrupo = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmClienteGrupo');
    }
}

var master = new webApp.Master;
var pag = new LstClienteGrupo();
master.Init(pag);