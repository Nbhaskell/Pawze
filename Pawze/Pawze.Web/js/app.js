angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:55442/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', templateUrl: '/templates/home/register/register.html', controller: 'RegisterController' })
        .state('login', { url: '/login', templateUrl: '/templates/home/login/login.html', controller: 'LoginController' })

        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })

            .state('app.accountsettings', { url: '/accountsettings', templateUrl: '/templates/app/accountsettings/accountsettings.html', controller: 'AccountSettingsController' })

            .state('app.checkout', { url: '/checkout', templateUrl: '/templates/app/checkout/checkout.html', controller: 'CheckoutController' });

          //  .state('app.itemselection', { url: '/itemselection', templateUrl: '/templates/app/itemselection/itemselection.html', controller: 'ItemSelectionController' });
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});