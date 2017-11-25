app.controller('showAllRouteController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.EditRouteInfo = false;
    $scope.ShowRouteList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/Route/GetAllRoute"
    }).then(function mySuccess(response) {
        $scope.RouteList = response.data.result;

    }, function myError(response) {

    });



    $scope.EditRoute = function (row) {
        $scope.Route = row;
        $scope.EditRouteInfo = true;
        $scope.ShowRouteList = false;
    }

    $scope.UpdateRoute = function () {
        if ($scope.updateRouteForm.RouteName.$valid) {

            $http({
                method: "POST",
                url: "/Route/Update",
                data: $scope.Route
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    toaster.success(response.data.successMessage);
                    $route.reload();
                    $scope.EditRouteInfo = false;
                    $scope.ShowRouteList = true;
                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.EditRouteInfo = true;
                    $scope.ShowRouteList = false;
                }
                //$scope.message = response.data.successMessage;
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
                //$scope.EditRouteInfo = true;
                //$scope.ShowRouteList = false;
            });
        }
        else {
            toaster.error("Enter Route Name");
        }

    };



    $scope.DeleteRoute = function (id) {
        $http({
            method: "GET",
            url: "/Route/Delete/?id=" + id
        }).then(function mySuccess(response) {
            if (response.data.success == true) {
                $route.reload();
                toaster.success(response.data.successMessage);
            } else {
                toaster.error(response.data.errorMessage);
            }
        }, function myError(response) {
            toaster.error(response.data.errorMessage);
        });
    }

}]);