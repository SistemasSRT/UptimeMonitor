(function () {
    'use strict';

    angular.module('constantes', [
        // Angular modules
        
        // Custom modules

        // 3rd Party Modules
        
    ]);
	
	angular.module('constantes')
		.constant('client_id', '9cae8e3f-1961-41f4-92e9-44b9c283474b')
        .constant('urlBaseAuth', (window.location.hostname == "digesto.srt.gob.ar") ? "https://auth.srt.gob.ar" : "https://desarrolloauth.srt.gob.ar")
        .constant('urlBase', (window.location.hostname == "digesto.srt.gob.ar") ? "https://api.srt.gob.ar" : "https://desarrolloapi.srt.gob.ar");
})();
