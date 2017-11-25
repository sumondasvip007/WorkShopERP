app.controller('AddPartsInBusRegistrationNoFromStoreController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$uibModal", "$location", "$routeParams", "$filter", "$timeout", "AddPartsStoreToBusRegistrationNoServices", function ($scope, $http, $window, toaster, $route, $uibModal, fileUpload, $location, $routeParams, $filter, $timeout, AddPartsStoreToBusRegistrationNoServices) {


   
    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];



    $scope.dateOptions = {
     
        startingDay: 1,
    };
  
    $scope.format = 'yyyy/MM/dd';
    $scope.datetpicker = new Date();
    $scope.open = function () {
        $scope.popup1.opened = true;
    };
    $scope.popup1 = {
        opened: false
    };


    $scope.RegistrationList = {};
    $scope.RegistrationList.selected = null;
    $scope.RegistrationNoClick = function () {
        //console.log($scope.BusInformation.RegistrationNo);
        if (!$scope.RegistrationList.selected) {
            $scope.showBusInfo = false;
        } else {
            $http({
                method: "POST",
                url: "/PriceForPartsFromSupplier/SearchBusInformationForSpecificBusRegistrationNo",
                data: { RegistrationNo: $scope.RegistrationList.selected.RegistrationNo }
            }).then(function mySuccess(response) {
                if (response.data.success === true) {
                    $scope.BusInformationList = response.data.result;
                    $scope.showBusInfo = true;

                } else {
                    toaster.error(response.data.errorMessage);
                    $scope.showBusInfo = false;
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
    }

    //$scope.RegistrationNoList = {};

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


    $http({
        method: "GET",
        url: "/AddPartsInBusRegistrationNoFromStore/GetAllStore"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.storeList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }

    }, function myError(response) {

    });
    


    $scope.PartsLoad = function () {
        if ($scope.Store.StoreId) {
           
                $http({
                    method: 'POST',
                    url: '/AddPartsInBusRegistrationNoFromStore/GetAllPartsByStore',
                    data: { id: $scope.Store.StoreId }
                }).then(function mySuccess(response) {
                    if (response.data.success === true) {

                        $scope.Parts = response.data.result;

                    } else {
                        toaster.error(response.data.errorMessage);
                    }
                }, function myError(response) {

                });
        }
        else {
            $scope.Parts = null;
            toaster.error("Select a Store");
            $route.reload();

        }
    }




    var i = 0;
  
    $scope.checkValidation = function (parts, store, index, rowIndex, partIndex) {
        var requestedQuantity = parts.Quantity[index].toString();
        var partsStore = {
            storeId: store.StoreId,
            partsId: parts.PartsId
        }
       
        $http({
            method: 'POST',
            url: '/AddPartsInBusRegistrationNoFromStore/GetPartsAvailableQuantity',
            data: partsStore
        }).then(function mySuccess(response) { 
            if (response.data.success === true) {
               
                var availablepartsQuantity = response.data.result;
                var isPartsAvailable = 0;
                angular.forEach($scope.parts, function (scopeParts, key) {
                    if (parts.PartsId === scopeParts.PartsId) {
                        isPartsAvailable = (availablepartsQuantity - requestedQuantity);
                        if (isPartsAvailable < 0) {
                            var a = requestedQuantity.slice(0, -1);
                            $scope.parts[scopeParts.PartIndex].Quantity[index] = parseInt(a);
                            toaster.warning(availablepartsQuantity + " " + "Partss Available");
                            return;
                        }
                    }
                });
                //isPartsAvailable = (availablepartsQuantity - requestedQuantity);

                //if (isPartsAvailable < 0) {
                //    var a = requestedQuantity.slice(0, -1);
                //    $scope.parts[$scope.partIndex].Quantity[index] = parseInt(a);
                //    toaster.warning(availablepartsQuantity + " " + "Parts Available");
                //}
            } else {
                toaster.error(response.data.errorMessage);
            }
        }, function myError(response) {

        });
    }



    var PartsList = [];
    //--------- save  --------------
    $scope.save = function () {

        if ($scope.RegistrationList.selected != null) {


            var isvalidate = AddPartsStoreToBusRegistrationNoServices.CheckModelValidation($scope);
            if (isvalidate === false) {
                return;
            }


            var partsList = $scope.parts;
            angular.forEach($scope.parts, function(parts, key) {
                var Parts = {};
                //


                var storeToBus = {
                    StoreId: $scope.Store.StoreId,
                    PartsId: parts.PartsId,
                    Quantity: parts.Quantity,
                    UnitPrice: parts.UnitPrice
                };


                Parts.StoreId = storeToBus.StoreId;
                Parts.PartsId = storeToBus.PartsId;
                Parts.Quantity = storeToBus.Quantity[parts.QuatityIndex];
                Parts.UnitPrice = storeToBus.UnitPrice;
                PartsList.push(Parts);

                i++;

            });
            if (isvalidate === true && PartsList.length != 0) {
                $http({
                    method: 'POST',
                    url: '/AddPartsInBusRegistrationNoFromStore/AddPartsInBusRegistrationNoFromStore',
                    data: { registrationNo: $scope.RegistrationList.selected.RegistrationNo, buyDate: $scope.datetpicker, storeId: $scope.Store.StoreId, partsList: PartsList }
                }).then(function mySuccess(response) {
                    if (response.data.success === true) {
                        toaster.success(response.data.successMessage);
                        $route.reload();

                    } else {
                        toaster.error(response.data.errorMessage);
                    }
                }, function myError(response) {

                });
            }

        } else {
            toaster.error("Select Registration No");
        }

    }









    $scope.parts = [];
    var partsIndex = 0;
    $scope.selectParts = function (p, rowIndex) {
        var partIndex = $scope.parts.indexOf(p);


        if (partIndex === -1) {
            console.log(rowIndex);
            $scope.partIndex = partsIndex;
            partsIndex = partsIndex + 1;
            p.PartIndex = $scope.partIndex;
            $scope.parts.push(p); //Add the selected host into array
        } else {
            console.log(partIndex);
            $scope.parts.splice(partIndex, 1); //Remove the selected host
            partsIndex = partsIndex - 1;
            $scope.PartIndex = partsIndex;
            console.log($scope.parts);
        }
    };


}]);