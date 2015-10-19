var frmReportViewer = function () {

    this.Init = function () {
        initControls();
    }

    function initControls() {
        var currentDate = new Date();
        currentDate = moment(currentDate).format('DD/MM/YYYY');
        $('#ctl00_body_btnGetRpt').button();
        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        }).val(currentDate);

        $('#ctl00_body_ddl_reporte').change(function () {
            switch ($(this).val()) {
                case 'Maquila':
                    $('#ctl00_body_txt_fecha_ini').val(currentDate);
                    break;
                case 'Remision':
                case 'Piso':
                    $('#ctl00_body_txt_fecha_ini').val('');
                    break;
            }
        });
    }
}

var master = new webApp.Master;
var pag = new frmReportViewer();
master.Init(pag);