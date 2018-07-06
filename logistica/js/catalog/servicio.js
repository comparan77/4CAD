/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Servicio = function () {

    var grdCatalog;
    var tabCatalog;

    this.Init = function () {

        fillgrdCatalog();
        loadPeriodo();
        tabCatalog = new TabCatalog({
            catalogo: 'servicio',
            callBackSaveData: saveData,
            parametersGet: parametersGet
        });
        tabCatalog.init();
    }

    function saveData() {
        CatalogosModel.catalogosLstAll(
            'servicio', 
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
            Nombre: $('#txt_nombre').val(),
            Descripcion: $('#txt_descripcion').val(),
            Id_periodo: $('#ddl_periodo').val(),
            Periodo_valor: $('#txt_dias').val(),
            Campo_cantidad: $('#txt_formula_cantidad').val(),
            Campo_importe: $('#txt_formula_importe').val()
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
                field = document.createTextNode(obj.Descripcion);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.PServPer.Nombre);
                td.setAttribute('id', 'id_per_' + obj.PServPer.Id);
                td.setAttribute('dias', obj.Periodo_valor);
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                CatalogosModel.catalogosSltById(
                    'servicio',
                    { key: id },
                    function (data) {
                        var nombre = data.Nombre;
                        var descripcion = data.Descripcion;
                        var id_periodo = data.Id_periodo;
                        var periodo_valor = data.Periodo_valor;
                        var formula_cantidad = data.Campo_cantidad;
                        var formula_importe = data.Campo_importe;

                        $('#txt_nombre').val(nombre);
                        $('#txt_descripcion').val(descripcion);
                        $('#ddl_periodo').val(id_periodo);
                        $('#ddl_periodo').trigger('change');
                        validatePeriodo(id_periodo);
                        $('#txt_dias').val(periodo_valor);
                        $('#txt_formula_cantidad').val(formula_cantidad);
                        $('#txt_formula_importe').val(formula_importe);
                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        CatalogosModel.catalogosLstAll('servicio', function (data) {
            grdCatalog.DataBind(data, function (data) {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function initControls() {

        initializeEvents();
    }

    function loadPeriodo() {

        CatalogosModel.catalogosLstAll('servicio_periodo', function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                return obj;
            });

            $('#ddl_periodo').select2({
                
                placeholder: "Selecciona un periodo",
                data: dataMap
            });

            initializeEvents();
        },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            }
        );
    }

    function initializeEvents() {
        ddl_periodo_change();
    }

    function ddl_periodo_change() {
        $('#ddl_periodo').on('select2:select', function (e) {
            var id = $('#ddl_periodo').select2('data')[0].Id;
            validatePeriodo(id);
        });
    }

    function validatePeriodo(id_periodo) {
        switch (id_periodo) {
            case 7:
                $('#div_dias').removeClass('hidden');
                break;
            default:
                $('#div_dias').addClass('hidden');
        }
    }

}

var master = new webApp.Master;
var pag = new Servicio();
master.Init(pag);