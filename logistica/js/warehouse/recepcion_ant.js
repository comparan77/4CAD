/// <reference path="almacenModel.js" />
/// <reference path="../catalogos/catalogosModel.js" />
/// <reference path="../common.js" />

var dataCteMcia = [];

var Recepcion = function () {

    this.Init = function () {
        callCortinaOcupada(verificaCortinaOcupada);
        
        fileUploadAjax = new AjaxInputUpload({
            content: 'fileUploadAjax',
            fileAccept: '.csv',
            processFunction: AlmacenModel.recepcionImportData,
            checkStatusFunction: AlmacenModel.recepcionImportDataStatus,
            resultDataShowedFunction: AlmacenModel.recepcionDataResultShowed
        });

        fileUploadAjax.open();
    }

    function verificaCortinaOcupada(data) {
        $('#pnl_ddl').addClass('hidden');
        $('#pnl_lbl').addClass('hidden');
        $('#div_cortina_ocupada').addClass('hidden');
        switch (typeof (data)) {
            case 'object':
                if (data.Id != 0) {
                    $('#pnl_lbl').removeClass('hidden');
                    $('#div_cortina_ocupada').removeClass('hidden');
                    fillControls(data);
                    loadMercanciaCliente($('#hf_id_cliente').val(), fillDataMercanciaCliente);
                    //console.log(JSON.stringify(data));
                    $('#txt_tarima_declarada').val(data.Por_recibir + data.Tarimas);
                    fillTarimasData(data);
                }
                else {
                    $('#pnl_ddl').removeClass('hidden');
                }
                break;
            default:
                console.log(data);
        }
        initControls();
    }

    function fillControls(data) {
        $('#hf_id_cortina_disponible').val(data.Id);
        $('#hf_id_cliente').val(data.Id_cliente);
        $('#spn_cliente').html(data.Cliente);
        $('#spn_bodega').html(data.Bodega);
        $('#h4-action-des').html('Descargando en ' + data.Cortina + ' . . .');
    }

    function callCortinaOcupada(callback) {
        AlmacenModel.recepcionCortinaVerificarUsuario(callback, function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function initControls() {
        loadCliente();
        loadBodega();
        initializeEvents();
    }

    function loadCliente() {

        CatalogosModel.catalogosLstAll('cliente', function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                return obj;
            });

            $('#ddl_cliente').select2({
                
                placeholder: "Selecciona un cliente",
                data: dataMap,
                allowClear: true
            });
        });
    }

    function loadBodega() {

        CatalogosModel.catalogosLstAll('bodega', function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                return obj;
            });

            $('#ddl_bodega').select2({
                
                placeholder: "Selecciona una bodega",
                data: dataMap,
                allowClear: true
            });
        });
    }

    function loadCortinaDisponible(id_bodega, callback) {
        AlmacenModel.recepcionCortinaDispBodega({ pk: id_bodega }, callback, function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function loadMercanciaCliente(id_cliente, callback) {

        CatalogosModel.catalogosLstAllBy(
            'mercancia',
            { key: id_cliente },
            callback
       );
    }

    function fillDataMercanciaCliente(data) {
        var dataMap = $.map(data, function (obj) {
            obj.id = obj.Id; // replace pk with your identifier
            obj.text = obj.Nombre;
            return obj;
        });
        dataCteMcia = dataMap;
        $('#ddl_mercancia_cliente').html('');

        if ($('#ddl_mercancia_cliente').hasClass("select2-hidden-accessible")) {
            // Destroy Select2
            $('#ddl_mercancia_cliente').select2('destroy');
            // Unbind the event
            $('#ddl_mercancia_cliente').off('select2:select');
        }

        $('#ddl_mercancia_cliente').select2({
            
            placeholder: "Selecciona una mercancía",
            data: dataCteMcia,
            allowClear: true
        });
        $('#ddl_mercancia_cliente').val(null).trigger('change');
        $('#ddl_mercancia_cliente').on('select2:select', function (e) {
            var id_mercancia_cliente = $('#ddl_mercancia_cliente').select2('data')[0].Id;
            $('#txt_pieza_x_caja').val($('#ddl_mercancia_cliente').select2('data')[0].Piezas_x_caja);
            $('#txt_cajas_x_tarima').val($('#ddl_mercancia_cliente').select2('data')[0].Cajas_x_tarima);
        });
    }

    function initializeEvents() {
        ddl_cliente_change();
        ddl_bodega_change();
        btn_tomar_cortina_click();
        btn_liberar_cortina_click();
        btn_tarima_descargada_click();
        tab_admin_tab();
    }

    function ddl_cliente_change() {
        $('#ddl_cliente').on('select2:select', function (e) {
            loadMercanciaCliente($('#ddl_cliente').select2('data')[0].Id, fillDataMercanciaCliente);
        });
    }

    function ddl_bodega_change() {
        $('#ddl_bodega').on('select2:select', function (e) {
            $('#ddl_cortina').html('');
            loadCortinaDisponible($('#ddl_bodega').select2('data')[0].Id, function (data) {
                var option = '<option value="0">Selecciona una cortina</option>';
                $.each(data, function (i, obj) {
                    option += '<option value=' + obj.Id + '>' + obj.Nombre + '</option>';
                });
                $('#ddl_cortina').html(option);
                $('#div_cortina').removeClass('hidden');
            });
        });
    }

    function btn_tomar_cortina_click() {
        $('#btn_tomar_cortina').click(function () {

            if ($('#ddl_cortina').val() == 0) return false;

            $(this).addClass('disabled');
            $(this).html('Tomando cortina ...');

            var objCDisp = {
                Id: 0,
                Id_usuario: 1,
                Id_cliente: $('#ddl_cliente').select2('data')[0].Id,
                Cliente: $('#ddl_cliente').select2('data')[0].text,
                Id_bodega: $('#ddl_bodega').select2('data')[0].Id,
                Bodega: $('#ddl_bodega').select2('data')[0].text,
                Id_cortina: $('#ddl_cortina').val(),
                Cortina: $('#ddl_cortina option:selected').text(),
                Por_recibir: 0,
                Tarimas: 0
            };

            var btn = this;

            AlmacenModel.recepcionCortinaTomar(JSON.stringify(objCDisp),
            function (data) {

                $('#pnl_ddl').addClass('hidden');
                $('#pnl_lbl').removeClass('hidden');
                $('#div_cortina').addClass('hidden');
                $('#div_cortina_ocupada').removeClass('hidden');
                fillControls(data);

                $(btn).removeClass('disabled');
                $(btn).html('Tomar Cortina');

            }, function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
        });
    }

    function btn_liberar_cortina_click() {
        $('#btn_liberar_cortina').unbind('clikc').click(function () {

            $(this).addClass('disabled');
            $(this).html('Liberando cortina ...');

            var btn = this;

            AlmacenModel.recepcionCortinaLiberar({ pk: $('#hf_id_cortina_disponible').val() },
                function (data) {

                    $(btn).removeClass('disabled');
                    $(btn).html('Liberar Cortina');

                    $('#pnl_ddl').removeClass('hidden');
                    $('#pnl_lbl').addClass('hidden');
                    $('#div_cortina').removeClass('hidden');
                    $('#div_cortina_ocupada').addClass('hidden');

                    $('#aspnetForm')[0].reset();
                },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
        });
    }

    function fillTarimasData(data) {
        $('#txt_tarima_descargada').val(data.Tarimas);
        var tarimas_x_descargar = $('#txt_tarima_declarada').val() * 1;
        tarimas_x_descargar = tarimas_x_descargar - data.Tarimas;
        $('#txt_tarima_x_descargar').val(tarimas_x_descargar);
    }

    function calcularTarimas(callback) {
        var objCDisp = {
            Id: $('#hf_id_cortina_disponible').val(),
            Id_usuario: 0,
            Id_cliente: 0,
            Cliente: '',
            Id_bodega: 0,
            Bodega: '',
            Id_cortina: 0,
            Cortina: '',
            Por_recibir: $('#txt_tarima_declarada').val(),
            Tarimas: 0
        };

        AlmacenModel.recepcionCortinaTarimaPush(JSON.stringify(objCDisp),
            function (data) {
                fillTarimasData(data);
                if (callback) callback(data);
            }, function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
    }

    function btn_tarima_descargada_click() {
        $('#btn_tarima_descargada').click(function () {

            $(this).addClass('disabled');
            $(this).html('Agregando cortina descargada al sistema ...');
            var btn = this;
            calcularTarimas(function (data) {
                $(btn).removeClass('disabled');
                $(btn).html('Agergar tarima descargada');
            });

        });
    }

    function tab_admin_tab() {
        $('#adminTab').on('shown.bs.tab', function (e) {
            var tabSelect = $(e.target).attr('href');
            switch (tabSelect) {
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
}

var master = new webApp.Master;
var pag = new Recepcion();
master.Init(pag);