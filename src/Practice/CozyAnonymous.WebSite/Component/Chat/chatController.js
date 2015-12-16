define(['app',
        'Component/Chat/chatFactory'
], function (app) {
    'use strict';

    app.registerController('ChatController', ChatController);

    ChatController.$inject = ['$scope', 'ChatFactory'];

    function ChatController($scope, chatFactory) {

        $scope.title = "hello,blog";
        $scope.content1 = "cozy";
        $scope.content2 = "hehe";

        var connection = $.connection('/xchat');
        connection.received(function (data) {
            var _this = this;
            _this.content2 = data;
        });
        connection.error(function (error) {
            console.warn(error);
        });
        connection.stateChanged(function (change) {
            if (change.newState === $.signalR.connectionState.reconnecting) {
                console.log('Re-connecting');
            }
            else if (change.newState === $.signalR.connectionState.connected) {
                console.log('The server is online');
            }
        });
        connection.reconnected(function () {
            console.log('Reconnected');
        });
        connection.start().done(function () {
            console.log("connection started!");
            connection.send("Hello World");
        });
    }
});