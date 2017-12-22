var MngOrdenTrabajo = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        var precio = document.getElementById('ctl00_body_chklst_servicio_0');
        var uva = document.getElementById('ctl00_body_chklst_servicio_1');
        var up_pedido = $('#ctl00_body_up_pedido');

        $("#tabs").tabs();
        $('#ctl00_body_btn_consultar').button();

        $('#ctl00_body_btn_guardar').button();

        $(up_pedido).panelReady(function () {
            if (precio.checked) $('#div_pedido').removeClass('hidden');
            if (uva.checked) $('#div_uva').removeClass('hidden');
        });

        $(precio).click(function () {
            $('#div_pedido').addClass('hidden');
            if (this.checked) {
                $('#div_pedido').removeClass('hidden');
            }
        });

        $(uva).click(function () {
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