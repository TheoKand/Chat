Single Page Application demonstrating a fully functional chat room with MVC / JQuery timers. 
﻿
This sample was submitted as an article to CodeProject.com:
http://www.codeproject.com/Articles/794147/MVC-Chat-room

The server state (users,chat history) is persisted with a static Model instance. 
The partial views query the server state every 5 seconds. 
If a registered user does not query for 15 seconds he is considered "logged off" by the server. 

Technologies used:
- MVC4
- Razor
- JQuery

Online demo: http://chat.theokand.com/


