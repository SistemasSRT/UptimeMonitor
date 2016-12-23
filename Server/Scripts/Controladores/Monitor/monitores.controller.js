(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitores', monitores);

    monitores.$inject = ['$scope', '$uibModal', 'monitor', 'monitorlog'];

    function monitores($scope, $uibModal, monitor, monitorlog) {              
        function doAction(OID, action) {
            monitor[action + 'Async'](OID).then(_refrescar, _error);
        }

        function _iniciarAsync(OID) {
            doAction(OID, 'iniciar');
        }

        function _detenerAsync(OID) {
            doAction(OID, 'detener');
        }

        function _limpiarAsync(OID) {
            if (confirm('¿Desea eliminar los logs del monitor?'))
                monitor.limpiarAsync(OID);
        }

        function _eliminarAsync(OID) {
            if (confirm('¿Desea eliminar el monitor?'))
                doAction(OID, 'eliminar');
        }

        function _refrescar(response) {
            $scope.lista = response.data;
        }

        function _error() {
            console.log('error');
        }

        function _abrirModal(monitor) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Modales/MonitorLog',
                controller: 'monitorlog',
                resolve: {
                    monitor: function () {
                        return monitor;
                    },
                    monitorlog: function () {
                        return monitorlog;
                    }
                }
            });
        }

        function _verLogs(monitor) {
            _abrirModal(monitor);
        }

        activate();
        monitor.listarAsync().then(_refrescar, _error);

        function activate() {
            $scope.title = 'monitores';
            $scope.lista = [];
            $scope.iniciarAsync = _iniciarAsync;
            $scope.detenerAsync = _detenerAsync;
            $scope.limpiarAsync = _limpiarAsync;
            $scope.verLogs = _verLogs;
            $scope.eliminarAsync = _eliminarAsync;
        }
    }
})();
