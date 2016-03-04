angular.module('app').controller('AccountSettingsController', function ($scope, $http, apiUrl, SubscriptionResource) {

    function activate() {
        $http.get(apiUrl + '/pawzeuser/user')
               .then(function (response) {
                   $scope.user = response.data;
               })
               .catch(function (err) {
                   //bootbox.alert('Failed to get the user');
               });
        $scope.subscription = SubscriptionResource.query();
    }

    activate();

    $scope.save = function () {
        $http.put(apiUrl + '/pawzeuser/user', $scope.user)
                 .then(function () {
                     bootbox.alert("User Information Has Been Updated")
                 })
                 .catch(function (err) {
                     bootbox.alert('Couldn\'t update your settings.');
                 });
    };

    $scope.cancel = function () {
        // TODO: FIX THIS.
        $scope.subscription = SubscriptionResource.query();

        if ($scope.subscription != null) {
            bootbox.confirm("Are you sure you want to cancel?", function (result) {
                if (result) {
                    $http.post(apiUrl + '/subscriptions/cancel', {
                        stripeToken: $scope.subscription.StripeSubscriptionId
                    }).success(function () {
                        bootbox.alert('Subscription Cancelled.'); //delete
                    }).error(function () {
                        bootbox.alert('Could not cancel subscription, Sorry bout it.'); //delete
                    });
                }
            });
        }
        else
        {
            bootbox.alert("You don't have an active subscription.");
        }
        
        
    };

    

});