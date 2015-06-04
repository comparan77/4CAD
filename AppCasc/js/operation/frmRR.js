var MngRR = function () {

    this.Init = init;

    //Init <<ini>>
    function init() {

        var div_search = $('#div-search');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var div_busqueda = $('#div_busqueda');
        var up_resultados = $('#ctl00_body_up_resultados');

        //$('#ctl00_body_txt_dato').mask('9999-99');

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
        });

        $(up_resultados).panelReady(function () {

            $("html, body").animate({ scrollTop: $(document).height() }, "slow");

            $(div_busqueda).removeClass('ajaxLoading');

            $('#ctl00_body_txt_fecha_rr').datepicker({
                'dateFormat': 'dd/mm/yy'
            })

            var up_saveData = $('#ctl00_body_up_saveData');
            $(up_saveData).panelReady(function () {
                var btn_guardar = $('#ctl00_body_btn_guardar');
                $(btn_guardar).button().click(function () {

                    var IsValid = true;

                    $('.validator').each(function () {
                        if ($(this).css('visibility') == 'visible') {
                            //                            $('html,body').animate({
                            //                                scrollTop: $(this).offset().top
                            //                            }, 2000);
                            IsValid = false;
                        }
                    });
                    if (IsValid) {
                        $(up_saveData).addClass('ajaxLoading');
                        alert('El registro se ha guardado correctamente');
                    }
                });
                $(up_saveData).removeClass('ajaxLoading');
            });
        });
        //up_resultados <<fin>>
    }
}

var master = new webApp.Master;
var pag = new MngRR();
master.Init(pag);