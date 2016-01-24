define([], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'components/shared/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/shared/welcomeController']
            }
        }
    };
});