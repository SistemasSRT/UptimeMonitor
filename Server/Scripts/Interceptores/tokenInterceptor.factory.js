(function () {
    'use strict';

    angular
        .module('authorization')
        .factory('tokenInterceptor', tokenInterceptor);

    tokenInterceptor.$inject = ['$window','authorization'];

    function tokenInterceptor($window, authorization) {
        var service = {
            request: function (request) {
                request.headers.Authorization = authorization.getToken();
                return request;
            },
            response: function (response) {
                return response;
            },
            responseError: function (response) {
                if (response.status == 401) {
                    $window.location.href = '/Static/Login.html';
                }
                return response;
            }
        };

        return service;
    }
})();