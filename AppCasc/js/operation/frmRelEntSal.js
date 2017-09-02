var objLnkPrint;
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

        $('#msgNumCopies').dialog({
            autoOpen: false,
            resizable: false,
            height: 200,
            width: 300,
            modal: true,
            buttons: {
                'Imprimir': function () {
                    var copiesSelected = '';

                    $('.copieOp').each(function () {

                        if ($(this).is(':checked')) {
                            copiesSelected += $(this).val() + ',';
                        }

                    });

                    if (copiesSelected.length > 0) {
                        $('#ctl00_body_hf_copies').val(copiesSelected.substr(0, copiesSelected.length - 1));
                        var idParent = $('#' + objLnkPrint).children('td:nth-child(7)').children('a').attr('id').split('_')[0];
                        var idChildren = $('#' + objLnkPrint).children('td:nth-child(7)').children('a').attr('id').split('_')[3];
                        __doPostBack(idParent + '$body$repRows$' + idChildren + '$lnk_change_status', '');
                    }
                    else
                        alert('Es necesario seleccionar por lo menos una casilla');

                    $(this).dialog("close");

                }
            }
        });

        $('.ui-icon-print').each(function () {
            $(this).click(function () {
                objLnkPrint = $(this).parent().parent().attr('id');
                id_cliente = $(this).parent().parent().children('td:nth-child(5)').attr('id');
                id_operation = $('#ctl00_body_hfOperation').val();
                var copies = $('#ctl00_body_hf_copies').val();
                if (copies.length == 0) {
                    fillCopyOperationByCliente(id_cliente, 1);
                    return false;
                }
            });
        });

        $('.lnkCancel').click(function () {

            $('.lnkDelete').hide();
            $('.lnkCancel').show();
            var id = $(this).attr('id').replace('del_', '');
            $('#hfId').val(id * 1);

            return false;
        });

        loadError();
    }

    function fillCopyOperationByCliente(id_cliente, id_operation) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Catalog.ashx?catalogo=cliente&opt=getCopyByOperation",
            //dataType: "jsonp",
            data: {
                id_cliente: id_cliente,
                id_operation: id_operation
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {

                var tr = '';
                var td = '';
                var tbodyCopies = $('#tbodyCopies');
                tbodyCopies.html('');
                $.each(data, function (i, obj) {
                    tr = '<tr>';
                    td = '<td><input type="checkbox" class="copieOp" name="chkcopie" id="chk_' + obj.Id + '" value="' + obj.Id + '" /></td>';
                    td += '<td><span>' + obj.Nombre + '</span></td>';
                    tr += td;
                    tr += '</tr>';
                    tbodyCopies.append(tr);
                });

                $('#msgNumCopies').dialog('open');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
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