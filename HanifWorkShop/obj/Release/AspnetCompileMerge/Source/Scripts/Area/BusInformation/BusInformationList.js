app.controller('showAllBusInformationController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.EditBusInfo = false;
    $scope.ShowBusList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/BusInformation/GetAllBusInformation"
    }).then(function mySuccess(response) {
        $scope.BusInformationList = response.data.result;

    }, function myError(response) {

    });

    $http({
        method: "GET",
        url: "/Route/GetAllRoute"
    }).then(function mySuccess(response) {
        $scope.RouteList = response.data.result;

    }, function myError(response) {

    });

    $scope.EditBusInformation = function (row) {
        $scope.BusInformation = row;
        $scope.EditBusInfo = true;
        $scope.ShowBusList = false;
    }

    $scope.updateBusInformation = function () {
        if ($scope.editBusInformationForm.$valid) {

            $http({
                method: "POST",
                url: "/BusInformation/Update",
                data: $scope.BusInformation
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    toaster.success(response.data.successMessage);
                    $route.reload();
                    $scope.EditBusInfo = false;
                    $scope.ShowBusList = true;
                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.EditBusInfo = true;
                    $scope.ShowBusList = false;
                }
                //$scope.message = response.data.successMessage;
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
                //$scope.EditBusInfo = true;
                //$scope.ShowBusList = false;
            });
        }
        else {
            toaster.error("Enter All Required Field");
        }

    };



    $scope.DeleteDesignation = function (id) {
        $http({
            method: "GET",
            url: "/BusInformation/Delete/?id=" + id
        }).then(function mySuccess(response) {
            if (response.data.success == true) {
                $route.reload();
                toaster.success(response.data.successMessage);
            }
            else {
                toaster.error(response.data.errorMessage);
            }
        }, function myError(response) {
            toaster.error(response.data.errorMessage);
        });
    }






}]);