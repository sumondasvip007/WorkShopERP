app.controller('addPartsInfoController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addPartsInfo = function () {
        if ($scope.addPartsInfoForm.PartsName.$valid) {
            if ($scope.addPartsInfoForm.BasePrice.$valid) {
                    $http({
                        method: 'POST',
                        url: '/PartsInformation/AddPartsInfo/',
                        data: $scope.PartsInfo
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
                toaster.error("Enter Base Price");
            }
        }
        else {
            toaster.error("Enter  Parts Name");
        }
    };
}]);