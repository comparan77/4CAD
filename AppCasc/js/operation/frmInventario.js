var CTE_NUM_COL = 4;
var tdDirection;
var stepNum = 0;
var lstEntInvDet = [];
var autoIncEntInvDet = 0;
var lstEntInvLote = [];
var autoIncEntInvLote = 0;

var BeanEntradaInventarioLote = function (id, idEntradaInventario, codigo, ordencompra, lote, piezas) {
    this.Id = id;
    this.Id_entrada_inventario = idEntradaInventario;
    this.Codigo = codigo;
    this.Ordencompra = ordencompra;
    this.Lote = lote;
    this.Piezas = piezas;
}

var BeanEntradaInventarioDetail = function (id, idEntradaInventario, bultos, piezasxbulto) {
    this.Id = id;
    this.Id_entrada_inventario = idEntradaInventario;
    this.Bultos = bultos;
    this.Piezasxbulto = piezasxbulto;
}

var BeanEntradaInventario = function (id, idEntrada, idUsuario, idEntradaFondeo, codigoCliente, referencia, ordenCompra, codigo, idVendor, mercancia, idNom, solicitud, factura, valorUnitario, valorFactura, piezas, bultos, bultosxpallet, pallets, piezasRecibidas, bultosRecibidos, piezasFaltantes, piezasSobrantes, bultosFaltantes, bultosSobrantes, fechaMaquila, observaciones) {
    this.Id = id;
    this.Id_entrada = idEntrada;
    this.Id_usuario = idUsuario;
    this.Id_entrada_fondeo = idEntradaFondeo;
    this.Codigo_cliente = codigoCliente;
    this.Referencia = referencia;
    this.Orden_compra = ordenCompra;
    this.Codigo = codigo;
    this.Id_vendor = idVendor;
    this.Mercancia = mercancia;
    this.Id_nom = idNom;
    this.Solicitud = solicitud;
    this.Factura = factura;
    this.Valor_unitario = valorUnitario;
    this.Valor_factura = valorFactura;
    this.Piezas = piezas;
    this.Bultos = bultos;
    this.Bultosxpallet = bultosxpallet;
    this.Pallets = pallets;
    this.Piezas_recibidas = piezasRecibidas;
    this.Bultos_recibidos = bultosRecibidos;
    this.Piezas_falt = piezasFaltantes;
    this.Piezas_sobr = piezasSobrantes;
    this.Bultos_falt = bultosFaltantes;
    this.Bultos_sobr = bultosSobrantes;
    this.Observaciones = observaciones;
    this.Fecha_maquila = fechaMaquila;
    this.Id_estatus = 1;
}

function validateCatalog(source, args) {
    var cat = source.id.split('_')[3];
    var id = $('#ctl00_body_hf_' + cat).val() * 1;
    var spnErr = $('#spn_err_catalog');
    if (id <= 0) {
        $(spnErr).html('Es necesario seleccionar un ' + cat + ' de la lista');
        args.IsValid = false;
    }
    else
        args.IsValid = true;

    $(spnErr).click(function () {
        $(this).html('');
        $('#ctl00_body_txt_' + cat).focus();
    });
}

