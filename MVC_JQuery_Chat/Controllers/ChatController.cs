using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_JQuery_Chat.Models;

namespace MVC_JQuery_Chat.Controllers
{
public class ChatController : Controller
{

    static ChatModel chatModel;

    /// <summary>
    /// When the method is called with no arguments, just return the view
    /// When argument logOn is true, a user logged on
    /// When argument logOff is true, a user closed their browser or navigated away (log off)
    /// When argument chatMessage is specified, the user typed something in the chat
    /// </summary>
    public ActionResult Index(string user,bool? logOn, bool? logOff, string chatMessage)
    {
        try
        {
            if (chatModel == null) chatModel = new ChatModel();
                
            //trim chat history if needed
            if (chatModel.ChatHistory.Count > 100)
                chatModel.ChatHistory.RemoveRange(0, 90);

            if (!Request.IsAjaxRequest())
            {
                //first time loading
                return View(chatModel);
            }
            else if (logOn != null && (bool)logOn)
            {
                //check if nickname already exists
                if (chatModel.Users.FirstOrDefault(u => u.NickName == user) != null)
                {
                    throw new Exception("This nickname already exists");
                }
                else if (chatModel.Users.Count > 10)
                {
                    throw new Exception("The room is full!");
                }
                else
                {
                    #region create new user and add to lobby
                    chatModel.Users.Add( new ChatModel.ChatUser()
                    {
                        NickName = user,
                        LoggedOnTime = DateTime.Now,
                        LastPing = DateTime.Now
                    });

                    //inform lobby of new user
                    chatModel.ChatHistory.Add(new ChatModel.ChatMessage()
                    {
                        Message = "User '" + user + "' logged on.",
                        When = DateTime.Now
                    });
                    #endregion

                }

                return PartialView("Lobby", chatModel);
            }
            else if (logOff != null && (bool)logOff)
            {
                LogOffUser( chatModel.Users.FirstOrDefault( u=>u.NickName==user) );
                return PartialView("Lobby", chatModel);
            }
            else
            {

                ChatModel.ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.NickName == user);

                //remember each user's last ping time
                currentUser.LastPing = DateTime.Now;

                #region remove inactive users
                List<ChatModel.ChatUser> removeThese = new List<ChatModel.ChatUser>();
                foreach (Models.ChatModel.ChatUser usr in chatModel.Users)
                {
                    TimeSpan span = DateTime.Now - usr.LastPing;
                    if (span.TotalSeconds > 15)
                        removeThese.Add(usr);
                }
                foreach (ChatModel.ChatUser usr in removeThese)
                {
                    LogOffUser(usr);
                }
                #endregion

                #region if there is a new message, append it to the chat
                if (!string.IsNullOrEmpty(chatMessage))
                {
                    chatModel.ChatHistory.Add(new ChatModel.ChatMessage()
                    {
                        ByUser = currentUser,
                        Message = chatMessage,
                        When = DateTime.Now
                    });
                }
                #endregion

                return PartialView("ChatHistory", chatModel);
            }
        }
        catch (Exception ex)
        {
            //return error to AJAX function
            Response.StatusCode = 500;
            return Content(ex.Message);
        }
    }

    /// <summary>
    /// Remove this user from the lobby and inform others that he logged off
    /// </summary>
    /// <param name="user"></param>
    public void LogOffUser(ChatModel.ChatUser user)
    {
        chatModel.Users.Remove(user);
        chatModel.ChatHistory.Add(new ChatModel.ChatMessage()
        {
            Message = "User '" + user.NickName + "' logged off.",
            When = DateTime.Now
        });
    }

}
}
