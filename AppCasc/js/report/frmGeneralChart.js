var GeneralChart = function () {

    this.Init = init;

    function init(div, title, sEntradas, sSalidas, ticks) {

        var plot1 = $.jqplot(div, [sEntradas, sSalidas], {
            title: title,
            // The "seriesDefaults" option is an options object that will
            // be applied to all series in the chart.
            seriesDefaults: {
                renderer: $.jqplot.BarRenderer,
                rendererOptions: { fillToZero: true }
            },
            // Custom labels for the series are specified with the "label"
            // option on the series option.  Here a series option object
            // is specified for each series.
            series: [
            { label: 'Entradas:' + sEntradas },
            { label: 'Salidas:' + sSalidas }
        ],
            // Show the legend and put it outside the grid, but inside the
            // plot container, shrinking the grid to accomodate the legend.
            // A value of "outside" would not shrink the grid and allow
            // the legend to overflow the container.
            legend: {
                show: true,
                placement: 'outsideGrid'
            },
            axes: {
                // Use a category axis on the x axis and use our custom ticks.
                xaxis: {
                    renderer: $.jqplot.CategoryAxisRenderer,
                    ticks: ticks
                },
                // Pad the y axis just a little so bars can get close to, but
                // not touch, the grid boundaries.  1.2 is the default padding.
                yaxis: {
                    pad: 1.05,
                    tickOptions: { formatString: '%d' }
                }
            }
        });

    }
}


//$(document).ready(function () {

//    try {

//        var sBultoEntrada = [$('#ctl00_body_hfsBultoEntrada').val() * 1];
//        var sBultoSalida = [$('#ctl00_body_hfsBultoSalida').val() * 1];
//        var sPiezaEntrada = [$('#ctl00_body_hfsPiezaEntrada').val() * 1];
//        var sPiezaSalida = [$('#ctl00_body_hfsPiezaSalida').val() * 1];
//        var hfMesOperacion = $('#ctl00_body_hfMesOperacion');

//        // Can specify a custom tick Array.
//        // Ticks should match up one for each y value (category) in the series.
//        var ticks = [hfMesOperacion.val()];

//        var oGC = new GeneralChart();
//        oGC.Init('divBultos', 'Bultos', sBultoEntrada, sBultoSalida, ticks);
//        oGC.Init('divPiezas', 'Piezas', sPiezaEntrada, sPiezaSalida, ticks);


//    } catch (e) {
//        alert(e);
//    }

//});


var FrmGeneralChart = function () {

    this.Init = function () {
        try {

            var sBultoEntrada = [$('#ctl00_body_hfsBultoEntrada').val() * 1];
            var sBultoSalida = [$('#ctl00_body_hfsBultoSalida').val() * 1];
            var sPiezaEntrada = [$('#ctl00_body_hfsPiezaEntrada').val() * 1];
            var sPiezaSalida = [$('#ctl00_body_hfsPiezaSalida').val() * 1];
            var hfMesOperacion = $('#ctl00_body_hfMesOperacion');

            // Can specify a custom tick Array.
            // Ticks should match up one for each y value (category) in the series.
            var ticks = [hfMesOperacion.val()];

            var oGC = new GeneralChart();
            oGC.Init('divBultos', 'Bultos', sBultoEntrada, sBultoSalida, ticks);
            oGC.Init('divPiezas', 'Piezas', sPiezaEntrada, sPiezaSalida, ticks);


        } catch (e) {
            alert(e);
        }   
    }
}

var master = new webApp.Master;
var pag = new FrmGeneralChart();
master.Init(pag);