app.controller('showAllModuleController', ["$scope", "$http", "toaster", "$route", function($scope, $http, toaster, $route) {

    $scope.EditModuleInfo = false;
    $scope.ShowModuleList = true;

    $scope.itemsPerPage = [2,5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/Module/GetAllModule"
    }).then(function mySuccess(response) {
        $scope.ModuleList = response.data.result;

    }, function myError(response) {

    });


    $http({
        method: "GET",
        url: "/Module/GetModules"
    }).then(function mySuccess(response) {
        $scope.AllModule = response.data.result;

    }, function myError(response) {

    });

    

    $http({
        method: "GET",
        url: "/Action/GetAllAction"
    }).then(function mySuccess(response) {
        $scope.ActionList = response.data.result;

    }, function myError(response) {

    });


    $scope.EditModule = function (row) {
        $scope.Module = row;
        $scope.EditModuleInfo = true;
        $scope.ShowModuleList = false;
    }

    $scope.UpdateModule = function () {
        if ($scope.addModuleForm.moduleName.$valid) {
            if ($scope.addModuleForm.moduleOrder.$valid) {
                if ($scope.addModuleForm.moduleIcon.$valid) {
        $http({
            method: "POST",
            url: "/Module/Update",
            data: $scope.Module
        }).then(function mySuccess(response) {
            if (response.data.success == true) {
                toaster.success(response.data.successMessage);
                $route.reload();
                $scope.EditModuleInfo = false;
                $scope.ShowModuleList = true;
            } else {
                toaster.error(response.data.errorMessage);
                $scope.EditModuleInfo = true;
                $scope.ShowModuleList = false;
            }
            //$scope.message = response.data.successMessage;
        }, function myError(response) {
            toaster.error(response.data.errorMessage);
            //$scope.EditModuleInfo = true;
            //$scope.ShowModuleList = false;
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
 


    $scope.DeleteModule = function (id) {
        $http({
            method: "GET",
            url: "/Module/Delete/?id=" + id
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





    //$scope.GetAllActionForSpecificModule = function (ModuleId) {
    //    $http({
    //        method: "GET",
    //        url: "/Action/GetAllActionForSpecificModule/?ModuleId="+ModuleId
    //    }).then(function mySuccess(response) {
    //        $scope.AllActionList = response.data.result;

    //    }, function myError(response) {

    //    });
    //}



    // collapsed[row] = !collapsed[row]
    //$scope.addClass = function($event) {
    //    $scope.tclass = "block";
    //    var el = (function() {
    //        if ($event.next("ul")) {
    //            return angular.element($event.next("ul"));
    //        }

    //        el.

    //    });

    //}
}]);