const connection = new signalR
.HubConnectionBuilder()
.withUrl("/notificationHub")
.withAutomaticReconnect([0, 1000, 5000, null])
.configureLogging(signalR.LogLevel.Debug)
.build();

connection.on("ReceiveLikeNotification", function (user, photo) {
    $("#liveToast .toast-body").html(`<strong>${user}</strong> liked your <a href="/Photo/Details/${photo}">photo</a>`);
    $(".toast").toast("show");
    GetNotifications();
});

connection.on("ReceiveCommentNotification", function (user, photo) {
    $("#liveToast .toast-body").html(`<strong>${user}</strong> commented on your <a href="/Photo/Details/${photo}">photo</a>`);
    $(".toast").toast("show");
    GetNotifications();
});

connection.on("ReceiveFollowNotification", function (user) {
    $("#liveToast .toast-body").html(`<strong>${user}</strong> followed you</a>`);
    $(".toast").toast("show");
    GetNotifications();
});

connection.on("ReceiveNewPostNotification", function (firstName, photoId) {
    $("#liveToast .toast-body").html(`<strong>${firstName}</strong> just uploaded a new <a href="/Photo/Details/${photoId}">photo</a>!`);
    $(".toast").toast("show");
    GetNotifications();
});

connection.start().then(function () {
    console.log("Connection started");
}).catch(function (err) {
    return console.error(err.toString());
});