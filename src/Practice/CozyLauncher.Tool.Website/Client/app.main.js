requirejs.config({
    paths:{
        'angular':'bower_components/angular/bower-angular-1.4.9/angular.min',
        'angular-route': 'bower_components/angular-route/angular-route.min'
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