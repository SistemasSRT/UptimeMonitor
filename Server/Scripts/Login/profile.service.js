(function () {
    'use strict';

    angular
        .module('login')
        .service('profile', profile);

    profile.$inject = ['$http', 'authorization', 'urlBaseAuth', 'client_id'];

    function profile($http, authorization, urlBaseAuth, clientId) {
        this.getProfileAsync = _getProfileAsync;

        function _getProfileAsync() {
            return $http({
                url: urlBaseAuth + '/api/profile?clientID='+ clientId ,
                method: 'GET',                
                headers: {
                    Authorization: authorization.getToken()
                }
            });
        }
    }
})();