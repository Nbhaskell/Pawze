angular.module('app').controller('AppController', function ($scope, localStorageService, apiUrl, $http) {
    

    function activate() {
        $http.get(apiUrl + '/pawzeuser/user')
          .then(function (response) {
              $scope.user = response.data;
          })
          .catch(function (err) {
              alert('Failed to get the user');
          });
    };

    activate();

    $scope.logout = function () {
        AuthenticationService.logout();
        location.replace('#/home');
    };
});