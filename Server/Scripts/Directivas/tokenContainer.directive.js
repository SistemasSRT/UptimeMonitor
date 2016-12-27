(function() {
    'use strict';

    angular
        .module('app')
        .directive('tokenContainer', tokenContainer);

    tokenContainer.$inject = ['authorization'];
    
    function tokenContainer(authorization) {
        // Usage:
        //     <token-container></token-container>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            authorization.setToken($("#Token").val());
            $("#Token").remove();
        }
    }

})();