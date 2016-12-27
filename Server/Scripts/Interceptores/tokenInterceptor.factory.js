(function () {
    'use strict';

    angular
        .module('authorization')
        .factory('tokenInterceptor', tokenInterceptor);

    tokenInterceptor.$inject = ['authorization'];

    function tokenInterceptor(authorization) {
        var service = {
            request: function (request) {
                request.headers.Authorization = 'Bearer ' + authorization.getToken();                
                return request;
            },
            response: function (response) {
                console.log(response);
                return response;
            }
        };

        return service;

    }
})();