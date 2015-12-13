define([], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'components/test/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/test/welcomeController']
            }
        }
    };
});