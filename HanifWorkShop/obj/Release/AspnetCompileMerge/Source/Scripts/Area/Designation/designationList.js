app.controller('showAllDesignationController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.EditDesignationInfo = false;
    $scope.ShowDesignationList = true;

    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $http({
        method: "GET",
        url: "/Designation/GetAllDesignation"
    }).then(function mySuccess(response) {
        $scope.DesignationList = response.data.result;

    }, function myError(response) {

    });



    $scope.EditDesignation = function (row) {
        $scope.Designation = row;
        $scope.EditDesignationInfo = true;
        $scope.ShowDesignationList = false;
    }

    $scope.UpdateDesignation = function () {
        if ($scope.addDesignationForm.DesignationName.$valid) {
         
                    $http({
                        method: "POST",
                        url: "/Designation/Update",
                        data: $scope.Designation
                    }).then(function mySuccess(response) {
                        if (response.data.success == true) {
                            toaster.success(response.data.successMessage);
                            $route.reload();
                            $scope.EditDesignationInfo = false;
                            $scope.ShowDesignationList = true;
                        }
                        else {
                            toaster.error(response.data.errorMessage);
                            $scope.EditDesignationInfo = true;
                            $scope.ShowDesignationList = false;
                        }
                        //$scope.message = response.data.successMessage;
                    }, function myError(response) {
                        toaster.error(response.data.errorMessage);
                        $scope.EditDesignationInfo = true;
                        $scope.ShowDesignationList = false;
                    });
                }
                else {
                    toaster.error("Enter Designation Name");
                }
       
    };



    $scope.DeleteDesignation = function (id) {
        $http({
            method: "GET",
            url: "/Designation/Delete/?id=" + id
        }).then(function mySuccess(response) {
            if (response.data.success == true) {
                $route.reload();
                toaster.success(response.data.successMessage);
            }
            else {
                toaster.error(response.data.errorMessage);
            }
        }, function myError(response) {
            toaster.error(response.data.errorMessage);
        });
    }






}]);