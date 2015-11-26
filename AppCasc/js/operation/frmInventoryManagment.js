//var oCrtlCM;
var lstCodigoOrden = [];

var BeanCodigoOrden = function (id, codigo, orden) {
    this.Id = id;
    this.Orden = orden;
    this.Codigo = codigo;
}

var BeanEntInvCambios = function (id_entrada_inventario, codigo, orden, vendor, observaciones) {
    this.Id = 0;
    this.Id_entrada_inventario = id_entrada_inventario;
    this.Id_usuario = 0;
    this.Codigo = codigo;
    this.Orden = orden;
    this.Vendor = vendor;
    this.Observaciones = observaciones;
}

var MngInventoryManagment = function () {
    this.Init = init;
    //    this.inventarioChange = inventarioChange;
    //    this.Id_entrada_inventario = 0;
    this.P_entrada_inventario_cambios = null;
    this.CtrlCM = null;
    this.CtrlCV = null;
    this.OCtrSelected = null;
    this.Recall = recall;
    this.TipoCambio = null;

    function init() {
        //$("html, body").animate({ scrollTop: $(document).height() }, "slow");
        initControls();
    }

    function recall(oCM, oCtrlCM) {
        //var o = new BeanEntInvCambios(pag.Id_entrada_inventario, oCM.Codigo, '', '');
        inventarioChange(pag.P_entrada_inventario_cambios, oCtrlCM, pag.OCtrSelected, pag.TipoCambio);
    }

    function inventarioChange(oEntInvCambios, oCrtlCM, trSelected, tipo_cambio) {

        var urlHandler = '';
        if (tipo_cambio == 'cod')
            urlHandler = '/handlers/Operation.ashx?op=inventoryCodigo';

        if (tipo_cambio == 'ord')
            urlHandler = '/handlers/Operation.ashx?op=inventoryOrden';

        if (tipo_cambio == 'ven')
            urlHandler = '/handlers/Operation.ashx?op=inventoryVendor';

        $.ajax({
            type: "POST",
            url: urlHandler,
            //data: id_entrada_inventario,
            data: JSON.stringify(oEntInvCambios),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {

                if (tipo_cambio == 'cod') {

                    var n = data.indexOf("existe");
                    if (n > 0) {
                        if (confirm(data + '. Desea dar de alta la nueva mercancía?')) {
                            pag.P_entrada_inventario_cambios = oEntInvCambios;
                            pag.CtrlCM = oCrtlCM;
                            pag.OCtrSelected = trSelected;
                            pag.TipoCambio = tipo_cambio;
                            oCrtlCM.OpenFrm(oEntInvCambios.Codigo, pag);
                        }
                    }
                    else {
                        $('#txt_mer_nombre').val(data[0].Nombre);
                        $(trSelected).children('td').first().html(oEntInvCambios.Codigo);
                        $('#txt_mer_cod').val(oEntInvCambios.Codigo);
                        $('#spn_close_cod').trigger('click');
                        alert('El código ha sido cambiado exitosamente');
                    }
                }

                if (tipo_cambio == 'ord') {
                    $(trSelected).children('td:nth-child(2)').html(oEntInvCambios.Orden);
                    $('#txt_mer_ord').val(oEntInvCambios.Orden);
                    $('#spn_close_ord').trigger('click');
                    alert('La orden de compra ha sido cambiado exitosamente');
                }

                if (tipo_cambio == 'ven') {

                    var n = data.indexOf("existe");
                    if (n > 0) {
                        if (confirm(data + '. Desea dar de alta el nuevo vendor?')) {
                            pag.P_entrada_inventario_cambios = oEntInvCambios;
                            var oCtrlCV = new ctrlClienteVendor();
                            pag.CtrlCV = oCtrlCV;
                            pag.OCtrSelected = trSelected;
                            pag.TipoCambio = tipo_cambio;
                            oCtrlCV.OpenFrm(oEntInvCambios.Vendor, pag);
                        }
                    }
                    else {
                        $('#txt_nombre_vendor').val(data[0].Nombre);
                        $(trSelected).children('td').first().html(oEntInvCambios.Vendor);
                        $('#txt_codigo_vendor').val(oEntInvCambios.Vendor);
                        $('#spn_close_vendor').trigger('click');
                        alert('El vendor ha sido cambiado exitosamente');
                    }
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function fondeoGetById(idFondeo, oCrtlCM, trSelected) {

        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx",
            //dataType: "jsonp",
            data: {
                op: 'stockcode',
                key: idFondeo
            },
            complete: function () {
                // $('#div-info-codigo').removeClass('ajaxLoading');
            },
            success: function (data) {

                $('#txt_importador').val(data.Importador);
                $('#txt_factura').val(data.Factura);

                $(data.LstClienteVendor).each(function (i, obj) {
                    $('#txt_codigo_vendor').val(obj.Codigo);
                    $('#txt_nombre_vendor').val(obj.Nombre);
                });

                var oCommon = new Common();
                $('#txt_pieza_declarada').val(oCommon.GetSeparatorComasNumber(data.Piezas));
                $('#txt_val_fact').val(oCommon.GetCurrencyFormat(data.Valorfactura));

                $(data.LstClienteMercancia).each(function (i, obj) {
                    $('#txt_mer_nombre').val(obj.Nombre);
                });

                $('#txt_mer_cod').val(data.PEntInv.Codigo);
                $('#txt_mer_ord').val(data.PEntInv.Orden_compra);

                //codigo
                $('#spn_edit_codigo').attr('id_entrada_inventario', data.PEntInv.Id);
                $('#spn_edit_codigo').unbind('click').click(function () {
                    if ($(this).hasClass('ui-icon-pencil')) {
                        $(this).removeClass('ui-icon-pencil').removeClass('ui-icon');
                        $('#div_udt_codigo').removeClass('hidden');

                        var btn_save_code = $('#btn_save_code');
                        $(btn_save_code).button().unbind('click').click(function () {
                            var oEIC = new BeanEntInvCambios($('#spn_edit_codigo').attr('id_entrada_inventario'), $('#txt_new_code').val(), '', '', $('#txt_obs_code').val());
                            if (cambioCodigoValido()) {
                                inventarioChange(oEIC, oCrtlCM, trSelected, 'cod');
                            }
                            return false;
                        });
                    }
                });

                //orden de compra
                $('#spn_edit_orden').attr('id_entrada_inventario', data.PEntInv.Id);
                $('#spn_edit_orden').unbind('click').click(function () {
                    if ($(this).hasClass('ui-icon-pencil')) {
                        $(this).removeClass('ui-icon-pencil').removeClass('ui-icon');
                        $('#div_udt_orden').removeClass('hidden');

                        var btn_save_orden = $('#btn_save_orden');
                        $(btn_save_orden).button().unbind('click').click(function () {
                            var oEIC = new BeanEntInvCambios($('#spn_edit_orden').attr('id_entrada_inventario'), '', $('#txt_new_orden').val(), '', $('#txt_obs_orden').val());
                            if (cambioOrdenValido()) {
                                inventarioChange(oEIC, oCrtlCM, trSelected, 'ord');
                                //La orden de compra cambia directo
                            }
                            return false;
                        });
                    }
                });

                //vendor
                $('#spn_edit_vendor').attr('id_entrada_inventario', data.PEntInv.Id);
                $('#spn_edit_vendor').unbind('click').click(function () {
                    if ($(this).hasClass('ui-icon-pencil')) {
                        $(this).removeClass('ui-icon-pencil').removeClass('ui-icon');
                        $('#div_udt_vendor').removeClass('hidden');

                        var btn_save_orden = $('#btn_save_vendor');
                        $(btn_save_orden).button().unbind('click').click(function () {
                            var oEIC = new BeanEntInvCambios($('#spn_edit_vendor').attr('id_entrada_inventario'), '', '', $('#txt_new_vendor').val(), $('#txt_obs_orden').val());
                            if (cambioVendorValido()) {
                                inventarioChange(oEIC, oCrtlCM, trSelected, 'ven');
                                //La orden de compra cambia directo
                            }
                            return false;
                        });
                    }
                });

            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }

    function fill_Cod_OC(oCrtlCM) {

        $('#tbdy_oc_cod').html('');

        $.each(lstCodigoOrden, function (i, o) {
            var tr = '<tr id="fondeo_' + o.Id_entrada_fondeo + '">';
            tr += '<td>' + o.Codigo + '</td>';
            tr += '<td>' + o.Orden_compra + '</td>';
            tr += '<td align="center"><span class="ui-icon ui-icon-arrowthick-1-e icon-button-action"></span></td>';
            tr += '</tr>';
            $('#tbdy_oc_cod').append(tr);
        });

        $('#tbdy_oc_cod').children('tr').each(function () {
            $(this).unbind('click').click(function () {
                //alert($(this).attr('id'));
                var idFondeo = $(this).attr('id').split('_')[1] * 1;
                fondeoGetById(idFondeo, oCrtlCM, $(this));
            });
        });
    }

    function load_Inf_Gral(referencia) {

        $('#txt_referencia').val(referencia);

    }

    function load_Cod_OC(referencia, oCrtlCM) {

        if (lstCodigoOrden.length == 0) {

            $.ajax({
                type: 'GET',
                url: "/handlers/Operation.ashx",
                //dataType: "jsonp",
                data: {
                    op: 'fondeoCodigoOrden',
                    key: referencia
                },
                complete: function () {
                    //$('#div-info-codigo').removeClass('ajaxLoading');
                },
                success: function (data) {

                    $.each(data, function (i, o) {
                        lstCodigoOrden.push(o);
                    });
                    fill_Cod_OC(oCrtlCM);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    var oErrorMessage = new ErrorMessage();
                    oErrorMessage.SetError(jqXHR.responseText);
                    oErrorMessage.Init();
                }
            });
        }
        else
            fill_Cod_OC(oCrtlCM);

    }

    function clearControls() {
        $('.txtClear').each(function () {
            $(this).val('');
        });

        //        $('#spn_edit_codigo').removeClass('ui-icon-save').addClass('ui-icon-pencil').next().attr('readonly', 'readonly').next().addClass('txtNoBorder').attr('title', 'Cambiar código');
        $('#txt_mer_cod').trigger('click');
        $('#txt_mer_ord').trigger('click');
    }

    function initControls() {

        var tabs = $('#tabs');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var div_busqueda = $('#div_busqueda');
        var up_resultados = $('#ctl00_body_up_resultados');
        var div_search = $('#div-search');
        var h_referencia = $('#h_referencia');

        oCrtlCM = new ctrlClienteMercancia();

        $(tabs).tabs({
            activate: function (event, ui) {
                switch (ui.newPanel.attr('id')) {
                    case 'tabs-1':
                        load_Inf_Gral(h_referencia.val());
                        break;
                    case 'tabs-2':
                        load_Cod_OC(h_referencia.val(), oCrtlCM);
                        break;
                }
            }
        });


        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
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

        $(up_resultados).panelReady(function () {
            $(div_busqueda).removeClass('ajaxLoading');

            $('.referencias').click(function () {
                div_search.next().hide('slow');
                lstCodigoOrden = [];
                h_referencia.val($(this).html());
                $(tabs).tabs("option", "active", 0);
                clearControls();
                load_Inf_Gral($(this).html());
                return false;
            });
        });

        $('#spn_close_cod').click(function () {
            $('#spn_edit_codigo').addClass('ui-icon-pencil').addClass('ui-icon');
            $('#div_udt_codigo').addClass('hidden');
        });

        $('#spn_close_ord').click(function () {
            $('#spn_edit_orden').addClass('ui-icon-pencil').addClass('ui-icon');
            $('#div_udt_orden').addClass('hidden');
        });

        $('#spn_close_vendor').click(function () {
            $('#spn_edit_vendor').addClass('ui-icon-pencil').addClass('ui-icon');
            $('#div_udt_vendor').addClass('hidden');
        });
    }

    function cambioCodigoValido() {

        if ($('#txt_new_code').val().trim().length == 0) {
            alert('El nuevo código no puede estar vacío');
            $('#txt_new_code').focus();
            return false;
        }

        if ($('#txt_mer_cod').val() == $('#txt_new_code').val()) {
            alert('El nuevo código no puede ser el mismo que el anterior');
            $('#txt_new_code').focus();
            return false;
        }

        if ($('#txt_obs_code').val().trim().length == 0) {
            alert('El motivo no puede estar vacío');
            $('#txt_obs_code').focus();
            return false;
        }

        return true;
    }

    function cambioOrdenValido() {

        if ($('#txt_new_orden').val().trim().length == 0) {
            alert('La nueva orden de compra no puede estar vacía');
            $('#txt_new_orden').focus();
            return false;
        }

        if ($('#txt_mer_ord').val() == $('#txt_new_orden').val()) {
            alert('La nueva orden de compra no puede ser la mismo que la anterior');
            $('#txt_new_orden').focus();
            return false;
        }

        if ($('#txt_obs_orden').val().trim().length == 0) {
            alert('El motivo no puede estar vacío');
            $('#txt_obs_orden').focus();
            return false;
        }

        return true;

    }

    function cambioVendorValido() {

        if ($('#txt_new_vendor').val().trim().length == 0) {
            alert('El nuevo código de vendor no puede estar vacía');
            $('#txt_new_vendor').focus();
            return false;
        }

        if ($('#txt_codigo_vendor').val() == $('#txt_new_vendor').val()) {
            alert('El nuevo código de vendor, no puede ser el mismo que el anterior');
            $('#txt_new_vendor').focus();
            return false;
        }

        if ($('#txt_obs_vendor').val().trim().length == 0) {
            alert('El motivo no puede estar vacío');
            $('#txt_obs_vendor').focus();
            return false;
        }

        return true;

    }
}

var master = new webApp.Master;
var pag = new MngInventoryManagment();
master.Init(pag);