var LstClienteVendor = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.Fkey = $('#ctl00_body_hfFkey').val();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmClienteVendor');
    }
}

var master = new webApp.Master;
var pag = new LstClienteVendor();
master.Init(pag);