var MngInventario = function () {

    this.Init = init;

    function init() {

        $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        clearInputsInventory();

        var inventory_control = $('#div-floor-control');
        var div_search = $('#div-search');
        var up_piso = $('#ctl00_body_up_piso');
        var up_resultados = $('#ctl00_body_up_resultados');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var hf_id_entrada = $('#ctl00_body_hf_id_entrada');
        var div_control_piso = $('#div-control-piso');
        var div_busqueda = $('#div_busqueda');
        var div_data_stock = $('#div-data-stock');

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
        });

        if (hf_id_entrada.val() != 0) {
            div_search.next().hide();
            inventory_control.next().show();
            div_data_stock.next().show();
            div_control_piso.show();
        }


        $(inventory_control).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        $(div_search).click(function () {
            var div = $(this).next()
            if ($(div).css('display') == 'none') {
                $(div).show('slow')
            }
            else {
                $(div).hide('slow')
            }
        });

        //up_resultados <<ini>>
        $(up_resultados).panelReady(function () {
            $(div_busqueda).removeClass('ajaxLoading');
        });
        //up_resultados <<fin>>

        //up_piso <<ini>>
        //        $(up_piso).panelReady(function () {

        //            var txt_firstField = $('#ctl00_body_txt_vendor');
        //            var td_entrada;
        //            var td_total;
        //            var tbl_control_piso = $('#tbl_control_piso');
        //            var tr_header_piso = $('#tr_header_piso');
        //            var tr_input_piso = $('#tr_input_piso');
        //            var tr_footer_piso = $('#tr_footer_piso');
        //            var tbody_val_piso = $('#tbody_val_piso');
        //            var btn_save = $('#ctl00_body_btn_save');
        //            var txt_codigo_mercancia = $('#ctl00_body_txt_codigo_mercancia');
        //            var txt_ordencompra = $('#ctl00_body_txt_ordencompra');

        //            var txt_pza_bulto = $('#ctl00_body_txt_pza_bulto');
        //            var txt_pieza = $('#ctl00_body_txt_pieza');
        //            var txt_bulto = $('#ctl00_body_txt_bulto');
        //            var txt_val_unitario = $('#ctl00_body_txt_val_unitario');
        //            var txt_valor_factura = $('#ctl00_body_txt_valor_factura');

        //            var txt_pieza_recibida = $('#ctl00_body_txt_pieza_recibida');
        //            var txt_bulto_recibido = $('#ctl00_body_txt_bulto_recibido');
        //            var txt_pieza_falt = $('#ctl00_body_txt_pieza_falt');
        //            var txt_pieza_sobr = $('#ctl00_body_txt_pieza_sobr');
        //            var txt_bulto_falt = $('#ctl00_body_txt_bulto_falt');
        //            var txt_bulto_sobr = $('#ctl00_body_txt_bulto_sobr');

        //            var lnkMercancia = $('#ctl00_body_lnkMercancia');
        //            var up_codigo_mercancia = $('#ctl00_body_up_codigo_mercancia');

        //            var lnk_add_piso = $('#ctl00_body_lnk_add_piso');
        //            var lnk_del_piso = $('.ui-icon-minus');

        //            $(lnkMercancia).click(function () {
        //                $(up_piso).addClass('ajaxLoading');
        //            });

        //            $(lnk_add_piso).click(function () {
        //                $(up_piso).addClass('ajaxLoading');
        //            });

        //            $(lnk_del_piso).each(function () {
        //                $(this).click(function () { $(up_piso).addClass('ajaxLoading'); });
        //            });

        //            $(btn_save).button().click(function () {
        //                if (confirm('Se va(n) a guardar ' + countData() + ' registro(s), ¿Desea continuar con la operación?')) {
        //                    return true;
        //                }
        //                else
        //                    return false;
        //            });

        //            if (countData() <= 0)
        //                $(btn_save).hide();

        //            $(txt_pza_bulto).unbind('blur').blur(function () {
        //                var cant_bulto = $(txt_bulto).val() * 1;
        //                var cant_pzaXbulto = $(txt_pza_bulto).val() * 1;
        //                var val_unitario = $(txt_val_unitario).val() * 1;
        //                $(txt_pieza).val(cant_bulto * cant_pzaXbulto);
        //                $(txt_valor_factura).val(getCurrFormat(cant_bulto * cant_pzaXbulto * val_unitario));
        //            });

        //            $(txt_bulto).unbind('blur').blur(function () {
        //                var cant_bulto = $(txt_bulto).val() * 1;
        //                var cant_pzaXbulto = $(txt_pza_bulto).val() * 1;
        //                var val_unitario = $(txt_val_unitario).val() * 1;
        //                $(txt_pieza).val(cant_bulto * cant_pzaXbulto);
        //                $(txt_valor_factura).val(getCurrFormat(cant_bulto * cant_pzaXbulto * val_unitario));
        //            });

        //            $(txt_val_unitario).unbind('blur').blur(function () {
        //                var cant_bulto = $(txt_bulto).val() * 1;
        //                var cant_pzaXbulto = $(txt_pza_bulto).val() * 1;
        //                var val_unitario = $(txt_val_unitario).val() * 1;
        //                $(txt_valor_factura).val(getCurrFormat(cant_bulto * cant_pzaXbulto * val_unitario));
        //            });

        //            $(txt_pieza_recibida).unbind('blur').blur(function () {
        //                var calculado = $(txt_pieza).val() * 1;
        //                var recibido = $(txt_pieza_recibida).val() * 1;
        //                $(txt_pieza_sobr).val(0);
        //                $(txt_pieza_falt).val(0);
        //                var dif = calculado - recibido;
        //                if (dif < 0)
        //                    $(txt_pieza_sobr).val(dif * -1);
        //                if (dif > 0)
        //                    $(txt_pieza_falt).val(dif);
        //            });

        //            $(txt_bulto_recibido).unbind('blur').blur(function () {
        //                var calculado = $(txt_bulto).val() * 1;
        //                var recibido = $(txt_bulto_recibido).val() * 1;
        //                $(txt_bulto_sobr).val(0);
        //                $(txt_bulto_falt).val(0);
        //                var dif = calculado - recibido;
        //                if (dif < 0)
        //                    $(txt_bulto_sobr).val(dif * -1);
        //                if (dif > 0)
        //                    $(txt_bulto_falt).val(dif);
        //            });

        //            $(txt_ordencompra).unbind('blur').blur(function () {
        //                if (existsData($(this).val(), $(txt_codigo_mercancia).val())) {
        //                    alert('La orden de compra o código ya fueron proporcionados')
        //                    $(txt_codigo_mercancia).focus().select();
        //                }
        //            });

        //            $(txt_codigo_mercancia).unbind('blur').blur(function () {
        //                if (existsData($(txt_ordencompra).val(), $(this).val())) {
        //                    alert('La orden de compra o código ya fueron proporcionados')
        //                    $(txt_ordencompra).focus().select();
        //                }
        //            });

        //            txt_firstField.keydown(function (event) {
        //                if (event.which == 9) {
        //                    if (event.shiftKey) {
        //                        event.preventDefault();
        //                    }
        //                }
        //            });

        //            //Seleccionar todo el contenido en el evento focus
        //            $(tr_input_piso).children('td').each(function () {
        //                $(this).children('input:text').click(function () {
        //                    $(this).select();
        //                });
        //            });

        //            var countCols = $(tr_input_piso)[0].cells.length - 1;
        //            stateButtons(countCols, 0);
        //            stepNum = 0;
        //            clickButtons(countCols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso);

        //            $('.tdMove').each(function () {
        //                $(this).keydown(function (event) {
        //                    if (event.which == 9) {
        //                        var num_col = $(this).parent().children().index($(this));

        //                        var countCols = $(tr_input_piso)[0].cells.length - 1;
        //                        var firstReturn = countCols - CTE_NUM_COL;

        //                        if (event.shiftKey) {

        //                            stateButtons(countCols, num_col - 1);
        //                            stepNum--;
        //                            if (num_col == 0)
        //                                event.preventDefault();
        //                            else
        //                                if (num_col <= firstReturn) {
        //                                    event.preventDefault();

        //                                    tr_header_piso.children('th').eq(num_col - 1).removeClass('hidden');
        //                                    tr_header_piso.children('th').eq(num_col + CTE_NUM_COL).addClass('hidden');

        //                                    tr_input_piso.children('td').eq(num_col - 1).removeClass('hidden');
        //                                    tr_input_piso.children('td').eq(num_col + CTE_NUM_COL).addClass('hidden');
        //                                    tr_input_piso.children('td').eq(num_col - 1).children('input').focus().select();

        //                                    tbody_val_piso.children('tr').each(function () {
        //                                        $(this).children('td').eq(num_col - 1).removeClass('hidden');
        //                                        $(this).children('td').eq(num_col + CTE_NUM_COL).addClass('hidden');
        //                                    });

        //                                    tr_footer_piso.children('td').eq(num_col - 1).removeClass('hidden');
        //                                    tr_footer_piso.children('td').eq(num_col + CTE_NUM_COL).addClass('hidden');
        //                                }
        //                        }
        //                        else {

        //                            stateButtons(countCols, num_col + 1);
        //                            stepNum++;
        //                            if (num_col >= CTE_NUM_COL) {

        //                                event.preventDefault();

        //                                tr_header_piso.children('th').eq(num_col - CTE_NUM_COL).addClass('hidden');
        //                                tr_header_piso.children('th').eq(num_col + 1).removeClass('hidden');

        //                                tr_input_piso.children('td').eq(num_col - CTE_NUM_COL).addClass('hidden');
        //                                tr_input_piso.children('td').eq(num_col + 1).removeClass('hidden');
        //                                tr_input_piso.children('td').eq(num_col + 1).children('input').focus().select();

        //                                tbody_val_piso.children('tr').each(function () {
        //                                    $(this).children('td').eq(num_col - CTE_NUM_COL).addClass('hidden');
        //                                    $(this).children('td').eq(num_col + 1).removeClass('hidden');
        //                                });

        //                                tr_footer_piso.children('td').eq(num_col - CTE_NUM_COL).addClass('hidden');
        //                                tr_footer_piso.children('td').eq(num_col + 1).removeClass('hidden');
        //                                try {
        //                                    //tr_input_piso.children('td').eq(num_col).children('input').focus();
        //                                } catch (e) { }
        //                            }
        //                        }
        //                    }
        //                });
        //            });

        var cantidades = ['pallet', 'bulto', 'pieza'];
        for (var c in cantidades) {
            td_entrada = $('#td_' + cantidades[c] + '_recibido');
            td_total = $('#td_' + cantidades[c] + '_total');
            td_inventario = $('#td_' + cantidades[c] + '_inventario');
            td_maquilado = $('#td_' + cantidades[c] + '_maquilado');

            var valEntrada = td_entrada.html() * 1;
            var valTotal = td_total.children('span').children('input').val() * 1;
//            $(td_inventario).html(valTotal);

            //                //alert(valEntrada + ', ' + valTotal);

            td_total.children('span').removeClass('ui-icon-alert');
            td_total.children('span').removeClass('ui-icon-check');
            td_inventario.prev('span').removeClass('ui-icon-alert');
            td_inventario.prev('span').removeClass('ui-icon-check');
            td_maquilado.prev('span').removeClass('ui-icon-alert');
            td_maquilado.prev('span').removeClass('ui-icon-check');

            td_maquilado.prev('span').addClass('ui-icon-alert');

            if (valEntrada != valTotal) {
                td_total.css('color', 'red');
                td_inventario.css('color', 'red');
                td_maquilado.css('color', 'red');

                td_total.children('span').addClass('ui-icon-alert');
                td_inventario.prev('span').addClass('ui-icon-alert');
                //td_maquilado.prev('span').addClass('ui-icon-alert');
            }
            else {
                td_total.css('color', '');
                td_inventario.css('color', '');
                td_maquilado.css('color', '');

                td_total.children('span').addClass('ui-icon-check');
                td_inventario.prev('span').addClass('ui-icon-check');
                //td_maquilado.prev('span').addClass('ui-icon-check');
            }
        }

        //            //Catalogos
        //            var ct100 = 'ctl00_body_';
        //            var arrCat = ['nom', 'vendor', 'codigo_mercancia']; //, 'vendor', 'proveedor', 'descripcion', 'NOM'];

        //            try {
        //                for (var arr in arrCat) {
        //                    fillCatalogo(arrCat[arr], $('#' + ct100 + 'hf_cat_' + arrCat[arr]).val());
        //                    //validateCatalogo(arrCat[arr]);
        //                }
        //            } catch (e) {

        //            }

        //            txt_firstField.focus().select();

        //            //up_codigo_mercancia <<ini>>
        //            $(up_codigo_mercancia).panelReady(function () {
        //                var cat = 'codigo_mercancia';
        //                var values = $('#' + ct100 + 'hf_cat_' + cat).val();
        //                fillCatalogo(cat, values);
        //                $('#' + ct100 + 'txt_codigo_mercancia').val($('#' + ct100 + 'hf_codigo_proporcionado').val());
        //                $(up_piso).removeClass('ajaxLoading');
        //            });
        //            //up_codigo_mercancia <<fin>>

        //            $(up_piso).removeClass('ajaxLoading');
        //            $("html, body").animate({ scrollTop: $(document).height() }, "slow");

        //        });
        //up_piso <<fin>>

        //$('#div-codigos ul').children('li').each(function () {
        $('.selectCodigo').each(function () {
            $(this).click(function () {
                $('#div-info-codigo').addClass('ajaxLoading');
                $('.selectCodigo').each(function () {
                    $(this).removeClass('selectLink');
                });
                //$('#div-codigos ul').children('li').each(function () { $(this).children('a').removeClass('selectLink'); });
                //$(this).children('a').addClass('selectLink');
                $(this).addClass('selectLink');
                var idFondeo = $(this).children('td').first().children('input').attr('id');
                $('#hf_id_entrada_fondeo').val(idFondeo);

                clearInputsInventory();

                $('#ctl00_body_btnSaveCodigo').show();

                $.ajax({
                    type: 'GET',
                    url: "/handlers/Operation.ashx",
                    //dataType: "jsonp",
                    data: {
                        op: 'stockcode',
                        key: idFondeo
                    },
                    complete: function () {
                        $('#div-info-codigo').removeClass('ajaxLoading');
                    },
                    success: function (data) {

                        $(data.LstClienteVendor).each(function (i, obj) {
                            $("#vendor").append('<option value="' + obj.Id + '">' + obj.Codigo + '-' + obj.Nombre + '</option>');
                        });
                        $(data.LstClienteMercancia).each(function (i, obj) {
                            $("#mercancia").append('<option codigo="' + obj.Codigo + '" mercancia="' + obj.Nombre + '" value="' + obj.Id + '">' + obj.Codigo + '-' + obj.Nombre + '</option>');
                        });
                        $('#ctl00_body_hf_codigo_mercancia').val(data.Codigo);
                        $('#orden').val(data.Orden);
                        $('#factura').val(data.Factura);
                        var oCommon = new Common();
                        $('#valorunitario').val('$' + (data.Valorfactura / data.Piezas));
                        $('#valorfactura').val(oCommon.GetCurrencyFormat(data.Valorfactura));
                        $('#piezasdeclaradas').val(data.Piezas);

                        lstEntInvDet = [];
                        lstEntInvLote = [];
                        $('#tblDetailBultos tbody').html('');
                        $('#tblDetailLote tbody').html('');

                        $('#ctl00_body_fechamaquila').datepicker("setDate", new Date());

                        //Verifica si existe el registro en inventario
                        if (data.PEntInv.Id > 0) {
                            $('#ctl00_body_btnSaveCodigo').show();
                            $('#idEntInv').val(data.PEntInv.Id);
                            $('#solicitud').val(data.PEntInv.Lote);
                            $('#lote').val(data.PEntInv.Solicitud);
                            $('#ctl00_body_bultos').val(data.PEntInv.Bultos);
                            $('#ctl00_body_piezasxbulto').val(data.PEntInv.Piezasxbulto);
                            $('#ctl00_body_piezasrecibidas').val(data.PEntInv.Piezas_recibidas);
                            $('#ctl00_body_bultosrecibidos').val(data.PEntInv.Bultos_recibidos);
                            $('#bultosxpallet').val(data.PEntInv.Bultosxpallet);
                            $('#pallets').val(data.PEntInv.Pallets);
                            $('#piezasfaltantes').val(data.PEntInv.Piezas_falt);
                            $('#piezassobrantes').val(data.PEntInv.Piezas_sobr);
                            $('#bultosfaltantes').val(data.PEntInv.Bultos_falt);
                            $('#bultossobrantes').val(data.PEntInv.Bultos_sobr);
                            $('#ctl00_body_fechamaquila').val(data.PEntInv.Fecha_maquila.replace('T00:00:00', ''));
                            $('#notas').val(data.PEntInv.Observaciones);

                            if (data.PEntInv.Id_estatus * 1 > 2) {
                                $('#ctl00_body_btnSaveCodigo').hide();
                            }

                            $.each(data.PEntInv.LstEntInvDet, function (i, obj) {
                                lstEntInvDet.push(obj);
                            });
                            addDetail();

                            $.each(data.PEntInv.LstEntInvLote, function (i, obj) {
                                lstEntInvLote.push(obj);
                            });
                            addDetailLote();
                        }

                        $('#ctl00_body_bultos, #ctl00_body_piezasxbulto, #ctl00_body_piezasrecibidas, #ctl00_body_bultosrecibidos').change(function () {
                            calculateCantidades();
                        });

                        $('#bultosxpallet').unbind('change').change(function () {
                            if (isNaN($(this).val())) {
                                $(this).val('');
                                $(this).focus();
                                alert('Es necesario capturar un número');
                            }
                            else {
                                var pxb = $(this).val() * 1;
                                if (pxb < 1) {
                                    alert('Es necesario capturar un valor mayor a 0');
                                }
                                else {
                                    var bt = $('#ctl00_body_bultosrecibidos').val() * 1;
                                    var tot = bt / pxb;
                                    $('#pallets').val(Math.ceil(tot));
                                }
                            }
                        });

                        calculateCantidades();

                        $('#btn_add_lote').removeAttr('disabled').unbind('click').click(function () {

                            var lote = $.trim($('#th_lote').val());
                            var piezas = $('#th_piezasLote').val();

                            if (isNaN(piezas) || piezas.length == 0)
                                return false;

                            if (piezas <= 0)
                                return false;

                            $('#th_lote').val('');
                            $('#th_piezasLote').val('');

                            var oEntInvLote = new BeanEntradaInventarioLote(autoIncEntInvLote++, 0, $('#ctl00_body_hf_codigo_mercancia').val(), $('#orden').val(), lote, piezas * 1);

                            //Verifica que no se trate del mismo lote
                            var lstMismoLote = $.grep(lstEntInvLote, function (obj) {
                                return obj.Lote == oEntInvLote.Lote;
                            });

                            if (lstMismoLote.length > 0) {
                                alert('El lote ya ha sido agregado, en caso de estar duplicado realizar la suma por lotes y eliminar el existente');
                                return false;
                            }

                            lstEntInvLote.push(oEntInvLote);

                            addDetailLote();
                            return false;
                        });

                        $('#btn_add_detalle').removeAttr('disabled').unbind('click').click(function () {

                            var bultos = $('#th_bultos').val();
                            var piezasxbulto = $('#th_piezasxbulto').val();
                            var lote = $('#th_lote').val();

                            if (isNaN(bultos) || isNaN(piezasxbulto) || bultos.length == 0 || piezasxbulto.length == 0)
                                return false;

                            bultos = bultos * 1;
                            piezasxbulto = piezasxbulto * 1;

                            if (bultos <= 0 || piezasxbulto <= 0)
                                return false;

                            $('#th_bultos').val('');
                            $('#th_piezasxbulto').val('');

                            var oEntInvDet = new BeanEntradaInventarioDetail(autoIncEntInvDet++, 0, bultos, piezasxbulto);
                            lstEntInvDet.push(oEntInvDet);

                            addDetail();
                            return false;
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        var oErrorMessage = new ErrorMessage();
                        oErrorMessage.SetError(jqXHR.responseText);
                        oErrorMessage.Init();
                    }
                });

                return false;
            });

            $('#ctl00_body_btnSaveCodigo').unbind('click').button().click(function () {
                var oCommon = new Common();
                if (lstEntInvDet.length < 1) {
                    alert('Es necesario desglosar bultos y piezas por bulto');
                    return false;
                }
                var oBEI = new BeanEntradaInventario(
                                    $('#idEntInv').val(),
                                    $('#ctl00_body_hf_id_entrada').val(),
                                    0,
                                    $('#hf_id_entrada_fondeo').val(),
                                    $('#ctl00_body_hf_codigo').val(),
                                    $('#ctl00_body_hf_referencia_entrada').val(),
                                    $('#orden').val(),
                                    $('#mercancia option:selected').attr('codigo'),
                                    $('#vendor').val(),
                                    $('#mercancia option:selected').attr('mercancia'),
                                    $('#ctl00_body_nom').val(),
                                    $('#solicitud').val(),
                                    $('#factura').val(),
                                    oCommon.GetOnlyDecimal($('#valorunitario').val()),
                                    oCommon.GetOnlyDecimal($('#valorfactura').val()),
                                    oCommon.GetOnlyDecimal($('#piezasdeclaradas').val()),
                                    oCommon.GetOnlyDecimal($('#bultos').val()),
                                    oCommon.GetOnlyDecimal($('#bultosxpallet').val()),
                                    (isNaN($('#pallets').val()) || $('#pallets').val().length < 1 ? 0 : $('#pallets').val()),
                                    oCommon.GetOnlyDecimal($('#ctl00_body_piezasrecibidas').val()),
                                    oCommon.GetOnlyDecimal($('#ctl00_body_bultosrecibidos').val()),
                                    oCommon.GetOnlyDecimal($('#piezasfaltantes').val()),
                                    oCommon.GetOnlyDecimal($('#piezassobrantes').val()),
                                    oCommon.GetOnlyDecimal($('#bultosfaltantes').val()),
                                    oCommon.GetOnlyDecimal($('#bultossobrantes').val()),
                                    $('#ctl00_body_fechamaquila').val(),
                                    $('#notas').val());
                $('#ctl00_body_hf_entrada_inventario').val(JSON.stringify(oBEI));
            });
        });

        //NOM info
        showNomDescription();
        $('#ctl00_body_nom').change(function () {
            showNomDescription();
        });

        //ShowHide desglose lotes
        $('#spn_desglose_lotes').click(function () {
            if ($('#tblDetailLotes').hasClass('hidden')) {
                $('#tblDetailLotes').removeClass('hidden');
                $(this).removeClass('ui-icon-arrowthick-1-s');
                $(this).addClass('ui-icon-arrowthick-1-n');
            }
            else {
                $('#tblDetailLotes').addClass('hidden');
                $(this).addClass('ui-icon-arrowthick-1-s');
                $(this).removeClass('ui-icon-arrowthick-1-n');
            }
        });

    } //init <<fin>>

    function showNomDescription() {

        $('#ctl00_body_nom option:selected').each(function () {
            //alert($(this).attr('description'));
            $('#spnNomDesc').attr('title', $(this).attr('description'));
            $("[title]").tooltip({
                position: {
                    my: "left top",
                    at: "right+5 top-5"
                }
            });
        });

    }

    function addDetailLote() {

        var tr = '';
        $('#tblDetailLotes tbody').html('');

        calculateCantidadesLote();

        var oCommon = new Common();
        for (var item in lstEntInvLote) {
            tr = '<tr id="' + lstEntInvLote[item].Id + '">';
            var td = '<td align="left">' + lstEntInvLote[item].Lote + '</td>';
            td += '<td align="right">' + oCommon.GetSeparatorComasNumber(lstEntInvLote[item].Piezas) + '</td>';
            td += '<td><button class="rem_detail_lote"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tblDetailLotes tbody').append(tr);
        }

        var hf_entrada_inventario_lote = $('#ctl00_body_hf_entrada_inventario_lote');

        $(hf_entrada_inventario_lote).val(JSON.stringify(lstEntInvLote));
        $('.rem_detail_lote').each(function () {
            $(this).click(function () {
                //alert($(this).parent().parent().attr('id'));
                var Id = $(this).parent().parent().attr('id');
                lstEntInvLote = $.grep(lstEntInvLote, function (obj) {
                    return obj.Id != Id;
                });
                $(this).parent().parent().remove();
                $(hf_entrada_inventario_lote).val(JSON.stringify(lstEntInvLote));
                calculateCantidadesLote();
                return false;
            });
        });

    }

    function addDetail() {

        calculateCantidades();

        var tr = '';
        $('#tblDetailBultos tbody').html('');

        var oCommon = new Common();
        for (var item in lstEntInvDet) {
            tr = '<tr id="' + lstEntInvDet[item].Id + '">';
            var td = '<td align="right">' + oCommon.GetSeparatorComasNumber(lstEntInvDet[item].Bultos) + '</td>';
            td += '<td align="right">' + oCommon.GetSeparatorComasNumber(lstEntInvDet[item].Piezasxbulto) + '</td>';
            td += '<td><button class="rem_detail"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tblDetailBultos tbody').append(tr);
        }

        var hf_entrada_inventario_detail = $('#ctl00_body_hf_entrada_inventario_detail');

        $(hf_entrada_inventario_detail).val(JSON.stringify(lstEntInvDet));
        $('.rem_detail').each(function () {
            $(this).click(function () {
                //alert($(this).parent().parent().attr('id'));
                var Id = $(this).parent().parent().attr('id');
                lstEntInvDet = $.grep(lstEntInvDet, function (obj) {
                    return obj.Id != Id;
                });
                $(this).parent().parent().remove();
                $(hf_entrada_inventario_detail).val(JSON.stringify(lstEntInvDet));
                calculateCantidades();
                return false;
            });
        });
    }

    function calculateCantidadesLote() {

        var th_totLote = $('#th_totLote');
        var txt_sumLotes = $('#ctl00_body_txt_sumLotes');
        var difLote = 0;
        var piezasRem = 0;
        var piezasLote = 0;
        var bultos = 0;

        $('#ctl00_body_rangeVal_sumLotes').css('visibility', 'hidden');

        $.each(lstEntInvLote, function (i, obj) {
            piezasLote += obj.Piezas;
        });

        $.each(lstEntInvDet, function (i, obj) {
            bultos += obj.Bultos;
            piezasRem += obj.Piezasxbulto * obj.Bultos;
        });

        difLote = piezasLote - piezasRem;
        //Solo se valida la diferencia de piezas en lotes cuando no tiene maquila
        if ($('#idEntInv').val() * 1 > 0)
            difLote = 0;
        $(txt_sumLotes).val(difLote);
        $(th_totLote).html(piezasLote);

        if (lstEntInvLote.length == 0) {
            $(txt_sumLotes).val(0);
        }

    }

    function calculateCantidades() {
        var bultos = 0;
        var piezas = 0;

        $.each(lstEntInvDet, function (i, obj) {
            bultos += obj.Bultos;
            piezas += obj.Piezasxbulto * obj.Bultos;
        });

        var oCommon = new Common;

        $('#bultos').val(oCommon.GetSeparatorComasNumber(bultos));
        $('#piezas').val(oCommon.GetSeparatorComasNumber(piezas));

        var piezasrecibidas = $('#ctl00_body_piezasrecibidas').val();
        var bultosrecibidos = $('#ctl00_body_bultosrecibidos').val();

        var txt_piezasfaltantes = $('#piezasfaltantes');
        var txt_piezassobrantes = $('#piezassobrantes');
        var txt_bultosfaltantes = $('#bultosfaltantes');
        var txt_bultossobrantes = $('#bultossobrantes');

        var piezasCalculadas = piezas;
        var difPiezas = piezasCalculadas - piezasrecibidas;
        $(txt_piezasfaltantes).val(0);
        $(txt_piezassobrantes).val(0);
        if (difPiezas > 0)
            $(txt_piezasfaltantes).val(difPiezas);
        if (difPiezas < 0)
            $(txt_piezassobrantes).val(Math.abs(difPiezas));

        var difBultos = bultos - bultosrecibidos;
        $(txt_bultosfaltantes).val(0);
        $(txt_bultossobrantes).val(0);
        if (difBultos > 0)
            $(txt_bultosfaltantes).val(difBultos);
        if (difBultos < 0)
            $(txt_bultossobrantes).val(Math.abs(difBultos));

        calculateCantidadesLote();
    }

    function getCurrFormat(valor) {
        var oCommon = new Common();
        return oCommon.GetCurrencyFormat(valor);
    }

    function fillCatalogo(cat, cat_json) {
        var arrCat = jQuery.parseJSON(cat_json);
        $('#ctl00_body_txt_' + cat).autocomplete({
            minLength: 1,
            source: cat != 'codigo_mercancia' ? arrCat :
                function (request, response) {
                    $.ajax({
                        type: 'GET',
                        url: "/handlers/Catalog.ashx",
                        //dataType: "jsonp",
                        data: {
                            catalogo: 'cliente_mercancia',
                            codigo: request.term,
                            Idcliente: $('#ctl00_body_hf_cliente_grupo').val()
                        },
                        success: function (data) {
                            response(data);
                        }
                    });
                }
            ,
            focus: function (event, ui) {
                $('#ctl00_body_txt_' + cat).val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $('#ctl00_body_txt_' + cat).val(ui.item.label);
                $('#ctl00_body_hf_' + cat).val(ui.item.value);
                if (cat == 'codigo_mercancia') {
                    $('#ctl00_body_txt_mercancia').val(ui.item.name);
                    $('#hf_clienteMercanciaClase').val(ui.item.clase);
                    $('#ctl00_body_txt_pza_bulto, #ctl00_body_txt_pieza_recibida').removeAttr('readonly');
                    switch (ui.item.clase) {
                        case 'COM':
                            $('#ctl00_body_txt_pza_bulto').attr('readonly', 'readonly').val(1);
                            $('##ctl00_body_txt_pieza_recibida').attr('readonly', 'readonly').val(0);
                            break;
                    }
                }
                if (cat == 'vendor') {
                    $('#ctl00_body_txt_vendor').val(ui.item.text);
                }
                return false;
            }
        }).val('');
    }

    function existsData(orden_compra, codigo) {

        var allowData = [];
        $('.unique').each(function () {
            allowData.push($(this).html().toUpperCase() + '|' + $(this).prev().prev().html().toUpperCase());
        });
        var index = $.inArray(orden_compra.toUpperCase() + '|' + codigo.toUpperCase(), allowData);
        return index >= 0;
    }

    function countData() {

        var data = [];
        $('.unique').each(function () {
            data.push($(this).html());
        });

        return data.length;
    }

    function stateButtons(countCols, index_th) {

        var firstReturn = countCols - CTE_NUM_COL;

        if (index_th <= 0)
            $('#spn_move_fir').addClass('ui-state-disabled');
        else
            $('#spn_move_fir').removeClass('ui-state-disabled');

        if (index_th <= CTE_NUM_COL)
            $('#spn_move_pre').addClass('ui-state-disabled');
        else
            $('#spn_move_pre').removeClass('ui-state-disabled');

        if (index_th >= firstReturn)
            $('#spn_move_nex').addClass('ui-state-disabled');
        else
            $('#spn_move_nex').removeClass('ui-state-disabled');

        if (index_th >= countCols - 1)
            $('#spn_move_end').addClass('ui-state-disabled');
        else
            $('#spn_move_end').removeClass('ui-state-disabled');
    }

    function clickButtons(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso) {

        //var h_stepnumber = $('#h_stepnumber');

        $('#spn_move_end').unbind('click').click(function () {
            hideCols(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso);
            for (var idx = count_cols - CTE_NUM_COL; idx <= count_cols; idx++) {
                tr_header_piso.children('th').eq(idx).removeClass('hidden');
                tr_input_piso.children('td').eq(idx).removeClass('hidden');
                tbody_val_piso.children('tr').each(function () {
                    $(this).children('td').eq(idx).removeClass('hidden');
                });
                tr_footer_piso.children('td').eq(idx).removeClass('hidden');
            }
            tr_input_piso.children('td').eq(count_cols - 1).children('input').focus().select();
            stateButtons(count_cols, count_cols - 1);
        });

        $('#spn_move_nex').unbind('click').click(function () {
            hideCols(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso);

            stepNum += CTE_NUM_COL + 1;
            if (stepNum + CTE_NUM_COL >= count_cols)
                stepNum = count_cols - CTE_NUM_COL;
            //$(h_stepnumber).val(stepNum);
            for (var idx = stepNum; idx < stepNum + CTE_NUM_COL + 1; idx++) {
                tr_header_piso.children('th').eq(idx).removeClass('hidden');
                tr_input_piso.children('td').eq(idx).removeClass('hidden');
                tbody_val_piso.children('tr').each(function () {
                    $(this).children('td').eq(idx).removeClass('hidden');
                });
                tr_footer_piso.children('td').eq(idx).removeClass('hidden');
            }
            tr_input_piso.children('td').eq(stepNum).children('input').focus().select();
            stateButtons(count_cols, stepNum);
        });

        $('#spn_move_pre').unbind('click').click(function () {
            hideCols(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso);

            stepNum -= CTE_NUM_COL + 1;
            if (stepNum <= CTE_NUM_COL)
                stepNum = 0;
            //$(h_stepnumber).val(stepNum);
            for (var idx = stepNum; idx < stepNum + CTE_NUM_COL + 1; idx++) {
                tr_header_piso.children('th').eq(idx).removeClass('hidden');
                tr_input_piso.children('td').eq(idx).removeClass('hidden');
                tbody_val_piso.children('tr').each(function () {
                    $(this).children('td').eq(idx).removeClass('hidden');
                });
                tr_footer_piso.children('td').eq(idx).removeClass('hidden');
            }
            tr_input_piso.children('td').eq(stepNum).children('input').focus().select();
            stateButtons(count_cols, stepNum);
        });

        $('#spn_move_fir').unbind('click').click(function () {
            hideCols(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso);
            stepNum = 0;
            for (var idx = 0; idx <= CTE_NUM_COL; idx++) {
                tr_header_piso.children('th').eq(idx).removeClass('hidden');
                tr_input_piso.children('td').eq(idx).removeClass('hidden');
                tbody_val_piso.children('tr').each(function () {
                    $(this).children('td').eq(idx).removeClass('hidden');
                });
                tr_footer_piso.children('td').eq(idx).removeClass('hidden');
            }
            tr_input_piso.children('td').eq(0).children('input').focus().select();
            stateButtons(count_cols, 0);
        });

        //        tr_header_piso.children('th').eq(num_col - 1).removeClass('hidden');
        //        tr_header_piso.children('th').eq(num_col + CTE_NUM_COL).addClass('hidden');

        //        tr_input_piso.children('td').eq(num_col - 1).removeClass('hidden');
        //        tr_input_piso.children('td').eq(num_col + CTE_NUM_COL).addClass('hidden');
        //        tr_input_piso.children('td').eq(num_col - 1).children('input').focus().select();

        //        tbody_val_piso.children('tr').each(function () {
        //            $(this).children('td').eq(num_col - 1).removeClass('hidden');
        //            $(this).children('td').eq(num_col + CTE_NUM_COL).addClass('hidden');
        //        });

        //        tr_footer_piso.children('td').eq(num_col - 1).removeClass('hidden');
    }

    function hideCols(count_cols, tr_header_piso, tr_input_piso, tbody_val_piso, tr_footer_piso) {
        for (var idx = 0; idx <= count_cols; idx++) {
            tr_header_piso.children('th').eq(idx).addClass('hidden');
            tr_input_piso.children('td').eq(idx).addClass('hidden');
            tbody_val_piso.children('tr').each(function () {
                $(this).children('td').eq(idx).addClass('hidden');
            });
            tr_footer_piso.children('td').eq(idx).addClass('hidden');
        }
    }

    function clearInputsInventory() {

        $("#vendor").html('');
        $("#mercancia").html('');
        $('#idEntInv').val('0');
        $('#ctl00_body_hf_codigo_mercancia').val('');
        $('#solicitud').val('');
        $('#lote').val('');
        $('#ctl00_body_bultos').val('');
        $('#ctl00_body_piezasxbulto').val('');
        $('#ctl00_body_piezasrecibidas').val('');
        $('#ctl00_body_bultosrecibidos').val('');
        $('#bultosxpallet').val('');
        $('#pallets').val('');
        $('#piezasfaltantes').val('');
        $('#piezassobrantes').val('');
        $('#bultosfaltantes').val('');
        $('#bultossobrantes').val('');
        $('#ctl00_body_fechamaquila').val('').datepicker({
            'dateFormat': 'yy-mm-dd'
        });
        $('#notas').val('');

        $('#ctl00_body_btnSaveCodigo').hide();

        $('#th_totLote').html('0');
        $('#ctl00_body_txt_sumLotes').val('0');
    }
}

var master = new webApp.Master;
var pag = new MngInventario();
master.Init(pag);