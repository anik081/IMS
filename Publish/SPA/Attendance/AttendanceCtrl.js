app.controller("AttendanceCtrl", function ($scope, $filter, $http, blockUI) {
    clear();
    getSemesterActive();
    getCourceActive();
    getMarkTypeActive();
    function clear() {
        $scope.entity = { SemesterId: 0, IsActive: true };
        $("#txtFocus").focus();
    };
    //$scope.semesterList = [{
    //    "SemesterId": "1",
    //    "SemesterName": "Spring"
    //}, {
    //    "SemesterId": "2",
    //    "SemesterName": "Summer"
    //}, {
    //    "SemesterId": "3",
    //    "SemesterName": "Fall"

    //    }]
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
    function getCourceActive() {
        var where = "IsActive =1";
        $http({
            url: '/Course/GetDynamic?where=' + where + '&orderBy=CourseCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.courseList = data;
            }
        });

    }
    function getMarkTypeActive() {
        var where = "IsActive =1";
        $http({
            url: '/MarkType/GetDynamic?where=' + where + '&orderBy=MarkTypeName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.examList = data;
            }
        });

    }
    //$scope.examList = [{
    //    "ExamId": "1",
    //    "ExamName": "Class Test"
    //}, {
    //    "ExamId": "2",
    //    "ExamName": "Mid"
    //}, {
    //    "ExamId": "3",
    //    "ExamName": "Lab Exam"

    //}]
    //$scope.courseList = [{
    //    "CourseId": "1",
    //    "CourseName": "CSE105"
    //}, {
    //    "CourseId": "2",
    //    "CourseName": "CSE225"
    //}, {
    //    "CourseId": "1",
    //    "CourseName": "CSE435"

    //    }]
    $('#atDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#atDate').keypress(function (e) {
        e.preventDefault();
    });
    $('#eDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#eDate').keypress(function (e) {
        e.preventDefault();
    });
});