(function () {
    'use strict';

    angular
        .module('app')
        .service('monitorlog', monitorlog);

    monitorlog.$inject = ['$http'];

    function monitorlog($http) {
        this.obtenerMonitorLogPorIDAsync = _obtenerMonitorLogPorIDAsync;

        function _obtenerMonitorLogPorIDAsync(id, pagina, cantidad) {
            return $http.get('/api/monitorlogs/monitor/' + id + '/' + (pagina || '0') + '/' + (cantidad || '10'));
        }
    }
})();