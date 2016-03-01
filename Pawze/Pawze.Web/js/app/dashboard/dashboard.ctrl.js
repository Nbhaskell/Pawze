angular.module('app').controller('DashboardController', function ($scope, InventoryResource) {

    $scope.boxItem1 = [];

    $scope.inventories = InventoryResource.query();

    $scope.inventorySelect = function (inventory) {
        $scope.boxItem1 = inventory.InventoryId;
    }
});




    //$scope.user = {
    //    agree: null
    //};
    //$scope.selected = null;
