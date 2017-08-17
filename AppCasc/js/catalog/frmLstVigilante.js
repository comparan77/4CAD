var LstVigilante = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmVigilante');
    }
}

var master = new webApp.Master;
var pag = new LstVigilante();
master.Init(pag);