var beanEntrada_estatus = function (id, id_usuario, id_entrada_inventario, id_entrada_maquila, id_estatus_proceso, fecha) {

    this.Id = id;
    this.Id_usuario = id_usuario;
    this.Id_entrada_inventario = id_entrada_inventario;
    this.Id_entrada_maquila = id_entrada_maquila;
    this.Id_estatus_proceso = id_estatus_proceso;
    this.Fecha = fecha;

}

var beanCliente_mercancia = function (codigo, nombre) {
    
    this.Id = 0;
    this.Id_cliente_grupo = 0;
    this.Codigo = codigo;
    this.Nombre = nombre;
    this.Clase = '';
    this.Negocio = '';
    this.Valor_unitario = 0;
    this.Unidad = '';
    this.Presentacion_x_bulto = 0;
    this.Bultos_x_tarima = 0;
    this.IsActive = false;
}

var CitaRemision = function (folio_cita, id_remision) {
    this.Folio_cita = folio_cita;
    this.Id_remision = id_remision;
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
            height: 290,
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
                //                if ($(this).hasClass('ui-icon-unlocked')) {
                //                    if (confirm('Desea cerrar esta maquila?'))
                //                        cambiarMaquila($(this).prev().val(), $(this));
                //                }
                //                else
                //                    if (confirm('Desea abrir esta maquila?'))
                //                        cambiarMaquila($(this).prev().val(), $(this));

            });

            $(this).children('td').first().click(function () {

                //                var locked = true;
                //                if ($(this).parent().children('td').last().children('span').hasClass('ui-icon-unlocked')) {
                //                    locked = false;
                //                }

                //                if (!locked) {
                //                    alert('Es necesario cerrar la maquila total o parcialmente');
                //                    return false;
                //                }
                var idEntrada_maquila_detail = $(this).parent().children('td').last().children('input').val();

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
                    $('#hf_id_maquila_detail_1').val(idEntrada_maquila_detail);
                    $('#spn_pzaXbulto-1').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(4)').html());
                    $('#hf_max_bulto-1').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(3)').html());
                    $('#spn_lote-1').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(6)').html())
                    $('#hf_danado-1').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(0)').html().trim() == 'Indemne' ? 'false' : 'true');
                }
                else {
                    if (pxb2 == '') {
                        $('#hf_id_maquila_detail_2').val(idEntrada_maquila_detail);
                        $('#spn_pzaXbulto-2').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(4)').html());
                        $('#hf_max_bulto-2').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(3)').html());
                        $('#spn_lote-2').html($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(6)').html())
                        $('#hf_danado-2').val($('#ctl00_body_grdDetMaq tbody tr:eq(' + i + ')').children('td:eq(0)').html().trim() == 'Indemne' ? 'false' : 'true');
                    }
                }

                pxb1 = $('#hf_id_maquila_detail_1').val();
                pxb2 = $('#hf_id_maquila_detail_2').val();

                if (pxb1 == pxb2) {
                    $('#spn_pzaXbulto-2').html('');
                    $('#hf_max_bulto-2').val('');
                    $('#hf_id_maquila_detail_2').val('');
                    $('#spn_lote-2').html('');
                }

            });
        });

        $('#spn_del-1, #spn_del-2').click(function () {
            $(this).parent().parent().children('td:eq(1)').children('span').html('');
            $(this).parent().parent().children('td:eq(2)').children('span').html('');

            //            $('#ctl00_body_hf_id_entrada_maquila_detail_1').val('0');
            $('#ctl00_body_txt_bulto').val('0');
            $('#ctl00_body_txt_piezasXbulto').val('0');
            $('#ctl00_body_hf_mercancia_danada').val('false');
            $('#ctl00_body_hf_lote_1').val('');

            //            $('#ctl00_body_hf_id_entrada_maquila_detail_2').val('0');
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
                        //                        $('#ctl00_body_hf_id_entrada_maquila_detail_1').val($('#hf_id_maquila_detail_1').val());
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
                        //                        $('#ctl00_body_hf_id_entrada_maquila_detail_2').val($('#hf_id_maquila_detail_2').val());
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

        //<<ini>> citas

        $('#div_citas').dialog({
            autoOpen: false,
            resizable: false,
            //height: 250,
            width: 450,
            modal: true
        });

        $('#spn-folio_cita').click(function () {
            //if('#spn-tieneOrdenCarga').html()
            if ($('#spn-tieneOrdenCarga').html() != 'False') {
                alert('La remision ya cuenta con orden de carga');
            }
            else {
                fillCitas(0);
            }
        });

        $('#spn_folio_cita').click(function () {
            fillCitas();
        });
        //<<fin>> citas

        // Ordenes y códigos del pedimento
        $('#div_ordenescodigos').dialog({
            autoOpen: false,
            resizable: false,
            width: 850,
            modal: true
        });
        $('#lnk_pedimento').click(function () {
            $('#div_ordenescodigos').dialog('open');
            return false;
        });

        //Cambio de la descripción de la mercancía
        $('#btn_udt_description').button().click(function () {
            var description = $('#txt_new_description').val();
            var codigo = $('#hf_codigo_udt').val();
            if (description.length == 0) {
                alert('Es necesario proporcionar una nueva descripción');
            }
            else {
                var obeanCliente_mercancia = new beanCliente_mercancia(codigo, description);
                MercanciaDescriptionChange(obeanCliente_mercancia);
                $('#div_udt_mercancia').dialog('close');
            }
        });
        $('#div_udt_mercancia').dialog({
            autoOpen: false,
            resizable: false,
            width: 650,
            modal: true
        });
        $('#lnk_mercancia').click(function () {
            $('#div_udt_mercancia').dialog('open');
            return false;
        });

        //Cambio del estado de la maquila (abierta o cerrada por pedimento, orden y codigo)
        $('#spn_estado_maquila').click(function () {
            if ($(this).hasClass('ui-icon-unlocked'))
                alert('La maquila sólo puede ser cerrada desde el módulo de Maquila');
            else {
                if (confirm('Abrir la maquila permitirá la modificación de datos en el módulo de Maquila, desea continuar?')) {
                    MaquilaEstadoChange(hf_id_entrada_inventario.val());
                }
            }
        });
    }
    //Init <<fin>>

    //Cambiar el estado de la maquila
    function MaquilaEstadoChange(id_entrada_inventario) {
        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=MqStateChange',
            data: id_entrada_inventario,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                $('#spn_estado_maquila').removeClass('ui-icon-locked');
                $('#spn_estado_maquila').addClass('ui-icon-unlocked');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    //Cambio de la mercancia ajax
    function MercanciaDescriptionChange(o) {
        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=MciaDescChange',
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                $('#lnk_mercancia').html(o.Nombre);
                $('#txt_old_description').val(o.Nombre);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    //Citas
    function fillCitas(NuevaRemision) {

        NuevaRemision = typeof NuevaRemision !== 'undefined' ? NuevaRemision : 1;

        $('#ul_citas').html('');
        $('#spn_cita_sel').html('')
        $('#ctl00_body_txt_folio_cita').val('');

        var bultos = 0;
        if (NuevaRemision == 1)
            bultos = $('#ctl00_body_txt_bulto').val() * 1 + $('#ctl00_body_txt_bultoInc').val() * 1;
        else
            bultos = $('#spn-bulto-1').html() * 1 + $('#spn-bulto-2').html() * 1;

        if (bultos > 0) {

            $.ajax({
                type: 'GET',
                url: '/handlers/Operation.ashx',
                data: {
                    op: 'cita',
                    opt: 'getCitas',
                    id_entrada_inventario: $('#ctl00_body_hf_id_entrada_inventario').val(),
                    bultos: bultos,
                    folio_cita: NuevaRemision == 1 ? '' : $('#spn-folio_cita').html()
                },
                complete: function () {
                    $('#div-info-codigo').removeClass('ajaxLoading');
                },
                success: function (data) {

                    if (data.length == 0) {
                        $('#ul_citas').append('<li>Sin citas <br /><a href="/operation/frmTrafico.aspx">Solicitar citas aqu&iacute;</a></li>');
                    }
                    else {
                        $(data).each(function (i, obj) {
                            var liCita = '<li>';
                            liCita += '<font color="#0d5fb3">Folio:&nbsp;</font>' + obj.Folio_cita + '<font color="#0d5fb3">&nbsp;Fecha:&nbsp;</font>' + moment(obj.Fecha_cita).format("DD/MM/YYYY");
                            liCita += '&nbsp;<font color="#0d5fb3">Hora:</font>&nbsp;' + obj.Hora_cita;
                            liCita += '<div style="position: relative">';
                            liCita += '<ul>';
                            liCita += '<li><b>Destino:</b> ' + obj.PSalidaDestino.Destino + '</li>';
                            liCita += '<li><b>Línea:</b> ' + obj.Transporte + '</li>';
                            liCita += '<li><b>Operador:</b> ' + obj.Operador + '</li>';
                            liCita += '<li><b>Tipo:</b> ' + obj.Transporte_tipo_cita + '</li>';
                            liCita += '<li><b>Placa:</b> ' + obj.Placa + '</li>';
                            liCita += '<li><b>Fecha Carga Solicitada:</b> ' + moment(obj.Fecha_carga_solicitada).format('DD/MM/YYYY') + '</li>';
                            liCita += '<li><b>Hora Carga Solicitada:</b> ' + obj.Hora_carga_solicitada + '</li>';
                            liCita += '</ul>'
                            liCita += '<button class="btn_sel_cita" id="btn_cita_' + obj.Id + '" style="position: absolute; top: 20px; right: 20px;">Seleccionar</button>';
                            liCita += '<span id="spn_pallet_' + obj.Id + '" style="position: absolute; top: 60px; right: 80px; font-size: 2.5em; color:' + (!obj.Pallet ? 'black' : obj.Pallet < 21 ? 'black' : obj.Pallet == 22 ? 'yellow' : 'red') + '">' + (!obj.Pallet ? '0' : obj.Pallet) + '</span>';
                            liCita += '<span style="position: absolute; top: 100px; right: 60px;">Pallet(s)</span>';
                            liCita += '<input type="hidden" id="hf_folio_cita_' + obj.Id + '" value="' + obj.Folio_cita + '" />';
                            liCita += '</div></li>';
                            liCita += '<hr />'

                            $('#ul_citas').append(liCita);
                        });

                        $('.btn_sel_cita').each(function () {
                            $(this).button().click(function () {
                                var id = $(this).attr('id').split('_')[2];
                                if (NuevaRemision == 1) {
                                    $('#ctl00_body_txt_folio_cita').val(id);
                                    $('#spn_cita_sel').html($('#hf_folio_cita_' + id).val());
                                    $('#div_citas').dialog('close');
                                }
                                else {
                                    if (confirm('Se va a modificar la cita de la remisión, desea continuar con la modificación?')) {
                                        udtCita($('#hf_folio_cita_' + id).val(), $('#spn-dlt').html());
                                    }
                                }
                            });
                        });

                    }

                    $('#div_citas').dialog('open');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    var oErrorMessage = new ErrorMessage();
                    oErrorMessage.SetError(jqXHR.responseText);
                    oErrorMessage.Init();
                }
            });
        }
        else {
            alert('Es necesario seleccionar la maquila y proporcionar una cantidad de bultos');
        }
    }

    //Actualizacion de cita en remisiones
    function udtCita(folio_cita, id_remision) {

        var o = new CitaRemision(folio_cita, id_remision);

        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=cita&opt=udtCita',
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {
                $('#div_citas').dialog('close');
            },
            success: function (data) {
                alert(data);
                $('#spn-folio_cita').html(folio_cita);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    //Calculo de cantidades
    function calculateAmounts() {

        var bultos;
        var piezasXbulto;
        var piezas;
        var totalPiezas;

        var oCommon = new Common();

        bultos = oCommon.GetOnlyDecimal($('#ctl00_body_txt_bulto').val()) * 1;
        piezasXbulto = oCommon.GetOnlyDecimal($('#ctl00_body_txt_piezasXbulto').val()) * 1;
        piezas = bultos * piezasXbulto;
        totalPiezas = piezas;
        $('#ctl00_body_txt_piezas').val(piezas);

        //Incidencias
        bultos = oCommon.GetOnlyDecimal($('#ctl00_body_txt_bultoInc').val()) * 1;
        piezasXbulto = oCommon.GetOnlyDecimal($('#ctl00_body_txt_piezasXbultoInc').val()) * 1;
        piezas = bultos * piezasXbulto;
        totalPiezas = totalPiezas + piezas;
        $('#ctl00_body_txt_piezasInc').val(piezas);

        $('#ctl00_body_txt_piezaTotal').val(totalPiezas);
    }

    //Cerrar maquila parcial
    //    function cambiarMaquila(idEntradaMaquila, span) {

    //        var o = new beanEntrada_estatus(0, $('#ctl00_body_hf_idUsuario').val() * 1, $('#ctl00_body_hf_id_entrada_inventario').val() * 1, idEntradaMaquila, 0);

    //        $.ajax({
    //            type: "POST",
    //            url: '/handlers/Operation.ashx?op=changeMaqPar',
    //            data: JSON.stringify(o),
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            complete: function () {

    //            },
    //            success: function (data) {

    //                alert(data);
    //                if ($(span).hasClass('ui-icon-unlocked'))
    //                    $(span).removeClass('ui-icon-unlocked').addClass('ui-icon-locked');
    //                else
    //                    $(span).removeClass('ui-icon-locked').addClass('ui-icon-unlocked');
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                var oErrorMessage = new ErrorMessage();
    //                oErrorMessage.SetError(jqXHR.responseText);
    //                oErrorMessage.Init();
    //            }
    //        });
    //    }

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