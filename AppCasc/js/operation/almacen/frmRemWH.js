var lstTARDet = [];
var idTARDet = 1;

var BeanTarimaAlmacenRemisionDet = function (id, id_entrada, mercancia_codigo, rr, estandar, tarimas, cajas, piezas) {
    this.Id = id;
    this.Id_tarima_almacen_remision = 0;
    this.Id_entrada = id_entrada;
    this.Mercancia_codigo = mercancia_codigo;
    this.Rr = rr;
    this.Estandar = estandar;
    this.Tarimas = tarimas;
    this.Cajas = cajas;
    this.Piezas = piezas;
}

var MngfrmRemWH = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        var hf_codigo = $('#ctl00_body_hf_codigo');

        if (hf_codigo.val().length > 0) {
            $('#div-control').removeClass('hidden');
        }

        var grd_mercancia_disp = $('#ctl00_body_grd_mercancia_disp');
        if ($(hf_codigo).val().length > 0) {
            $(grd_mercancia_disp).append('<tr><td colspan="7" align="center">Totales</td><td align="center" id="td_tot_tarima"></td><td align="center" id="td_tot_caja"></td><td align="center" id="td_tot_pieza"></td></tr>');
        }

        calculaTotales();

        $('.addTarima').each(function () {
            $(this).click(function () {
                var cantidad = prompt('Proporcione la cantidad de Tarimas', '');
                if (cantidaValida($(this).parent().parent(), cantidad)) {
                    addTarima($(this).parent().parent(), cantidad);
                }
            });
        });

        $('.remTarima').each(function () {
            $(this).click(function () {
                remTarima($(this).parent().parent());
            });
        });

        //Citas
        $('#div_citas').dialog({
            autoOpen: false,
            resizable: false,
            //height: 250,
            width: 450,
            modal: true
        });

        $('#spn_folio_cita').click(function () {
            var numCajas = $('#td_tot_caja').html() * 1;
            if (numCajas > 0) {
                fillCitas();
            }
            else {
                alert('Es necesario remisionar por lo menos una tarima');
            }
        });

        //Guardar remision
        var btn_save = $('#ctl00_body_btn_save').button().click(function () {
            $('#ctl00_body_hf_tarima_remision').val(JSON.stringify(lstTARDet));
        });

        //Detalle remisiones elaboradas
        $('#div_rem_info').dialog({
            autoOpen: false,
            resizable: false,
            height: 320,
            width: 330,
            modal: true
        });

        $('#ctl00_body_up_resultados').panelReady(function () {
            $('.rem').each(function () {
                $(this).click(function () {
                    getRemById($(this).attr('id').split('_')[1]);
                });
            });
        });

        $('#print_rem').button();
    }

    function fillCitas() {

        $('#ul_citas').html('');
        $('#spn_cita_sel').html('')
        $('#ctl00_body_txt_folio_cita').val('');

        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            data: {
                'case': 'trafico',
                opt: 'getAvailableForRem'
            },
            complete: function () {
                $('#div-info-codigo').removeClass('ajaxLoading');
            },
            success: function (data) {

                if (data.length == 0) {
                    $('#ul_citas').append('<li>Sin citas <br /><a href="/operation/Almacen/frmTraficoWH.aspx">Solicitar citas aqu&iacute;</a></li>');
                }
                else {
                    $(data).each(function (i, obj) {
                        var liCita = '<li>';
                        liCita += '<font color="#0d5fb3">Folio:&nbsp;</font>' + obj.Folio_cita + '<font color="#0d5fb3">&nbsp;Fecha:&nbsp;</font>' + moment(obj.Fecha_cita).format("DD/MM/YYYY");
                        liCita += '&nbsp;<font color="#0d5fb3">Hora:</font>&nbsp;' + obj.Hora_cita;
                        liCita += '<div style="position: relative">';
                        liCita += '<ul>';
                        liCita += '<li><b>Destino:</b> ' + obj.PSalidaDestino.Destino + '</li>';
                        liCita += '<li><b>Línea:</b> ' + obj.Transporte + '</li>';
//                        liCita += '<li><b>Operador:</b> ' + obj.Operador + '</li>';
                        liCita += '<li><b>Tipo:</b> ' + obj.Transporte_tipo_cita + '</li>';
//                        liCita += '<li><b>Placa:</b> ' + obj.Placa + '</li>';
                        liCita += '<li><b>Fecha Carga Solicitada:</b> ' + moment(obj.Fecha_carga_solicitada).format('DD/MM/YYYY') + '</li>';
                        liCita += '<li><b>Hora Carga Solicitada:</b> ' + obj.Hora_carga_solicitada + '</li>';
                        liCita += '</ul>'
                        liCita += '<button class="btn_sel_cita" id="btn_cita_' + obj.Id + '" style="position: absolute; top: 20px; right: 20px;">Seleccionar</button>';
                        liCita += '<span id="spn_pallet_' + obj.Id + '" style="position: absolute; top: 60px; right: 80px; font-size: 2.5em; color:' + (!obj.Pallet ? 'black' : obj.Pallet < 21 ? 'black' : obj.Pallet == 22 ? 'yellow' : 'red') + '">' + (!obj.Pallet ? '0' : obj.Pallet) + '</span>';
                        liCita += '<span style="position: absolute; top: 100px; right: 60px;">Pallet(s)</span>';
                        liCita += '<input type="hidden" id="hf_folio_cita_' + obj.Id + '" value="' + obj.Folio_cita + '" />';
                        liCita += '</div></li>';
                        liCita += '<hr />'

                        $('#ul_citas').append(liCita);
                    });

                    $('.btn_sel_cita').each(function () {
                        $(this).button().click(function () {
                            var id = $(this).attr('id').split('_')[2];
                            //                            if (NuevaRemision == 1) {
                            $('#ctl00_body_txt_folio_cita').val(id);
                            $('#spn_cita_sel').html($('#hf_folio_cita_' + id).val());
                            $('#div_citas').dialog('close');
                            //                            }
                            //                            else {
                            //                                if (confirm('Se va a modificar la cita de la remisión, desea continuar con la modificación?')) {
                            //                                    udtCita($('#hf_folio_cita_' + id).val(), $('#spn-dlt').html());
                            //                                }
                            //                            }
                        });
                    });

                }

                $('#div_citas').dialog('open');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }

    function cantidaValida(row, cantidad) {
        var oCommon = new Common();
        var cantidadMaxima = oCommon.GetOnlyDecimal($(row).children('td:nth-child(4)').html()) * 1;
        if (isNaN(cantidad)) {
            alert('La cantidad debe ser numérica');
            return false;
        }

        if (cantidad <= 0) {
            alert('La cantidad debe ser mayor a 0');
            return false;
        }

        if (cantidad > cantidadMaxima) {
            alert('La cantidad debe ser menor a ' + cantidadMaxima);
            return false;
        }
        return true;
    }

    function addTarima(row, cantidadTarimas) {
        var cajasXTarima = 0;
        var pizasXCaja = 0;
        var oCommon = new Common();
        var piezas = oCommon.GetOnlyDecimal($(row).children('td:nth-child(6)').html()) * 1;
        var cajas = oCommon.GetOnlyDecimal($(row).children('td:nth-child(5)').html()) * 1;
        var tarimas = oCommon.GetOnlyDecimal($(row).children('td:nth-child(4)').html()) * 1;

        pizasXCaja = piezas / cajas;
        cajasXTarima = cajas / tarimas;

        $(row).children('td:nth-child(8)').html(cantidadTarimas);
        $(row).children('td:nth-child(9)').html(cantidadTarimas * cajasXTarima);
        $(row).children('td:nth-child(10)').html(cantidadTarimas * cajasXTarima * pizasXCaja);

        $(row).children('td:nth-child(11)').children('span').removeClass('hidden').attr('id', 'tar_' + idTARDet);
        $(row).children('td:nth-child(7)').children('span').addClass('hidden');

        $(row).css('color', '#0d5fb3');

        var id_entrada = $(row).children('td:nth-child(12)').html();

        var oBTARDet = new BeanTarimaAlmacenRemisionDet(
            idTARDet,
            id_entrada,
            $('#ctl00_body_hf_codigo').val(),
            $(row).children('td:nth-child(1)').html(),
            $(row).children('td:nth-child(3)').html(),
            cantidadTarimas,
            cantidadTarimas * cajasXTarima,
            cantidadTarimas * cajasXTarima * pizasXCaja);

        lstTARDet.push(oBTARDet);
        idTARDet++;
        calculaTotales();
    }

    function remTarima(row) {

        $(row).children('td:nth-child(8)').html(0);
        $(row).children('td:nth-child(9)').html(0);
        $(row).children('td:nth-child(10)').html(0);

        calculaTotales();

        $(row).children('td:nth-child(7)').children('span').removeClass('hidden');
        $(row).children('td:nth-child(11)').children('span').addClass('hidden');

        var id = $(row).children('td:nth-child(11)').children('span').attr('id').split('_')[1] * 1;
        lstTARDet = $.grep(lstTARDet, function (obj) {
            return obj.Id != id;
        });

        $(row).css('color', '#000');
    }

    function calculaTotales() {
        var totTarima = 0;
        var totCaja = 0;
        var totPieza = 0;

        $('.totTarima').each(function () {
            totTarima += $(this).html() * 1;
        });

        $('.totCaja').each(function () {
            totCaja += $(this).html() * 1;
        });

        $('.totPieza').each(function () {
            totPieza += $(this).html() * 1;
        });

        $('#td_tot_tarima').html(totTarima);
        $('#td_tot_caja').html(totCaja);
        $('#td_tot_pieza').html(totPieza);
    }

    function getRemById(id_remision) {

        $('#tbl_rem_detail').html('');
        $('#txt_folio_cita_read').val('');
        $('#txt_tot_tar_read').val('');
        $('#txt_operador_read').val('');
        $('#txt_transporte_read').val('');
        var totTar = 0;

        $.ajax({
            type: 'GET',
            url: '/handlers/Almacen.ashx',
            data: {
                'case': 'remision',
                opt: 'getById',
                id: id_remision
            },
            complete: function () {
                //                $('#div-info-codigo').removeClass('ajaxLoading');
            },
            success: function (data) {
                $('#ui-id-4').html('Folio: ' + data.Folio + ', Fecha: ' + moment(data.Fecha).format("DD/MM/YYYY"));
                $('#div_rem_info').dialog('open');

                $.each(data.PLstTARDet, function (i, obj) {
                    var trDet = '<tr>';
                    trDet += '<td align="center">' + obj.Rr + '</td>';
                    trDet += '<td align="center">' + obj.Estandar + '</td>';
                    trDet += '<td align="center">' + obj.Tarimas + '</td>';
                    trDet += '<td align="center">' + obj.Cajas + '</td>';
                    trDet += '<td align="center">' + obj.Piezas + '</td>';
                    trDet += '</tr>';
                    $('#tbl_rem_detail').append(trDet);
                    totTar += obj.Tarimas;
                });

                $('#txt_tot_tar_read').val(totTar);
                $('#txt_folio_cita_read').val(data.PTarAlmTrafico.Folio_cita);
                $('#txt_operador_read').val(data.PTarAlmTrafico.Operador);
                $('#txt_transporte_read').val(data.PTarAlmTrafico.PTransporteTipo.Nombre);

                $('#print_rem').unbind('click').click(function () {
                    window.open('frmReportViewer.aspx?rpt=remision&_key=' + data.Id, '_blank', 'toolbar=no');
                    return false;
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }
}

var master = new webApp.Master;
var pag = new MngfrmRemWH();
master.Init(pag);