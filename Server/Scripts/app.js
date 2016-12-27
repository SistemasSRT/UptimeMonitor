(function () {
    'use strict';

    angular.module('app', [
        // Angular modules
        'ngRoute',
        // Custom modules
        'authorization',
        // 3rd Party Modules
         'ui.bootstrap',
        'formly',
        'formlyBootstrap',
        'rzModule'
    ]);
    angular.module('app').run(function (formlyConfig) {
        // single slider type
        formlyConfig.setType({
            name: 'slider',
            template: ['<rzslider rz-slider-model="model[options.key]"' +
                       ' rz-slider-options="to.sliderOptions"></rzslider>'].join(' '),
            wrapper: ['bootstrapLabel', 'bootstrapHasError']
        });

        //range slider type
        formlyConfig.setType({
            name: 'range-slider',
            template: ['<rzslider rz-slider-model="model[options.key].low"' +
                       'rz-slider-high="model[options.key].high" ' +
                       'rz-slider-options="to.sliderOptions"></rzslider>'].join(' '),
            wrapper: ['bootstrapLabel', 'bootstrapHasError']
        });
    });

    angular.module('app').config(['$routeProvider', function ($routeProvider) {

        //https://plnkr.co/edit/alfgOPSZHpKs0QTGLDHK

        $routeProvider.when('/monitores', {
            templateUrl: '/aplicacion/monitores', // /Opciones/Ruta2
            controller: 'monitores',
            resolve: {
                monitores: function (monitor) {
                    return monitor.listarAsync();
                }
            },
            activetab: 'monitores'
        }).when('/monitor', {
            templateUrl: '/aplicacion/monitor', // /Opciones/Ruta2
            controller: 'monitor', resolve: {
                monitorDTO: function (monitor) {
                    //return monitor.getModelAsync(); //OJO: devolver promise!!
                    return monitor.obtenerAsync(0);  //OJO: devolver promise!!
                }
            },
            activetab: 'monitores'
        }).when('/monitor/editar/:id', {
            templateUrl: '/Aplicacion/Monitor',
            controller: 'monitor',
            resolve: {
                monitorDTO: function (monitor, $route) {
                    return monitor.obtenerAsync($route.current.params.id);  //OJO: devolver promise!!
                }
            },
            activetab: 'monitores'
        }).otherwise('/monitores');

    }])
    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('tokenInterceptor');
    }]);

})();
