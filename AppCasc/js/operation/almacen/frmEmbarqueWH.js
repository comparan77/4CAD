var lstTar = [];

var lstCondTran = [];
var idEntradaTransporte = 0;
var arrCondTran = ['Paredes Limpias', 'Pisos Limpios', 'Techos Limpios', 'Presencia de grietas, huecos, astillas, agujeros', 'Presencia de plagas en la unidad', 'Presencia de olores extraños', 'Unidad exterior limpia', 'Cuenta el transporte con certificado de fumigación'];

var BeanSalidaTransporteCondicion = function (id_transporte_condicion, si_no) {
    this.Id = 0;
    this.Id_salida = 0;
    this.Id_transporte_condicion = id_transporte_condicion;
    this.Si_no = si_no;
}

var beanTarima = function (id, id_entrada, bultos, piezas, rr) {
    this.Id = id;
    this.Id_entrada = id_entrada;
    this.folio = '';
    this.Mercancia_codigo = '';
    this.Mercancia_nombre = '';
    this.Rr = rr;
    this.Estandar = '';
    this.Bultos = bultos;
    this.Piezas = piezas;
    this.Id_salida = 0;
}

var MngEmbarqueWH = function () {

    this.CtrlCM = null;

    this.Init = init;
    //    this.Recall = recall;

    function init() {
        setDestino();
        initControls();
    }

    function initControls() {
        $('#ctl00_body_txt_hora_salida').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });

        //Mascara para el RR
        $.mask.definitions['u'] = "[1-9]";
        var anioAct = new Date();
        var digAnio = anioAct.getFullYear().toString().substr(2, 2);
        $('#txt_folio_oc').mask('OCA-999999-' + digAnio);

        $('#btnBuscaOC').button().click(function () {
            if ($('#txt_folio_oc').val().length == 0) {
                alert('Es necesario proporcionar el folio de la orden de carga');
                $('#txt_folio_oc').focus();
            }
            else {
                $('#div_orde_carga, #div_generales, #div_transporte').addClass('ajaxLoading');
                fillDataByOC($('#txt_folio_oc').val());
            }
            return false;
        });

        $('#ctl00_body_btnGuardar').change(function () {
            setDestino();
        });

        //Condidiones del transporte
        fillCondicionesTransporte();

        //Boton guardar
        $('#ctl00_body_btnGuardar').button().click(function () {
            var IsValid = true;

            validaOC();

            condicionesTransporteSet();
            validaCondTransporte();

            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    $('html,body').animate({
                        scrollTop: $(this).offset().top
                    }, 2000);
                    IsValid = false;
                    return false;
                }
            });

            if (IsValid) {
                $('#ctl00_body_hf_click_save').val('1');
                $(this).hide();
            }

            return IsValid;
        });
    }

    function clearOC() {

        $('#tbodyRemByOc').html('');
        $('#ctl00_body_hf_id_orden_carga').val(0);
    }

    function fillDataByOC(folio_oc) {
        clearOC();

        $.ajax({
            type: 'GET',
            url: "/handlers/Almacen.ashx",
            //dataType: "jsonp",
            data: {
                'case': 'carga',
                opt: 'getForArribo',
                key: folio_oc
            },
            complete: function () {
                $('#div_orde_carga, #div_generales').removeClass('ajaxLoading');
            },
            success: function (data) {
                if (typeof (data) != 'string') {
                    if (data.Id_salida == null) {
                        fillFormWithOC(data);
                    }
                    else {
                        $('#div_transporte').removeClass('ajaxLoading');
                        if (confirm('La orden de carga ya cuenta con una salida, desea imprimirla?')) {
                            window.open('frmReportViewer.aspx?rpt=salidaAlm&_key=' + data.Id_salida, '_blank', 'toolbar=no');
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init()
            }
        });
    }

    function fillFormWithOC(data) {

        var tr;
        var acumTar;
        var acumCja;
        var acumPza;

        $('#ctl00_body_hf_id_orden_carga').val(data.Id);

        $.each(data.PLstTACRpt, function (i, obj) {
            tr += '<tr>';
            tr += '<td>' + obj.Folio_remision + '</td>';
            tr += '<td>' + obj.Mercancia_codigo + '</td>';
            tr += '<td>' + obj.Mercancia_nombre + '</td>';
            tr += '<td>' + obj.Tarimas + '</td>';
            tr += '<td>' + obj.Bultos + '</td>';
            tr += '<td>' + obj.Piezas + '</td>';
            tr += '</tr>';

            acumTar += obj.Tarimas;
            acumCja += obj.Bultos;
            acumPza += obj.Piezas;
        });
        $('#tbodyRemByOc').append(tr);
        //id bodega
        $('#ctl00_body_hf_id_entrada').val(data.PLstTACRpt[0].Id_entrada);
        //Destino
        $('#ctl00_body_ddlDestino').val(data.PTarAlmTrafico.PSalidaDestino.Id);

        //        $('#ctl00_body_upTipoTransporte').panelReady(function () {
        $('#ctl00_body_ddlTipo_Transporte').val(data.PTarAlmTrafico.PTransporteTipo.Id);

        $('#ctl00_body_txt_placa').val(data.PTarAlmTrafico.Placa);
        $('#ctl00_body_txt_caja').val(data.PTarAlmTrafico.Caja);
        $('#ctl00_body_txt_caja_1').val(data.PTarAlmTrafico.Caja1);
        $('#ctl00_body_txt_caja_2').val(data.PTarAlmTrafico.Caja2);
        $('#ctl00_body_txt_operador').val(data.PTarAlmTrafico.Operador);
        $('#div_transporte').removeClass('ajaxLoading');
        //Transporte
        $('#ctl00_body_ddlTransporte').val(data.PTarAlmTrafico.PTransporte.Id).trigger('change').change(function () {
            $('#div_transporte').addClass('ajaxLoading');
        });
        //        });

        $('#ctl00_body_upTipoTransporte').panelReady(function () {
            $('#div_transporte').removeClass('ajaxLoading');
        });
    }

    //    function getTarimaByRR(rr) {

    //        $.ajax({
    //            type: 'GET',
    //            url: "/handlers/Operation.ashx",
    //            //dataType: "jsonp",
    //            data: {
    //                op: 'tarimaAlm',
    //                opt: 'getByRR',
    //                key: rr
    //            },
    //            complete: function () {

    //            },
    //            success: function (data) {
    //                fill_liTarima(data, rr);
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                var oErrorMessage = new ErrorMessage();
    //                oErrorMessage.SetError(jqXHR.responseText);
    //                oErrorMessage.Init();
    //            }
    //        });
    //    }

    //    function fill_liTarima(data, rr) {
    //        $('#ul_tarima').html('');
    //        var li = '';
    //        $.each(data, function (i, obj) {
    //            li = '<li class="floatLeft ' + (i % 2 == 0 ? '' : 'liOdd') + '" style="margin-right: 20px; margin-bottom: 2px; width: 300px;"><span>Folio: ' + obj.Folio + '; Estandar: ' + obj.Estandar + '</span><input idEnt="' + obj.Id_entrada + '" codigo="' + obj.Mercancia_codigo + '" descripcion="' + obj.Mercancia_nombre + '" rr="' + obj.Rr + '" bto="' + obj.Bultos + '" pza="' + obj.Piezas + '" id="tar_' + obj.Id + '" type="checkbox" class="tarSel"/></li>';
    //            $('#ul_tarima').append(li);
    //        });

    //        clearTarimaByRR(rr);

    //        $('.tarSel').each(function () {
    //            $(this).click(function () {
    //                var cajas = 0;
    //                var piezas = 0;
    //                var lstExiste = [];
    //                $('.tarSel').each(function () {
    //                    var v_Id = $(this).attr('id').split('_')[1] * 1;
    //                    var cajas = $(this).attr('bto') * 1;
    //                    var piezas = $(this).attr('pza') * 1;
    //                    var v_IdEntrada = $(this).attr('idEnt') * 1;
    //                    if ($(this).is(':checked')) {
    //                        lstExiste = $.grep(lstTar, function (obj) {
    //                            return obj.Id == v_Id;
    //                        });
    //                        if (lstExiste.length == 0) {
    //                            var oBeanTarima = new beanTarima(v_Id, v_IdEntrada, cajas, piezas, $(this).attr('rr'));
    //                            lstTar.push(oBeanTarima);
    //                        }
    //                    }
    //                    else {
    //                        lstTar = $.grep(lstTar, function (obj) {
    //                            return obj.Id != v_Id;
    //                        });
    //                    }
    //                });
    //                fillTarimaByRR(rr);
    //                $('#ctl00_body_hf_tarimas_agregadas').val(JSON.stringify(lstTar));
    //            });
    //        });
    //    }

    //    function fillTarimaByRR(rr) {
    //        var cajas = 0;
    //        var piezas = 0;
    //        var tarimas = 0;
    //        var lstTarByRR = [];

    //        clearTarimaByRR(rr);

    //        lstTarByRR = $.grep(lstTar, function (obj) {
    //            return obj.Rr == rr;
    //        });

    //        $.each(lstTarByRR, function (i, obj) {
    //            cajas += obj.Bultos;
    //            piezas += obj.Piezas;
    //            tarimas++;
    //        });

    //        var tr = '<tr class="tarAgregada_' + rr + '">';
    //        tr += '<td align="center">' + rr + '</td>';
    //        tr += '<td align="center">' + tarimas + '</td>';
    //        tr += '<td align="center">' + cajas + '</td>';
    //        tr += '<td align="center">' + piezas + '</td>';
    //        tr += '</tr>';
    //        $('#tbody_tarAgregadas').append(tr);
    //    }

    //    function clearTarimaByRR(rr) {
    //        $('.tarAgregada_' + rr).remove();
    //    }

    function setDestino() {
        $('#ctl00_body_hf_destino').val($('#ctl00_body_ddlDestino option:selected').attr('direccion'));
    }

    function fillCondicionesTransporte() {
        $('#tbody_condiciones').html('');
        var tr;
        var td;
        var ind = 1;
        for (var itemCT in arrCondTran) {
            tr = '<tr id="condTr_' + ind + '">';
            td = '<td>';
            td += arrCondTran[itemCT];
            td += '</td>';
            tr += td;
            td = '<td><input name="name_' + ind + '" type="radio" value="1" /></td>';
            tr += td;
            td = '<td><input name="name_' + ind + '" type="radio" value="0" /></td>';
            tr += td;
            $('#tbody_condiciones').append(tr);
            ind++;
        }
    }

    //Condiciones del transporte
    function condicionesTransporteSet() {
        lstCondTran = [];
        $('#tbody_condiciones').children('tr').each(function () {
            var id = $(this).attr('id').split('_')[1] * 1;
            var val = $('input[name="name_' + id + '"]:checked', '#tbody_condiciones').val();
            if (val != undefined) {
                val = val == 1 ? true : false;
                var o = new BeanSalidaTransporteCondicion(id, val);
                lstCondTran.push(o);
            }
        });
        $('#ctl00_body_hf_condiciones_transporte').val(JSON.stringify(lstCondTran));
    }

    //validar oc
    function validaOC() {
        var idOc = $('#ctl00_body_hf_id_orden_carga').val();
        if (idOc.length > 0)
            idOc = idOc * 1;
        else
            idOc = 0;
        $('#spn_rfv_oc').css('visibility', idOc > 0 ? 'hidden' : 'visible');
    }

    //valida condiciones transporte
    function validaCondTransporte() {
        $('#rfv_condiciones_transporte').css('visibility', lstCondTran.length == arrCondTran.length ? 'hidden' : 'visible');
    }
}

var master = new webApp.Master;
var pag = new MngEmbarqueWH();
master.Init(pag);