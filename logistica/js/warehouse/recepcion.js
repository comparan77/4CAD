/// <reference path="almacenModel.js" />
/// <reference path="../process/procesosModel.js" />
/// <reference path="../catalog/catalogosModel.js" />

var Recepcion = function () {

    var oCortDisp = { Id_usuario: 1 };

    this.Init = function () {

        $('#asn').tab('show');

        fileUploadAjax = new AjaxInputUpload({
            content: 'fileUploadAjax',
            fileAccept: '.csv',
            processFunction: AlmacenModel.recepcionImportData,
            checkStatusFunction: AlmacenModel.recepcionImportDataStatus,
            resultDataShowedFunction: AlmacenModel.recepcionDataResultShowed
        });

        fileUploadAjax.open();

        initializeEvents();
    }

    function initializeEvents() {
        tab_admin_tab();
        btn_asignar_cortina_click();
        li_calendario_click();
    }

    function li_calendario_click() {
        $('#li_calendario').click(function () {
            $('#nav_asn_folio').addClass('hidden');
            $('#calendar_asn').removeClass('hidden');
            $('#div_folio_info').addClass('hidden');
        });
    }

    function btn_asignar_cortina_click(asn_event) {
        var btn = $('#btn_asignar_cortina');
        $('#btn_asignar_cortina').unbind('click').click(function () {
            oCortDisp.Id = 0;
            oCortDisp.Id_cortina = $('#ddl_cortina').select2('data')[0].Id;
            var nombreCortina = $('#ddl_cortina').select2('data')[0].text;
            console.log(JSON.stringify(oCortDisp));
            $(this).html('Asignando la cortina ...');
            $(this).addClass('disabled');
            AlmacenModel.recepcionCortinaTomar(
                JSON.stringify(oCortDisp),
                function (data) {
                    $(btn).html('Asignar Cortina');
                    $(btn).removeClass('disabled');

                    $('#nav_asn_folio').addClass('hidden');
                    $('#calendar_asn').removeClass('hidden');
                    $('#div_folio_info').addClass('hidden');

                    asn_event.color = '';
                    asn_event.cortinaAsignada = data;
                    asn_event.cortinaNombre = nombreCortina;

                    $('#calendar_asn').fullCalendar('updateEvent', asn_event);
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                });
        });
    }

    function tab_admin_tab() {
        $('#adminTab').on('shown.bs.tab', function (e) {
            var tabSelect = $(e.target).attr('href');
            switch (tabSelect) {
                case '#asn':
                    fileUploadAjax.clearUploadStatus();
                    initCalendar();
                    break;
                case '#imp':
                    fileUploadAjax.startUploadStatus();
                    break;
                default:
                    fileUploadAjax.clearUploadStatus();
                    break;

            }
            //console.log($(e.target).attr('href'));
        });
    }

    function initCalendar() {

        $('#calendar_asn').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: false
            },
            themeSystem: 'bootstrap4',
            locale: 'es',
            defaultView: 'agendaWeek',
            axisFormat: 'HH:mm',
            minTime: '06:00:00',
            maxTime: '24:59:59',
            height: 450,
            events: function (start, end, timezone, callback) {
                fillAsn(moment(start).format('YYYY-MM-DD'), callback);
            },
            eventClick: function (calEvent, jsEvent, view) {
                //alert(calEvent.title);
                if (calEvent.cortinaAsignada.Id_cortina == 0)
                    fillAsnByID(calEvent);
                else
                    alert('El ASN ya ha sido asignado la cortina: ');
            },
            eventRender: function (event, element) {

                element.qtip({
                    content: '<br />' + event.cortinaAsignada.Id == 0 ? 'Sin cortina asignada' : event.cortinaNombre,
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

    function fillAsnByID(asn_event) {

        ProcesosModel.procesosSltById(
        'asn',
        { key: asn_event.id },
        function (data) {

            $('#nav_asn_folio').removeClass('hidden');
            $('#calendar_asn').addClass('hidden');
            $('#div_folio_info').removeClass('hidden');
            $('#li_folio').html(asn_event.title);
            $('#txt_cliente').val(data.ClienteNombre);
            $('#txt_bodega').val(data.BodegaNombre);
            $('#txt_referencia').val(data.Referencia);
            $('#txt_transporte').val(data.TransporteNombre);
            $('#txt_sello').val(data.Sello);
            $('#txt_operador').val(data.Operador);
            $('#txt_tarima').val(data.Pallet);
            $('#txt_caja').val(data.Caja);
            $('#txt_pieza').val(data.Pieza);

            oCortDisp.Id_asn = asn_event.id;
            oCortDisp.Tarima_x_recibir = data.Pallet;
            oCortDisp.Tarima_recibida = 0;

            AlmacenModel.recepcionCortinaDispBodega(
                { pk: data.Id_bodega },
                function (data) {
                    fillDdlCortina(data, asn_event)
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function fillDdlCortina(data, asn_event) {
        var dataMap = $.map(data, function (obj) {
            obj.id = obj.Id; // replace pk with your identifier
            obj.text = obj.Nombre;
            return obj;
        });

        $('#ddl_cortina').select2({

            placeholder: "Selecciona una cortina disponible",
            data: dataMap
        });

        btn_asignar_cortina_click(asn_event);
    }

    function fillAsn(start, callback) {
        ProcesosModel.procesosLst(
        'asn',
        function (data) {
            var citasAsn = [];
            $(data).each(function (i, obj) {

                citasAsn.push({
                    id: obj.Id,
                    title: obj.Folio,
                    start: obj.Fecha_hora.replace('T', ' '),
                    //end: moment(obj.Fecha_hora).format('YYYY-MM-DD HH:mm:ss')
                    color: obj.PCortinaAsignada.Id_cortina > 0 ? '' : 'orange',
                    cortinaAsignada: obj.PCortinaAsignada,
                    cortinaNombre: obj.CortinaNombre == null ? '' : obj.CortinaNombre
                    //folioOC: obj.folio_orden_carga.indexOf('OCA') >= 0 ? obj.folio_orden_carga : '',
                    //idOC: obj.id_orden_carga
                });
            });
            callback(citasAsn);
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }
}

var master = new webApp.Master;
var pag = new Recepcion();
master.Init(pag);