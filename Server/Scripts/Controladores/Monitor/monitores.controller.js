(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitores', monitores);

    monitores.$inject = ['$scope', '$uibModal', 'monitor', 'monitorlog'];

    function monitores($scope, $uibModal, monitor, monitorlog) {

        $scope.title = 'monitores';
        $scope.lista = [];
        monitor.listarAsync().then(_refrescar, _error);

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

        function _refrescar(response)
        {
            $scope.lista = response.data;
        }

        function _error()
        {
            console.log('error');
        }

        function AbrirModal(resultado, monitor) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Modales/MonitorLog',
                controller: 'monitorlog',
                resolve: {
                    logs: function () {
                        return resultado;
                    },
                    monitor: function () {
                        return monitor;
                    },
                    monitorlog: function () {
                        return monitorlog;
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
            });
        }

        function _verLogs(monitor) {
            monitorlog.obtenerMonitorLogPorIDAsync(monitor.Id).then(function (response) {
                AbrirModal(response.data, monitor);
            }, function () { alert('No se pudo obtener los datos') })
        }

        activate();

        function activate() {
            $scope.iniciarAsync = _iniciarAsync;
            $scope.detenerAsync = _detenerAsync;
            $scope.limpiarAsync = _limpiarAsync;
            $scope.verLogs = _verLogs;
            $scope.eliminarAsync = _eliminarAsync;
        }
    }
})();
