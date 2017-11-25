app.controller('addModuleController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.addModule = function () {
        if ($scope.addModuleForm.moduleName.$valid) {
            if ($scope.addModuleForm.moduleOrder.$valid) {
                if ($scope.addModuleForm.moduleIcon.$valid) {
                    $http({
                        method: 'POST',
                        url: '/Module/AddModule/',
                        data: $scope.Module
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
                    toaster.error("Enter Module Icon");
                }
            }
            else {
                toaster.error("Enter Module Order");
            }
        }
        else {
            toaster.error("Enter Module Name");
        }
    };
}]);