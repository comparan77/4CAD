﻿/// <reference path="../common.js" />

var CatalogosModel = (function () {

    //"private" variables:
    //    var myPrivateVar = "I can be accessed only from within obj.";

    //"private" method:
    var catalogosAjax = function (method, datatype, opcion, obj, opt, callback, error) {
        try {
            Common.ajax('/handlers/Catalog.ashx?op=' + opcion + '&opt=' + opt,
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
        catalogosSltById: function (catalogo, obj, callback, error) {
            catalogosAjax("POST", "json", catalogo, obj, 'sltById', callback, error);
        },

        catalogosLst: function (catalogo, callback, error) {
            catalogosAjax("GET", "json", catalogo, null, 'lst', callback, error);
        },

        catalogosLstBy: function (catalogo, obj, callback, error) {
            catalogosAjax("GET", "json", catalogo, obj, 'lst', callback, error);
        },

        catalogosAdd: function (catalogo, obj, callback, error) {
            catalogosAjax("POST", "json", catalogo, obj, 'add', callback, error);
        },

        catalogosUdt: function (catalogo, obj, callback, error) {
            catalogosAjax("POST", "json", catalogo, obj, 'udt', callback, error);
        },

        catalogosEnb: function (catalogo, obj, callback, error) {
            catalogosAjax("POST", "json", catalogo, obj, 'enb', callback, error);
        },

        catalogosDsb: function (catalogo, obj, callback, error) {
            catalogosAjax("POST", "json", catalogo, obj, 'dsb', callback, error);
        }

    };

})();