app.controller('addAspNetUserController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.UserEditHideDiv = true;
    $scope.UserListHideDiv = false;

    $scope.itemsPerPage = [2,5,10, 15, 20, 25, 30];
    $scope.CheckPassword = function () {
        if ($scope.AspNetUser.Password != $scope.AspNetUser.ConfirmPassword) {
            $scope.AspNetUserForm.ConfirmPassword.$setValidity('mismatch', false);
        }
            // else if()
        else {
            //toastr.success('Password Matched');
            $scope.AspNetUserForm.ConfirmPassword.$setValidity('mismatch', true);
        }
    };

    $scope.SaveUser = function () {
        if ($scope.AspNetUserForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/Register/',
                data: $scope.AspNetUser
            }).then(function mySuccess(response) {
                toaster.success(response.data.successMessage);
                $route.reload();
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        }
        else {
            toaster.error("Please Enter all required Field");
        }
    };


    $http({
        method: "GET",
        url: "/Account/GetAllUser/"
    }).then(function mySuccess(response) {
        $scope.UserList = response.data.result;

    }, function myError(response) {

    });


    //$scope.AllUserList = function () {
    //    $scope.UserEditHideDiv = true;
    //    $scope.UserList = [];
    //    $http.get('/Account/GetAllUser/').
    //      success(function (data) {
    //          if (data.success) {
    //              $scope.UserList = data.result;
    //          }
    //          else {
    //              toastr.error(data.errorMessage);
    //          }
    //      }).
    //      error(function (XMLHttpRequest, textStatus, errorThrown) {
    //          toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
    //      });
    //};

    $scope.DeleteUser = function (Id) {
            $http.get('/Account/DeleteUser/?id=' + Id)
               .then(function mySuccess(response) {
                   $route.reload();
                   toaster.success(response.data.successMessage);
               }, function myError(response) {
                   toaster.error(response.data.errorMessage);
               });
        }
  

    $scope.EditUser = function (row) {
        $scope.AspNetUser = row;
        $scope.UserEditHideDiv = false;
        $scope.UserListHideDiv = true;

    };
    //$scope.CancelEdit = function () {
    //    $route.reload();
    //    $scope.UserEditHideDiv = true;
    //    $scope.UserListHideDiv = false;
    //}

    $scope.UpdateUser = function () {

        if ($scope.AspNetUserForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/UpdateUser/',
                data: $scope.AspNetUser
            }).then(function mySuccess(response) {
                toaster.success(response.data.successMessage);
                $route.reload();
                $scope.UserEditHideDiv = true;
                $scope.UserListHideDiv = false;
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
                $scope.UserEditHideDiv = false;
                $scope.UserListHideDiv = true;
            });
        }
        else {
            toaster.error("Please Fill Up all field");
        }
    };

}]);