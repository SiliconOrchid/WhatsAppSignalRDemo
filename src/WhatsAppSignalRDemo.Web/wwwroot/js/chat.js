"use strict";

const serviceEndpoint = 'http://localhost:7071/api/';   // include trailing slash
//const serviceEndpoint = 'https://{your function app hostname}.azurewebsites.net/api/';

const signalRBroadcastApiMethod = "BroadcastSignalRMessage"; // do not include any slashes
const signalRTargetGroupName = "AllUsers";

var signalRconnection = new signalR.HubConnectionBuilder()
    .withUrl(serviceEndpoint)
    .build();


//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;


// listener handler for receiving messages broadcast from SignalR Service
signalRconnection.on(signalRTargetGroupName, function (broadcastMessage) {
    console.log(broadcastMessage);

    var encodedMsg = "<b>" + broadcastMessage.User + "</b> : &quot;" + broadcastMessage.Message + "&quot;";

    var li = document.createElement("li");
    li.innerHTML   = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});


// startup handler for establishing connection to SignalR service
signalRconnection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("connectingMessage").style.display = "none";
}).catch(function (err) {
    return console.error(err.toString());
});


signalRconnection.onclose(() => console.log('disconnected from SignalR service'));


// event handler for taking content from page and sending it to SignalR Broadcast http function
document.getElementById("sendButton").addEventListener("click", function (event) {

    var userMessage = new Object();
        userMessage.User = document.getElementById("userInput").value;
        userMessage.Message = document.getElementById("messageInput").value;
    
    postMessage(serviceEndpoint + signalRBroadcastApiMethod, userMessage);

    event.preventDefault();
});


function postMessage(url = ``, data = {}) {
    // Default options are marked with *
    return fetch(url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, cors, *same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        redirect: "follow", // manual, *follow, error
        referrer: "no-referrer", // no-referrer, *client
        body: JSON.stringify(data), // body data type must match "Content-Type" header
    })
        .then(response => response.json()); // parses JSON response into native Javascript objects 
}