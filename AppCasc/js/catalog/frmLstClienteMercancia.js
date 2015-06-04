var LstClienteMercancia = function () {

    this.Init = function () {
        var oCatalog = new Catalog();
        oCatalog.Fkey = $('#ctl00_body_hfFkey').val();
        oCatalog.CreateGrid($('#grdCatalog'), 'frmClienteMercancia');

        var inputFindText = $('#grdCatalog_filter').children('label').find("input");

        $(inputFindText).on('keyup', function () {
            $('#ctl00_body_hf_buscar').val($(this).val());
            var tdEmpty = $('#grdCatalog').children('tbody').children('tr').children('td').first();
            if ($(tdEmpty).hasClass('dataTables_empty'))
                $('#ctl00_body_btn_find_by').trigger('click');
        });

        inputFindText.val($('#ctl00_body_hf_buscar').val());
        var oComon = new Common();
        oComon.SetCaretAtEnd(inputFindText);
    }
}

var master = new webApp.Master;
var pag = new LstClienteMercancia();
master.Init(pag);