(function () {
    'use strict';

    angular
        .module('authorization')
        .controller('authorization', authorization);

    authorization.$inject = ['$scope', '$window', 'authorization'];

    function authorization($scope, $window, authorization) {
        $scope.title = 'authorization';

        activate();

        function _getFragment() {
            if ($window.location.hash.indexOf("#") === 0) {
                return _parseQueryString($window.location.hash.substr(1));
            } else {
                return {};
            }
        }

        function _parseQueryString(queryString) {
            var data = {},
                pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

            if (queryString === null) {
                return data;
            }

            pairs = queryString.split("&");

            for (var i = 0; i < pairs.length; i++) {
                pair = pairs[i];
                separatorIndex = pair.indexOf("=");

                if (separatorIndex === -1) {
                    escapedKey = pair;
                    escapedValue = null;
                } else {
                    escapedKey = pair.substr(0, separatorIndex);
                    escapedValue = pair.substr(separatorIndex + 1);
                }

                key = decodeURIComponent(escapedKey);
                value = decodeURIComponent(escapedValue);

                data[key] = value;
            }

            return data;
        }

        function activate() {
            $scope.login = _login;
            $scope.logout = _logout;
        }

        function _login() {
            var token_data = _getFragment();

            authorization.setToken(token_data);

            $window.location.href = "/";
        }

        function _logout() {
            authorization.clearToken();
        }
    }
})();
