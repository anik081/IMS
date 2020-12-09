app.controller("RegistrationCtrl", function ($scope, $filter, $http, blockUI) {
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
    $scope.cList = [];
    getSemesterTypeActive();

    $scope.getGrid = function () {


        if (!angular.isUndefined($scope.entity.SemesterId) && $scope.entity.SemesterId !== null && !angular.isUndefined($scope.entity.StudentId) && $scope.entity.StudentId !== null) {
            getList();
        }

    }
    function clear() {
        $scope.entity = { CourseRegistrationId: 0, IsActive: true };
        $("#txtFocus").focus();
    };

    function getSemesterTypeActive() {
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
    function getList() {
        var where = "SemesterId = '" + $scope.entity.SemesterId + "' AND R.StudentId = '" + $scope.entity.StudentId + "'";
        $scope.lsitBlock.start();
        $http({
            url: '/CourseRegistration/GetDynamic?where=' + where + '&orderBy=RegStatusId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.lsitBlock.stop();

            if (data.length) {
                angular.forEach(data, function (obj) {
                    var res1 = obj.RegistrationDate.substring(0, 5);
                    if (res1 == "/Date") {
                        var parsedDate1 = new Date(parseInt(obj.RegistrationDate.substr(6)));
                        var date1 = ($filter('date')(parsedDate1, 'MMM dd, yyyy')).toString();
                        obj.RegistrationDate = date1;
                    }
                    obj.Status = obj.RegStatusId == 1 ? 'Registered' : (obj.RegStatusId == 2 ? 'Dropped' : 'Withdrawn');

                })

                

                //var begin = ($scope.PerPage * ($scope.currentPage - 1));
                //var end = begin + $scope.PerPage;
                //$scope.entityListPaged = $scope.entityList.slice(begin, end);
            }

            $scope.entityList = data;
            $scope.total_count = data.length;

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });

    };



    $scope.post = function () {
        var where = "RegStatusId = 1 AND R.StudentId = " + $scope.entity.StudentId + " AND R.CourseOfferId = " + $scope.entity.CourseOfferId;
        //if ($scope.entity.CourseRegistrationId > 0)
        //    where += " AND StudentId <> " + $scope.entity.StudentId;

        $http({ 
            url: '/CourseRegistration/GetDynamic?where=' + where + '&orderBy=CourseOfferId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.CourseTitle + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                $scope.entity.RegStatusId = 1;
                var params = JSON.stringify({ obj: $scope.entity, transactionType: 'INSERT' });

                $scope.entryBlock.start();

                $http.post('/CourseRegistration/Post', params).success(function (data) {
                    if (data != '') {
                        if (data.indexOf('successfully') > -1) {
                            $scope.resetForm();
                            $scope.getGrid();
                            alertify.log(data, 'success', '5000');

                        }
                        else {
                            alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                        }
                    }
                    else {
                        alertify.log('System could not execute the operation.', 'error', '10000');
                    }

                    $scope.entryBlock.stop();
                }).error(function () {
                    $scope.entryBlock.stop();
                    alertify.log('Unknown server error', 'error', '10000');
                });
            }

        });
    };
    $scope.dropOrWithdraw = function (trnType, rowItem) {
        var where = "CourseOfferId = '" + rowItem.CourseOfferId + "'";
        $http({ 
            url: '/CourseRegistration/GetDynamic?where=' + where + '&orderBy=CourseOfferId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.entity.RegStatusId = trnType === 'DROP' ? 2 : 3;
            var params = JSON.stringify({ obj: rowItem, transactionType: trnType });
            $scope.entryBlock.start();

            $http.post('/CourseRegistration/Post', params).success(function (data) {
                $scope.entryBlock.stop();

                if (data != '') {
                    if (data.indexOf('successfully') > -1) {
                        $scope.resetForm();
                        $scope.getGrid();
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
                $scope.entryBlock.stop();
                alertify.log('Unknown server error', 'error', '10000');
            });
        });
    };
    $scope.getCourseOffer = function () { //
        var where = "SemesterId=" + $scope.cmbSemester.SemesterId;
        $http({
            url: '/CourseOffer/GetDynamic?where=' + where + '&orderBy=CourseOfferId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            //if (data.length) { //
            $scope.courseList = data;
            angular.forEach(data, function (obj) {
                obj.CourseCodeWithTitle = obj.CourseTitle + " (" + obj.CourseCode + ")";

            })
            //}
        });
    }

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
        $scope.cmbSemester = "-- Semester --";
        $scope.cmbSelectStudent = "-- Select Student --";
        $scope.cmbCourse = "-- Select Course --";
    };


});