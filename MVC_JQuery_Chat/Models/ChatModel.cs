using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_JQuery_Chat.Models
{
    public class ChatModel
    {

        /// <summary>
        /// Users that have connected to the chat
        /// </summary>
        public List<ChatUser> Users;

        /// <summary>
        /// Messages by the users
        /// </summary>
        public List<ChatMessage> ChatHistory;

        public ChatModel()
        {
            Users = new List<ChatUser>();
            ChatHistory = new List<ChatMessage>();

            ChatHistory.Add(new ChatMessage() {
                Message="The chat server started at " + DateTime.Now });
        }

        public class ChatUser
        {
            public string NickName;
            public DateTime LoggedOnTime;
            public DateTime LastPing;
        }

        public class ChatMessage
        {
            /// <summary>
            /// If null, the message is from the server
            /// </summary>
            public ChatUser ByUser;

            public DateTime When = DateTime.Now;

            public string Message = "";

        }
    }
}