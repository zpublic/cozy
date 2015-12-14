requirejs.config({
    paths: {
        'angular': 'bower_components/angular/angular.min',
        'angular-route': 'bower_components/angular-route/angular-route.min',
        'angular-resource': 'bower_components/angular-resource/angular-resource.min',
        'jquery': 'bower_components/jquery/dist/jquery.min',
    },
    shim: {
        'angular': { exports: 'angular' },
        'angular-route': { deps: ['angular'] },
        'angular-resource': { deps: ['angular'] }
    }
});

require(['app'], function (app) {
    angular.bootstrap(document, ['app']);
});