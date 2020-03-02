//rossu 05102018

(function (factory) {
    if (typeof module === 'object' && typeof module.exports === 'object') {
        factory(require('jquery'), require('moment'), window, document);
    } else {
        factory(jQuery, moment, window, document);
    }
}(function ($, moment, window, document, undefined) {
    var input = function (elem, opts) {
        this.$elem = $(elem);
        this.opts = opts;
        this.defaultOpts = {
            type: 'alphabet',
            decimalPlace: 2,
            format: 'MM/DD/YYYY'
        }
    };

    input.prototype = {
        init: function (opts) {
            var _ = this;
            _.options = $.extend({}, _.defaultOpts, _.opts, opts);
            if (_.options.type == '') return _;
            if (_.options.type == 'date' || _.options.type == 'time' || _.options.type == 'datetime') {
                _.$elem.on("change", function (e) {
                    if (_.options.type == "date") {
                        _.loadDateFomatter(e, _.options);
                    } else if (_.options.type == "time") {
                        _.loadTimeFomatter(e, _.options);
                    } else if (_.options.type == "datetime") {
                        _.loadDateTimeFomatter(e, _.options);
                    }
                });
                _.$elem.on("keydown", function (e) {
                    if (_.options.type == "date") {
                        var pattern = new RegExp("[0-9./-]");
                        var result = pattern.test(e.key);
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    } else if (_.options.type == "time") {
                        var pattern = new RegExp("[0-9.:AMPMampm ]");
                        var result = pattern.test(e.key);
                        if (e.target.value.indexOf(" ") > -1) {
                            if (e.key == " ") e.preventDefault();
                        }
                        if (e.target.value.indexOf(":") > -1) {
                            if (e.key == ":") e.preventDefault();
                        }
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    } else if (_.options.type == "datetime") {
                        var pattern = new RegExp("[0-9./-:AMPMampm ]");
                        var result = pattern.test(e.key);
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    }
                });
            } else {
                _.$elem.on('keydown', function (e) {
                    var pattern = null;
                    var result = null;
                    if (_.options.type == 'alphabet') {
                        pattern = new RegExp("[a-zA-Z -]");
                        result = pattern.test(e.key);
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    } else if (_.options.type == 'number') {
                        pattern = new RegExp("[0-9]");
                        result = pattern.test(e.key);
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    } else if (_.options.type == 'alphanumeric') {
                        pattern = new RegExp("[a-zA-Z 0-9-]");
                        result = pattern.test(e.key);
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        }
                    } else if (_.options.type == 'decimal') {
                        pattern = new RegExp("[0-9.]");
                        result = pattern.test(e.key);
                        var selectedLength = _.getSelected().toString().length;
                        if (e.keyCode == 110 || e.keyCode == 190) {
                            if (_.$elem.val().indexOf(".") > -1) {
                                e.preventDefault();
                            }
                        }
                        if (!result && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                            e.preventDefault();
                        } else {
                            if (_.$elem.val().indexOf(".") > -1) {
                                var l = _.$elem.val().split(".")[1].length;
                                if (selectedLength == 0) {
                                    if ((l > (_.options.decimalPlace - 1)) && !(e.keyCode == 8) && !(e.keyCode == 9)) {
                                        e.preventDefault();
                                    }
                                }
                            }
                        }
                    }
                });
            }
            return _;
        },
        getSelected: function () {
            var t = '';
            if (window.getSelection) {
                t = window.getSelection();
            } else if (document.getSelection) {
                t = document.getSelection();
            } else if (document.selection) {
                t = document.selection.createRange().text;
            }
            return t;
        },
        isDate: function (validDate) {
            var objDate,
                mSeconds,
                day,
                month,
                year;
            if (validDate.length !== 10) {
                return false;
            }
            if (validDate.substring(2, 3) !== '/' || validDate.substring(5, 6) !== '/') {
                return false;
            }
            month = validDate.substring(0, 2) - 1;
            day = validDate.substring(3, 5) - 0;
            year = validDate.substring(6, 10) - 0;

            if (year < 1000 || year > 3000) {
                return false;
            }

            mSeconds = (new Date(year, month, day)).getTime();
            objDate = new Date();
            objDate.setTime(mSeconds);
            if (objDate.getFullYear() !== year ||
                objDate.getMonth() !== month ||
                objDate.getDate() !== day) {
                return false;
            }
            return true;
        },
        loadDateFomatter: function (e, opts) {
            var _ = this;
            var v = e.target.value;
            e.target.value = v.replace(/^([\d]{1,2})([\d]{1,2})([\d]{2,4})$/, "$1/$2/$3");
            try {
                if (v == "") return;
                var separator = ".";
                v = v.replace(new RegExp("-", "g"), separator);

                if (v.indexOf(separator) > -1) {
                    if (v.split(separator).length == 2) {
                        var tmp = [];
                        var current = new Date(Date.parse(v));
                        tmp.push(current.getMonth() + 1);
                        tmp.push(current.getDate());
                        tmp.push(new Date().getFullYear());
                        e.target.value = moment(new Date(tmp.join("/"))).format(opts.format);
                    } else if (v.split(separator).length == 3) {
                        var tmp = [];
                        var current = new Date(Date.parse(v));
                        tmp.push(current.getMonth() + 1);
                        tmp.push(current.getDate());
                        tmp.push(current.getFullYear());
                        e.target.value = moment(new Date(tmp.join("/"))).format(opts.format);
                    }
                } else {
                    e.target.value = moment(new Date(e.target.value)).format(opts.format);
                }
            } catch (ee) {
                if (e.target.value == "") {
                    e.target.value = "";
                } else {
                    e.target.value = "Invalid Date";
                }
            }
            
        },
        loadTimeFomatter: function (e, opts) {
            var _ = this;
            e.target.value = e.target.value.replace(/^([\d]{1,2})([\d]{2})|([\D]{1,2})$/, "$1:$2 $3");
            try {
                var v = e.target.value.replace(".", ":");
                if (v == "") return;
                var d = new Date();
                var time = v.match(/(\d+)(?::(\d\d|\d))?\s*(p?)/);
                if ((e.target.value.indexOf("pm") > -1) || (e.target.value.indexOf("p") > -1)) {
                    if (parseInt(time[1]) >= 12) d.setHours(parseInt(time[1])); else d.setHours(parseInt(time[1]) + 12);
                } else {
                    d.setHours(parseInt(time[1]) + (time[3] ? 12 : 0));
                }
                d.setMinutes(parseInt(time[2]) || 0);
                e.target.value = moment(d).format(opts.format);
            } catch (ee) {
                if (e.target.value == "") {
                    e.target.value = "";
                } else {
                    e.target.value = "Invalid Time";
                }
            }

        },
        loadDateTimeFomatter: function (e, opts) {
            var _ = this;
            if (e.target.value == "") return;
            var tmp = e.target.value.split(" ");
            var tmpValue = [];

            try {
                tmpValue.push(loadDate(tmp[0]));
                tmpValue.push(loadTime(tmp[1]));
                e.target.value = moment(new Date(tmpValue.join(" "))).format(opts.format);

                function loadDate(v) {
                    v = v.replace(/^([\d]{1,2})([\d]{1,2})([\d]{2,4})$/, "$1/$2/$3");

                    var separator = ".";
                    v = v.replace(new RegExp("-", "g"), separator);

                    if (v.indexOf(separator) > -1) {
                        if (v.split(separator).length == 2) {
                            var tmp = [];
                            var current = new Date(Date.parse(v));
                            tmp.push(current.getMonth() + 1);
                            tmp.push(current.getDate());
                            tmp.push(new Date().getFullYear());
                            v = moment(new Date(tmp.join("/"))).format("MM/DD/YYYY");
                        } else if (v.split(separator).length == 3) {
                            var tmp = [];
                            var current = new Date(Date.parse(v));
                            tmp.push(current.getMonth() + 1);
                            tmp.push(current.getDate());
                            tmp.push(current.getFullYear());
                            v = moment(new Date(tmp.join("/"))).format("MM/DD/YYYY");
                        }
                    } else {
                        v = moment(new Date(v)).format("MM/DD/YYYY");
                    }
                    return v;
                }
                function loadTime(v) {
                    v = v.replace(/^([\d]{1,2})([\d]{2})|([\D]{1,2})$/, "$1:$2 $3");
                    v = v.replace(".", ":");
                    var d = new Date();
                    var time = v.match(/(\d+)(?::(\d\d|\d))?\s*(p?)/);
                    if ((v.indexOf("pm") > -1) || (v.indexOf("p") > -1)) {
                        if (parseInt(time[1]) >= 12) d.setHours(parseInt(time[1])); else d.setHours(parseInt(time[1]) + 12);
                    } else {
                        d.setHours(parseInt(time[1]) + (time[3] ? 12 : 0));
                    }
                    d.setMinutes(parseInt(time[2]) || 0);
                    v = moment(d).format("hh:mm A");
                    return v;
                }
            } catch (ee) {
                if (e.target.value == "") {
                    e.target.value = "";
                } else {
                    e.target.value = "Invalid Date Time";
                }
            }

        }
    };

    $.fn.input = function (opts) {
        return new input(this, opts).init();
    };
}))