var TransporteChart = function () {

    this.Init = init;

    function init(div, title, sCantidad, ticks, max_Yvalue) {

        var plot1 = $.jqplot(div, [sCantidad], {
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
            { label: 'Total' }
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
                    labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                    tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                    ticks: ticks,
                    tickOptions: {
                        angle: 75
                    }
                },
                // Pad the y axis just a little so bars can get close to, but
                // not touch, the grid boundaries.  1.2 is the default padding.
                yaxis: {
                    //pad: 1.05,
                    max: max_Yvalue,
                    min: 0,
                    tickOptions: { formatString: '%d' }
                }
            }
        });

//        $('.jqplot-xaxis-tick').each(function (i) {
//            $(this).addClass('rot90');
//            $(this).html(sTransporte[i] + ': ' + sCantidad[i]);
//        });

    }
}


//$(document).ready(function () {

//    try {

//        var hfsTransporte = $('#ctl00_body_hfsTransporte');
//        var hfsCantidad = $('#ctl00_body_hfsCantidad');
//        var sCantidad = [];

//        var arrCantidad = hfsCantidad.val().split(',');
//        var max_Yvalue;
//        max_Yvalue = 0;
//        for (var Cant in arrCantidad) {
//            sCantidad.push(arrCantidad[Cant]);
//            if (arrCantidad[Cant] > max_Yvalue) {
//                max_Yvalue = arrCantidad[Cant] * 1;
//            }
//        }

//        // Can specify a custom tick Array.
//        // Ticks should match up one for each y value (category) in the series.
//        //var ticks = [hfsTransporte.val()];
//        var ticks = [];
//        //var sTransporte = [];
//        var arrTransporte = hfsTransporte.val().split('|');
//        for (var Trans in arrTransporte) {
//            //sTransporte.push(arrTransporte[Trans]);
//            ticks.push(arrTransporte[Trans] + ': ' + arrCantidad[Trans] + '  ');
//        }

//        var oTC = new TransporteChart();
//        oTC.Init('divTransporte', 'Movimientos', sCantidad, ticks, max_Yvalue);

//    } catch (e) {
//        alert(e);
//    }

//});


var FrmTransporteChart = function () {

    this.Init = function () {
        try {

            var hfsTransporte = $('#ctl00_body_hfsTransporte');
            var hfsCantidad = $('#ctl00_body_hfsCantidad');
            var sCantidad = [];

            var arrCantidad = hfsCantidad.val().split(',');
            var max_Yvalue;
            max_Yvalue = 0;
            for (var Cant in arrCantidad) {
                sCantidad.push(arrCantidad[Cant]);
                if (arrCantidad[Cant] > max_Yvalue) {
                    max_Yvalue = arrCantidad[Cant] * 1;
                }
            }

            // Can specify a custom tick Array.
            // Ticks should match up one for each y value (category) in the series.
            //var ticks = [hfsTransporte.val()];
            var ticks = [];
            //var sTransporte = [];
            var arrTransporte = hfsTransporte.val().split('|');
            for (var Trans in arrTransporte) {
                //sTransporte.push(arrTransporte[Trans]);
                ticks.push(arrTransporte[Trans] + ': ' + arrCantidad[Trans] + '  ');
            }

            var oTC = new TransporteChart();
            oTC.Init('divTransporte', 'Movimientos', sCantidad, ticks, max_Yvalue);

        } catch (e) {
            alert(e);
        }
    }
}

var master = new webApp.Master;
var pag = new FrmTransporteChart();
master.Init(pag);