app.controller("FeesEntryCtrl", function ($scope, $cookieStore, $window, $location, $http, blockUI) {
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
    getStudentName();
    getSemesterActive();
    getFeesType();
    getList();


    function clear() {
        $scope.entity = { FeesEntryId: 0, IsActive: true };
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
    function getSemesterActive() {
        var where = "IsActive = 1";
        $http({
            url: '/Semester/GetDynamic?where=' + where + '&orderBy=SemesterName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.semesterList = data;
            }
        });
    }
    function getFeesType() {
        var where = "IsActive = 1";
        $http({
            url: '/FeesType/GetDynamic?where=' + where + '&orderBy=FeesName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.feesList = data;
            }
        });
    }
    $scope.Search = function () {
       
        var where = "R.StudentId = " + $scope.entity.StudentIdSearch + " AND R.FeesId = " + $scope.entity.FeesIdSearch;
        $http({
            url: '/FeesEntry/GetDynamic?where=' + where + '&orderBy=R.StudentId',
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
    function getList() {
        var where = "L.IsActive = 1";
        $http({
            url: '/FeesEntry/GetDynamic?where=' + where + '&orderBy=R.StudentId',
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
    }
    $scope.Save = function () {

        var where = "R.StudentId = " + $scope.entity.StudentId + " AND R.SemesterId = " + $scope.entity.SemesterId + "AND O.FeesName =" + $scope.entity.FeesName ;
        $http({
            url: '/FeesEntry/GetDynamic?where=' + where + '&orderBy=R.StudentId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.SemesterId + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                $scope.entity.RegStatusId = 1;
                var params = JSON.stringify({ obj: $scope.entity, transactionType: 'INSERT' });
                $http.post('/FeesEntry/Post', params).success(function (data) {
                    if (data != '') {
                        if (data.indexOf('successfully') > -1) {
                            $scope.resetForm();
                            alertify.log(data, 'success', '5000');
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
            }

        });
    }
    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

})