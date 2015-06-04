var CancelDoc = function () {
    this.Init = function () {
        var divActions = $('#divActions');
        divActions.children('input[type="submit"]').each(function () {
            $(this).button();
        });
    }
}

var master = new webApp.Master;
var pag = new CancelDoc();
master.Init(pag);