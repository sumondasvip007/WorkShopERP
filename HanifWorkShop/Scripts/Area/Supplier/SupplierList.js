app.controller('showAllSupplierController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.EditSupplierInfo = false;
    $scope.ShowSupplierList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/Supplier/GetAllSuppliers"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.supplierList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {

    });


    $scope.EditSupplier = function (row) {
        $scope.Supplier = row;
        $scope.EditSupplierInfo = true;
        $scope.ShowSupplierList = false;
    }

    $scope.UpdateSupplier = function () {
        if ($scope.editSupplierForm.CompanyName.$valid) {
            if ($scope.editSupplierForm.Address.$valid) {
                if ($scope.editSupplierForm.ContactNo.$valid) {
                    $http({
                        method: "POST",
                        url: "/Supplier/Update",
                        data: $scope.Supplier
                    }).then(function mySuccess(response) {
                        if (response.data.success === true) {
                            toaster.success(response.data.successMessage);
                            $route.reload();
                            $scope.EditSupplierInfo = false;
                            $scope.ShowSupplierList = true;
                        } else {
                            toaster.error(response.data.errorMessage);
                            $scope.EditSupplierInfo = true;
                            $scope.ShowSupplierList = false;
                        }
                        //$scope.message = response.data.successMessage;
                    }, function myError(response) {
                        toaster.error(response.data.errorMessage);
                        //$scope.EditModuleInfo = true;
                        //$scope.ShowModuleList = false;
                    });
                }
                else {
                    toaster.error("Enter ContactNo");
                }
            }
            else {
                toaster.error("Enter Address");
            }
        }
        else {
            toaster.error("Enter Company Name");
        }
    };



    $scope.DeleteSupplier = function (id) {
        $http({
            method: "GET",
            url: "/Supplier/Delete/?id=" + id
        }).then(function mySuccess(response) {
            if (response.data.success === true) {
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