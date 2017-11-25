app.controller('addStoreController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addStore = function () {
        if ($scope.addStoreForm.StoreName.$valid) {

            $http({
                method: 'POST',
                url: '/Store/AddStore/',
                data: $scope.Store
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
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
            toaster.error("Enter Store Name");
        }
    }


}]);