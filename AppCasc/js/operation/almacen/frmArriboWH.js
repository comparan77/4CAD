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
    }

    function recall(obj, oCtrlCM) {
        if (obj.Id > 0) {
            $('#ctl00_body_txt_mercancia_desc').val(obj.Nombre);
        }
        else {
            oCrtlCM.OpenFrm(obj.Codigo, pag);
        }
    }
}

var master = new webApp.Master;
var pag = new MngArriboWH();
master.Init(pag);