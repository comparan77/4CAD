/// <reference path="../webcontrols/monthPicker.js" />

var Reporte = function () {

    var paramReport;
    var id_cliente;
    var month_picker;

    this.Init = function () {

        month_picker = new MonthPicker({
            content: 'month_picker',
            callBackMonthClick: function (year, month) {
                setParamReport(year, month);
            },
            callBackYearClick: function (year, month) {
                setParamReport(year, month);
            }
        });
        month_picker.init();

        initializeEvents();
    }

    function initializeEvents() {
        loadCliente();
        ddl_change();
        btn_generar_click();
    }

    function setParamReport(year, month) {
        paramReport = {
            id_cliente: $('#ddl_cliente').select2('data')[0].id,
            year: year,
            month: month
        };
    }

    function loadCliente() {
        var oCatCliente = new Catalog({
            name: 'cliente'
        });

        oCatCliente.catalogLst(function (data) {
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

            setParamReport((new Date()).getFullYear(), (new Date()).getMonth() + 1);

        });
    }

    function ddl_change() {
        $('#ddl_cliente').on('select2:select', function (e) {
            if (($('#ddl_cliente').select2('data')[0].id * 1) > 0) {
                $('#div_panel_controls').removeClass('hidden');
                setParamReport(month_picker.getYear(), month_picker.getMonth());
            }
        });
    }

    function btn_generar_click() {

        $('#btn_generar').click(function () {

            $('#btn_generar').addClass('disabled');
            $('#btn_generar').html('Genrando reporte...');

            var request = $.ajax({
                url: '/handlers/Reportes.ashx?op=resmov&opt=cte',
                data: paramReport,
                method: "POST",
                dataType: "json"
            });

            request.done(function (data) {
                window.open(data, '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');
                $('#btn_generar').removeClass('disabled');
                $('#btn_generar').html('Generar Reporte');
            });

            request.fail(function (jqXHR, textStatus) {
                $('#btn_generar').removeClass('disabled');
                $('#btn_generar').html('Generar Reporte');
                alert("Request failed: " + textStatus);
            });
        });
    }
}

var master = new webApp.Master;
var pag = new Reporte();
master.Init(pag);