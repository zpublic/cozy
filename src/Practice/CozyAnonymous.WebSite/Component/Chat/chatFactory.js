define(['app'], function (app) {
    'use strict';

    app.registerFactory('ChatFactory', ChatFactory);

    ChatFactory.$inject = ['$resource'];

    function ChatFactory($resource) {

        return $resource('', {}, {
            test: {
                url: 'http://news-at.zhihu.com/api/4/version/android/2.3.0/',
                method: 'GET'
            }
        });
    }
});