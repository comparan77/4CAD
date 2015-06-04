//$(document).ready(function () {
//    $('#ctl00_body_btnGetChart').button();
//    $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
//        'dateFormat': 'dd/mm/yy'
//    }).click(function () {
//        $('#ctl00_body_lnkChart').hide();
//    });

//});

var FrmChart = function () {

    this.Init = function () {
        $('#ctl00_body_btnGetChart').button();
        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        }).click(function () {
            $('#ctl00_body_lnkChart').hide();
        });
    }
}

var master = new webApp.Master;
var pag = new FrmChart();
master.Init(pag);