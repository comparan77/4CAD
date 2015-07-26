var oCrtlCM;
var Fondeo = function () {

    this.Init = init;

    function init() {

        $('.txtFecha').each(function () {
            $(this).datepicker({
                dateFormat: 'dd/mm/yy',
                regional: 'es'
            });
        });

        var div_busqueda = $('#div_busqueda');
        var btn_buscar = $('#ctl00_body_btn_buscar');
        var up_resultados = $('#ctl00_body_up_resultados');

        $(btn_buscar).button({
            'icons': {
                'primary': 'ui-icon-search'
            }
        }).click(function () {
            div_busqueda.addClass('ajaxLoading');
        });

        //up_resultados <<ini>>
        $(up_resultados).panelReady(function () {
            $(div_busqueda).removeClass('ajaxLoading');
        });

        $('#spn_Search').click(function () {
            if ($(this).hasClass('ui-icon-search')) {
                $(this).removeClass('ui-icon-search');
                $(this).addClass('ui-icon-close');
                $(div_busqueda).show('slow');
            }
            else {
                $(this).removeClass('ui-icon-close');
                $(this).addClass('ui-icon-search');
                $(div_busqueda).hide('slow');
            }
        });

        var btn_processFile = $('#ctl00_body_btn_processFile').button();
        btn_processFile.click(function () {
            var IsValid = true;
            $('.validator').each(function () {
                if ($(this).css('visibility') == 'visible') {
                    IsValid = false;
                    return false;
                }
            });
            if (IsValid)
                $(this).hide();
        });

        var btn_importar = $('#ctl00_body_btn_importar');

        var up_ImportStep = $('#ctl00_body_up_ImportStep');
        var up_folioDup = $('#ctl00_body_up_folioDup');

        up_ImportStep.panelReady(function () {

            var divActions = $('#divActions');
            divActions.children('input[type="submit"]').each(function () {
                $(this).button();
            });

            btn_processFile = $('#ctl00_body_btn_processFile');

            //removeDup();

            var fu_Folios = $('#ctl00_body_fu_Folios');
            fu_Folios.change(function () {
                changeFileUpload(this);
            });
            if (btn_processFile.length > 0)
                fu_Folios.hide();
            //fu_Folios.tooltip({ position: { my: "left+15 center", at: "right center"} });
            fu_Folios.button();

            //Captura de codigos
            
            oCrtlCM = new ctrlClienteMercancia();
            oCrtlCM.Init();
            capturaCodigoVendor();
        });

        function capturaCodigoVendor() {
            var lbl_NoFoliosMsg = $('#ctl00_body_lbl_NoFoliosMsg');
            var grd_reviewFile = $('#ctl00_body_grd_reviewFile');
            var idx = $(lbl_NoFoliosMsg).html().indexOf('codigos no existentes');
            if (idx > 0) {
                $(grd_reviewFile).children('tbody').children('tr').each(function () {
                    var td = $(this).children('td:eq(6)');
                    var codigo = $(td).html();
                    $(td).html('').append('<a id="lnk_' + codigo + '" class="newCode" href="#">' + codigo + '<span class="ui-icon ui-icon-plus"></span></a>');
                });

                $('.newCode').each(function () {
                    $(this).click(function () {
                        var cod = $(this).attr('id').split('_')[1];
                        oCrtlCM.OpenFrm(cod);
                        return false;
                    });
                });
            }
        }
    }

    //
    function changeFileUpload(fu_folios) {
        //Valida que el archivo no sea mayor a la cantidad de máxima de bytes declarada en el webconfig <httpRuntime maxRequestLength="20000"/>
        try {
            var size = fu_folios.files[0].size;
            var MaxRequestLen = $('#ctl00_body_hf_MaxRequestLen').val() * 1;
            if (size > MaxRequestLen) {
                alert('El archivo es de ' + size / 1048576 + 'MB y sobrepasa el tamaño máximo permitido de ' + MaxRequestLen / 1048576 + 'MB');
                return false;
            }
        }
        catch (err) { }

        if (confirm('Desea importar ' + fu_folios.value + '?')) {
            $(this).hide();
            $('#ctl00_body_btn_importar').trigger('click');
        }
        else
            return false;
    }

}

var master = new webApp.Master;
var pag = new Fondeo();
master.Init(pag);