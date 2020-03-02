
define([], function () {
    return (function ($s, $c, $r, $info, $cp) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });

        $s.mTree = null;
        $s.mtUserGroupMenu = 'tUserGroupMenu';
        $s.mMenuTreeTab = {}
        $s.tree = {
            treeBase: null,
            treeJson: null,
            treeData: null
        }

        $s.AfterInit = (function () {
            return $s.NewTable($s.mtUserGroupMenu).then(function (d) {
                d.MenuDetailTab.Name = 'Menu';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;
                d.MenuDetailTab.ID_MenuDetailTabType = 0; //not set means we need to register it manually
                d.MenuDetailTab.SeqNo = 4;

                $s.AddTable(d);
                $s.mMenuTreeTab = d;
                $s.WaitForElement('div#mdt-' + d.MenuDetailTab.ID).then(function (g) {
                    $.get("/Web/Template/MenuTreeView.tmpl.html", function (d) {
                        var newElement = angular.element(d);// $cp(angular.element(d))($s);
                        g.append(newElement);

                        $s.mTree = new dhtmlXTreeObject("tree-Menu", "100%", "100%", 0);
                        $s.mTree.setImagePath("/Scripts/Pack/dhtmlx/dhxtree_material/");
                        $s.mTree.enableCheckBoxes(1);
                        $s.mTree.enableThreeStateCheckboxes(false);
                    });

                    $s.g.invoke('InitReady', d.MenuDetailTab.ID);

                });
            })
        })

        //@Override
        $s.FormLoad = (function () {
            $s.NoTransactionTables.push($s.mtUserGroupMenu);
            $s.LoadData().then(function () {
                $s.WaitForElement('div#mdt-' + $s.mMenuTreeTab.MenuDetailTab.ID).then(function (g) {
                    return $s.LoadMenuData();
                });
            })
        });

        $s.LoadMenuData = (function () {
            return $s.Request("LoadTable", { DataSource: 'vMenu' })
                .then(function (d) {
                    try {
                        $s.tree.treeJson = { id: 0, text: "All", item: [], child: 1 }; 
                        $s.mTree.deleteChildItems(0);
                        $s.PopulateTreeView(d, $s.tree.treeJson, null, 'ID_Menu');
                        $s.mTree.parse($s.tree.treeJson, "json");
                        return $s.LoadUserMenuData();
                    } catch (ex) {
                        console.error(ex);
                    }
                })
        })

        $s.LoadUserMenuData = (function () {
            return $s.Request('ListSource', { TableName: 'v' + $s.mtUserGroupMenu.substr(1), childcolumn: 'ID_UserGroup', value: $s.data.Row.ID }).then(function (d) {
                $s.data.TreeBase = d;
                $s.tree.treeData = d;
                Enumerable.From($s.tree.treeData).Select('$.ID_Menu').ForEach(function (x) {
                    $s.mTree.setCheck(x, true);
                });
            })
        })


        $s.bLoad = (function () {
            $s.LoadMenuData();
        })

        //@LJ 20161011
        //--> from GSCOM
        $s.PopulateTreeView = (function (DataSource, nc, vID, pParentColumn) {
            try {
                var s;
                if (!vID)
                    s = '$.' + pParentColumn + ' === null';
                else
                    s = '$.' + pParentColumn + ' ===  ' + vID;

                var dra = Enumerable.From(DataSource).Where(s).OrderBy('$.' + pParentColumn).ThenBy('$.SeqNo').ThenBy('$.ID');
                dra.ForEach(function (dr) {
                    var n = { id: dr['ID'], text: dr['Name'], item: [], child: 0 };
                    nc.item.push(n);
                    n.child = 1;
                    $s.PopulateTreeView(DataSource, n, dr['ID'], pParentColumn);
                })
            } catch (ex) {
                throw ex;
            }
        })

        $s.g.on('GetDetailData', function () {
            return $s.Task().then(function () {
                try {

                    var f = Enumerable.From($s.mTree.getAllChecked().split(",")).Where(function (x) { return !isNaN(x) && parseFloat(x) > 0 }).Select(function (x) {
                        var g = { ID: 0 };
                        g['ID_Menu'] = parseFloat(x)
                        return g
                    }).ToArray();

                    $s.mTree.getAllPartiallyChecked().split(",").forEach(function (y) {
                        if ($s.IsNull(y, "") != "") {
                            f.push({ ID: 0, ID_Menu: y });
                        }
                    })

                    return {
                        TableName: $s.mtUserGroupMenu,
                        Data: f,
                        Deleted: Enumerable.From($s.data.TreeBase).Where(function (x) { return $s.IsNull(x.ID, 0) > 0 }).Select(function (x) { return x.ID }).ToArray()
                    };
                } catch (Ex) {
                    console.error('Menu Tree', Ex);
                }
            });
        });

        
    })
})
