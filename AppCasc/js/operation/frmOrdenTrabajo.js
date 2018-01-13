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

//        $('#ctl00_body_rep_servicios_ctl00_up_servicios').panelReady(function () {
//            $('#accordion').accordion('refresh');
//        });

        $('#ctl00_body_txt_referencia').mask('99-9999-9999999');

        $(".txtPedidos").autocomplete({
            source: function (request, response) {
                var data = $.parseJSON($('#ctl00_body_hf_pedidos').val());

                data = $.grep(data, function (obj, idx) {
                    return obj.label.substring(0, request.term.length) == request.term;
                });
                response(data);
            },
            minLength: 2,
            select: function (event, ui) {
                //console.log("Selected: " + ui.item.value + " aka " + ui.item.Id);
                $('#accordion').accordion('refresh');
            }
        });
    }
}

var master = new webApp.Master;
var pag = new MngOrdenTrabajo();
master.Init(pag);