app.controller('ViewBusInformationForSpecificBusRegistrationNoController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    //$scope.RegistrationNoClick = function () {
    //    if (!$scope.BusInformation.RegistrationNo) {
    //        $scope.reportButton = false;
    //    }
    //}

    $scope.RegistrationList = {};
    $scope.RegistrationList.selected = null;

    $http({
        method: "GET",
        url: "/BusInformation/GetAllRegistrationNo"
    }).then(function mySuccess(response) {
        if (response.data.success == true) {
            $scope.RegistrationNoList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }

    }, function myError(response) {

    });

    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ViewBusInformationForSpecificBusRegistrationNoForm.$valid) {
            $http({

                method: 'POST',
                url: '/BusInformation/GenerateReportForSearchResult/',
                data: { RegistrationNo: $scope.RegistrationList.selected.RegistrationNo }
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.BusInformationList = response.data.result;
                    toaster.success(response.data.successMessage);
                    $scope.reportButton = true;

                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.reportButton = false;
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Select Registration No");
        }

    };

    $scope.SearchBusInformationForSpecificBusRegistrationNo = function () {
        if ($scope.ViewBusInformationForSpecificBusRegistrationNoForm.$valid) {

            $http({
                method: "POST",
                url: "/BusInformation/SearchBusInformationForSpecificBusRegistrationNo",
                //data: { RegistrationNo: $scope.BusInformation.RegistrationNo }
                data: { RegistrationNo: $scope.RegistrationList.selected.RegistrationNo }
            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.BusInformationList = response.data.result;
                    $scope.reportButton = true;

                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.reportButton = false;
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Select Registration No");
            $scope.reportButton = false;
        }

    };





}]);