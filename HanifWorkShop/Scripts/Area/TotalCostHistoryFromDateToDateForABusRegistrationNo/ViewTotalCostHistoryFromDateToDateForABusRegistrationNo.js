app.controller('ViewTotalCostHistoryFromDateToDateForABusRegistrationNoController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    //$scope.RegistrationNoClick = function () {
    //    if (!RegistrationList.selected.RegistrationNo) {
    //        $scope.reportButton = false;
    //    }
    //}

    $scope.RegistrationList = {};
    $scope.RegistrationList.selected = null;


    $http({
        method: "GET",
        url: "/PriceForPartsFromSupplier/GetAllRegistrationNo"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.RegistrationNoList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }

    }, function myError(response) {

    });




    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ViewTotalCostHistoryFromDateToDateForABusRegistrationNoForm.$valid) {
            $http({

                method: 'POST',
                url: '/TotalCostHistoryFromDateToDateForABusRegistrationNo/GenerateReportForSearchResult/',
                data: { registrationNo: $scope.RegistrationList.selected.RegistrationNo, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.TotalCostList = response.data.result;
                    $scope.totalAmount = response.data.TotalAmount;
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
            toaster.error("Select or Registration No");
        }

    };

  
    $scope.SearchTotalCostHistoryFromDateToDateForABusRegistrationNo = function () {
        if ($scope.ViewTotalCostHistoryFromDateToDateForABusRegistrationNoForm.$valid) {

            $http({
                method: "POST",
                url: "/TotalCostHistoryFromDateToDateForABusRegistrationNo/SearchTotalCostHistoryFromDateToDateForABusRegistrationNo",
                data: { registrationNo: $scope.RegistrationList.selected.RegistrationNo, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.TotalCostList = response.data.result;
                    $scope.totalAmount = response.data.TotalAmount;
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
            toaster.error("Select All field");
            $scope.reportButton = false;
        }

    };





}]);