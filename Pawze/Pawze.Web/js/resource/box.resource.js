angular.module('app').factory('BoxResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/box/:boxId', { boxId: '@BoxId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});