var MngOrdenTrabajo = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {

        $("#tabs").tabs();
        $('#ctl00_body_btn_consultar').button();

        $('#ctl00_body_btn_guardar').button();

        $('#accordion').accordion({
            icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" }
        });

        $('#ctl00_body_rep_servicios_ctl00_up_servicios').panelReady(function () {
            $('#accordion').accordion('refresh');
        });


    }

    function precioClick(chk_precio) {
        $(chk_precio).click(function () {
            $('#div_pedido').addClass('hidden');
            if (this.checked) {
                $('#div_pedido').removeClass('hidden');
            }
        });
    }

    function UvaClick(chk_uva) {
        $(chk_uva).click(function () {
            $('#div_uva').addClass('hidden');
            if (this.checked) {
                $('#div_uva').removeClass('hidden');
            }
        });
    }
}

var master = new webApp.Master;
var pag = new MngOrdenTrabajo();
master.Init(pag);