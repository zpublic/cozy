define([], function () {
    return {
        defaultRoute: '/cozy',
        routes: {
            '/cozy': {
                templateUrl: 'Component/Chat/index.html',
                controller: 'ChatController',
                dependencies: ['Component/Chat/chatController']
            }
        }
    };
});