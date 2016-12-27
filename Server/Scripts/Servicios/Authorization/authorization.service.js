(function () {
    'use strict';

    angular
        .module('authorization')
        .service('authorization', authorization);

    authorization.$inject = [];
    
    function authorization() {
        var _token;
        this.getToken = _getToken;
        this.setToken = _setToken;

        function _getToken() {
            return _token;
        }
        function _setToken(token) {
            _token = token;
        }
    }
})();