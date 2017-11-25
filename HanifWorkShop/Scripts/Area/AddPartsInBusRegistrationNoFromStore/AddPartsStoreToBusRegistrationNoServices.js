app.factory('AddPartsStoreToBusRegistrationNoServices', function ($http, toaster) {
    return {
        CheckModelValidation: function ($scope) {
            var isvalidate = true;
            if ($scope.AddPartsInBusRegistrationNoFromStoreForm.StoreId.$invalid) {
                toaster.error("select a  Store");
                return false;
            }
          
            if ($scope.parts.length === 0) {
                toaster.error("please select a Parts");
                return false;
            }
            angular.forEach($scope.parts, function (parts, key) {

                if (parts.Quantity == null || parts.Quantity === 0 || parts.Quantity === "") {
                    toaster.error("please give a parts quantity");
                    isvalidate = false;
                    return;
                }
            });
            angular.forEach($scope.parts, function (parts, key) {

                if (parts.Quantity) {
                    if (parts.Quantity[parts.QuatityIndex] == null || parts.Quantity[parts.QuatityIndex] === 0 || parts.Quantity[parts.QuatityIndex] === "") {
                        toaster.error("please give a Parts quantity");
                        isvalidate = false;
                        return;
                    }
                }

            });

            return isvalidate;
        }

    }
});