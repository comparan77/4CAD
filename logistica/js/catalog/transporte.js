﻿/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Transporte = function () {

    var grdCatalog;
    var tabCatalog;

    this.Init = function () {

        fillgrdCatalog();
        tabCatalog = new TabCatalog({
            catalogo: 'transporte',
            callBackSaveData: saveData,
            parametersGet: parametersGet,
            callBackEnbDis: saveData
        });
        tabCatalog.init();
    }

    function saveData() {
        CatalogosModel.catalogosLstAll('transporte', function (data) {
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
                    'transporte',
                    { key: id },
                    function (data) {
                        var nombre = data.Nombre;

                        $('#txt_nombre').val(nombre);

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        CatalogosModel.catalogosLstAll('transporte', function (data) {
            grdCatalog.DataBind(data, function (data) {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

}

var master = new webApp.Master;
var pag = new Transporte();
master.Init(pag);