var LstClienteComprador = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.Fkey = $('#ctl00_body_hfFkey').val();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmClienteComprador');
    }
}

var master = new webApp.Master;
var pag = new LstClienteComprador();
master.Init(pag);