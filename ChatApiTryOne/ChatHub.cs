using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace ChatApiTryOne
{
    public class ChatHub:Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
        }

        public void PrintId(string id)
        {
            Clients.All.SendAsync("printId", Context.ConnectionId);
        }

        public void JoinGroup(string name)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "foo");
        }

        public void LeaveGroup(string name)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, "foo");
        }

         public void SendMessageToGroups(string name,string message)
        {
            List<string> groups = new List<string>() { "foo" };
             Clients.Groups(groups).SendAsync("SendMessageToGroups",name, message);
        }
    }
}
