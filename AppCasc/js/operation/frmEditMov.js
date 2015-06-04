$(document).ready(function () {
    var oMng = new MngEditMov();
    oMng.Init();

});


var MngEditMov = function () {

    this.Init = init;

    function init() {

        var btnGuardar = $('#ctl00_body_btnActFolio');

        btnGuardar.button().click(function () {
            var IsValid = true;
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
        });

        $('#ctl00_body_txt_hora_llegada, #ctl00_body_txt_hora_descarga, #ctl00_body_txt_hora_salida, #ctl00_body_txt_hora_carga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });
    }

}