var MngMaq = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        //Mascara para el folio
        try {
            $.mask.definitions['u'] = "[1-9]";
            var anioAct = new Date();
            var digAnio = anioAct.getFullYear().toString().substr(2, 2);
            $('#ctl00_body_txt_folio').mask('OT-999999-' + digAnio);
            $('#ctl00_body_up_info_ot').panelReady(function () {

                $('#tabs').tabs();

                var spn_estado_ot = $('#spn_estado_ot');
                $(spn_estado_ot).click(function () {
                    alert('hola');
                });

            });

        } catch (err) {
            alert(err.Message);
        }
    }
}

var master = new webApp.Master;
var pag = new MngMaq();
master.Init(pag);