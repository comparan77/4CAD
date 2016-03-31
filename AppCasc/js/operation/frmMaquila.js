var txt_fecha_aux;
var lstEntMaqDet = [];
var autoIncEntMaqDet = 0;

var BeanEntradaMaquilaDetail = function (id, idEntradaMaquila, danado, bultos, piezasxbulto, lote, tiene_remision) {
    this.Id = id;
    this.Id_entrada_maquila = idEntradaMaquila;
    this.Danado = danado;
    this.Bultos = bultos;
    this.Piezasxbulto = piezasxbulto;
    this.Lote = lote;
    this.Tiene_remision = tiene_remision;
}

var BeanEntradaMaquila = function (id_cliente, id_entrada, id_entrada_inventario, id_usuario, pallet, lst) {

    this.Id = 0;
    this.Id_cliente = id_cliente;
    this.Id_entrada = id_entrada;
    this.Id_usuario = id_usuario;
    this.Id_entrada_inventario = id_entrada_inventario;
    this.Fecha_trabajo = '1999-01-01';
    this.Pallet = pallet;
    this.Bulto = 0;
    this.Pieza = 0;
    this.Pieza_danada = 0;
    this.Bulto_faltante = 0;
    this.Bulto_sobrante = 0;
    this.Pieza_faltante = 0;
    this.Pieza_sobrante = 0;
    this.LstEntMaqDet = lst;
}

