/// <reference path="../catalogos/catalogosModel.js" />
; (function () {

    // Define constructor
    this.TabCatalog = function () {

        // Create global element references
        this.key = 0;
        // Define option defaults
        var defaults = {
            catalogo: '',
            callBackChangeTab: null,
            parametersGet: null,
            callBackSaveData: null,
            callBackEnbDis: null
        }

        // Create options by extending defaults with the passed in arugments
        if (arguments[0] && typeof arguments[0] === "object") {
            this.options = extendDefaults(defaults, arguments[0]);
        }

    }

    // Public methods
    TabCatalog.prototype.init = function () {
        // open code goes here
        //buildOut.call(this);

        initializeEvents.call(this);
    }

    TabCatalog.prototype.changeAdmonTab = function(id) {
        this.key = id;
        $('#div-nuevo').removeClass('hidden');
        $('#adminTab a[href="#admon"]').tab('show');
    }

    TabCatalog.prototype.changeListTab = function(id) {
        $('#adminTab a[href="#list"]').tab('show');
    }

    TabCatalog.prototype.validateOptActive = function(active) {
        $('#opt_active').prop('checked', false).parent().removeClass('active');
        $('#opt_inactive').prop('checked', false).parent().removeClass('active');
        if (active) {
            $('#opt_active').prop('checked', true).parent().addClass('active');
        } else {
            $('#opt_inactive').prop('checked', true).parent().addClass('active');
        }
    }

    // Private methods
    function buildOut() {
        var _ = this;
        // $('#adminTab a[href="#list"]').tab('show')
    }

    function clearInput(_) {
        $('#aspnetForm')[0].reset();
        _.key = 0;
    }

    // Private ajax methods 
    function changeActiveRow(obj, IsActive, _) {
        
        if(IsActive) {
            CatalogosModel.catalogosEnb(
                _.options.catalogo,
                obj,
                _.options.callBackEnbDis,
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        } else {
            CatalogosModel.catalogosDsb(
                _.options.catalogo,
                obj,
                _.options.callBackEnbDis,
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        }
    }

    function saveData(obj, _) {
        
        if(_.key == 0) {
            CatalogosModel.catalogosAdd(
                _.options.catalogo,
                JSON.stringify(obj),
                _.options.callBackSaveData,
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        } else {
            CatalogosModel.catalogosUdt(
                _.options.catalogo,
                JSON.stringify(obj),
                _.options.callBackSaveData,
                function (jqXHR, textStatus) {
                    alert("Request failed: " + textStatus);
                }
            );
        }
    }

    // Private events methods
    function initializeEvents() {
        var _ = this;
        tab_show(_);
        btn_nuevo_click(_);
        btn_save_click(_);
        opt_active_click(_);
    }

    function tab_show(_) {
        $('.nav-tabs a').on('shown.bs.tab', function (e) {
            var tab = String(e.target).split('#')[1];
            switch (tab) {
                case 'admon':
                    if (_.key != 0) {
                        $('#div-nuevo').removeClass('hidden');
                        $('#div_active_opt').removeClass('hidden');
                    }
                    else {
                        $('#div-nuevo').addClass('hidden');
                        $('#div_active_opt').addClass('hidden');
                    }
                    break;
                case 'list':
                    clearInput(_);
                    break;
                default:
            }
            if (_.options.callBackChangeTab) _.options.callBackChangeTab(tab);
        });
    }

    function btn_nuevo_click(_) {
        $('#btn_nuevo').click(function () {
            clearInput(_);
            $(this).parent().parent().addClass('hidden');
            $('#div_active_opt').addClass('hidden');
        });
    }

    function btn_save_click(_) {
        $('#btn_save').click(function () {

            var obj = _.options.parametersGet();
            obj.Id = _.key;
            // console.log(JSON.stringify(obj));
            saveData(obj, _);

        });
    }

    function opt_active_click(_) {

        $('input[type=radio][name=activo]').change(function () {

            var obj = {
                key: _.key
            };

            if (obj.Id != 0) {

                if ($(this).attr('id') == 'opt_active') {
                    if (confirm('¿Desea activar el almacén?')) {
                        changeActiveRow(obj, true, _);
                    }
                }
                else {
                    if (confirm('¿Desea desactivar el almacén?')) {
                        changeActiveRow(obj, false, _);
                    }
                }
            }
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