app.controller("SideBarController", function ($scope, $cookieStore, $window, $location) {
    $scope.loginUserSb = [];
    $scope.loginUserSb = $cookieStore.get('UserData');
    ResetMenuCSS();

    var path = $location.path();
    if (path === '/Home')
        $scope.isHome = true;
    else if (path === '/Program' || path === '/Course' || path === '/MarkType' || path === '/Semester' || path === '/FeesType' || path === '/PaymentMethod' || path === '/EmployeeEntry')
        $scope.isAdmin = true;
    else if (path === '/Admission' || path === '/Registration')
        $scope.isStudent = true;
    else if (path === '/Attendance' || path === '/ExamResult')
        $scope.isClassExam = true;
    else if (path === '/FeesEntry' || path === '/FeesCollection')
        $scope.isAccounts = true;
    else
        $scope.isHome = true;

    function ResetMenuCSS() {
        //Menu
        $scope.isHome = false;
        $scope.isAdmin = false;
        $scope.isAdminProgram = false;
        $scope.isAdminCourse = false;
        $scope.isAdminMarkType = false;
        $scope.isAdminFeesType = false;
        $scope.isAdminSemester = false;
        $scope.isAdminCourseOffer = false;
        $scope.isAdminEmployeeEntry = false;
        $scope.isStudent = false;
        $scope.isStudentAdmission = false;
        $scope.isStudentRegistration = false;
        $scope.isClassExam = false;
        $scope.isClassExamExamResult = false;
        $scope.isClassExamsAttandence = false;
        $scope.isAccounts = false;
        $scope.isAccountsFeesCollection = false;
        $scope.isAccountsFeesEnrty = false;
        $scope.isAdminPaymentMethod = false;

    };

    $scope.setActiveMenu = function (menu) {
        ResetMenuCSS();

        if (menu === 'home')
            $scope.isHome = true;

        else if (menu === 'program') {
            $scope.isAdmin = true;
            $scope.isAdminProgram = true;
        }
        else if (menu === 'course') {
            $scope.isAdmin = true;
            $scope.isAdminCourse = true;
        }
        else if (menu === 'marktype') {
            $scope.isAdmin = true;
            $scope.isAdminMarkType = true;
        }
        else if (menu === 'feestype') {
            $scope.isAdmin = true;
            $scope.isAdminFeesType = true;
        }
        else if (menu === 'semester') {
            $scope.isAdmin = true;
            $scope.isAdminSemester = true;
        }
        else if (menu === 'courseOffer') {
            $scope.isAdmin = true;
            $scope.isAdminCourseOffer = true;
        }
        else if (menu === 'employeeEntry') {
            $scope.isAdmin = true;
            $scope.isAdminEmployeeEntry = true;
        }
        else if (menu === 'admission') {
            $scope.isStudent = true;
            $scope.isStudentAdmission = true;
        }
        else if (menu === 'registration') {
            $scope.isStudent = true;
            $scope.isStudentRegistration = true;
        }
        else if (menu === 'attendance') {
            $scope.isClassExam = true;
            $scope.isClassExamAttandence = true;
        }

        else if (menu === 'examResult') {
            $scope.isClassExam = true;
            $scope.isClassExamExamResult = true;
        }
        else if (menu === 'feesEntry') {
            $scope.isAccounts = true;
            $scope.isAccountsFeesEnrty = true;
        }
        else if (menu === 'paymentMethod') {
            $scope.isAdmin = true;
            $scope.isAdminPaymentMethod = true;
        }
        else if (menu === 'feesCollection') {
            $scope.isAccounts = true;
            $scope.isAccountsFeesCollection = true;

        }
    };
});