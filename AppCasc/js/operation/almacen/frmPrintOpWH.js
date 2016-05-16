var MngPrintOpWH = function () {

    this.Init = init;

    function init() {
        $('#btn_search').button().click(function () {
            var movimiento = $('#ddl_tipo_mov').val();
            var folio_cita = $('#txt_folio_cita').val();
            var rr = $('#txt_rr').val();
            var mercancia_codigo = $('#txt_mercancia_codigo').val();
            var folio = $('#txt_folio').val();
            clearResult();
            search(movimiento, folio_cita, rr, mercancia_codigo, folio);
            return false;
        });
    }

    function search(movimiento, folio_cita, rr, mercancia_codigo, folio) {

        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            //dataType: "jsonp",
            data: {
                'case': movimiento,
                opt: 'getBy',
                folio_cita: folio_cita,
                rr: rr,
                mercancia_codigo: mercancia_codigo,
                folio: folio
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                switch (movimiento) {
                    case 'arribo':
                        fillTbl(data, movimiento);
                        break;
                    case 'embarque':
                        fillTbl(data, movimiento);
                        break;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }

    function clearResult() {
        $('#tbody_result').html('');
    }

    function fillTbl(data, movimiento) {
        var tr;
        $.each(data, function (i, obj) {
            tr = '';
            tr += '<tr>';
            tr += '<td>' + obj.Cita + '</td>';
            tr += '<td>' + obj.Rr + '</td>';
            tr += '<td>' + obj.Folio + '</td>';
            tr += '<td>' + obj.Mercancia + '</td>';
            tr += '<td>' + obj.Nombre + '</td>';
            tr += '<td align="center"><input type="checkbox" /></td>';
            tr += '<td align="center"><span class="ui-icon ui-icon-print icon-action-button icon-button-action printMov" id="print_' + obj.Id + '"></span></td>';
            tr += '></tr>';
            $('#tbody_result').append(tr);
        });

        $('.printMov').each(function () {
            $(this).click(function () {
                var id = $(this).attr('id').split('_')[1] * 1;
                var withDetail = !$(this).parent().prev().children('input').is(':checked');
                switch (movimiento) {
                    case 'arribo':
                        window.open('frmReportViewer.aspx?rpt=entradaAlm&_key=' + id + '&_wdet=' + withDetail, '_blank', 'toolbar=no');
                        break;
                    case 'embarque':
                        window.open('frmReportViewer.aspx?rpt=salidaAlm&_key=' + id + '&_wdet=' + withDetail, '_blank', 'toolbar=no');
                        break;
                }
            });
        });
    }
}

var master = new webApp.Master;
var pag = new MngPrintOpWH();
master.Init(pag);