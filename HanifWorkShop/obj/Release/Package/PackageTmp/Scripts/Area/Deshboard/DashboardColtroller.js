app.controller('DashboardController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.Login = function () {
        $scope.menu = true;
        $scope.pagename = true;
    };



    $scope.ForLoginSuccess = function () {
        $scope.menu = false;
        $scope.pagename = false;

    };


    $scope.init = function () {
        $scope.menu = false;
        $scope.LoadModules();
    };
    //$scope.LoadModules = function () {
    //    $scope.module = null;
    //    $http({
    //        method: "GET",
    //        url: "/Account/LoadModules"
    //    }).then(function mySuccess(response) {
    //        $scope.module = response.data.result;
    //        $scope.mapped_module = $scope.module;

    //    }, function myError(response) {

    //    });
     
    //};

    $scope.LoadModules = function () {
        $scope.ModuleList = null;
        $http({
            method: "GET",
            url: "/Module/GetAllModule"
        }).then(function mySuccess(response) {
            $scope.ModuleList = response.data.result;

        }, function myError(response) {

        });

    };


        //$http({
        //    method: "GET",
        //    url: "/Module/GetAllModule"
        //}).then(function mySuccess(response) {
        //    $scope.ModuleList = response.data.result;

        //}, function myError(response) {

        //});
}]);