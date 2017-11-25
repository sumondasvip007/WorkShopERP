app.controller('ShowBuyPartsHistoryForSpecificDateFromSupplierController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    $scope.SupplierClick = function () {
        if (!$scope.Supplier.SupplierId) {
            $scope.reportButton = false;
        }
    }



    //$scope.dateOptions = {
 
    //    startingDay: 1,
    //};
  
    //$scope.format = 'yyyy/MM/dd';
    //$scope.datetpicker = new Date();
    //$scope.open = function () {
    //    $scope.popup1.opened = true;
    //};
    //$scope.popup1 = {
    //    opened: false
    //};






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

    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ShowBuyPartsHistoryForSpecificDateFromSupplierForm.$valid) {
            $http({

                method: 'POST',
                url: '/BuyPartsHistoryForSpecificDateFromSupplier/GenerateReportForSearchResult/',
                data: { supplierId: $scope.Supplier.SupplierId, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.BuyPartsList = response.data.result;
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
            toaster.error("Select Supplier Name");
        }

    };

    $scope.SearchBuyPartsHistoryForSpecificDateFromSupplier = function () {
        if ($scope.ShowBuyPartsHistoryForSpecificDateFromSupplierForm.$valid) {

            $http({
                method: "POST",
                url: "/BuyPartsHistoryForSpecificDateFromSupplier/SearchBuyPartsHistoryForSpecificDateFromSupplier",
                data: { supplierId: $scope.Supplier.SupplierId, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.BuyPartsList = response.data.result;
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