var MngMaquila = function () {

    this.Init = init;

    function init() {

        $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        var div_search = $('#div-search');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var hf_id_entrada_inventario = $('#ctl00_body_hf_id_entrada_inventario');
        var hf_id_entrada = $('#ctl00_body_hf_id_entrada');
        var inventory_control = $('#div-floor-control');
        var div_control_piso = $('#div-control-piso');
        var txt_fecha_trabajo = $('#ctl00_body_txt_fecha_trabajo');
        //        txt_fecha_aux = txt_fecha_trabajo.val();
        var btn_save = $('#ctl00_body_btn_save');
        var div_busqueda = $('#div_busqueda');

        var up_cantidades = $('#ctl00_body_up_cantidades');
        var up_maquila = $('#ctl00_body_up_maquila');
        var up_resultados = $('#ctl00_body_up_resultados');

        var btn_cerrar_maquila = $('#ctl00_body_btn_cerrar_maquila');

        $('#spn_Search').click(function () {
            if ($(this).hasClass('ui-icon-search')) {
                $(this).removeClass('ui-icon-search');
                $(this).addClass('ui-icon-close');
                $(div_busqueda).show('slow');
            }
            else {
                $(this).removeClass('ui-icon-close');
                $(this).addClass('ui-icon-search');
                $(div_busqueda).hide('slow');
            }
        });

        $(btn_save).button({ disabled: false }).click(function () {
            if (lstEntMaqDet.length == 0) {
                alert('Es necesario agregar detalle de la maquila');
                return false;
            }
            $(this).hide();
        });
        $(btn_cerrar_maquila).button();

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
        });

        //up_resultados <<ini>>
        $(up_resultados).panelReady(function () {
            $(div_busqueda).removeClass('ajaxLoading');
        });
        //up_resultados <<fin>>

        $(div_search).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        $(inventory_control).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        if (hf_id_entrada_inventario.val() != 0) {
            div_search.next().hide();
            inventory_control.next().show();
            div_control_piso.show();

            checkAmounts();

            //Estados de la mercancia <<ini>>
            $('#th_estados').buttonset();
            $('.rbEstado').each(function () {
                $(this).click(function () {
                    $('#thEstado').html($(this).next().children('span').html());
                });
            });

            $('#btn_add_detalle').unbind('click').click(function () {

                var bultos = $('#th_bultos').val();
                var piezasxbulto = $('#th_piezasxbulto').val();

                if (isNaN(bultos) || isNaN(piezasxbulto) || bultos.length == 0 || piezasxbulto.length == 0)
                    return false;

                bultos = bultos * 1;
                piezasxbulto = piezasxbulto * 1;

                if (bultos <= 0 || piezasxbulto <= 0)
                    return false;

                $('#th_bultos').val('');
                $('#th_piezasxbulto').val('');
                var lote = $('#ddl_lote').next().children('input').val();

                var oEntMaqDet = new BeanEntradaMaquilaDetail(autoIncEntMaqDet++, 0, $('#thEstado').html() == 'Indemne' ? 0 : 1, bultos, piezasxbulto, lote, false);
                lstEntMaqDet.push(oEntMaqDet);

                addDetail();
                $('#rb_indemne').trigger('click');
                return false;
            });

            //<<Captura de Trabajo Fin>>

            $(up_cantidades).panelReady(function () {

                var hf_bultos = $('#ctl00_body_hf_bultos');
                //var hf_pzasXbulto = $('#ctl00_body_hf_pzasXbulto');
                var txt_bulto = $('#ctl00_body_txt_bulto');
                var txt_pieza = $('#ctl00_body_txt_pieza');
                var txt_pieza_danada = $('#ctl00_body_txt_pieza_danada');

                var bulto_faltante = $('#td_bulto_faltante_fecha');
                var bulto_sobrante = $('#td_bulto_sobrante_fecha');
                var pieza_faltante = $('#td_pieza_faltante_fecha');
                var pieza_sobrante = $('#td_pieza_sobrante_fecha');

                var bulto_inventario = $(hf_bultos).val() * 1;
                var pieza_inventario = $('#td_pieza_inventario').html() * 1;
                //var piezaXbulto = $(hf_pzasXbulto).val() * 1;

                var diferencia = 0;
                var maquilado = 0;

                $(txt_bulto).blur(function () {
                    calculateDiferencias(txt_bulto, bulto_faltante, bulto_sobrante, txt_pieza, txt_pieza_danada, pieza_faltante, pieza_sobrante, bulto_inventario, pieza_inventario);
                });

                $(txt_pieza).blur(function () {
                    calculateDiferencias(txt_bulto, bulto_faltante, bulto_sobrante, txt_pieza, txt_pieza_danada, pieza_faltante, pieza_sobrante, bulto_inventario, pieza_inventario);
                });

                $(txt_pieza_danada).blur(function () {
                    calculateDiferencias(txt_bulto, bulto_faltante, bulto_sobrante, txt_pieza, txt_pieza_danada, pieza_faltante, pieza_sobrante, bulto_inventario, pieza_inventario);
                });

                if ($(txt_bulto).val() * 1 != 0 || $(txt_pieza).val() * 1 != 0)
                    calculateDiferencias(txt_bulto, bulto_faltante, bulto_sobrante, txt_pieza, txt_pieza_danada, pieza_faltante, pieza_sobrante, bulto_inventario, pieza_inventario);

                $(up_cantidades).removeClass('ajaxLoading');
            });
        }

        $('#ddl_lote').combobox();

        // Ordenes y códigos del pedimento
        $('#div_ordenescodigos').dialog({
            autoOpen: false,
            resizable: false,
            //height: 250,
            width: 850,
            modal: true
        });
        $('#lnk_pedimento').click(function () {
            $('#div_ordenescodigos').dialog('open');
            return false;
        });

        // Admnistrar dias trabajados
        mngDiasTrabajados(txt_fecha_trabajo, hf_id_entrada.val(), hf_id_entrada_inventario.val());

        //Limpiar informacion de maquila
        $('#lnk_limpiarCaptura').click(function () {
            txt_fecha_trabajo.val(moment().format('DD/MM/YYYY HH:mm:ss'));
            $('#ctl00_body_txt_pallet').val('0');
            $('#hf_piezasMaquiladasXDia').val(0);
            $('#ctl00_body_hf_id_maquilado').val(0);
            lstEntMaqDet = [];
            addDetail();
            return false;
        });

    } //Fin init

    function mngDiasTrabajados(txt_fecha_trabajo, id_entrada, id_entrada_inventario) {

        $('.diasTrabajados').children('li').each(function () {
            var lnk = $(this).children('a');
            $(lnk).unbind('click').click(function () {
                var id = $(this).attr('id').split('_')[1] * 1;
                loadDetailByDate(id);
                $(txt_fecha_trabajo).val($(this).html());
                return false;
            });

            var spn = $(this).children('span');
            $(spn).unbind('click').click(function () {
                var id = $(this).attr('id').split('_')[1];
                var diaTrabajado = $('#lnkDiaTrabajado_' + id).html();
                if (confirm('Desea eliminar el día de trabajo :' + diaTrabajado)) {
                    dltDiaTrabajado(id, id_entrada, id_entrada_inventario);
                }
            });

        });

    }

    function dltDiaTrabajado(id_entrada_maquila, id_entrada, id_entrada_inventario) {

        $('.diasTrabajados').parent().addClass('ajaxLoading');

        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=MqDelete',
            data: id_entrada_maquila,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {
                $('.diasTrabajados').parent().removeClass('ajaxLoading');
            },
            success: function (data) {
                alert(data);
                window.location.href = 'frmMaquila.aspx?_fk=' + id_entrada + "&_pk=" + id_entrada_inventario;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function calculateDiferencias(txt_bulto, bulto_faltante, bulto_sobrante, txt_pieza, txt_pieza_danada, pieza_faltante, pieza_sobrante, bulto_inventario, pieza_inventario) {

        var maquilado = 0;
        var diferencia = 0;
        var bulto_maquilado = 0;

        var bulto_maquilado_total = $('#td_bulto_maquilado').html() * 1;
        var pieza_maquilado_total = $('#td_pieza_maquilado').html() * 1;
        var pieza_danada_total = $(txt_pieza_danada).val() * 1;

        var dia_pallet = $('#ctl00_body_hf_dia_pallet').val() * 1;
        var dia_bulto = $('#ctl00_body_hf_dia_bulto').val() * 1;
        var dia_pieza = $('#ctl00_body_hf_dia_pieza').val() * 1;

        maquilado = $(txt_bulto).val() * 1;
        bulto_maquilado = maquilado;
        diferencia = bulto_inventario - maquilado - bulto_maquilado_total + dia_bulto;
        $(bulto_faltante).html('');
        $(bulto_sobrante).html('');
        if (diferencia > 0)
            $(bulto_faltante).html(diferencia);
        else
            $(bulto_sobrante).html(diferencia * -1);

        diferencia = 0;
        maquilado = 0;

        maquilado = $(txt_pieza).val() * 1;
        diferencia = pieza_inventario - maquilado - pieza_maquilado_total + dia_pieza; // -pieza_danada_total;
        $(pieza_faltante).html('');
        $(pieza_sobrante).html('');
        if (diferencia > 0)
            $(pieza_faltante).html(diferencia);
        else
            $(pieza_sobrante).html(diferencia * -1);

    }

    function checkAmounts() {

        var cantidades = ['pallet', 'bulto', 'piezabulto', 'pieza'];
        for (var c in cantidades) {
            //td_entrada = $('#td_' + cantidades[c] + '_entrada');
            //td_total = $('#td_' + cantidades[c] + '_total');
            td_inventario = $('#td_' + cantidades[c] + '_inventario');
            td_maquilado = $('#td_' + cantidades[c] + '_maquilado');

            var valInventario = td_inventario.html() * 1;
            var valMaquilado = td_maquilado.html() * 1;
            //var valTotal = td_total.children('span').children('input').val() * 1;
            //$(td_inventario).html(valTotal);

            //alert(valEntrada + ', ' + valTotal);

            //td_total.children('span').removeClass('ui-icon-alert');
            //td_total.children('span').removeClass('ui-icon-check');
            //td_inventario.prev('span').removeClass('ui-icon-alert');
            //td_inventario.prev('span').removeClass('ui-icon-check');
            td_maquilado.prev('span').removeClass('ui-icon-alert');
            td_maquilado.prev('span').removeClass('ui-icon-check');

            td_maquilado.prev('span').addClass('ui-icon-alert');

            if (valInventario != valMaquilado) {
                //td_total.css('color', 'red');
                //td_inventario.css('color', 'red');
                td_maquilado.css('color', 'red');

                //td_total.children('span').addClass('ui-icon-alert');
                //d_inventario.prev('span').addClass('ui-icon-alert');
                //td_maquilado.prev('span').addClass('ui-icon-alert');
            }
            else {
                //td_total.css('color', '');
                //td_inventario.css('color', '');
                td_maquilado.css('color', '');

                //td_total.children('span').addClass('ui-icon-check');
                //td_inventario.prev('span').addClass('ui-icon-check');
                td_maquilado.prev('span').addClass('ui-icon-check');
            }
        }

    } //Fin checkAmounts

    function addDetail() {

        calculateCantidades();

        var tr = '';
        $('#tblDetailBultos tbody').html('');

        var PzasTotal = 0;
        var oCommon = new Common();
        for (var item in lstEntMaqDet) {
            tr = '<tr id="' + lstEntMaqDet[item].Id + '">';
            var td = '<td>&nbsp;</td>';
            td += '<td>' + (lstEntMaqDet[item].Danado == 0 ? 'Indemne' : 'Dañado') + '</td>';
            td += '<td align="right">' + oCommon.GetSeparatorComasNumber(lstEntMaqDet[item].Bultos) + '</td>';
            td += '<td align="right">' + oCommon.GetSeparatorComasNumber(lstEntMaqDet[item].Piezasxbulto) + '</td>';
            td += '<td align="center">' + (lstEntMaqDet[item].Lote ? lstEntMaqDet[item].Lote : '') + '</td>';
            var pzaTot = lstEntMaqDet[item].Bultos * lstEntMaqDet[item].Piezasxbulto;
            //            td += '<td>' + lstEntMaqDet[item].Lote + '</td>';
            td += '<td align="right">' + oCommon.GetSeparatorComasNumber(pzaTot) + '</td>';
            if (lstEntMaqDet[item].Tiene_remision)
                td += '<td align="center">R</td>';
            else
                td += '<td align="center"><button class="rem_detail"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tblDetailBultos tbody').append(tr);
        }

        var hf_entrada_maquila_detail = $('#ctl00_body_hf_entrada_maquila_detail');

        $(hf_entrada_maquila_detail).val(JSON.stringify(lstEntMaqDet));
        $('.rem_detail').each(function () {
            $(this).click(function () {
                var Id = $(this).parent().parent().attr('id');
                lstEntMaqDet = $.grep(lstEntMaqDet, function (obj) {
                    return obj.Id != Id;
                });
                $(this).parent().parent().remove();
                $(hf_entrada_maquila_detail).val(JSON.stringify(lstEntMaqDet));
                calculateCantidades();
                return false;
            });
        });
    }

    function calculateCantidades() {
        var bultos = 0;
        var piezas = 0;

        $.each(lstEntMaqDet, function (i, obj) {
            bultos += obj.Bultos;
            piezas += obj.Piezasxbulto * obj.Bultos;
        });

        var oCommon = new Common;

        $('#bultos').val(oCommon.GetSeparatorComasNumber(bultos));
        $('#piezas').val(oCommon.GetSeparatorComasNumber(piezas));
        $('#thPzasTotal').html($('#piezas').val());

        var pieza_faltante = $('#td_pieza_faltante_fecha');
        var pieza_sobrante = $('#td_pieza_sobrante_fecha');
        var pieza_inventario = $('#td_pieza_inventario').html() * 1;
        var pieza_maquilado = $('#td_pieza_maquilado').html() * 1;

        var txt_piezasfaltantes = $('#piezasfaltantes');
        var txt_piezassobrantes = $('#piezassobrantes');

        $(txt_piezasfaltantes).val(0);
        $(txt_piezassobrantes).val(0);
        $(pieza_faltante).html('');
        $(pieza_sobrante).html('');
        var piezaMaquiladoXDia = $('#hf_piezasMaquiladasXDia').val() * 1;
        var difPiezas = pieza_inventario - piezas - pieza_maquilado + piezaMaquiladoXDia;

        if (difPiezas > 0) {
            $(txt_piezasfaltantes).val(difPiezas);
            $(pieza_faltante).html(difPiezas);
        }
        if (difPiezas < 0) {
            $(txt_piezassobrantes).val(Math.abs(difPiezas));
            $(pieza_sobrante).html(difPiezas * -1);
        }
    }

    function loadDetailByDate(idEntrada_maquila) {

        $('#up_cantidades').addClass('ajaxLoading');
        $('#hf_piezasMaquiladasXDia').val(0);
        $('#ctl00_body_hf_id_maquilado').val(0);
        $('#hf_maquila_abierta').val('');
        $('#ctl00_body_btn_save').val('Guardar Trabajo Realizado');
        $('#ctl00_body_btn_save').button('option', 'disabled', false);

        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx",
            data: {
                op: 'maquilaGet',
                key: idEntrada_maquila
            },
            complete: function () {
                $('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {

                lstEntMaqDet = [];

                if (data.Id > 0) {
                    $('#ctl00_body_hf_id_maquilado').val(data.Id);
                    $('#ctl00_body_txt_pallet').val(data.Pallet);
                    $('#hf_piezasMaquiladasXDia').val(data.Pieza);
                    $('#hf_maquila_abierta').val(data.Maquila_abierta);
                    lstEntMaqDet = [];
                    $.each(data.LstEntMaqDet, function (i, obj) {
                        lstEntMaqDet.push(obj);
                    });

                    if ($('#hf_maquila_abierta').val() == 'false') {
                        $('#ctl00_body_btn_save').button('option', 'disabled', true);
                        $('#ctl00_body_btn_save').val('La Maquila ya se ha Cerrado.');
                    }
                }
                addDetail();
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
var pag = new MngMaquila();
master.Init(pag);