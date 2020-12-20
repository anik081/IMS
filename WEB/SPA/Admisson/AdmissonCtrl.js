app.controller("AdmissonCtrl", function ($scope, $cookieStore, $window, $location, $http, blockUI) {

    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    $scope.educationList = [];
    dateToday = new Date();
    var year = dateToday.getFullYear();
    getProgramTypeActive();
    yearLimit = 40;
    $scope.yearList = [];
    for (i = 0; i <= yearLimit; i++) {
        $scope.yearList.push({ "YearId": year - i });

    }
    function clear() {
        $scope.entity = { StudentId: 0 };

        $("#txtFocus").focus();
    };
    $('#dobirth').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '5',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dobirth').keypress(function (e) {
        e.preventDefault();
    });

    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Admission/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

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
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType, educationList: $scope.educationList });

        $http.post('/Admission/Post', params).success(function (data) {
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
        var where = "Email = '" + $scope.entity.Email + "'";
        if ($scope.entity.StudentId > 0)
            where += " AND StudentId <> " + $scope.entity.StudentId;

        $http({
            url: '/Admission/GetDynamic?where=' + where + '&orderBy=FullName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.Email + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.StudentId === 0 ? "INSERT" : "UPDATE";
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

    $scope.examList = [{
        "ExamId": "1",
        "ExamName": "J.S.C."
    }, {
        "ExamId": "2",
        "ExamName": "S.S.C."
    }, {
        "ExamId": "3",
        "ExamName": "H.S.C."

    }]
    $scope.genderList = [{
        "GenderId": "1",
        "GenderType": "Male"
    }, {
        "GenderId": "2",
        "GenderType": "Female"
    }, {
        "GenderId": "3",
        "GenderType": "Other"

    }]
    $scope.divisionList = [{
        "DivisionId": "1",
        "DivisionName": "Barisal"
    }, {
        "DivisionId": "2",
        "DivisionName": "Chittagong"
    },
    {
        "DivisionId": "3",
        "DivisionName": "Dhaka"
    }, {
        "DivisionId": "4",
        "DivisionName": "Khulna"
    },
    {
        "DivisionId": "5",
        "DivisionName": "Mymensingh"
    },
    {
        "DivisionId": "6",
        "DivisionName": "Rajshahi"
    },
    {
        "DivisionId": "7",
        "DivisionName": "Sylhet"

    },
    {
        "DivisionId": "8",
        "DivisionName": "Rangpur"

    }
    ]
    $scope.districtList = [{
        "DistrictId": "1",
        "DistrictName": "Bagerhat"
    }, {
        "DistrictId": "2",
        "DistrictName": "Bandarban"
    },
    {
        "DistrictId": "3",
        "DistrictName": "Barguna"
    }, {
        "DistrictId": "4",
        "DistrictName": "Barishal"
    },
    {
        "DistrictId": "5",
        "DistrictName": "Bhola"
    },
    {
        "DistrictId": "6",
        "DistrictName": "Bogura"
    },
    {
        "DistrictId": "7",
        "DistrictName": "Brahmanbaria"

    },
    {
        "DistrictId": "8",
        "DistrictName": "Chandpur"

    },
    {
        "DistrictId": "9",
        "DistrictName": "Chapainawabganj"

    },
    {
        "DistrictId": "10",
        "DistrictName": "Chattogram"

    },
    {
        "DistrictId": "11",
        "DistrictName": "Chuadanga"

    },
    {
        "DistrictId": "12",
        "DistrictName": "Cox's Bazar"

    },
    {
        "DistrictId": "13",
        "DistrictName": "Cumilla"

    },
    {
        "DistrictId": "14",
        "DistrictName": "Dhaka"

    },
    {
        "DistrictId": "15",
        "DistrictName": "Dinajpur"

    },
    {
        "DistrictId": "16",
        "DistrictName": "Faridpur"

    },
    {
        "DistrictId": "17",
        "DistrictName": "Feni"

    },
    {
        "DistrictId": "18",
        "DistrictName": "Gaibandha"

    },
    {
        "DistrictId": "19",
        "DistrictName": "Gazipur"

    },
    {
        "DistrictId": "20",
        "DistrictName": "Gopalganj"

    },
    {
        "DistrictId": "21",
        "DistrictName": "Habiganj"

    },
    {
        "DistrictId": "22",
        "DistrictName": "Jamalpur"

    },
    {
        "DistrictId": "23",
        "DistrictName": "Jashore"

    },
    {
        "DistrictId": "24",
        "DistrictName": "Jhalokati"

    },
    {
        "DistrictId": "25",
        "DistrictName": "Jhenaidah"

    },
    {
        "DistrictId": "26",
        "DistrictName": "Joypurhat"

    },
    {
        "DistrictId": "27",
        "DistrictName": "Khagrachhari"

    },
    {
        "DistrictId": "28",
        "DistrictName": "Khulna"

    },
    {
        "DistrictId": "29",
        "DistrictName": "Kishoreganj"

    },
    {
        "DistrictId": "30",
        "DistrictName": "Kurigram"

    },
    {
        "DistrictId": "31",
        "DistrictName": "Kushtia"

    },
    {
        "DistrictId": "32",
        "DistrictName": "Lakshmipur"

    },
    {
        "DistrictId": "33",
        "DistrictName": "Lalmonirhat"

    },
    {
        "DistrictId": "34",
        "DistrictName": "Madaripur"

    },
    {
        "DistrictId": "35",
        "DistrictName": "Magura"


    },
    {
        "DistrictId": "36",
        "DistrictName": "Manikganj"

    },
    {
        "DistrictId": "37",
        "DistrictName": "Meherpur"

    },
    {
        "DistrictId": "38",
        "DistrictName": "Moulvibazar"

    },
    {
        "DistrictId": "39",
        "DistrictName": "Munshiganj"

    },
    {
        "DistrictId": "40",
        "DistrictName": "Mymensingh"

    },
    {
        "DistrictId": "41",
        "DistrictName": "Naogaon"

    },
    {
        "DistrictId": "42",
        "DistrictName": "Narail"

    },
    {
        "DistrictId": "43",
        "DistrictName": "Narayanganj"

    },
    {
        "DistrictId": "44",
        "DistrictName": "Narsingdi"

    },
    {
        "DistrictId": "45",
        "DistrictName": "Natore"


    },
    {
        "DistrictId": "46",
        "DistrictName": "Netrokona"


    },
    {
        "DistrictId": "47",
        "DistrictName": "Nilphamari"

    },
    {
        "DistrictId": "48",
        "DistrictName": "Noakhali"

    },
    {
        "DistrictId": "49",
        "DistrictName": "Pabna"

    },
    {
        "DistrictId": "50",
        "DistrictName": "Panchagarh"

    },
    {
        "DistrictId": "51",
        "DistrictName": "Patuakhali"

    },
    {
        "DistrictId": "52",
        "DistrictName": "Pirojpur"

    },
    {
        "DistrictId": "53",
        "DistrictName": "Rajbari"


    },
    {
        "DistrictId": "54",
        "DistrictName": "Rajshahi"


    },
    {
        "DistrictId": "55",
        "DistrictName": "Rangamati"

    },
    {
        "DistrictId": "56",
        "DistrictName": "Rangpur"

    },
    {
        "DistrictId": "57",
        "DistrictName": "Satkhira"

    },
    {
        "DistrictId": "58",
        "DistrictName": "Shariatpur"

    },
    {
        "DistrictId": "59",
        "DistrictName": "Sherpur"


    },
    {
        "DistrictId": "60",
        "DistrictName": "Sirajganj"


    },
    {
        "DistrictId": "61",
        "DistrictName": "Sunamganj"

    },
    {
        "DistrictId": "62",
        "DistrictName": "Sylhet"


    },
    {
        "DistrictId": "63",
        "DistrictName": "Tangail"


    },
    {
        "DistrictId": "64",
        "DistrictName": "Thakurgaon"

    }
    ]
    $scope.bloodGroupList = [{
        "BloodGroupId": "1",
        "BloodGroupType": "A+"
    }, {
        "BloodGroupId": "2",
        "BloodGroupType": "A-"
    }, {
        "BloodGroupId": "3",
        "BloodGroupType": "B+"

    }, {
        "BloodGroupId": "4",
        "BloodGroupType": "B-"

    },
    {
        "BloodGroupId": "5",
        "BloodGroupType": "AB+"

    },
    {
        "BloodGroupId": "6",
        "BloodGroupType": "AB-"

    }, {
        "BloodGroupId": "7",
        "BloodGroupType": "O+"

    }, {
        "BloodGroupId": "8",
        "BloodGroupType": "O-"

    }]
    $scope.maritalStatusList = [{
        "MaritalStatusId": "1",
        "MaritalStatusType": "Single"
    }, {
        "MaritalStatusId": "2",
        "MaritalStatusType": "Married"
    }]
    $scope.photoIdTypeList = [{
        "PhotoId": "1",
        "PhotoIdType": "National Id"
    },
    {
        "PhotoId": "2",
        "PhotoIdType": "Birth Certificate"
    }, {
        "PhotoId": "3",
        "PhotoIdType": "Passport"
    }, {
        "PhotoId": "4",
        "PhotoIdType": "Driver License"

    }]
    $scope.batchList = [{
        "BatchId": "1",
        "BatchName": "Batch-11"
    }, {
        "BatchId": "2",
        "BatchName": "Batch-12"
    }, {
        "BatchId": "3",
        "BatchName": "Batch-13"

    }, {
        "BatchId": "4",
        "BatchName": "Batch-14"
    }, {
        "BatchId": "5",
        "BatchName": "Batch-15"
    }, {
        "BatchId": "6",
        "BatchName": "Batch-16"

    },
    {
        "BatchId": "7",
        "BatchName": "Batch-17"
    }, {
        "BatchId": "8",
        "BatchName": "Batch-18"
    }, {
        "BatchId": "9",
        "BatchName": "Batch-19"

    }, {
        "BatchId": "10",
        "BatchName": "Batch-20"
    }, {
        "BatchId": "11",
        "BatchName": "Batch-21"
    }, {
        "BatchId": "12",
        "BatchName": "Batch-22"

    }, {
        "BatchId": "13",
        "BatchName": "Batch-23"
    }, {
        "BatchId": "114",
        "BatchName": "Batch-24"
    }, {
        "BatchId": "15",
        "BatchName": "Batch-25"

    }, {
        "BatchId": "16",
        "BatchName": "Batch-26"
    }, {
        "BatchId": "17",
        "BatchName": "Batch-27"
    }, {
        "BatchId": "18",
        "BatchName": "Batch-28"

    },
    {
        "BatchId": "19",
        "BatchName": "Batch-29"
    }, {
        "BatchId": "20",
        "BatchName": "Batch-30"
    }, {
        "BatchId": "21",
        "BatchName": "Batch-31"

    }, {
        "BatchId": "22",
        "BatchName": "Batch-32"
    }, {
        "BatchId": "23",
        "BatchName": "Batch-33"
    }, {
        "BatchId": "24",
        "BatchName": "Batch-34"

    }]
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
    //$scope.programList = [{
    //    "ProgramId": "1",
    //    "ProgramName": "C.S.E."
    //}, {
    //    "ProgramId": "2",
    //    "ProgramName": "B.B.A."
    //}, {
    //    "ProgramId": "3",
    //    "ProgramName": "English"
    //}]
    $scope.boardList = [

        {
            "BoardId": "1",
            "BoardName": "Barisal"
        }, {
            "BoardId": "2",
            "BoardName": "Chittagong"
        },
        {
            "BoardId": "3",
            "BoardName": "Dhaka"
        }, {
            "BoardId": "4",
            "BoardName": "Khulna"
        },
        {
            "BoardId": "5",
            "BoardName": "Mymensingh"
        },
        {
            "BoardId": "6",
            "BoardName": "Rajshahi"
        },
        {
            "BoardId": "7",
            "BoardName": "Sylhet"

        },
        {
            "BoardId": "8",
            "BoardName": "Rangpur"

        },
        {
            "BoardId": "8",
            "BoardName": "Dinajpur"
        }
    ]
    //$scope.yearList = [{
    //    "YearId": "1",
    //    "YearName": "2020"
    //}, {
    //    "YearId": "2",
    //    "YearName": "2019"
    //}, {
    //    "YearId": "3",
    //    "YearName": "2018"

    //},
    //{
    //    "YearId": "4",
    //    "YearName": "2017"

    //}]
    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };
    $scope.addEducation = function () {
        var obj = {};
        obj.ExamType = $scope.entityEducation.ExamType;
        obj.BoardName = $scope.entityEducation.BoardName;
        obj.PassingYear = $scope.entityEducation.PassingYear;
        obj.GpaOrClass = $scope.entityEducation.GpaOrClass;
        obj.InstituteName = $scope.entityEducation.InstituteName;
        obj.InstituteAddress = $scope.entityEducation.InstituteAddress;
        $scope.educationList.push(obj);
    }
    $scope.removeEducation = function (element) {
        $scope.educationLis = $scope.educationList.splice(element, 1);
    }

});