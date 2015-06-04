var LstAduana = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmAduana');
    }
}

var master = new webApp.Master;
var pag = new LstAduana();
master.Init(pag);