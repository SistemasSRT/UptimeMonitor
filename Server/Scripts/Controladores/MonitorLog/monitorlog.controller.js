(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitorlog', monitorlog);

    monitorlog.$inject = ['$scope', '$uibModalInstance', 'logs', 'monitor', 'monitorlog'];

    function monitorlog($scope, $uibModalInstance, logs, monitor, monitorlog) {
        console.log(monitorlog);
        $scope.logs = logs;
        $scope.monitor = monitor;

        function _ok() {
            $uibModalInstance.close();
        }

        function _cancel() {
            $uibModalInstance.dismiss();
        }

        function activate() {
            $scope.ok = _ok;
            $scope.cancel = _cancel;
        }

        activate();
    }
})();
