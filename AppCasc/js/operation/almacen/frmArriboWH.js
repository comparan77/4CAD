var lstEntTran = [];
var lstCondTran = [];
var idEntradaTransporte = 0;
var arrCondTran = ['Paredes Limpias', 'Pisos Limpios', 'Techos Limpios', 'Presencia de grietas, huecos, astillas, agujeros', 'Presencia de plagas en la unidad', 'Presencia de olores extraños', 'Unidad exterior limpia', 'Cuenta el transporte con certificado de fumigación'];

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
        //        var pza_declarada = $('#ctl00_body_txt_no_pieza_declarada');
        //        var pza_recibida = $('#ctl00_body_txt_no_pieza_recibida');
        var pza_x_bulto = $('#ctl00_body_txt_pza_x_bulto');
        var bto_x_pallet = $('#ctl00_body_txt_bto_x_pallet');

        $('.calculaDif').each(function () {
            $(this).blur(function () {
                calculaDif(bto_declarado, bto_recibido, $(pza_x_bulto).val());
                calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val());
            });
        });

        $('.calculaStd').each(function () {
            $(this).blur(function () {
                calculaStd($(pza_x_bulto).val(), $(bto_x_pallet).val(), $(bto_recibido).val());
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
            addTransporte();
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
            condicionesTransporteSet();

            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    $('html,body').animate({
                        scrollTop: $(this).offset().top
                    }, 2000);
                    IsValid = false;
                    return false;
                }
            });

            if (IsValid)
                $(this).hide();

            return IsValid;

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

    function calculaStd(pza_x_bto, bto_x_tarima, btos) {

        if (isNaN(pza_x_bto) || isNaN(bto_x_tarima) || isNaN(btos))
            return false;

        pza_x_bto = pza_x_bto * 1;
        bto_x_tarima = bto_x_tarima * 1;
        pzas = btos * pza_x_bto;
        btos = btos * 1;

        if (pza_x_bto <= 0 || bto_x_tarima <= 0 || btos <= 0)
            return false;

        //var residuoPza = pzas % pza_x_bto;
        var completaBto = Math.floor(pzas / pza_x_bto);

        var residuoBto = btos % bto_x_tarima;
        var completaTar = Math.floor(btos / bto_x_tarima);

        var tbody = $('#tbody_build_tarima');
        $(tbody).html('');
        //Arma tarimas
        var tr = '<tr>';
        var td = '<td>' + completaTar + '</td>';
        td += '<td>Tarima(s)</td>';
        td += '<td>X</td>';
        td += '<td>' + bto_x_tarima + '</td>';
        td += '<td>Caja(s) por Tarima</td>';
        td += '<td>=</td>';
        td += '<td>' + completaTar * bto_x_tarima + '</td>';
        td += '<td>Caja(s)</td>';
        td += '<td>X</td>';
        td += '<td>' + pza_x_bto + '</td>';
        td += '<td>Pza(s) por Caja</td>';
        td += '<td>=</td>';
        td += '<td>' + completaTar * bto_x_tarima * pza_x_bto + '</td>';
        td += '<td>Pieza(s)</td>';
        tr += td;
        tr += '</tr>';
        $(tbody).append(tr);

        //Resto
        if (residuoBto != 0) {
            tr = '<tr>';
            td = '<td>1</td>';
            td += '<td>Tarima</td>';
            td += '<td>X</td>';
            td += '<td>' + residuoBto + '</td>';
            td += '<td>Caja(s) por Tarima</td>';
            td += '<td>=</td>';
            td += '<td>' + residuoBto + '</td>';
            td += '<td>Cajas</td>';
            td += '<td>X</td>';
            td += '<td>' + pza_x_bto + '</td>';
            td += '<td>Pza(s) por Caja</td>';
            td += '<td>=</td>';
            td += '<td>' + residuoBto * pza_x_bto + '</td>';
            td += '<td>Pieza(s)</td>';
            tr += td;
            tr += '</tr>';
            $(tbody).append(tr);
        }
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
        $('#tbody_condiciones').children('tr').each(function () {
            var id = $(this).attr('id').split('_')[1];
            var val = $('input[name="name_' + id + '"]:checked', '#tbody_condiciones').val();
            val = val == 1 ? true : false;
            var o = new BeanEntradaTransporteCondicion(id, val);
            lstCondTran.push(o);
        });
        $('#ctl00_body_hf_condiciones_transporte').val(JSON.stringify(lstCondTran));
    }
}

var master = new webApp.Master;
var pag = new MngArriboWH();
master.Init(pag);