define(['app'], function (app) {
    'use strict';

    app.registerFactory('WelcomeFactory', WelcomeFactory);

    WelcomeFactory.$inject = ['$resource'];

    function WelcomeFactory($resource) {

        return $resource('', {}, {
            test: {
                url: 'http://localhost:12306/',
                method: 'GET'
            }
        });
    }
});