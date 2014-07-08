Single Page Application demonstrating a fully functional chat room with MVC / JQuery timers. 

The server state (users,chat history) is persisted with a static Model instance. 
The partial views query the server state every 5 seconds. 
If a registered user does not query for 15 seconds he is considered "logged off" by the server. 

Technologies used:
- MVC4
- Razor
- JQuery

Possible improvements:
- Use seperate css for mobile devices so that everything is displayed correctly in small screens
- Don't refresh the chat history when the user has scroller the window upwards to look at earlier messages. Refreshing returns the user to the bottom of the chat history and interferes with his action.
- Possibly add a locking around the code that updates the static ChatModel instance, to avoid concurrency issues when many people are interacting with the chat

Online demo: http://chat.theokand.com/

