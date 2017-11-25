app.controller('EmployeeInformationController', ["$scope", "$http","$window", "toaster", "$route","fileUpload", "$timeout", function ($scope, $http,$window, toaster, $route,fileUpload, $timeout) {

    $scope.EditEmployeeInfo = false;
    $scope.ShowEmployeeList = true;
    $scope.myValue = true;
    $scope.myNidValue = true;
    $scope.drivingLicenseField = true;
    $scope.myDrivingLicenseValue = true;
    $scope.itemsPerPage = [2, 5, 10, 20, 30, 40, 50];


    $scope.SelectDriver=function(DesignationId) {
        if (DesignationId === 8) {
            $scope.drivingLicenseField = false;
        } else {
            $scope.drivingLicenseField = true;
        }
    }


    $http({
        method: "GET",
        url: "/Designation/GetAllDesignation"
    }).then(function mySuccess(response) {
        $scope.DesignationList = response.data.result;

    }, function myError(response) {

    });

    $http({
        method: "GET",
        url: "/Route/GetAllRoute"
    }).then(function mySuccess(response) {
        $scope.RouteList = response.data.result;

    }, function myError(response) {

    });

    $scope.SaveEmployeeInformation = function() {
        if ($scope.EmployeeInformationAddForm.$valid) {
            var file = $scope.myFile;
            var nidFile = $scope.myNidFile;
            var drivingLicenseFile = $scope.myDrivingLicenseFile;
            console.log('file is ');
            console.dir(file);
            console.log('nidFile is ');
            console.dir(nidFile);
            console.log('drivingLicenseFile is ');
            console.dir(drivingLicenseFile);


            var fd = new FormData();
            fd.append('file', file);
            fd.append('nidFile', nidFile);
            fd.append('drivingLicenseFile', drivingLicenseFile);
            fd.append('EmployeeName', $scope.Employee.EmployeeName);
            fd.append('EmployeeAddress', $scope.Employee.EmployeeAddress);
            fd.append('ContactNumber', $scope.Employee.ContactNumber);
            fd.append('EmployeeNid', $scope.Employee.EmployeeNid);
            fd.append('EmployeeEmail', $scope.Employee.EmployeeEmail);
            fd.append('DesignationId', $scope.Employee.DesignationId);
            fd.append('Salary', $scope.Employee.Salary);
            fd.append('RouteId', $scope.Employee.RouteId);

            console.log(fd);
            uploadUrl = "/EmployeeInformation/SaveEmployeeInformation";

            // Simple GET request example:
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }

            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                    toaster.success(response.data.successMessage);
                    $scope.Employee = {};
                    $route.reload();
                } else {
                    toaster.error(response.data.errorMessage);
                }
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });
        } else {
            toaster.error("Please Enter all required Field");
        }
    };

    $scope.fileReaderSupported = window.FileReader != null;


    $scope.drivingLicenseImageChanged = function (files) {
        $scope.myDrivingLicenseValue = false;
        $scope.myInsertValue = true;
        if (files != null) {
            var file = files[0];
            if ($scope.fileReaderSupported && file.type.indexOf('image') > -1) {
                $timeout(function () {
                    var fileReader = new FileReader();
                    fileReader.readAsDataURL(file);
                    fileReader.onload = function (e) {
                        $timeout(function () {
                            $scope.c = e.target.result;
                        });
                    }
                });
            }
        }
    };

    $scope.nidImageChanged = function (files) {
        $scope.myNidValue = false;
        $scope.myInsertValue = true;
        if (files != null) {
            var file = files[0];
            if ($scope.fileReaderSupported && file.type.indexOf('image') > -1) {
                $timeout(function () {
                    var fileReader = new FileReader();
                    fileReader.readAsDataURL(file);
                    fileReader.onload = function (e) {
                        $timeout(function () {
                            $scope.b = e.target.result;
                        });
                    }
                });
            }
        }
    };



    $scope.photoChanged = function (files) {
        $scope.myValue = false;
        $scope.myInsertValue = true;
        if (files != null) {
            var file = files[0];
            if ($scope.fileReaderSupported && file.type.indexOf('image') > -1) {
                $timeout(function () {
                    var fileReader = new FileReader();
                    fileReader.readAsDataURL(file);
                    fileReader.onload = function (e) {
                        $timeout(function () {
                            $scope.a = e.target.result;
                        });
                    }
                });
            }
        }
    };

    $http({
        method: "GET",
        url: "/EmployeeInformation/GetAllEmployeeInformation"
    }).then(function mySuccess(response) {
        if (response.data.success == true) {
            $scope.EmployeeList = response.data.result;
        } else {
            toaster.error(response.data.errorMessage);
        }
    }, function myError(response) {
                toaster.error(response.data.errorMessage);
            });


    $scope.DeleteEmployee = function (id) {
        $http({
            method: "GET",
            url: "/EmployeeInformation/Delete/?id=" + id
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


    $scope.EditEmployee = function (row) {
        $scope.Employee = row;
        if ($scope.Employee.DesignationId === 8) {
            $scope.drivingLicenseField = false;
        } else {
            $scope.drivingLicenseField = true;
        }
        $scope.EditEmployeeInfo = true;
        $scope.ShowEmployeeList = false;
    }

    $scope.UpdateEmployeeInformation = function () {
        if ($scope.EmployeeInformationEditForm.$valid) {

            var file = $scope.myFile;
            var nidFile = $scope.myNidFile;
            var drivingLicenseFile = $scope.myDrivingLicenseFile;
            console.log('file is ');
            console.dir(file);
            console.log('nidFile is ');
            console.dir(nidFile);
            console.log('drivingLicenseFile is ');
            console.dir(drivingLicenseFile);
            

            var fd = new FormData();
            fd.append('file', file);
            fd.append('nidFile', nidFile);
            fd.append('drivingLicenseFile', drivingLicenseFile);
            fd.append('EmployeeId', $scope.Employee.EmployeeId);
            fd.append('EmployeeName', $scope.Employee.EmployeeName);
            fd.append('EmployeeAddress', $scope.Employee.EmployeeAddress);
            fd.append('ContactNumber', $scope.Employee.ContactNumber);
            fd.append('EmployeeNid', $scope.Employee.EmployeeNid);
            fd.append('EmployeeEmail', $scope.Employee.EmployeeEmail);
            fd.append('DesignationId', $scope.Employee.DesignationId);
            fd.append('Salary', $scope.Employee.Salary);
            fd.append('EmployeeImage', $scope.Employee.EmployeeImage);
            fd.append('EmployeeNidImage', $scope.Employee.EmployeeNidImage);
            fd.append('DrivingLicenseImage', $scope.Employee.DrivingLicenseImage);
            fd.append('RouteId', $scope.Employee.RouteId);

            console.log(fd);
            uploadUrl = "/EmployeeInformation/Update";

            // Simple GET request example:
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }

            }).then(function mySuccess(response) {
                if (response.data.success == true) {
                toaster.success(response.data.successMessage);
               
                    $route.reload();
                    $scope.EditEmployeeInfo = false;
                    $scope.ShowEmployeeList = true;
                }
                else {
                    toaster.error(response.data.errorMessage);
                    $scope.EditEmployeeInfo = true;
                    $scope.ShowEmployeeList = false;
                }
            }, function myError(response) {
                toaster.error(response.data.errorMessage);
                //$scope.EditEmployeeInfo = true;
                //$scope.ShowEmployeeList = false;
            });
        }
        else {
            toaster.error("Enter all required Field");
        }

    };




}]);