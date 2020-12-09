app.controller("LoginController", function ($scope, $cookieStore, $http, $window, blockUI) {
    //var plant = $cookieStore.get('Plant');
    $window.localStorage.clear();
    $scope.loginUser = [];
    $scope.user = { };
    $scope.loginFailAlert = false;
    $scope.loginFailMessage = '';
    //SetIsAuthentic();
    //GetMaintenanceInfo();
    alertify.set({ buttonReverse: true });

    function SetIsAuthentic() {
        $http({
            url: "/Login/SetIsAuthentic?isAuthentic=0",
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        })
    }

    function GetMaintenanceInfo() {
        $http({
            url: '/Login/GetMaintenanceInfo',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            var infoArr = data.split('~');
            $scope.IsMaintenance = infoArr[0];
            if ($scope.IsMaintenance == 1) {
                $scope.loginFailMessage = infoArr[1];
                $scope.loginFailAlert = true;
                $scope.MaintenancePin = infoArr[2];
            }
        });
    }

    function doLogin() {
        $window.localStorage.setItem('userName', $scope.user.Username);
        window.location = '/Home/Index#/Home';
    }

    $scope.dismissAlert = function () {
        $scope.loginFailAlert = false;
    }

    $scope.Login = function () {
        if ($scope.IsMaintenance == 1) {
            setTimeout(function () {
                alertify.prompt('Enter Maintenance Passcode', function (e, val) {
                    if (e) {
                        if (val === $scope.MaintenancePin)
                            doLogin();
                        else
                            alertify.log('Incorrect Maintenance Passcode!', 'error', '3000');
                    }
                })
            }, 200);
        }
        else {
            doLogin();
        }
    };  
});
