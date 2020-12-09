app.controller("FeesCollectionCtrl", function ($scope, $filter, $cookieStore, $window, $location, $http, blockUI) {

    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.selectedCourseId = 0;
    $scope.entityList = [];
    $scope.entityCourseList = [];
    $scope.entityListPaged = [];
    //$scope.schedule = {};
    $scope.scheduleList = [];
    $scope.scheduleListDisplay = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    getStudentName();
    function clear() {
        $scope.entity = { PaymentId: 0, IsActive: true };
        $("#txtFocus").focus();
    };
    function getStudentName() {
        var where = "IsActive = 1";
        $http({
            url: '/Admission/GetDynamic?where=' + where + '&orderBy=FullName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.studentList = data;
            }
        });
    }
    $scope.payList = [{
        "PayMethodId": 1,
        "PayMethodNo": "Cash"
    },
    {
        "PayMethodId": 2,
        "PayMethodNo": "Cheque"
    },
    {
        "PayMethodId": 3,
        "PayMethodNo": "Card"
    }
    ]

    
    $('#chequeDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#chequeDate').keypress(function (e) {
        e.preventDefault();
    }); $('#cDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#cDate').keypress(function (e) {
        e.preventDefault();
    });
    $('#fDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#fDate').keypress(function (e) {
        e.preventDefault();
    });
    $('#tDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#tDate').keypress(function (e) {
        e.preventDefault();
    });

    function getList() {
        var where = "L.IsActive = 1";
        $http({
            url: '/FeesCollection/GetDynamic?where=' + where + '&orderBy=L.StudentId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                angular.forEach(data, function (obj) {
                    var res = obj.PaymentDate.substring(0, 5);
                    if (res == "/Date") {
                        var parsedDate = new Date(parseInt(obj.PaymentDate.substr(6)));
                        var date = ($filter('date')(parsedDate, 'MMM dd, yyyy')).toString();
                        obj.PaymentDate = date;
                    }

                })
                $scope.entityList = data;
                $scope.total_count = data.length;

            }
            else {
                alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }
        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    }
    $scope.Search = function () {
        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var dateSplit = $scope.entity.FromDate.split(' ');
        var date = dateSplit[1].replace(',', '');
        var year = dateSplit[2];
        var month;
        for (var j = 0; j < months.length; j++) {
            if (dateSplit[0] == months[j]) {
                month = months.indexOf(months[j]) + 1;
            }
        }
        var fromDate = year + "-" + month + "-" + date;


        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var dateSplit = $scope.entity.ToDate.split(' ');
        var date = dateSplit[1].replace(',', '');
        var year = dateSplit[2];
        var month;
        for (var j = 0; j < months.length; j++) {
            if (dateSplit[0] == months[j]) {
                month = months.indexOf(months[j]) + 1;
            }
        }
        var toDate = year + "-" + month + "-" + date;

        var where = "L.StudentId = " + $scope.entity.StudentIdSearch + " AND PaymentDate BETWEEN '" + fromDate + "' AND '" + toDate + "' ";
        $http({
            url: '/FeesCollection/GetDynamic?where=' + where + '&orderBy= L.StudentId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {

                $scope.entityList = data;
                $scope.total_count = data.length;

                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end);
            }
            else {
                alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }
        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });

    };
    $scope.Save = function () {

        var where = "L.StudentId = " + $scope.entity.StudentId;
        $http({
            url: '/FeesCollection/GetDynamic?where=' + where + '&orderBy=L.StudentId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            var params = JSON.stringify({ obj: $scope.entity, transactionType: 'INSERT' });
            $http.post('/FeesCollection/Post', params).success(function (data) {
                if (data != '') {
                    if (data.indexOf('successfully') > -1) {
                        $scope.resetForm();
                        alertify.log(data, 'success', '5000');
                        getList();
                    }
                    else {
                        alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                    }
                }
                else {
                    alertify.log('System could not execute the operation.', 'error', '10000');
                }
            }).error(function () {
                alertify.log('Unknown server error', 'error', '10000');
            });

        });
    }
    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
        $scope.cmbStudent = "-- Student --";
        $scope.cmbPayType = "-- Pay Type --";
    };


})