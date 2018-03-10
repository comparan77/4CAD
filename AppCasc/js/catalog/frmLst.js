var Catalog = function () {

    var fkey;
    var gridView;
    var lastCol;

    this.CreateGrid = createGrid;
    this.Fkey = fkey;

    function createGrid(gridView, urlIst) {

        lastCol = gridView.children('tbody').children('tr').first().children('td').length - 1;

        if (lastCol > 0) {

            gridView.dataTable({
                'bJQueryUI': true,
                'bPaginate': false,
                'oLanguage': {
                    'sEmptyTable': 'No existen datos para este catalogo',
                    'sZeroRecords': 'No existen coincidencias',
                    'sSearch': 'Buscar:',
                    'sInfo': 'Total: _TOTAL_ Registro(s), Mostrando de _START_ a _END_',
                    'sInfoEmpty': 'Mostrando 0 Registros',
                    'sInfoFiltered': ' - Filtrando de _MAX_ registros'
                },
                'order': [[lastCol, 'desc'], [0, 'asc']]
            });
        } else {
            gridView.dataTable({
                'bJQueryUI': true,
                'bPaginate': false,
                'oLanguage': {
                    'sEmptyTable': 'No existen datos para este catalogo',
                    'sZeroRecords': 'No existen coincidencias',
                    'sSearch': 'Buscar:',
                    'sInfo': 'Total: _TOTAL_ Registro(s), Mostrando de _START_ a _END_',
                    'sInfoEmpty': 'Mostrando 0 Registros',
                    'sInfoFiltered': ' - Filtrando de _MAX_ registros'
                }
            });
        }

        gridView.children('tbody').children('tr').click(function (e) {
            if ($(this).hasClass('row_selected')) {
                $(this).removeClass('row_selected');
            }
            else {
                gridView.$('tr.row_selected').removeClass('row_selected');
                $(this).addClass('row_selected');
            }
        });

        $('.dataTables_filter').css('position', 'relative');

        fkey = this.Fkey;
        if (fkey == undefined)
            fkey = '';

        if (fkey != '')
            fkey = '&fKey=' + fkey;

        if (urlIst != '')
            $('.dataTables_filter').append('<a style="position: absolute; right: 10px; top: 4px;" href="' + urlIst + '.aspx?Action=Ist' + fkey + '"><span class="ui-icon ui-icon-plus spnIcon">Agregar</span></a>');

        $('.ui-icon-pencil').each(function () {
            var href = $(this).parent().attr('href');
            $(this).parent().attr('href', href + fkey);
        });

        gridView.children('tbody').children('tr').each(function () {

            var Msg = '¿Desea DESActivar este registro?';
            var lnkAction = $(this).children('td:nth-child(' + lastCol + ')').children('a');
            if (lnkAction.hasClass('ui-icon-circle-close')) {
                $(this).css('text-decoration', 'line-through');
                Msg = '¿Desea Activar este registro?';
            }

            $(this).children('td').last().children('a').click(function () {
                if (!confirm(Msg))
                    return false;
            });
        });

        loadError();

    }
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