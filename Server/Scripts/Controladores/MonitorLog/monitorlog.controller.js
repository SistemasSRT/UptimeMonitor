(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitorlog', monitorlog);

    monitorlog.$inject = ['$scope', '$uibModalInstance', 'monitor', 'monitorlog'];

    function monitorlog($scope, $uibModalInstance, monitor, monitorlog) {

        function _ok() {
            $uibModalInstance.close();
        }

        function _cancel() {
            $uibModalInstance.dismiss();
        }

        function _traerLogsAsync(direccion) {

            $scope.habilitarPedidoLog = false;
            $scope.pagina = $scope.pagina + direccion;

            return monitorlog.obtenerMonitorLogPorIDAsync(monitor.Id, $scope.pagina, $scope.cantidad).then(function (response) {
                $scope.logs = response.data;
                $scope.habilitarPedidoLog = true;
            });
        }

        function _traerLogsSiguientes() {
            _traerLogsAsync(1);
        }

        function _traerLogsAnteriores() {
            _traerLogsAsync(-1);
        }

        function activate() {
            $scope.habilitarPedidoLog = true;
            $scope.logs = []                        
            $scope.monitor = monitor;
            $scope.ok = _ok;
            $scope.cancel = _cancel;
            $scope.pagina = 0;
            $scope.cantidad = 10;
            $scope.traerLogsSiguientes = _traerLogsSiguientes;
            $scope.traerLogsAnteriores = _traerLogsAnteriores;
        }

        activate();

        _traerLogsAsync(0);
    }
})();
