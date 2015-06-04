var RelEntSal = function () {

    this.Init = function () {

        $('input[type="submit"]').each(function () {
            $(this).button();
        });

        $('#ctl00_body_txt_fecha_ini, #ctl00_body_txt_fecha_fin').datepicker({
            'dateFormat': 'dd/mm/yy'
        });

        $('.bodega').hide();
        if ($('#ctl00_body_ddlBodega').val() == '0')
            $('.bodega').show();

        $('.cliente').hide();
        if ($('#ctl00_body_ddlCliente').val() == '0')
            $('.cliente').show();

        var oCatalog = new Catalog();
        oCatalog.CreateGrid($('#grdCatalog'), '');

        $('#msgConfirmCancel').dialog({
            autoOpen: false,
            resizable: false,
            height: 240,
            width: 400,
            modal: true,
            buttons: {
                'Guardar': function () {
                    var id = $('#hfId').val();
                    $('#del_' + id).hide();
                    $('#del_' + id).next().show();
                    $('#ctl00_body_hfMotivo').val($('#ctl00_body_txt_motivo').val());
                    $(this).dialog("close");
                }
            }
        });

        $('.lnkCancel').click(function () {

            $('.lnkDelete').hide();
            $('.lnkCancel').show();
            var id = $(this).attr('id').replace('del_', '');
            $('#hfId').val(id * 1);
            $('#msgConfirmCancel').dialog('open');

            return false;
        });

        loadError();
    }
}

function confirmDelete() {
    if (!confirm('Se va a cancelar este movimiento por el siguiente motivo: ' + $('#ctl00_body_txt_motivo').val()))
        return false;
}

function loadError() {
    $('#errorMsgs').attr('title', $('#ctl00_body_hfTitleErr').val())

    $('#errorMsgs').dialog({
        autoOpen: false,
        height: 190,
        width: 420,
        modal: true,
        resizable: false
    });

    if ($('#ctl00_body_hfTitleErr').val().length > 0) {
        $('#errorMsg').html($('#ctl00_body_hfDescErr').val());
        $('#errorMsgs').dialog('open');
        $('#ctl00_body_hfTitleErr').val('');
    }
}

var master = new webApp.Master;
var pag = new RelEntSal();
master.Init(pag);