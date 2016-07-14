var frmReportViewer = function () {

    this.Init = function () {
        initControls();
    }

    function initControls() {
        var currentDate = new Date();
        currentDate = moment(currentDate).format('DD/MM/YYYY');

        $('#ctl00_body_btnGetRpt').hide();

        $('#ctl00_body_btnGetRpt').button();
        $('#ctl00_body_btnGetRptXls').button();

        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        }).val(currentDate);

        var iniYear = new Date(moment(new Date()).year(), 0, 1)
        iniYear = moment(iniYear).format('DD/MM/YYYY');
        $('#ctl00_body_txt_fecha_ini').val(iniYear);

        $('#div_destino, #div_estatus').hide();

        $('#ctl00_body_ddl_reporte').change(function () {
            $('#div_destino, #div_estatus').hide();
            $('#ctl00_body_btnGetRpt').hide();
            $('#ctl00_body_btnGetRptXls').hide();
            switch ($(this).val()) {
                case 'Maquila':
                    $('#ctl00_body_txt_fecha_ini').val(currentDate);
                    $('#ctl00_body_btnGetRptXls').show();
                    break;
                case 'Remision':
                case 'Piso':
                    $('#ctl00_body_txt_fecha_ini').val('');
                    $('#ctl00_body_btnGetRptXls').show();
                    break;
                case 'Trafico':
                    $('#ctl00_body_txt_fecha_ini').val(currentDate);
                    $('#div_destino, #div_estatus').show();
                    $('#ctl00_body_btnGetRpt').show();
                    break;
                case 'Inventario':
                case 'Fondeo':
                    var iniYear = new Date(moment(new Date()).year(), 0, 1)
                    iniYear = moment(iniYear).format('DD/MM/YYYY');
                    $('#ctl00_body_txt_fecha_ini').val(iniYear);
                    $('#ctl00_body_btnGetRptXls').show();
                    break;
            }
        });
    }
}

var master = new webApp.Master;
var pag = new frmReportViewer();
master.Init(pag);