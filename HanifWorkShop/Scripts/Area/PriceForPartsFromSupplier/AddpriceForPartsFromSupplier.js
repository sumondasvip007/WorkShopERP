app.controller('AddPriceForPartsFromSupplierController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];

    

    $scope.dateOptions = {
        //dateDisabled: disabled,   //disables the popup
        //minDate: new Date(),
        startingDay: 1,
    };
    /*// Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }*/
    $scope.format = 'yyyy/MM/dd';
    $scope.datetpicker = new Date();
    $scope.open = function () {
        $scope.popup1.opened = true;
    };
    $scope.popup1 = {
        opened: false
    };



 

    //$scope.SelectedRegistrationNo = null;

    //$scope.RegistrationNoClick = function (selected) {
    //    if (selected) {
    //        $scope.SelectedRegistrationNo = selected.originalObject;

    //        $http({
    //            method: "POST",
    //            url: "/PriceForPartsFromSupplier/SearchBusInformationForSpecificBusRegistrationNo",
    //            data: { RegistrationNo: $scope.SelectedRegistrationNo.RegistrationNo }
    //        }).then(function mySuccess(response) {
    //            if (response.data.success === true) {
    //                $scope.BusInformationList = response.data.result;
    //                $scope.showBusInfo = true;

    //            } else {
    //                toaster.error(response.data.errorMessage);
    //                $scope.showBusInfo = false;
    //            }

    //        }, function myError(response) {
    //            toaster.error(response.data.errorMessage);
    //        });
    //    } else {
    //        $scope.showBusInfo = false;
    //    }
    //}

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
        url: "/PartsInformation/GetAllPartsInfo"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.partsInfoList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {

    });

    $http({
        method: "GET",
        url: "/PriceForPartsFromSupplier/GetAllSuppliers"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.supplierList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {

    });



    //$scope.choices = [{ id: 'choice1', 'PartsId': "", 'SupplierId': "", 'Price': "" }];

    //$scope.addNewChoice = function () {
    //    var newItemNo = $scope.choices.length + 1;
    //    $scope.choices.push({ 'id': 'choice' + newItemNo, 'PartsId': "", 'SupplierId': "", 'Price': "" });
    //};



    $scope.choices = [{ id: 'choice1'}];

    $scope.addNewChoice = function () {
        var newItemNo = $scope.choices.length + 1;
        $scope.choices.push({ 'id': 'choice' + newItemNo});
    };

    $scope.removeChoice = function (index) {
        $scope.choices.splice(index, 1);
    };

    //$scope.GetTotalAmount = function() {
    //    $scope.totalAmount = 0;
    //    for (var i = 0; i < $scope.choices.length; i++) {
    //        $scope.totalAmount = (parseInt($scope.choices[i].Price | 0)) + parseInt($scope.totalAmount);
    //    }
    //};


    //$scope.GetTotalPrice=function() {
    //    $scope.choice.Price = (($scope.choice.Quantity * $scope.choice.UnitPrice) || 0);

    //    var totalPrice = $scope.choice.Price;

    //}

    

    //$scope.Total = function (index) {
    //    var total = (parseInt($scope.choices[index].Quantity | 0) * parseInt($scope.choices[index].UnitPrice | 0));
    //    return total | 0;
    //};

   
    //$scope.totalPrice = 0;
    //$scope.Total = function (index) {
    //    $scope.totalPrice = (parseInt($scope.choices[index].Quantity | 0) * parseInt($scope.choices[index].UnitPrice | 0));
       
    //};


    $scope.otherItems = [{ id: 'otherItem1' }];

    $scope.addNewOtherItem = function () {
        var newItemNo = $scope.otherItems.length + 1;
        $scope.otherItems.push({ 'id': 'otherItem' + newItemNo });
    };
    

    $scope.removeOtherItem = function (index) {
        $scope.otherItems.splice(index, 1);
    };
    




    $scope.saveBuyPartsFromSupplier = function () {
        //if ($scope.SelectedRegistrationNo != null) {
        if ($scope.RegistrationList.selected != null) {


            if ($scope.AddPriceForPartsFromSupplierForm.$valid) {
                $http({
                    method: 'POST',
                    url: '/PriceForPartsFromSupplier/AddPriceForPartsFromSupplier/',
                    //data: { other: $scope.Other, price: $scope.Price, registrationNo: $scope.SelectedRegistrationNo.RegistrationNo, buyPartsFromSupplier: $scope.choices }
                    //data: { other: $scope.Other, price: $scope.Price, registrationNo: $scope.RegistrationList.selected.RegistrationNo,buyDate:$scope.datetpicker, buyPartsFromSupplier: $scope.choices }
                    data: { otherExpense: $scope.otherItems, registrationNo: $scope.RegistrationList.selected.RegistrationNo, buyDate: $scope.datetpicker, buyPartsFromSupplier: $scope.choices }
                }).then(function mySuccess(response) {
                    if (response.data.success === true) {
                        toaster.success(response.data.successMessage);
                        $route.reload();
                    } else {
                        toaster.error(response.data.errorMessage);
                    }
                }, function myError(response) {
                    toaster.error(response.data.errorMessage);
                });
            } else {
                toaster.error("Please Fill Up all required Fill.");
            }

        } else {
            toaster.error("Select Registration No");
        }
    }


}]);