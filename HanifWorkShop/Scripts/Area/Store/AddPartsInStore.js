app.controller('AddPartsInStoreController', ["$scope", "$http", "$window", "toaster", "$route", "fileUpload", "$timeout", function ($scope, $http, $window, toaster, $route, fileUpload, $timeout) {



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
        url: "/PartsAddInStore/GetAllStore"
    }).then(function mySuccess(response) {
        if (response.data.success === true) {
            $scope.storeList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {

    });



    $scope.choices = [{ id: 'choice1' }];

    $scope.addNewChoice = function () {
        var newItemNo = $scope.choices.length + 1;
        $scope.choices.push({ 'id': 'choice' + newItemNo });
    };

    $scope.removeChoice = function (index) {
        $scope.choices.splice(index, 1);
    };

    $scope.addPartsInStore = function () {
        //if ($scope.SelectedRegistrationNo != null) {
       


        if ($scope.AddPartsInStoreForm.$valid) {
                $http({
                    method: 'POST',
                    url: '/PartsAddInStore/AddPartsInStore/',
                    //data: { other: $scope.Other, price: $scope.Price, registrationNo: $scope.SelectedRegistrationNo.RegistrationNo, buyPartsFromSupplier: $scope.choices }
                    data: {addDate: $scope.datetpicker, addPartInStore: $scope.choices }
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

        }
    


}]);