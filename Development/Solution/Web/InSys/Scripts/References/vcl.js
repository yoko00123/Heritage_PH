/*!
 * VCL JavaScript library v2.0.1
 * (c) LJ Gomez 2016
 */
(function () {
    var DEBUG = true;
    (function (undefined) {
        var window = this || (0, eval)('this'),
		document = window['document'],
		navigator = window['navigator'],
		jQueryInstance = window["jQuery"],
		JSON = window["JSON"];

        (function (factory) {
            // Support three module loading scenarios
            if (typeof define === 'function' && define['amd']) {
                // [1] AMD anonymous module
                define(['exports', 'require'], factory);
            } else if (typeof exports === 'object' && typeof module === 'object') {
                // [2] CommonJS/Node.js
                factory(module['exports'] || exports); // module.exports is for Node.js
            } else {
                // [3] No module loader (plain <script> tag) - put directly in global namespace
                factory(window['vcl'] = {});
            }
        }
			(function (vclExports, amdRequire) {
			    var vcl = typeof vclExports !== 'undefined' ? vclExports : {};
			    vcl.exportSymbol = function (vclPath, object) {
			        var tokens = vclPath.split(".");
			        var target = vcl;
			        for (var i = 0; i < tokens.length - 1; i++)
			            target = target[tokens[i]];
			        target[tokens[tokens.length - 1]] = object;
			    };

			    vcl.exportProperty = function (owner, publicName, object) {
			        owner[publicName] = object;
			    };
			    vcl.version = "2.0.1";

			    vcl.exportSymbol('version', vcl.version);

			    vcl.classes = {}; //class inheritance collection

			    vcl.exportSymbol('classes', vcl.classes);

			    vcl.Options = (function () {
			        return {
			            Load: {
			                OnStart: function () { },
			                OnEnd: function () { }
			            },
			            Path: {
			                Root: ''
			            },
			            SystemParameters: [],
			            SystemSettings: []
			        }
			    }());

			    vcl.exportSymbol('Options', vcl.Options);
			    vcl.exportSymbol('Options.Load', vcl.Options.Load);
			    vcl.exportSymbol('Options.Path', vcl.Options.Path);
			    vcl.exportSymbol('Options.SystemParameters', vcl.Options.SystemParameters);
			    vcl.exportSymbol('Options.SystemSettings', vcl.Options.SystemSettings);


			    vcl.Utils = (function () {
			        function objectForEach(obj, action) {
			            for (var prop in obj) {
			                if (obj.hasOwnProperty(prop)) {
			                    action(prop, obj[prop]);
			                }
			            }
			        }

			        function extend(target, source) {
			            if (source) {
			                for (var prop in source) {
			                    if (source.hasOwnProperty(prop)) {
			                        target[prop] = source[prop];
			                    }
			                }
			            }
			            return target;
			        }

			        function setPrototypeOf(obj, proto) {
			            obj.__proto__ = proto;
			            return obj;
			        }

			        var canSetPrototype = ({
			            __proto__: []
			        } instanceof Array);

			        var canUseSymbols = !DEBUG && typeof Symbol === 'function';

			        var ieVersion = document && (function () {
			            var version = 3,
                            div = document.createElement('div'),
                            iElems = div.getElementsByTagName('i');

			            // Keep constructing conditional HTML blocks until we hit one that resolves to an empty fragment
			            while (
                            div.innerHTML = '<!--[if gt IE ' + (++version) + ']><i></i><![endif]-->',
                            iElems[0]) {
			            }
			            return version > 4 ? version : undefined;
			        }
                    ());
			        var isIe6 = ieVersion === 6,
                        isIe7 = ieVersion === 7;

			        return {
			            Extend: extend,
			            IEVersion: function () {
			                return ieVersion || null
			            }
			        }
			    }());

			    vcl.exportSymbol('Utils', vcl.Utils);
			    vcl.exportSymbol('Utils.Extend', vcl.Utils.Extend);
			    vcl.exportSymbol('Utils.IEVersion', vcl.Utils.IEVersion);

			    vcl.Core = (function () {
			        var _ = this;

			        this.StringFormat = function (text) {
			            var args = Array.prototype.slice.call(arguments, 1);
			            return text.replace(/{(\d+)}/g, function (match, number) {
			                return typeof args[number] != 'undefined'
							 ? args[number]
							 : match;
			            });
			        };

			        this.Post = function (o) {
			            var m = { url: null, controller: null, param: {} }
			            for (var v in o) { m[v] = o[v]; }
			            return $.post(_.ActionUrl(m.url, m.controller), m.param, function (data) {
			                data.Method = o.url;
			                data.Controller = o.controller;
			                data.param = o.param;
			                return data;
			            }).then(_.ResultAdapter);
			        };

			        this.PostCors = function (o) {
			            var m = {
			                url: null,
			                controller: null,
			                param: {},
			                callback: null,
			                othercallback: null,
			                errorcallback: null
			            }
			            for (v in o) {
			                m[v] = o[v];
			            }

			            //add security encryption here
			            //console.log(vcl.Data.Compress(m.param), '-', vcl.Sha1.hash(vcl.Data.Compress(m.param), 47));
			            m.param.tokenid = vcl.Sha1.hash(vcl.Data.Compress(m.param), 47);

			            // return;
			            $.ajax({
			                url: _.ActionUrl(m.url, m.controller),
			                type: "POST",
			                headers: {
			                    "Access-Control-Allow-Origin": "*",
			                    "Accept": "application/json; charset=utf-8",
			                    "Content-Type": "application/json; charset=utf-8",
			                },
			                data: m.param,
			                dataType: "json",
			                //The name of the function that's sent back
			                //Optional because JQuery will create random name if you leave this out
			                // jsonpCallback: "callback",
			                //This defaults to true if you are truly working cross-domain
			                //But you can change this for force JSONP if testing on same server
			                crossDomain: true,
			                xhrFields: {
			                    withCredentials: false
			                }
			            }).done(function (data) {
			                data.Method = o.url;
			                data.Controller = o.controller;
			                data.param = o.param;
			                _.ResultAdapter(data, function (result) {
			                    if (m.callback)
			                        m.callback(result);
			                }, m.othercallback, m.errorcallback);
			            }).fail(function (xhr, textStatus, errorThrown) {
			                if (m.errorcallback)
			                    m.errorcallback(textStatus);
			                console.log(xhr.responseText);
			            });
			        }

			        this.ResultAdapter = function (data) {
			            var def = $.Deferred();
			            if (typeof data === 'string') data = JSON.parse(data);
			            switch (data.Status) {
			                case 1:
			                    def.reject(data.ErrorMsg);
			                    break;
			                default:
			                    def.resolve(data.ResultSet, data.Status, data);
			                    break;
			            }

			            return def.promise();
			        };

			        this.Task = (function (ms) {
			            var def = $.Deferred();
			            setTimeout(def.resolve, ms || 100);
			            return def.promise();
			        });

			        this.ActionUrl = function (action, controller) {
			            var url;
			            if (controller.toLowerCase().indexOf("controller") >= 0) {
			                url = '/' + controller.replace('Controller', '') + '/' + action;
			            } else {
			                url = '/' + controller + '/' + action;
			            }
			            if (vcl.String.StartsWith(url, '/http'))
			                return url.substr(1);
			            return url;
			        };

			        this.IsNull = function (inp, oup) {
			            return (inp) ? inp : oup;
			        }

			        this.SQLFormat = function (obj, suffix) {
			            var s = obj;
			            if (obj === null || typeof obj === typeof undefined) {
			                s = "NULL";
			            }
			            else {
			                if (obj === true || obj === false || obj === 'true' || obj === 'false') s = (obj === true || obj === 'true') ? 1 : 0;
			                else if (isNaN(obj)) s = "'" + obj + ((suffix) ? '%' : '') + "'";
			            }
			            return s;
			        }

			    }) //()

			    vcl.exportSymbol('Core', vcl.Core);

			    vcl.Data = (function () {
			        return {
			            ToUri: function (obj) {
			                var j = Object.keys(obj);
			                var h = [];
			                for (var k in j) {
			                    if (typeof obj[j[k]] === 'function')
			                        console.error('warning!! Parameter ' + j[k] + ' is a function this will affect hash token to server');
			                    h.push(j[k] + '=' + obj[j[k]]);
			                }
			                return h.join('&');
			            },
			            Compress: function (data) {
			                var kys = Object.keys(data);
			                var j = [];
			                for (var k in kys) {
			                    if (typeof data[kys[k]] === 'undefined' || data[kys[k]] === null)
			                        throw new Error('(Not Enough Coffee) Parameter ' + kys[k] + ' is set to undefined');
			                    j.push(kys[k]);
			                    j.push(kys[k].length < data[kys[k]].toString().length ? data[kys[k]].toString().substr(0, kys[k].length) : data[kys[k]]);
			                }
			                return j.join('').replace(/�|'|_| /g, '').replace(/[^\w\s]/gi, '').toLowerCase();
			            },
			            utf8Encode: function (data) {
			                return unescape(encodeURIComponent(data));
			            },
			            utf8Decode: function (data) {
			                return decodeURIComponent(escape(data));
			            },
			            Parse: function (data, format) {
			                return new DOMParser().parseFromString(data, format || "text/html"); //maybe not supported by old browsers
			            }
			        }
			    }());

			    vcl.exportSymbol('Data', vcl.Data);
			    vcl.exportSymbol('Data.ToUri', vcl.Data.ToUri);
			    vcl.exportSymbol('Data.Compress', vcl.Data.Compress);
			    vcl.exportSymbol('Data.utf8Encode', vcl.Data.utf8Encode);
			    vcl.exportSymbol('Data.utf8Decode', vcl.Data.utf8Decode);
			    vcl.exportSymbol('Data.Parse', vcl.Data.Parse);

			    vcl.Sha1 = (function () {
			        return {
			            hash: function (msg, salt) {
			                msg = vcl.Data.utf8Encode(msg);

			                // constants [�4.2.1]
			                var K = [0x5a827999, 0x6ed9eba1, 0x8f1bbcdc, 0xca62c1d6];

			                // PREPROCESSING
			                msg += String.fromCharCode(0x80) + String.fromCharCode(salt); // add trailing '1' bit (+ 0's padding) to string [�5.1.1]
			                // convert string msg into 512-bit/16-integer blocks arrays of ints [�5.2.1]
			                var l = msg.length / 4 + 2; // length (in 32-bit integers) of msg + �1� + appended length
			                var N = Math.ceil(l / 16); // number of 16-integer-blocks required to hold 'l' ints
			                var M = new Array(N);

			                for (var i = 0; i < N; i++) {
			                    M[i] = new Array(16);
			                    for (var j = 0; j < 16; j++) { // encode 4 chars per integer, big-endian encoding
			                        //console.log(msg.charCodeAt(i*64+j*4) , i, j);
			                        M[i][j] = (msg.charCodeAt(i * 64 + j * 4) << 24) | (msg.charCodeAt(i * 64 + j * 4 + 1) << 16) |
									(msg.charCodeAt(i * 64 + j * 4 + 2) << 8) | (msg.charCodeAt(i * 64 + j * 4 + 3));
			                    } // note running off the end of msg is ok 'cos bitwise ops on NaN return 0
			                }
			                // add length (in bits) into final pair of 32-bit integers (big-endian) [�5.1.1]
			                // note: most significant word would be (len-1)*8 >>> 32, but since JS converts
			                // bitwise-op args to 32 bits, we need to simulate this by arithmetic operators
			                M[N - 1][14] = ((msg.length - 1) * 8) / Math.pow(2, 32);
			                M[N - 1][14] = Math.floor(M[N - 1][14]);
			                M[N - 1][15] = ((msg.length - 1) * 8) & 0xffffffff;

			                // set initial hash value [�5.3.1]
			                var H0 = 0x67452301;
			                var H1 = 0xefcdab89;
			                var H2 = 0x98badcfe;
			                var H3 = 0x10325476;
			                var H4 = 0xc3d2e1f0;

			                // HASH COMPUTATION [�6.1.2]

			                var W = new Array(80);
			                var a,
							b,
							c,
							d,
							e;
			                for (var i = 0; i < N; i++) {

			                    // 1 - prepare message schedule 'W'
			                    for (var t = 0; t < 16; t++)
			                        W[t] = M[i][t];
			                    for (var t = 16; t < 80; t++)
			                        W[t] = vcl.Sha1.ROTL(W[t - 3] ^ W[t - 8] ^ W[t - 14] ^ W[t - 16], 1);

			                    // 2 - initialise five working variables a, b, c, d, e with previous hash value
			                    a = H0;
			                    b = H1;
			                    c = H2;
			                    d = H3;
			                    e = H4;

			                    // 3 - main loop
			                    for (var t = 0; t < 80; t++) {
			                        var s = Math.floor(t / 20); // seq for blocks of 'f' functions and 'K' constants
			                        var T = (vcl.Sha1.ROTL(a, 5) + vcl.Sha1.f(s, b, c, d) + e + K[s] + W[t]) & 0xffffffff;
			                        e = d;
			                        d = c;
			                        c = vcl.Sha1.ROTL(b, 30);
			                        b = a;
			                        a = T;
			                    }

			                    // 4 - compute the new intermediate hash value (note 'addition modulo 2^32')
			                    H0 = (H0 + a) & 0xffffffff;
			                    H1 = (H1 + b) & 0xffffffff;
			                    H2 = (H2 + c) & 0xffffffff;
			                    H3 = (H3 + d) & 0xffffffff;
			                    H4 = (H4 + e) & 0xffffffff;
			                }

			                return vcl.Sha1.toHexStr(H0) + vcl.Sha1.toHexStr(H1) + vcl.Sha1.toHexStr(H2) +
							vcl.Sha1.toHexStr(H3) + vcl.Sha1.toHexStr(H4);
			            },
			            f: function (s, x, y, z) {
			                switch (s) {
			                    case 0:
			                        return (x & y) ^ (~x & z); // Ch()
			                    case 1:
			                        return x ^ y ^ z; // Parity()
			                    case 2:
			                        return (x & y) ^ (x & z) ^ (y & z); // Maj()
			                    case 3:
			                        return x ^ y ^ z; // Parity()
			                }
			            },
			            ROTL: function (x, n) {
			                return (x << n) | (x >>> (32 - n));
			            },
			            toHexStr: function (n) {
			                var s = "",
							v;
			                for (var i = 7; i >= 0; i--) {
			                    v = (n >>> (i * 4)) & 0xf;
			                    s += v.toString(16);
			                }
			                return s;
			            }
			        }
			    }())

			    vcl.exportSymbol('Sha1', vcl.Sha1);

			    vcl.Path = (function () {
			        return {
			            Combine: function () {
			                var args = Array.prototype.slice.call(arguments, 0);
			                var lst = [];
			                for (var i in args)
			                    lst.push(vcl.String.Trim(args[i], '/'));
			                return lst.join('/');
			            },
			            GetPage: function (fle) {
			                // $.get(o.page + '?=' + new Date().getTime(), function (dataml) { o.success($.parseHTML(dataml)); });
			                return vcl.Path.GetFile(fle).then(function (hl) {
			                    return vcl.Data.Parse(hl, 'text/html');
			                });
			            },
			            GetFile: function (fle) {
			                var def = $.Deferred();
			                var http = new vcl.AjaxRequest();
			                http.open('GET', fle + '?t=' + new Date().getTime(), true);
			                http.onreadystatechange = function () {
			                    if (http.readyState == 4 && http.status == 200)
			                        def.resolve(http.responseText.toString());
			                }
			                http.send();
			                return def.promise();
			            },
			            RootUrl: function () {
			                return window.location.toString().split("://")[0] + '://' + window.location.host.toString() + '/';
			            },
			            Exists: function (url, tag) {
			                var def = $.Deferred();
			                var http = new vcl.AjaxRequest();
			                http.open('HEAD', url, true);

			                http.onreadystatechange = function () {
			                    def.resolve(http.status !== 404, tag);
			                }

			                http.send();

			                return def.promise(); //http.status !== 404;
			            },
			            GetFileName: function (pth) {
			                return pth.substr(pth.lastIndexOf('/') + 1, pth.lastIndexOf('.') - pth.lastIndexOf('/') - 1)
			            },
			            Get: function (name) {
			                name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
			                var regexS = "[\\?&]" + name + "=([^&#]*)";
			                var regex = new RegExp(regexS);
			                var results = regex.exec(window.location.href);
			                if (results == null) return "";
			                else return results[1];
			            },
			            GetList: function () {
			                var vars = {};
			                var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
                            function (m, key, value) {
                                vars[key] = isNaN(value) ? value : parseFloat(value);
                            });
			                return vars;
			            }
			        }
			    }())

			    vcl.exportSymbol('Path', vcl.Path);
			    vcl.exportSymbol('Path.Combine', vcl.Path.Combine);
			    vcl.exportSymbol('Path.GetPage', vcl.Path.GetPage);
			    vcl.exportSymbol('Path.GetFile', vcl.Path.GetFile);
			    vcl.exportSymbol('Path.RootUrl', vcl.Path.RootUrl);
			    vcl.exportSymbol('Path.Exists', vcl.Path.Exists);
			    vcl.exportSymbol('Path.GetFileName', vcl.Path.GetFileName);
			    vcl.exportSymbol('Path.Get', vcl.Path.Get);
			    vcl.exportSymbol('Path.GetList', vcl.Path.GetList);

			    vcl.Cookie = (function (name, value, exdays) {
			        var setCookie = function (cname, cvalue, exdays) {
			            var d = new Date();
			            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
			            var expires = "expires=" + d.toGMTString();
			            document.cookie = cname + "=" + cvalue + "; " + expires;
			        }

			        var getCookie = function (cname) {
			            var name = cname + "=";
			            var ca = document.cookie.split(';');
			            for (var i = 0; i < ca.length; i++) {
			                var c = ca[i];
			                while (c.charAt(0) == ' ') c = c.substring(1);
			                if (c.indexOf(name) == 0) {
			                    return c.substring(name.length, c.length);
			                }
			            }
			            return "";
			        }

			        if (typeof value === typeof undefined) {
			            return getCookie(name);
			        } else {
			            setCookie(name, value, exdays);
			        }
			    })

			    vcl.exportSymbol('Cookie', vcl.Cookie);

			    vcl.String = (function () {
			        return {
			            StartsWith: function (string, prefix) {
			                return (string) ? string.toLowerCase().slice(0, prefix.length) == prefix.toLowerCase() : '';
			            },
			            EndsWith: function (string, suffix) {
			                return suffix == '' || string.toLowerCase().slice(-suffix.length) == suffix.toLowerCase();
			            },
			            LTrim: function (str, charlist) {
			                if (str === null || str == undefined) return;
			                if (charlist === undefined)
			                    charlist = "\s";
			                return str.replace(new RegExp("^[" + charlist + "]+"), "");
			            },
			            RTrim: function (str, charlist) {
			                if (str === null || str == undefined) return;
			                if (charlist === undefined)
			                    charlist = "\s";
			                return str.replace(new RegExp("[" + charlist + "]+$"), "");
			            },
			            Trim: function (str, charlist) {
			                return vcl.String.RTrim(vcl.String.LTrim(str, charlist), charlist);
			            },
			            PadLeft: function (str, pad) {
			                return (pad.toString() + str).substr(str.length);
			            }
			        }
			    }())

			    vcl.exportSymbol('String', vcl.String);
			    vcl.exportSymbol('String.StartsWith', vcl.String.StartsWith);
			    vcl.exportSymbol('String.EndsWith', vcl.String.EndsWith);
			    vcl.exportSymbol('String.LTrim', vcl.String.LTrim);
			    vcl.exportSymbol('String.RTrim', vcl.String.RTrim);
			    vcl.exportSymbol('String.Trim', vcl.String.Trim);
			    vcl.exportSymbol('String.PadLeft', vcl.String.PadLeft);

			    vcl.Array = (function () {
			        return {
			            Remove: function (obj, comparer) {
			                for (var i = obj.length - 1; i >= 0; --i) {
			                    if (comparer(obj[i]) === true)
			                        obj.splice(i, 1);
			                }
			            },
			            Distinct: function (obj, comparer) {
			                var arr = [];
			                var l = obj.length;
			                for (var i = 0; i < l; i++) {
			                    if (!vcl.Array.Contains(arr, obj[i], comparer))
			                        arr.push(obj[i]);
			                }
			                return arr;
			            },
			            Contains: function (obj, comparer) {
			                comparer = comparer || vcl.Array.DefaultEqualityComparer;
			                var l = obj.length;
			                while (l-- > 0)
			                    if (comparer(obj[l]) === true) return true;
			                return false;
			            },
			            DefaultEqualityComparer: function (a, b) {
			                return a === b || a.valueOf() === b.valueOf();
			            }
			        }
			    }())

			    vcl.exportSymbol('Array', vcl.Array);
			    vcl.exportSymbol('Array.Remove', vcl.Array.Remove);
			    vcl.exportSymbol('Array.Distinct', vcl.Array.Distinct);

			    vcl.Element = (function () {
			        return {
			            All: function (elem) {
			                elem = elem === null || elem === undefined ? document : $(elem)[0];
			                var eid = elem.querySelectorAll('[vcl-bind]');
			                var lst = [];
			                Enumerable.From(eid).ForEach(function (elem, index) {
			                    var attr = elem.getAttribute('vcl-bind');
			                    var pair = attr.split(',');
			                    var jsk = {};
			                    for (var i in pair) {
			                        var k = pair[i].split(':');
			                        jsk[k[0].trim()] = k[1].trim();
			                    }
			                    lst.push({
			                        element: elem,
			                        index: index,
			                        binding: jsk,
			                        //bindStr: attr
			                    });
			                });
			                return lst;
			            },
			            Query: function (elem, fltr) {
			                return Enumerable.From(vcl.Element.All(elem)).Where(function (x) {
			                    return Enumerable.From(x.binding).Where(fltr).Any();
			                });
			            },
			            Single: function (elem, key, value) {
			                if (key)
			                    if (value)
			                        return vcl.Element.Query(elem, function (x) { return x.Key === key && x.Value === value; }).SingleOrDefault();
			                    else
			                        return vcl.Element.Query(elem, function (x) { return x.Key === key; }).SingleOrDefault();
			                else
			                    return vcl.Element.All(elem)[0];
			            },
			            List: function (elem, key, value) {
			                if (key)
			                    if (value)
			                        return vcl.Element.Query(elem, function (x) { return x.Key === key && x.Value === value; }).ToArray();
			                    else
			                        return vcl.Element.Query(elem, function (x) { return x.Key === key; }).ToArray();
			                else
			                    return vcl.Element.All(elem);
			            }
			        }
			    }())

			    vcl.exportSymbol('Element', vcl.Element);
			    vcl.exportSymbol('Element.All', vcl.Element.All);
			    vcl.exportSymbol('Element.Query', vcl.Element.Query);
			    vcl.exportSymbol('Element.Single', vcl.Element.Single);
			    vcl.exportSymbol('Element.List', vcl.Element.List);

			    vcl.Encryption = (function () {
			        var constnum = "1234567890";

			        return {
			            Encrypt: function (data, key, flag) {
			                if (data === null || data === 'null') return data;
			                if (data === true || data === false || data === 'true' || data === 'false') return data;
			                if (!isNaN(data)) return (flag === true) ? parseFloat(data) * parseFloat(key) : parseFloat(data) / parseFloat(key);
			                var res = '',
                                i = 0,
                                j = 0;
			                while (i < data.length) {
			                    j = data.charCodeAt(i);
			                    res += String.fromCharCode(j ^ key);
			                    i++;
			                }
			                return res;
			            },
			            ToNum: function (data) {
			                var h = [], i = 0;
			                while (i < data.length) {
			                    h.push(parseInt(data.charCodeAt(i)));
			                    i++;
			                }
			                return parseFloat(h.join(''));
			            },
			            ToChar: function (num) {
			                var h = [], i = 0;
			                while (i < num.toString().length) {
			                    h.push(String.fromCharCode(i));
			                    i++;
			                }
			                return h.join('');
			            },
			            Chunk: function (num, sze) {
			                var h = [], curdata = [], i = 0, countr = 1;
			                while (i < num.toString().length) {
			                    curdata.push(num.toString().substr(i, 1));
			                    if (countr >= sze) {
			                        countr = 0;
			                        h.push(curdata.join(''));
			                        curdata = [];
			                    }
			                    countr++;
			                    i++;
			                }
			                var kj = num.toString().length % sze;
			                var lst = num.toString().substr(num.toString().length - kj, kj);
			                h[h.length - 1] = h[h.length - 1] + lst;
			                var g = '';
			                for (var k in h) g += String.fromCharCode(h[k]);
			                return g;
			            }
			        }
			    }())

			    vcl.exportSymbol('Encryption', vcl.Encryption);
			    vcl.exportSymbol('Encryption.Encrypt', vcl.Encryption.Encrypt);
			    vcl.exportSymbol('Encryption.ToNum', vcl.Encryption.ToNum);
			    vcl.exportSymbol('Encryption.ToChar', vcl.Encryption.ToChar);
			    vcl.exportSymbol('Encryption.Chunk', vcl.Encryption.Chunk);

			    /*
                   LJ 20150910
                   Date Formatter
                */
			    vcl.DateTime = (function () {
			        return {
			            Format: function () {
			                var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
                                timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
                                timezoneClip = /[^-+\dA-Z]/g,
                                pad = function (val, len) {
                                    val = String(val);
                                    len = len || 2;
                                    while (val.length < len) val = "0" + val;
                                    return val;
                                };

			                // Regexes and supporting functions are cached through closure
			                return function (date, mask, utc) {
			                    //var dF = vcl.DateTime.Format;
			                    if (vcl.DateTime.IsNewtonFormat(date))
			                        date = date.replace('T', ' ');

			                    // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
			                    if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
			                        mask = date;
			                        date = undefined;
			                    }

			                    // Passing date through Date applies Date.parse, if necessary
			                    date = date ? new Date(date) : new Date;
			                    if (isNaN(date)) throw SyntaxError("invalid date");

			                    //mask = String(dF.masks[mask] || mask || dF.masks["default"]);
			                    mask = String(vcl.DateTime.masks[mask] || mask || vcl.DateTime.masks['default']);

			                    // Allow setting the utc argument via the mask
			                    if (mask.slice(0, 4) == "UTC:") {
			                        mask = mask.slice(4);
			                        utc = true;
			                    }

			                    var _ = utc ? "getUTC" : "get",
                                    d = date[_ + "Date"](),
                                    D = date[_ + "Day"](),
                                    m = date[_ + "Month"](),
                                    y = date[_ + "FullYear"](),
                                    H = date[_ + "Hours"](),
                                    M = date[_ + "Minutes"](),
                                    s = date[_ + "Seconds"](),
                                    L = date[_ + "Milliseconds"](),
                                    o = utc ? 0 : date.getTimezoneOffset(),
                                    flags = {
                                        d: d,
                                        dd: pad(d),
                                        ddd: vcl.DateTime.i18n.dayNames[D], // dF.i18n.dayNames[D],
                                        dddd: vcl.DateTime.i18n.dayNames[D + 7],
                                        m: m + 1,
                                        mm: pad(m + 1),
                                        mmm: vcl.DateTime.i18n.monthNames[m],
                                        mmmm: vcl.DateTime.i18n.monthNames[m + 12],
                                        yy: String(y).slice(2),
                                        yyyy: y,
                                        h: H % 12 || 12,
                                        hh: pad(H % 12 || 12),
                                        H: H,
                                        HH: pad(H),
                                        M: M,
                                        MM: pad(M),
                                        s: s,
                                        ss: pad(s),
                                        l: pad(L, 3),
                                        L: pad(L > 99 ? Math.round(L / 10) : L),
                                        t: H < 12 ? "a" : "p",
                                        tt: H < 12 ? "am" : "pm",
                                        T: H < 12 ? "A" : "P",
                                        TT: H < 12 ? "AM" : "PM",
                                        Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                                        o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                                        S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
                                    };

			                    return mask.replace(token, function ($0) {
			                        return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
			                    });
			                };
			            }(),
			            masks: {
			                "default": "ddd mmm dd yyyy HH:MM:ss",
			                shortDate: "m/d/yy",
			                mediumDate: "mmm dd, yyyy",
			                longDate: "mmmm d, yyyy",
			                fullDate: "dddd, mmmm d, yyyy",
			                shortTime: "h:MM TT",
			                mediumTime: "h:MM:ss TT",
			                longTime: "h:MM:ss TT Z",
			                isoDate: "yyyy-mm-dd",
			                isoTime: "HH:MM:ss",
			                isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
			                isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'",
			                inSysDateTime: "hh:MM TT mmm dd, yyyy",
			                sometime: 'hhMMss',
			                shortDate2: "mm/dd/yyyy",
			                jDPInsysDate: "M dd, yyyy"
			            },
			            i18n: {
			                dayNames: [
                                "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
                                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
			                ],
			                monthNames: [
                                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
                                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
			                ]
			            },
			            JSONDate: function (jsonDate, format) {
			                //console.log(jsonDate.match(/\d+/)[0]);

			                var d = new Date(parseInt(jsonDate.substr(6)));

			                return vcl.DateTime.Format(d, format);
			            },
			            ShortDate2: function (dateValue) {
			                date = new Date();

			                if (typeof dateValue != 'undefined')
			                    date = dateValue;

			                return vcl.DateTime.Format(date, vcl.DateTime.masks.shortDate2)
			            },
			            Between: function (sdate, edate, callback, onFinish) {
			                try {
			                    var i = 0;
			                    var d = sdate;
			                    while (true) {
			                        if (d >= edate) {
			                            onFinish();
			                            break;
			                        } else
			                            callback(i, d);

			                        d.setTime(d.getTime() + 86400000);
			                        i++;
			                    }
			                } catch (ex) {
			                    console.error('vcl.DateTime.Between', ex);
			                }
			            },
			            ToDate: function (jsonDate) {
			                return new Date(parseInt(jsonDate.substr(6)));
			            },
			            IsNewtonFormat: function (dte) {
			                return /^(\d{4})-(\d{1,2})-(\d{1,2})T(\d{1,2}):(\d{1,2}):(\d{1,2})?.(\d{1,3})?$/.test(dte);
			                //return /^(\d{4})-(\d{1,2})-(\d{1,2})T(\d{1,2}):(\d{1,2}):(\d{1,2})+(.\d{3})?$/.test(dte);
			            }
			        }
			    }())

			    vcl.exportSymbol('DateTime', vcl.DateTime);
			    vcl.exportSymbol('DateTime.Format', vcl.DateTime.Format);
			    vcl.exportSymbol('DateTime.masks', vcl.DateTime.masks);
			    vcl.exportSymbol('DateTime.i18n', vcl.DateTime.i18n);
			    vcl.exportSymbol('DateTime.JSONDate', vcl.DateTime.JSONDate);
			    vcl.exportSymbol('DateTime.ShortDate2', vcl.DateTime.ShortDate2);
			    vcl.exportSymbol('DateTime.Between', vcl.DateTime.Between);
			    vcl.exportSymbol('DateTime.ToDate', vcl.DateTime.ToDate);
			    vcl.exportSymbol('DateTime.IsNewtonFormat', vcl.DateTime.IsNewtonFormat);

			    vcl.RootUrl = (function () {
			        return window.location.toString().split("://")[0] + '://' + window.location.host.toString() + '/';
			    });

			    vcl.exportSymbol('RootUrl', vcl.RootUrl);

			    vcl.Random = (function () {
			        return {
			            S4: function () {
			                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
			            },
			            GUID: function () {
			                return (vcl.Random.S4() + vcl.Random.S4() + "-" + vcl.Random.S4() + "-4" + vcl.Random.S4().substr(0, 3) + "-" + vcl.Random.S4() + "-" + vcl.Random.S4() + vcl.Random.S4() + vcl.Random.S4()).toLowerCase();
			            }
			        }
			    }())

			    vcl.exportSymbol('Random', vcl.Random);
			    vcl.exportSymbol('Random.S4', vcl.Random.S4);
			    vcl.exportSymbol('Random.GUID', vcl.Random.GUID);

			    vcl.AjaxRequest = (function () {
			        var activexmodes = ["Msxml2.XMLHTTP", "Microsoft.XMLHTTP"] //activeX versions to check for in IE
			        if (window.ActiveXObject) { //Test for support for ActiveXObject in IE first (as XMLHttpRequest in IE7 is broken)
			            for (var i = 0; i < activexmodes.length; i++) {
			                try {
			                    return new ActiveXObject(activexmodes[i])
			                }
			                catch (e) {
			                    //suppress error
			                }
			            }
			        }
			        else if (window.XMLHttpRequest) // if Mozilla, Safari etc
			            return new XMLHttpRequest()
			        else
			            return false
			    })

			    vcl.exportSymbol('AjaxRequest', vcl.AjaxRequest);

			    //add promises LJ 20160419
			    vcl.Require = (function (pth, script, tag) {
			        var Inject = function (pth, script, tag) {
			            var def = new $.Deferred();
			            var body = $(document.body);
			            var b = false;
			            var scrpt = $('<script />');
			            var url = pth + '.js?sdate=' + new Date().getTime(); //to update when necessary
			            scrpt.attr('type', "text/javascript");
			            scrpt.attr('src', url);
			            jQueryInstance.each(body.children(), function (i, j) {
			                if (j.nodeName.toLowerCase() == 'script') {
			                    var msrc = vcl.Path.GetFileName(j.src);
			                    if (msrc.toLowerCase() === pth.substr(pth.lastIndexOf('/') + 1).toLowerCase()) {
			                        b = true;
			                        return;
			                    }
			                }
			            });
			            if (!b)
			                body.append(scrpt);

			            setTimeout(function () {
			                def.resolve(script, tag);
			            }, 500);

			            return def.promise();
			        }


			        if (script instanceof Array) {
			            var g = [];
			            for (v in script)
			                g.push(Inject(vcl.Path.Combine(pth, script[v]), script[v], tag));
			            return $.when.apply($, g);
			        } else
			            return Inject(vcl.Path.Combine(pth, script), script, tag);
			    })

			    vcl.Require.Css = (function (pth, rlst) {
			        var head = $('head');
			        for (var ii in rlst) {
			            var b = false;
			            var css = $('<link />');
			            css.attr('rel', "stylesheet");
			            css.attr('href', vcl.Path.Combine(pth, rlst[ii] + '.css?sdate=' + new Date().getTime()));
			            $.each(head.children(), function (i, j) {
			                if (j.nodeName.toLowerCase() == 'link') {
			                    var msrc = vcl.Path.GetFileName(j.href);
			                    if (msrc.toLowerCase() === rlst[ii].toLowerCase()) {
			                        b = true;
			                        return;
			                    }
			                }
			            });
			            if (!b) head.append(css);
			        }
			    })

			    vcl.exportSymbol('Require', vcl.Require);
			    vcl.exportSymbol('Require.Css', vcl.Require.Css);

			    vcl.Get = (function (name) {
			        //check if exists
			        var j = Enumerable.From(Object.keys(vcl.classes)).Where(function (x) { return x.split('_')[0] === name });
			        if (!j.Any()) throw new Error('Object Not Found!');
			        if (j.Count() === 1)
			            return vcl.classes[j.Single()];
			        else
			            return j.Select(function (x) { return vcl.classes[x]; }).ToArray();
			    })

			    vcl.Remove = (function (name) {
			        var j = Enumerable.From(Object.keys(vcl.classes)).Where(function (x) { return x.split('_')[0] === name })
                    .ForEach(function (x) {
                        vcl.classes[x] = null;
                    })
			    })

			    vcl.Get.First = (function (name) {
			        var g = vcl.Get(name);
			        if (Array.isArray(g)) {
			            return g[0];
			        } else
			            return g;
			    })

			    vcl.Get.Last = (function (name) {
			        var g = vcl.Get(name);
			        if (Array.isArray(g)) {
			            return g[g.length - 1];
			        } else
			            return g;
			    })

			    vcl.exportSymbol('Get', vcl.Get);
			    vcl.exportSymbol('Get.First', vcl.Get.First);
			    vcl.exportSymbol('Get.Last', vcl.Get.Last);
			    vcl.exportSymbol('Remove', vcl.Remove);

			    vcl.Load = (function (elem, page, args) {
			        var e = vcl.Element.Single(null, 'ID', elem);
			        var LoadP = function (el, pg, args) {
			            return (function (el, pg, args) {
			                var def = $.Deferred();
			                $(el).load(vcl.Path.Combine('Views', pg) + '?t=' + new Date().getTime(), function (r, s, x) {
			                    if (s === 'error') {
			                        console.error(el, pg, 'Sorry but there was an error loading the page');
			                    } else {
			                        var partials = vcl.Element.List($(el), 'Partial');
			                        for (var p in partials)
			                            $(partials[p].element).html(vcl.Path.GetFile(vcl.Path.Combine(vcl.Options.Path.Root, 'Views', partials[p].binding.Partial)));

			                        var bnd = vcl.Element.Single($(el), 'Controller');
			                        if (bnd && bnd.binding.Controller) {
			                            vcl.Path.Exists(vcl.Path.Combine('Controllers', bnd.binding.Controller) + '.js').then(function (b) {
			                                if (!b) throw new Error('Controller ' + bnd.binding.Controller + ' Not Found');
			                                else
			                                    ko.cleanNode(bnd.element);
			                            })
                                        .then(function () {
                                            vcl.Require(vcl.Path.Combine('Controllers'), bnd.binding.Controller)
                                            .then(function (cls) {
                                                cls = cls.split('/')[cls.split('/').length - 1];
                                                //need to create a unique registration ID's
                                                var ID = cls + '_' + vcl.Random.S4();
                                                vcl.classes[ID] = new window[cls](args || vcl.Path.GetList());
                                                ko.applyBindings(vcl.classes[ID], bnd.element);
                                                def.resolve($(el)[0], vcl.classes[ID], args);
                                            });
                                        })
			                        } else
			                            def.resolve($(el)[0]); //if (cb) cb($(el)[0]);
			                    }
			                });
			                return def.promise();
			            }(el, pg, args));
			        }
			        if (typeof page === 'object') {
			            if (page.MasterPage) {
			                var prnt = LoadP(e.element, page.MasterPage, args);
			                return prnt.then(function (el, cls, args) {
			                    return LoadP(el.querySelector('[vcl-RenderBody]'), page.Page, args);
			                });
			            } else
			                return LoadP(e.element, page.Page, args);
			        }
			        else {
			            return LoadP(e.element, page, args);
			        }
			    })

			    vcl.exportSymbol('Load', vcl.Load);

			    vcl.html = (function () {
			        return {
			            Decode: function (value) {
			                return (value) ? value.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;') : value;
			            },
			            Encode: function (value) {
			                return (value) ? value.replace(/&amp;/g, '&').replace(/&lt;/g, '<').replace(/&gt;/g, '>') : value;
			            },
			            Clean: function (value) {
			                var regex = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>|<meta.*>/ig;
			                return value.replace(regex, '');
			            }
			        }
			    }())

			    vcl.exportSymbol('html', vcl.html);
			    vcl.exportSymbol('html.Decode', vcl.html.Decode);
			    vcl.exportSymbol('html.Encode', vcl.html.Encode);
			    vcl.exportSymbol('html.Clean', vcl.html.Clean);

			    if (!Function.prototype['bind']) {
			        // Function.prototype.bind is a standard part of ECMAScript 5th Edition (December 2009, http://www.ecma-international.org/publications/files/ECMA-ST/ECMA-262.pdf)
			        // In case the browser doesn't implement it natively, provide a JavaScript implementation. This implementation is based on the one in prototype.js
			        Function.prototype['bind'] = function (object) {
			            var originalFunction = this;
			            if (arguments.length === 1) {
			                return function () {
			                    return originalFunction.apply(object, arguments);
			                };
			            } else {
			                var partialArgs = Array.prototype.slice.call(arguments, 1);
			                return function () {
			                    var args = partialArgs.slice(0);
			                    args.push.apply(args, arguments);
			                    return originalFunction.apply(object, args);
			                };
			            }
			        };
			    }

			    vcl.Extend = (function (a) {
			        var args = Array.prototype.slice.call(arguments, 0);
			        vcl.Core.call(a, args);
			    })

			    vcl.exportSymbol('Extend', vcl.Extend);
                 
			}))
    }())
})()

