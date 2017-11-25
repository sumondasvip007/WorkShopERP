app.controller('addActionController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {


    //$http({
    //    method: "GET",
    //    url: "/Module/GetAllModule"
    //}).then(function mySuccess(response) {
    //    $scope.ModuleList = response.data.result;

    //}, function myError(response) {

    //});

    $http({
        method: "GET",
        url: "/Module/GetModules"
    }).then(function mySuccess(response) {
        $scope.AllModule = response.data.result;

    }, function myError(response) {

    });


    $scope.addAction = function () {
        if ($scope.addActionForm.actionName.$valid) {
            if ($scope.addActionForm.actionDisplayName.$valid) {
                if ($scope.addActionForm.actionUrl.$valid) {
                    if ($scope.addActionForm.moduleId.$valid) {
                        $http({
                            method: 'POST',
                            url: '/Action/AddAction/',
                            data: $scope.Action
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
}]);