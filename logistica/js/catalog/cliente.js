/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Cliente = function () {

    var grdCatalog;
    var tabCatalog;

    this.Init = function () {

        fillgrdCatalog();
        tabCatalog = new TabCatalog({
            catalogo: 'cliente',
            callBackSaveData: saveData,
            parametersGet: parametersGet,
            callBackEnbDis: saveData
        });
        tabCatalog.init();
    }

    function saveData() {
        CatalogosModel.catalogosLst('cliente', function (data) {
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
            rfc: $('#txt_rfc').val(),
            razon: $('#txt_razon').val()
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
                field = document.createTextNode(obj.Rfc);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Razon);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.IsActive == true ? 'Si' : 'No');
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                CatalogosModel.catalogosSltById(
                    'cliente',
                    { key: id },
                    function (data) {
                        var nombre = data.Nombre;
                        var rfc = data.Rfc;
                        var razon = data.Razon;

                        $('#txt_nombre').val(nombre);
                        $('#txt_rfc').val(rfc);
                        $('#txt_razon').val(razon);

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        CatalogosModel.catalogosLst('cliente', function (data) {
            grdCatalog.DataBind(data, function (data) {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }
}

var master = new webApp.Master;
var pag = new Cliente();
master.Init(pag);