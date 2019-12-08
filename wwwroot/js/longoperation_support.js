﻿"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/longOperationHub")
    .build();
//alert(connection);
connection.on("ReceiveMessage",
    (user, message) => {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = `${user} says ${msg}`;
        alert(encodedMsg);
    });

connection.on("ReportProgress",
    (message, percentage) => {
        ProgressBarModal("show", message + ":" + percentage + "%");
        $('#ProgressMessage').width(percentage + "%");
    });

connection.on("ReportFinish",
    (message, percentage) => {
        ProgressBarModal();
        location.href = "Index";
    });

function StartProcess(parameter) {
    //alert("Start");

    connection.start()
        .then(function() {
            console.log('Now connected, connection ID=' + connection.id);
            connection.invoke('start', parameter);
        })
        .catch(error =>
            console.error('Could not connect', error)
        );
}