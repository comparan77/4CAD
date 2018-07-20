function Common() {}

Common.ajax = function (url, method, datatype, obj, callback, error) {

    var request = $.ajax({
        url: url,
        data: obj,
        method: method,
        dataType: datatype
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.ajaxAppJson = function (url, method, obj, callback, error) {

    var request = $.ajax({
        url: url,
        data: obj,
        method: method,
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.ajaxFileUpload = function (url, frmData, callback, error) {

    var request = $.ajax({
        url: url,
        method: "POST",
        data: frmData,
        processData: false,
        contentType: false
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.getCurrencyFormat = function (dato, decimals) {
 
    var num_decimal = typeof decimals !== 'undefined' ? decimals : 2;
    var isNegative;
    if (dato < 0)
        isNegative = '-$';
    else
        isNegative = '$';
    dato = Math.abs(dato);
    return isNegative + dato.toFixed(num_decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,');
}

Common.fillSelect = function (id_control, array, first_element) {
    var ddl = document.getElementById(id_control);
    for (var i = 0; i < array.length; i++) {
        opt = document.createElement('option');
        opt.innerHTML = array[i].datatext;
        opt.value = array[i].datavalue;
        ddl.appendChild(opt);
    }
}

Common.clearNode = function (node) {
    var myNode = document.getElementById(node);
    while (myNode.firstChild) {
        myNode.removeChild(myNode.firstChild);
    }
}

Common.fillTableBody = function (id_table_body, data, datakey, callbackRowAdd, callbackFill) {
    var table = document.getElementById(id_table_body);
    Common.clearNode(id_table_body);
    for (var i in data) {
        var tr = document.createElement('tr');
        if (datakey != undefined) tr.setAttribute('id', id_table_body + '_' + data[datakey]);
        var values = Object.values(data[i]);
        for (var y in values) {
            var td = document.createElement('td');
            field = document.createTextNode(values[y]);
            td.appendChild(field);
            tr.appendChild(td);
        }
        if (callbackRowAdd) callbackRowAdd(tr, data[i]);
        table.appendChild(tr);
    }
    if (callbackFill) callbackFill();
}

//Es necesario tener el plugin select2 y jquery
Common.fillSelect2 = function (arr_catalog, callbackMap, callback, loaded) {
    if (loaded == undefined) {
        loaded = 0;
    }
    if (loaded < arr_catalog.length) {

        var catalog = arr_catalog[loaded][0];
        var ddlControl = arr_catalog[loaded][1]
        
        CatalogosModel.catalogosLst(catalog, function (data) {
            var dataMap = $.map(data, function (obj) {
                obj.id = obj.Id; // replace pk with your identifier
                obj.text = obj.Nombre;
                if (callbackMap) callbackMap(obj, catalog);
                return obj;
            });

            $('#' + ddlControl).select2({

                placeholder: "Selecciona una opción",
                data: dataMap,
                theme: "classic"
            });

            loaded++;
            Common.fillSelect2(arr_catalog, callbackMap, callback, loaded);

        });
    } else {
        if (callback) callback();
    }
}