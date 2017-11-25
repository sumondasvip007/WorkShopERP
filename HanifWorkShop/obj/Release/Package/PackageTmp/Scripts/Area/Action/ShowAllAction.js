app.controller('showAllActionController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.EditActionInfo = false;
    $scope.ShowActionList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    $http({
        method: "GET",
        url: "/Action/GetAllAction"
    }).then(function mySuccess(response) {
        $scope.ActionList = response.data.result;

    }, function myError(response) {

    });


    $http({
        method: "GET",
        url: "/Module/GetModules"
    }).then(function mySuccess(response) {
        $scope.AllModule = response.data.result;

    }, function myError(response) {

    });


    $scope.EditAction = function (row) {
        $scope.Action = row;
        $scope.EditActionInfo = true;
        $scope.ShowActionList = false;
    }

    $scope.UpdateAction = function () {
        if ($scope.addActionForm.actionName.$valid) {
            if ($scope.addActionForm.actionDisplayName.$valid) {
                if ($scope.addActionForm.actionUrl.$valid) {
                    if ($scope.addActionForm.moduleId.$valid) {
                    $http({
                        method: "POST",
                        url: "/Action/Update",
                        data: $scope.Action
                    }).then(function mySuccess(response) {
                        if (response.data.success == true) {
                            toaster.success(response.data.successMessage);
                            $route.reload();
                            $scope.EditActionInfo = false;
                            $scope.ShowActionList = true;
                        } else {
                            toaster.error(response.data.errorMessage);
                            $scope.EditActionInfo = true;
                            $scope.ShowActionList = false;
                        }
                        //$scope.message = response.data.successMessage;
                    }, function myError(response) {
                        toaster.error(response.data.errorMessage);
                        $scope.EditActionInfo = true;
                        $scope.ShowActionList = false;
                    });
                }
                    else {
                        toaster.error("Select a Module");
                    }
                }
                else {
                    toaster.error("Enter Action Url");
                }
            }
            else {
                toaster.error("Enter Action's Display Name");
            }
        }
        else {
            toaster.error("Enter Action Name");
        }
    };

    $scope.DeleteAction = function (id) {
        $http({
            method: "GET",
            url: "/Action/Delete/?id=" + id
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


}]);