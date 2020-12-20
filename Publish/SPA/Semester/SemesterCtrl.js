app.controller("SemesterCtrl", function ($scope, $filter, $http, blockUI) {

    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    getSemesterTypeActive();
    dateToday = new Date();
    var year = dateToday.getFullYear();
    yearLimit = 5;
    $scope.yearList = [];
    for (i = 0; i < yearLimit; i++) {
        $scope.yearList.push({ "YearId": year + i });
    }

    $('#dtFrom').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtTo').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtAddDropStart').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtAddDropEnd').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtWithdrawStart').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtWithdrawEnd').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtFrom').keypress(function (e) {
        e.preventDefault();
    });   
    $('#dtTo').keypress(function (e) {
        e.preventDefault();
    });
    $('#dtAddDropStart').keypress(function (e) {
        e.preventDefault();
    });
    $('#dtAddDropEnd').keypress(function (e) {
        e.preventDefault();
    });
    $('#dtWithdrawStart').keypress(function (e) {
        e.preventDefault();
    }); 
    $('#dtWithdrawEnd').keypress(function (e) {
        e.preventDefault();
    }); 

    function clear() {
        $scope.entity = { SemesterId: 0, IsActive: true};
        $("#txtFocus").focus();
    };

    //$scope.yearList = [{
    //    "YearId": "2020",
    //}, {
    //    "YearId": "2021",
    //}, {
    //    "YearId": "2022",

    //}]
    //$scope.yearList = [{
    //    "YearId": parseInt(year),
    //}, {
    //        "YearId": parseInt(year)+1,
    //}, {
    //        "YearId": parseInt(year)+2,

    //}]
    $scope.setSemesterCode = function () {
        var semester = "";
        var year = "";

        if (!angular.isUndefined($scope.entity.SemesterTypeName) && $scope.entity.SemesterTypeName !== null) {
            semester = $scope.entity.SemesterTypeName;
        }

        if (!angular.isUndefined($scope.entity.YearId) && $scope.entity.YearId !== null) {
            year = $scope.entity.YearId;
        }

        $scope.entity.SemesterName = semester + " " + year;
    }

    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Semester/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

                angular.forEach(data, function (obj) {
                    var res1 = obj.StartDate.substring(0, 5);
                    if (res1 == "/Date") {
                        var parsedDate1 = new Date(parseInt(obj.StartDate.substr(6)));
                        var date1 = ($filter('date')(parsedDate1, 'MMM dd, yyyy')).toString();
                        obj.StartDate = date1;
                    }

                    var res2 = obj.EndDate.substring(0, 5);
                    if (res2 == "/Date") {
                        var parsedDate2 = new Date(parseInt(obj.EndDate.substr(6)));
                        var date2 = ($filter('date')(parsedDate2, 'MMM dd, yyyy')).toString();
                        obj.EndDate = date2;
                    }
                    var res3 = obj.AddDropStartDate.substring(0, 5);
                    if (res3 == "/Date") {
                        var parsedDate3 = new Date(parseInt(obj.AddDropStartDate.substr(6)));
                        var date3 = ($filter('date')(parsedDate3, 'MMM dd, yyyy')).toString();
                        obj.AddDropStartDate = date3;
                    }
                    var res4 = obj.AddDropEndDate.substring(0, 5);
                    if (res4 == "/Date") {
                        var parsedDate4 = new Date(parseInt(obj.AddDropEndDate.substr(6)));
                        var date4 = ($filter('date')(parsedDate4, 'MMM dd, yyyy')).toString();
                        obj.AddDropEndDate = date4;
                    }
                    var res5 = obj.WithdrawStartDate.substring(0, 5);
                    if (res5 == "/Date") {
                        var parsedDate5 = new Date(parseInt(obj.WithdrawStartDate.substr(6)));
                        var date5 = ($filter('date')(parsedDate5, 'MMM dd, yyyy')).toString();
                        obj.WithdrawStartDate = date5;
                    }
                    var res6 = obj.WithdrawEndDate.substring(0, 5);
                    if (res6 == "/Date") {
                        var parsedDate6 = new Date(parseInt(obj.WithdrawEndDate.substr(6)));
                        var date6 = ($filter('date')(parsedDate6, 'MMM dd, yyyy')).toString();
                        obj.WithdrawEndDate = date6;
                    }
                })

                $scope.entityList = data;
                $scope.total_count = data.length;

                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end);
            }
            else {
                $scope.lsitBlock.stop();
                //alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/Semester/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    $scope.resetForm();
                    getList();
                    alertify.log(data, 'success', '5000');
                }
                else {
                    $scope.entryBlock.stop();
                    alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                }
            }
            else {
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            $scope.entryBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    $scope.GetPaged = function (curPage) {
        $scope.currentPage = curPage;
        $scope.PerPage = (angular.isUndefined($scope.PerPage) || $scope.PerPage == null) ? $scope.DefaultPerPage : $scope.PerPage;

        if ($scope.PerPage > 100) {
            $scope.PerPage = 100;
            alertify.log('Maximum record  per page is 100', 'error', '5000');
        }
        else if ($scope.PerPage < 1) {
            $scope.PerPage = 1;
            alertify.log('Minimum record  per page is 1', 'error', '5000');
        }

        var begin = ($scope.PerPage * (curPage - 1));
        var end = begin + $scope.PerPage;

        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    $scope.post = function (trnType) {
        var where = "SemesterName = '" + $scope.entity.SemesterName + "'";
        if ($scope.entity.SemesterId > 0)
            where += " AND SemesterId <> " + $scope.entity.SemesterId;

        $http({
            url: '/Semester/GetDynamic?where=' + where + '&orderBy=SemesterName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.SemesterName + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.SemesterId === 0 ? "INSERT" : "UPDATE";
                    submitRequest(trnType);
                }

                else {
                    trnType = "DELETE";

                    alertify.set({
                        labels: {
                            ok: "Yes",
                            cancel: "No"
                        },
                        buttonReverse: true
                    });

                    alertify.confirm('Are you sure to delete?', function (e) {
                        if (e) {
                            submitRequest(trnType);
                        }
                    });
                }
            }
        });
    };
    function getSemesterTypeActive() {
        var where = "IsActive = 1";
        $http({
            url: '/Semester/SemesterTypeGetDynamic?where=' + where + '&orderBy=SemesterTypeId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.semesterTypeList = data;
            }
        });
    }

    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.cmbSemester = "-- Semester --";
        $scope.cmbYear = "-- Year --";
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };
});