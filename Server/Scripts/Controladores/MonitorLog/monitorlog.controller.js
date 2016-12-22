(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitorlog', monitorlog);

    monitorlog.$inject = ['$scope', '$uibModalInstance', 'logs', 'monitor', 'monitorlog'];

    function monitorlog($scope, $uibModalInstance, logs, monitor, monitorlog) {                
        var cantidad = 10;        

        function _ok() {
            $uibModalInstance.close();
        }

        function _cancel() {
            $uibModalInstance.dismiss();
        }

        function _traerLogs(direccion) {
            $scope.pagina = $scope.pagina + direccion;

            monitorlog.obtenerMonitorLogPorIDAsync(monitor.Id, $scope.pagina, cantidad).then(function (response) {
                $scope.logs = response.data;
            });
        }

        function _traerLogsSiguientes() {
            _traerLogs(1);
        }

        function _traerLogsAnteriores() {
            _traerLogs(-1);
        }

        function activate() {
            $scope.logs = logs;
            $scope.monitor = monitor;
            $scope.ok = _ok;
            $scope.cancel = _cancel;
            $scope.pagina = 0;
            $scope.traerLogsSiguientes = _traerLogsSiguientes;
            $scope.traerLogsAnteriores = _traerLogsAnteriores;
        }        

        activate();
    }
})();
