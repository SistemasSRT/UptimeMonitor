(function () {
    'use strict';

    angular.module('app', [
        // Angular modules

        // Custom modules

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

})();
