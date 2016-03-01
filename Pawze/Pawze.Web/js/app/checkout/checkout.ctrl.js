angular.module('app').controller('CheckoutController', function ($scope, localStorageService, apiUrl, $http) { //PawzeUserResource,
    ////  $scope.username = localStorageService.get('user');

    //  //  $scope.user = PawzeUserResource.get({ name: $scope.username })

    $scope.email = "cd@gmail.com"; //get user email

    var handler = StripeCheckout.configure({
        key: 'pk_test_fhxY8eOWWpOb6dE1K7rBCgik',
        locale: 'auto',
        allowRememberMe: false,
        email: $scope.email,
        token: function (token) {
            console.log(token); //remove
            $http.post(apiUrl + '/subscriptions/create', {
                stripeToken: token.id,
                boxId: 1 //TODO: This is where you'll need the box id to create a subscription for.
            }).success(function () {
                alert('charged ya'); //delete
            }).error(function () {
                alert('could not charge you'); //delete
            });
        }
    });

    $('#customButton').on('click', function (e) {
        // Open Checkout with further options
        handler.open({
            description: 'Monthly Charge - 1 Box',
            amount: 3000,
        });
        e.preventDefault();
    });

    // Close Checkout on page navigation
    $(window).on('popstate', function () {
        handler.close();
    });


    //  $scope.saveUser = function () {
    //      $scope.user.$update();
    //  };
});