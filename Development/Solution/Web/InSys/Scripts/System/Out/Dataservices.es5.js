'use strict';
define(['app'], function (app) {

    app.provider("DataService", function () {
        var _ = this;
        _.Controller = 'Action';
        _.AppName = 'Web';
        _.Encryption = {
            Token: null, Salt: null
        };

        _.ResultAdapter = function (rdata, requestDate) {
            var d = {};
            var data = rdata; //.data;
            if (typeof data === 'string') {
                data = JSON.parse(data);
            }

            //Token = CryptoJS.Encrypt(GenPrefix, EncContext.EncKey, EncContext.EncSalt),
            //    Salt = CryptoJS.Encrypt(SaltPrefix, EncContext.EncKey, EncContext.EncSalt),
            //    Date = unixTime.ToString(),
            //    Guid

            var rToken = (_.Encryption.Token + requestDate).substr(0, 32);
            var rSalt = (_.Encryption.Salt.substr(0, rToken.length / 4) + _.Encryption.Guid + requestDate).substr(0, rToken.length / 2); //vcl.Cookie('APID')

            if (data.Status === 1) throw new Error(data.ErrorMsg);

            if (typeof data.ResultSet === 'number' || typeof data.ResultSet === 'object') {
                d.ResultSet = data.ResultSet;
            } else {
                if (data.ResultSet) {
                    d.ResultSet = JSON.parse(NickCrypt.Decrypt(data.ResultSet, rToken, rSalt)); //  LZString.decompressFromEncodedURIComponent(data.ResultSet));
                }
            }

            d.Status = data.Status;
            d.ErrorMsg = data.ErrorMsg;
            d.data = rdata;

            return d;
        };

        _.EncryptData = function (name, obj) {

            var date = new Date().getTime() / 1e3 | 0;
            var rToken = (_.Encryption.Token + date + name).substr(0, 32);
            var rSalt = _.Encryption.Salt.substr(0, rToken.length / 4) + _.Encryption.Guid + date; //vcl.Cookie('APID')
            var k = JSON.stringify(obj || {});

            k = NickCrypt.Encrypt(k, rToken, rSalt.substr(0, rToken.length / 2));

            return {
                Date: date,
                Data: k
            };
        };

        return {
            SetDefaultController: function SetDefaultController(value) {
                _.Controller = value;
            },
            SetAppName: function SetAppName(value) {
                _.AppName = value;
            },
            $get: ["$http", '$q', function ($http, $q) {
                return {
                    SetController: function SetController(value) {
                        _.Controller = value;
                    },
                    GetController: function GetController() {
                        return _.Controller;
                    },
                    SetEncryption: function SetEncryption(token) {
                        //_.Encryption = {
                        //    Token: token,
                        //    Salt: salt
                        //};
                        _.Encryption = JSON.parse(token);
                    },
                    Post: function Post(name, param, CustomController, disableInterceptor) {
                        var deferred = $.Deferred();
                        var vPath = window.VirtualPath || '';

                        var hash = _.EncryptData(name, param);

                        var p = {
                            url: vPath + "/api/" + (CustomController || _.Controller) + "/" + name,
                            method: "POST",
                            data: hash.Data, //LZString.compressToEncodedURIComponent(JSON.stringify(param || {})),
                            dataType: "json",
                            disableInterceptor: disableInterceptor == undefined ? false : disableInterceptor,
                            headers: {
                                "Request-Date": hash.Date
                            }
                        };

                        $http(p).then(function (d) {
                            try {
                                var j = _.ResultAdapter(d.data, d.headers('Request-Date'));
                                deferred.resolve(j.ResultSet, j.Status);
                            } catch (ex) {
                                deferred.reject(ex);
                            }
                        })['catch'](deferred.reject);

                        return deferred.promise();
                    },
                    ByteLength: function ByteLength(str) {
                        // returns the byte length of an utf8 string
                        var s = str.length;
                        for (var i = str.length - 1; i >= 0; i--) {
                            var code = str.charCodeAt(i);
                            if (code > 0x7f && code <= 0x7ff) s++;else if (code > 0x7ff && code <= 0xffff) s += 2;
                            if (code >= 0xDC00 && code <= 0xDFFF) i--; //trail surrogate
                        }
                        return s;
                    },
                    UrlApi: function UrlApi(name) {
                        var vPath = window.VirtualPath || '';
                        return vPath + "/api/" + _.Controller + "/" + name;
                    },
                    Upload: function Upload(postUrl, payload) {
                        var deferred = $.Deferred(); //$q.defer();
                        var vPath = window.VirtualPath || '';
                        var hash = _.EncryptData(name, null);
                        $http.post(vPath + "/api/" + _.Controller + "/" + postUrl, payload, {
                            headers: {
                                'Content-Type': undefined,
                                "Request-Date": hash.Date
                            },
                            transformRequest: function transformRequest(data) {
                                return data;
                            }
                        }).then(function (d) {
                            try {
                                var j = _.ResultAdapter(d.data, d.headers('Request-Date'));
                                deferred.resolve(j.ResultSet, j.Status);
                            } catch (ex) {
                                deferred.reject(ex);
                            }
                        })['catch'](deferred.reject);

                        return deferred.promise();
                    },
                    Download: function Download(name, param, CustomController) {
                        var _ = this;
                        return this.Post(name, param, CustomController).then(function (mFile) {
                            return _.DownloadSlim(mFile);
                        });
                    },
                    DownloadSlim: function DownloadSlim(mFile, Container) {
                        var vPath = window.VirtualPath || '';

                        var cntr = '';
                        if (Container) {
                            cntr = '&c=' + LZString.compressToEncodedURIComponent(Container);
                        }

                        // Use an arraybuffer
                        return $http.get(vPath + "/api/Action/DownloadFile?f=" + mFile + cntr, {
                            responseType: 'arraybuffer',
                            headers: {
                                "Request-Date": new Date().getTime() / 1e3 | 0
                            }
                        }).then(function (data) {
                            try {
                                var octetStreamMime = 'application/octet-stream';
                                var success = false;

                                // Get the filename from the x-filename header or default to "download.bin"
                                var filename = data.headers('x-filename') || 'download.bin';

                                // Determine the content type from the header or default to "application/octet-stream"
                                var contentType = data.headers('content-type') || octetStreamMime;

                                try {
                                    // Try using msSaveBlob if supported
                                    console.log("Trying saveBlob method ...");
                                    var blob = new Blob([data.data], { type: contentType });
                                    if (navigator.msSaveBlob) navigator.msSaveBlob(blob, filename);else {
                                        // Try using other saveBlob implementations, if available
                                        var saveBlob = navigator.webkitSaveBlob || navigator.mozSaveBlob || navigator.saveBlob;
                                        if (saveBlob === undefined) throw "Not supported";
                                        saveBlob(blob, filename);
                                    }
                                    console.log("saveBlob succeeded");
                                    success = true;
                                } catch (ex) {
                                    console.log("saveBlob method failed with the following exception:");
                                    console.log(ex);
                                }

                                if (!success) {
                                    // Get the blob url creator
                                    var urlCreator = window.URL || window.webkitURL || window.mozURL || window.msURL;
                                    if (urlCreator) {
                                        // Try to use a download link
                                        var link = document.createElement('a');
                                        if ('download' in link) {
                                            // Try to simulate a click
                                            try {
                                                // Prepare a blob URL
                                                console.log("Trying download link method with simulated click ...");
                                                var blob = new Blob([data.data], { type: contentType });
                                                var url = urlCreator.createObjectURL(blob);
                                                link.setAttribute('href', url);

                                                // Set the download attribute (Supported in Chrome 14+ / Firefox 20+)
                                                link.setAttribute("download", filename);

                                                // Simulate clicking the download link
                                                var event = document.createEvent('MouseEvents');
                                                event.initMouseEvent('click', true, true, window, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
                                                link.dispatchEvent(event);
                                                console.log("Download link method with simulated click succeeded");
                                                success = true;
                                            } catch (ex) {
                                                console.log("Download link method with simulated click failed with the following exception:");
                                                console.log(ex);
                                            }
                                        }

                                        if (!success) {
                                            // Fallback to window.location method
                                            try {
                                                // Prepare a blob URL
                                                // Use application/octet-stream when using window.location to force download
                                                console.log("Trying download link method with window.location ...");
                                                var blob = new Blob([data.data], { type: octetStreamMime });
                                                var url = urlCreator.createObjectURL(blob);
                                                window.location = url;
                                                console.log("Download link method with window.location succeeded");
                                                success = true;
                                            } catch (ex) {
                                                console.log("Download link method with window.location failed with the following exception:");
                                                console.log(ex);
                                            }
                                        }
                                    }
                                }

                                if (!success) {
                                    // Fallback to window.open method
                                    console.log("No methods worked for saving the arraybuffer, using last resort window.open");
                                    window.open(httpPath, '_blank', '');
                                }
                            } catch (ex) {
                                console.error(ex);
                            }
                        })['catch'](function (data, status) {
                            console.log("Request failed with status: " + status);
                        });
                    },
                    GetMenu: function GetMenu(ID_Menu) {
                        return this.Post('GetMenu', { ID_Menu: ID_Menu });
                    },
                    GetWebWidget: function GetWebWidget(ID_Widget) {
                        return this.Post('GetWidget', { ID_Menu: ID_Widget });
                    },
                    AppName: _.AppName
                };
            }]
        };
    });

    //Factories
    app.factory('Session', function ($http, $rootScope) {

        var _ = this;

        _.DataBank = {};
        _.SessionData = {};

        return {
            UserRow: function UserRow(value) {
                if (value) {
                    //var UserRow = value;
                    //UserRow.ID_Session = UserRow.ID;
                    //delete UserRow.ID;
                    //var key = Object.keys(UserRow);
                    //var keycode = new Date().getTime();
                    //keycode = parseInt(keycode.toString().substr(keycode.toString().length / 2));
                    //keycode = parseInt(Math.sqrt(keycode));

                    //window.localStorage.setItem('Date', new Date());
                    //for (var i in key) {
                    //    window.localStorage.setItem(key[i], vcl.Encryption.Encrypt(UserRow[key[i]], keycode, true));
                    //}
                    //window.localStorage.setItem('SessionID', keycode);
                    _.SessionData = value;
                    _.SessionData.ID_Session = _.SessionData.ID;
                    delete _.SessionData.ID;
                } else {
                    //var j = {};
                    //var keycode = window.localStorage.getItem('SessionID');
                    //for (var i = 0; i < window.localStorage.length; i++) {
                    //    if (window.localStorage.key(i) === 'SessionID') continue;
                    //    j[window.localStorage.key(i)] = vcl.Encryption.Encrypt(window.localStorage.getItem(window.localStorage.key(i)), keycode, false);
                    //    var f = j[window.localStorage.key(i)];
                    //    f = (!isNaN(f)) ? parseFloat(f) : f;
                    //    if (f === 'true' || f === 'false') f = (f == 'true') ? true : false;
                    //    else if (f === 'null') f = null;
                    //    j[window.localStorage.key(i)] = f;
                    //}
                    //return j;
                    return _.SessionData;
                }
            },
            Session: function Session(name) {
                //var bh = vcl.Encryption.Encrypt(window.localStorage.getItem(name), window.localStorage.getItem('SessionID'), false);
                //if (bh === null || bh === 'null') return null;
                //return bh === 'true' || bh === 'false' ? (bh === 'true' ? true : false) : bh;
                return _.SessionData[name];
            },
            Clear: function Clear() {
                // window.localStorage.clear();
                _.SessionData = {};
            },
            DataBank: function DataBank(Name, Value) {
                if (Value) _.DataBank[Name] = Value;else return _.DataBank[Name];
            }
        };
    });

    app.factory('$Invoker', function () {
        return {
            events: {},
            group: function group(ID) {
                var _ = this;

                if (_.events[ID] == null) _.events[ID] = []; //register Group ID;

                return {
                    on: function on(Name, Action) {
                        _.on(ID, Name, Action);
                    },
                    invoke: function invoke(Name) {
                        try {
                            var infodata = [];
                            var args = Array.prototype.slice.call(arguments, 1);
                            _.events[ID].forEach(function (d) {
                                if (Name === d.Name) infodata.push(d.Action.apply(this, args));
                            });
                            return $.when.apply(undefined, infodata).promise().then(function () {
                                return Array.prototype.slice.call(arguments, 0);
                            }).fail(function (x) {
                                console.error(x);
                            });
                        } catch (ex) {
                            console.error(Name, ex);
                            throw new Error("Method " + Name + ' not found');
                        }
                    },
                    all: function all(Name) {
                        try {
                            var infodata = [];
                            var args = Array.prototype.slice.call(arguments, 1);
                            var kys = Object.keys(_.events);

                            for (var ii = 0; ii < kys.length; ii++) _.events[kys[ii]].forEach(function (d) {
                                if (Name === d.Name) infodata.push(d.Action.apply(this, args));
                            });

                            return $.when.apply(undefined, infodata).promise().then(function () {
                                return Array.prototype.slice.call(arguments, 0);
                            }).fail(function (x) {
                                console.error(x);
                            });
                        } catch (ex) {
                            console.error(Name, ex);
                            throw new Error("Method " + Name + ' not found');
                        }
                    },
                    clear: function clear() {
                        _.clear(ID);
                    }
                };
            },
            on: function on(ID, Name, Action) {
                var _ = this;

                //vcl.Array.Remove(this.events[ID], function (x) { return x.ID === ID && x.Name === Name }); //check constraints

                this.events[ID].push({ Name: Name, Action: Action });
            },
            invoke: function invoke(ID, Name) {
                try {
                    var infodata = [];
                    var args = Array.prototype.slice.call(arguments, 2);

                    this.events[ID].forEach(function (d) {
                        if (Name === d.Name) infodata.push(d.Action.apply(this, args));
                    });
                    return $.when.apply(undefined, infodata).promise().then(function () {
                        return Array.prototype.slice.call(arguments, 0);
                    }).fail(function (x) {
                        console.error(x);
                    });
                } catch (ex) {
                    console.error(Name, ex);
                    throw new Error("Method " + Name + ' not found');
                }
            },
            clear: function clear(ID) {
                delete this.events[ID]; //remove object
                // vcl.Array.Remove(this.events, function (x) { return x.ID === ID });
            }
        };
    });

    //Filters

    //take all whitespace out of string
    app.filter('nospace', function () {
        return function (value) {
            return !value ? '' : value.replace(/ /g, '');
        };
    });

    //replace uppercase to regular case
    app.filter('humanizeDoc', function () {
        return function (doc) {
            if (!doc) return;
            if (doc.type === 'directive') {
                return doc.name.replace(/([A-Z])/g, function ($1) {
                    return '-' + $1.toLowerCase();
                });
            }

            return doc.label || doc.name;
        };
    });
});

