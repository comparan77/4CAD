var MngTrafico = function () {

    this.Init = init;

    function init() {

        var up_trafico = $('#ctl00_body_up_trafico');
        var up_trafico_con_cita = $('#ctl00_body_up_trafico_con_cita');
        var btn_solicitar_cita = $('#ctl00_body_btn_solicitar_cita');

        $(btn_solicitar_cita).button().click(function () {

            var IsValid = true;
            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    IsValid = false;
                    return false;
                }
            });
            if (IsValid) {
                $(this).val('Solicitando cita ...');
                $(up_trafico).addClass('ajaxLoading');
            }
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

        $(up_trafico_con_cita).panelReady(function () {

            $('.activaGuardarCita').each(function () {
                $(this).button({
                    disabled: true
                }).click(function () {

                });
            });

            $('.classTransporte_tipo').each(function () {
                $(this).unbind('change').change(function () {
                    $(this).parent().next().children('select').html('');
                    tipo_transporte_change($(this));
                    validaCamposRequeridos($(this));
                });
            });

            $('.citaReq').each(function () {
                $(this).unbind('change').change(function () {
                    var valido = validaCamposRequeridos($(this));
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

                    var id_cita = $(this).parent().prev().children('input').val() * 1;
                    var id_transporte_tipo = $(this).parent().prev().children('select').val() * 1;
                    getTransporteByTipo(id_transporte_tipo, id_cita, $(this).parent().children('select'));
                });
            });

            $('.clsLnkFolioCita').each(function () {
                $(this).click(function () {
                    var id = $(this).attr('id').split('_')[3] * 1;
                    $('#lbl_folio_cita_' + id).next('input').val($(this).html());
                    $('#lbl_fecha_cita_' + id).next('input').val($('#spn_fecha_cita_' + id).html());
                    $('#lbl_hora_cita_' + id).next('input').val($('#spn_hora_cita_' + id).html());
                    return false;
                });
            });

        });

        $(up_trafico).panelReady(function () {

            $('.activaGuardarCita').each(function () {
                $(this).button({
                    disabled: true
                });
            });

            $('.eliminarCitaSinAsignar').each(function () {
                $(this).button().click(function () {
                    if (!confirm('La cita sólo podrá ser eliminada por el solicitante, Desea eliminar esta cita?'))
                        return false;
                });
            });

            $('.classTransporte_tipo').each(function () {
                $(this).unbind('change').change(function () {
                    $(this).parent().next().children('select').html('');
                    tipo_transporte_change($(this));
                    validaCamposRequeridos($(this));
                });
            });

            $('.citaReq').each(function () {
                $(this).unbind('change').change(function () {
                    var valido = validaCamposRequeridos($(this));
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

                    var id_cita = $(this).parent().prev().children('input').val() * 1;
                    var id_transporte_tipo = $(this).parent().prev().children('select').val() * 1;
                    getTransporteByTipo(id_transporte_tipo, id_cita, $(this).parent().children('select'));
                });
            });

            $('.clsLnkFolioCita').each(function () {
                $(this).click(function () {
                    var id = $(this).attr('id').split('_')[3] * 1;
                    $('#lbl_folio_cita_' + id).next('input').val($(this).html());
                    $('#lbl_fecha_cita_' + id).next('input').val($('#spn_fecha_cita_' + id).html());
                    $('#lbl_hora_cita_' + id).next('input').val($('#spn_hora_cita_' + id).html());
                    return false;
                });
            });

            $(btn_solicitar_cita).val('Solicitar Cita');
            clearAfterApply();
            $(up_trafico).removeClass('ajaxLoading');

        });
    }

    function clearAfterApply() {
        $('.cleanAfterApply').each(function () {
            $(this).val('');
        });
    }

    function validaCamposRequeridos(input) {
        var valido = true;
        $(input).parent().parent().find('.citaReq').each(function () {
            if ($(this).val() == null || $(this).val().length == 0) {
                valido = false;
                return;
            }
        });
        if (valido)
            $(input).parent().parent().children('div').last().children('input').button('enable');
        else
            $(input).parent().parent().children('div').last().children('input').button('option', 'disabled');

        return valido;
    }

    function getTransporteByTipo(id_transporte_tipo, id_cita, ddlTransporte) {

        $(ddlTransporte).html('');

        $.ajax({
            type: 'GET',
            url: '/handlers/Catalog.ashx?catalogo=transporte&id_transporte_tipo=' + id_transporte_tipo,
            //dataType: "jsonp",
            complete: function () {
                $('#div-info-codigo').removeClass('ajaxLoading');
            },
            success: function (data) {

                $(data).each(function (i, obj) {
                    var opt = '<option placa="' + obj.PTransporte_tipo.Requiere_placa + '" ';
                    opt += 'caja="' + obj.PTransporte_tipo.Requiere_caja + '" ';
                    opt += 'caja1="' + obj.PTransporte_tipo.Requiere_caja1 + '" ';
                    opt += 'caja2="' + obj.PTransporte_tipo.Requiere_caja2 + '" ';
                    opt += 'value="' + obj.Id_transporte + (i == 0 ? '" selected="selected"' : '"') + '>' + obj.Transporte + '</option>';
                    $(ddlTransporte).append(opt);
                });

                $(ddlTransporte).prev().val(data[0].Id_transporte);

                //validaCamposRequeridos();
                tipo_transporte_change(ddlTransporte);
                validaCamposRequeridos(ddlTransporte);
                $(ddlTransporte).unbind('change').change(function () {
                    $(ddlTransporte).prev().val($(this).val());
                });

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }

    function tipo_transporte_change(ddl_tipo_transporte) {

        if (ddl_tipo_transporte.length > 0) {
            var optionSelected = $(ddl_tipo_transporte).find(':selected');

            var placa = $(optionSelected).attr('placa');
            var caja = $(optionSelected).attr('caja');
            var caja1 = $(optionSelected).attr('caja1');
            var caja2 = $(optionSelected).attr('caja2');

            for (var iP = 7; iP < 11; iP++) {
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(' + iP + ')').addClass('hidden');
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(' + iP + ')').children('input').val('N.A');
            }

            if (placa.toLowerCase() == 'true') {
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(7)').removeClass('hidden');
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(7)').children('input').val('');
            }

            if (caja.toLowerCase() == 'true') {
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(8)').removeClass('hidden');
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(8)').children('input').val('');
            }

            if (caja1.toLowerCase() == 'true') {
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(9)').removeClass('hidden');
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(9)').children('input').val('');
            }

            if (caja2.toLowerCase() == 'true') {
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(10)').removeClass('hidden');
                $(ddl_tipo_transporte).parent().parent().children('div:nth-child(10)').children('input').val('');
            }
        }
    }
}

MngTrafico.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngTrafico();
master.Init(pag);