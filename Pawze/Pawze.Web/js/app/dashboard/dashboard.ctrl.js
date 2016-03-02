angular.module('app').controller('DashboardController', function ($scope, BoxResource, BoxItemResource, InventoryResource, SubscriptionResource, apiUrl, $http) {

    function activate() {
        $http.get(apiUrl + '/boxes/user')
             .then(function (response) {
                 $scope.box = response.data;
             })
             .catch(function (err) {
                 alert('Failed to get the box');
             });
    }

    activate();

    $scope.inventories = InventoryResource.query();

    $scope.selectInventory = function (inventory, boxItem) {
        boxItem.InventoryId = inventory.InventoryId;
        boxItem.Inventory = inventory;
    };

    $scope.save = function () {
        if ($scope.box.BoxId === 0) {
            BoxResource.save($scope.box, function (newBox) {
                alert('Saved box, box id is now ' + newBox.BoxId);
            });
        } else {
            $http.put(apiUrl + '/boxes/' + $scope.box.BoxId, $scope.box)
                 .then(function () {
                     alert('Updated box successfully');
                 })
                 .catch(function (err) {
                     alert('Couldn\'t update the box');
                 });
        }
    };

    //$scope.subsave = function () {
    //    if (SubscriptionResource.query().length < 1) {
    //        SubscriptionResource.save($scope.sub, function (data) {
    //            $scope.box.SubscriptionId = data.SubscriptionId;
    //        })
    //    }
    //    else {
    //        $scope.box.SubscriptionId = SubscriptionResource.query().SubscriptionId; //change to .first?
    //    }
    //}

    //$scope.save = function () {

    //    SubscriptionResource.save($scope.sub, function (data) {
    //        $scope.box.SubscriptionId = data.SubscriptionId;
    //    });


    //    BoxResource.save($scope.box, function (data) {
    //        // putting boxitems 
    //        $scope.boxItem1.BoxItemPrice = 7;
    //        $scope.boxItem1.BoxId = data.BoxId;
    //        BoxItemResource.save($scope.boxItem1)
    //        $scope.boxItem2.BoxItemPrice = 7;
    //        $scope.boxItem2.BoxId = data.BoxId;
    //        BoxItemResource.save($scope.boxItem2)
    //        $scope.boxItem3.BoxItemPrice = 7;
    //        $scope.boxItem3.BoxId = data.BoxId;
    //        BoxItemResource.save($scope.boxItem3)
    //        $scope.boxItem4.BoxItemPrice = 7;
    //        $scope.boxItem4.BoxId = data.BoxId;
    //        BoxItemResource.save($scope.boxItem4)
    //    });
    //};
});