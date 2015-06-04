var LstTipoCarga = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmTipoCarga');
    }
}

var master = new webApp.Master;
var pag = new LstTipoCarga();
master.Init(pag);