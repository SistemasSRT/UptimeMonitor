(function () {
    'use strict';

    angular
        .module('app')
        .controller('aplicacion', aplicacion);

    aplicacion.$inject = ['$scope']; 

    function aplicacion($scope) {

        activate();

        function activate() { }
    }
})();
