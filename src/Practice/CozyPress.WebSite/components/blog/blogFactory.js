define(['app'], function (app) {
    'use strict';

    app.registerFactory('BlogFactory', BlogFactory);

    BlogFactory.$inject = ['$resource'];

    function BlogFactory($resource) {

        return $resource('', {}, {
            test: {
                url: 'http://localhost:12306/',
                method: 'GET'
            }
        });
    }
});