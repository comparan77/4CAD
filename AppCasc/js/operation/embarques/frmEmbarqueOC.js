var lstDoc = [];
var lstCondTran = [];
var arrCondTran = [];

var BeanSalidaDocumento = function (id_documento, referencia) {
    this.Id = 0;
    this.Id_salida = 0;
    this.Id_documento = id_documento;
    this.Referencia = referencia;
}

var BeanSalidaTransporteCondicion = function (id_transporte_condicion, si_no) {
    this.Id = 0;
    this.Id_salida_orden_carga = 0;
    this.Id_transporte_condicion = id_transporte_condicion;
    this.Si_no = si_no;
}

var MngEmbarqueOC = function () {
    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        $('#ctl00_body_txt_hora_salida, #ctl00_body_txt_hora_carga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });

        $('#ctl00_body_btnGuardar').button().click(function () {
            var IsValid = true;

            validaReferencias();

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
                fillJsonDocByReferencia();
                $(this).hide();
            }
        });

        var up_Rem = $('#ctl00_body_up_Rem');
        $(up_Rem).panelReady(function () {
            $('.add_doc').each(function () {
                $(this).button().unbind('click').click(function () {

                    var idTipoDoc;
                    var tipoDoc;
                    var selectDoc;
                    var referenciaDoc;

                    referenciaDoc = $(this).parent().parent().children('div:nth-child(2)').children('input').val();

                    selectDoc = $(this).parent().parent().children('div').first().children('select');
                    idTipoDoc = selectDoc.val();
                    tipoDoc = selectDoc.find(":selected").text();

                    if (referenciaDoc.length <= 0) {
                        alert('La referencia del documento ' + tipoDoc + ' no puede estar vacía');
                        return false;
                    }

                    //Verifica que no se repita el tipo de documento
                    var existe = false;
                    $(this).parent().next().children('ul').children('li').each(function () {
                        if ($(this).attr('iddoc') == idTipoDoc) {
                            existe = true;
                            return false;
                        }
                    });
                    if (!existe)
                        $(this).parent().next().children('ul').append('<li iddoc="' + idTipoDoc + '" style="clear: left"><span style="float: left; display: block;">' + tipoDoc + ':</span>&nbsp;<span style="float: left; display: block;">' + referenciaDoc + '</span><span style="float: left; display: block;" class="ui-icon ui-icon-trash icon-button-action removeDoc"></span></li>');

                    $('.removeDoc').each(function () {
                        $(this).click(function () {
                            $(this).parent().remove();
                        });
                    });
                });
            });
        });

        loadTransporteCondicion(1, false, true);
    }

    function validaReferencias() {
        $('.revReferencia').each(function () {
            var txt_pallet = $(this).children('td:nth-child(5)').children('input');
            var txt_mercancia = $(this).children('td:nth-child(6)').children('textarea');

            //            $(txt_pallet).next().next().css('visibility', 'hidden');
            //            if (txt_pallet.val().length == 0)
            //                $(txt_pallet).next().next().css('visibility', 'visible');

            $(txt_mercancia).next().next().css('visibility', 'hidden');
            if (txt_mercancia.val().length == 0)
                $(txt_mercancia).next().next().css('visibility', 'visible');
        });
    }

    function fillJsonDocByReferencia() {
        $('.revReferencia').each(function () {
            var ulDocs = $(this).children('td:nth-child(2)').children('div:nth-child(4)').children('ul');
            lstDoc = [];
            $(ulDocs).children('li').each(function () {

                var idTipoDoc = $(this).attr('iddoc');
                var tipoDoc = $(this).children('span').first().html();
                var referenciaDoc = $(this).children('span:nth-child(2)').html();

                var oSalDoc = new BeanSalidaDocumento(idTipoDoc, referenciaDoc);
                var arrDocEx = $.grep(lstDoc, function (obj) {
                    return obj.Id_documento == idTipoDoc;
                });

                if (arrDocEx.length == 0)
                    lstDoc.push(oSalDoc);
            });
            var hfJsonDoc = $(this).children('td:nth-child(2)').children('div:nth-child(4)').children('input');
            hfJsonDoc.val(JSON.stringify(lstDoc));
        });
    }

    function clearControls() {
        $('#ul_result_oc').html('');
        $('#tbody_remisiones').html('');
    }

    function fillResultSearchOC(data) {
        var ul_result_oc = $('#ul_result_oc');
        $.each(data, function (i, o) {
            var li = '<li>';
            li += '<a href="#" id="ocKey_' + o.Id + '" class="liOc">' + o.Folio_orden_carga + '</a>';
            li += '</li>';
            ul_result_oc.append(li);
        });

        $('.liOc').each(function () {
            $(this).unbind('click').click(function () {
                var idOC = $(this).attr('id').split('_')[1];
                //alert(idOC);
                loadOCById(idOC);
                return false;
            });
        });
    }

    function loadOC(folio) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=ordenCarga&opt=getByFolio",
            //dataType: "jsonp",
            data: {
                folio: folio
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                clearControls();
                fillResultSearchOC(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function fillRemisiones(data) {

        var tbody_remisiones = $('#tbody_remisiones');
        var tr;
        var td;
        $.each(data.LstRem, function (i, o) {
            tr = '<tr>';

            td = '<td align="center">';
            td += o.PSalRem.Referencia;
            td += '</td>';
            tr += td;

            td = '<td>';
            td += '<div><label>Tipo de documento:</label><select></select></div>';
            td += '<div><label>Referencia del documento:</label><input type="text" /></div>';
            td += '<div><input type="button" value="Agregar"/></div>';
            td += '<div>';
            td += '<ul id="ulDocCap_' + o.PSalRem.Id + '">Documentos Capturados';
            td += '<li id="liDoc_' + o.PSalRem.Id + '_3">Remisión->' + o.PSalRem.Folio_remision + '</li>';
            td += '</ul></div>';
            td += '</td>';
            tr += td;

            tr += '</tr>';
            tbody_remisiones.append(tr);
        });
    }

    function loadOCById(id_orden_carga) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=ordenCarga&opt=getById",
            //dataType: "jsonp",
            data: {
                id_orden_carga: id_orden_carga
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                clearControls();
                fillRemisiones(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function fillCondicionesTransporte() {
        $('#tbody_condiciones').html('');
        var tr;
        var td;
        var ind = 1;
        var categoria = "";
        for (var itemCT in arrCondTran) {
            if (categoria != arrCondTran[itemCT].PTransCondCat.Nombre) {
                categoria = arrCondTran[itemCT].PTransCondCat.Nombre;
                tr = '<tr class="header_cat_cond"><td style="font-weight: bold;">'
                    + categoria
                    + '</td><td style="font-weight: bold;">Sí</td><td style="font-weight: bold;">No</td></tr>';
                $('#tbody_condiciones').append(tr);
            }
            tr = '<tr id="condTr_' + arrCondTran[itemCT].Id + '">';
            td = '<td>';
            td += arrCondTran[itemCT].Nombre;
            td += '</td>';
            tr += td;
            td = '<td><input name="name_' + arrCondTran[itemCT].Id + '" type="radio" checked="checked" value="1" /></td>';
            tr += td;
            td = '<td><input name="name_' + arrCondTran[itemCT].Id + '" type="radio" value="0" /></td>';
            tr += td;
            $('#tbody_condiciones').append(tr);
            ind++;
        }
    }

    function loadTransporteCondicion(id_cliente, es_entrada, es_salida) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=transCond&opt=condCli",
            //dataType: "jsonp",
            data: {
                id_cliente: id_cliente,
                es_entrada: es_entrada,
                es_salida: es_salida
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                $.each(data.PLstTransporte_condicion, function (i, o) {
                    arrCondTran.push(o);
                });
                fillCondicionesTransporte();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    //Condiciones del transporte
    function condicionesTransporteSet() {
        lstCondTran = [];
        $('#tbody_condiciones').children('tr').each(function () {
            if (!$(this).hasClass('header_cat_cond')) {
                var id = $(this).attr('id').split('_')[1] * 1;
                var val = $('input[name="name_' + id + '"]:checked', '#tbody_condiciones').val();
                
                if (val != undefined) {
                    val = val == 1 ? true : false;
                    var o = new BeanSalidaTransporteCondicion(id, val);
                    lstCondTran.push(o);
                }
            }
        });
        $('#ctl00_body_hf_condiciones_transporte').val(JSON.stringify(lstCondTran));
    }

    //valida condiciones transporte
    function validaCondTransporte() {
        $('#rfv_condiciones_transporte').css('visibility', lstCondTran.length == arrCondTran.length ? 'hidden' : 'visible');
    }


}

var master = new webApp.Master;
var pag = new MngEmbarqueOC();
master.Init(pag);