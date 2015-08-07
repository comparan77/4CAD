var MngSalida = function () {

    this.Init = init;

    function init() {

        $("#dialog-confirm").dialog({
            autoOpen: false,
            resizable: false,
            height: 250,
            width: 450,
            modal: true,
            buttons: {
                "Guardar Salida": function () {
                    $(this).dialog("close");
                    $('#hf-confirmado').val(1);
                    var clickButton = document.getElementById('ctl00_body_btnGuardar');
                    clickButton.click();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        var upDocRequerido = $('#ctl00_body_upDocRequerido');

        $('#ctl00_body_ddlCliente').change(function () {
            upDocRequerido.addClass('ajaxLoading');
        });

        upDocRequerido.panelReady(function () {
            var DocReq;
            var DocMask;

            if ($('#ctl00_body_ddlDocumento option:selected').val() == 1) {
                $('#ctl00_body_txt_referencia_documento').mask('99-9999-9999999');
            }
            else {
                $('#ctl00_body_txt_referencia_documento').unmask();
            }

            $('#ctl00_body_ddlDocumento').unbind('change').change(function () {
                if ($('#ctl00_body_ddlDocumento option:selected').val() == 1)
                    $('#ctl00_body_txt_referencia_documento').mask('99-9999-9999999');
                else
                    $('#ctl00_body_txt_referencia_documento').unmask();
            });

            DocReq = $('#ctl00_body_hfReferencia').val();
            //$('#divReferenciaReq').hide();
            if (DocReq != '') {
                $('#lblReferencia').html($('#ctl00_body_hfReferencia').val());
                DocMask = $('#ctl00_body_hfMascara').val();
                if (DocMask != '')
                    $('#ctl00_body_txt_referencia').mask(DocMask);
                //$('#divReferenciaReq').show(); 

            }

            if ($('#ctl00_body_hfIdDocReq').val() == '1')
                $('#chkConsolidado').removeAttr('disabled');
            else
                $('#chkConsolidado').attr('disabled', 'true');

            upDocRequerido.removeClass('ajaxLoading');

            //Validacion de destinos

        });

        $('#ulSalHoy').children('li').each(function () {
            $(this).children('div').children('input').each(function () {
                $(this).button();
            });
        });

        $('#ctl00_body_txt_hora_salida, #ctl00_body_txt_hora_carga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });

        $('#ctl00_body_btnGuardar').button().click(function () {
            var IsValid = true;
            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    $('html,body').animate({
                        scrollTop: $(this).offset().top
                    }, 2000);
                    IsValid = false;
                    return false;
                }
            });

            if (IsValid) {
                var confirm = $('#hf-confirmado').val() * 1;
                if (confirm == 0) {
                    var mensaje = '';
                    mensaje = '<b>Tipo:</b> ' + $('#ctl00_body_chk_tipo_salida').next().html();
                    if ($('#ctl00_body_chk_ultima').is(':checked') == true) {
                        mensaje += ', <b>Última salida.</b>';
                    }
                    mensaje += '<br />';
                    mensaje += '<b>Transporte:</b> ' + $('#ctl00_body_ddlTransporte option:selected').text() + ', Tipo ' + $('#ctl00_body_ddlTipo_Transporte option:selected').text();
                    mensaje += '<hr />';
                    mensaje += '<b>Compartida:</b> ' + ($('#chkConsolidado').is(':checked') == true ? 'Sí' : 'No');
                    if ($('#chkConsolidado').is(':checked') == true) {
                        mensaje += '<br />';
                        mensaje += '<ul>';
                        $('#ctl00_body_lst_pedimentos_consolidados > option').each(function () {
                            mensaje += '<li>';
                            mensaje += this.text;
                            mensaje += '</li>';
                        });
                        mensaje += '</ul>';
                    }
                    $('#spn-aviso-registro').html(mensaje);
                    $("#dialog-confirm").dialog('open');
                    return false;
                }
                else
                    $(this).hide();
            }
        });

        var pnl_consolidada;

        var chkConsolidado = $('#chkConsolidado').click(function () {
            var lblConsolidada = $('#lblConsolidada').html('NO Consolidada');
            pnl_consolidada = $('#pnl_consolidada').hide('slow');
            var hfConsolidada = $('#ctl00_body_hfConsolidada').val('false');
            if ($(this).is(':checked')) {
                $(lblConsolidada).html('Consolidada');
                $(pnl_consolidada).show('slow');
                $(hfConsolidada).val('true');
            }
        });

        if ($('#ctl00_body_hfEsCompartida').val() == "True") {
            $('#lblConsolidada').html('Consolidada');
            $(chkConsolidado).attr('disabled', 'disabled').attr('checked', 'checked');
            $('#ctl00_body_txt_pedimento_consolidado').val('');
            $('#pnl_consolidada').show('slow');
        }

        var up_documento_enviado = $('#ctl00_body_up_documento_enviado');
        var up_Destino = $('#ctl00_body_up_Destino');

        $('#ctl00_body_ddlCliente').change(function () {
            up_documento_enviado.addClass('ajaxLoading');
            up_Destino.addClass('ajaxLoading');
        });

        $(up_Destino).panelReady(function () {
            $(up_Destino).removeClass('ajaxLoading');
        });

        $(up_documento_enviado).panelReady(function () {

            $('.documentoEnviado').unbind('change').change(function () {
                $('#div_btnRemoveDR').show();
            });

            var btnAdd_documento = $('#ctl00_body_btnAdd_documento').button().unbind('click').click(function () {
                var txt_referencia_documento = $('#ctl00_body_txt_referencia_documento')
                if ($(txt_referencia_documento).val().length <= 1) {
                    alert('La referencia proporcionada es muy corta');
                    txt_referencia_documento.focus();
                    return false;
                }
                else
                    $(up_documento_enviado).addClass('ajaxLoading');
            });

            $('#ctl00_body_btnRem_documento').button().unbind('click').click(function () {
                $(up_documento_enviado).addClass('ajaxLoading');
            });

            $(up_documento_enviado).removeClass('ajaxLoading');
            $('#ctl00_body_ddlDocumento').trigger('change');
        });

        $('#ctl00_body_up_consolidada').panelReady(function () {

            var btnAdd_pedimento = $('#ctl00_body_btnAdd_pedimento');
            var btnRem_pedimento = $('#ctl00_body_btnRem_pedimento');
            var txt_documentosReq = $('#ctl00_body_txt_documentosReq');
            var txt_pedimento_consolidado = $('#ctl00_body_txt_pedimento_consolidado');

            if ($('#ctl00_body_hfMascara').val() != '')
                $('#ctl00_body_txt_pedimento_consolidado').mask($('#ctl00_body_hfMascara').val());

            btnAdd_pedimento.button().unbind('click').click(function () {
                $('#ctl00_body_up_consolidada').addClass('ajaxLoading');
            });

            btnRem_pedimento.button().unbind('click').click(function () {
                $('#ctl00_body_up_consolidada').addClass('ajaxLoading');
            });

            $('.pedimentoAgregado').unbind('change').change(function () {
                $('#div_btnRemovePT').show();
            });

            $('#ctl00_body_up_consolidada').removeClass('ajaxLoading');

        });

        $('#ctl00_body_upTipoTransporte').panelReady(function () {

            var ddlTransporte = $('#ctl00_body_ddlTransporte');

            $(ddlTransporte).unbind('change').change(function () {
                $('#ctl00_body_upTipoTransporte').addClass('ajaxLoading');
                $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
            });

            $('#ctl00_body_upTipoTransporte').removeClass('ajaxLoading');
            $('#ctl00_body_upDatosVehiculo').removeClass('ajaxLoading');
        });

        $('#ctl00_body_upDatosVehiculo').panelReady(function () {

            var ddlTipo_Transporte = $('#ctl00_body_ddlTipo_Transporte');

            $(ddlTipo_Transporte).unbind('change').change(function () {
                $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
            });

            $('#ctl00_body_upDatosVehiculo').removeClass('ajaxLoading');

        });

        $('#ctl00_body_upTotalCarga').panelReady(function () {

            var txt_peso_unitario = $('#ctl00_body_txt_peso_unitario');
            var txt_no_bulto = $('#ctl00_body_txt_no_bulto');

            $(txt_peso_unitario).blur(function () {
                $('#upCantTotalCarga').addClass('ajaxLoading');
            });

            $(txt_no_bulto).blur(function () {
                $('#upCantTotalCarga').addClass('ajaxLoading');
            });

            $('#upCantTotalCarga').removeClass('ajaxLoading');
        });

        $('#ctl00_body_upChkSalida').panelReady(function () {

            var chk_tipo_salida = $('#ctl00_body_chk_tipo_salida');

            $(chk_tipo_salida).unbind('click').click(function () {
                $('#ctl00_body_upChkSalida').addClass('ajaxLoading');
            });

            $('#ctl00_body_upChkSalida').removeClass('ajaxLoading');

        });

        //Destinos
        listDestinos();
    }

    function listDestinos() {
        $('#ul_destinos').children('li').each(function () {
            $(this).children('a').click(function () {
                var destino = $(this).attr('destino');
                $('#ctl00_body_txt_destino').val(destino);
                return false;
            });
        });
    }

    function setMaskDocumentType(id) {
        if (id == 1)
            $('#ctl00_body_txt_referencia_documento').mask('9999-9999999');
        else
            $('#ctl00_body_txt_referencia_documento').unmask();
    }

    function validaHoraPicker(obj) {
        var span;
        var IsValid = true;
        if (obj == null) {
            $('.horaPicker').each(function () {
                span = $(this).next('span');
                if (span != null) {
                    $(span).hide();
                    if ($(this).val().length < 1) {
                        $(span).show();
                        IsValid = false;
                        return false;
                    }
                }
            });
            return IsValid;
        }
        else {
            span = $(obj).next('span');
            if (span != null) {
                $(span).hide();
                if ($(obj).val().length < 1)
                    $(span).show();
            }
        }
    }

    //    function loadError() {
    //        $('#errorMsgs').attr('title', $('#ctl00_body_hfTitleErr').val())

    //        $('#errorMsgs').dialog({
    //            autoOpen: false,
    //            height: 190,
    //            width: 420,
    //            modal: true,
    //            resizable: false
    //        });

    //        if ($('#ctl00_body_hfTitleErr').val().length > 0) {
    //            $('#errorMsg').html($('#ctl00_body_hfDescErr').val());
    //            $('#errorMsgs').dialog('open');
    //            $('#ctl00_body_hfTitleErr').val('');
    //        }
    //    }

}

//$(document).ready(function () {
//    var oMngSalida = new MngSalida();
//    oMngSalida.Init();
//});

var master = new webApp.Master;
var pag = new MngSalida();
master.Init(pag);