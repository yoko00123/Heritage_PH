

(function (Handsontable) {

    var onBeforeKeyDown = function (event) {
        var instance = this; // context of listener function is always set to Handsontable.Core instance
        var editor = instance.getActiveEditor();
        var selectedIndex = editor.select.selectedIndex;

        //Handsontable.dom.enableImmediatePropagation(event);
        console.log(event.keyCode);
        switch (event.keyCode) {
            case 38: //Handsontable.helper.keyCode.ARROW_UP
                var previousOption = editor.select.options[selectedIndex - 1];

                if (previousOption) { // if previous option exists
                    editor.select.value = previousOption.value; // mark it as selected
                }

                event.stopImmediatePropagation(); // prevent EditorManager from processing this event
                event.preventDefault(); // prevent browser from scrolling the page up
                break;

            case 40: //Handsontable.helper.keyCode.ARROW_DOWN
                var nextOption = editor.select.options[selectedIndex + 1];

                if (nextOption) { // if previous option exists
                    editor.select.value = nextOption.value; // mark it as selected
                }
                event.stopImmediatePropagation(); // prevent EditorManager from processing this event
                event.preventDefault(); // prevent browser from scrolling the page up
                break;
        }
    }

    //Dropdown
    var SelectEditor = Handsontable.editors.BaseEditor.prototype.extend();

    SelectEditor.prototype.init = function () {
        // Create detached node, add CSS class and make sure its not visible
        this.select = document.createElement('SELECT');
        Handsontable.dom.addClass(this.select, 'htSelectEditor');
        this.select.style.position = 'absolute';
        this.select.style.display = 'none';

        // Attach node to DOM, by appending it to the container holding the table
        this.instance.rootElement.appendChild(this.select);
    };

    // Create options in prepare() method
    SelectEditor.prototype.prepare = function () {
        // Remember to invoke parent's method
        Handsontable.editors.BaseEditor.prototype.prepare.apply(this, arguments);

        var selectOptions = this.cellProperties.selectOptions;
        var sobj = this.select;
        var options;

        selectOptions.then(function (data) {
            options = data.DataList;
            Handsontable.dom.empty(sobj);

            for (var option in options) {
                if (options.hasOwnProperty(option)) {
                    var optionElement = document.createElement('OPTION');
                    optionElement.value = options[option].ID;
                    Handsontable.dom.fastInnerHTML(optionElement, options[option].Name);
                    sobj.appendChild(optionElement);
                }
            }
        })

        // console.log(selectOptions);

        //if (typeof selectOptions == 'function') {
        //    options = this.prepareOptions(selectOptions(this.row,
        //    this.col, this.prop))
        //} else {
        //    options = this.prepareOptions(selectOptions);
        //} 
    };

    SelectEditor.prototype.getValue = function () {
        return this.select.value;
    };

    SelectEditor.prototype.setValue = function (value) {
        this.select.value = value;
    };

    SelectEditor.prototype.open = function () {
        var width = Handsontable.dom.outerWidth(this.TD);
        // important - group layout reads together for better performance
        var height = Handsontable.dom.outerHeight(this.TD);
        var rootOffset = Handsontable.dom.offset(this.instance.rootElement);
        var tdOffset = Handsontable.dom.offset(this.TD);
        var editorSection = this.checkEditorSection();
        var cssTransformOffset;

        switch (editorSection) {
            case 'top':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.vertical.clone.wtTable.holder.parentNode);
                break;
            case 'left':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.horizontal.clone.wtTable.holder.parentNode);
                break;
            case 'corner':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.corner.clone.wtTable.holder.parentNode);
                break;
        }
        var selectStyle = this.select.style;

        if (cssTransformOffset && cssTransformOffset !== -1) {
            selectStyle[cssTransformOffset[0]] = cssTransformOffset[1];
        } else {
            Handsontable.dom.resetCssTransform(this.select);
        }

        selectStyle.height = height + 'px';
        selectStyle.minWidth = width + 'px';
        selectStyle.top = tdOffset.top - rootOffset.top + 'px';
        selectStyle.left = tdOffset.left - rootOffset.left + 'px';
        selectStyle.margin = '0px';
        selectStyle.display = '';

        this.instance.addHook('beforeKeyDown', onBeforeKeyDown);
    };

    SelectEditor.prototype.close = function () {
        this.select.style.display = 'none';
        // remove listener
        this.instance.removeHook('beforeKeyDown', onBeforeKeyDown);
    };

    SelectEditor.prototype.checkEditorSection = function () {
        if (this.row < this.instance.getSettings().fixedRowsTop) {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'corner';
            } else {
                return 'top';
            }
        } else {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'left';
            }
        }
    };

    SelectEditor.prototype.focus = function (event) {
        this.select.focus();
    }

    Handsontable.editors.registerEditor('mtSelect', SelectEditor);

    // -------------------------------------------------------------------------------------------- DataLookUp

    var LookUpEditor = Handsontable.editors.BaseEditor.prototype.extend();

    LookUpEditor.prototype.init = function () {
        // Create password input and update relevant properties
        this.dvInput = document.createElement('input');
        this.dvInput.setAttribute('type', 'text');
        this.dvInput.setAttribute('ng-model', 'RowVal');
        this.dvInput.setAttribute('look-up-input', '');
        this.dvInput.style.border = 'none';

        this.drpContainer = document.createElement('SPAN');
        this.drpContainer.classList = 'input-group-btn lu';
        this.drpContainer.setAttribute('uib-dropdown', '');
        this.drpContainer.setAttribute('on-toggle', 'fld.OnLookUpOpen(open)');
        this.drpContainer.setAttribute('is-open', 'fld.IsLookUpOpen');
        this.drpContainer.setAttribute('auto-close', 'disabled');

        //button
        this.drpButton = document.createElement('BUTTON');
        this.drpButton.classList = 'btn btn-default fa fa-folder lookup-btn';
        this.drpButton.type = 'button';
        this.drpButton.setAttribute('uib-dropdown-toggle', '');

        this.drpContainer.appendChild(this.drpButton);

        this.drpLookUp = document.createElement('look-up-div');
        this.drpLookUp.classList = 'dropdown-menu look-up';
        this.drpLookUp.setAttribute('uib-dropdown-menu', '');

        this.drpContainer.appendChild(this.drpLookUp);

        this.dvContainer = document.createElement('DIV');
        this.dvContainer.className = 'input-group htLookUpEditor';
        this.dvContainer.style.display = 'none';
        this.dvContainer.style.position = 'absolute';
        this.dvContainer.setAttribute('look-up', 'fld');
        this.dvContainer.setAttribute('look-up-set', 'onSet');
        this.dvContainer.setAttribute('look-up-tab-fields', 'Columns');
        this.dvContainer.setAttribute('look-up-type', '2');
        this.dvContainer.setAttribute('look-up-width', 'getWidth');

        this.dvContainer.appendChild(this.dvInput);
        this.dvContainer.appendChild(this.drpContainer);

        // Attach node to DOM, by appending it to the container holding the table
        this.instance.rootElement.appendChild(this.dvContainer);

    };

    // Create options in prepare() method
    LookUpEditor.prototype.prepare = function () {
        // Remember to invoke parent's method
        Handsontable.editors.BaseEditor.prototype.prepare.apply(this, arguments);
        var cp = this.cellProperties;
        var _ = this;

        if (!this.IsCompiled) {

            cp.$s.getWidth = function () {
                return Handsontable.dom.outerWidth(_.TD);
            }

            cp.$s.onSet = (function (row) {
                _.cellProperties.DataRow = row;
                _.cellProperties.OnSet(_.instance, _.TD, _.row, _.col, _.prop, row, _.cellProperties);
                _.instance.destroyEditor();
            })

            cp.$s.RowVal = null;

            cp.compile(angular.element(this.dvContainer))(cp.$s);
            _.IsCompiled = true;
        }

        this.cellProperties.DataRow = {};
    };

    LookUpEditor.prototype.getValue = function () {
        return this.cellProperties.DataRow['ID'];
    };

    LookUpEditor.prototype.setValue = function (value) {
        if (this.cellProperties.DataRow)
            this.cellProperties.$s.RowVal = this.cellProperties.DataRow['Name'];
        //RowVal
        // this.dvInput.value = value;
    };

    LookUpEditor.prototype.open = function () {
        var $s = this.cellProperties.$s;

        var width = Handsontable.dom.outerWidth(this.TD);
        var height = Handsontable.dom.outerHeight(this.TD);
        var rootOffset = Handsontable.dom.offset(this.instance.rootElement);
        var tdOffset = Handsontable.dom.offset(this.TD);

        // sets select dimensions to match cell size
        this.dvContainer.style.height = (height - 4) + 'px';
        this.drpButton.style.height = (height - 2) + 'px';

        this.dvContainer.style.width = (width - 1) + 'px';
        this.drpButton.style.width = 28 + 'px';
        this.drpButton.style.lineHeight = '8px';
        this.dvInput.style.width = ((width - 1) - 26) + 'px';

        // make sure that list positions matches cell position
        this.dvContainer.style.top = tdOffset.top - rootOffset.top + 'px';
        this.dvContainer.style.left = ((tdOffset.left - rootOffset.left) - 1) + 'px';
        this.dvContainer.style.margin = '0px';

        // display the list
        this.dvContainer.style.display = '';

    };

    LookUpEditor.prototype.close = function () {
        this.dvContainer.style.display = 'none';
    };

    LookUpEditor.prototype.checkEditorSection = function () {
        if (this.row < this.instance.getSettings().fixedRowsTop) {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'corner';
            } else {
                return 'top';
            }
        } else {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'left';
            }
        }
    };

    LookUpEditor.prototype.focus = function (event) {
        this.dvInput.focus();
    }

    Handsontable.editors.registerEditor('mtdatalookup', LookUpEditor);

    // ----------------------------------------------------------------------------------------------- mTable

    var TableEditor = Handsontable.editors.BaseEditor.prototype.extend();

    TableEditor.prototype.init = function () {
        var _ = this;
        // Create detached node, add CSS class and make sure its not visible
        var UID = vcl.Random.S4();
        this.select = document.createElement('INPUT');
        this.select.setAttribute('type', 'text');
        this.select.setAttribute('drp-input', UID);
        Handsontable.dom.addClass(this.select, 'htTableEditor');
        this.select.style.display = 'none';

        // Attach node to DOM, by appending it to the container holding the table
        this.instance.rootElement.appendChild(this.select);

        this.selectContainer = document.createElement('DIV');
        this.selectContainer.setAttribute('class', 'drp-dropdown');
        this.selectContainer.setAttribute('drp-lookup', UID);

        this.innerConatiner = document.createElement('DIV');
        this.innerConatiner.setAttribute('class', 'drp-inner');
        this.selectContainer.appendChild(this.innerConatiner);

        this.closeBtn = document.createElement('BUTTON');
        this.closeBtn.style.width = '30px';
        this.closeBtn.style.height = '30px';
        this.closeBtn.style.position = 'absolute';
        this.closeBtn.style.right = '-30px';
        this.closeBtn.style.top = 0;
        this.closeBtn.setAttribute('class', 'fa fa-close');
        this.closeBtn.onclick = (function () {
            _.instance.destroyEditor();
        })

        this.selectContainer.appendChild(this.closeBtn);

        this.instance.rootElement.appendChild(this.selectContainer);

        $(this.select).keyup(function () {
            _.hot.loadData(_.getData());
            _.hot.render();
        });
    };

    // Create options in prepare() method
    TableEditor.prototype.prepare = function () {
        // Remember to invoke parent's method
        Handsontable.editors.BaseEditor.prototype.prepare.apply(this, arguments);

        //var row = this.instance.getSourceDataAtRow(_.instance.getSelected()[0]); 
        //var fxfltr = this.cellProperties.FixedFilter(row); //getSelected

        //fxfltr.then(function (ssdd) {
        //    tableOptions.filter = ssdd;

        //    $s.Request('LoadCombo', tableOptions).then(function (data) {
        //        _.cellProperties.DataList = data.DataList;
        //        TableEditor.prototype.DataList = data.DataList;
        //        TableEditor.prototype.Page = {
        //            CurrentPage: 1,
        //            Limit: 10
        //        }

        //        _.hot.loadData(_.getData());
        //    })
        //})
    };

    TableEditor.prototype.getData = (function () {
        var inp = this.select;
        var fld = this.cellProperties.mField;
        return Enumerable.From(this.cellProperties.DataList).Where(function (x) {
            return new RegExp(inp.value + ".*", "gi").test(fld.Name.substr(0, 3) === 'ID_' ? x[fld.Text || 'Name'] : x[fld.Text || fld.Name] || x['Name']); ///x[fld.Text || 'Name'].toLowerCase().indexOf(inp.value.toLowerCase()) != -1;
        }).Take(this.Page.Limit || 10).ToArray();
    })

    TableEditor.prototype.getValue = function () {
        var fld = this.cellProperties.mField;
        var jj = this.select.getAttribute('nValue');
        if (jj)
            return fld.Name.substr(0, 3) === 'ID_' ? parseFloat(jj) : jj;
    };

    TableEditor.prototype.setValue = function (value) {
        var fld = this.cellProperties.mField;
        var jjj = Enumerable.From(this.cellProperties.tableOptions).Where(function (x) {
            return fld.Name.substr(0, 3) === 'ID_' ? x.ID == parseFloat(value) : (x[fld.Text || fld.Name] || x['Name']) === value;
        }).Select(function (x) { return x[fld.Text || 'Name'] });
        if (jjj.Any())
            this.select.value = jjj.Single();
        else
            this.select.value = null;
    };

    TableEditor.prototype.Page = {
        CurrentPage: 1,
        Limit: 10
    }

    TableEditor.prototype.open = function () {
        var width = Handsontable.dom.outerWidth(this.TD);
        // important - group layout reads together for better performance
        var height = Handsontable.dom.outerHeight(this.TD);
        var rootOffset = Handsontable.dom.offset(this.instance.rootElement);
        var tdOffset = Handsontable.dom.offset(this.TD);
        var editorSection = this.checkEditorSection();
        var cssTransformOffset;

        switch (editorSection) {
            case 'top':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.vertical.clone.wtTable.holder.parentNode);
                break;
            case 'left':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.horizontal.clone.wtTable.holder.parentNode);
                break;
            case 'corner':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.corner.clone.wtTable.holder.parentNode);
                break;
        }
        var selectStyle = this.select.style;
        var conStyle = this.selectContainer.style;

        if (cssTransformOffset && cssTransformOffset !== -1) {
            selectStyle[cssTransformOffset[0]] = cssTransformOffset[1];
            conStyle[cssTransformOffset[0]] = cssTransformOffset[1];
        } else {
            Handsontable.dom.resetCssTransform(this.select);
            Handsontable.dom.resetCssTransform(this.selectContainer);
        }

        selectStyle.height = (height - 5) + 'px';
        selectStyle.width = (width - 3) + 'px';
        selectStyle.top = tdOffset.top - rootOffset.top + 'px';
        selectStyle.left = tdOffset.left - rootOffset.left + 'px';
        selectStyle.margin = '0px';
        selectStyle.display = '';

        conStyle.minWidth = width + 'px';
        conStyle.top = (tdOffset.top - rootOffset.top) + height + 5 + 'px';
        conStyle.left = tdOffset.left - rootOffset.left + 'px';
        conStyle.margin = '0px';
        conStyle.display = '';

        var _ = this;
        var fld = this.cellProperties.mField;
        var inStyle = $(this.selectContainer);

        this.hot = new Handsontable(this.innerConatiner, {
            colHeaders: fld.ListColumn.split(','), //
            autoColumnSize: true,
            readonly: true,
            contextMenu: false, //['row_above', 'row_below', 'remove_row']
            stretchH: 'all',
            manualColumnResize: true,
            columns: Enumerable.From(fld.ListColumn.split(',')).Select(function (x) {
                var gg = { data: x.trim(), editor: false };

                return gg;
            }).ToArray(),
            rowHeaders: false,
            width: inStyle.width(),
            height: inStyle.height(),
            afterSelection: function (ev, data) {
                var nRow = _.hot.getSourceDataAtRow(ev);
                _.select.value = nRow[fld.Text || 'Name'];

                _.select.setAttribute('nValue', (fld.Name.substr(0, 3) === 'ID_' ? nRow.ID : nRow[fld.Text || fld.Name] || nRow['Name']));
                _.cellProperties.DataRow = nRow;
                _.instance.destroyEditor();
            }
        });

        //this.instance.addHook('beforeKeyDown', onBeforeKeyDown); 
        var tableOptions = this.cellProperties.tableOptions;
        var $s = this.cellProperties.scope;

        var row = this.instance.getSourceDataAtRow(_.instance.getSelected()[0]);
        var fxfltr = this.cellProperties.FixedFilter(row); //getSelected

        fxfltr.then(function (ssdd) {
            tableOptions.filter = ssdd;

            $s.Request('LoadCombo', tableOptions).then(function (data) {
                _.cellProperties.DataList = data.DataList;

                $s.SetCurEditorList(_.row, _.col, data.DataList);

                _.hot.loadData(_.getData());
            })
        })
    };

    TableEditor.prototype.close = function () {
        this.cellProperties.scope.OnCellEditorClosed(this.row, this.col, this.cellProperties, this.select.value);
        this.select.value = null;
        this.select.removeAttribute('nValue');
        this.select.style.display = 'none';
        this.selectContainer.style.display = 'none';
        // remove listener
        //this.instance.removeHook('beforeKeyDown', onBeforeKeyDown);
    };

    TableEditor.prototype.checkEditorSection = function () {
        if (this.row < this.instance.getSettings().fixedRowsTop) {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'corner';
            } else {
                return 'top';
            }
        } else {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'left';
            }
        }
    };

    TableEditor.prototype.focus = function (event) {
        this.select.focus();
    }

    Handsontable.editors.registerEditor('mtTable', TableEditor);

    var TimeEditor = Handsontable.editors.BaseEditor.prototype.extend();
    TimeEditor.prototype.tmpValue = null;
    TimeEditor.prototype.init = function () {

        this.clock = document.createElement('INPUT');
        Handsontable.dom.addClass(this.clock, 'clockInput');
        this.clock.style.position = 'absolute';
        this.clock.style.display = 'none';
        this.instance.rootElement.append(this.clock);

        this.clockInput = $('.clockInput').datetimepicker({ sideBySide: false, debug: false, format: "hh:mm A", focusOnShow: true }).on('dp.show', function () {
            $(".bootstrap-datetimepicker-widget").addClass("no-border");
        });
    };

    TimeEditor.prototype.open = function () {
        var width = Handsontable.dom.outerWidth(this.TD) - 3;
        var height = Handsontable.dom.outerHeight(this.TD) - 6;
        var rootOffset = Handsontable.dom.offset(this.instance.rootElement);
        var tdOffset = Handsontable.dom.offset(this.TD);
        var editorSection = this.checkEditorSection();
        var cssTransformOffset;

        switch (editorSection) {
            case 'top':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.vertical.clone.wtTable.holder.parentNode);
                break;
            case 'left':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.horizontal.clone.wtTable.holder.parentNode);
                break;
            case 'corner':
                cssTransformOffset = Handsontable.dom.getCssTransform(this.instance.view.wt.wtScrollbars.corner.clone.wtTable.holder.parentNode);
                break;
        }
        var clockStyle = this.clock.style;

        if (cssTransformOffset && cssTransformOffset !== -1) {
            selectStyle[cssTransformOffset[0]] = cssTransformOffset[1];
        } else {
            Handsontable.dom.resetCssTransform(this.clock);
        }

        clockStyle.height = height + 'px';
        clockStyle.minWidth = width + 'px';
        clockStyle.top = tdOffset.top - rootOffset.top + 'px';
        clockStyle.left = ((tdOffset.left - rootOffset.left)-1) + 'px';
        clockStyle.margin = '0px';
        clockStyle.display = '';
        clockStyle.textIndent = '3px';
        this.clock.focus();
        
    };

    TimeEditor.prototype.close = function () {
        this.clock.style.display = 'none';
    };
    TimeEditor.prototype.getValue = function () {
        var nd = new Date(this.tmpValue);
        var dd = moment(nd).format("YYYY-MM-DD");
        var tt = $('.clockInput').val();
        var nDate = dd + " " + tt;
        return nDate;
    };
    TimeEditor.prototype.setValue = function (newValue) {
        if (newValue == null || newValue == "") {
            newValue = new Date();
        }
        this.tmpValue = newValue;
        var nd = new Date(this.tmpValue);
        var tt = moment(nd).format("hh:mm A");
        this.clock.value = tt;
    };
    TimeEditor.prototype.focus = function () { };
    TimeEditor.prototype.checkEditorSection = function () {
        if (this.row < this.instance.getSettings().fixedRowsTop) {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'corner';
            } else {
                return 'top';
            }
        } else {
            if (this.col < this.instance.getSettings().fixedColumnsLeft) {
                return 'left';
            }
        }
    };

    Handsontable.editors.TimeEditor = TimeEditor;
    Handsontable.editors.registerEditor('time', TimeEditor);

}(Handsontable))
