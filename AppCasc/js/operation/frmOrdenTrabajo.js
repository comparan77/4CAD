var MngOrdenTrabajo = function () {

    this.Init = init;
    var clickTabConsulta = false;

    function init() {
        initControls();
    }

    function initControls() {

        $("#tabs").tabs({
            activate: function (event, ui) {
                if (ui.newPanel.selector === '#tabs-2' && !clickTabConsulta) {
                    $('#ctl00_body_btn_consultar').trigger('click');
                    clickTabConsulta = true;
                }
            }
        });
        $('#ctl00_body_btn_consultar').button();

        $('#ctl00_body_btn_guardar').button();

        $('#accordion').accordion({
            icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" }
        });

        $('#ctl00_body_txt_referencia').mask('99-9999-9999999');

        //        $('#ctl00_body_rep_servicios_ctl00_up_servicios').panelReady(function () {
        //            autoCompletePedido();
        //        });

        //        $('#ctl00_body_up_trafico').panelReady(function () {
        //            console.log('up_trafico');
        //            autoCompletePedido();
        //        });

        $('#ctl00_body_rep_servicios_ctl00_up_servicios').panelReady(function () {
            $('#accordion').accordion('refresh');
        });
    }

    //    function autoCompletePedido() {
    //        $(".txtPedidos").autocomplete({
    //            source: function (request, response) {
    //                var data = $.parseJSON($('#ctl00_body_hf_pedidos').val());

    //                data = $.grep(data, function (obj, idx) {
    //                    return obj.label.substring(0, request.term.length) == request.term;
    //                });
    //                response(data);
    //            },
    //            minLength: 2,
    //            select: function (event, ui) {
    //                //console.log("Selected: " + ui.item.value + " aka " + ui.item.Id);
    //                $('#accordion').accordion('refresh');
    //                var spanProveedor = $(this).next().children('span').first();
    //                $(spanProveedor).html('Proveedor: ' + ui.item.Proveedor);
    //                var spanPiezas = $(spanProveedor).next();
    //                $(spanPiezas).html('Piezas: ' + ui.item.Piezas);
    //                var pnl_pedido = $(this).next().next();
    //                $(pnl_pedido).removeClass('hidden');
    //                $('#accordion').accordion('refresh');
    //            }
    //        });
    //    }
}

var master = new webApp.Master;
var pag = new MngOrdenTrabajo();
master.Init(pag);