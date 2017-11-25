app.controller('UserPermissionController', ["$scope", "$http", "toaster", "$route", function ($scope, $http, toaster, $route) {

    $scope.LoadInitial = function () {
        $http.get('/Account/GetAllUserForCurrentAgency')
           .then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.userList = response.data.result;
                } else {
                    toaster.error(response.data.errorMessage);
                }

            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
    }


    //$http({
    //    method: "GET",
    //    url: "/Account/GetAllUser/"
    //}).then(function mySuccess(response) {
    //    $scope.UserList = response.data.result;

    //}, function myError(response) {

    //});


    $scope.LoadActionListForUser = function (userId) {
        $http.get('/Account/UserMenuActionForPermission?userId=' + userId)
        .then(function mySuccess(response) {
                if (response.data.success == true) {
                    $scope.bag = response.data.result;
                    console.log($scope.bag);
                }
            }, function myError(response) {

        });
    };

    $scope.SaveUserPermission = function () {
        if ($scope.UserPermissionForm.$valid) {
            $scope.selectedMenu = [];
            for (var i = 0; i < $scope.bag.length; i++) {
                for (var j = 0; j < $scope.bag[i].children.length; j++) {
                    if ($scope.bag[i].children[j].selected === true) {
                        $scope.selectedMenu.push($scope.bag[i].children[j].ActionId);
                    }
                }
            }
            $http({
                method: 'POST',
                url: '/Account/SaveUserPermission',
                data: { id: $scope.selectedMenu, userId: $scope.User.user_id }
            })
                .then(function mySuccess(response) {
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
            toaster.error("Please select a  username");
        }
    }
    

}]);