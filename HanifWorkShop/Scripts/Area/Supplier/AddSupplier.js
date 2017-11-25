app.controller('addSupplierController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addSupplier = function () {
        if ($scope.addSupplierForm.CompanyName.$valid) {
            if ($scope.addSupplierForm.Address.$valid) {
                if ($scope.addSupplierForm.ContactNo.$valid) {
                    $http({
                        method: 'POST',
                        url: '/Supplier/AddSupplier/',
                        data: $scope.Supplier
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
                    toaster.error("Enter Contact No");
                }
            }
            else {
                toaster.error("Enter Address");
            }
        }
        else {
            toaster.error("Enter  Company Name");
        }
    };
}]);