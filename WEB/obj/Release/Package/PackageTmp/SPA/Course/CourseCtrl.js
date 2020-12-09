app.controller("CourseCtrl", function ($scope, $cookieStore, $http, blockUI) {

    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getProgramTypeActive();
    getCourseTypeActive();
    getList();
    getProgramHead();



    function clear() {
        $scope.entity = { CourseId: 0, IsActive: true };
        $("#txtFocus").focus();
    };


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
    //    "InstructorId": "1",
    //    "InstructorName": "Hasan Ali",
    //    IsSelected: false
    //}, {
    //    "InstructorId": "2",
    //    "InstructorName": "Masud Ali",
    //    IsSelected: false
    //}, {
    //    "InstructorId": "3",
    //    "InstructorName": "Javed Ali",
    //    IsSelected: false
    //}]
    function getProgramTypeActive() {
        var where = "IsActive = 1";
        $http({
            url: '/Program/GetDynamic?where=' + where + '&orderBy=ProgramId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.programList = data;
            }
        });
    }

    function getCourseTypeActive() {
        var where = "IsActive = 1";
        $http({
            url: '/Course/GetDynamic?where=' + where + '&orderBy=CourseId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.prerequisiteList = data;
            }

        });
    }
    function getStrListPr(courseId, i) {
        var where = "CourseId = " + courseId;
        var retStr = "";
        $http({
            url: '/Course/courseProgramGetDynamic?where=' + where + '&orderBy=CourseId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                for (j = 0; j < data.length; j++) {
                    retStr += data[j].ProgramCode;
                    if (j != data.length - 1) {
                        retStr += ",";
                    }

                }
                $scope.entityList[i].courseProgStr = retStr;
            }
           
            });
        

    }
    function getStrListCr(courseId, i) {
        var where = "R.[CourseId] = " + courseId;
        var retStr = "";
        var orderBy = 'R.[CourseId]'
        $http({
            url: '/Course/coursePrereqGetDynamic?where=' + where + '&orderBy= ' + orderBy,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                for (j = 0; j < data.length; j++) {
                    retStr += data[j].CourseCode;
                    if (j != data.length - 1) {
                        retStr += ",";
                    }

                }
                $scope.entityList[i].coursePrereqStr = retStr;
            }

        });


    }

    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Course/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

                $scope.entityList = data;
                $scope.total_count = data.length;
                for (i = 0; i < data.length; i++) {
                    getStrListPr(data[i].CourseId, i);
                    getStrListCr(data[i].CourseId, i);
                }

                //var begin = ($scope.PerPage * ($scope.currentPage - 1));
                //var end = begin + $scope.PerPage;
                //$scope.entityListPaged = $scope.entityList.slice(begin, end);
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
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType, programIds: $scope.entity.ProgramIds, prerequisitIds: $scope.entity.PrerequisitIds });

        $http.post('/Course/Post', params).success(function (data) {
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
    $scope.post = function (trnType) {
        var where = "CourseCode = '" + $scope.entity.CourseCode + "'";
        if ($scope.entity.CourseId > 0)
            where += " AND CourseId <> " + $scope.entity.CourseId;

        $http({
            url: '/Course/GetDynamic?where=' + where + '&orderBy=CourseCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.CourseCode + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.CourseId === 0 ? "INSERT" : "UPDATE";
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
    $scope.getPrograms = function () {
        var programNames = "";
        var programIds = "";

        angular.forEach($scope.programList, function (aProgram) {
            if (aProgram.IsSelected) {
                programNames += programNames === "" ? aProgram.ProgramCode : (", " + aProgram.ProgramCode);
                programIds += programIds === "" ? aProgram.ProgramId : ("," + aProgram.ProgramId);

            }
        })

        $scope.entity.ProgramNames = programNames;
        $scope.entity.ProgramIds = programIds;
    };

    $scope.getPrerecuisite = function () {

        var prerequisitNames = "";
        var prerequisitIds = "";
        angular.forEach($scope.prerequisiteList, function (aPrerequisite) {
            if (aPrerequisite.IsSelected) {
                prerequisitNames += (prerequisitNames === "" ? aPrerequisite.CourseCode : (", " + aPrerequisite.CourseCode));
                prerequisitIds += (prerequisitIds === "" ? aPrerequisite.CourseId : (", " + aPrerequisite.CourseId));
            }
        })

        $scope.entity.PrerequisitNames = prerequisitNames;
        $scope.entity.PrerequisitIds = prerequisitIds;
    };

    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };
});