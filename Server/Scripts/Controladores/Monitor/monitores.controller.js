(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitores', monitores);

    monitores.$inject = ['$scope', '$uibModal', 'monitor', 'monitorlog'];

    function monitores($scope, $uibModal, monitor, monitorlog) {

        $scope.title = 'monitores';
        $scope.lista = [];
        monitor.listarAsync().then(function (response) {
            $scope.lista = response.data;

        }, function () {
            console.log('error');
        });

        function _iniciarAsync(OID) {
            monitor.iniciarAsync(OID);

        }

        function _detenerAsync(OID) {
            monitor.detenerAsync(OID);
        }

        function _limpiarAsync(OID) {
            monitor.limpiarAsync(OID);
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
                    }
                }
            });

            modalInstance.result.then(function () {
                alert("OK");
            }, function () {
                alert("CANECL");
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
        }

        $scope
    }
})();
