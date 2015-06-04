var LstNom = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmNom');
    }
}

var master = new webApp.Master;
var pag = new LstNom();
master.Init(pag);