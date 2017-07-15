var Form = function () {

    this.Init = init;

    function init() {
        var divActions = $('#divActions');
        divActions.children('input[type="submit"]').each(function () {
            $(this).button();
        });
    }

    //loadError();

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



//$(document).ready(function () {

//    var oForm = new Form();
//    oForm.Init();

//});

var master = new webApp.Master;
var pag = new Form();
master.Init(pag);

function rfcValido(rfc, aceptarGenerico = true) {
    const re       = /^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/;
    var   validado = rfc.match(re);

    if (!validado)  //Coincide con el formato general del regex?
        return false;

    //Separar el dígito verificador del resto del RFC
    const digitoVerificador = validado.pop(),
          rfcSinDigito      = validado.slice(1).join(''),
          len               = rfcSinDigito.length,

    //Obtener el digito esperado
          diccionario       = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
          indice            = len + 1;
    var   suma,
          digitoEsperado;

    if (len == 12) suma = 0
    else suma = 481; //Ajuste para persona moral

    for(var i=0; i<len; i++)
        suma += diccionario.indexOf(rfcSinDigito.charAt(i)) * (indice - i);
    digitoEsperado = 11 - suma % 11;
    if (digitoEsperado == 11) digitoEsperado = 0;
    else if (digitoEsperado == 10) digitoEsperado = "A";

    //El dígito verificador coincide con el esperado?
    // o es un RFC Genérico (ventas a público general)?
    if ((digitoVerificador != digitoEsperado)
     && (!aceptarGenerico || rfcSinDigito + digitoVerificador != "XAXX010101000"))
        return false;
    else if (!aceptarGenerico && rfcSinDigito + digitoVerificador == "XEXX010101000")
        return false;
    return rfcSinDigito + digitoVerificador;
}

function curpValida(curp) {
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = curp.match(re);
	
    if (!validado)  //Coincide con el formato general?
    	return false;
    
    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {
        //Fuente https://consultas.curp.gob.mx/CurpSP/
        var diccionario  = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma      = 0.0,
            lngDigito    = 0.0;
        for(var i=0; i<17; i++)
            lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10) return 0;
        return lngDigito;
    }
  
    if (validado[2] != digitoVerificador(validado[1])) 
    	return false;
        
    return true; //Validado
}

function nssValido(nss) {
    const re       = /^(\d{2})(\d{2})(\d{2})\d{5}$/,
          validado = nss.match(re);
        
    if (!validado)  // 11 dígitos y subdelegación válida?
        return false;
        
    const subDeleg = parseInt(validado[1],10),
          anno     = new Date().getFullYear() % 100;
    var   annoAlta = parseInt(validado[2],10),
          annoNac  = parseInt(validado[3],10);
    
    //Comparar años (excepto que no tenga año de nacimiento)
    if (subDeleg != 97) {
        if (annoAlta <= anno) annoAlta += 100;
        if (annoNac  <= anno) annoNac  += 100;
        if (annoNac  >  annoAlta)
    	    return false; // Err: se dio de alta antes de nacer!
    }
    
    return luhn(nss);
}

// Algoritmo de Luhn
//  https://es.wikipedia.org/wiki/Algoritmo_de_Luhn
function luhn(nss) {
    var suma   = 0,
        par    = false,
        digito;
    
    for (var i = nss.length - 1; i >= 0; i--) {
        var digito = parseInt(nss.charAt(i),10);
        if (par)
        	if ((digito *= 2) > 9)
        	    digito -= 9;
        
        par = !par;
        suma += digito;
    }
    return (suma % 10) == 0;
}


function validarNSS(source, arguments) {
    var nss = arguments.Value;
    var nssCorrecto = nssValido(nss);   // ⬅️ Acá se comprueba
    arguments.IsValid = nssCorrecto;
}

function validarCURP(source, arguments) {
    var curp = arguments.Value;
    var curpCorrecto = curpValida(curp);   // ⬅️ Acá se comprueba
    arguments.IsValid = curpCorrecto;
}

function validarRFC(source, arguments) {
//    var rfc         = source.value.trim().toUpperCase(),
//        //resultado   = document.getElementById(validator),
//        valido;
//for(var propertyName in arguments) {
//   // propertyName is what you want
//   // you can get the value like this: myObject[propertyName]
//   alert(propertyName);
//}
var rfc = arguments.Value;
//        
    var rfcCorrecto = rfcValido(rfc);   // ⬅️ Acá se comprueba
  arguments.IsValid = rfcCorrecto;
//    if (rfcCorrecto) {
//    	valido = "Válido";
//      resultado.style.visibility = 'hidden';
//    } else {
//    	valido = "No válido"
//    	resultado.style.visibility = 'visible';
//    }
        
//    resultado.innerText = "Formato: " + valido;

}