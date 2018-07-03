/// <reference path="almacenModel.js" />
/// <reference path="../webcontrols/ajaxInputUpload.js" />

var Expedicion = function () {

    var fileUploadAjax;

    this.Init = function () {

        fileUploadAjax = new AjaxInputUpload({
            content: 'fileUploadAjax',
            fileAccept: '.csv',
            processFunction: AlmacenModel.expedicionImportData,
            checkStatusFunction: AlmacenModel.expedicionImportDataStatus,
            resultDataShowedFunction: AlmacenModel.expedicionDataResultShowed
        });

        fileUploadAjax.open();
        initializedEvents();
    }

    function initializedEvents() {
        tab_admin_tab();
    }

    function tab_admin_tab() {
        $('#tab_admin').on('shown.bs.tab', function (e) {
            var tabSelect = $(e.target).attr('href');
            switch (tabSelect) {
                case '#imp':
                    fileUploadAjax.startUploadStatus();
                    break;
                default:
                    fileUploadAjax.clearUploadStatus();
                    break;

            }
            //console.log($(e.target).attr('href'));
        });
    }



}

var master = new webApp.Master;
var pag = new Expedicion();
master.Init(pag);