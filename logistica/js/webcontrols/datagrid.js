; (function () {

    // Define constructor
    this.DataGrid = function () {

        // Create global element references
        this.table = null;
        this.firstTime = true;
        this.key = 0;
        // Define option defaults
        var defaults = {
            idtable: '',
            dataKey: 'Id',
            getDataFunction: null,
            callBackRowFill: null,
            callBackRowClick: null,
            callBackSave: null,
            callBackChangeTab: null
        }

        // Create options by extending defaults with the passed in arugments
        if (arguments[0] && typeof arguments[0] === "object") {
            this.options = extendDefaults(defaults, arguments[0]);
        }

    }

    // Public methods
    DataGrid.prototype.init = function () {
        // open code goes here
        // buildOut.call(this);
        // buildInit.call(this);
        // initializeEvents.call(this);
    }    

    DataGrid.prototype.DataBind = function(data, callback) {
        var _ = this;
        if ($.fn.DataTable.isDataTable( '#' + _.options.idtable ) ) {
            grdCatalogClear(_);
        }
        grdCatalogBind.call(this, data, callback);
    }

    // Private methods    
    function grdCatalogBind(data, callback) {
        var _ = this;
        fillGrd(data, _, callback);
        grdCatalogInit(_);
    }

    function fillGrd(data, _, callback) {

        var tr;
        var td;
        var field;
        
        $('#' + _.options.idtable).children('tbody').html('');
        // console.log('dataKey: ' + _.options.dataKey);
        $.each(data, function (i, obj) {
            tr = document.createElement('tr');
            tr.setAttribute('id', 'row_' + obj[_.options.dataKey]);
            _.options.callBackRowFill(tr, obj);

            $('#' + _.options.idtable).children('tbody').append(tr);
        });

        if(callback) callback();
    }

    function grdCatalogInit(_) {

        _.table = $('#' + _.options.idtable).DataTable({
            responsive: true,
            "autoWidth": false,
            rowId: _.options.dataKey,
            "language": {
                "lengthMenu": "Mostrando _MENU_ registros",
                "infoFiltered": " - Filtrando de _MAX_ registros",
                "emptyTable": "Sin datos disponibles para la tabla",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                "infoEmpty": "Sin datos que mostrar",
                "loadingRecords": "Porfavor aguarde - Cargando registros...",
                "search": "Buscar"
            },
            "order": []
        });
        grdCatalogRowClick(_);
    }

    function grdCatalogClear(_) {
        _.table.clear().destroy();
    }

    function initializeEvents(_) {
    }

    function grdCatalogRowClick(_) {
        _.table.on('click', 'tr', function () {
            _.key = _.table.row(this).id().split('_')[1];
            if(_.options.callBackRowClick) _.options.callBackRowClick(this, _.key);
        });
    }

    // Utility method to extend defaults with user options
    function extendDefaults(source, properties) {
        var property;
        for (property in properties) {
            if (properties.hasOwnProperty(property)) {
                source[property] = properties[property];
            }
        }
        return source;
    }

} ());