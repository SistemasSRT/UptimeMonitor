(function () {
    'use strict';

    angular
        .module('app')
        .service('monitor', monitor);

    monitor.$inject = ['$http'];

    function monitor($http) {
        this.guardarAsync = _guardarAsync; 
        
        function _guardarAsync(dto) {
            return $http.post('/api/Monitor', dto);
        }
    }
})();