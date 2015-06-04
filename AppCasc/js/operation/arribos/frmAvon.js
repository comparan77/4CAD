var lstDoc = [];
var lstEntComp = [];
var lstEntTran = [];
var idEntradaTransporte = 0;
var BeanEntradaDocumento = function (id_documento, Pdocumento, referencia) {
    this.Id = 0;
    this.Id_entrada = 0;
    this.Id_documento = id_documento;
    this.Referencia = referencia;
    this.PDocumento = Pdocumento;
}
var BeanDocumento = function (id_documento, nombre) {
    this.Id = id_documento;
    this.Nombre = nombre;
    this.Mascara = '';
    this.IsActive = true;
}
var BeanEntradaCompartido = function (id_usuario, referencia) {
    this.Id = 0;
    this.Id_entrada = null;
    this.Id_usuario = id_usuario;
    this.Folio = '';
    this.Referencia = referencia;
    this.Capturada = false;
    this.IsActive = true;
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

var MngAvon = function () {

    this.Init = init;

    function init() {
        $('#ctl00_body_btn_save').button();
        $('#ctl00_body_btn_buscar').button();
        $('#ctl00_body_txt_dato, #txt_pedimento_compartido').mask('99-9999-9999999');
        $('#ctl00_body_txt_hora_llegada, #ctl00_body_txt_hora_descarga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });
        $('#btn_add_documento').click(function () {
            addDocumento();
            return false;
        });

        $('#btn_add_compartido').click(function () {
            validaCompartido();
            return false;
        });

        validaTipoTransporte();
        $('#ctl00_body_ddlTipo_Transporte').change(function () {
            validaTipoTransporte();
        }).trigger('change');

        $('#btn_add_tipoTransporte').click(function () {
            addTransporte();
            verificaTranportes();
            return false;
        });

        $('#btn_show_cantidadesProblema').button().click(function () {
            $(this).hide();
            $('#cantidadesProblema').show('slow');
            return false;
        });

        $('.rbTipoEntrada').buttonset().children('input:radio').each(function () {
            verificaTipoEntrada($(this));
            $(this).change(function () {
                verificaTipoEntrada($(this));
            });
        });

        $('#ctl00_body_btn_save').click(function () {
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
                var confirm = $('#hf-confirmado').val() * 1;
                if (confirm == 0) {
                    var mensaje = '';
                    mensaje = '<b>Tipo:</b> ' + $('#spnTipoEntrada').html();
                    if ($('#ctl00_body_chk_ultima').is(':checked') == true) {
                        mensaje += ', <b>Última entrada.</b>';
                    }
                    mensaje += '<br />';
                    mensaje += '<ul>';
                    $(lstEntTran).each(function (i, itemET) {
                        mensaje += '<li>';
                        mensaje += itemET.Transporte_linea + ', ' + itemET.Transporte_tipo + ', Placa: ' + itemET.Placa + ', Caja: ' + itemET.Caja + ', Cont1: ' + itemET.Caja1 + ', Cont2: ' + itemET.Caja2;
                        mensaje += '</li>';
                    });
                    mensaje += '</ul>';
                    mensaje += '<hr />';
                    mensaje += '<b>Compartida:</b> ' + (lstEntComp.length > 0 ? 'Sí' : 'No');
                    $('#spn-aviso-registro').html(mensaje);
                    $("#dialog-confirm").dialog('open');
                    return false;
                }
                else
                    $(this).hide();
            }
        });

        $("#dialog-confirm").dialog({
            autoOpen: false,
            resizable: false,
            height: 250,
            width: 450,
            modal: true,
            buttons: {
                "Guardar Entrada": function () {
                    $(this).dialog("close");
                    $('#hf-confirmado').val(1);
                    var clickButton = document.getElementById('ctl00_body_btn_save');
                    clickButton.click();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

    } // init <<fin>>

    function addDocumento() {
        var idDocumento = $('#ctl00_body_ddlDocumento').val();
        var tipoDocumento = $('#ctl00_body_ddlDocumento>option:selected').text();
        var referencia = $('#txt_documento').val();
        var hf_entradaDocumento = $('#ctl00_body_hf_entradaDocumento');

        if (referencia.length < 1)
            return false;

        var oDoc = new BeanDocumento(idDocumento, tipoDocumento);
        var oEntDoc = new BeanEntradaDocumento(idDocumento, oDoc, referencia);
        var arrDocEx = $.grep(lstDoc, function (obj) {
            return obj.Id_documento == idDocumento;
        });
        if (arrDocEx.length == 0)
            lstDoc.push(oEntDoc);
        var tr = '';
        $('#tbody_documentos').html('');
        for (var iDoc in lstDoc) {
            tr = '<tr id="' + lstDoc[iDoc].Id_documento + '">';
            var td = '<td>' + lstDoc[iDoc].PDocumento.Nombre + '</td>';
            td += '<td>' + lstDoc[iDoc].Referencia + '</td>';
            td += '<td><button class="rem_documento" id="btn_rem_documento"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tbody_documentos').append(tr);
        }
        $(hf_entradaDocumento).val(JSON.stringify(lstDoc));
        $('.rem_documento').each(function () {
            $(this).click(function () {
                //alert($(this).parent().parent().attr('id'));
                var Id_Documento = $(this).parent().parent().attr('id');
                lstDoc = $.grep(lstDoc, function (obj) {
                    return obj.Id_documento != Id_Documento;
                });
                $(this).parent().parent().remove();
                $(hf_entradaDocumento).val(JSON.stringify(lstDoc));
                return false;
            });
        });
    } // fin addDocumento

    function addCompartido() {
        var idusuario = $('#ctl00_body_hf_id_usuario').val();
        var referencia = $('#txt_pedimento_compartido').val();
        var hf_arribo_compartido = $('#ctl00_body_hf_arribo_compartido');
        var pedimento = $('#ctl00_body_txt_referencia').val();

        if (referencia == pedimento) {
            alert('El pedimento proporcionado, es el principal de este arribo');
            return false;
        }

        var oEntComp = new BeanEntradaCompartido(idusuario, referencia);
        var arrEntComp = $.grep(lstEntComp, function (obj) {
            return obj.Referencia == referencia;
        });
        if (arrEntComp.length == 0)
            lstEntComp.push(oEntComp);
        var tr = '';
        $('#tbody_pedimentos').html('');
        for (var iEntComp in lstEntComp) {
            tr = '<tr id="' + lstEntComp[iEntComp].Referencia + '">';
            var td = '<td>' + lstEntComp[iEntComp].Referencia + '</td>';
            td += '<td><button class="rem_pedimento" id="btn_rem_pedimento"><span class="ui-icon ui-icon-trash"></button></td>';
            tr += td;
            tr += '</tr>';
            $('#tbody_pedimentos').append(tr);
        }
        $(hf_arribo_compartido).val(JSON.stringify(lstEntComp));
        $('.rem_pedimento').each(function () {
            $(this).click(function () {
                var refer = $(this).parent().parent().attr('id');
                lstEntComp = $.grep(lstEntComp, function (obj) {
                    return obj.Referencia != refer;
                });
                $(this).parent().parent().remove();
                $(hf_arribo_compartido).val(JSON.stringify(lstEntComp));
                return false;
            });
        });
    } // fin addCompartido

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
    // valida tipo transporte <<fin>>

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

    function verificaTipoEntrada(radiobtn) {
        $('#divParcialidad').addClass('hidden');
        $('#spnNoEntradaParcial').html('');
        if ($(radiobtn).is(':checked')) {
            switch ($(radiobtn).val()) {
                case 'rb_parcial':
                    $('#spnTipoEntrada').html('Se ha seleccionado entrada Parcial');
                    $('#spnNoEntradaParcial').html('No. Entrada: ' + $('#ctl00_body_hf_no_entrada_parcial').val());
                    $('#divParcialidad').removeClass('hidden');
                    break;
                case 'rb_unica':
                    $('#spnTipoEntrada').html('Se ha seleccionado entrada Única');
                    break;
            }
        }
    }

    function verificaTranportes() {
        $('#rfv_entradaTransporte').css('visibility', lstEntTran.length == 0 ? 'visible' : 'hidden');
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

    function validaCompartido() {
        var referencia = $('#txt_pedimento_compartido').val();
        if (referencia.length < 1)
            return false;

        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx",
            //dataType: "jsonp",
            data: {
                op: 'arribo',
                ref: referencia
            },
            success: function (data) {
                if (data.toString() == 'true')
                    addCompartido();
                else {
                    var oErrorMessage = new ErrorMessage();
                    oErrorMessage.SetError(data);
                    oErrorMessage.Init();
                }
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
var pag = new MngAvon();
master.Init(pag);