angular.module('app').factory('BoxItemResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/boxitem/:boxItemId', { boxItemId: '@BoxItemId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});