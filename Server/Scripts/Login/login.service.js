(function () {
    'use strict';

    angular
        .module('login')
        .service('login', login);

    login.$inject = ['$http', 'urlBase', 'urlBaseAuth', 'client_id'];

    function login($http, urlBase, urlBaseAuth, client_id) {
        this.getToken = _getToken;        
        this.logoutAsync = _logoutAsync;

        function _getToken() {
            var token = {};

            var settings = {
                "async": false,
                "crossDomain": true,
                "url": urlBase + "/auth/digesto.aspx?client_id=" + client_id,
                "method": "GET"
            };

            $.ajax(settings).done(function (response) {
                token = response;
            }).error(function () {
                token = null;
            });

            return token;
        }

        function _logoutAsync() {
            return $http.get(urlBaseAuth + '/account/logout');
        }
    }
})();