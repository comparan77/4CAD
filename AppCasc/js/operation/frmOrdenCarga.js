var MngOrdenCarga = function () {

    this.Init = init;

    function init() {

        $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        var btn_buscar = $('#ctl00_body_btn_buscar');
        var div_busqueda = $('#div_busqueda');
        var up_resultados = $('#ctl00_body_up_resultados');

        var up_ordenes = $('#ctl00_body_up_ordenes');

        var div_tbl_folio_remision = $('#div-tbl-folio-remision');

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
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
        //inputs de fechas <<fin>>

        //up_resultados <<ini>>
        $(up_resultados).panelReady(function () {

            $('.lnk_result').each(function () {
                $(this).click(function () {
                    $(div_busqueda).addClass('ajaxLoading');
                });
            });

            $(div_busqueda).removeClass('ajaxLoading');
        });
        //up_resultados <<fin>>

        //Transporte <<ini>>
        $('#ctl00_body_upTipoTransporte').panelReady(function () {

            var ddlTransporte = $('#ctl00_body_ddlTransporte');

            $(ddlTransporte).unbind('change').change(function () {
                $('#ctl00_body_upTipoTransporte').addClass('ajaxLoading');
                $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
            });

            $('#ctl00_body_upTipoTransporte').removeClass('ajaxLoading');
            $('#ctl00_body_upDatosVehiculo').removeClass('ajaxLoading');
        });
        //Transporte <<fin>>

        //actualizar orden de carga <<ini>>
        $(up_ordenes).panelReady(function () {
            var btnSaveOrden = $('#ctl00_body_btnSaveOrden');
            var hf_remisiones_count = $('#ctl00_body_hf_remisiones_count');

            $(btnSaveOrden).button().click(function () {
                //alert('Funcionaldad pendiente de desarrollar...');
            }).attr('disabled', 'disabled').addClass('ui-button-disabled ui-state-disabled');
            if (hf_remisiones_count.val() * 1 > 0)
                $(btnSaveOrden).removeAttr('disabled').removeClass('ui-button-disabled ui-state-disabled');


            //Detalle remision <<ini>>
            var oRemDetail = new RemDetail();
            oRemDetail.ShowRemisionDetail(div_tbl_folio_remision);
            //folios-remisiones
            //            $('.lnk-folio-remision').each(function () {
            //                $(this).unbind('click').click(function () {
            //                    $('#ui-id-3').html($(this).html() + ', Fecha: ' + $(this).attr('title'));
            //                    $(div_tbl_folio_remision).dialog('open');
            //                    var arrData = ['bulto', 'piezaxbulto', 'pieza', 'bultoinc', 'piezaxbultoinc', 'piezainc', 'piezatotal', 'dano_especifico'];
            //                    var hf_element = $(this).next();
            //                    for (var data in arrData) {
            //                        $('#spn-' + arrData[data]).html($(hf_element).val());
            //                        hf_element = $(hf_element).next();
            //                    }

            //                    //                    if ($('#spn-etiqueta_rr').html() == '') {
            //                    //                        $('#spn-etiqueta_rr').html('-');
            //                    //                        $('#spn-fecha_recibido').html('-');
            //                    //                    }

            //                    //                    var hf_EST_REM_SIN_APROBACION = $('#ctl00_body_hf_EST_REM_CON_APROBACION');
            //                    //                    if (hf_EST_REM_SIN_APROBACION.val() == $('#spn-estatus').html()) {
            //                    //                        $(eliminar_remision).attr('disabled', 'true');
            //                    //                    }

            //                    return false;
            //                });
            //            });


            //Detalle remision <<fin>>

            $("html, body").animate({ scrollTop: $(document).height() }, "slow");
        });
        //actualizar orden de carga <<fin>>

        $(div_tbl_folio_remision).dialog({
            autoOpen: false,
            height: 200,
            width: 450,
            modal: true,
            resizable: false,
            close: function (event, ui) {
                $("html, body").animate({ scrollTop: $(document).height() }, "slow");
            }
        });
    }
}

MngOrdenCarga.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngOrdenCarga();
master.Init(pag);