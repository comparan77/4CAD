/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Tarifa = function () {

    var grdCatalog;
    var grdServiceTarifa;
    //    var tabCatalog;
    var id_cliente;
    var cliente;
    var id_servicio;

    this.Init = function () {

        //        tabCatalog = new TabCatalog({
        //            catalogo: 'mercancia',
        //            callBackSaveData: saveData,
        //            parametersGet: parametersGet,
        //            callBackEnbDis: saveData,
        //            callBackChangeTab: changeTab
        //        });

        //        tabCatalog.init();

        initControls();
    }

    function changeTab(tab) {
        switch (tab) {
            case "admon":
                $('#h4-action').html($('#ddl_cliente').select2('data')[0].text);
                break;
            case "list":
                $('#h4-action').html('');
                $('#ddl_cliente').val(id_cortina); // Select the option with a value of '1'
                $('#ddl_cliente').trigger('change');
                break;
            default:
        }
    }

    function saveData() {
        CatalogosModel.tarifaClienteMercancia(
                { key: id_cliente },
                function (data) {
                    grdCatalog.DataBind(data, function () {
                        //                        tabCatalog.changeListTab();

                    });
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
    }

    function parametersGet() {
        return {
            Id_cliente: id_cliente,
            Sku: $('#txt_sku').val(),
            Upc: $('#txt_upc').val(),
            Nombre: $('#txt_nombre').val(),
            Precio: $('#txt_precio').val(),
            Piezas_x_caja: $('#txt_piezas_x_caja').val(),
            Cajas_x_tarima: $('#txt_cajas_x_tarima').val()
        };
    }

    function InitGrdServiceTarifa() {
        grdServiceTarifa = new DataGrid({
            idtable: 'grdServiceTarifa',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Sku);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Nombre);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(Common.getCurrencyFormat(obj.Tarifa, 2));
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {

            }
        });

        console.log(typeof grdServiceTarifa);
    }

    function fillgrdCatalog() {
        grdCatalog = new DataGrid({
            idtable: 'grdCatalog',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Nombre);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Tarifas);
                td.appendChild(field);
                td.addEventListener('click', function () {
                    fillSelectedService(
                        grdCatalog.getIdSelected(this),
                        'tarifa'
                    );
                });
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Total_mercancia - obj.Tarifas);
                td.addEventListener('click', function () {
                    fillSelectedService(
                        grdCatalog.getIdSelected(this),
                        'notarifa'
                    );
                });
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {

            }
        });

        CatalogosModel.tarifaClienteMercancia(
            { key: id_cliente },
            function (data) {
                grdCatalog.DataBind(data, function (data) {
                });
            },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            }
        );
    }

    function fillSelectedService(obj, tipo) {
        switch (tipo) {
            case "tarifa":
                CatalogosModel.tarifaClienteMercanciaServicio(
                        {
                            id_cliente: id_cliente,
                            id_servicio: obj.Id
                        },
                        function (data) {

                            grdServiceTarifa.DataBind(data, function (data) {
                                $('#li_cliente_tipo').html(cliente + ' con Tarifa');
                                $('#li_servicio').html(obj.col_0);
                                showSelectedService();
                            });
                        },
                        function (jqXHR, textStatus) {
                            alert("Request failed: " + textStatus);
                        }
                    );
                break;
            case "notarifa":
                CatalogosModel.tarifaClienteMercanciaNoServicio(
                        {
                            id_cliente: id_cliente,
                            id_servicio: obj.Id
                        },
                        function (data) {

                            grdServiceTarifa.DataBind(data, function (data) {
                                $('#li_cliente_tipo').html(cliente + ' sin Tarifa');
                                $('#li_servicio').html(obj.col_0);
                                showSelectedService();
                            });
                        },
                        function (jqXHR, textStatus) {
                            alert("Request failed: " + textStatus);
                        }
                    );
                break;
            default:

        }
    }

    function showSelectedService() {
        $('#div_unselected_service').addClass('hidden');
        $('#div_selected_service').removeClass('hidden');
        $('#div_bread_service').removeClass('hidden');
    }

    function hideSelecedService() {
        $('#div_unselected_service').removeClass('hidden');
        $('#div_selected_service').addClass('hidden');
        $('#div_bread_service').addClass('hidden');
        $('#li_cliente_tipo').html('');
        $('#li_servicio').html('');
    }

    function initControls() {
        loadCliente();
        initializeEvents();
        InitGrdServiceTarifa();
    }

    function loadCliente() {

        CatalogosModel.catalogosLstAll('cliente', function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                return obj;
            });

            $('#ddl_cliente').select2({
                tags: "true",
                placeholder: "Selecciona un cliente",
                data: dataMap
            });

            id_cliente = $('#ddl_cliente').select2('data')[0].Id;
            cliente = $('#ddl_cliente').select2('data')[0].text;

            fillgrdCatalog();
        });
    }

    function initializeEvents() {
        ddl_change();
        li_cliente_tipo_click();
    }

    function li_cliente_tipo_click() {
        $('#li_cliente_tipo').click(function () {
            CatalogosModel.tarifaClienteMercancia(
                { key: id_cliente },
                function (data) {
                    grdCatalog.DataBind(data, function (data) {
                        hideSelecedService();
                    });
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        });
    }

    function ddl_change() {
        $('#ddl_cliente').on('select2:select', function (e) {
            id_cliente = $('#ddl_cliente').select2('data')[0].Id;
            cliente = $('#ddl_cliente').select2('data')[0].text;
            CatalogosModel.tarifaClienteMercancia(
                { key: id_cliente },
                function (data) {
                    grdCatalog.DataBind(data, function (data) {
                    });
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        });
    }

}

var master = new webApp.Master;
var pag = new Tarifa();
master.Init(pag);