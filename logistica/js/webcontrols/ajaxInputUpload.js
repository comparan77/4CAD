; (function () {

    // Define constructor
    this.AjaxInputUpload = function () {

        // Create global element references
        this.div_progress_upload = null;
        this.btn_upload = null;
        this.uploadInterval = false;

        // Define option defaults
        var defaults = {
            content: '',
            fileAccept: '',
            processFunction: null,
            checkStatusFunction: null,
            resultDataShowedFunction: null
        }

        // Create options by extending defaults with the passed in arugments
        if (arguments[0] && typeof arguments[0] === "object") {
            this.options = extendDefaults(defaults, arguments[0]);
        }

    }

    // Public methods
    AjaxInputUpload.prototype.open = function () {
        // open code goes here
        buildOut.call(this);

        initializeEvents.call(this);
    }

    AjaxInputUpload.prototype.setValueProgressVar = function(value) {
        this.div_progress_upload.style.width = value + '%';
        this.div_progress_upload.setAttribute('aria-valuenow', value);
        this.div_progress_upload.innerHTML = value + '%';
    }

    AjaxInputUpload.prototype.importDataStatus = function(data) {
        var valor = data[1];
        if (isNaN(valor))
            valor = 0;

        $('#btn_upload').addClass('disabled');
        $('#btn_upload').html('Procesando el archivo ...');

        if (data[1] == 0) {
            $('#btn_upload').removeClass('disabled');
            $('#btn_upload').html('Procesar archivo');
            $("#div_file_recepcion").removeClass('hidden');
        }

        if (valor != 0)
            this.setValueProgressVar(Math.round(data[0] / data[1] * 100));
        else {
            this.setValueProgressVar(0);
        }

        if(data[2].length > 0 && data[3]==false) {

            var msg = '';

            var arrError = data[2].filter(function(obj) { 
//                console.log(obj.ErrUpload);
                return obj.ErrUpload != null;
            });
//            console.log('aqui: ' + JSON.stringify(arrError)) ;

            var Exito = data[2].length - arrError.length;

            msg = 'Se procesaron: ' + Exito + ' de ' + data[2].length + ' registros.\n\n';

            if(arrError.length > 0) {
                msg += 'Registros con problemas: \n';
                $.each(arrError, function (i, obj) {
                    msg += obj.ErrUpload + '\n';
                });
            }

            alert(msg);
            this.options.resultDataShowedFunction();
        }
    }

    AjaxInputUpload.prototype.checkUploadStatus = function() {
        var _ = this;
        _.options.checkStatusFunction(function(data) {
            //console.log(JSON.stringify(data));
            _.importDataStatus(data);
        }, 
        function(jqXHR, textStatus) {
            alert("Request failed: " + textStatus);    
        });
    }

    AjaxInputUpload.prototype.clearUploadStatus = function() {
        if(this.uploadInterval != false) {
            clearInterval(this.uploadInterval);
            this.uploadInterval = false;
        }
    }

    AjaxInputUpload.prototype.startUploadStatus = function() {
        if(this.options.checkStatusFunction) {
            if(this.uploadInterval == false) {
                var _ = this;
                //this.uploadInterval = setInterval(function() { _.checkUploadStatus(); }, 500);
                this.uploadInterval = setInterval(function() { 
                     _.options.checkStatusFunction(function(data) {
                        console.log(JSON.stringify(data));
                        _.importDataStatus(data);
                    }, 
                    function(jqXHR, textStatus) {
                        alert("Request failed: " + textStatus);    
                    });
                
                 }, 500);

            }
        }
    }

    // Private methods

    function buildOut() {
        
        var content = document.getElementById(this.options.content);

        var div_file_recepcion = document.createElement('div');
        div_file_recepcion.className = 'form-group';
        var lbl = document.createElement('label');
        lbl.innerHTML = 'Seleccionar Archivo';
        var input = document.createElement('input');
        input.setAttribute('type', 'file');
        input.setAttribute('id', 'file_recepcion');
        input.setAttribute('accept', this.options.fileAccept);
        div_file_recepcion.appendChild(lbl);
        div_file_recepcion.appendChild(input);

        var div_process = document.createElement('div');
        var div = document.createElement('div');
        div.className = 'progress';
        this.div_progress_upload = document.createElement('div');
        this.div_progress_upload.className = 'progress-bar progress-bar-striped progress-bar-animated';
        this.div_progress_upload.setAttribute('role', 'progressbar');
        this.div_progress_upload.setAttribute('style', 'width: 0%');
        this.div_progress_upload.setAttribute('aria-valuenow', 0);
        this.div_progress_upload.setAttribute('aria-valuemin', 0);
        this.div_progress_upload.setAttribute('aria-valuemax', 0);
        div.appendChild(this.div_progress_upload);
        this.btn_upload = document.createElement('button');
        this.btn_upload.setAttribute('type', 'button');
        this.btn_upload.setAttribute('id', 'btn_upload');
        this.btn_upload.className = 'btn btn-primary';
        this.btn_upload.innerHTML = 'Procesar archivo';
        div_process.appendChild(div);
        div_process.appendChild(this.btn_upload);
        
        content.appendChild(div_file_recepcion);
        content.appendChild(div_process);
    }

    function initializeEvents() {
        var _ = this;
        //addEventListener
        this.btn_upload.addEventListener('click', function() { 

            $(this).addClass('disabled');
            $(this).html('Procesando el archivo ...');

            var file = document.getElementById('file_recepcion');
            var frmData = new FormData();
            if (file.files.length == 0) return false;

            frmData.append('name', file.files[0], file.files[0].name);
            var input = $("#file_recepcion");
            input.replaceWith(input.val('').clone(true));

            if(_.options.processFunction) _.options.processFunction(frmData);
        });
    }

    function setEstatusBtn(element, text, disabled) {
        if (disabled) {
            document.getElementById(element).setAttribute('disabled', 'disabled');
            document.getElementById(element).innerHTML = text;
        }
        else {
            document.getElementById(element).removeAttribute('disabled');
            document.getElementById(element).innerHTML = text;
        }
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