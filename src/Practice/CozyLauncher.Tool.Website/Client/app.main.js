requirejs.config({
    paths:{
        'angular':'bower_components/angular/angular.min',
        'angular-route': 'bower_components/angular-route/angular-route.min',
        'bootstrap':'bower_components/bootstrap/dist/js/bootstrap.min'
    },
    shim:{
        'angular': { exports: 'angular' },
        'angular-route': { deps: ['angular'] },
    }
});
require(['app'],function(app){
    angular.bootstrap(document, ['app']);
    console.log('hello,require');
});