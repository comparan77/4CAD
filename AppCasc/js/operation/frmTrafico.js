var MngTrafico = function () {

    this.Init = init;

    function init() {

        $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        //        var btn_buscar = $('#ctl00_body_btn_buscar');
        //        var div_busqueda = $('#div_busqueda');
        //        var up_resultados = $('#ctl00_body_up_resultados');

        //        var up_ordenes = $('#ctl00_body_up_ordenes');

        //        var div_tbl_folio_remision = $('#div-tbl-folio-remision');

        //        $(btn_buscar).button({
        //            'icons': {
        //                'primary': 'ui-icon-search'
        //            }
        //        }).click(function () {
        //            div_busqueda.addClass('ajaxLoading');
        //        });

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

        var up_trafico = $('#ctl00_body_up_trafico');

        $(up_trafico).panelReady(function () {

            $('.activaGuardarCita').each(function () {
                $(this).attr('disabled', 'disabled');
            });

            $('.citaReq').each(function () {
                $(this).unbind('change').change(function () {
                    var valido = validaCamposRequeridos();
                    if (valido)
                        $(this).parent().parent().children('div').last().children('input').removeAttr('disabled');
                    else
                        $(this).parent().parent().children('div').last().children('input').attr('disabled', 'disabled');
                });
            });

            $('.txt_fecha').each(function () {
                $(this).datepicker({
                    'dateFormat': 'dd/mm/yy'
                })
            });

            $('.txt_hora').each(function () {
                $(this).scroller({
                    preset: 'time',
                    theme: 'default',
                    display: 'modal',
                    mode: 'clickpick',
                    timeFormat: 'HH:ii:ss'
                });
            });

            $('.spn_TransporteGetByTipo').each(function () {
                $(this).click(function () {
                    var id_transporte_tipo = $(this).attr('id').split("_")[1] * 1;
                    getTransporteByTipo(id_transporte_tipo);
                });
            });

        });
    }

    function validaCamposRequeridos() {
        var valido = true;
        $('.citaReq').each(function () {
            if ($(this).val().length == 0) {
                valido = false;
                return;
            }
        });

        return valido;
    }

    function getTransporteByTipo(id_transporte_tipo) {

        $('#linea_' + id_transporte_tipo).html('');

        $.ajax({
            type: 'GET',
            url: '/handlers/Catalog.ashx?catalogo=transporte&id_transporte_tipo=' + id_transporte_tipo,
            //dataType: "jsonp",
            complete: function () {
                $('#div-info-codigo').removeClass('ajaxLoading');
            },
            success: function (data) {

                $(data).each(function (i, obj) {
                    var opt = '<option value="' + obj.Id + (i == 0 ? '" selected="selected"' : '"') + '>' + obj.Nombre + '</option>';
                    $('#linea_' + id_transporte_tipo).append(opt);
                });

                //$('#linea_' + id_transporte_tipo).prev().val(data[0].Id);

                $('#linea_' + id_transporte_tipo).trigger('change');

                validaCamposRequeridos();

                $('#linea_' + id_transporte_tipo).unbind('change').change(function () {
                    $(this).prev().val($(this).val());
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }

}

MngTrafico.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngTrafico();
master.Init(pag);