"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("SetHistory", function (msgs) {
    for (var i = 0; i < msgs.length; i++) {
        var msg = msgs[i];
        var li = document.createElement("li");
        li.textContent = msg;
        document.getElementById("messagesList").appendChild(li);
    }
});
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    connection.invoke("GetHistory").catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    sessionStorage.setItem("username", user);
    var msgInput = document.getElementById("messageInput");
    var message = msgInput.value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    msgInput.value = '';
    msgInput.focus();
});

//document.getElementById("userInput").value =
//    sessionStorage.getItem("username");