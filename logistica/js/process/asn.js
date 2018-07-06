/// <reference path="procesosModel.js" />
/// <reference path="../catalog/catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Asn = function () {

    var grdCatalog;
    var tabCatalog;
    var id_catalog;
    var id_bodega;
    var id_transporte;

    this.Init = function () {

        id_catalog = {
        };

        fillgrdCatalog();
        tabCatalog = new TabCatalog({
            catalogo: 'asn',
            callBackSaveData: saveData,
            parametersGet: parametersGet,
            callBackEnbDis: saveData,
            callBackChangeTab: changeTab
        });
        tabCatalog.init();

        initControls();
    }

    function changeTab(tab) {
        switch (tab) {
            case "admon":
                $('#h4-action').html($('#ddl_almacen').select2('data')[0].text);
                break;
            case "list":
                $('#h4-action').html('');
                //                
                break;
            default:
        }
    }

    function saveData() {

    }

    function parametersGet() {
        return {
            Id_bodega: id_bodega,
            Nombre: $('#txt_nombre').val()
        };
    }

    function initControls() {
        loadCatalogs(['cliente', 'bodega', 'transporte', 'aduana'], loadedCatalogs);
        $('#txt_fecha').datepicker({
            language: 'es',
            startDate: new Date(),
            autoclose: true
        })

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
                return obj;
            });

            $('#ddl_cliente_mercancia').select2({
                tags: "true",
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
                    return obj;
                });

                $('#ddl_' + catalog).select2({
                    tags: "true",
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

    function changeTab(tab) {
        switch (tab) {
            case "admon":

                break;
            default:

        }
    }

    function saveData() {
        ProcesosModel.procesosLst('asn', function (data) {
            grdCatalog.DataBind(data, function () {
                tabCatalog.changeListTab();
            });
        },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            }
        );
    }

    function parametersGet() {
        return {
            Nombre: $('#txt_nombre').val(),
            Direccion: $('#txt_direccion').val()
        };
    }

    function fillgrdCatalog() {
        grdCatalog = new DataGrid({
            idtable: 'grdCatalog',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                //                td = document.createElement('td');
                //                field = document.createTextNode(obj.Nombre);
                //                td.appendChild(field);
                //                tr.appendChild(td);

                //                td = document.createElement('td');
                //                field = document.createTextNode(obj.Direccion);
                //                td.appendChild(field);
                //                tr.appendChild(td);

                //                td = document.createElement('td');
                //                field = document.createTextNode(obj.IsActive == true ? 'Si' : 'No');
                //                td.appendChild(field);
                //                tr.appendChild(td);

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
    }

    function btn_add_click() {
        $('#btn_add').click(function () {
            
        });
    }

    function change_radio_tipo() {
        $('input[type=radio][name=tipo]').change(function () {
            if (this.value == 'nacional') {
                $('#div_extranjero').addClass('hidden');
            }
            else if (this.value = 'extranjero') {
                $('#div_extranjero').removeClass('hidden');
            }
        });
    }

}

var master = new webApp.Master;
var pag = new Asn();
master.Init(pag);