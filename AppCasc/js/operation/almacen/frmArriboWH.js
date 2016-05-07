var lstEntTran = [];
var lstCondTran = [];
var lstTarAlmResto = [];
var lstTarAlm = [];

var idEntradaTransporte = 0;
var arrCondTran = ['Paredes Limpias', 'Pisos Limpios', 'Techos Limpios', 'Presencia de grietas, huecos, astillas, agujeros', 'Presencia de plagas en la unidad', 'Presencia de olores extraños', 'Unidad exterior limpia', 'Cuenta el transporte con certificado de fumigación'];

var idTarimaAlmacen = 1;
var idTarimaAlmacenResto = 1;

var BeanTarimaAlmacen = function (id, id_entrada, folio, mercancia_codigo, mercancia_nombre, rr, estandar, bultos, piezas, es_resto, id_salida, lstTAResto) {
    this.Id = id;
    this.Id_entrada = id_entrada;
    this.Folio = folio;
    this.Mercancia_codigo = mercancia_codigo;
    this.Mercancia_nombre = mercancia_nombre;
    this.Rr = rr;
    this.Estandar = estandar;
    this.Bultos = bultos;
    this.Piezas = piezas;
    this.Es_resto = es_resto;
    this.Id_salida = id_salida;
    this.PLTAResto = lstTAResto;
}

var BeanTarimaAlmacenResto = function (id, id_tarima_almacen, cajas, piezasxcaja, piezas) {
    this.Id = id;
    this.Id_tarima_almacen = id_tarima_almacen;
    this.Cajas = cajas;
    this.Piezasxcaja = piezasxcaja;
    this.Piezas = piezas;
}

var BeanEntradaTransporte = function (id, transporteLinea, idTransporteTipo, transporteTipo, placa, caja, caja1, caja2) {
    this.Id = id;
    this.Id_entrada = 0;
    this.Transporte_linea = transporteLinea;
    this.Id_transporte_tipo = idTransporteTipo;
    this.Placa = placa;
    this.Caja = caja;
    this.Caja1 = caja1;
    this.Caja2 = caja2;
    this.Transporte_tipo = transporteTipo;
}

var BeanEntradaTransporteCondicion = function (id_transporte_condicion, si_no) {
    this.Id = 0;
    this.Id_entrada_transporte = 0;
    this.Id_transporte_condicion = id_transporte_condicion;
    this.Si_no = si_no;
}

