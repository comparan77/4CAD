var MngTraficoWH = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        var btn_solicitar_cita = $('#ctl00_body_btn_guardar_trafico');

        $(btn_solicitar_cita).button().click(function () {

            var IsValid = true;
            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    IsValid = false;
                    return false;
                }
            });
        });

        //inputs de horario <<ini>>
        $('#ctl00_body_txt_hora_carga_solicitada, #ctl00_body_txt_hora_cita').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });
        //inputs de horario <<fin>>

        //inputs de fechas <<ini>>
        $('#ctl00_body_txt_fecha_solicitud, #ctl00_body_txt_fecha_carga_solicitada, #ctl00_body_txt_fecha_cita').datepicker({
            'dateFormat': 'dd/mm/yy'
        })
    }
}

var master = new webApp.Master;
var pag = new MngTraficoWH();
master.Init(pag);