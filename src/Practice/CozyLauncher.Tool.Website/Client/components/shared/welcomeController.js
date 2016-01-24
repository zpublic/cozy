define(['app'],function(app){
    'use strict';
    app.registerController('WelcomeController',WelcomeController);
    WelcomeController.$inject = ['$scope'];
    function WelcomeController($scope){
        $scope.test = "welcome！！！";
        console.log('waht?');
    }
});