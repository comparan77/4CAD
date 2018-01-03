var MngRptWH = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {

        var currentDate = new Date();
        currentDate = moment(currentDate).format('DD/MM/YYYY');
        //currentDate.locale('es');
        $('#ctl00_body_btnGetRpt').button();
        $('#ctl00_body_btnGetRptAsync').button();
        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        }).val(currentDate);

        //        var iniYear = new Date(moment(new Date()).year(), new Date().getMonth(), 1)
        //        iniYear = moment(iniYear).format('DD/MM/YYYY');
        //        $('#ctl00_body_txt_fecha_ini').val(iniYear);

        porMes();

        $('#ctl00_body_ddl_reporte').change(function () {
            hideAll();
            switch ($(this).val()) {
                case 'resumen':
                    porMes();
                    break;
                case 'RelDiaEnt':
                case 'RelDiaSal':
                case 'InvTotDia':
                    porPeriodo();
                    break;
            }
        });

        $('#ctl00_body_btnGetRpt').click(function () {
            $('#ctl00_body_hf_mes').val($('#spn_ActYear').html() + '|' + $('#MesOperacion').val());
        });
    }

    function porMes() {

        var fecha = moment(new Date());
        fecha.locale('es');
        $('#div_mensual').removeClass('hidden');

        $('#spn_ActYear').html(fecha.format('YYYY'));
        setSelectYear($('#spn_ActYear'));
        $('#spn_PrvYear').html(fecha.subtract(1, 'year').format('YYYY')).unbind('click').click(function () {

            var actYear = $('#spn_ActYear').html() * 1;
            $('#spn_ActYear').html(actYear - 1);
            $('#spn_NxtYear').html(actYear);
            $('#spn_NxtYear').removeClass('hidden');
            $('#spn_PrvYear').html($('#spn_PrvYear').html() * 1 - 1);

        });

        $('#spn_NxtYear').html(fecha.add(1, 'year').format('YYYY')).unbind('click').click(function () {

            var actYear = $('#spn_ActYear').html() * 1;
            $('#spn_ActYear').html(actYear + 1);
            $('#spn_PrvYear').html(actYear);
            $('#spn_NxtYear').html(actYear + 2);
            if (actYear + 2 <= moment(new Date()).year()) {
                $('#spn_NxtYear').removeClass('hidden');
            }
            else {
                $('#spn_NxtYear').addClass('hidden');
            }

        });

        //Mes actual
        var mesAct = fecha.format('M');
        $('#MesOperacion').val(mesAct);
    }

    function setSelectYear(obj) {
        $(obj).css("font-weight", "bold").css("color", "#fda100");

    }

    function hideAll() {
        $('#div_mensual').addClass('hidden');
        $('#div_periodo').addClass('hidden');
    }

    function porPeriodo() {
        $('#div_periodo').removeClass('hidden');
        var iniYear = new Date(moment(new Date()).year(), new Date(). + 1, 1)
        iniYear = moment(iniYear).format('DD/MM/YYYY');
        $('#ctl00_body_txt_fecha_ini').val(iniYear);
    }
}


var master = new webApp.Master;
var pag = new MngRptWH();
master.Init(pag);