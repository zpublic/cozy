define(['app'], function (app) {
    'use strict';

    app.registerController('ChatController', ChatController);

    ChatController.$inject = ['$scope'];

    function ChatController($scope) {

        $scope.title = "Cozy聊天室";
        $scope.sendContent = '';
        $scope.recMsgList = [];
        $scope.connection = {};

        $scope.sendMsg = sendMsg;

        initChat();

        function initChat() {
            $scope.connection = $.connection('/xchat');
            $scope.connection.received(receivedMsg);
            $scope.connection.error(onError);
            $scope.connection.stateChanged(onStateChanged);
            $scope.connection.reconnected(onReconnected);
            $scope.connection.start().done(onStart);
        }

        function onError() {
            console.warn(error);
        }

        function onStateChanged(change) {
            if (change.newState === $.signalR.connectionState.reconnecting) {
                console.log('Re-connecting');
            }
            else if (change.newState === $.signalR.connectionState.connected) {
                console.log('The server is online');
            }
        }

        function onReconnected() {
            console.log('Reconnected');
        }

        function onStart() {
            console.log("connection started!");
            $scope.connection.send("Hello World");
        }

        function receivedMsg(msg) {
            msg = new Date().toLocaleString() + '--' + msg;
            $scope.recMsgList.push(msg);
            $scope.$apply();
        }

        function sendMsg() {
            $scope.connection.send($scope.sendContent);
            $scope.sendContent = "";
        }
    }
});