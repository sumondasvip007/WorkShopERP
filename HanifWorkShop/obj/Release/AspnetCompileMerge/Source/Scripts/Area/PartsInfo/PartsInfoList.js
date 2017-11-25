app.controller('showAllPartsInfoController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.editPartsInfo = false;
    $scope.ShowPartsInfoList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/PartsInformation/GetAllPartsInfo"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.partsInfoList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {

    });


    $scope.EditPartsInfo = function (row) {
        $scope.PartsInfo = row;
        $scope.editPartsInfo = true;
        $scope.ShowPartsInfoList = false;
    }

    $scope.UpdatePartsInfo = function () {
        if ($scope.editPartsInfoForm.PartsName.$valid) {
            if ($scope.editPartsInfoForm.BasePrice.$valid) {
                    $http({
                        method: "POST",
                        url: "/PartsInformation/Update",
                        data: $scope.PartsInfo
                    }).then(function mySuccess(response) {
                        if (response.data.success === true) {
                            toaster.success(response.data.successMessage);
                            $route.reload();
                            $scope.editPartsInfo = false;
                            $scope.ShowPartsInfoList = true;
                        } else {
                            toaster.error(response.data.errorMessage);
                            $scope.editPartsInfo = true;
                            $scope.ShowPartsInfoList = false;
                        }
                        //$scope.message = response.data.successMessage;
                    }, function myError(response) {
                        toaster.error(response.data.errorMessage);
                        //$scope.EditModuleInfo = true;
                        //$scope.ShowModuleList = false;
                    });
                }
            else {
                toaster.error("Enter Base Price");
            }
        }
        else {
            toaster.error("Enter Parts Name");
        }
    };



    $scope.DeletePartsInfo = function (id) {
        $http({
            method: "GET",
            url: "/PartsInformation/Delete/?id=" + id
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