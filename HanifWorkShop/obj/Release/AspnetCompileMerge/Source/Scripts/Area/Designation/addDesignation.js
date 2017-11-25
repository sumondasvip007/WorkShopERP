app.controller('addDesignationController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addDesignation = function () {
        if ($scope.addDesignationForm.DesignationName.$valid) {

                    $http({
                        method: 'POST',
                        url: '/Designation/AddDesignation/',
                        data: $scope.Designation
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
                    toaster.error("Enter Designation Name");
                }
            }
         
   
}]);