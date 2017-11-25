app.controller('AddBusInformationController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $http({
        method: "GET",
        url: "/Route/GetAllRoute"
    }).then(function mySuccess(response) {
        $scope.RouteList = response.data.result;

    }, function myError(response) {

    });
    $scope.addBusInformation = function () {
        if ($scope.addBusInformationForm.$valid) {

            $http({
                method: 'POST',
                url: '/BusInformation/addBusInformation/',
                data: $scope.BusInformation
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    toaster.success(response.data.successMessage);
                    $route.reload();
                } else {
                    toaster.error(response.data.errorMessage);
                }
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Enter All Required field");
        }
    }


}]);