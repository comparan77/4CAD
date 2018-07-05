/// <reference path="../common.js" />

var ProcesosModel = (function () {

    //"private" variables:
    //    var myPrivateVar = "I can be accessed only from within obj.";

    //"private" method:
    var procesosAjax = function (method, datatype, opcion, obj, opt, callback, error) {
        try {
            Common.ajax('/handlers/Process.ashx?op=' + opcion + '&opt=' + opt,
            method,
            datatype,
            obj,
            function (data) {
                if (callback) callback(data);
            },
            function (jqXHR, textStatus) {
                if (error) error(jqXHR, textStatus);
            });
        } catch (err) {
            console.log(err.message);
        }
    };

    return {
        //        myPublicVar: "I'm accessible as obj.myPublicVar",
        proformaProcesar: function (callback, error) {
            procesosAjax("POST", "json", 'proforma', null, 'procesar', callback, error);
        },

        proformaConcentradoGet: function (callback, error) {
            procesosAjax("GET", "json", 'proforma', null, 'concentrado_get', callback, error);
        },

        proformaConcentradoGetAplicada: function (callback, error) {
            procesosAjax("GET", "json", 'proforma', null, 'concentrado_getAplicada', callback, error);
        },

        proformaconcentrado_getAllCliente: function (obj, callback, error) {
            procesosAjax("POST", "json", 'proforma', obj, 'concentrado_getAllCliente', callback, error);
        },

        proformaconcentrado_getAllClienteApp: function (obj, callback, error) {
            procesosAjax("POST", "json", 'proforma', obj, 'concentrado_getAllClienteApp', callback, error);
        },

        proformaConcentradoGetByCte: function (obj, callback, error) {
            procesosAjax("POST", "json", 'proforma', obj, 'concentrado_getByCte', callback, error);
        },

        proformaConcentradoUdtActiva: function (obj, callback, error) {
            procesosAjax("POST", "json", 'proforma', obj, 'concentradoUdtActiva', callback, error);
        },

        proformaConcetradoProfActByFolio: function (folio, callback, error) {
            procesosAjax("GET", "json", 'proforma', null, 'concetradoProfActByFolio&folio=' + folio, callback, error);
        },

        proformaResProfXlsm: function (data, callback, error) {
            Common.ajaxAppJson(
                'http://4cad.casc.com.mx:8002/algeyaResProf',
                'POST',
                JSON.stringify(data),
                function (data) {
                    if (callback) callback(data);
                },
                function (jqXHR, textStatus) {
                    if (error) error(jqXHR, textStatus);
                }
            );
        },

        proformaConcetradoProfActByFolio: function (folio, callback, error) {
            procesosAjax("GET", "json", 'proforma', null, 'concetradoProfActByFolio&folio=' + folio, callback, error);
        },

        procesosLst: function (catalogo, callback, error) {
            procesosAjax("GET", "json", catalogo, null, 'lst', callback, error);
        },

        procesosLstById: function (catalogo, obj, callback, error) {
            procesosAjax("GET", "json", catalogo, obj, 'lst', callback, error);
        }

    };

})();