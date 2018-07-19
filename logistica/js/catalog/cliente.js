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

    function fillDdlRegimen() {
        CatalogosModel.catalogosLst('cliente_regimen',
            function (data) {
                var dataMap = $.map(data, function (obj) {
                    obj.id = obj.Id; // replace pk with your identifier
                    obj.text = obj.Nombre;
                    return obj;
                });

                $('#ddl_regimen').select2({
                    placeholder: "Selecciona una opción",
                    data: dataMap,
                    theme: "classic"
                });

            },
            function (err, text) {
            }
        );
    }

    function saveData() {
        CatalogosModel.catalogosLstAll('cliente', function (data) {
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

        var lstReg = $('#ddl_regimen').val();
        var arrRegSel = [];
        for (var r in lstReg) {
            console.log(lstReg[r]);
            arrRegSel.push({ Id: lstReg[r], Nombre: '' });
        }
        console.log(JSON.stringify(arrRegSel));

        return {
            Nombre: $('#txt_nombre').val(),
            Rfc: $('#txt_rfc').val(),
            Razon: $('#txt_razon').val(),
            Numero: $('#txt_numero').val(),
            PLstCteReg: arrRegSel
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
                        var numero = data.Numero;
                        var LstRegimen = data.PLstCteReg;
                        $('#txt_nombre').val(nombre);
                        $('#txt_rfc').val(rfc);
                        $('#txt_razon').val(razon);
                        $('#txt_numero').val(numero);

                        var dataMap = $.map(LstRegimen, function (obj) {
                            return obj.Id;
                        });

                        $('#ddl_regimen').val(dataMap).trigger('change');

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        CatalogosModel.catalogosLstAll('cliente', function (data) {
            grdCatalog.DataBind(data, function (data) {
                fillDdlRegimen();
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