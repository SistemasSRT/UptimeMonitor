(function () {
    'use strict';

    angular
        .module('app')
        .controller('monitor', monitor);

    monitor.$inject = ['$scope', '$location', 'monitor', 'monitorDTO'];

    function monitor($scope, $location, monitor, monitorDTO) {
        function _guardar() {
            monitor.guardarAsync($scope.monitor).then(
                function () { $location.path('/monitores') });
        }

        activate();

        function activate() {
            $scope.monitor = monitorDTO.data || {};
            $scope.title = 'monitor';
            $scope.fields = [
                {
                    "key": "Tipo",
                    "type": "select",
                    "templateOptions": {
                        "label": "Tipo de Monitor",
                        "valueProp": "valor",
                        "labelProp": "name",
                        "required": true,
                        "options": [
                        {
                            "name": "URL",
                            "valor": 1
                        },
                        {
                            "name": "IP",
                            "valor": 2
                        }
                        ]
                    }
                },
                {
                    key: 'Nombre',
                    type: 'input',
                    templateOptions: {
                        type: 'text',
                        required: true,
                        label: 'Nombre',
                        placeholder: 'Nombre Monitor'
                    }
                },
                {
                    key: 'URL', //Condicional al tipo de monitor URL
                    type: 'input',
                    templateOptions: {
                        type: 'url',
                        label: 'URL',
                        placeholder: 'URL Monitor'
                    }
                },
                {
                    key: 'IP',//Condicional al tipo de monitor IP
                    type: 'input',
                    templateOptions: {
                        type: 'text',
                        label: 'IP',
                        placeholder: 'IP'
                    }
                },
                {
                    key: 'Puerto',//Condicional al tipo de monitor IP??? o nuevo tipo
                    type: 'input',
                    templateOptions: {
                        type: 'text',
                        label: 'Puerto',
                        placeholder: 'Puerto'
                    }
                },
                {
                    key: 'Intervalo',
                    type: 'slider',
                    templateOptions: {
                        label: 'Intervalo',
                        sliderOptions: {
                            required: true,
                            floor: 1,
                            ceil: 60
                        }
                    }
                },
                {
                    key: 'Descripcion',
                    type: 'textarea',
                    templateOptions: {
                        type: 'text',
                        label: 'Descripción',
                        placeholder: 'Descripción'
                    }
                },
                {
                    key: 'Autenticacion',
                    type: 'checkbox',
                    templateOptions: {
                        type: 'checkbox',
                        label: 'Requiere autenticación'
                    }
                },
                {
                    key: 'Usuario',
                    type: 'input',
                    templateOptions: {
                        type: 'text',
                        /*required : function() {
                            return false;//$scope.monitor.Autenticacion;
                        },*/
                        label: 'Usuario'
                    },
                    expressionProperties: {
                        "templateOptions.disabled": "!model.Autenticacion",
                        "templateOptions.required": "model.Autenticacion"
                        
                    }
                },
                {
                    key: 'Password',
                    type: 'input',
                    templateOptions: {
                        type: 'password',
                        label: 'Password'
                    },
                    expressionProperties: {
                        "templateOptions.disabled": "!model.Autenticacion",
                        "templateOptions.required": "model.Autenticacion"
                    }
                },
                {
                    key: 'Dominio',
                    type: 'input',
                    templateOptions: {
                        type: 'text',
                        label: 'Dominio'
                    },
                    expressionProperties: {
                        "templateOptions.disabled": "!model.Autenticacion",
                        "templateOptions.required": "model.Autenticacion"

                    }
                }

            ];
            $scope.monitor.Intervalo = 1;
            $scope.monitor.Autenticacion = false;
            $scope.guardar = _guardar;
        }
    }
})();
