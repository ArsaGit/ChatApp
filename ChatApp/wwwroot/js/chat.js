let currentRoomId, currentRoomName = 'None';

function createRoom(userId) {
    var roomName = prompt("Enter room name:");
    let data = { Name: roomName, UserId: String(userId) }

    alert(userId);
    if (roomName) {
        $.ajax({
            url: '/Api/Chatroom/AddRoom',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function () {
                alert('Room created successfully!');
                
                let li = document.createElement("li");
                document.getElementById("chatRoomList").appendChild(li);
                li.textContent = roomName;
            }
        });
    }
}

function inviteUser() {
    var username = prompt("Enter username:");
    if (username) {
        $.ajax({
            url: '/Api/Chatroom/InviteUser',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ RoomId: currentRoomId, UserName: username }),
            success: function () {
                alert('User invited successfully!');
            }
        });
        
        // sendInvite(username, currentRoomName);
        hubConnection.invoke("SendInvite", username, currentRoomName)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }
}

let messageForm = document.getElementById("send-message-form");

messageForm.addEventListener("submit", (e) => {
    e.preventDefault();

    let data = {
        SenderId: document.getElementById("user-id").value,
        Text: document.getElementById("message-input").value,
        DateCreated: new Date().toJSON(),
        ChatRoomId: currentRoomId,
        SenderName: document.getElementById("user-name").value
    }

    $.ajax({
        url: '/Api/Message',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function () {
            alert('Msg++++!');
            // var listItem = $('<li>').addClass('list-group-item')
            //     .text(data.SenderName + ': ' + data.Text);
            // $('#message-list').append(listItem);
            // let parentNode = $('#message-list').parentNode;
            // let firstChild = $('#message-list').firstChild;
            // parentNode.insertBefore(listItem, firstChild);
            // sendInvite(username, currentRoomName);
            hubConnection.invoke("SendMessage", currentRoomName, data)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
    });
});

function handleItemClick(roomId, roomName) {
    if (currentRoomName !== 'None') {
        hubConnection.invoke("LeaveRoom", currentRoomName);
    }
    
    currentRoomId = roomId;
    currentRoomName = roomName;
    document.getElementById("chosen-chat-room").innerText = "Chat Room(" + currentRoomName + "):";
    // alert("Selected " + roomName);
    loadMessages();
    
    hubConnection.invoke("JoinRoom", currentRoomName);
}

function loadMessages() {
    $('#message-list').empty();

    $.ajax({
        url: '/Api/Messages',
        type: 'GET',
        data: { roomId: currentRoomId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var message = data[i];
                var listItem = $('<li>').addClass('list-group-item')
                    .text(message.senderName + ': ' + message.text);
                $('#message-list').append(listItem);
            }
        }
    });
}