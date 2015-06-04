var beanEntrada_estatus = function (id, id_usuario, id_entrada_inventario, id_entrada_maquila, id_estatus_proceso, fecha) {

    this.Id = id;
    this.Id_usuario = id_usuario;
    this.Id_entrada_inventario = id_entrada_inventario;
    this.Id_entrada_maquila = id_entrada_maquila;
    this.Id_estatus_proceso = id_estatus_proceso;
    this.Fecha = fecha;

}

var MngRemision = function () {

    this.Init = init;

    //Init <<ini>>
    function init() {

        var arrBarCode = ['codigo', 'orden', 'vendor'];
        for (var barCode in arrBarCode) {
            var imageBase64 = '';
            imageBase64 = $('#ctl00_body_hf_img_' + arrBarCode[barCode]).val();
            if (imageBase64 != '')
                $('#img-' + arrBarCode[barCode]).attr('src', "data:image/png;base64," + imageBase64);
            else
                $('#img-' + arrBarCode[barCode]).hide();
            imageBase64 = '';
        }

        $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        var div_search = $('#div-search');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var up_resultados = $('#ctl00_body_up_resultados');
        var div_busqueda = $('#div_busqueda');
        var inventory_control = $('#div-floor-control');
        var hf_id_entrada_inventario = $('#ctl00_body_hf_id_entrada_inventario');
        var hf_id_entrada = $('#ctl00_body_hf_id_entrada');

        var btn_save = $('#ctl00_body_btn_save').button().click(function () {
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
                $('#disabledCantidadesSalida *').removeAttr("disabled");
            }
            return IsValid;
        });

        var first_focus = $('#ctl00_body_txt_bulto');
        var txt_fecha_remision = $('#ctl00_body_txt_fecha_remision');

        var div_tbl_folio_remision = $('#div-tbl-folio-remision');
        var imprimir_remision = $('#imprimir-remision');
        var eliminar_remision = $('#eliminar-remision');

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
        });

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

        $(div_search).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        $(inventory_control).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        if (hf_id_entrada_inventario.val() != 0) {
            div_search.next().hide();
            inventory_control.next().show();

        }

        $('#ctl00_body_txt_bulto, #ctl00_body_txt_piezasXbulto, #ctl00_body_txt_bultoInc, #ctl00_body_txt_piezasXbultoInc').blur(function () {
            calculateAmounts();
        });

        //feha de remision
        $(txt_fecha_remision).datepicker({
            'dateFormat': 'dd/mm/yy'
        });

        //folios-remisiones
        var oRemDetail = new RemDetail();
        oRemDetail.ShowRemisionDetail(div_tbl_folio_remision, eliminar_remision);

        $(imprimir_remision).button().click(function () {
            window.location.href = 'frmRemision.aspx?_fk=' + $(hf_id_entrada).val() + '&_pk=' + $(hf_id_entrada_inventario).val() + '&_key=' + $('#spn-dlt').html();
        });

        $(eliminar_remision).button().click(function () {
            if (confirm('¿Desea eliminar el registro?')) {
                $('#ctl00_body_hf_id_remision').val($('#spn-dlt').html());
                $('#ctl00_body_btnDltRemision').trigger('click');
            }
            return false;
        }).removeAttr('disabled');

        $(div_tbl_folio_remision).dialog({
            autoOpen: false,
            height: 270,
            width: 450,
            modal: true,
            resizable: false,
            close: function (event, ui) {
                $("html, body").animate({ scrollTop: $(document).height() }, "slow");
            }
        });

        //<<ini>> Eventos para el grid con detalle de la maquila (El usuario selecciona una fila y el sistema establece la cantidad de piezas por bulto)
        $('#ctl00_body_grdDetMaq tbody tr').each(function (i) {

            $(this).children('td').last().children('span').click(function () {
                //alert($(this).prev().val());
                if ($(this).hasClass('ui-icon-unlocked')) {
                    if (confirm('Desea cerrar esta maquila?'))
                        cerrarMaquila($(this).prev().val(), $(this));
                    else
                        return false;
                }
                else
                    alert('La maquila ya ha sido cerrada');
            });

            $(this).children('td').first().click(function () {

                var locked = true;
                if ($(this).parent().children('td').last().children('span').hasClass('ui-icon-unlocked')) {
                    locked = false;
                }

                if (!locked) {
                    alert('Es necesario cerrar la maquila total o parcialmente');
                    return false;
                }

                var Disponible = $('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(3)').html() * 1;

                if (Disponible <= 0) {
                    alert('Esta maquila ya no tiene bultos disponibles');
                    return false;
                }

                var pxb1 = $('#spn_pzaXbulto-1').html();
                var pxb2 = $('#spn_pzaXbulto-2').html();

                $('#hf_danado-1').val('false');
                $('#hf_danado-2').val('false');

                if (pxb1 == '') {
                    $('#spn_pzaXbulto-1').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(4)').html());
                    $('#hf_max_bulto-1').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(3)').html());
                    $('#spn_lote-1').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(6)').html())
                    $('#hf_danado-1').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(0)').html().trim() == 'Indemne' ? 'false' : 'true');
                }
                else {
                    if (pxb2 == '') {
                        $('#spn_pzaXbulto-2').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(4)').html());
                        $('#hf_max_bulto-2').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(3)').html());
                        $('#spn_lote-2').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(6)').html())
                        $('#hf_danado-2').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(0)').html().trim() == 'Indemne' ? 'false' : 'true');
                    }
                }

                pxb1 = $('#spn_pzaXbulto-1').html();
                pxb2 = $('#spn_pzaXbulto-2').html();

                if (pxb1 == pxb2 && $('#hf_danado-1').val() == $('#hf_danado-2').val()) {
                    $('#spn_pzaXbulto-2').html('');
                    $('#hf_max_bulto-2').val('');
                }

            });
        });

        $('#spn_del-1, #spn_del-2').click(function () {
            $(this).parent().parent().children('td:eq(1)').children('span').html('');
            $(this).parent().parent().children('td:eq(2)').children('span').html('');
            $('#ctl00_body_txt_bulto').val('0');
            $('#ctl00_body_txt_piezasXbulto').val('0');
            $('#ctl00_body_hf_mercancia_danada').val('false');
            $('#ctl00_body_hf_lote_1').val('');
            $('#ctl00_body_txt_bultoInc').val('0');
            $('#ctl00_body_txt_piezasXbultoInc').val('0');
            $('#ctl00_body_hf_mercancia_danadaInc').val('false');
            $('#ctl00_body_hf_lote_2').val('');
            $('#div-danada').addClass('hidden');
            lotesClear();
            lotesSet();
            calculateAmounts();
        });

        $('#spn_add-1, #spn_add-2').click(function () {
            var CantBultoValido = true;
            var cantBulto = 0;
            var cantMaxBulto = 0;

            lotesClear();

            switch ($(this).attr('id')) {
                case 'spn_add-1':
                    cantBulto = $('#txt_bulto-1').val();
                    cantMaxBulto = $('#hf_max_bulto-1').val() * 1;
                    if (cantBulto > 0 && cantBulto <= cantMaxBulto) {
                        $('#ctl00_body_txt_bulto').val(cantBulto);
                        $('#ctl00_body_txt_piezasXbulto').val($('#spn_pzaXbulto-1').html());
                        $('#ctl00_body_hf_mercancia_danada').val($('#hf_danado-1').val());
                        $('#ctl00_body_hf_lote_1').val($('#spn_lote-1').html().replace('&nbsp;', ''));
                    }
                    else
                        CantBultoValido = false;
                    break;
                case 'spn_add-2':
                    cantBulto = $('#txt_bulto-2').val();
                    cantMaxBulto = $('#hf_max_bulto-2').val() * 1;
                    if (cantBulto > 0 && cantBulto <= cantMaxBulto) {
                        $('#ctl00_body_txt_bultoInc').val(cantBulto);
                        $('#ctl00_body_txt_piezasXbultoInc').val($('#spn_pzaXbulto-2').html());
                        $('#ctl00_body_hf_mercancia_danadaInc').val($('#hf_danado-2').val());
                        $('#ctl00_body_hf_lote_2').val($('#spn_lote-2').html().replace('&nbsp;', ''));
                    }
                    else
                        CantBultoValido = false;
                    break;
            }
            if (CantBultoValido) {

                lotesSet();

                calculateAmounts();
                $('#div-danada').addClass('hidden');
                if ($('#ctl00_body_hf_mercancia_danada').val() == 'true' || $('#ctl00_body_hf_mercancia_danadaInc').val() == 'true') {
                    $('#div-danada').removeClass('hidden');
                    $('#ctl00_body_txt_dano').val('');
                }
                else {
                    $('#ctl00_body_txt_dano').val('SinDano');
                }
            }
            else {
                alert('Es necesario proporcionar una catidad de bultos mayor a 0 y menor a ' + cantMaxBulto);
                $(this).prev().focus();
            }
        });

        $('#disabledCantidadesSalida *').attr("disabled", "disabled");

        //<<fin>> Eventos para el grid con detalle de la maquila (El usuario selecciona una fila y el sistema establece la cantidad de piezas por bulto)
        verifyLote();
        first_focus.focus().select();
    }
    //Init <<fin>>

    //Calculo de cantidades
    function calculateAmounts() {

        var bultos;
        var piezasXbulto;
        var piezas;
        var totalPiezas;

        bultos = $('#ctl00_body_txt_bulto').val() * 1;
        piezasXbulto = $('#ctl00_body_txt_piezasXbulto').val() * 1;
        piezas = bultos * piezasXbulto;
        totalPiezas = piezas;
        $('#ctl00_body_txt_piezas').val(piezas);

        //Incidencias
        bultos = $('#ctl00_body_txt_bultoInc').val() * 1;
        piezasXbulto = $('#ctl00_body_txt_piezasXbultoInc').val() * 1;
        piezas = bultos * piezasXbulto;
        totalPiezas = totalPiezas + piezas;
        $('#ctl00_body_txt_piezasInc').val(piezas);

        $('#ctl00_body_txt_piezaTotal').val(totalPiezas);
    }

    //Cerrar maquila parcial
    function cerrarMaquila(idEntradaMaquila, span) {

        var o = new beanEntrada_estatus(0, $('#ctl00_body_hf_idUsuario').val() * 1, $('#ctl00_body_hf_id_entrada_inventario').val() * 1, idEntradaMaquila, 0);

        $.ajax({
            type: "POST",
            url: "/handlers/Operation.ashx?op=closeMaqPar",
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {

                alert(data);
                $(span).removeClass('ui-icon-unlocked').addClass('ui-icon-locked');

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function lotesClear() {
        $('#spnLotes').html('');
    }

    function lotesSet() {
        var lotes = $('#spn_lote-1').html();
        if ($('#spn_lote-2').html() != '' && $('#spn_lote-2').html() != lotes)
            lotes += ", " + $('#spn_lote-2').html();
        $('#spnLotes').html(lotes);
    }

    function verifyLote() {
        var hasLote = Boolean($('#ctl00_body_hf_HasLote').val());
        $('.hasLote').each(function () {
            if (hasLote)
                $(this).show();
            else
                $(this).hide();
        });
    }
}


var master = new webApp.Master;
var pag = new MngRemision();
master.Init(pag);