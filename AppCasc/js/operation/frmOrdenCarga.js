var lstRem = [];
var beanSalidaOrdenCarga = function (id_tipo_carga, id_salida_trafico, lstRem) {
    this.Id_tipo_carga = id_tipo_carga;
    this.Id_usuario = 0;
    this.Id_salida_trafico = id_salida_trafico;
    this.Folio_orden_carga = '';
    this.LstRem = lstRem;
}

var beanSalidaOrdenCargaRem = function (id_salida_remision, pallet) {
    this.Id_salida_remision = id_salida_remision;
    this.Id_salida_orden_carga = 0;
    this.Pallet = pallet;
    this.Referencia = '';
}

var MngOrdenCarga = function () {

    this.Init = init;

    function init() {
        //$("html, body").animate({ scrollTop: $(document).height() }, "slow");
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

    function getCitaBy(id_salida_trafico, id_orden_carga, folio_orden_carga, calEvent) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=cita&opt=getByIdTrafico",
            //dataType: "jsonp",
            data: {
                id_salida_trafico: id_salida_trafico
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {

                fillTblRemisiones(data, id_orden_carga, folio_orden_carga, calEvent);

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
    }

    // Llena la tabla de remisiones
    function fillTblRemisiones(data, id_orden_carga, folio_orden_carga, calEvent) {

        $('#btnSave').button({ disabled: true }).unbind('click').click(function () {
            saveOrdenCarga(calEvent);
            return false;
        });

        $('#lnk_dlt_orden_carga').unbind('click').click(function () {
            if (confirm('¿Desea eliminar el registro?')) {
                //deleteOrdenCarga(calEvent);
            }
        });

        $('#hf_id_salida_trafico').val(data.Id);
        $('#txt_cita').val(data.Folio_cita + ", " + moment(data.Fecha_cita).format('DD-MM-YYYY') + ", " + data.Hora_cita);
        $('#txt_destino').val(data.PSalidaDestino.Destino);
        $('#txt_linea').val(data.PTransporte.Nombre);
        $('#txt_unidad').val(data.PTransporteTipo.Nombre);
        $('#lnk_orden_carga').html(folio_orden_carga);
        $('#lnk_dlt_orden_carga').addClass('hidden');
        if (folio_orden_carga.length > 0)
            $('#lnk_dlt_orden_carga').removeClass('hidden');
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

        $.each(data.PLstSalRem, function (i, obj) {

            if (obj.Referencia != referencia) {
                tdRowSpan = 1;
                referencia = obj.Referencia;
                lstRefEqual = $.grep(data.PLstSalRem, function (obj) {
                    return obj.Referencia == referencia;
                });
                sumRefEqualPallet = 0;
                $.each(lstRefEqual, function (i, obj) {
                    sumRefEqualPallet += obj.Pallet;
                });
                tdRowSpan = lstRefEqual.length;
                tdFirstSpan = true;
            }

            var tr = '<tr id="rem_' + obj.Id + '">';
            tr += '<td align="left">' + obj.Referencia + '</td>';
            tr += '<td align="left">' + obj.Folio_remision + '</td>';
            tr += '<td align="left">' + obj.Orden + '</td>';
            tr += '<td align="left">' + obj.Codigo + '</td>';
            tr += '<td id="td_pieza_' + obj.Id + '" align="right">' + obj.PiezaTotal + '</td>';
            tr += '<td id="td_bulto_' + obj.Id + '" align="right">' + obj.BultoTotal + '</td>';
            if (tdRowSpan > 1) {
                if (tdFirstSpan) {
                    tr += '<td rowspan="' + tdRowSpan + '" id="txt_pallet_' + obj.Id + '" align="center">' + sumRefEqualPallet + '</td>';
                    tdFirstSpan = false;
                }
            }
            else
                tr += '<td id="txt_pallet_' + obj.Id + '" align="center">' + obj.Pallet + '</td>';
            // tr += '<td align="center"><input type="checkbox" readonly="readonly" checked="checked" class="chk_verificar_remisiones" id="chk_' + obj.Id + '" /></td>';
            tr += '</tr>';
            $('#tbody_remisiones').append(tr);

            pieza += obj.PiezaTotal;
            bulto += obj.BultoTotal;
            pallet += obj.Pallet;
            var rem = new beanSalidaOrdenCargaRem(obj.Id, obj.Pallet);
            lstRem.push(rem);
        });

        $('#td_pieza_total').html(pieza);
        $('#td_bulto_total').html(bulto);
        $('#td_pallet_total').html(pallet);
        $('#btnSave').button('option', 'disabled', lstRem.length == 0);

        //        $('.chk_verificar_remisiones').each(function () {
        //            $(this).click(function () {
        //                sumarSeleccionadas();
        //            });
        //        });
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
                // pallet += $('#txt_pallet_' + $(this).attr('id').split('_')[1]).val() * 1;
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
            url: "/handlers/Operation.ashx?op=cita&opt=getWithRem",
            //dataType: "jsonp",
            data: {
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
                        color: obj.id_orden_carga > 0 ? '' : 'orange',
                        folioOC: obj.folio_orden_carga,
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
        window.open('frmReporter.aspx?rpt=ordcarga&id=' + id_orden_carga, '_blank', 'toolbar=no');
    }

    function dltOrdenCarga(id_orden_carga, calEvent) {
        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=dltOrdenCarga',
            data: {
                id_orden_carga: id_orden_carga
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                
                //$('#dates_calendar').fullCalendar('updateEvent', calEvent);
                //$('#guia_embarque').dialog('close');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function saveOrdenCarga(calEvent) {
        var oOC = new beanSalidaOrdenCarga($('#ctl00_body_ddl_tipo_carga').val(), $('#hf_id_salida_trafico').val(), lstRem);
        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=AddOrdenCarga',
            data: JSON.stringify(oOC),
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

MngOrdenCarga.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngOrdenCarga();
master.Init(pag);