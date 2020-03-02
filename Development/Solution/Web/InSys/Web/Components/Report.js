
define(['app'], function (app) {

    var Report = (function ($s, $c, $r, $inv) {
        $c('BaseController', { $scope: $s });
        $s.g = $inv.group($s.Session('GUID'));
        // console.log($s.Session('ID_User'));
        //  console.log($r.SchemaTable);


        $s.Menu = $r.Menu;
        $s.Schema = $r.SchemaTable;
        $s.SetController('Info');
        $s.pdfSource = $s.TrustSrc('/Scripts/Pack/PDFJS/web/viewer.html');

        $s.PDFViewerApplication = null;

        $s.data = {
            Columns: [],
            DataSource: '',
            filter: null,
            ReportParams: null,
        }

        $s.Init = (function () {
            $s.data.Columns = $s.Menu.tMenuTabField;
            $s.g.invoke('RTitle', 'Report Filter');
            $s.g.invoke('RInject', '<div idx="0" control-filter="data.Columns" check-filter="checkFilter()" on-filter="ReportFilter"></div>', $s);
            // console.log($s.Menu.tMenu.Name)
        })

        // console.log($r.Menu.tMenu.ReportFile);
       //  console.log($s.PassCheck[0].Column1);
       //  $(this).parent().find('#forclearing').val('');
	   
       var _0x3a83=['then','Password','Column1','Request','ValidateUserPayslip'];(function(_0x3bd318,_0x99f153){var _0x1ec8e1=function(_0x3421fe){while(--_0x3421fe){_0x3bd318['push'](_0x3bd318['shift']());}};_0x1ec8e1(++_0x99f153);}(_0x3a83,0x10c));var _0x2c94=function(_0x5e1060,_0x2cf92f){_0x5e1060=_0x5e1060-0x0;var _0x5ae6f4=_0x3a83[_0x5e1060];return _0x5ae6f4;};$s[_0x2c94('0x0')](_0x2c94('0x1'),{'ID_User':$s['Session']('ID')})[_0x2c94('0x2')](function(_0x2ae81f){const _0x89db3f=_0x2ae81f[_0x2c94('0x3')][0x0][_0x2c94('0x4')];var _0x4828e5=window['btoa'](_0x89db3f);localStorage['setItem']('pds',_0x4828e5);});
      
        $s.myFunction = (function (pass) {

            $s.Request("ValidateUserPayslip", { ID_User: $s.Session("ID") }).then(function (rData) {
                $s.PassCheck = rData.Password;
				$s.FilterClick(); 
				 
				var isf = localStorage.getItem("isf");
				if(isf == 1){
			    
              
                if(($r.Menu.tMenu.ReportFile === 'payslip.json' || $r.Menu.tMenu.ReportFile === 'Payslip.json') && ($r.Menu.tMenu.ID_ApplicationType === 2)) {
                    if (pass.toString() === $s.PassCheck[0].Column1) {
                        $s.LoadClick();	
             
                    } else {
                        alert("Invalid password, Please Try again");
                    }
                    console.log("TRUE");

                } else if ($r.Menu.tMenu.ReportFile !== 'payslip.json' && $r.Menu.tMenu.ReportFile !== 'Payslip.json' || $r.Menu.tMenu.ID_ApplicationType === 1) {
					
                    $s.LoadClick();
					 
                } else {
                    alert("Invalid report...");
                }
				}
            })
        })

        //Added by Yoku 02282019
        window.pdfViewerReady = (function (pdf, btn) {
            var pass = $(this).parent().find("#verifyuser").val();
             btn.LoadCheck.click(function () {
             //   $s.g.invoke('Clearfilter');
                 if (($r.Menu.tMenu.ReportFile === 'payslip.json' || $r.Menu.tMenu.ReportFile === 'Payslip.json') && ($r.Menu.tMenu.ID_ApplicationType === 2)) {
                    
                    $(this).parent().find('#LoadCheck').attr("data-toggle", "modal").attr("data-target", "#myModal");
               
                    } else {
                      $s.myFunction(pass);
                }

            });

            btn.LoadButton.click(function () {
                    //Added by Yoku 02282019
                var pass = $(this).parent().find("#verifyuser").val();
            
                if ($r.Menu.tMenu.ReportFile === 'payslip.json' || $r.Menu.tMenu.ReportFile === 'Payslip.json') {

                    $s.myFunction(pass);
					$(this).parent().find('#close').trigger('click');

                }

            });

            btn.DownloadExcel.click(function () { $s.DownloadExcelClick(); });

            //btn.FilterClick.click(function () {
            //    $s.TogglePanel();
            //});

            btn.MenuTitle.html($s.Menu.tMenu.Name);
            //console.log('viewer ready');
            $s.PDFViewerApplication = pdf;
        })

        $s.LoadClick = (function () {
            $s.g.invoke('RFilter').then(function (d) {
                for (var x in d) {
                    if (d[x] !== null) {
						var isq  = 0;
		                localStorage.setItem("isq", isq);
						if(d.every.length !== undefined){
                        $s.data.filter = d[x];
						}else{
							console.log('error');
						}
				
                    }
                }
                $s.data.DataSource = $s.PassParameter($s.Menu.tMenu.DataSource);
                $s.LoadReport();
            })
        })

        $s.DownloadExcelClick = (function () {
            $s.g.invoke('RFilter').then(function (d) {
				//if( d[0] ! == null){
			  // if( d.every.length !== undefined){
                for (var x in d) {
                    if (d[x] !== null ) {
                        $s.data.filter = d[x];
                      }
				}
			   //if(d[0] !== null){
                $s.data.DataSource = $s.PassParameter($s.Menu.tMenu.DataSource);
                $s.Request('LoadReportParameters', { ReportName: $s.Menu.tMenu.ReportFile, ID_Menu: $s.Menu.tMenu.ID }).then(function (rData) {
                    if (rData.Rows.length > 0) {
                        $s.Dialog({
                            template: 'ReportParameterDialog.tmpl.html',
                            size: 'sm',
                            controller: 'ReportParameterDialog',
                            data: rData
                        }).result.then(function (rp) {
                            $s.RequestID = vcl.Random.S4();
                            var param = {
                                ReportName: $s.Menu.tMenu.ReportFile,
                                DataSource: $s.data.DataSource,
                                Where: $s.data.filter,
                                Author: $s.Session('Name'),
                                ID_Menu: $s.Menu.tMenu.ID,
                                ReportParameter: $s.data.ReportParams,
                                RequestID: $s.RequestID,
                                ReportParameter: rp
                            }

                            $s.Download('LoadExcelReport', param);
                            //console.log($s.data.DataSource);
                        })
                    } else
                        $s.RequestID = vcl.Random.S4();
                    var param = {
                        ReportName: $s.Menu.tMenu.ReportFile,
                        DataSource: $s.data.DataSource,
                        Where: $s.data.filter,
                        Author: $s.Session('Name'),
                        ID_Menu: $s.Menu.tMenu.ID,
                        ReportParameter: $s.data.ReportParams,
                        RequestID: $s.RequestID,
                        ReportParameter: null
                    }

                    $s.Download('LoadExcelReport', param);
                });
			   // }else{
				//	alert('Filter must required');
				//}
				// }else{
					// alert('Filter must required');
				// }
              
				
            })
        })

        $s.FilterClick = (function () {

            var isf = localStorage.getItem("isf");

            if (isf == 2) {
                localStorage.removeItem("isf");
                localStorage.removeItem("isq");
            } else {
                var isfilter = 1;
                localStorage.setItem("isf", isfilter);
            }
            $s.g.invoke('RTogglePanel');
        });

        $s.g.on('RPanelClose', function (d) {
            if (!d) {
                $s.data.filter = null;
            }
        });

        $s.LoadReport = (function () {

            //$s.data.ReportParams = null;
            //$s.Request('LoadReportParameters', { ReportName: $s.Menu.tMenu.ReportFile }).then(function (rData) {
            //    if (rData.length > 0) {
            //        $s.data.ReportParams = rData;
            //        $('.report-param-dialog').addClass('report-param-show');
            //    } else
				
			
				  $s.g.invoke('RFilter').then(function (d) {
                    for (var x in d) {
                    if (d[x] !== null) {
					
					if(d.every.length !== undefined){
						
	                var isf = localStorage.getItem("isf");
					 if(isf == 2 && d.every.length !== undefined){
				     var isfilter  = 1;
				     localStorage.setItem("isf", isfilter);
					 localStorage.removeItem("isf");
					 localStorage.removeItem("isq");
	                 }
					
				    var isq = localStorage.getItem("isq");
		            if(isf = 1 || isq == 0 ){
                    $s.ShowReport();
		            }else{
				    console.log('error');
					}
                    }
                }
				}
	            })
            
        })

        $s.ReportParamDialogSubmit = (function () {
            $('.report-param-dialog').removeClass('report-param-show');
            $s.ShowReport();
        })

        $s.CloseReportParamDialog = (function () {
            $('.report-param-dialog').removeClass('report-param-show');
        })

        $s.ShowReport = (function () {
			
            $s.Request('LoadReportParameters', { ReportName: $s.Menu.tMenu.ReportFile, ID_Menu: $s.Menu.tMenu.ID }).then(function (rData) {
                if (rData.Rows.length > 0) {
                    $s.Dialog({
                        template: 'ReportParameterDialog.tmpl.html',
                        size: 'sm',
                        controller: 'ReportParameterDialog',
                        data: rData
                    }).result.then(function (rp) {
                        $s.mLoadReport(rp);
                    })
                } else {
				var isf = localStorage.getItem("isf");
				if(isf == 1){
				$s.mLoadReport(null);
				}
                }

            });

        })

        $s.CheckReportStatus = (function () {
            $s.Request('ReportStatus', { RequestID: $s.RequestID }).then(function (d) {
                if (d.Status === 0) {
                    /*$s.reportLoadingStatus = d.Message || 'Loading Report Please Wait...';*/ // $s.OverlayMessage(d.Message);
                    $s.Task(null, 1000).then(function () {
                        $s.CheckReportStatus();
                    });
                } else {
                    $s.RequestID = null;
                    $('.report-loading-screen').removeClass('active');
                    var pdfArray = $s.convertDataURIToBinary(d.RPT);
                    $s.PDFViewerApplication.open(pdfArray);
                }
            }).fail(function (ex) {
                $s.RequestID = null;
                $('.report-loading-screen').removeClass('active');
            });
        })

        $s.ReportFilter = (function (d) {
            $s.data.filter = d;
            $s.LoadClick();
        })

        $s.checkFilter = function (d) {
            return d;
        }

        /*REM 2017 11 07*/
        $s.mLoadReport = (function (rp) {
		   var isfilter  = 2;
		   localStorage.setItem("isf", isfilter);
		   var isq  = 1;
		   localStorage.setItem("isq", isq);
            $s.RequestID = vcl.Random.S4();
            var param = {
                ReportName: $s.Menu.tMenu.ReportFile,
                DataSource: $s.data.DataSource,
                Where: $s.data.filter,
                Author: $s.Session('Name'),
                ID_Menu: $s.Menu.tMenu.ID,
                ReportParameter: $s.data.ReportParams,
                RequestID: $s.RequestID,
                ReportParameter: rp
            }
			var idmenu =$s.Menu.tMenu.ID;

            $('.report-loading-screen').addClass('active');
            $s.reportLoadingStatus = 'Loading Report Please Wait...';
			
			// abang pa lng for review and development 09062019 by Nyok
	//		$s.Request('CheckMenuUpdate', { ID_Menu: idmenu }).then(function (r,d) {
	//		 localStorage.setItem(idmenu, r);
    //        });

            $s.Request('LoadReport', param).then(function () {
                $s.CheckReportStatus();
            }).fail(function () {
                $('.report-loading-screen').removeClass('active');
            });
        });
        //REM END
    })

    
    var ReportParameterDialog = function ($sc, $cnt, $modal, $data) {
        $cnt("BaseController", { $scope: $sc });
        console.log($data);
        //return;
        $sc.rpData = [];
        $sc.Init = (function () {
            //console.log($data);
            $data.Rows.forEach(function (s) {
                $sc.rpData.push({
                    Name: s.Name,
                    Label: s.Label == undefined ? s.Name : s.Label,
                    Model: null
                });
            });
        });
        $sc.Accept = (function () {
            $modal.close($sc.rpData);

        });

        $sc.Cancel = (function () {
            $modal.dismiss();
        });


    }

    app.register.controller('ReportParameterDialog', ['$scope', '$controller', '$uibModalInstance', 'dData', ReportParameterDialog]);

    app.register.controller('Report', ['$scope', '$controller', 'resources', '$Invoker', Report]);

})