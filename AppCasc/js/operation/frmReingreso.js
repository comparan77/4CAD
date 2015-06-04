var MngReingreso = function () {

    this.Init = init;

    function init() {

        $("#dialog-confirm").dialog({
            autoOpen: false,
            resizable: false,
            height: 250,
            width: 450,
            modal: true,
            buttons: {
                "Guardar Entrada": function () {
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

        var btnGuardar = $('#ctl00_body_btnGuardar'); //.removeAttr('disabled');
        $('#ctl00_body_txt_dato').focus();
        var btnBuscar = $('#ctl00_body_btn_buscar').button();

        var upChkEntrada = $('#ctl00_body_upChkEntrada');
        upChkEntrada.panelReady(function () {
            $('#ctl00_body_chk_tipo_entrada').click(function () {
                upChkEntrada.addClass('ajaxLoading');
            });
            upChkEntrada.removeClass('ajaxLoading');
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
            $('#divReferenciaReq').hide();
            if (DocReq != '') {
                $('#lblReferencia').html($('#ctl00_body_hfReferencia').val());
                DocMask = $('#ctl00_body_hfMascara').val();
                if (DocMask != '')
                    $('#ctl00_body_txt_referencia').mask(DocMask);
                $('#divReferenciaReq').show();
                $('#chkConsolidado').removeAttr('disabled');
            }
            else {
                $('#chkConsolidado').attr('disabled', 'true');
            }
            upDocRequerido.removeClass('ajaxLoading');
        });

        $('#ulEntHoy').children('li').each(function () {
            $(this).children('div').children('input').each(function () {
                $(this).button();
            });
        });

        $('#ctl00_body_txt_hora_llegada, #ctl00_body_txt_hora_descarga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });

        btnGuardar.button().click(function () {
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
                    mensaje = '<b>Tipo:</b> ' + $('#ctl00_body_chk_tipo_entrada').next().html();
                    if ($('#ctl00_body_chk_ultima').is(':checked') == true) {
                        mensaje += ', <b>Última entrada.</b>';
                    }
                    mensaje += '<br />';
                    mensaje += '<ul>';
                    $('#ctl00_body_lstTransportes > option').each(function () {
                        mensaje += '<li>';
                        mensaje += this.text;
                        mensaje += '</li>';
                    });
                    mensaje += '</ul>';
                    //mensaje += '<b>Transporte:</b> ' + $('#ctl00_body_ddlTransporte option:selected').text() + ', Tipo ' + $('#ctl00_body_ddlTipo_Transporte option:selected').text();
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

        $('#ctl00_body_up_documento_recibido').panelReady(function () {

            var btnAdd_documento = $('#ctl00_body_btnAdd_documento');
            var btnRem_documento = $('#ctl00_body_btnRem_documento');

            $('.documentoRecibido').unbind('change').change(function () {
                $('#div_btnRemoveDR').show();
            });

            $(btnAdd_documento).button().unbind('click').click(function () {
                var txt_referencia_documento = $('#ctl00_body_txt_referencia_documento')
                if ($(txt_referencia_documento).val().length <= 1) {
                    alert('La referencia proporcionada es muy corta');
                    txt_referencia_documento.focus();
                    return false;
                }
                else
                    $('#ctl00_body_up_documento_recibido').addClass('ajaxLoading');
            });

            $(btnRem_documento).button().unbind('click').click(function () {
                $('#ctl00_body_up_documento_recibido').addClass('ajaxLoading');
            });

            $('#ctl00_body_up_documento_recibido').removeClass('ajaxLoading');
            $('#ctl00_body_ddlDocumento').trigger('change');
        });

        $('#ctl00_body_up_consolidada').panelReady(function () {

            btnAdd_pedimento = $('#ctl00_body_btnAdd_pedimento');
            btnRem_pedimento = $('#ctl00_body_btnRem_pedimento');
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

        $('#spnRefreshTransporte').button().click(function () {
            $('#ctl00_body_btnRefreshTransporte').trigger('click');
            $('#ctl00_body_upTransportes').addClass('ajaxLoading');
            $('#ctl00_body_upTipoTransporte').addClass('ajaxLoading');
            $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
        });

        $('#ctl00_body_upTransportes').panelReady(function () {
            $('#ctl00_body_upTransportes').removeClass('ajaxLoading');
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
            var btnRem_transporte = $('#ctl00_body_btnRem_transporte');

            var btnAddTransporte = $('#ctl00_body_btnAddTransporte').button().unbind('click').click(function () {
                var IsValid = true;
                $('.validatorTransporte').each(function () {
                    if ($(this).css('visibility') == 'visible') {
                        IsValid = false;
                        return false;
                    }
                });
                if (IsValid) {
                    $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
                }
            });

            $(ddlTipo_Transporte).unbind('change').change(function () {
                $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
            });

            $('.transporteAgregado').unbind('change').change(function () {
                $('#div_btnRemTransporte').show();
            });

            btnRem_transporte.button().unbind('click').click(function () {
                $('#ctl00_body_upDatosVehiculo').addClass('ajaxLoading');
            });

            $('#ctl00_body_upDatosVehiculo').removeClass('ajaxLoading');

        });

        $('#btn_bulto_declarado').button().click(function () {
            $('#ctl00_body_txt_no_bulto_recibido').val($('#ctl00_body_txt_no_bulto_declarado').val());
            return false;
        });

        $('#btn_pieza_declarada').click(function () {
            $('#ctl00_body_txt_no_pieza_recibida').val($('#ctl00_body_txt_no_pieza_declarada').val());
            return false;
        });

        $('#btn_show_cantidadesProblema').button().click(function () {
            $(this).hide();
            $('#cantidadesProblema').show('slow');
            return false;
        });

        loadError();
    }

    function loadError() {
        $('#errorMsgs').attr('title', $('#ctl00_body_hfTitleErr').val())

        $('#errorMsgs').dialog({
            autoOpen: false,
            height: 190,
            width: 420,
            modal: true,
            resizable: false
        });

        if ($('#ctl00_body_hfTitleErr').val().length > 0) {
            $('#errorMsg').html($('#ctl00_body_hfDescErr').val());
            $('#errorMsgs').dialog('open');
            $('#ctl00_body_hfTitleErr').val('');
        }
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
                        IsValid = false;
                        return false;
                        $(span).show();
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
}

//$(document).ready(function () {
//    var oMngEntrada = new MngEntrada();
//    oMngEntrada.Init();
//});

var master = new webApp.Master;
var pag = new MngReingreso();
master.Init(pag);