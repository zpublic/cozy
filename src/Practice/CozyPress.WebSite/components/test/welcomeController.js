define(['app',
        'components/test/welcomeFactory'
], function (app) {
    'use strict';

    app.registerController('WelcomeController', WelcomeController);

    WelcomeController.$inject = ['$scope', 'WelcomeFactory'];

    function WelcomeController($scope, welcomeFactory) {

        $scope.test = "hello,cozy";

        loadData();

        function loadData() {
            welcomeFactory.test(function (data) {
                $scope.test = data;
            });
        };

    }
});