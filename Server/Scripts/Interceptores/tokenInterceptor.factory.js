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
                return response;
            },
            responseError: function (response) {
                if (response.status == 401) {
                    window.location.href = 'http://localhost:55749/Login?client_id=9cae8e3f-1961-41f4-92e9-44b9c283474b&redirect_uri=http://localhost:51388/Token';
                }
                return response;
            }
        };

        return service;

    }
})();