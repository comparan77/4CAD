var LstPersonal = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmPersonal');
    }
}

var master = new webApp.Master;
var pag = new LstPersonal();
master.Init(pag);