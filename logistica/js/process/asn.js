﻿/// <reference path="procesosModel.js" />
/// <reference path="../catalog/catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Asn = function () {

    var grdCatalog;
    var tabCatalog;
    var id_catalog;

    var id_transporte;
    var arrPartidas = [];
    var arrTotal = [];
    var arrTransporteTipo = [];
    var PLstTranSello = [];

    var EsPedimento = false;

    this.Init = function () {

        id_catalog = {
        };

        fillgrdCatalog();
        tabCatalog = new TabCatalog({
            catalogo: 'asn',
            isCatalog: false,
            callBackSaveData: saveData,
            parametersGet: parametersGet,
            callBackChangeTab: changeTab
        });
        tabCatalog.init();

        initControls();
    }

    function changeTab(tab) {
        switch (tab) {
            case "admon":
                //$('#h4-action').html($('#ddl_almacen').select2('data')[0].text);
                break;
            case "list":
                $('#h4-action').html('');
                //                
                break;
            default:
        }
    }

    function initControls() {

        $('#txt_hora').clockpicker({
            autoclose: true,
            'default': new Date().getHours() + ':' + new Date().getMinutes()
        });

        $('#txt_anio').val(moment(new Date()).format('YYYY'));


        loadCatalogs(['cliente', 'bodega', 'transporte', 'aduana', 'transporte_tipo'], loadedCatalogs);
        $('#txt_fecha').datepicker({
            language: 'es',
            startDate: new Date(),
            autoclose: true
        }).on('changeDate', function (e) {
            var fecha_asn = moment(e.date).format('YYYY-MM-DD');
            id_catalog.fecha = fecha_asn;
        });

        id_catalog.fecha = moment(new Date()).format('YYYY-MM-DD');

        initializeEvents();
    }

    function loadedCatalogs() {
        fillMercanciaByCliente();
    }

    function fillMercanciaByCliente() {
        CatalogosModel.catalogosLstAllBy('mercancia', { pk: id_catalog.id_cliente }, function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Sku;
                obj.nombre = obj.Nombre;
                obj.pxc = obj.Piezas_x_caja;
                obj.cxt = obj.Cajas_x_tarima;
                return obj;
            });

            $('#ddl_cliente_mercancia').select2({

                placeholder: "Selecciona una opción",
                data: dataMap,
                theme: "classic"
            });

            var id_value = $('#ddl_cliente_mercancia').select2('data')[0].Id;

            id_catalog["id_cliente_mercancia"] = id_value;
            ddl_cliente_mercancia_change();
        });
    }

    function ddl_cliente_mercancia_change() {

    }

    function loadCatalogs(arr_catalog, callback, loaded) {
        if (loaded == undefined) {
            loaded = 0;
        }
        if (loaded < arr_catalog.length) {

            var catalog = arr_catalog[loaded];

            CatalogosModel.catalogosLst(catalog, function (data) {
                var dataMap = $.map(data, function (obj) {
                    obj.id = obj.Id; // replace pk with your identifier
                    obj.text = obj.Nombre;
                    if (catalog == 'aduana') {
                        obj.clave = obj.Clave;
                        obj.text = obj.Clave + ' - ' + obj.Nombre;
                    } else if (catalog == 'transporte_tipo') {
                        obj.TransTipo = { caja1: obj.Requiere_caja1, caja2: obj.Requiere_caja2, caja3: obj.Requiere_caja3 };
                    }
                    return obj;
                });

                $('#ddl_' + catalog).select2({

                    placeholder: "Selecciona una opción",
                    data: dataMap,
                    theme: "classic"
                });

                var id_value = $('#ddl_' + catalog).select2('data')[0].Id;

                id_catalog["id_" + catalog] = id_value;

                loaded++;
                loadCatalogs(arr_catalog, callback, loaded);

            });
        } else {
            if (callback) callback();
        }
    }

    function saveData(obj, btn) {
        ProcesosModel.procesosAdd('asn', JSON.stringify(obj), function () {
            $(btn).removeClass('disabled');
            $(btn).html('Guardar');

            ProcesosModel.procesosLst('asn', function (data) {
                grdCatalog.DataBind(data, function (data) {
                    tabCatalog.changeListTab();
                });
            },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });

        },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            }
        );
    }

    function parametersGet() {

        $('#tbody_sellos').children('tr').each(function (i, obj) {
            PLstTranSello.push({
                Id: 0,
                Id_asn: 0,
                Contenedor: $(this).children(':nth-child(2)').children('input').val(),
                Sello: $(this).children(':nth-child(3)').children('input').val()
            });
        });

        var ref = '';
        if (EsPedimento) {
            ref = $('#ddl_aduana').select2('data')[0].clave + '-' + $('#txt_patente').val() + '-' + $('#txt_anio').val().substr(3) + $('#txt_documento').val();
        }

        return {
            Id_cliente: id_catalog.id_cliente,
            Folio: '',
            Referencia: ref,
            Id_bodega: id_catalog.id_bodega,
            Fecha_hora: id_catalog.fecha + ' ' + $('#txt_hora').val(),
            Id_transporte: id_catalog.id_transporte,
            Sello: $('#txt_sello').val(),
            Operador: $('#txt_operador').val(),
            Pallet: arrTotal[0].tarima,
            Caja: arrTotal[0].caja,
            Pieza: arrTotal[0].pieza,
            PLstPartida: arrPartidas,
            PLstTranSello: PLstTranSello
        };
    }

    function fillgrdCatalog() {
        grdCatalog = new DataGrid({
            idtable: 'grdCatalog',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Folio);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(moment(obj.Fecha_hora).format('DD-MM-YYYY'));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.ClienteNombre);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Referencia);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Pallet);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Caja);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Pieza);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Descargada == true ? 'Si' : 'No');
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                ProcesosModel.procesosLstById(
                    'asn',
                    { key: id },
                    function (data) {
                        //                        var nombre = data.Nombre;
                        //                        var direccion = data.Direccion;

                        //                        $('#txt_nombre').val(nombre);
                        //                        $('#txt_direccion').val(direccion);

                        //                        tabCatalog.validateOptActive(data.IsActive);

                        //                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        ProcesosModel.procesosLst('asn', function (data) {
            grdCatalog.DataBind(data, function (data) {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function initializeEvents() {
        change_radio_tipo();
        btn_add_click();
        txt_tarima_blur();
        change_transporte_tipo();
    }

    function change_transporte_tipo() {
        $('#ddl_transporte_tipo').on('select2:select', function (e) {
            //console.log(JSON.stringify($('#ddl_transporte_tipo').select2('data')[0].TransTipo));
            var arrTipo = [];
            var TransTipo = $('#ddl_transporte_tipo').select2('data')[0].TransTipo;
            var rowNum = 1;
            if (TransTipo.caja1)
                arrTipo.push({ id: rowNum++ });
            if (TransTipo.caja2)
                arrTipo.push({ id: rowNum++ });
            if (TransTipo.caja3)
                arrTipo.push({ id: rowNum++ });

            //console.log(JSON.stringify(TransTipo));

            Common.fillTableBody('tbody_sellos', arrTipo, 'id', function (tr, obj) {
                var td = document.createElement('td');
                var txt_caja = document.createElement('input');
                td.appendChild(txt_caja);
                tr.appendChild(td);

                td = document.createElement('td');
                var txt_sello = document.createElement('input');
                td.appendChild(txt_sello);
                tr.appendChild(td);
            });
        });
    }

    function txt_tarima_blur() {
        $('#txt_tarima').blur(function () {
            //alert($('#ddl_cliente_mercancia').select2('data')[0].cxt);
        });
    }

    function btn_add_click() {
        $('#btn_add').click(function () {
            var partida = {
                Id: arrPartidas.length + 1,
                Sku: $('#ddl_cliente_mercancia').select2('data')[0].text,
                Tarima: $('#txt_tarima').val() * 1,
                Caja: $('#txt_caja').val() * 1,
                Pieza: $('#txt_pieza').val() * 1
            };

            if (arrPartidas.filter(function (obj) {
                return obj.Sku == partida.Sku;
            }).length == 0) {

                arrPartidas.push(partida);
                Common.fillTableBody('t_body_partidas', arrPartidas, 'Id', function (tr, obj) {
                    td = document.createElement('td');
                    var button = document.createElement('button');
                    button.setAttribute('id', 'rem_par_' + obj.Id);
                    button.setAttribute('type', 'button');
                    button.className = 'btn-link';
                    button.innerHTML = '-';
                    button.addEventListener('click', function () {
                        alert($(this).attr('id').split('_')[2]);
                        this.blur();
                    });
                    td.appendChild(button);
                    tr.appendChild(td);
                }, function () {
                    var total = {
                        no: '',
                        cant: arrPartidas.length,
                        tarima: arrPartidas.reduce(function (a, b) {
                            return { Tarima: a.Tarima + b.Tarima };
                        }).Tarima,
                        caja: arrPartidas.reduce(function (a, b) {
                            return { Caja: a.Caja + b.Caja };
                        }).Caja,
                        pieza: arrPartidas.reduce(function (a, b) {
                            return { Pieza: a.Pieza + b.Pieza };
                        }).Pieza,
                        vacio: ''
                    };
                    arrTotal = [];
                    arrTotal.push(total);
                    Common.fillTableBody('t_foot_partidas', arrTotal);
                });
            } else {
                alert('El sku ya fué agregado');
            }
            this.blur();
        });
    }

    function change_radio_tipo() {
        $('input[type=radio][name=tipo]').change(function () {
            EsPedimento = false;
            if (this.value == 'nacional') {
                $('#div_extranjero').addClass('hidden');
            }
            else if (this.value = 'extranjero') {
                $('#div_extranjero').removeClass('hidden');
                EsPedimento = true;
            }
        });
    }

}

var master = new webApp.Master;
var pag = new Asn();
master.Init(pag);