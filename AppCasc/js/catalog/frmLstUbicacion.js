var LstUbicacion = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmUbicacion');
    }
}

var master = new webApp.Master;
var pag = new LstUbicacion();
master.Init(pag);