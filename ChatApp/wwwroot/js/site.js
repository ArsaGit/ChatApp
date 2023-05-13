// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.on("ReceiveInvite", function (roomId, roomName) {
    let li = document.createElement("li");
    li.addEventListener("click", handleItemClick);
    
    document.getElementById("chatRoomList").appendChild(li);

    li.textContent = roomName;
});

hubConnection.on("ReceiveMessage", function (senderName, text) {
    let listItem = $('<li>').addClass('list-group-item')
        .text(senderName + ': ' + text);
    $('#message-list').append(listItem);
});

hubConnection.start()
    .then(function () {
        document.getElementById("sendBtn").disabled = false;
    })
    .catch(function (err) {
        return console.error(err.toString());
    });