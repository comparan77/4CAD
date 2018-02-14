var MngOrdenTrabajo = function () {

    this.Init = init;
    var clickTabConsulta = false;
    var up_consulta;
    var btn_consultar;

    function init() {
        initControls();
    }

    function initControls() {

        up_consulta = $('#ctl00_body_up_consulta');
        btn_consultar = $('#ctl00_body_btn_consultar');

        $("#tabs").tabs({
            activate: function (event, ui) {
                if (ui.newPanel.selector === '#tabs-2' && !clickTabConsulta) {
                    $('#ctl00_body_btn_consultar').trigger('click');
                }
            }
        });

        $(btn_consultar).button().click(function () {
            $(btn_consultar).addClass('ui-state-disabled').val('Actualizando registros ...');
        });

        $('#ctl00_body_btn_guardar').button();

        $('#accordion').accordion({
            icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" }
        });

        $('#ctl00_body_txt_referencia').mask('99-9999-9999999');

        $('#ctl00_body_rep_servicios_ctl00_up_servicios').panelReady(function () {
            $('#accordion').accordion('refresh');
        });

        $(up_consulta).panelReady(function () {
            $(btn_consultar).removeClass('ui-state-disabled').val('Actualizar registros');
        });
    }

}

var master = new webApp.Master;
var pag = new MngOrdenTrabajo();
master.Init(pag);