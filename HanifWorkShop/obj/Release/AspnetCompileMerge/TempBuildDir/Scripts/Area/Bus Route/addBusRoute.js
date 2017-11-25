app.controller('addRouteController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addRoute = function () {
        if ($scope.addRouteForm.RouteName.$valid) {

            $http({
                method: 'POST',
                url: '/Route/AddRoute/',
                data: $scope.Route
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    toaster.success(response.data.successMessage);
                    $route.reload();
                }   else {
                    toaster.error(response.data.errorMessage);
                }
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Enter Bus Route Name");
        }
    }


}]);