app.controller("CourseOfferCtrl", function ($scope, $http, blockUI) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.selectedCourseId = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    //$scope.schedule = {};
    $scope.scheduleList = [];
    $scope.scheduleListDisplay = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    $scope.entity.CampusName = "Main Campus";
    getSemesterTypeActive();
    getCourse();
    getProgramHead();

    //var isManagerRef = data[0].IsManagerRef
    //angular.forEach(data, function (emp) {
    //    if (isManagerRef)
    //        emp.IsSelected = emp.EmployeeId === RefEmployeeId ? false : true;
    //    else
    //        emp.IsSelected = emp.EmployeeId === RefEmployeeId ? true : false;
    //});

    function getProgramHead() {

        var where = "EmployeeType = 'Faculty'";
        $http({
            url: '/Program/GetDynamicEmployee?where=' + where + '&orderBy=EmployeeName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.employeeList = data;
            }
        });
    }
    //$scope.employeeList = [{
    //    "ProgramHeadId": "1",
    //    "ProgramHeadName": "Hasan Ali",
    //    IsSelected: false
    //}, {
    //    "ProgramHeadId": "2",
    //    "ProgramHeadName": "Masud Ali",
    //    IsSelected: false
    //}, {
    //    "ProgramHeadId": "3",
    //    "ProgramHeadName": "Javed Ali",
    //    IsSelected: false
    //}]
    $scope.dayList = [{
        "DayId": "1",
        "DayName": "Saturday",

    }, {
        "DayId": "2",
        "DayName": "Sunday",
    },
    {
        "DayId": "3",
        "DayName": "Monday",
    }, {
        "DayId": "4",
        "DayName": "Tuesday",
    }, {
        "DayId": "5",
        "DayName": "Wednesday",
    }, {
        "DayId": "6",
        "DayName": "Thursday",
    },
    {
        "DayId": "7",
        "DayName": "Friday",
    }]
    $('#dtFrom').timepicker({
        minuteStep: 5,
        showSeconds: false,
        showMeridian: true,
        icons: {
            up: 'glyphicon glyphicon-chevron-up',
            down: 'glyphicon glyphicon-chevron-down'
        }
    });

    $('#dtFrom').keypress(function (e) {
        e.preventDefault();
    });

    $('#dtTo').timepicker({
        minuteStep: 5,
        showSeconds: false,
        showMeridian: true,
        icons: {
            up: 'glyphicon glyphicon-chevron-up',
            down: 'glyphicon glyphicon-chevron-down'
        }
    });

    $('#dtTo').keypress(function (e) {
        e.preventDefault();
    });
    $scope.openCalendar = function (from) {
        switch (from) {
            case 'from':
                $("#dtFrom").focus();
                break;
            case 'to':
                $("#dtTo").focus();
                break;
            default:
                break;
        }
    };
    function clear() {
        $scope.entity = { SemesterId: 0, IsActive: true };
        $("#txtFocus").focus();
    };
    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Semester/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();
                $scope.entityList = data;
                $scope.total_count = data.length;

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
    function getSemesterTypeActive() {
        var where = "IsActive = 1";
        $http({
            url: '/Semester/GetDynamic?where=' + where + '&orderBy=SemesterName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.semesterTypeList = data; 
            }
        });
    }

    function getCourse() {
        var where = "IsActive = 1";
        $http({
            url: '/Course/GetDynamic?where=' + where + '&orderBy=CourseCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.courseTypeList = data;
            }
        });
    }

    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

    $scope.scheduleOpen = function (courseId, index) {
        $scope.selectedCourseId = courseId;
        $scope.scheduleIndex = index;
        //$scope.scheduleList = [];
        //$scope.scheduleListDisplay = [];

        if ($scope.scheduleList.length) {
            $scope.scheduleListDisplay = Enumerable.From($scope.scheduleList).Where("$.CourseId== " + courseId).ToArray();
        }

    }
    $scope.removeSchedule = function (index) {
        $scope.modalIndex = index;
        $scope.scheduleList.splice(index, 1);
        string = "";
        sum = "";
        $scope.scheduleListDisplay.splice(index, 1);
        for (i = 0; i < $scope.scheduleListDisplay.length; i++) {
            sch = $scope.scheduleListDisplay[i].DayName + "(" + $scope.scheduleListDisplay[i].StartTime + "-" + $scope.scheduleListDisplay[i].EndTime + ")";
            sum = sum + sch;
            if (i != $scope.scheduleListDisplay.length - 1) {
                sum = sum + ",";
            }

        }
        var objSchedule = $scope.courseTypeList[$scope.scheduleIndex];
        objSchedule.ScheduleSummary = sum;
    }

    $scope.addSchedule = function () {
        var obj = {};
        string = "";
        sum = "";
        obj.CourseId = $scope.selectedCourseId;
        obj.DayName = $scope.cmbDay.DayName;
        obj.StartTime = $scope.StartTime;
        obj.EndTime = $scope.EndTime;
        obj.RoomNo = $scope.RoomNo;
        $scope.scheduleList.push(obj);
        $scope.scheduleListDisplay.push(obj);
        for (i = 0; i < $scope.scheduleListDisplay.length; i++) {
            sch = $scope.scheduleListDisplay[i].DayName + "(" + $scope.scheduleListDisplay[i].StartTime + "-" + $scope.scheduleListDisplay[i].EndTime + ")";
            sum = sum + sch;
            if (i != $scope.scheduleListDisplay.length - 1) {
                sum = sum + ",";
            }
            
        }
        var objSchedule = $scope.courseTypeList[$scope.scheduleIndex];
        objSchedule.ScheduleSummary = sum;
        $scope.cmbDay = "-- Day --";
        $scope.StartTime = "";
        $scope.EndTime = "";
        $scope.RoomNo = "";

        
    }
    $scope.post = function (trnType) {
        if (trnType === 'save') {
            trnType = "INSERT";
        }
        var selectedList = Enumerable.From($scope.courseTypeList).Where("$.IsSelected == true").ToArray();
        var params = JSON.stringify({ lstCourseOffer: selectedList, lstCourseOfferSchedule: $scope.scheduleList, transactionType: trnType });

        $http.post('/CourseOffer/Post', params).success(function (data) {
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.resetForm();
                    getCourse();
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

    //$scope.getSchedule = function () {

    //    $scope.ScheduleSummary = $scope.cmbDay.DayName + "(" + $scope.StartTime + "-" + $scope.EndTime + ")";
    //}
});