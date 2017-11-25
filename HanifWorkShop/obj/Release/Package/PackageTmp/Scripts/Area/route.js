app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        //.when("/", {
        //    templateUrl: "/Home/Home"
        //})
        .when("/", {
            templateUrl: '/Account/Login'
        })
        .when('/Index',
        {
            templateUrl: '/Home/Home'
        })
        .when('/RedirectToMain',
        {
            templateUrl: 'Home/RedirectToMain'
        })
        .when('/Login',
        {
            templateUrl: '/Account/Login'

        })
        .when('/UserAdd',
        {
            templateUrl: '/Account/AddAspNetUser'
        })
        .when('/UserList',
        {
            templateUrl: '/Account/AspNetUserList'
        })
        .when('/UserPermissions',
        {
            templateUrl: '/Account/UserActionMapping'

        })
        .when("/AddModule", {
            templateUrl: "/Module/AddModule"
        })
        .when("/ShowAllModule", {
            templateUrl: "/Module/ShowAllModule"
        })
        .when("/AddAction", {
            templateUrl: "/Action/AddAction"
        })
        .when("/ShowAllAction", {
            templateUrl: "/Action/ShowAllAction"
        })
        .when("/AddDesignation", {
            templateUrl: "/Designation/AddDesignation"
        })
        .when("/ShowAllDesignation", {
            templateUrl: "/Designation/ShowAllDesignation"
        })
        .when('/EmployeeInformationAdd', {
            templateUrl: '/EmployeeInformation/SaveEmployeeInformation'
        })
        .when('/EmployeeInformationList', {
            templateUrl: '/EmployeeInformation/EmployeeInformationList'
        })
        .when('/AddRoute', {
            templateUrl: '/Route/AddRoute'
        })
        .when('/ShowAllRoute', {
            templateUrl: '/Route/ShowAllRoute'
        })
        .when('/AddBusInformation', {
            templateUrl: '/BusInformation/AddBusInformation'
        })
        .when('/ShowAllBusInformation', {
            templateUrl: '/BusInformation/ShowAllBusInformation'
        })
     .when('/ViewAllEmployeeInformationForSpecificBusRoute', {
         templateUrl: '/EmployeeInformation/ViewAllEmployeeInformationForSpecificBusRoute'
     })
     .when('/ViewBusInformationForSpecificBusRegistrationNo', {
         templateUrl: '/BusInformation/ViewBusInformationForSpecificBusRegistrationNo'
     })
     .when('/AddSupplier', {
         templateUrl: '/Supplier/AddSupplier'
     })
    .when('/ShowAllSupplier', {
        templateUrl: '/Supplier/ShowAllSupplier'
    })
    .when('/AddPartsInfo', {
        templateUrl: '/PartsInformation/AddPartsInfo'
    })
     .when('/ShowAllPartsInfo', {
         templateUrl: '/PartsInformation/ShowAllPartsInfo'
     })
    .when('/AddPriceForPartsFromSupplier', {
        templateUrl: '/PriceForPartsFromSupplier/AddPriceForPartsFromSupplier'
    })
     .when('/ShowBuyPartsHistoryForSpecificDateFromSupplier', {
         templateUrl: '/BuyPartsHistoryForSpecificDateFromSupplier/ShowBuyPartsHistoryForSpecificDateFromSupplier'
     })
    .when('/AddPartsInStore', {
        templateUrl: '/PartsAddInStore/AddPartsInStore'
    })
    .when('/AddStore', {
        templateUrl: '/Store/AddStore'
    })
     .when('/ViewTotalCostHistoryFromDateToDateForABusRegistrationNo', {
         templateUrl: '/TotalCostHistoryFromDateToDateForABusRegistrationNo/ViewTotalCostHistoryFromDateToDateForABusRegistrationNo'
     })
     .when('/ViewBusInfornationListForSpecificBusRoute', {
         templateUrl: '/BusInfornationListForSpecificBusRoute/ViewBusInfornationListForSpecificBusRoute'
     })
     .when('/AddPartsInBusRegistrationNoFromStore', {
         templateUrl: '/AddPartsInBusRegistrationNoFromStore/AddPartsInBusRegistrationNoFromStore'
     })
     .when('/ViewPartsStatusInStore', {
         templateUrl: '/PartsStatusInStore/ViewPartsStatusInStore'
     });
    
    
    $locationProvider.hashPrefix('');
});