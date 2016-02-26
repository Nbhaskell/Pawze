angular.module('app').controller('AppController', function ($scope, localStorageService) {
    $scope.user = localStorageService.get('user');
});