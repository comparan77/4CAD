; (function () {

    // Define constructor
    this.MonthPicker = function () {

        // Create global element references
        // Define option defaults
        this.lnkYearBack = null;
        this.lnkYearAct = null;
        this.lnkYearNext = null;

        this.activeYear = (new Date()).getFullYear();
        this.activeMonth = (new Date()).getMonth();

        var defaults = {
            content: '',
            callBackYearClick: null,
            callBackMonthClick: null
        }

        // Create options by extending defaults with the passed in arugments
        if (arguments[0] && typeof arguments[0] === "object") {
            this.options = extendDefaults(defaults, arguments[0]);
        }

    }

    // Public methods
    MonthPicker.prototype.init = function () {
        // open code goes here
        buildOut.call(this);

        initializeEvents.call(this);
    }

    MonthPicker.prototype.getYear = function () {
        return this.activeYear;
    }

    MonthPicker.prototype.getMonth = function () {
        return this.activeMonth + 1;
    }

    // Private methods
    function buildOut() {
        var _ = this;
        
        var content = document.getElementById(this.options.content);
        var navYear = document.createElement('nav');
        
        var ulYear = document.createElement('ul');
        ulYear.className = 'pagination justify-content-center';
        ulYear.setAttribute('style', 'margin:0');
        
        var liYearBack = document.createElement('li');
        liYearBack.className = 'page-item';
        _.lnkYearBack = document.createElement('a');
        _.lnkYearBack.className = 'page-link';
        _.lnkYearBack.setAttribute('href', '#');
        _.lnkYearBack.setAttribute('aria-label', 'Previous');
        var spnYearBackIcon = document.createElement('span');
        spnYearBackIcon.setAttribute('aria-hidden', 'true');
        spnYearBackIcon.innerHTML = '&laquo;';
        var spnYearBack = document.createElement('span');
        spnYearBack.className = 'sr-only';
        spnYearBack.innerHTML = 'Previous';
        _.lnkYearBack.appendChild(spnYearBackIcon);
        _.lnkYearBack.appendChild(spnYearBack);
        liYearBack.appendChild(_.lnkYearBack);
        ulYear.appendChild(liYearBack);

        var liYearAct = document.createElement('li');
        liYearAct.className = 'page-item';
        _.lnkYearAct = document.createElement('span');
        _.lnkYearAct.className = 'page-link';
        _.lnkYearAct.innerHTML = _.activeYear;
        liYearAct.appendChild(_.lnkYearAct);
        ulYear.appendChild(liYearAct);

        var liYearNext = document.createElement('li');
        liYearNext.className = 'page-item';
        _.lnkYearNext = document.createElement('a');
        _.lnkYearNext.className = 'page-link';
        _.lnkYearNext.setAttribute('href', '#');
        _.lnkYearNext.setAttribute('aria-label', 'Next');
        var spnYearNextIcon = document.createElement('span');
        spnYearNextIcon.setAttribute('aria-hidden', 'true');
        spnYearNextIcon.innerHTML = '&raquo;';
        var spnYearNext = document.createElement('span');
        spnYearNext.className = 'sr-only';
        spnYearNext.innerHTML = 'Next';
        _.lnkYearNext.appendChild(spnYearNextIcon);
        _.lnkYearNext.appendChild(spnYearNext);
        liYearNext.appendChild(_.lnkYearNext);
        ulYear.appendChild(liYearNext);

        navYear.appendChild(ulYear);

        var navMonth = document.createElement('nav');
        var ulMonth = document.createElement('ul');
        ulMonth.className = 'pagination justify-content-center';
        var months = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
        for(var m in months) {
            var liMonth = document.createElement('li');
            liMonth.className = 'page-item mothSel';
            liMonth.setAttribute('id', 'month_' + m);
            var lnkMonth = document.createElement('a');
            lnkMonth.className = 'page-link';
            lnkMonth.setAttribute('href', '#');
            lnkMonth.innerHTML = months[m];
            liMonth.appendChild(lnkMonth);
            ulMonth.appendChild(liMonth);
        }
        navMonth.appendChild(ulMonth);

        content.appendChild(navYear);
        content.appendChild(navMonth);

        setMonth(_.activeMonth, _);

    }

    function setMonth(number, _) {
        $('#month_' + _.activeMonth).removeClass('active');
        $('#month_' + number).addClass('active');
        _.activeMonth = number;
    }

    // Private events methods
    function initializeEvents() {
        var _ = this;
        month_lnk_click(_);
        year_lnk_click(_);
    }

    function month_lnk_click(_) {
        $('.mothSel').each(function() {
            $(this).click(function() {
                setMonth($(this).attr('id').split('_')[1] * 1, _);
                if(_.options.callBackMonthClick) _.options.callBackMonthClick(_.activeYear, _.activeMonth + 1);
                return false;
            });
        });
    }

    function year_lnk_click(_) {
        _.lnkYearBack.addEventListener('click', function(e) { 
            _.activeYear--;
            _.lnkYearAct.innerHTML = _.activeYear;
            if(_.options.callBackYearClick) _.options.callBackYearClick(_.activeYear, _.activeMonth + 1);
            e.preventDefault();
        }, false);

        _.lnkYearNext.addEventListener('click', function(e) { 
            _.activeYear++;
            _.lnkYearAct.innerHTML = _.activeYear;
            if(_.options.callBackYearClick) _.options.callBackYearClick(_.activeYear, _.activeMonth + 1);
            e.preventDefault();
        }, false);

    }

    function opt_active_click(_) {

        $('input[type=radio][name=activo]').change(function () {

            var obj = {
                Id: _.key,
                Nombre: '',
                Direccion: ''
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