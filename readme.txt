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


Online demo:http://chat.oblapps.com

