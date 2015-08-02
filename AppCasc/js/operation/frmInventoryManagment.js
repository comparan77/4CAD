//var oCrtlCM;
var lstCodigoOrden = [];

var BeanCodigoOrden = function (id, codigo, orden) {
    this.Id = id;
    this.Orden = orden;
    this.Codigo = codigo;
}

var MngInventoryManagment = function () {
    this.Init = init;
    this.InventarioChangeCodigo = inventarioChangeCodigo;
    this.Id_entrada_inventario = 0;
    this.CtrlCM = null;
    this.OCtrSelected = null;
    this.Recall = recall;

    function init() {
        //$("html, body").animate({ scrollTop: $(document).height() }, "slow");
        initControls();
    }

    function recall(oCM, oCtrlCM) {
        inventarioChangeCodigo(pag.Id_entrada_inventario, oCM.Codigo, oCtrlCM, pag.OCtrSelected);
    }

    function inventarioChangeCodigo(id_entrada_inventario, codigo, oCrtlCM, trSelected) {

        $.ajax({
            type: "POST",
            url: '/handlers/Operation.ashx?op=inventoryCodigo&codigo=' + codigo + '&key=' + id_entrada_inventario,
            //data: id_entrada_inventario,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                var n = data.indexOf("existe");
                if (n > 0) {
                    if (confirm(data + '. Desea dar de alta la nueva mercancía?')) {
                        pag.Id_entrada_inventario = id_entrada_inventario;
                        pag.CtrlCM = oCrtlCM;
                        pag.OCtrSelected = trSelected;
                        oCrtlCM.OpenFrm(codigo, pag);
                    }
                }
                else {
                    $('#txt_mer_nombre').val(data[0].Nombre);
                    $(trSelected).children('td').first().html(codigo);
                    alert('El código ha sido cambiado exitosamente');
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
                $('#orden').val(data.PEntInv.Orden_compra);

                $('#spn_edit_codigo').attr('id_entrada_inventario', data.PEntInv.Id);

                $('#spn_edit_codigo').unbind('click').click(function () {
                    if ($(this).hasClass('ui-icon-pencil')) {
                        $(this).removeClass('ui-icon-pencil')
                        $(this).addClass('ui-icon-disk')
                        $(this).next().removeAttr('readonly');
                        $(this).next().removeClass('txtNoBorder');
                        $(this).attr('title', 'Guardar cambio');
                    }
                    else {

                        inventarioChangeCodigo($('#spn_edit_codigo').attr('id_entrada_inventario'), $(this).next().val(), oCrtlCM, trSelected);

                        $(this).removeClass('ui-icon-disk')
                        $(this).addClass('ui-icon-pencil')
                        $(this).next().attr('readonly', 'readonly');
                        $(this).next().addClass('txtNoBorder');
                        $(this).attr('title', 'Cambiar código');
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

        $('#spn_edit_codigo').removeClass('ui-icon-save').addClass('ui-icon-pencil').next().attr('readonly', 'readonly').next().addClass('txtNoBorder').attr('title', 'Cambiar código');

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
    }

}

var master = new webApp.Master;
var pag = new MngInventoryManagment();
master.Init(pag);