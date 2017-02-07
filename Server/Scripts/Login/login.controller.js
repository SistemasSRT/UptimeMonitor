(function () {
    'use strict';

    angular
        .module('login')
        .controller('login', login);

    login.$inject = ['$scope', '$window', 'authorization', 'login', 'session', 'profile', 'client_id', 'urlBaseAuth']; 

    function login($scope, $window, authorization, login, session, profile, client_id, urlBaseAuth) {
        $scope.title = 'login';               

        activate();

        function activate() {            
            $scope.loginUsuario = _login;
            $scope.loginInvitado = _anonymous;
            $scope.logout = _logout;
            $scope.esUsuarioAnonimo = _esUsuarioAnonimo;
            $scope.cargarDatosUsuario = _cargarDatosUsuario;
            $scope.getDatosUsuario = _getDatosUsuario;
        }

        function _login() {
            var server = $window.location.protocol + "//" + $window.location.host;

            authorization.clearToken();
            
            $window.location.href = urlBaseAuth + "/OAuth/Authorize?client_id=" + client_id + "&redirect_uri=" + server + "/Token.html&response_type=token&state=usuario";
        }        

        function _anonymous() {
            authorization.clearToken();

            session.clearData('usuario');

            var token = login.getToken();

            authorization.setToken(token);

            session.setData('usuario', { anonimo: true });

            $window.location.href = '/#/monitores';
        }

        function _logout() {            
            login.logoutAsync().then(function () {              
                _anonymous();
            });
        }

        function _cargarDatosUsuario() {

            session.clearData('usuario');

            profile.getProfileAsync().then(function (response) {
                session.setData('usuario', { anonimo: false, profile: response.data });
                $window.location.href = '/#/monitores';
            }, function (response) {
                console.log(response);
            });
        }

        function _esUsuarioAnonimo() {
            var user = session.getData('usuario');

            return (user == null || user.anonimo);
        }

        function _getDatosUsuario() {

            var user = session.getData('usuario');

            return user.profile;
        }
    }
})();
