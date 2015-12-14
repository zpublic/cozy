define(['app',
        'components/blog/blogFactory'
], function (app) {
    'use strict';

    app.registerController('BlogController', BlogController);

    BlogController.$inject = ['$scope', 'BlogFactory'];

    function BlogController($scope, blogFactory) {

        $scope.title = "hello,blog";
        $scope.content1 = "cozy";
        $scope.content2 = "hehe";

        loadData();

        function loadData() {
            blogFactory.test(function (data) {
                $scope.title = data;
            });
        };
    }
});