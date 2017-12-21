var MngMaq = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        //Mascara para el folio
        try {
            $.mask.definitions['u'] = "[1-9]";
            var anioAct = new Date();
            var digAnio = anioAct.getFullYear().toString().substr(2, 2);
            $('#ctl00_body_txt_folio').mask('OT-999999-' + digAnio);
            $('#ctl00_body_up_info_ot').panelReady(function () {

                $('#tabs').tabs();

                var spn_estado_ot = $('#spn_estado_ot');
                $(spn_estado_ot).click(function () {
                    var state = $(this).hasClass('ui-icon-locked') ? 'open' : 'close';
                    OrdenTrabajoEstadoChange($('#ctl00_body_hf_id_orden_trabajo').val(), state);
                });

            });

        } catch (err) {
            alert(err.Message);
        }
    }

    //Cambiar el estado de la maquila
    function OrdenTrabajoEstadoChange(id_orden_trabajo, state) {
        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=orden_trabajo&opt=' + state,
            data: id_orden_trabajo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                $('#spn_estado_ot').removeClass('ui-icon-locked');
                $('#spn_estado_ot').removeClass('ui-icon-unlocked');
                if (state == 'open') {
                    $('#spn_estado_ot').addClass('ui-icon-unlocked');
                }
                else {
                    $('#spn_estado_ot').addClass('ui-icon-locked');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

}

var master = new webApp.Master;
var pag = new MngMaq();
master.Init(pag);