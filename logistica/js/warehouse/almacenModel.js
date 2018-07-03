/// <reference path="../common.js" />

var AlmacenModel = (function () {

    //"private" variables:
    //    var myPrivateVar = "I can be accessed only from within obj.";

    //"private" method:
    var almacenAjax = function (method, datatype, opcion, obj, opt, callback, error) {
        try {
            Common.ajax('/handlers/Warehouse.ashx?op=' + opcion + '&opt=' + opt,
            method,
            datatype,
            obj,
            function (data) {
                if(callback) callback(data);
            },
            function (jqXHR, textStatus) {
                if(error) error(jqXHR, textStatus);
            });
        } catch (err) {
            console.log(err.message);
        }
    };

    return {
        //        myPublicVar: "I'm accessible as obj.myPublicVar",
        recepcionCortinaVerificarUsuario: function (callback, error) {
            almacenAjax("GET", "json", 'recepcion', null, 'cortinaVerificarByUsuario', callback, error);
        },

        recepcionCortinaDispBodega: function (obj, callback, error) {
            almacenAjax("GET", "json", 'recepcion', obj, 'cortinaDispobleByBodega', callback, error);
        },

        recepcionCortinaTomar: function (obj, callback, error) {
            almacenAjax("POST", "json", 'recepcion', obj, 'cortinaTomar', callback, error);
        },

        recepcionCortinaLiberar: function (obj, callback, error) {
            almacenAjax("POST", "json", 'recepcion', obj, 'cortinaLiberar', callback, error);
        },

        recepcionCortinaTarimaPush: function (obj, callback, error) {
            almacenAjax("POST", "json", 'recepcion', obj, 'cortinaTarimaPush', callback, error);
        },

        recepcionImportDataStatus: function (callback, error) {
            almacenAjax("GET", "", 'recepcion', null, 'importRecepcionDataStatus', callback, error);
        },

        recepcionImportData: function (frmData, callback, error) {
            Common.ajaxFileUpload('/handlers/Warehouse.ashx?op=recepcion&opt=importRecepcionData', frmData, callback, error);
        },

        recepcionDataResultShowed: function (callback, error) {
            almacenAjax("POST", "", 'recepcion', null, 'importRecepcionDataResultShowed', callback, error);
        },

        expedicionImportDataStatus: function (callback, error) {
            almacenAjax("GET", "", 'expedicion', null, 'importExpedicionDataStatus', callback, error);
        },

        expedicionImportData: function (frmData, callback, error) {
            Common.ajaxFileUpload('/handlers/Warehouse.ashx?op=expedicion&opt=importExpedicionData', frmData, callback, error);
        },

        expedicionDataResultShowed: function (callback, error) {
            almacenAjax("POST", "", 'expedicion', null, 'importExpedicionDataResultShowed', callback, error);
        }

    };

})();