var MngArriboWH = function () {

    this.CtrlCM = null;

    this.Init = init;
    this.Recall = recall;

    function init() {
        initControls();
    }

    function initControls() {
        //Horas de arribo y descarga
        $('#ctl00_body_txt_hora_llegada, #ctl00_body_txt_hora_descarga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });

        //Carga catálogo de proveedores
        fillCatalogo();

        //Mascara para el RR
        $.mask.definitions['a'] = "[A-Z]";
        $('#ctl00_body_txt_rr').mask('a99999');

        //Captura de nuevos codigos de mercancia
        oCrtlCM = new ctrlClienteMercancia();

        $('#ctl00_body_txt_mercancia_codigo').blur(function () {

            $('#ctl00_body_txt_mercancia_desc').val('');

            var codigo = $(this).val();
            if (codigo.length < 1) {
                alert('Es necesario proporcionar el código de la mercancía');
                $(this).focus();
            }
            else {
                oCrtlCM.FindByCode(pag, $(this).val(), 1);
            }
        });

        var bto_declarado = $('#ctl00_body_txt_no_bulto_declarado');
        var bto_recibido = $('#ctl00_body_txt_no_bulto_recibido');
        var pza_x_bulto = $('#ctl00_body_txt_pza_x_bulto');
        var bto_x_pallet = $('#ctl00_body_txt_bto_x_pallet');
        var piezaDeclarada = $('#ctl00_body_txt_no_pieza_declarada');

        $('.calculaDif').each(function () {
            $(this).blur(function () {
                calculaDif(bto_declarado, bto_recibido, $(pza_x_bulto).val());
                calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val(), $(piezaDeclarada).val());
            });
        });

        $('.calculaStd').each(function () {
            $(this).blur(function () {
                calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val(), $(piezaDeclarada).val());
            });
        });

        $('#btn_show_cantidadesProblema').button().click(function () {
            $(this).hide();
            $('#cantidadesProblema').show('slow');
            return false;
        });

        validaTipoTransporte();
        $('#ctl00_body_ddlTipo_Transporte').change(function () {
            validaTipoTransporte();
        }).trigger('change');

        $('#btn_add_tipoTransporte').click(function () {
            if (lstEntTran.length < 1) {
                addTransporte();
            }
            verificaTranportes();
            return false;
        });

        //Condidiones del transporte
        fillCondicionesTransporte();

        //Boton guardar entrada
        $('#ctl00_body_btn_save').button().click(function () {
            var IsValid = true;
            //valida transportes
            verificaTranportes();

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
                condicionesTransporteSet();
                $(this).hide();
            }

            return IsValid;

        });

        //Restos

        $('#div_restos').dialog({
            autoOpen: false,
            height: 180,
            width: 300,
            modal: true,
            resizable: false,
            closeOnEscape: false,
            open: function (event, ui) {
            },
            close: function (event, ui) {
                var ur = $('#ctl00_body_h_ubica_resto').val() * 1;
                if (ur == 0) {
                    alert('Es necesario seleccionar la ubicación del resto');
                    $('#div_restos').dialog('open');
                }
            }
        });

        $('#restoTarNueva').button().click(function () {
            $('#ctl00_body_h_ubica_resto').val(1);
            $('#div_restos').dialog('close');
            calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val(), $(piezaDeclarada).val());
        });
        $('#restoTarExistente').button().click(function () {
            $('#ctl00_body_h_ubica_resto').val(2)
            $('#div_restos').dialog('close');
            calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val(), $(piezaDeclarada).val());
        });

        //Limpiar tabla de tarimas
        $('#clearTableRestoTar').click(function () {
            $('#ctl00_body_h_ubica_resto').val('0');
            clearTableTarima();
        });
    }

    function fillCatalogo() {
        $('#ctl00_body_txt_proveedor').autocomplete({
            minLength: 3,
            source: function (request, response) {
                $.ajax({
                    type: 'GET',
                    url: "/handlers/Catalog.ashx",
                    //dataType: "jsonp",
                    data: {
                        catalogo: 'cliente_vendor',
                        opt: 'autoComplete',
                        w: request.term
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            }
            ,
            focus: function (event, ui) {
                $('#ctl00_body_txt_proveedor').val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $('#ctl00_body_txt_proveedor').val(ui.item.label);
                $('#ctl00_body_hf_vendor').val(ui.item.value);
                return false;
            }
        });
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

    function recall(obj, oCtrlCM) {
        if (obj.Id > 0) {
            $('#ctl00_body_txt_mercancia_desc').val(obj.Nombre);
            $('#ctl00_body_txt_negocio').val(obj.Negocio);
            switch (obj.Negocio) {
                case 'PR':
                case 'PT':
                    break;
                default:
                    alert('Es necesario corregir el negoio de la mercancía en el catálogo. \nPor el momento sólo puede ser PR o PT.');
                    oCrtlCM.OpenFrmUdt(obj, pag);
                    break;
            }
        }
        else {
            oCrtlCM.OpenFrm(obj.Codigo, pag);
        }
    }

    function calculaDif(bto_declarado, bto_recibido, pza_x_bulto) {

        $('#bto_faltante').html('0');
        $('#bto_sobrante').html('0');
        $('#pza_faltante').html('0');
        $('#pza_faltante').html('0');

        var dif_bto = $(bto_declarado).val() - $(bto_recibido).val();
        if (dif_bto > 0)
            $('#bto_faltante').html(dif_bto);
        else
            $('#bto_sobrante').html(Math.abs(dif_bto));

        var pza_declarada = $(bto_declarado).val() * pza_x_bulto;
        var pza_recibida = $(bto_recibido).val() * pza_x_bulto;

        var dif_pza = pza_declarada - pza_recibida;
        if (dif_pza > 0)
            $('#pza_faltante').html(dif_pza);
        else
            $('#pza_sobrante').html(Math.abs(dif_pza));

        if (dif_bto != 0 && $(bto_declarado).val() != '0' && $(bto_recibido).val() != '0')
            $.notify('Diferencia en Bultos y piezas', 'error');
    }

    function clearTableTarima() {
        $('#tbody_build_tarima').html('');
        $('#tf_tarimas').html('');
        $('#tf_cajas').html('');
        $('#tf_piezas').html('');
    }

    function calculaStd(pza_x_bto, bto_x_tarima, btos, pza_declarada) {

        clearTableTarima();

        var tbody = $('#tbody_build_tarima');

        if (isNaN(pza_x_bto) || isNaN(bto_x_tarima) || isNaN(btos) || isNaN(pza_declarada))
            return false;

        pza_x_bto = pza_x_bto * 1;
        bto_x_tarima = bto_x_tarima * 1;
        pzas = btos * pza_x_bto;
        btos = btos * 1;
        pza_declarada = pza_declarada * 1;

        if (pza_x_bto <= 0 || bto_x_tarima <= 0 || btos <= 0 || pza_declarada <= 0)
            return false;

        if (pza_declarada > btos * pza_x_bto) {
            alert('La cantidad de piezas (' + pza_declarada + ') no debe exceder el estandar (' + btos * pza_x_bto + ') para las cajas declaradas');
            $('#ctl00_body_txt_no_pieza_declarada').focus();
            return false;
        }
        var pzaMin = ((btos - 1) * pza_x_bto) + 1;
        if (pza_declarada < pzaMin) {
            alert('La cantidad de piezas (' + pza_declarada + ') no debe ser menor al estandar para las cajas declaradas (' + pzaMin + ')');
            $('#ctl00_body_txt_no_pieza_declarada').focus();
            return false;
        }

        var residuoBto = btos % bto_x_tarima;
        var residuoPza = (btos * pza_x_bto) % pza_declarada;
        if (pza_declarada < pza_x_bto)
            residuoPza = pza_declarada;

        var completaTar = Math.floor(btos / bto_x_tarima);

        var tf_tarimas = completaTar;
        var tf_cajas = completaTar * bto_x_tarima;
        var tf_piezas = completaTar * bto_x_tarima * pza_x_bto;

        //        if (residuoPza != 0) { //Hay restos
        //            if (residuoBto != 0 && $('#ctl00_body_h_ubica_resto').val() * 1 == 0) //Si existen tarimas que no estan completas, se puede dar la opción al usuario para seleccionar en que tarima viene el resto
        //                $('#div_restos').dialog('open');
        //            residuoBto--;
        //        }

        residuoBto = (pza_declarada / pza_x_bto) % bto_x_tarima;

        if (residuoPza != 0) { //Hay restos
            if (residuoBto != 0 && $('#ctl00_body_h_ubica_resto').val() * 1 == 0)
                $('#div_restos').dialog('open');
            else
                getTarByData(bto_x_tarima, pza_x_bto, btos, pza_declarada, residuoBto == 0 ? 1 : $('#ctl00_body_h_ubica_resto').val() * 1);
        }
        else {
            getTarByData(bto_x_tarima, pza_x_bto, btos, pza_declarada, $('#ctl00_body_h_ubica_resto').val() * 1);
        }



        //        //Arma tarimas con estandar
        //        if (residuoBto > 0) {
        //            var tr = '<tr>';
        //            var td = '<td>' + completaTar + '</td>';
        //            td += '<td>Tarima(s)</td>';
        //            td += '<td>X</td>';
        //            td += '<td>' + bto_x_tarima + '</td>';
        //            td += '<td>Caja(s) por Tarima</td>';
        //            td += '<td>=</td>';
        //            td += '<td>' + completaTar * bto_x_tarima + '</td>';
        //            td += '<td>Caja(s)</td>';
        //            td += '<td>X</td>';
        //            td += '<td>' + pza_x_bto + '</td>';
        //            td += '<td>Pza(s) por Caja</td>';
        //            td += '<td>=</td>';
        //            td += '<td>' + completaTar * bto_x_tarima * pza_x_bto + '</td>';
        //            td += '<td>Pieza(s)</td>';
        //            tr += td;
        //            tr += '</tr>';
        //            $(tbody).append(tr);

        //            //arma tarimas sin estandar (tarimas que no tienen la cantidad de cajas por tarima)
        //            if (residuoBto != 0) {
        //                tf_tarimas++;
        //                tf_cajas += residuoBto;
        //                tf_piezas += residuoBto * pza_x_bto;
        //                tr = '<tr>';
        //                td = '<td>1</td>';
        //                td += '<td>Tarima</td>';
        //                td += '<td>X</td>';
        //                td += '<td>' + residuoBto + '</td>';
        //                td += '<td>Caja(s) por Tarima</td>';
        //                td += '<td>=</td>';
        //                td += '<td>' + residuoBto + '</td>';
        //                td += '<td>Cajas</td>';
        //                td += '<td>X</td>';
        //                td += '<td>' + pza_x_bto + '</td>';
        //                td += '<td>Pza(s) por Caja</td>';
        //                td += '<td>=</td>';
        //                td += '<td>' + residuoBto * pza_x_bto + '</td>';
        //                td += '<td>Pieza(s)</td>';
        //                tr += td;
        //                tr += '</tr>';
        //                $(tbody).append(tr);
        //            }
        //        }
        //        else {
        //            tf_piezas = pza_declarada;
        //        }

        //        var tarNueva = true;
        //        if (residuoPza != 0) {
        //            if (residuoBto != 0) {
        //                switch ($('#ctl00_body_h_ubica_resto').val() * 1) {
        //                    case 1:
        //                        tarNueva = true;
        //                        break;
        //                    case 2:
        //                        tarNueva = false;
        //                        break;
        //                }
        //            }

        //            if (residuoBto > 0) {
        //                tf_cajas++;
        //                tf_piezas += residuoPza;
        //            }

        //            tr = '<tr>';
        //            if (tarNueva) {
        //                td = '<td>1</td>';
        //                if (residuoBto > 0)
        //                    tf_tarimas++;
        //            }
        //            else
        //                td = '<td>0</td>';
        //            td += '<td>Tarima</td>';
        //            td += '<td>X</td>';
        //            td += '<td>1</td>';
        //            td += '<td>Caja por Tarima</td>';
        //            td += '<td>=</td>';
        //            td += '<td>1</td>';
        //            td += '<td>Caja</td>';
        //            td += '<td>X</td>';
        //            td += '<td>' + residuoPza + '</td>';
        //            td += '<td>Pza(s) por Caja</td>';
        //            td += '<td>=</td>';
        //            td += '<td>' + residuoPza + '</td>';
        //            td += '<td>Pieza(s)</td>';
        //            tr += td;
        //            tr += '</tr>';


        //            $(tbody).append(tr);
        //        }

        //        $('#tf_tarimas').html(tf_tarimas);
        //        $('#tf_cajas').html(tf_cajas);
        //        $('#tf_piezas').html(tf_piezas);
    }

    function getTarByData(CjXTr, PzXCj, CjRec, PzRec, UbRes) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Almacen.ashx",
            //dataType: "jsonp",
            data: {
                'case': 'almacen',
                opt: 'calcTarimas',
                CjXTr: CjXTr,
                PzXCj: PzXCj,
                CjRec: CjRec,
                PzRec: PzRec,
                UbRes: UbRes
            },
            complete: function () {
                $('#div_orde_carga, #div_generales').removeClass('ajaxLoading');
            },
            success: function (data) {
                fillTblTar(data, CjXTr, PzXCj);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init()
            }
        });
    }

    function fillTblTar(data, bto_x_tarima, pza_x_bto) {

        clearTableTarima();

        var tbody = $('#tbody_build_tarima');
        var tf_tarimas = 0;
        var tf_cajas = 0;
        var tf_piezas = 0;

        $.each(data, function (i, obj) {

            tf_tarimas += obj.Id;
            tf_cajas += obj.Bultos;
            tf_piezas += obj.Piezas;

            var tr = '<tr>';
            var td = '<td>' + obj.Id + '</td>';
            td += '<td>Tarima(s)</td>';
            td += '<td>X</td>';
            td += '<td>' + bto_x_tarima + '</td>';
            td += '<td>Caja(s) por Tarima</td>';
            td += '<td>=</td>';
            td += '<td>' + obj.Bultos + '</td>';
            td += '<td>Caja(s)</td>';
            td += '<td>X</td>';
            td += '<td>' + pza_x_bto + '</td>';
            td += '<td>Pza(s) por Caja</td>';
            td += '<td>=</td>';
            td += '<td>' + obj.Piezas + '</td>';
            td += '<td>Pieza(s)</td>';
            tr += td;
            tr += '</tr>';

            $(tbody).append(tr);
        });

        $('#tf_tarimas').html(tf_tarimas);
        $('#tf_cajas').html(tf_cajas);
        $('#tf_piezas').html(tf_piezas);
    }

    function addTransporte() {
        var hf_entradaTransporte = $('#ctl00_body_hf_entradaTransporte');
        var transporteLinea = $('#txt_linea').val().toUpperCase().trim();
        var idTransporteTipo = $('#ctl00_body_ddlTipo_Transporte').val();
        var transporteTipo = $("#ctl00_body_ddlTipo_Transporte option:selected").text();
        var placa = $('#txt_placa').val().toUpperCase().trim();
        var caja = $('#txt_caja').val().toUpperCase().trim();
        var caja1 = $('#txt_caja1').val().toUpperCase().trim();
        var caja2 = $('#txt_caja2').val().toUpperCase().trim();

        if (!TransporteValido(transporteLinea, placa, caja, caja1, caja2))
            return false;

        idEntradaTransporte++;
        var oEntTran = new BeanEntradaTransporte(idEntradaTransporte, transporteLinea, idTransporteTipo, transporteTipo, placa, caja, caja1, caja2);

        var arrEntTran = $.grep(lstEntTran, function (obj) {
            return (obj.Transporte_linea == transporteLinea
                && obj.Id_transporte_tipo == idTransporteTipo
                && obj.Placa == placa
                && obj.Caja == caja
                && obj.Caja1 == caja1
                && obj.Caja2 == caja2);
        });
        if (arrEntTran.length == 0)
            lstEntTran.push(oEntTran);

        var tr = '';
        $('#tbody_transporte').html('');
        for (var iT in lstEntTran) {
            tr = '<tr id="' + lstEntTran[iT].Id + '">';
            var td = '<td>' + lstEntTran[iT].Transporte_linea + '</td>';
            td += '<td>' + lstEntTran[iT].Transporte_tipo + '</td>';
            td += '<td>' + lstEntTran[iT].Placa + '</td>';
            td += '<td>' + lstEntTran[iT].Caja + '</td>';
            td += '<td>' + lstEntTran[iT].Caja1 + '</td>';
            td += '<td>' + lstEntTran[iT].Caja2 + '</td>';
            td += '<td><button class="rem_transporte"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tbody_transporte').append(tr);
        }
        $(hf_entradaTransporte).val(JSON.stringify(lstEntTran));
        $('.rem_transporte').each(function () {
            $(this).click(function () {
                //alert($(this).parent().parent().attr('id'));
                var Id_EntTran = $(this).parent().parent().attr('id');
                lstEntTran = $.grep(lstEntTran, function (obj) {
                    return obj.Id != Id_EntTran;
                });
                $(this).parent().parent().remove();
                $(hf_entradaTransporte).val(JSON.stringify(lstEntTran));
                return false;
            });
        });
    } // fin addTransporte

    // valida tipo transporte <<ini>>
    function validaTipoTransporte() {
        $('#ctl00_body_ddlTipo_Transporte option:selected').each(function () {
            txt_dato_transRequerido($('#txt_placa'), $(this).attr('placa'));
            txt_dato_transRequerido($('#txt_caja'), $(this).attr('caja'));
            txt_dato_transRequerido($('#txt_caja1'), $(this).attr('caja1'));
            txt_dato_transRequerido($('#txt_caja2'), $(this).attr('caja2'));
        });
    }

    function txt_dato_transRequerido(txt, requerido) {
        $(txt).attr('readonly', 'readonly').val('N.A.');
        if (requerido == 'True') {
            $(txt).removeAttr('readonly');
            $(txt).val('');
        }
    }

    function TransporteValido(linea, placa, caja, caja1, caja2) {
        var strDatos = ['linea', 'placa', 'caja', 'caja1', 'caja2'];
        for (var dato in strDatos) {
            var input = $('#txt_' + strDatos[dato]);
            var info = $(input).val().toUpperCase().trim();
            if (info.length == 0) {
                $(info).focus();
                alert('Es necesario capturar la: ' + strDatos[dato]);
                return false;
            }
        }
        return true;
    }
    // valida tipo transporte <<fin>>

    // verifica tipo transporte <<ini>>
    function verificaTranportes() {
        $('#rfv_entradaTransporte').css('visibility', lstEntTran.length == 0 ? 'visible' : 'hidden');
    } // fin verifica tipo transporte

    //Condiciones del transporte
    function condicionesTransporteSet() {
        lstCondTran = [];
        $('#tbody_condiciones').children('tr').each(function () {
            var id = $(this).attr('id').split('_')[1];
            var val = $('input[name="name_' + id + '"]:checked', '#tbody_condiciones').val();
            val = val == 1 ? true : false;
            var o = new BeanEntradaTransporteCondicion(id, val);
            lstCondTran.push(o);
        });
        $('#ctl00_body_hf_condiciones_transporte').val(JSON.stringify(lstCondTran));
    }

    //Valida numero
    function numeroValido(dato, obj) {
        if (isNaN(dato)) {
            alert('La cantidad debe ser numérica');
            $(obj).focus().select();
            return false;
        }

        if (dato <= 0) {
            alert('La cantidad debe ser mayor a 0');
            $(obj).focus().select();
            return false;
        }
        //        if (cantidad > cantidadMaxima) {
        //            alert('La cantidad debe ser menor a ' + cantidadMaxima);
        //            return false;
        //        }
        return true;
    }
}

var master = new webApp.Master;
var pag = new MngArriboWH();
master.Init(pag);