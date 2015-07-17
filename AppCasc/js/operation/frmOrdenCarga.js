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
                getCitaBy(calEvent.id);
                // change the border color just for fun
                // $(this).css('border-color', 'red');

            }
        });
    }

    function getCitaBy(id_salida_trafico) {
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

                fillTblRemisiones(data);

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
        $('#txt_cita').val('');
        $('#txt_destino').val('');
        $('#txt_linea').val('');
        $('#txt_unidad').val('');
        $('#tbody_remisiones').html('');
        $('#td_pieza_total').html('0');
        $('#td_bulto_total').html('0');
        $('#td_pallet_total').html('0');
    }

    // Llena la tabla de remisiones
    function fillTblRemisiones(data) {

        $('#txt_cita').val(data.Folio_cita + ", " + moment(data.Fecha_cita).format('DD-MM-YYYY') + ", " + data.Hora_cita);
        $('#txt_destino').val(data.Destino);
        $('#txt_linea').val(data.PTransporte.Nombre);
        $('#txt_unidad').val(data.PTransporteTipo.Nombre);

        $.each(data.PLstSalRem, function (i, obj) {
            var tr = '<tr id="rem_' + obj.Id + '">';
            tr += '<td align="left">' + obj.Referencia + '</td>';
            tr += '<td align="left">' + obj.Folio_remision + '</td>';
            tr += '<td align="left">' + obj.Orden + '</td>';
            tr += '<td align="left">' + obj.Codigo + '</td>';
            tr += '<td id="td_pieza_' + obj.Id + '" align="right">' + obj.PiezaTotal + '</td>';
            tr += '<td id="td_bulto_' + obj.Id + '" align="right">' + obj.BultoTotal + '</td>';
            tr += '<td align="center"><input style="text-align: center;" class="txtSmall" type="text" value="0" id="txt_pallet_' + obj.Id + '" /></td>';
            tr += '<td align="center"><input type="checkbox" class="chk_verificar_remisiones" id="chk_' + obj.Id + '" /></td>';
            tr += '</tr>';
            $('#tbody_remisiones').append(tr);
        });

        $('.chk_verificar_remisiones').each(function () {
            $(this).click(function () {
                var pieza = 0;
                var bulto = 0;
                var pallet = 0;
                $('.chk_verificar_remisiones').each(function () {
                    if ($(this).is(':checked')) {
                        pieza += $('#td_pieza_' + $(this).attr('id').split('_')[1]).html() * 1;
                        bulto += $('#td_bulto_' + $(this).attr('id').split('_')[1]).html() * 1;
                        pallet += $('#txt_pallet_' + $(this).attr('id').split('_')[1]).val() * 1;
                    }
                });
                $('#td_pieza_total').html(pieza);
                $('#td_bulto_total').html(bulto);
                $('#td_pallet_total').html(pallet);
            });
        });
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
                        end: obj.end
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

    }
}

MngOrdenCarga.printOrdeCarga = function (url) {
    window.location.href = url;
    return false;
}

var master = new webApp.Master;
var pag = new MngOrdenCarga();
master.Init(pag);