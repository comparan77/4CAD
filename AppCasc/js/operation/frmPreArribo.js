var MngPreArribo = function () {

    this.Init = init;

    function initControls() {
        $('#ctl00_body_btn_buscar, #ctl00_body_btn_save').button();
        $('#ctl00_body_txt_referencia').mask('99-9999-9999999');
        validaTipoTransporte();
        $('#ctl00_body_ddlTipo_Transporte').change(function () {
            validaTipoTransporte();
        }).trigger('change');
        chk_sello_click();
    }

    function init() {
        initControls();
    }

    // valida tipo transporte <<ini>>
    function validaTipoTransporte() {
        $('#ctl00_body_ddlTipo_Transporte option:selected').each(function () {
            txt_dato_transRequerido($('#ctl00_body_txt_placa'), $(this).attr('placa'));
            txt_dato_transRequerido($('#ctl00_body_txt_caja'), $(this).attr('caja'));
            txt_dato_transRequerido($('#ctl00_body_txt_caja_1'), $(this).attr('caja1'));
            txt_dato_transRequerido($('#ctl00_body_txt_caja_2'), $(this).attr('caja2'));
        });
    }

    function txt_dato_transRequerido(txt, requerido) {
        $(txt).attr('readonly', 'readonly').val('N.A.');
        if (requerido == 'True') {
            $(txt).removeAttr('readonly');
            $(txt).val('');
        }
    }

    function chk_sello_click() {
        $('#chk_sello').click(function () {
            if ($(this).is(':checked')) {
                $(this).parent().children('span').html('Con sello');
                $(this).parent().next().val('');
                $(this).parent().next().removeClass('hidden');
            }
            else {
                $(this).parent().children('span').html('Sin sello');
                $(this).parent().next().val('N.A.');
                $(this).parent().next().addClass('hidden');
                $('#ctl00_body_rfvSello').css('visibility', 'hidden');
            }
        });
    }
}

var master = new webApp.Master;
var pag = new MngPreArribo();
master.Init(pag);