/// <reference path="catalogosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/tabCatalog.js" />
/// <reference path="../common.js" />

var Destino = function () {

    var grdCatalog;
    var tabCatalog;

    this.Init = function () {

        fillgrdCatalog();
        tabCatalog = new TabCatalog({
            catalogo: 'destino',
            callBackSaveData: saveData,
            parametersGet: parametersGet,
            callBackEnbDis: saveData
        });
        tabCatalog.init();

        initializedEvents();
    }

    function saveData() {
        CatalogosModel.catalogosLstAll('destino', function (data) {
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
            Destino: $('#txt_destino').val(),
            Direccion: $('#txt_direccion').val()
        };
    }

    function fillgrdCatalog() {
        grdCatalog = new DataGrid({
            idtable: 'grdCatalog',
            dataKey: 'Id',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Destino);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Direccion);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.IsActive == true ? 'Si' : 'No');
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                CatalogosModel.catalogosSltById(
                    'destino',
                    { key: id },
                    function (data) {
                        var destino = data.Destino;
                        var direccion = data.Direccion;

                        $('#txt_destino').val(destino);
                        $('#txt_direccion').val(direccion);

                        tabCatalog.validateOptActive(data.IsActive);

                        tabCatalog.changeAdmonTab(id);
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    });
            }
        });

        CatalogosModel.catalogosLstAll('destino', function (data) {
            grdCatalog.DataBind(data, function (data) {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function initializedEvents() {
        btn_cp_click();
        txt_calle_num_keydown();
    }

    function txt_calle_num_keydown() {
        $('#txt_calle_num').keyup(function () {
            $('#txt_direccion').val($(this).val() + ', Del o Mun: ' + $('#txt_municipio').val() + ', ' + $('#txt_colonia').val() + '. ' + $('#txt_estado').val());
        });
    }

    function btn_cp_click() {
        var btn = $('#btn_cp');
        $('#div_colonia').addClass('hidden');
        $('#div_txt_col').addClass('hidden');
        $('#btn_cp').click(function () {
            $(this).addClass('disabled');
            $(this).html('validando codigo postal ...');
            $.ajax({
                type: 'GET',
                url: 'https://api-codigos-postales.herokuapp.com/v2/codigo_postal/' + $(txt_cp).val(),
                complete: function () {
                    webApp.Master.loading(false);
                },
                success: function (data) {
                    $(btn).removeClass('disabled');
                    $(btn).html('Validar C.P.');
                    $(txt_estado).val(data.estado);
                    $(txt_municipio).val(data.municipio);
                    if (data.colonias.length > 1) {
                        var arrCol = [];
                        for (var i = 0; i < data.colonias.length; i++) {
                            var col = {
                                datatext: data.colonias[i],
                                datavalue: data.colonias[i]
                            }
                            arrCol.push(col);
                        }
                        Common.fillSelect('ddl_colonia', arrCol);
                        $('#div_colonia').removeClass('hidden');
                        $('#div_txt_col').addClass('hidden');
                        $(txt_colonia).val(arrCol[0].datavalue);
                    } else {
                        $(txt_colonia).val(data.colonias[0]);
                        $('#div_colonia').addClass('hidden');
                        $('#div_txt_col').removeClass('hidden');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    var oErrorMessage = new ErrorMessage();
                    oErrorMessage.SetError(jqXHR.responseText);
                    oErrorMessage.Init();
                }
            });
        });
    }

}

var master = new webApp.Master;
var pag = new Destino();
master.Init(pag);