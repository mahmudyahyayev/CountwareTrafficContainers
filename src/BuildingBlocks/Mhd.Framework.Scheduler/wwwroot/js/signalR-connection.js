var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalr")
    .withAutomaticReconnect()
    .build();


var conStatusText = $("#connectionStatus");
var conStatusDiv = $(".connectionStatus");


async function startConnection() {

    conStatusText.text("Connecting...");
    conStatusDiv.removeClass("text-danger");

    await connection.start();

    if (connection.state === signalR.HubConnectionState.Connected) {
        conStatusText.text("Online");
        conStatusDiv.removeClass("text-danger").addClass("text-success");
    }
}

connection.onreconnecting(error => {
    conStatusText.text("Offline");
    conStatusDiv.removeClass("text-success").addClass("text-danger");

    if (connection.state === signalR.HubConnectionState.Connected) {
        conStatusText.text("Online");
        conStatusDiv.removeClass("text-danger").addClass("text-success");
    }
});

$(document).ready(function () {

    startConnection();
});


connection.on("notify", (user, message) => {
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: message
    },
        {
            type: 'danger',
            timer: 4000,
        }
    )
});


