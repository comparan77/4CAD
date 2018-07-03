/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Mercancia = function () {

    var grdCatalog;
    var tabCatalog;
    var id_cliente;

    this.Init = function () {

        tabCatalog = new TabCatalog({
            catalogo: 'mercancia',
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
        CatalogosModel.catalogosLstBy(
                'mercancia',
                { key: id_cliente },
                function (data) {
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
            Id_cliente: id_cliente,
            Sku: $('#txt_sku').val(),
            Upc: $('#txt_upc').val(),
            Nombre: $('#txt_nombre').val(),
            Precio: $('#txt_precio').val(),
            Piezas_x_caja: $('#txt_piezas_x_caja').val(),
            Cajas_x_tarima: $('#txt_cajas_x_tarima').val()
        };
    }

    function fillgrdCatalog() {
        grdCatalog = new DataGrid({
            idtable: 'grdCatalog',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Sku);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Upc);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Nombre);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Precio);
                td.appendChild(field);
                tr.appendChild(td);

//                td = document.createElement('td');
//                field = document.createTextNode(obj.Cajas_x_tarima);
//                td.appendChild(field);
//                tr.appendChild(td);

//                td = document.createElement('td');
//                field = document.createTextNode(obj.Piezas_x_caja);
//                td.appendChild(field);
//                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                CatalogosModel.catalogosSltById(
                    'mercancia',
                    { key: id },
                    function (data) {

                        var sku = data.Sku;
                        var upc = data.Upc;
                        var nombre = data.Nombre;
                        var precio = data.Precio;
                        var piezas_x_caja = data.Piezas_x_caja;
                        var cajas_x_tarima = data.Cajas_x_tarima;

                        $('#txt_sku').val(sku);
                        $('#txt_upc').val(upc);
                        $('#txt_nombre').val(nombre);
                        $('#txt_precio').val(precio);
                        $('#txt_piezas_x_caja').val(piezas_x_caja);
                        $('#txt_cajas_x_tarima').val(cajas_x_tarima);

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    }
                );
            }
        });

        CatalogosModel.catalogosLstBy(
            'mercancia',
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

    function initControls() {
        loadCliente();
        initializeEvents();
    }

    function loadCliente() {

        CatalogosModel.catalogosLst('cliente', function (data) {
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
            fillgrdCatalog();
        });
    }

    function initializeEvents() {
        ddl_change();
    }

    function ddl_change() {
        $('#ddl_cliente').on('select2:select', function (e) {
            id_cliente = $('#ddl_cliente').select2('data')[0].Id;
            CatalogosModel.catalogosLstBy(
                'mercancia',
                { key: id_cliente },
                function (data) {
                    grdCatalog.DataBind(data, function () {
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
var pag = new Mercancia();
master.Init(pag);