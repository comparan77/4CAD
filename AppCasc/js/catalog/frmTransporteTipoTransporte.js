//$(document).ready(function () {
//    $('#ctl00_body_btnMove').button();
//});

var LstTTT = function () {

    this.Init = function () {
        $('#ctl00_body_btnMove').button();
    }
}

var master = new webApp.Master;
var pag = new LstTTT();
master.Init(pag);