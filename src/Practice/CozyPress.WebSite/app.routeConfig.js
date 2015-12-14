define([], function () {
    return {
        defaultRoute: '/blog',
        routes: {
            '/welcome': {
                templateUrl: 'components/test/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/test/welcomeController']
            },
            '/blog': {
                templateUrl: 'components/blog/index.html',
                controller: 'BlogController',
                dependencies: ['components/blog/blogController']
            }
        }
    };
});