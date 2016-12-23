(function () {
    'use strict';

    angular
        .module('app')
        .service('monitor', monitor);

    monitor.$inject = ['$http'];

    function monitor($http) {
        this.guardarAsync = _guardarAsync;
        this.obtenerAsync = _obtenerAsync;
        this.listarAsync = _listarAsync;
        this.detenerAsync = _detenerAsync;
        this.iniciarAsync = _iniciarAsync;
        this.limpiarAsync = _limpiarAsync;
        this.eliminarAsync = _eliminarAsync;

        function _guardarAsync(dto) {
            return $http.post('/api/Monitor', dto);
        }

        function _obtenerAsync(OID) {
            return $http.get('/api/Monitor/' + OID);
        }

        function _listarAsync() {
            return $http.get('/api/Monitor');
        }

        function _detenerAsync(OID) {
            return $http.post('/api/Monitor/' + OID + '/detener');
        }

        function _limpiarAsync(OID) {
            return $http.post('/api/Monitor/' + OID + '/limpiar');
        }

        function _eliminarAsync(OID) {
            return $http.delete('/api/Monitor/' + OID);
        }

        function _iniciarAsync(OID) {
            return $http.post('/api/Monitor/' + OID + '/iniciar');
        }
    }
})();