var lstRem = [];

var beanTarAlmCargaDet = function (id_tarima_almacen_remision_detail, id_tarima_almacen) {
    this.Id = 0;
    this.Id_tarima_almacen_carga = 0;
    this.Id_tarima_almacen_remision_detail = id_tarima_almacen_remision_detail;
    this.Id_tarima_almacen = id_tarima_almacen;
}

var beanSalidaOrdenCarga = function (id_tipo_carga, id_salida_trafico, lstrem) {
    this.Id_tipo_carga = id_tipo_carga;
    this.Id_usuario = 0;
    this.Id_salida_trafico = id_salida_trafico;
    this.Folio_orden_carga = '';
    this.LstRem = lstrem;
}

var beanSalidaOrdenCargaRem = function (id_salida_remision, pallet) {
    this.Id_salida_remision = id_salida_remision;
    this.Id_salida_orden_carga = 0;
    this.Pallet = pallet;
    this.Referencia = '';
}

var beanUsuario_cancelacion = function (motivo_cancelacion) {
    this.Id = 0;
    this.Usuario_cancelacion = 0;
    this.Folio_operacion = '';
    this.Motivo_cancelacion = motivo_cancelacion;
}

var MngOCWH = function () {

    this.Init = init;

    function init() {
        initCalendar();
        initDialogGuiaEmbarque();
    }

    // Inicia full calendar con las citas
    function initCalendar() {

        $('#dates_calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: false
            },
            defaultView: 'agendaWeek',
            theme: true,
            axisFormat: 'HH:mm',
            minTime: '06:00:00',
            maxTime: '24:59:59',
            events: function (start, end, timezone, callback) {
                fillCitas(moment(start).format('YYYY-MM-DD'), callback);
            },
            eventClick: function (calEvent, jsEvent, view) {

                //alert('Event: ' + calEvent.id);
                if ($('#guia_embarque').dialog('isOpen'))
                    $('#guia_embarque').dialog('close');

                clearGuiaEmbarque();
                getCitaBy(calEvent.id, calEvent.idOC, calEvent.folioOC, calEvent);
                // change the border color just for fun
                // $(this).css('border-color', 'red');

            },
            eventRender: function (event, element) {
                element.qtip({
                    content: event.title + '<br />' + event.folioOC,
                    style: {
                        background: 'black',
                        color: '#FFFFFF'
                    },
                    position: {
                        corner: {
                            target: 'center',
                            tooltip: 'bottomMiddle'
                        }
                    }
                });
            }
        });
    }

    function getCitaBy(id_tarima_almacen_trafico, id_orden_carga, folio_orden_carga, calEvent) {
        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            //dataType: "jsonp",
            data: {
                'case': 'trafico',
                opt: 'getByIdTrafico',
                id_tarima_almacen_trafico: id_tarima_almacen_trafico
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                id_orden_carga = data.PCarga != null ? data.PCarga.Id : 0;
                fillTblRemisiones(data, id_orden_carga, folio_orden_carga, calEvent);
                lstRem = [];
                $('#guia_embarque').dialog('open');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    // Limpia los datos de la guia de embarque
    function clearGuiaEmbarque() {
        $('#hf_id_salida_trafico').val('');
        $('#txt_cita').val('');
        $('#txt_destino').val('');
        $('#txt_linea').val('');
        $('#txt_unidad').val('');
        $('#tbody_remisiones').html('');
        $('#td_pieza_total').html('0');
        $('#td_bulto_total').html('0');
        $('#td_pallet_total').html('0');
        $('#chk_todo').prop('checked', false);
        $('#lnk_orden_carga').attr('href', '#');
        $('#lnk_orden_carga').html('');
        $('#h_orden_carga').val(0);

        $('#tbody_cargadas').html('');
        $('#spn_rem_selected').html('');
        $('#ul_tarimas_disp').html('');
        $('#txt_tar_car').val('');
    }

    // Llena la tabla de remisiones
    function fillTblRemisiones(data, id_orden_carga, folio_orden_carga, calEvent) {

        $('#btnSave').button({ disabled: true }).unbind('click').click(function () {
            saveOrdenCarga(calEvent);
            return false;
        });

        $('#lnk_dlt_orden_carga').unbind('click').click(function () {
            var motivo = prompt('Proporcione el motivo de la cancelación:', '')
            if (motivo.length < 5) {
                alert('El motivo es muy corto, proporcione un motivo descriptivo');
            }
            else
                dltOrdenCarga($('#h_orden_carga').val(), motivo, calEvent);
        });

        $('#hf_id_salida_trafico').val(data.Id);
        $('#txt_cita').val(data.Folio_cita + ", " + moment(data.Fecha_cita).format('DD-MM-YYYY') + ", " + data.Hora_cita);
        $('#txt_destino').val(data.PSalidaDestino.Destino);
        $('#txt_linea').val(data.PTransporte.Nombre);
        $('#txt_unidad').val(data.PTransporteTipo.Nombre);

        $('#lnk_dlt_orden_carga').addClass('hidden');
        if (folio_orden_carga.indexOf('OCA') >= 0) {
            $('#lnk_orden_carga').html(folio_orden_carga);
            $('#lnk_dlt_orden_carga').removeClass('hidden');
        }
        $('#h_orden_carga').val(id_orden_carga);

        var pieza = 0;
        var bulto = 0;
        var pallet = 0;
        var referencia = '';
        var tdRowSpan = 1;
        var cantRefEqual = 0;
        var lstRefEqual = [];
        var tdFirstSpan = true;
        var sumRefEqualPallet = 0;

        var cargaCompleta = true;

        $.each(data.PLstRem, function (i, obj) {

            if (cargaCompleta) {
                cargaCompleta = obj.TarimaTotal == obj.CargaTotal;
            }

            var tr = '<tr id="rem_' + obj.Id + '">';
            tr += '<td align="left">' + obj.Folio + '</td>';
            tr += '<td align="left">' + obj.Mercancia_codigo + '</td>';
            tr += '<td id="td_pallet_' + obj.Id + '" align="center">' + obj.TarimaTotal + '</td>';
            tr += '<td id="td_bulto_' + obj.Id + '" align="center">' + obj.CajaTotal + '</td>';
            tr += '<td id="td_pieza_' + obj.Id + '" align="center">' + obj.PiezaTotal + '</td>';
            tr += '<td align="center"><span id="spnIdRem_' + obj.Id + '" folioRem="' + obj.Folio + '" class="icon-button-action selectRem' + (obj.TarimaTotal != obj.CargaTotal ? ' error' : '') + '">' + obj.CargaTotal + '</span></td>';
            //            tr += '<td align="center"><input type="checkbox" readonly="readonly" class="chk_verificar_remisiones" id="chk_' + obj.Id + '" /></td>';
            tr += '</tr>';
            $('#tbody_remisiones').append(tr);

            //            pieza += obj.TarimaTotal;
            //            bulto += obj.CajaTotal;
            //            pallet += obj.PiezaTotal;
            //            var rem = new beanSalidaOrdenCargaRem(obj.Id, obj.Pallet);
            //            lstRem.push(rem);
        });

        $('#btnSave').button('option', 'disabled', !cargaCompleta);

        $('.selectRem').each(function () {
            $(this).click(function () {
                selectRem($(this).attr('id').split('_')[1] * 1, $(this).attr('folioRem'), calEvent);
            });
        });
        showStep(1);
        //        $('.chk_verificar_remisiones').each(function () {
        //            $(this).click(function () {
        //                sumarSeleccionadas();
        //            });
        //        });
    }

    function selectRem(id_rem, folio, calEvent) {

        $('#tbody_cargadas').html('');
        $('#spn_rem_selected').html('');

        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            //dataType: "jsonp",
            data: {
                'case': 'rem_det',
                opt: 'getCargadas',
                id_tarima_almacen_remision: id_rem
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {

                $('#spn_rem_selected').html('<< ' + folio).unbind('click').click(function () {
                    var id_salida_trafico = $('#hf_id_salida_trafico').val();
                    var id_orden_carga = $('#h_orden_carga').val();
                    var folio_orden_carga = $('#lnk_orden_carga').html();
                    clearGuiaEmbarque();
                    getCitaBy(id_salida_trafico, id_orden_carga, folio_orden_carga, calEvent);
                });
                var tr = '';
                $.each(data, function (i, obj) {
                    tr = '<tr>';
                    tr += '<td>' + obj.Mercancia_codigo + '</td>';
                    tr += '<td align="center">' + obj.Rr + '</td>';
                    tr += '<td align="center">' + obj.Estandar + '</td>';
                    tr += '<td align="center">' + obj.Tarimas + '</td>';
                    tr += '<td align="center">' + obj.Cajas + '</td>';
                    tr += '<td align="center">' + obj.Piezas + '</td>';
                    tr += '<td align="center"><span sol="' + obj.Tarimas + '" est="' + obj.Estandar + '" id="spnIdRemDet_' + obj.Id + '" class="icon-button-action selectRemDet' + (obj.Tarimas != obj.Cargadas ? ' error' : '') + '">' + obj.Cargadas + '</span></td>';
                    tr += '</tr>';
                    $('#tbody_cargadas').append(tr);
                });

                showStep(2);

                $('.selectRemDet').each(function () {
                    $(this).click(function () {
                        selectRemDet($(this).attr('id').split('_')[1] * 1, folio, $(this).attr('est'), id_rem, $(this).attr('sol'), calEvent);
                    });
                });

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function selectRemDet(id_rem_det, folio, est, id_rem, solicitadas, callEvent) {

        $('#ul_tarimas_disp').html('');
        $('#txt_estandar_sel').val('');
        $('#txt_tar_sol').val('');
        $('#txt_tar_car').val('');

        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            //dataType: "jsonp",
            data: {
                'case': 'rem_det',
                opt: 'getCargadasDet',
                id_tarima_almacen_remision_detail: id_rem_det
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {

                $('#spn_rem_det_selected').html('<< ' + folio).unbind('click').click(function () { selectRem(id_rem, folio, callEvent); });
                $('#txt_estandar_sel').val(est);
                $('#txt_tar_sol').val(solicitadas);

                var li = '';
                var countSeleccionadas = 0;

                $.each(data.PLstTarAlm, function (i, obj) {
                    li = '';
                    li += '<li style="padding: 5px;' + (obj.Seleccionado > 0 ? 'font-weight: bold;' : '') + '" id="liTarAlm_' + obj.Id + '" class="floatLeft liTarAlm">' + obj.Folio + '</li>';
                    $('#ul_tarimas_disp').append(li);
                    if (obj.Seleccionado) {
                        countSeleccionadas++;
                    }
                });

                $('#txt_tar_car').val(countSeleccionadas);
                if (countSeleccionadas != solicitadas)
                    $('#txt_tar_car').addClass('error');
                else
                    $('#txt_tar_car').removeClass('error');

                showStep(3);

                $('.liTarAlm').each(function () {
                    $(this).click(function () {
                        saveTarAlmOC(id_rem_det, folio, est, id_rem, solicitadas, $(this).attr('id').split('_')[1] * 1, callEvent);
                    });
                });

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function saveTarAlmOC(id_rem_det, folio, est, id_rem, solicitadas, id_tar_alm, callEvent) {

        var obeanTarAlmCargaDet = new beanTarAlmCargaDet(id_rem_det, id_tar_alm);

        $.ajax({
            type: "POST",
            url: '/handlers/Almacen.ashx?case=carga_det&opt=saveMove',
            data: JSON.stringify(obeanTarAlmCargaDet),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                selectRemDet(id_rem_det, folio, est, id_rem, solicitadas, callEvent);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function showStep(stepNumber) {
        $('#step1, #step2, #step3').hide();
        $('#step' + stepNumber).show('slow');
    }

    function sumarSeleccionadas() {

        var pieza = 0;
        var bulto = 0;
        var pallet = 0;
        lstRem = [];
        $('.chk_verificar_remisiones').each(function () {
            if ($(this).is(':checked')) {
                pieza += $('#td_pieza_' + $(this).attr('id').split('_')[1]).html() * 1;
                bulto += $('#td_bulto_' + $(this).attr('id').split('_')[1]).html() * 1;
                pallet += $('#td_pallet_' + $(this).attr('id').split('_')[1]).html() * 1;
                var rem = new beanSalidaOrdenCargaRem($(this).attr('id').split('_')[1] * 1, pallet);
                lstRem.push(rem);
            }
        });
        $('#td_pieza_total').html(pieza);
        $('#td_bulto_total').html(bulto);
        $('#td_pallet_total').html(pallet);
        $('#btnSave').button('option', 'disabled', lstRem.length == 0);
    }

    // Llena las citas en el calendario via ajax.
    function fillCitas(firstDate, callback) {
        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            //dataType: "jsonp",
            data: {
                'case': 'trafico',
                opt: 'getWithRem',
                start: firstDate
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                var events = [];
                $(data).each(function (i, obj) {
                    events.push({
                        id: obj.id,
                        title: obj.title,
                        start: obj.start,
                        end: obj.end,
                        color: obj.folio_orden_carga.indexOf('OCA') >= 0 ? '' : 'orange',
                        folioOC: obj.folio_orden_carga.indexOf('OCA') >= 0 ? obj.folio_orden_carga : '',
                        idOC: obj.id_orden_carga
                    });
                });
                callback(events);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function initDialogGuiaEmbarque() {

        $('#guia_embarque').dialog({
            autoOpen: false,
            resizable: false,
            //height: 250,
            width: 850,
            modal: true,
            draggable: false
        });

        $('#chk_todo').click(function () {
            var isCheckAll = $(this).is(':checked');
            $('.chk_verificar_remisiones').each(function () {
                $(this).prop('checked', isCheckAll);
            });
            sumarSeleccionadas();
        });

        $('#lnk_orden_carga').click(function () {
            //alert($(this).attr('href'));
            var idOC = $('#h_orden_carga').val() * 1;
            if (idOC > 0)
                printOC(idOC);
            return false;
        });
    }

    function printOC(id_orden_carga) {
        window.open('frmReportViewer.aspx?rpt=carga&_key=' + id_orden_carga, '_blank', 'toolbar=no');
    }

    function dltOrdenCarga(id_orden_carga, motivo, calEvent) {

        var oUsrCancelacion = new beanUsuario_cancelacion(motivo);

        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=dltOrdenCarga&id_orden_carga=' + id_orden_carga,
            data: JSON.stringify(oUsrCancelacion),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                if (data.indexOf('correctamente') > 0) {
                    calEvent.color = 'orange';
                    calEvent.idOC = 0;
                    calEvent.folioOC = '';
                    $('#dates_calendar').fullCalendar('updateEvent', calEvent);
                }
                $('#guia_embarque').dialog('close');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function saveOrdenCarga(calEvent) {
        $.ajax({
            type: "POST",
            url: '/handlers/Almacen.ashx?case=carga&opt=udtFolioProv',
            data: JSON.stringify($('#h_orden_carga').val()),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                var idOC = data.Id;
                if (idOC > 0) {
                    alert('Se guardo corréctamente el registro');
                    calEvent.color = '';
                    calEvent.idOC = idOC;
                    calEvent.folioOC = data.Folio_orden_carga;
                    $('#dates_calendar').fullCalendar('updateEvent', calEvent);
                    printOC(idOC);
                }
                else {
                    alert(data);
                }
                $('#guia_embarque').dialog('close');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }
}

MngOCWH.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngOCWH();
master.Init(pag);