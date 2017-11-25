app.controller('PartsStatusInStoreController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$uibModal", "$location", "$routeParams", "$filter", "$timeout", "AddPartsStoreToBusRegistrationNoServices", function ($scope, $http, $window, toaster, $route, $uibModal, fileUpload, $location, $routeParams, $filter, $timeout, AddPartsStoreToBusRegistrationNoServices) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    $scope.ViewReport=function() {
        if (!$scope.Store.StoreId) {
            $scope.reportButton = false;
        }
    }


    $http({
        method: "GET",
        url: "/PartsStatusInStore/GetAllStore"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.storeList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }

    }, function myError(response) {

    });



    $scope.PartsLoad = function () {
        if ($scope.PartsStatusInStoreForm.$valid) {
            if ($scope.Store.StoreId) {

                $http({
                    method: 'POST',
                    url: '/PartsStatusInStore/GetAllPartsByStore',
                    data: { id: $scope.Store.StoreId }
                }).then(function mySuccess(response) {
                    if (response.data.success === true) {

                        $scope.Parts = response.data.result;
                        $scope.totalAmount = response.data.TotalAmount;
                        $scope.reportButton = true;

                    } else {
                        toaster.error(response.data.errorMessage);
                        $scope.reportButton = false;
                    }
                }, function myError(response) {

                });
            } else {
                $scope.Parts = null;
                toaster.error("Select a Store");
                $scope.reportButton = false;
                $route.reload();

            }
        } else {
            $scope.Parts = null;
            toaster.error("Select a Store");
            $scope.reportButton = false;
            $route.reload();
        }
    }

    $scope.GenerateReportForSearchResult = function () {
        if ($scope.PartsStatusInStoreForm.$valid) {
            $http({

                method: 'POST',
                url: '/PartsStatusInStore/GenerateReportForSearchResult/',
                data: { id: $scope.Store.StoreId }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.Parts = response.data.result;
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
            toaster.error("Select a Store");
        }

    };




}]);