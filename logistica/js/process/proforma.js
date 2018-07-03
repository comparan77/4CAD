/// <reference path="procesosModel.js" />
/// <reference path="../webcontrols/datagrid.js" />
/// <reference path="../webcontrols/monthPicker.js" />
/// <reference path="../common.js" />

var Proforma = function () {

    var id_cliente;
    var corte;
    var tblProfConcentrado;
    var tblProfConcentradoApp;
    var tblProfConcentradoAppFolio;

    this.Init = function () {

        fillProformaConcentradoXAplicar();
        fillProformaConcentradoApp();
        fillProfConcentradoAppFolio();
        initializedEvents();
    }

    function fillProfConcentradoAppFolio() {
        tblProfConcentradoAppFolio = new DataGrid({
            idtable: 'tblProfConcentradoAppFolio',
            dataKey: 'Nombre_servicio',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(moment(obj.Fecha_serv_min).format('DD-MM-YYYY'));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(moment(obj.Fecha_serv_max).format('DD-MM-YYYY'));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(Common.getCurrencyFormat(obj.Total, 2));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                lnkFolio = document.createElement('a');
                lnkFolio.setAttribute('target', '_blank');
                lnkFolio.setAttribute('href', '../../rpt/resmov/' + obj.Nombre_servicio + '.xlsm');
                lnkFolio.innerHTML = obj.Nombre_servicio;
                td.appendChild(lnkFolio);
                tr.appendChild(td);
            }
        });
    }

    function fillProformaConcentradoApp() {
        tblProfConcentradoApp = new DataGrid({
            idtable: 'tblProfConcentradoApp',
            dataKey: 'Id_cliente',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Cliente);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(Common.getCurrencyFormat(obj.Total, 2));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(obj.Cantidad);
                td.appendChild(field);
                tr.appendChild(td);
            },
            callBackRowClick: function (tbl, id) {
                $('#li_por_aplicarApp').removeClass('active');
                $('#li_por_aplicarApp').addClass('text-primary');
                $('#li_clienteApp').removeClass('hidden');
                $('#li_clienteApp').addClass('active');
                $('#li_clienteApp').html($(tbl).children(':nth-child(1)').html());
                $('#div_profConcentradoApp').addClass('hidden');
                $('#div_profByClienteApp').removeClass('hidden');
                id_cliente = id;

                ProcesosModel.proformaconcentrado_getAllClienteApp(
                    { key: id_cliente },
                    function (data) {
                        tblProfConcentradoAppFolio.DataBind(data, function () {
                        });
                    },
                    function (jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);
                    }
                );
            }
        });

        ProcesosModel.proformaConcentradoGetAplicada(
            function (data) {
                tblProfConcentradoApp.DataBind(data, function () {
                });
            },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });

    }

    function fillProformaConcentradoXAplicar() {
        tblProfConcentrado = new DataGrid({
            idtable: 'tblProfConcentrado',
            dataKey: 'Id_cliente',
            callBackRowFill: function (tr, obj) {

                td = document.createElement('td');
                field = document.createTextNode(obj.Cliente);
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(Common.getCurrencyFormat(obj.Total, 2));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(moment(obj.Fecha_serv_min).format('DD-MM-YYYY'));
                td.appendChild(field);
                tr.appendChild(td);

                td = document.createElement('td');
                field = document.createTextNode(moment(obj.Fecha_serv_max).format('DD-MM-YYYY'));
                td.appendChild(field);
                tr.appendChild(td);

            },
            callBackRowClick: function (tbl, id) {
                $('#li_por_aplicar').removeClass('active');
                $('#li_por_aplicar').addClass('text-primary');
                $('#li_cliente').removeClass('hidden');
                $('#li_cliente').addClass('active');
                $('#li_cliente').html($(tbl).children(':nth-child(1)').html());
                $('#div_profConcentrado').addClass('hidden');
                $('#div_profByCliente').removeClass('hidden');
                id_cliente = id;
                $('#txt_cliente').val($(tbl).children(':nth-child(1)').html());
                $('#txt_fecha_ini').val($(tbl).children(':nth-child(3)').html());

                $('#txt_fecha_corte').datepicker('destroy');

                $('#txt_fecha_corte').datepicker({
                    language: 'es',
                    endDate: $(tbl).children(':nth-child(4)').html(),
                    startDate: $(tbl).children(':nth-child(3)').html(),
                    autoclose: true
                }).on('changeDate', function (e) {
                    $('#txt_monto_corte').val('');
                    $('#txt_monto_corte').attr('placeholder', 'calculando ...');
                    $('#btn_aplicar').addClass('disabled');
                    corte = moment(e.date).format('YYYY-MM-DD');
                    ProcesosModel.proformaconcentrado_getAllCliente(
                        {
                            key: id_cliente,
                            corte: corte
                        },
                        function (data) {
                            $('#txt_monto_corte').val(Common.getCurrencyFormat(data[0].Total, 2));
                            $('#txt_monto_corte').attr('placeholder', 'Monto al corte');
                            $('#btn_aplicar').removeClass('disabled');
                        },
                        function (jqXHR, textStatus) {
                            alert("Request failed: " + textStatus);
                        }
                    );
                });
            }
        });

        ProcesosModel.proformaConcentradoGet(function (data) {
            tblProfConcentrado.DataBind(data, function () {
            });
        },
        function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }

    function initializedEvents() {
        btn_procesar_click();
        li_por_aplicar_click();
        btn_aplicar_click();
    }

    function li_por_aplicar_click() {
        $('#li_por_aplicar').click(function () {
            if ($(this).hasClass('text-primary')) {
                $(this).removeClass('text-primary');
                $(this).addClass('active');
                $('#li_cliente').addClass('hidden');
                $('#li_cliente').removeClass('active');
                $('#div_profConcentrado').removeClass('hidden');
                $('#div_profByCliente').addClass('hidden');
            }
        });

        $('#li_por_aplicarApp').click(function () {
            if ($(this).hasClass('text-primary')) {
                $(this).removeClass('text-primary');
                $(this).addClass('active');
                $('#li_clienteApp').addClass('hidden');
                $('#li_clienteApp').removeClass('active');
                $('#div_profConcentradoApp').removeClass('hidden');
                $('#div_profByClienteApp').addClass('hidden');
            }
        });
    }

    function btn_procesar_click() {
        $('#btn_procesar').click(function () {
            $(this).addClass('disabled');
            $(this).html('Procesando proformas ...');
            var btn = this;
            ProcesosModel.proformaProcesar(
            function (data) {
                $(btn).removeClass('disabled');
                $(btn).html('Procesar proformas');
                console.log(data);
            },
            function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
        });
    }

    function btn_aplicar_click() {
        var btn = $('#btn_aplicar');
        $(btn).click(function () {
            $(this).addClass('disabled');
            $(this).html('Aplicando corte ...');
            ProcesosModel.proformaConcentradoUdtActiva(
                {
                    key: id_cliente,
                    corte_ini: moment($('#txt_fecha_ini').val(), "DD-MM-YYYY").format('YYYY-MM-DD'),
                    corte_fin: corte
                },
                function (data) {
                    folio_aplicado = data;
                    $(btn).html('Aplicar');

                    $('#txt_fecha_corte').val('');
                    $('#txt_monto_corte').val('');

                    alert('Se realizó el corte correctamente');

                    ProcesosModel.proformaResProfXlsm(
                        { folio: folio_aplicado },
                        function (data) {
                            console.log(data);

                            ProcesosModel.proformaConcentradoGet(
                                function (data) {
                                    tblProfConcentrado.DataBind(data, function () {

                                        ProcesosModel.proformaConcentradoGetAplicada(
                                            function (data) {
                                                tblProfConcentradoApp.DataBind(data, function () {

                                                    $('#li_por_aplicar').addClass('active');
                                                    $('#li_por_aplicar').removeClass('text-primary');
                                                    $('#li_cliente').addClass('hidden');
                                                    $('#li_cliente').removeClass('active');
                                                    $('#li_cliente').html('');
                                                    $('#div_profConcentrado').removeClass('hidden');
                                                    $('#div_profByCliente').addClass('hidden');

                                                });
                                            },
                                            function (jqXHR, textStatus) {
                                                alert("Request failed: " + textStatus);
                                            }
                                        );
                                    });
                                },
                                function (jqXHR, textStatus) {
                                    alert("Request failed: " + textStatus);
                                }
                            );

                        },
                        function (jqXHR, textStatus) {
                            alert("Request failed: " + textStatus);
                            console.log("ProfXlsm Request failed: " + textStatus);
                        }
                    );

                },
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        });
    }
}

var master = new webApp.Master;
var pag = new Proforma();
master.Init(pag);