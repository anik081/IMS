app.controller("TopBarController", function ($scope, $cookieStore, $http, $window) {
    $scope.loggedinUser = {};
    $scope.loggedinUser.Username = $window.localStorage.getItem('userName');

    $scope.logOut = function () {
        window.location = '/Home/Login';
    }
});