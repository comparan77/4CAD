var MngOrdenTrabajo = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {

        var up_pedido = $('#ctl00_body_up_pedido');

        $("#tabs").tabs();
        $('#ctl00_body_btn_consultar').button();

        $('#ctl00_body_btn_guardar').button();

        $('#accordion').accordion({
            icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" },
            heigthStyle: "content"
        });

        $(up_pedido).panelReady(function () {

            var precio = document.getElementById('ctl00_body_chklst_servicio_0');
            var uva = document.getElementById('ctl00_body_chklst_servicio_1');

            if (precio.checked) $('#div_pedido').removeClass('hidden');
            if (uva.checked) $('#div_uva').removeClass('hidden');

            precioClick(precio);
            UvaClick(uva);
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