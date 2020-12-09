var app = angular.module('csmApp', ['ngRoute', 'ngCookies', 'ngAnimate', 'ngMaterial', 'jkAngularRatingStars', 'blockUI', 'angularUtils.directives.dirPagination']);

app.config(function ($routeProvider, blockUIConfig) {
    $routeProvider
        .when('/Home', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
		})
		.when('/Program', {
			templateUrl: '/SPA/Program/Program.html',
			controller: 'ProgramCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/Course', {
			templateUrl: '/SPA/Course/Course.html',
			controller: 'CourseCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
        })
        .when('/EmployeeEntry', {
            templateUrl: '/SPA/EmployeeEntry/EmployeeEntry.html',
            controller: 'EmployeeEntryCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
		.when('/MarkType', {
			templateUrl: '/SPA/MarkType/MarkType.html',
			controller: 'MarkTypeCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
        })
        .when('/FeesType', {
            templateUrl: '/SPA/FeesType/FeesType.html',
            controller: 'FeesTypeCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/PaymentMethod', {
            templateUrl: '/SPA/PaymentMethod/PaymentMethod.html',
            controller: 'PaymentMethodCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
		.when('/Semester', {
			templateUrl: '/SPA/Semester/Semester.html',
			controller: 'SemesterCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/CourseOffer', {
			templateUrl: '/SPA/CourseOffer/CourseOffer.html',
			controller: 'CourseOfferCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/Admisson', {
			templateUrl: '/SPA/Admisson/Admisson.html',
			controller: 'AdmissonCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/Registration', {
			templateUrl: '/SPA/Registration/Registration.html',
			controller: 'RegistrationCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
        })
        .when('/Attendance', {
            templateUrl: '/SPA/Attendance/Attendance.html',
            controller: 'AttendanceCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
       
        .when('/ExamResult', {
            templateUrl: '/SPA/ExamResult/ExamResult.html',
            controller: 'ExamResultCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/FeesEntry', {
            templateUrl: '/SPA/FeesEntry/FeesEntry.html',
            controller: 'FeesEntryCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/FeesCollection', {
            templateUrl: '/SPA/FeesCollection/FeesCollection.html',
            controller: 'FeesCollectionCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .otherwise({ redirectTo: '/' });

    blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container"> <img src="../img/loading.gif" /> <h4><strong>LOADING...</strong></h4> </div>'
});