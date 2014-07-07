$(document).ready(function () {

    $("#txtNickName").val("").focus();

    $("#btnLogin").click(function () {
        var nickName = $("#txtNickName").val();
        if (nickName) {
            //call the Index method of the controller and pass the attribute "logOn"
            var href = "/Chat?user=" + encodeURIComponent(nickName);
            href = href + "&logOn=true";
            $("#LoginButton").attr("href", href).click();

            //the nickname is persisted here
            $("#YourNickname").text(nickName);
        }
    });

    //auto click when enter is pressed
    $('#txtNickName').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnLogin").click();
        }
    })

});

//the login was successful. Setup events for the lobby and prepare other UI items
function LoginOnSuccess(result) {

    ScrollChat();
    ShowLastRefresh();

    $("#txtSpeak").val('').focus();

    //the chat state is fetched from the server every 5 seconds (ping)
    setTimeout("Refresh();", 5000);

    //auto post when enter is pressed
    $('#txtSpeak').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnSpeak").click();
        }
    });

    //setup the event for the "Speak" button that is rendered in the partial view 
    $("#btnSpeak").click(function () {
        var text = $("#txtSpeak").val();
        if (text) {

            //call the Index method of the controller and pass the attribute "chatMessage"
            var href = "/Chat?user=" + encodeURIComponent($("#YourNickname").text());
            href = href + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLink").attr("href", href).click();

            $("#txtSpeak").val('').focus();
        }
    });


    //setup the event for the "Speak" button that is rendered in the partial view 
    $("#btnLogOff").click(function () {

        //call the Index method of the controller and pass the attribute "logOff"
        var href = "/Chat?user=" + encodeURIComponent($("#YourNickname").text());
        href = href + "&logOff=true";
        $("#ActionLink").attr("href", href).click();

        document.location.href = "Chat";
    });

}

//briefly show login error message
function LoginOnFailure(result) {
    $("#YourNickname").val("");
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

//called every 5 seconds
function Refresh() {
    var href = "/Chat?user=" + encodeURIComponent($("#YourNickname").text());

    //call the Index method of the controller
    $("#ActionLink").attr("href", href).click();
    setTimeout("Refresh();", 5000);
}

//Briefly show the error returned by the server
function ChatOnFailure(result) {
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

//Executed when a successful communication with the server is finished
function ChatOnSuccess(result) {
    ScrollChat();
    ShowLastRefresh();
}

//scroll the chat window to the bottom
function ScrollChat() {
    var wtf = $('#ChatHistory');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
}

//show the last time the chat state was fetched from the server
function ShowLastRefresh() {
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    $("#LastRefresh").text("Last Refresh - " + time);
}

