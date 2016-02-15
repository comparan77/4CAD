var procesaFacturaExcel = function () {

    this.Init = init;

    function init() {

        var up_ImportStep = $('#ctl00_body_up_ImportStep');

        $(up_ImportStep).panelReady(function () {

            var btn_processFile = $('#ctl00_body_btn_processFile').button();
            var fileup_facturacion = $('#ctl00_body_fileup_facturacion');

            fileup_facturacion.unbind('change').change(function () {
                changeFileUpload(this);
            });
            if (btn_processFile.length > 0)
                fileup_facturacion.hide();
            fileup_facturacion.button();

            var hrefLnkFile = $('#ctl00_body_lnkFile').attr('href');
            if (typeof hrefLnkFile !== typeof undefined && hrefLnkFile !== false) {
                btn_processFile.hide();
                fileup_facturacion.show();
            }
        });

    }

    function changeFileUpload(file_upload) {
        //Valida que el archivo no sea mayor a la cantidad de máxima de bytes declarada en el webconfig <httpRuntime maxRequestLength="20000"/>
        try {
            var size = file_upload.files[0].size;
            var MaxRequestLen = $('#ctl00_body_hf_MaxRequestLen').val() * 1;
            if (size > MaxRequestLen) {
                alert('El archivo es de ' + size / 1048576 + 'MB y sobrepasa el tamaño máximo permitido de ' + MaxRequestLen / 1048576 + 'MB');
                return false;
            }
        }
        catch (err) { }

        if (confirm('Desea importar ' + file_upload.value + '?')) {
            $(this).hide();
            $('#ctl00_body_btn_importar').trigger('click');
        }
        else
            return false;
    }
}

var master = new webApp.Master;
var pag = new procesaFacturaExcel();
master.Init(pag);