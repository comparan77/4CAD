//$(document).ready(function () {
//    $('#ctl00_body_btnGetCancelMov').button();
//    $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
//        'dateFormat': 'dd/mm/yy'
//    }).click(function () {
//        $('#ctl00_body_lnkCancelMov').hide();
//    });

//});

var CancelMov = function () {
    this.Init = function () {
        $('#ctl00_body_btnGetCancelMov').button();
        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        }).click(function () {
            $('#ctl00_body_lnkCancelMov').hide();
        });    
    }
}

var master = new webApp.Master;
var pag = new CancelMov();
master.Init(pag);