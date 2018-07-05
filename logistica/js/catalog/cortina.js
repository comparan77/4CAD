/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Cortina = function () {

    var grdCatalog;
    var tabCatalog;
    var id_bodega;

    this.Init = function () {

        tabCatalog = new TabCatalog({
            catalogo: 'cortina',
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
                $('#ddl_almacen').val(id_bodega); // Select the option with a value of '1'
                $('#ddl_almacen').trigger('change');
                break;
            default:
        }
    }

    function saveData() {
        CatalogosModel.catalogosLstAllBy(
                'cortina',
                { key: id_bodega },
                function (data) {
                    grdCatalog.DataBind(data, function () {
                        tabCatalog.changeListTab();
                       
                    });
                    //console.log(id_bodega);
                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
    }

    function parametersGet() {
        return {
            Id_bodega: id_bodega,
            Nombre: $('#txt_nombre').val()
        };
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
                field = document.createTextNode(obj.IsActive == true ? 'Si' : 'No');
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                CatalogosModel.catalogosSltById(
                    'cortina',
                    { key: id },
                    function (data) {
                        var nombre = data.Nombre;

                        $('#txt_nombre').val(nombre);

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    }
                );
            }
        });

        CatalogosModel.catalogosLstAllBy(
            'cortina',
            { key: id_bodega },
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
        loadAlmacen();
        initializeEvents();
    }

    function loadAlmacen() {

        CatalogosModel.catalogosLstAll('bodega', function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                return obj;
            });

            $('#ddl_almacen').select2({
                tags: "true",
                placeholder: "Selecciona un almacen",
                data: dataMap
            });

            id_bodega = $('#ddl_almacen').select2('data')[0].Id;
            fillgrdCatalog();
        });
    }

    function initializeEvents() {
        ddl_change();
    }

    function ddl_change() {
        $('#ddl_almacen').on('select2:select', function (e) {
            id_bodega = $('#ddl_almacen').select2('data')[0].Id;
            CatalogosModel.catalogosLstAllBy(
                'cortina',
                { key: id_bodega },
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
var pag = new Cortina();
master.Init(pag);