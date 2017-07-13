var frmCrystalRpt = function () {

    this.Init = function () {
        initControls();
    }

    function initControls() {
        var currentDate = new Date();
        currentDate = moment(currentDate).format('DD/MM/YYYY');

        $('#ctl00_body_btn_get_rpt').button();

        if ($('#ctl00_body_hfPageLoaded').val() == "false") {
            $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
                'dateFormat': 'dd/mm/yy'
            }).val(currentDate);
        }
        else {
            $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
                'dateFormat': 'dd/mm/yy'
            });
        }

        $('#ctl00_body_ddl_rpt').change(function () {
            validaParametros($(this).val());
        });

        validaParametros($('#ctl00_body_ddl_rpt').val());
    }

    function validaParametros(sede) {
        $('#sede').hide();
        $('#periodo').hide();
        switch (sede) {
            case 'personal_empresa':
                $('#sede').hide();
                $('#periodo').hide();
                break;
            case '119':
                $('#sede').show();
                $('#periodo').show();
                break;
        }
    }
}

var master = new webApp.Master;
var pag = new frmCrystalRpt();
master.Init(pag);
