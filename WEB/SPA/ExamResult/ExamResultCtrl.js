app.controller("ExamResultCtrl", function ($scope, $filter, $http, blockUI) {
    getSemesterActive();
    getExamType();
    clear();
    function clear() {
        $scope.entity = { CourseMarkId: 0, IsActive: true };
        $("#txtFocus").focus();
        $scope.cmbSemester = "-- Semester --";
        $scope.cmbCourse = "-- Select Course --";
        $scope.cmbExamType = "-- Exam Type --";
        $scope.studentList = "";
    };
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

    function getExamType() {
        var where = "IsActive = 1";
        $http({
            url: '/MarkType/GetDynamic?where=' + where + '&orderBy=MarkTypeId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.examTypeList = data;
            }
        });
    }

    $scope.getStudentList = function () {
        var where = "RegStatusId =1 AND R.CourseOfferId=" + $scope.cmbCourse.CourseOfferId;
        $http({
            url: '/CourseRegistration/GetDynamic?where=' + where + '&orderBy=R.CourseOfferId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.studentList = data;
            }
            else {
                alertify.log('No Student Registered', 'error', '10000');
            }
        });
    }

    $scope.getCourseOffer = function () { 
        var where = "SemesterId=" + $scope.cmbSemester.SemesterId;
        $http({
            url: '/CourseOffer/GetDynamic?where=' + where + '&orderBy=CourseTitle',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
           
            $scope.courseList = data;
            angular.forEach(data, function (obj) {
                obj.CourseCodeWithTitle = obj.CourseTitle + " (" + obj.CourseCode + ")";

            })
        });
    }

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

    $scope.post = function (trnType) {
        if (trnType === 'save') {
            trnType = "INSERT";
        }
        var params = JSON.stringify({ obj: $scope.entity, lst: $scope.studentList, transactionType: trnType});
 
        $http.post('/ExamResult/Post', params).success(function (data) {
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
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            alertify.log('Unknown server error', 'error', '10000');
        });
    }
   

});