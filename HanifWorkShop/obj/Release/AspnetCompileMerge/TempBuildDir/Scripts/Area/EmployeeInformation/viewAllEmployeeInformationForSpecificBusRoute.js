app.controller('viewAllEmployeeInformationForSpecificBusRouteController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];
    
    //$scope.RouteClick=function() {
    //    if (!$scope.Route.RouteId) {
    //        $scope.reportButton = false;
    //    } 
    //}

    $scope.RouteList = {};
    $scope.RouteList.selected = null;

    $http({
        method: "GET",
        url: "/Route/GetAllRoute"
    }).then(function mySuccess(response) {
        $scope.RouteNoList = response.data.result;

    }, function myError(response) {

    });

    $scope.GenerateReportForSearchResult = function () {
        if ($scope.viewAllEmployeeInformationForSpecificBusRouteForm.$valid) {
            $http({

                method: 'POST',
                url: '/EmployeeInformation/GenerateReportForSearchResult/',
                data: { routeId: $scope.RouteList.selected.RouteId }
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.EmployeeList = response.data.result;
                    toaster.success(response.data.successMessage);
                    $scope.reportButton = true;

                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.reportButton = false;
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Select Route Name");
        }

    };

    $scope.SearchAllEmployeeInformationForSpecificBusRoute = function () {
        if ($scope.viewAllEmployeeInformationForSpecificBusRouteForm.$valid) {

            $http({
                method: "POST",
                url: "/EmployeeInformation/ViewAllEmployeeInformationForSpecificBusRoute",
                //data: { routeId: $scope.Route.RouteId }
                data: { routeId: $scope.RouteList.selected.RouteId }
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.EmployeeList = response.data.result;
                    $scope.reportButton = true;

                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.reportButton = false;
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Select Route Name");
            $scope.reportButton = false;
        }

    };
    




}]);